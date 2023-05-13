'' TASK TFS2375 Ayesha Rehman on 26-02-2018 Approval Hierarchy for All Transactional documents 
''TFS2988 Ayesha Rehman : 09-04-2018 : If document is approved from one stage then it should not change able for previous stage
''TFS2989 Ayesha Rehman : 10-04-2018 : If document is rejected from one stage then it should open for previous stage for approval
''TASK TFS3534 Muhammad Ameen created frmLoadPurchaseInquiryItems popup to load remaining purchase inquiry items in EDIT Mode. 13-06-2018
''TASK TFS3536 Muhammad Ameen made changes to load parent item attributes while creating aternate or child item.
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports System.Net.Mail

Public Class frmVendorQuotation
    Implements IGeneral
    Dim MasterModel As VendorQuotationMaster
    Dim DetailModel As VendorQuotationDetail
    Dim MasterDAL As New VendorQuotationMasterDAL
    Dim DetailDAL As New VendorQuotationDetailDAL
    Dim ChargesDetail As VendorQuotationDetailCharges
    Dim ExpenseModel As InwardExpenseDetailBE ''TFS3109
    Dim ExpenseModelList As List(Of InwardExpenseDetailBE) ''TFS3109
    Dim VendorQuotationId As Integer = 0
    Dim VendorQuotationDetailId As Integer = 0
    Dim HeadArticleId As Integer = 0
    Dim PurchaseInquiryDetailId As Integer = 0
    Dim SerialNo As String = String.Empty
    Dim BaseCurrencyId As Integer
    Dim BaseCurrencyName As String = String.Empty
    Dim arrFile As List(Of String)
    Dim objPath As String = String.Empty
    Dim IsAlternate As Boolean = False
    Dim CurrentRowIndex As Integer = -1
    Dim PreviousRowIndex As Integer = -1
    Dim IsFormLoaded As Boolean = False ''TFS2648
    ''TFS2375 : Ayesha Rehman : This Variable is Added to check the document state ,if it is in Eidt mode or not
    Dim IsEditMode As Boolean = False
    Dim ApprovalProcessId As Integer = 0
    ''TFS2375 : Ayesha Rehman :End
    Enum Detail
        VendorQuotationDetailId
        VendorQuotationId
        SerialNo
        RequirementDescription
        ArticleId
        Code
        ArticleDescription
        UnitId
        Unit
        ItemTypeId
        Type
        CategoryId
        Category
        SubCategoryId
        SubCategory
        OriginId
        Origin
        Qty
        QuotedTerms
        ValidityOfQuotation
        DeliveryPeriod
        Warranty
        ApproxGrossWeight
        HSCode
        DeliveryPort
        GenuineOrReplacement
        LiteratureOrDatasheet
        NewOrRefurbish
        Price
        DiscountPer
        DiscountAmount
        SalesTaxPer
        SalesTaxAmount
        AddTaxPer
        AddTaxAmount
        IncTaxPer
        IncTaxAmount
        CDPer
        CDAmount
        NetPrice
        Amount
        OtherCharges
        NetCostValue
        PurchaseInquiryDetailId
        ReferenceNo
        Comments
        HeadArticleId
        BaseCurrencyId
        BaseCurrencyRate
        CurrencyId
        Currency
        CurrencyRate
        CurrencySymbol
        Alternate
        ExWorks
    End Enum

    Private Sub frmVendorQuotation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            BaseCurrency()
            GetAllRecords()
            FillAllCombos()
            FillCombos("Customer")
            FillCombos("InquiryNumber")
            FillCombos("InwardExpense")
            FillInwardExpense(-1, "VQ")
            Me.txtTotalAmount.Text = "" 'TFS3106
            ReSetControls()
            IsFormLoaded = True ''TFS2648
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grdItems.RootTable.Columns
                c.EditType = Janus.Windows.GridEX.EditType.NoEdit
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            ''Start TFS2988
            If IsEditMode = True Then
                If ValidateApprovalProcessMapped(Me.uitxtName.Text.Trim) Then
                    If ValidateApprovalProcessInProgress(Me.uitxtName.Text.Trim) Then
                        msg_Error("Document is in Approval Process ") : Exit Function
                    End If
                End If
            End If
            ''End TFS2988
            FillModel()
            Try
                If MasterDAL.Delete(MasterModel) Then
                    SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Purchase, Me.uitxtName.Text.Trim, True)
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try

            Dim strSQL As String

            If Condition = "" Or Condition = "Customer" Then
                strSQL = String.Empty
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Vendor', 'Customer') and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbReference, strSQL)
                    Me.cmbReference.Rows(0).Activate()
                    Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type = 'Vendor' and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbReference, strSQL)
                    Me.cmbReference.Rows(0).Activate()
                    Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                End If
            ElseIf Condition = "Type" Then
                strSQL = "select ArticleTypeId as Id, ArticleTypeName as Name, TypeCode from ArticleTypeDefTable where active=1 order by sortOrder"
                FillUltraDropDownType(Me.cmbType, strSQL)
                If Me.cmbType.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbType.Rows(0).Activate()
                    Me.cmbType.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If

            ElseIf Condition = "Origin" Then
                strSQL = "select ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable where active=1 order by sortOrder"
                FillUltraDropDownOrigin(Me.cmbOrigin, strSQL)
                If Me.cmbOrigin.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbOrigin.Rows(0).Activate()
                    Me.cmbOrigin.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
            ElseIf Condition = "Unit" Then
                ''filling Unit combo
                strSQL = "select ArticleUnitId as Id, ArticleUnitName as Name from ArticleUnitDefTable where active=1 order by sortOrder"
                'strSQL = "Select Distinct ArticlePackId, PackName As Name, PackQty, ArticleMasterId From ArticleDefPackTable order by ArticlePackId"
                FillUltraDropDownUnit(Me.cmbUnit, strSQL)
                If Me.cmbUnit.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbUnit.Rows(0).Activate()
                    Me.cmbUnit.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
            ElseIf Condition = "SubCategory" Then
                strSQL = "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID, dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId , dbo.ArticleLpoDefTable.SubCategoryCode  FROM   dbo.ArticleLpoDefTable INNER JOIN dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId"
                FillUltraDropDownSubCategory(Me.cmbSubCategory, strSQL)
                If Me.cmbSubCategory.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbSubCategory.Rows(0).Activate()
                    Me.cmbSubCategory.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                End If
            ElseIf Condition = "Category" Then
                strSQL = "SELECT  ArticleCompanyId AS ID, ArticleCompanyName AS Name, CategoryCode" & _
                    " FROM ArticleCompanyDefTable"
                FillUltraDropDownCategory(Me.cmbCategory, strSQL)
                If Me.cmbCategory.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbCategory.Rows(0).Activate()
                    Me.cmbCategory.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                End If

            ElseIf Condition = "TypeAgainstItem" Then
                strSQL = "select ArticleTypeId as Id, ArticleTypeName as Name, TypeCode from ArticleTypeDefTable where ArticleTypeId=" & Val(Me.cmbItem.ActiveRow.Cells("ArticleTypeId").Value.ToString) & " And active=1 order by sortOrder"
                FillUltraDropDownType(Me.cmbType, strSQL)
                If Me.cmbType.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbType.Rows(0).Activate()
                    Me.cmbType.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If

            ElseIf Condition = "OriginAgainstItem" Then
                strSQL = "select ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable where  ArticleGenderId=" & Val(Me.cmbItem.ActiveRow.Cells("ArticleGenderId").Value.ToString) & " And active=1 order by sortOrder"
                FillUltraDropDownOrigin(Me.cmbOrigin, strSQL)
                If Me.cmbOrigin.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbOrigin.Rows(0).Activate()
                    Me.cmbOrigin.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
            ElseIf Condition = "UnitAgainstItem" Then
                ''filling Unit combo
                strSQL = "select ArticleUnitId as Id, ArticleUnitName as Name from ArticleUnitDefTable where ArticleUnitId=" & Val(Me.cmbItem.ActiveRow.Cells("ArticleUnitId").Value.ToString) & " And active=1 order by sortOrder"
                'strSQL = "Select Distinct ArticlePackId, PackName As Name, PackQty, ArticleMasterId From ArticleDefPackTable order by ArticlePackId"
                FillUltraDropDownUnit(Me.cmbUnit, strSQL)
                If Me.cmbUnit.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbUnit.Rows(0).Activate()
                    Me.cmbUnit.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
            ElseIf Condition = "SubCategoryAgainstItem" Then
                strSQL = "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID, dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId , dbo.ArticleLpoDefTable.SubCategoryCode  FROM   dbo.ArticleLpoDefTable INNER JOIN dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId Where dbo.ArticleLpoDefTable.ArticleLpoId =" & Val(Me.cmbItem.ActiveRow.Cells("ArticleLPOId").Value.ToString) & ""
                FillUltraDropDownSubCategory(Me.cmbSubCategory, strSQL)
                If Me.cmbSubCategory.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbSubCategory.Rows(0).Activate()
                    Me.cmbSubCategory.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                End If
            ElseIf Condition = "CategoryAgainstItem" Then
                strSQL = "SELECT  ArticleCompanyId AS ID, ArticleCompanyName AS Name Where ArticleCompanyId =" & Val(Me.cmbItem.ActiveRow.Cells("ArticleCategoryId").Value.ToString) & "" & _
                    " FROM ArticleCompanyDefTable"
                FillUltraDropDownCategory(Me.cmbCategory, strSQL)
                If Me.cmbCategory.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbCategory.Rows(0).Activate()
                    Me.cmbCategory.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                End If
            ElseIf Condition = "Item" Then
                'FillUltraDropDown(Me.cmbItem, "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination,Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SalePrice,0) as Price, ArticleDefView.SizeRangeID as [Size ID], ArticleDefView.MasterId FROM ArticleDefView where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & "")
                FillUltraDropDownItem(Me.cmbItem, "SELECT ArticleDefTable.ArticleId as Id, ArticleDefTable.ArticleDescription as Item, ArticleDefTable.ArticleCode as Code, dbo.ArticleSizeDefTable.ArticleSizeName as Size, dbo.ArticleColorDefTable.ArticleColorName as Combination, Isnull(ArticleDefTable.PurchasePrice,0) as PurchasePrice, Isnull(ArticleDefTable.SalePrice,0) as Price, ArticleDefTable.SizeRangeID as [Size ID], ArticleDefTable.MasterId, ArticleDefTable.ArticleGenderId, ArticleDefTable.ArticleCategoryId, ArticleDefTable.ArticleLPOId, ArticleDefTable.ArticleUnitId, ArticleDefTable.ArticleTypeId FROM ArticleDefTable Left Join dbo.ArticleColorDefTable ON ArticleDefTable.ArticleColorId = dbo.ArticleColorDefTable.ArticleColorId Left Join dbo.ArticleSizeDefTable On ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId Where ArticleDefTable.Active=1 ")
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleGenderId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleCategoryId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleLPOId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleUnitId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleTypeId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Header.Caption = "Price"
            ElseIf Condition = "InquiryNumber" Then
                strSQL = " Select PurchaseInquiryId, PurchaseInquiryNo, PurchaseInquiryDate From PurchaseInquiryMaster where IsNull(Posted,0) = 1 Order By PurchaseInquiryDate DESC "
                FillUltraDropDown(Me.cmbPurchaseInquiry, strSQL)
                Me.cmbPurchaseInquiry.Rows(0).Activate()
                Me.cmbPurchaseInquiry.DisplayLayout.Bands(0).Columns("PurchaseInquiryId").Hidden = True
            ElseIf Condition = "InquiryNumberAgainstCustomer" Then
                strSQL = " Select PurchaseInquiryMaster.PurchaseInquiryId, PurchaseInquiryMaster.PurchaseInquiryNo, PurchaseInquiryMaster.PurchaseInquiryDate From PurchaseInquiryMaster INNER JOIN PurchaseInquiryVendors ON PurchaseInquiryMaster.PurchaseInquiryId = PurchaseInquiryVendors.PurchaseInquiryId Where PurchaseInquiryVendors.VendorId =" & Val(Me.cmbReference.Value.ToString) & " And IsNull(PurchaseInquiryMaster.Posted,0) = 1 and PurchaseInquiryMaster.PurchaseInquiryDate >= Convert(Datetime, N'" & Me.dtpVQDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND PurchaseInquiryMaster.PurchaseInquiryDate <= Convert(Datetime, N'" & Me.dtpExpiryDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102) Order By PurchaseInquiryMaster.PurchaseInquiryDate DESC"
                FillUltraDropDown(Me.cmbPurchaseInquiry, strSQL)
                Me.cmbPurchaseInquiry.Rows(0).Activate()
                Me.cmbPurchaseInquiry.DisplayLayout.Bands(0).Columns("PurchaseInquiryId").Hidden = True
            ElseIf Condition = "Currency" Then ''TASK-407
                strSQL = String.Empty
                strSQL = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
                FillDropDown(Me.cmbCurrency, strSQL, False)
                Me.cmbCurrency.SelectedValue = BaseCurrencyId
                'ElseIf Condition = "Location" Then
                '    strSQL = "SELECT  LocationId AS ID, LocationTitle AS Name " & _
                '        " FROM tblDefCompanyLocations"
                '    FillUltraDropDown(Me.cmbCompanyLocation, strSQL)
                '    If Me.cmbCompanyLocation.DisplayLayout.Bands.Count > 0 Then
                '        Me.cmbCompanyLocation.Rows(0).Activate()
                '        Me.cmbCompanyLocation.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                '    End If
                'ElseIf Condition = "ContactPerson" Then
                '    strSQL = "SELECT  PK_Id AS ID, ContactName AS Name, RefCompanyId" & _
                '        " FROM TblCompanyContacts"
                '    FillUltraDropDown(Me.cmbContactPerson, strSQL)
                '    If Me.cmbContactPerson.DisplayLayout.Bands.Count > 0 Then
                '        Me.cmbContactPerson.Rows(0).Activate()
                '        Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                '        Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Name").Hidden = True
                '    End If

                'ElseIf Condition = "IndentDepartment" Then
                '    strSQL = "SELECT Distinct IndentingDepartment, IndentingDepartment " & _
                '        " FROM PurchaseInquiryMaster"
                '    FillDropDown(Me.cmbIndentDept, strSQL)
                'End If
                'ElseIf Condition = "Customer" Then
                '    strSQL = "Select coa_detail_id, detail_title From vwCOADetail WHERE account_type='Vendor'"
                '    FillUltraDropDown(Me.cmbReference, strSQL)
                '    If Me.cmbReference.DisplayLayout.Bands.Count > 0 Then
                '        Me.cmbReference.Rows(0).Activate()
                '    End If
                'Ali Faisal : TFS1310 : fill combos to set values in dropdowns

            ElseIf Condition = "ExWorks" Then
                strSQL = "Select Distinct ExWorks, ExWorks from VendorQuotationDetail Where ExWorks is not null"
                FillUltraDropDown(Me.cmbExWorks, strSQL, False)
                If Me.cmbExWorks.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbExWorks.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbExWorks.DisplayLayout.Bands(0).Columns(0).Width = 200
                End If
            ElseIf Condition = "DeliveryPort" Then
                strSQL = "Select Distinct DeliveryPort, DeliveryPort from VendorQuotationDetail Where DeliveryPort is not null"
                FillUltraDropDown(Me.cmbDelivery, strSQL, False)
                If Me.cmbDelivery.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbDelivery.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbDelivery.DisplayLayout.Bands(0).Columns(0).Width = 200
                End If
            ElseIf Condition = "GenuineOrReplacement" Then
                strSQL = "Select Distinct GenuineOrReplacement, GenuineOrReplacement from VendorQuotationDetail Where GenuineOrReplacement is not null"
                FillUltraDropDown(Me.cmbGenuine, strSQL, False)
                If Me.cmbGenuine.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbGenuine.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbGenuine.DisplayLayout.Bands(0).Columns(0).Width = 200
                End If
            ElseIf Condition = "LiteratureOrDatasheet" Then
                strSQL = "Select Distinct LiteratureOrDatasheet, LiteratureOrDatasheet from VendorQuotationDetail Where LiteratureOrDatasheet is not null"
                FillUltraDropDown(Me.cmbLiterature, strSQL, False)
                If Me.cmbLiterature.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbLiterature.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbLiterature.DisplayLayout.Bands(0).Columns(0).Width = 200
                End If
            ElseIf Condition = "NewOrRefurbish" Then
                strSQL = "Select Distinct NewOrRefurbish, NewOrRefurbish from VendorQuotationDetail Where NewOrRefurbish is not null"
                FillUltraDropDown(Me.cmbRefurbish, strSQL, False)
                If Me.cmbRefurbish.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbRefurbish.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbRefurbish.DisplayLayout.Bands(0).Columns(0).Width = 200
                End If
                'Ali Faisal : TFS1310 : End
            ElseIf Condition = "InwardExpense" Then
                FillUltraDropDown(Me.cmbInwardExpense, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], sub_sub_title as [Account Head], Account_Type as [Account Type] From vwCOADetail where detail_title <> ''")
                Me.cmbInwardExpense.Rows(0).Activate()
                If Me.cmbInwardExpense.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbInwardExpense.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
    '    Try
    '        MasterModel = New VendorQuotationMaster()
    '        MasterModel.VendorQuotationId = VendorQuotationId
    '        MasterModel.VendorQuotationNo = Me.uitxtName.Text
    '        MasterModel.VendorQuotationDate = Me.dtpVQDate.Value
    '        MasterModel.VendorQuotationExpiryDate = Me.dtpExpiryDate.Value
    '        MasterModel.VendorId = Me.cmbReference.Value
    '        MasterModel.ReferenceNo = Me.txtRefNo.Text
    '        MasterModel.PurchaseInquiryId = Me.cmbPurchaseInquiry.Value
    '        MasterModel.Remarks = Me.txtRemarks.Text
    '        MasterModel.UserName = LoginUserName
    '        For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetDataRows
    '            DetailModel = New VendorQuotationDetail()
    '            DetailModel.VendorQuotationDetailId = Val(Row.Cells("VendorQuotationDetailId").Value.ToString)
    '            DetailModel.SerialNo = Row.Cells("SerialNo").Value.ToString
    '            DetailModel.RequirementDescription = Row.Cells("RequirementDescription").Value.ToString
    '            DetailModel.ArticleId = Val(Row.Cells("ArticleId").Value.ToString)
    '            'DetailModel.Code = Row.Cells("Code").Value.ToString
    '            'DetailModel.ArticleDescription = Row.Cells("ArticleDescription").Value.ToString
    '            DetailModel.UnitId = Val(Row.Cells("UnitId").Value.ToString)
    '            DetailModel.ItemTypeId = Val(Row.Cells("ItemTypeId").Value.ToString)
    '            DetailModel.CategoryId = Val(Row.Cells("CategoryId").Value.ToString)
    '            DetailModel.SubCategoryId = Val(Row.Cells("SubCategoryId").Value.ToString)
    '            DetailModel.OriginId = Val(Row.Cells("OriginId").Value.ToString)
    '            DetailModel.Qty = Val(Row.Cells("Qty").Value.ToString)
    '            DetailModel.QuotedTerms = Row.Cells("QuotedTerms").Value.ToString
    '            DetailModel.ValidityOfQuotation = Row.Cells("ValidityOfQuotation").Value.ToString
    '            DetailModel.DeliveryPeriod = Row.Cells("DeliveryPeriod").Value.ToString
    '            DetailModel.Warranty = Row.Cells("Warranty").Value.ToString
    '            DetailModel.ApproxGrossWeight = Row.Cells("ApproxGrossWeight").Value.ToString
    '            DetailModel.HSCode = Row.Cells("HSCode").Value.ToString
    '            DetailModel.DeliveryPort = Row.Cells("ApproxGrossWeight").Value.ToString
    '            DetailModel.GenuineOrReplacement = Row.Cells("GenuineOrReplacement").Value.ToString
    '            DetailModel.LiteratureOrDatasheet = Row.Cells("LiteratureOrDatasheet").Value.ToString
    '            DetailModel.NewOrRefurbish = Row.Cells("NewOrRefurbish").Value.ToString
    '            DetailModel.Price = Val(Row.Cells("Price").Value.ToString)
    '            DetailModel.DiscountPer = Row.Cells("DiscountPer").Value
    '            'DetailModel.DiscountAmount = Val(Row.Cells("DiscountAmount").Value.ToString)
    '            DetailModel.SalesTaxPer = Row.Cells("SalesTaxPer").Value
    '            DetailModel.AddTaxPer = Row.Cells("AddTaxPer").Value
    '            DetailModel.IncTaxPer = Row.Cells("IncTaxPer").Value
    '            DetailModel.CDPer = Row.Cells("CDPer").Value
    '            DetailModel.NetPrice = Row.Cells("NetPrice").Value
    '            DetailModel.OtherCharges = Row.Cells("OtherCharges").Value
    '            DetailModel.PurchaseInquiryDetailId = Row.Cells("PurchaseInquiryDetailId").Value
    '            DetailModel.ReferenceNo = Row.Cells("ReferenceNo").Value.ToString
    '            DetailModel.Comments = Row.Cells("Comments").Value.ToString
    '            DetailModel.HeadArticleId = Val(Row.Cells("HeadArticleId").Value.ToString)
    '            MasterModel.DetailList.Add(DetailModel)
    '            'NetPrice()
    '            'Amount()
    '            'OtherCharges()
    '            'NetCostValue()
    '            'PurchaseInquiryDetailId()
    '            'ReferenceNo()
    '            'Comments()
    '            'HeadArticleId()
    '        Next

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Public Sub FillMasterModel(Optional Condition As String = "")
        Try
            MasterModel = New VendorQuotationMaster()
            MasterModel.VendorQuotationId = VendorQuotationId
            MasterModel.VendorQuotationNo = Me.uitxtName.Text
            MasterModel.VendorQuotationDate = Me.dtpVQDate.Value
            MasterModel.VendorQuotationExpiryDate = Me.dtpExpiryDate.Value
            MasterModel.VendorId = Me.cmbReference.Value
            MasterModel.ReferenceNo = Me.txtRefNo.Text
            MasterModel.PurchaseInquiryId = Me.cmbPurchaseInquiry.Value
            MasterModel.Remarks = Me.txtRemarks.Text
            MasterModel.UserName = LoginUserName
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetDataRows
            '    DetailModel = New VendorQuotationDetail()
            '    DetailModel.VendorQuotationDetailId = Val(Row.Cells("VendorQuotationDetailId").Value.ToString)
            '    DetailModel.SerialNo = Row.Cells("SerialNo").Value.ToString
            '    DetailModel.RequirementDescription = Row.Cells("RequirementDescription").Value.ToString
            '    DetailModel.ArticleId = Val(Row.Cells("ArticleId").Value.ToString)
            '    'DetailModel.Code = Row.Cells("Code").Value.ToString
            '    'DetailModel.ArticleDescription = Row.Cells("ArticleDescription").Value.ToString
            '    DetailModel.UnitId = Val(Row.Cells("UnitId").Value.ToString)
            '    DetailModel.ItemTypeId = Val(Row.Cells("ItemTypeId").Value.ToString)
            '    DetailModel.CategoryId = Val(Row.Cells("CategoryId").Value.ToString)
            '    DetailModel.SubCategoryId = Val(Row.Cells("SubCategoryId").Value.ToString)
            '    DetailModel.OriginId = Val(Row.Cells("OriginId").Value.ToString)
            '    DetailModel.Qty = Val(Row.Cells("Qty").Value.ToString)
            '    DetailModel.QuotedTerms = Row.Cells("QuotedTerms").Value.ToString
            '    DetailModel.ValidityOfQuotation = Row.Cells("ValidityOfQuotation").Value.ToString
            '    DetailModel.DeliveryPeriod = Row.Cells("DeliveryPeriod").Value.ToString
            '    DetailModel.Warranty = Row.Cells("Warranty").Value.ToString
            '    DetailModel.ApproxGrossWeight = Row.Cells("ApproxGrossWeight").Value.ToString
            '    DetailModel.HSCode = Row.Cells("HSCode").Value.ToString
            '    DetailModel.DeliveryPort = Row.Cells("ApproxGrossWeight").Value.ToString
            '    DetailModel.GenuineOrReplacement = Row.Cells("GenuineOrReplacement").Value.ToString
            '    DetailModel.LiteratureOrDatasheet = Row.Cells("LiteratureOrDatasheet").Value.ToString
            '    DetailModel.NewOrRefurbish = Row.Cells("NewOrRefurbish").Value.ToString
            '    DetailModel.Price = Val(Row.Cells("Price").Value.ToString)
            '    DetailModel.DiscountPer = Row.Cells("DiscountPer").Value
            '    'DetailModel.DiscountAmount = Val(Row.Cells("DiscountAmount").Value.ToString)
            '    DetailModel.SalesTaxPer = Row.Cells("SalesTaxPer").Value
            '    DetailModel.AddTaxPer = Row.Cells("AddTaxPer").Value
            '    DetailModel.IncTaxPer = Row.Cells("IncTaxPer").Value
            '    DetailModel.CDPer = Row.Cells("CDPer").Value
            '    DetailModel.NetPrice = Row.Cells("NetPrice").Value
            '    DetailModel.OtherCharges = Row.Cells("OtherCharges").Value
            '    DetailModel.PurchaseInquiryDetailId = Row.Cells("PurchaseInquiryDetailId").Value
            '    DetailModel.ReferenceNo = Row.Cells("ReferenceNo").Value.ToString
            '    DetailModel.Comments = Row.Cells("Comments").Value.ToString
            '    DetailModel.HeadArticleId = Val(Row.Cells("HeadArticleId").Value.ToString)
            '    MasterModel.DetailList.Add(DetailModel)
            'NetPrice()
            'Amount()
            'OtherCharges()
            'NetCostValue()
            'PurchaseInquiryDetailId()
            'ReferenceNo()
            'Comments()
            'HeadArticleId()
            'Next

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillDetailModel(Optional Condition As String = "")
        Try
            MasterModel = New VendorQuotationMaster()
            MasterModel.VendorQuotationId = VendorQuotationId
            MasterModel.VendorQuotationNo = Me.uitxtName.Text
            MasterModel.VendorQuotationDate = Me.dtpVQDate.Value
            MasterModel.VendorQuotationExpiryDate = Me.dtpExpiryDate.Value
            MasterModel.VendorId = Me.cmbReference.Value
            MasterModel.ReferenceNo = Me.txtRefNo.Text
            MasterModel.PurchaseInquiryId = Me.cmbPurchaseInquiry.Value
            MasterModel.Remarks = Me.txtRemarks.Text
            MasterModel.UserName = LoginUserName

            ''Start TFS3106
            MasterModel.Amount = Val(Me.txtTotalAmount.Text)
            MasterModel.Discount = Val(Me.txtNetDiscount.Text)
            MasterModel.NetTotal = Val(Me.txtNetTotal.Text)
            ''End TFS3106


            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetRows
            DetailModel = New VendorQuotationDetail()
            DetailModel.VendorQuotationDetailId = VendorQuotationDetailId
            DetailModel.VendorQuotationId = VendorQuotationId

            DetailModel.SerialNo = SerialNo
            DetailModel.RequirementDescription = Me.txtRequirementDescription.Text
            DetailModel.ArticleId = Me.cmbItem.Value
            'DetailModel.Code = Row.Cells("Code").Value.ToString
            'DetailModel.ArticleDescription = Row.Cells("ArticleDescription").Value.ToString
            DetailModel.UnitId = Me.cmbUnit.Value
            DetailModel.ItemTypeId = Me.cmbType.Value
            DetailModel.CategoryId = Me.cmbCategory.Value
            DetailModel.SubCategoryId = Me.cmbSubCategory.Value
            DetailModel.OriginId = Me.cmbOrigin.Value
            DetailModel.Qty = Val(Me.txtQty.Text)
            DetailModel.QuotedTerms = Me.txtQuotation.Text
            DetailModel.ValidityOfQuotation = Me.txtValidityOfQuotation.Text
            DetailModel.DeliveryPeriod = Me.txtDeliveryPeriod.Text
            DetailModel.Warranty = Me.txtWarranty.Text
            DetailModel.ApproxGrossWeight = Me.txtApproxgrossweight.Text
            DetailModel.HSCode = txtHSCode.Text
            DetailModel.DeliveryPort = Me.cmbDelivery.Text
            DetailModel.GenuineOrReplacement = Me.cmbGenuine.Text
            DetailModel.LiteratureOrDatasheet = Me.cmbLiterature.Text
            DetailModel.NewOrRefurbish = Me.cmbRefurbish.Text
            DetailModel.ExWorks = Me.cmbExWorks.Text
            DetailModel.Price = Val(Me.txtPrice.Text)
            DetailModel.DiscountPer = Val(Me.txtDiscount.Text)
            'DetailModel.DiscountAmount = Val(Row.Cells("DiscountAmount").Value.ToString)
            DetailModel.SalesTaxPer = Val(Me.txtSaleTax.Text)
            DetailModel.AddTaxPer = Val(Me.txtAddTax.Text)
            DetailModel.IncTaxPer = Val(Me.txtIncTax.Text)
            DetailModel.CDPer = Val(Me.txtCD.Text)
            DetailModel.NetPrice = Val(Me.txtNetPrice.Text)
            DetailModel.OtherCharges = Val(Me.txtCharges.Text)
            DetailModel.PurchaseInquiryDetailId = PurchaseInquiryDetailId
            DetailModel.ReferenceNo = Me.txtReferenceNo.Text
            DetailModel.Comments = Me.txtComments.Text
            DetailModel.HeadArticleId = HeadArticleId
            DetailModel.BaseCurrencyId = BaseCurrencyId
            DetailModel.BaseCurrencyRate = 1
            DetailModel.CurrencyId = Me.cmbCurrency.SelectedValue
            DetailModel.CurrencyRate = Val(Me.txtCurrencyRate.Text)
            DetailModel.CurrencySymbol = Me.cmbCurrency.Text
            DetailModel.IsAlternate = IsAlternate
            grdCharges.UpdateData()
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdCharges.GetRows
                If Val(Row.Cells("Amount").Value.ToString) > 0 Then
                    Dim DetailCharges As VendorQuotationDetailCharges = New VendorQuotationDetailCharges
                    DetailCharges.VendorQuotationDetailId = VendorQuotationDetailId
                    DetailCharges.VendorQuotationChargesId = Val(Row.Cells("VendorQuotationDetailChargesId").Value.ToString)
                    DetailCharges.VendorQuotationChargesTypeId = Val(Row.Cells("VendorQuotationChargesTypeId").Value.ToString)
                    DetailCharges.Amount = Val(Row.Cells("Amount").Value.ToString)
                    DetailModel.ChargesDetail.Add(DetailCharges)
                End If
            Next
            MasterModel.DetailList.Add(DetailModel)
            ''Start TFS3109
            ExpenseModelList = New List(Of InwardExpenseDetailBE)
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdInwardExpDetail.GetRows
                If Val(r.Cells("Exp_Amount").Value.ToString) <> 0 Then
                    Dim InwardExpenseDetail As InwardExpenseDetailBE = New InwardExpenseDetailBE
                    InwardExpenseDetail.AccountId = Val(r.Cells("AccountId").Value.ToString)
                    InwardExpenseDetail.Exp_Amount = Val(r.Cells("Exp_Amount").Value.ToString)
                    InwardExpenseDetail.DocType = "VQ"
                    ExpenseModelList.Add(InwardExpenseDetail)
                End If
            Next
            ''End TFS3109
            'NetPrice()
            'Amount()
            'OtherCharges()
            'NetCostValue()
            'PurchaseInquiryDetailId()
            'ReferenceNo()
            'Comments()
            'HeadArticleId()
            'Next

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If LoginUserName = "Admin" Or LoginUserName = "Administrator" Then
                Me.grdSaved.DataSource = MasterDAL.GetAllRecords(Me.Name)
            Else
                Me.grdSaved.DataSource = MasterDAL.GetAllRecords(Me.Name, LoginUserName)
            End If
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("VendorQuotationId").Visible = False
            Me.grdSaved.RootTable.Columns("VendorId").Visible = False
            Me.grdSaved.RootTable.Columns("PurchaseInquiryId").Visible = False
            Me.grdSaved.RootTable.Columns("VendorQuotationNo").Caption = "Vendor Quotation No" 'PurchaseInquiryNo
            Me.grdSaved.RootTable.Columns("PurchaseInquiryNo").Caption = "Purchase Inquiry No" 'PurchaseInquiryNo
            Me.grdSaved.RootTable.Columns("VendorQuotationDate").Caption = "Vendor Quotation Date"
            Me.grdSaved.RootTable.Columns("VendorQuotationExpiryDate").Caption = "Expiry Date"
            Me.grdSaved.RootTable.Columns("ReferenceNo").Caption = "Reference Number"
            Me.grdSaved.RootTable.Columns("VendorQuotationDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("VendorQuotationExpiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("No Of Attachments").ColumnType = Janus.Windows.GridEX.ColumnType.Link

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetTop50(Optional Condition As String = "")
        Try
            Me.grdSaved.DataSource = MasterDAL.GetTop50(Me.Name)
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("VendorQuotationId").Visible = False
            Me.grdSaved.RootTable.Columns("VendorId").Visible = False
            Me.grdSaved.RootTable.Columns("PurchaseInquiryId").Visible = False
            Me.grdSaved.RootTable.Columns("VendorQuotationNo").Caption = "Vendor Quotation No" 'PurchaseInquiryNo
            Me.grdSaved.RootTable.Columns("PurchaseInquiryNo").Caption = "Purchase Inquiry No" 'PurchaseInquiryNo
            Me.grdSaved.RootTable.Columns("VendorQuotationDate").Caption = "Vendor Quotation Date"
            Me.grdSaved.RootTable.Columns("VendorQuotationExpiryDate").Caption = "Expiry Date"
            Me.grdSaved.RootTable.Columns("ReferenceNo").Caption = "Reference Number"
            Me.grdSaved.RootTable.Columns("VendorQuotationDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("VendorQuotationExpiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("No Of Attachments").ColumnType = Janus.Windows.GridEX.ColumnType.Link

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAll(Optional Condition As String = "")
        Try
            Me.grdSaved.DataSource = MasterDAL.GetAll(Me.Name)
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("VendorQuotationId").Visible = False
            Me.grdSaved.RootTable.Columns("VendorId").Visible = False
            Me.grdSaved.RootTable.Columns("PurchaseInquiryId").Visible = False
            Me.grdSaved.RootTable.Columns("VendorQuotationNo").Caption = "Vendor Quotation No" 'PurchaseInquiryNo
            Me.grdSaved.RootTable.Columns("PurchaseInquiryNo").Caption = "Purchase Inquiry No" 'PurchaseInquiryNo
            Me.grdSaved.RootTable.Columns("VendorQuotationDate").Caption = "Vendor Quotation Date"
            Me.grdSaved.RootTable.Columns("VendorQuotationExpiryDate").Caption = "Expiry Date"
            Me.grdSaved.RootTable.Columns("ReferenceNo").Caption = "Reference Number"
            Me.grdSaved.RootTable.Columns("VendorQuotationDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("VendorQuotationExpiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("No Of Attachments").ColumnType = Janus.Windows.GridEX.ColumnType.Link

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' TFS2648 : Ayesha Rehman : This function is added to get the document in edit mode when open from anotherside
    ''' </summary>
    ''' <param name="VendorQuotationNo"></param>
    ''' <returns></returns>
    ''' <remarks>TFS2648 : Ayesha Rehman : 07-03-2018</remarks>
    Public Function Get_All(ByVal VendorQuotationNo As String)
        Try
            Get_All = Nothing
            If IsFormLoaded = True Then
                If VendorQuotationNo.Length > 0 Then
                    Dim str As String = "Select * from VendorQuotationMaster  where VendorQuotationNo=N'" & VendorQuotationNo & "'"
                    Dim dt As DataTable = GetDataTable(str)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then

                            GetAllRecords()
                            ''This LOC is Added to check if Record exist agianst the login user
                            If Not Me.grdSaved.RecordCount > 0 Then Exit Function
                            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
                            IsEditMode = True
                            If Not getConfigValueByType("VendorQuotationApproval") = "Error" Then
                                ApprovalProcessId = getConfigValueByType("VendorQuotationApproval")
                            End If
                            If ApprovalProcessId = 0 Then
                                Me.btnApprovalHistory.Visible = False
                                Me.btnApprovalHistory.Enabled = False
                            Else
                                Me.btnApprovalHistory.Visible = True
                                Me.btnApprovalHistory.Enabled = True
                            End If
                            ''Ayesha Rehman :TFS2375 :End
                            Me.uitxtName.Text = Me.grdSaved.CurrentRow.Cells("VendorQuotationNo").Value.ToString
                            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells("Remarks").Value.ToString
                            Me.txtReferenceNo.Text = Me.grdSaved.CurrentRow.Cells("ReferenceNo").Value.ToString
                            If IsDBNull(Me.grdSaved.CurrentRow.Cells("VendorQuotationDate").Value) Then
                                Me.dtpVQDate.Value = Now
                            Else
                                Me.dtpVQDate.Value = Me.grdSaved.CurrentRow.Cells("VendorQuotationDate").Value
                            End If
                            If IsDBNull(Me.grdSaved.CurrentRow.Cells("VendorQuotationExpiryDate").Value) Then
                                Me.dtpExpiryDate.Value = Now
                                'Me.dtpOldInquiryDate.Value = Now
                            Else
                                Me.dtpExpiryDate.Value = Me.grdSaved.CurrentRow.Cells("VendorQuotationExpiryDate").Value

                            End If
                            Me.cmbReference.Value = Val(Me.grdSaved.CurrentRow.Cells("VendorId").Value.ToString)
                            'Me.cmbDelivery.Text = Me.grdSaved.CurrentRow.Cells("DeliveryPort").Value.ToString
                            'Me.cmbGenuine.Text = Me.grdSaved.CurrentRow.Cells("GenuineOrReplacement").Value.ToString
                            'Me.cmbLiterature.Text = Me.grdSaved.CurrentRow.Cells("LiteratureOrDatasheet").Value.ToString
                            'Me.cmbRefurbish.Text = Me.grdSaved.CurrentRow.Cells("NewOrRefurbish").Value.ToString
                            'Me.cmbReference.Enabled = False
                            Me.cmbPurchaseInquiry.Value = Val(Me.grdSaved.CurrentRow.Cells("PurchaseInquiryId").Value.ToString)
                            'Me.cmbPurchaseInquiry.Enabled = False
                            VendorQuotationId = Val(Me.grdSaved.CurrentRow.Cells("VendorQuotationId").Value.ToString)
                            Me.btnAttachment.Text = "Attachment (" & Me.grdSaved.GetRow.Cells("No Of Attachments").Value.ToString & ")"
                            DisplayDetailForHistory(VendorQuotationId)
                            Me.BtnSave.Text = "&Update"
                            'Me.btnSave1.Text = "&Update"
                            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                        Else
                            Exit Function
                        End If
                    Else
                        Exit Function
                    End If
                End If
                IsDrillDown = False
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            'Dim dt As DataTable = Me.grdItems.DataSource
            Me.grdItems.UpdateData()
            If Me.cmbReference.Value <= 0 Then
                msg_Error("Customer is required")
                Me.cmbReference.Focus() : IsValidate = False : Exit Function
            End If
            'If MasterDAL.IsDetailExists(Me.uitxtName.Text) = False Then
            '    msg_Error("At least one row of detail record is required to save.")
            '    IsValidate = False : Exit Function
            'End If
            If Not grdItems.RowCount > 0 Then
                msg_Error("Items grid is empty")
                Me.grdItems.Focus() : IsValidate = False : Exit Function
            End If
            objPath = getConfigValueByType("FileAttachmentPath").ToString
            'FillMasterModel()
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidateDetail(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean
        Try
            If Val(Me.txtQty.Text) <= 0 Then
                msg_Error("Qty is required")
                Me.txtQty.Focus() : IsValidateDetail = False : Exit Function
            End If
            If cmbItem.Value = 0 AndAlso Me.txtRequirementDescription.Text = "" Then
                msg_Error("Item description is required")
                Me.cmbItem.Focus() : IsValidateDetail = False : Exit Function
            End If
            If IsEditMode = True Then
                If ValidateApprovalProcessMapped(Me.uitxtName.Text.Trim) Then
                    If ValidateApprovalProcessInProgress(Me.uitxtName.Text.Trim) Then
                        msg_Error("Document is in Approval Process ") : Exit Function
                    End If
                End If
            End If
            objPath = getConfigValueByType("FileAttachmentPath").ToString

            FillDetailModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.uitxtName.Text = GetDocumentNo()
            GetSecurityRights()
            Me.txtRefNo.Text = ""
            Me.txtRemarks.Text = ""
            Me.txtComments.Text = ""
            Me.BtnSave.Text = "&Save"
            Me.btnSave1.Text = "&Save"
            Me.dtpVQDate.Value = Now
            Me.dtpExpiryDate.Value = Now
            Me.cmbReference.Rows(0).Activate()
            FillInwardExpense(-1, "VQ")
            FillCombos("InquiryNumberAgainstCustomer")
            Me.cmbCurrency.SelectedValue = BaseCurrencyId
            If Not Me.cmbPurchaseInquiry.ActiveRow Is Nothing Then
                Me.cmbPurchaseInquiry.Rows(0).Activate()
            End If
            arrFile = New List(Of String)
            Me.btnAttachment.Text = "Attachment"
            Me.cmbReference.Enabled = True
            Me.cmbPurchaseInquiry.Enabled = True
            'Ali Faisal : TFS1314 : Add new button and make it invisible in start mode
            Me.btnGetChargesTypes.Visible = False
            'Ayesha Rehman : TFS2375 : Enable Approval History button only in Eidt Mode
            If IsEditMode = True Then
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            Else
                Me.btnApprovalHistory.Visible = False
            End If
            'Ayesha Rehman : TFS2375 : End

            IsEditMode = False
            Me.btnAddPurchaseInquiryItems.Enabled = False
            GetAllRecords()
            DisplayDetailForHistory(-1)
            ResetDetailControls()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            MasterModel.VendorQuotationNo = GetDocumentNo()
            VendorQuotationId = MasterDAL.Add(MasterModel, Me.Name, objPath, arrFile)
            SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Purchase, Me.uitxtName.Text.Trim, True)
            ''Start TFS2375
            ''insert Approval Log
            'SaveApprovalLog(EnumReferenceType.VendorQuotation, VendorQuotationId, Me.uitxtName.Text.Trim, Me.dtpVQDate.Value.Date, "Vendor Quotation ", Me.Name)
            ''End TFS2375
            If VendorQuotationId > 0 Then
                msg_Information("Record has been saved successfully.")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function
    Public Function SaveDetail(Optional Condition As String = "") As Integer
        Try
            'VendorQuotationDetailId = DetailDAL.AddSingle(MasterModel)
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                MasterModel.VendorQuotationNo = GetDocumentNo()
                Me.uitxtName.Text = MasterModel.VendorQuotationNo
            Else
                MasterModel.VendorQuotationNo = Me.uitxtName.Text
            End If

            'MasterModel.VendorQuotationNo = Me.uitxtName.Text
            'Ali Faisal : TFS1457 : Verify that if exception in save then return false
            If MasterDAL.ForSingle(MasterModel, Me.Name, objPath, arrFile, ExpenseModelList) Then
                VendorQuotationDetailId = MasterDAL.VendorQuotationDetailId
                If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                    Me.BtnSave.Text = "Update"
                End If
                Return True
            Else
                Return False
            End If
            'Ali Faisal : TFS1457 : End
            'If MasterDAL.Add(MasterModel) Then
            '    Return True
            'Else
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If MasterDAL.Update1(MasterModel, Me.Name, objPath, arrFile) Then
                SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Purchase, Me.uitxtName.Text.Trim, True)
                msg_Information("Record has been updated successfully.")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            ''TASK TFS3536
            frmAddChildItem.UnitId = Me.cmbUnit.Value
            frmAddChildItem.TypeId = Me.cmbType.Value
            frmAddChildItem.CategoryId = Me.cmbCategory.Value
            frmAddChildItem.SubCategoryId = Me.cmbSubCategory.Value
            frmAddChildItem.OriginId = Me.cmbOrigin.Value
            ''END TASK TFS3536


            frmAddChildItem.ShowDialog()

            'Dim frm As New frmAddChildItem(Me.cmbUnit.Value, Me.cmbType.Value, Me.cmbCategory.Value, Me.cmbSubCategory.Value, Me.cmbOrigin.Value)
            'frm.ShowDialog()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Sub FillUltraDropDownType(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql


            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)

            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)

                dr(1) = "Type of item" 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If

            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt

            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub
    Public Sub FillUltraDropDownOrigin(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql


            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)

            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)

                dr(1) = "Origin" 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If

            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt

            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub
    Public Sub FillUltraDropDownUnit(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql


            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)

            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)

                dr(1) = "Unit" 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If

            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt

            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub
    Public Sub FillUltraDropDownCategory(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql


            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)

            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)

                dr(1) = "Category" 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If

            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt

            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub
    Public Sub FillUltraDropDownSubCategory(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = strSql
            Dim dt As New DataTable
            Dim dr As DataRow
            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)
            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)

                dr(1) = "Sub Category" 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If
            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        End Try
    End Sub
    Public Sub FillUltraDropDownItem(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)
        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()
            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = strSql
            Dim dt As New DataTable
            Dim dr As DataRow
            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)
            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)
                dr(1) = "Select an item" 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If
            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        End Try
    End Sub

    Private Sub btnChargesType_Click(sender As Object, e As EventArgs) Handles btnChargesType.Click
        Try
            frmAddChargesType.ShowDialog()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim Id As Integer = 0
        Try
            Id = Me.cmbReference.Value
            FillCombos("Customer")
            Me.cmbReference.Value = Id

            ''Start TFS3103
            Id = Me.cmbPurchaseInquiry.Value
            FillCombos("InquiryNumber")
            Me.cmbPurchaseInquiry.Value = Id
            ''End TFS3103

            Id = Me.cmbItem.Value
            FillCombos("Item")
            Me.cmbItem.Value = Id
            Id = Me.cmbType.Value
            FillCombos("Type")
            Me.cmbType.Value = Id
            Id = Me.cmbOrigin.Value
            FillCombos("Origin")
            Me.cmbOrigin.Value = Id
            Id = Me.cmbUnit.Value
            FillCombos("Unit")
            Me.cmbUnit.Value = Id
            Id = Me.cmbCategory.Value
            FillCombos("Category")
            Me.cmbCategory.Value = Id
            Id = Me.cmbSubCategory.Value
            FillCombos("SubCategory")
            Me.cmbSubCategory.Value = Id
            ''Start TFS3109
            If Me.cmbInwardExpense.ActiveRow Is Nothing Then
                Me.cmbInwardExpense.Rows(0).Activate()
            End If
            Id = Me.cmbInwardExpense.ActiveRow.Cells(0).Value
            FillCombos("InwardExpense")
            Me.cmbInwardExpense.Value = Id
            ''End TF3109
            'Dim Department As String = Me.cmbIndentDept.Text
            'FillCombos("IndentDepartment")
            'Me.cmbIndentDept.Text = Department
            'GetSecurityRights()
            'ReSetControls()
            'Ali Faisal : TFS1310 : Refresh comboboxes
            Dim str As String = ""
            str = Me.cmbExWorks.Text
            FillCombos("ExWorks")
            Me.cmbExWorks.Text = str
            str = Me.cmbDelivery.Text
            FillCombos("DeliveryPort")
            Me.cmbDelivery.Text = str
            str = Me.cmbGenuine.Text
            FillCombos("GenuineOrReplacement")
            Me.cmbGenuine.Text = str
            str = Me.cmbLiterature.Text
            FillCombos("LiteratureOrDatasheet")
            Me.cmbLiterature.Text = str
            str = Me.cmbRefurbish.Text
            FillCombos("NewOrRefurbish")
            Me.cmbRefurbish.Text = str


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub AddChildToGrid(ByVal RequirementDescription As String, ByVal Qty As String, ByVal ReferenceNo As String, ByVal ArticleId As Integer, ByVal Code As String, ByVal ArticleDescription As String, ByVal UnitId As Integer, ByVal Unit As String, ByVal ItemTypeId As Integer, ByVal Type As String, ByVal CategoryId As Integer, ByVal Category As String, ByVal SubCategoryId As Integer, ByVal SubCategory As String, ByVal OriginId As Integer, ByVal Origin As String, ByVal Comments As String)
        Dim dt As New DataTable
        Try
            'VendorQuotationDetailId()
            'VendorQuotationId()
            'SerialNo()
            'RequirementDescription()
            'ArticleId()
            'Code()
            'ArticleDescription()
            'UnitId()
            'Unit()
            'ItemTypeId()
            'Type()
            'CategoryId()
            'Category()
            'SubCategoryId()
            'SubCategory()
            'OriginId()
            'Origin()
            'Qty()
            'QuotedTerms()
            'ValidityOfQuotation()
            'DeliveryPeriod()
            'Warranty()
            'ApproxGrossWeight()
            'HSCode()
            'DeliveryPort()
            'GenuineOrReplacement()
            'LiteratureOrDatasheet()
            'NewOrRefurbish()
            'Price()
            'DiscountPer()
            'SalesTaxPer()
            'AddTaxPer()
            'IncTaxPer()
            'CDPer()
            'NetPrice()
            'OtherCharges()
            'PurchaseInquiryDetailId()
            'Comments()
            'HeadArticleId()
            'Dim sNo As String = Me.grdItems.GetTotal(Me.grdItems.RootTable.Columns("SerialNo"), Janus.Windows.GridEX.AggregateFunction.Max).ToString
            Dim sNo As String = Me.grdItems.GetRow.Children.ToString
            Dim sErial As String = Me.grdItems.GetRow.Cells("SerialNo").Value.ToString
            Dim oSerialNo = "" & sErial & "." & sNo + 1 & ""


            dt = CType(Me.grdItems.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            'dr.Item(Detail.PurchaseInquiryDetailId) = Val(Me.grdItems.GetRow.Cells("PurchaseInquiryDetailId").Value.ToString)
            dr.Item(Detail.VendorQuotationDetailId) = Val(0)
            dr.Item(Detail.VendorQuotationId) = Val(0)
            dr.Item(Detail.SerialNo) = oSerialNo
            dr.Item(Detail.RequirementDescription) = RequirementDescription
            If ArticleId > 0 Then
                dr.Item(Detail.ArticleId) = ArticleId
            End If
            dr.Item(Detail.Code) = Code
            dr.Item(Detail.ArticleDescription) = ArticleDescription
            dr.Item(Detail.UnitId) = UnitId
            dr.Item(Detail.Unit) = Unit
            dr.Item(Detail.ItemTypeId) = ItemTypeId
            dr.Item(Detail.Type) = Type
            dr.Item(Detail.CategoryId) = CategoryId
            dr.Item(Detail.Category) = Category
            dr.Item(Detail.SubCategoryId) = SubCategoryId
            dr.Item(Detail.SubCategory) = SubCategory
            dr.Item(Detail.OriginId) = OriginId
            dr.Item(Detail.Origin) = Origin
            dr.Item(Detail.Qty) = Val(Qty)
            dr.Item(Detail.QuotedTerms) = ""
            dr.Item(Detail.ValidityOfQuotation) = ""
            dr.Item(Detail.DeliveryPeriod) = ""
            dr.Item(Detail.Warranty) = ""
            dr.Item(Detail.ApproxGrossWeight) = ""
            dr.Item(Detail.HSCode) = ""
            dr.Item(Detail.DeliveryPort) = ""
            dr.Item(Detail.GenuineOrReplacement) = ""
            dr.Item(Detail.LiteratureOrDatasheet) = ""
            dr.Item(Detail.NewOrRefurbish) = ""
            dr.Item(Detail.ExWorks) = ""
            dr.Item(Detail.Price) = Val(0)
            dr.Item(Detail.DiscountPer) = Val(0)
            dr.Item(Detail.SalesTaxPer) = Val(0)
            dr.Item(Detail.AddTaxPer) = Val(0)
            dr.Item(Detail.IncTaxPer) = Val(0)
            dr.Item(Detail.CDPer) = Val(0)
            dr.Item(Detail.NetPrice) = Val(0)
            dr.Item(Detail.OtherCharges) = Val(0)
            dr.Item(Detail.HeadArticleId) = Val(Me.grdItems.GetRow.Cells("PurchaseInquiryDetailId").Value.ToString) ''Val(Me.grdItems.GetRow.Cells("ArticleId").Value.ToString)
            dr.Item(Detail.ReferenceNo) = IIf(ReferenceNo = "Reference Number", "", ReferenceNo)
            dr.Item(Detail.Comments) = IIf(Comments = "Comments", "", Comments)
            dr.Item(Detail.Alternate) = False
            dt.Rows.Add(dr)
            'dt.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub AddAlternateToGrid(ByVal RequirementDescription As String, ByVal Qty As String, ByVal ReferenceNo As String, ByVal ArticleId As Integer, ByVal Code As String, ByVal ArticleDescription As String, ByVal UnitId As Integer, ByVal Unit As String, ByVal ItemTypeId As Integer, ByVal Type As String, ByVal CategoryId As Integer, ByVal Category As String, ByVal SubCategoryId As Integer, ByVal SubCategory As String, ByVal OriginId As Integer, ByVal Origin As String, ByVal Comments As String)
        Dim dt As New DataTable
        Try

            Dim sErial As String = Me.grdItems.GetRow.Cells("SerialNo").Value.ToString
            'Dim CurrentRowIndex As Integer = Me.grdItems.GetRow.RowIndex
            dt = CType(Me.grdItems.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item(Detail.PurchaseInquiryDetailId) = Val(Me.grdItems.GetRow.Cells("PurchaseInquiryDetailId").Value.ToString)
            dr.Item(Detail.VendorQuotationDetailId) = Val(0)
            dr.Item(Detail.VendorQuotationId) = Val(0)
            dr.Item(Detail.SerialNo) = sErial
            If RequirementDescription.Length > 0 Then
                dr.Item(Detail.RequirementDescription) = RequirementDescription & "(Alternate for: " & Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString & ")"
            Else
                dr.Item(Detail.RequirementDescription) = RequirementDescription

            End If
            If ArticleId > 0 Then
                dr.Item(Detail.ArticleId) = ArticleId
            End If
            dr.Item(Detail.Code) = Code
            If ArticleDescription.Length > 0 Then
                dr.Item(Detail.ArticleDescription) = ArticleDescription & "(Alternate for: " & Me.grdItems.GetRow.Cells("ArticleDescription").Value.ToString & ")"
            Else
                dr.Item(Detail.ArticleDescription) = ArticleDescription
            End If
            dr.Item(Detail.UnitId) = UnitId
            dr.Item(Detail.Unit) = Unit
            dr.Item(Detail.ItemTypeId) = ItemTypeId
            dr.Item(Detail.Type) = Type
            dr.Item(Detail.CategoryId) = CategoryId
            dr.Item(Detail.Category) = Category
            dr.Item(Detail.SubCategoryId) = SubCategoryId
            dr.Item(Detail.SubCategory) = SubCategory
            dr.Item(Detail.OriginId) = OriginId
            dr.Item(Detail.Origin) = Origin
            dr.Item(Detail.Qty) = Val(Qty)
            dr.Item(Detail.QuotedTerms) = ""
            dr.Item(Detail.ValidityOfQuotation) = ""
            dr.Item(Detail.DeliveryPeriod) = ""
            dr.Item(Detail.Warranty) = ""
            dr.Item(Detail.ApproxGrossWeight) = ""
            dr.Item(Detail.HSCode) = ""
            dr.Item(Detail.DeliveryPort) = ""
            dr.Item(Detail.GenuineOrReplacement) = ""
            dr.Item(Detail.LiteratureOrDatasheet) = ""
            dr.Item(Detail.NewOrRefurbish) = ""
            dr.Item(Detail.ExWorks) = ""
            dr.Item(Detail.Price) = Val(0)
            dr.Item(Detail.DiscountPer) = Val(0)
            dr.Item(Detail.SalesTaxPer) = Val(0)
            dr.Item(Detail.AddTaxPer) = Val(0)
            dr.Item(Detail.IncTaxPer) = Val(0)
            dr.Item(Detail.CDPer) = Val(0)
            dr.Item(Detail.NetPrice) = Val(0)
            dr.Item(Detail.OtherCharges) = Val(0)
            dr.Item(Detail.HeadArticleId) = Val(0) ''Val(Me.grdItems.GetRow.Cells("ArticleId").Value.ToString)
            dr.Item(Detail.ReferenceNo) = IIf(ReferenceNo = "Reference Number", "", ReferenceNo)
            dr.Item(Detail.Comments) = IIf(Comments = "Comments", "", Comments)
            dr.Item(Detail.Alternate) = True
            dt.Rows.InsertAt(dr, CurrentRowIndex + 1)
            'dt.AcceptChanges()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillAllCombos()
        Try
            Me.cmbDelivery2.SelectedIndex = 0
            Me.cmbGenuine1.SelectedIndex = 0
            Me.cmbLiterature1.SelectedIndex = 0
            Me.cmbRefurbish1.SelectedIndex = 0
            'Ali Faisal : Commented to set values in List and zero index setting on 16-May-2017
            'Me.cmbDelivery.Items.Insert(0, "Delievry Sea")
            'Me.cmbDelivery.Items.Insert(2, "Air Port")

            'Me.cmbGenuine.Items.Insert(0, "Genuine")
            'Me.cmbGenuine.Items.Insert(1, "Replacement")

            'Me.cmbLiterature.Items.Insert(0, "Literature")
            'Me.cmbLiterature.Items.Insert(1, "DataSheet")

            'Me.cmbRefurbish.Items.Insert(0, "New")
            'Me.cmbRefurbish.Items.Insert(1, "Refurbish")

            'FillCombos("Customer")
            'FillCombos("InquiryNumber")

            FillCombos("Item")
            FillCombos("Type")
            FillCombos("Origin")
            FillCombos("Unit")
            FillCombos("Category")
            FillCombos("SubCategory")
            FillCombos("Currency")
            'FillCombos("IndentDepartment")
            'FillCombos("ContactPerson")
            'FillCombos("Location")
            'Ali Faisal : TFS1310 : Call fill combo boxes
            FillCombos("ExWorks")
            FillCombos("DeliveryPort")
            FillCombos("GenuineOrReplacement")
            FillCombos("LiteratureOrDatasheet")
            FillCombos("NewOrRefurbish")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ResetDetailControls()
        Try
            Me.btnSave1.Text = "&Save"
            Me.txtQuotation.Text = ""
            Me.txtValidityOfQuotation.Text = ""
            Me.txtDeliveryPeriod.Text = ""
            Me.txtWarranty.Text = ""
            Me.txtApproxgrossweight.Text = ""
            Me.txtHSCode.Text = ""
            Me.cmbDelivery.Text = ""
            Me.cmbGenuine.Text = ""
            Me.cmbLiterature.Text = ""
            Me.cmbRefurbish.Text = ""
            Me.txtPrice.Text = ""
            Me.txtDiscount.Text = ""
            Me.txtDiscountAmount.Text = ""
            Me.txtSaleTax.Text = ""
            Me.txtSaleTaxAmount.Text = ""
            Me.txtAddTax.Text = ""
            Me.txtAddTaxAmount.Text = ""
            Me.txtIncTax.Text = ""
            Me.txtIncTaxAmount.Text = ""
            Me.txtCD.Text = ""
            Me.txtCDAmount.Text = ""
            Me.txtNetPrice.Text = ""
            Me.txtAmount.Text = ""
            Me.txtCharges.Text = ""
            Me.txtNetCostValue.Text = ""
            PurchaseInquiryDetailId = 0
            Me.txtReferenceNo.Text = ""
            Me.txtPriceafterdiscount.Text = ""
            Me.txtQty1.Text = ""
            HeadArticleId = 0
            Me.txtRequirementDescription.Text = ""
            Me.cmbItem.Rows(0).Activate()
            Me.cmbUnit.Rows(0).Activate()
            Me.cmbType.Rows(0).Activate()
            Me.cmbCategory.Rows(0).Activate()
            Me.cmbSubCategory.Rows(0).Activate()
            Me.cmbOrigin.Rows(0).Activate()
            Me.cmbExWorks.Text = ""
            Me.txtQty.Text = "Qty"
            Me.txtComments.Text = "Comments"
            Me.txtReferenceNo.Text = "Reference No"
            IsAlternate = False
            PreviousRowIndex = -1
            CurrentRowIndex = -1
            'Me.cmbUnit.Enabled = True
            'Me.cmbType.Enabled = True
            'Me.cmbCategory.Enabled = True
            'Me.cmbSubCategory.Enabled = True
            'Me.cmbOrigin.Enabled = True
            Me.DisplayChargesTypes()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' This Sub is Edit to reset Values when Document i opened in Edit Mode
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetDetailControlsInUpdate()
        Try
            Me.txtQuotation.Text = ""
            Me.txtValidityOfQuotation.Text = ""
            Me.txtDeliveryPeriod.Text = ""
            Me.txtWarranty.Text = ""
            Me.txtApproxgrossweight.Text = ""
            Me.txtHSCode.Text = ""
            Me.cmbDelivery.Text = ""
            Me.cmbGenuine.Text = ""
            Me.cmbLiterature.Text = ""
            Me.cmbRefurbish.Text = ""
            Me.txtPrice.Text = ""
            Me.txtDiscount.Text = ""
            Me.txtDiscountAmount.Text = ""
            Me.txtSaleTax.Text = ""
            Me.txtSaleTaxAmount.Text = ""
            Me.txtAddTax.Text = ""
            Me.txtAddTaxAmount.Text = ""
            Me.txtIncTax.Text = ""
            Me.txtIncTaxAmount.Text = ""
            Me.txtCD.Text = ""
            Me.txtCDAmount.Text = ""
            Me.txtNetPrice.Text = ""
            Me.txtAmount.Text = ""
            Me.txtCharges.Text = ""
            Me.txtNetCostValue.Text = ""
            Me.txtReferenceNo.Text = ""
            Me.txtPriceafterdiscount.Text = ""
            Me.txtQty1.Text = ""
            Me.txtRequirementDescription.Text = ""
            Me.cmbItem.Rows(0).Activate()
            Me.cmbUnit.Rows(0).Activate()
            Me.cmbType.Rows(0).Activate()
            Me.cmbCategory.Rows(0).Activate()
            Me.cmbSubCategory.Rows(0).Activate()
            Me.cmbOrigin.Rows(0).Activate()
            Me.cmbExWorks.Text = ""
            Me.txtQty.Text = "Qty"
            Me.txtComments.Text = "Comments"
            Me.txtReferenceNo.Text = "Reference No"
            PreviousRowIndex = -1
            CurrentRowIndex = -1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbItem_ValueChanged(sender As Object, e As EventArgs) Handles cmbItem.ValueChanged
        Try
            If Not Me.cmbItem.Text = "Select an item" Then
                Me.cmbUnit.Value = Val(Me.cmbItem.ActiveRow.Cells("ArticleUnitId").Value.ToString)
                Me.cmbType.Value = Val(Me.cmbItem.ActiveRow.Cells("ArticleTypeId").Value.ToString)
                Me.cmbCategory.Value = Val(Me.cmbItem.ActiveRow.Cells("ArticleCategoryId").Value.ToString)
                Me.cmbSubCategory.Value = Val(Me.cmbItem.ActiveRow.Cells("ArticleLPOId").Value.ToString)
                Me.cmbOrigin.Value = Val(Me.cmbItem.ActiveRow.Cells("ArticleGenderId").Value.ToString)
                Me.cmbUnit.Enabled = False
                Me.cmbType.Enabled = False
                Me.cmbCategory.Enabled = False
                Me.cmbSubCategory.Enabled = False
                'Me.cmbOrigin.Enabled = False
                'FillCombos("Item")
                'FillCombos("TypeAgainstItem")
                'FillCombos("OriginAgainstItem")
                'FillCombos("UnitAgainstItem")
                'FillCombos("CategoryAgainstItem")
                'FillCombos("SubCategoryAgainstItem")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub DisplayDetailForHistory(ByVal VendorQuotationId As Integer)
        Try
            Me.grdItems.DataSource = DetailDAL.DisplayDetailForHistory(VendorQuotationId)
            Me.grdItems.ExpandRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub DisplayDetailForPurchase(ByVal PurchaseInquiryId As Integer)
        Try
            Me.grdItems.DataSource = DetailDAL.DisplayDetailForPurchase(PurchaseInquiryId)
            'Me.grdItems.RetrieveStructure()
            Me.grdItems.ExpandRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Me.btnSave1.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.BtnPrint.Enabled = False
                    Me.btnSave1.Enabled = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmVendorQuotation)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                            Me.btnSave1.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                            Me.btnSave1.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If


            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                Me.btnSave1.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False
                'CtrlGrdBar1.mGridChooseFielder.Enabled = False

                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                        If Me.btnSave1.Text = "&Save" Then btnSave1.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        If Me.btnSave1.Text = "&Update" Then btnSave1.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then

                        'End Task:2395
                        'Task:2406 Added Field Chooser Rights
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        'CtrlGrdBar1.mGridChooseFielder.Enabled = True
                        'End Task:2406
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdCharges_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdCharges.CellEdited

    End Sub



    Private Sub txtPrice_TextChanged(sender As Object, e As EventArgs) Handles txtPrice.TextChanged
        Try
            'If Val(Me.txtPrice.Text) > 0 Then
            Me.txtNetPrice.Text = Val(Me.txtPrice.Text)
            Me.txtPriceafterdiscount.Text = Val(Me.txtPrice.Text)
            TaxAmounts()
            GetNetPrice()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSaleTax_TextChanged(sender As Object, e As EventArgs) Handles txtSaleTax.TextChanged
        Try
            'If Val(txtPriceafterdiscount.Text) > 0 Then
            Me.txtSaleTaxAmount.Text = Val(txtPriceafterdiscount.Text) * Val(Me.txtSaleTax.Text) / 100
            'Me.txtNetPrice.Text = Val(Me.txtNetPrice.Text) + Val(Me.txtSaleTaxAmount.Text)
            GetNetPrice()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub TaxAmounts()
        Try
            Me.txtDiscountAmount.Text = (Val(txtPrice.Text) * Val(Me.txtDiscount.Text) / 100)
            Me.txtSaleTaxAmount.Text = Val(txtPriceafterdiscount.Text) * Val(Me.txtSaleTax.Text) / 100
            Me.txtAddTaxAmount.Text = (Val(txtPriceafterdiscount.Text) * Val(Me.txtAddTax.Text) / 100)
            Me.txtIncTaxAmount.Text = (Val(txtPriceafterdiscount.Text) * Val(Me.txtIncTax.Text) / 100)
            Me.txtCDAmount.Text = (Val(txtPriceafterdiscount.Text) * Val(Me.txtCD.Text) / 100)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAddTax_TextChanged(sender As Object, e As EventArgs) Handles txtAddTax.TextChanged
        Try
            'If Val(txtAddTax.Text) > 0 Then
            Me.txtAddTaxAmount.Text = (Val(txtPriceafterdiscount.Text) * Val(Me.txtAddTax.Text) / 100)
            'Me.txtNetPrice.Text += Val(Me.txtAddTaxAmount.Text)
            GetNetPrice()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtIncTax_TextChanged(sender As Object, e As EventArgs) Handles txtIncTax.TextChanged
        Try
            'If Val(txtIncTax.Text) > 0 Then
            Me.txtIncTaxAmount.Text = (Val(txtPriceafterdiscount.Text) * Val(Me.txtIncTax.Text) / 100)
            'Me.txtNetPrice.Text += Val(Me.txtIncTaxAmount.Text)
            GetNetPrice()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCD_TextChanged(sender As Object, e As EventArgs) Handles txtCD.TextChanged
        Try
            'If Val(txtCD.Text) > 0 Then
            Me.txtCDAmount.Text = (Val(txtPriceafterdiscount.Text) * Val(Me.txtCD.Text) / 100)
            'Me.txtNetPrice.Text += Val(Me.txtCDAmount.Text)
            GetNetPrice()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        If Val(txtQty.Text) > 0 Then
            Me.txtQty1.Text = Val(Me.txtQty.Text)
        End If
    End Sub

    Private Sub txtNetPrice_TextChanged(sender As Object, e As EventArgs) Handles txtNetPrice.TextChanged ''TFS3106
        Try
            If Val(Me.txtNetPrice.Text) > 0 AndAlso Val(Me.txtQty1.Text) > 0 Then
                Me.txtAmount.Text = Val(Me.txtNetPrice.Text) * Val(Me.txtQty1.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCharges_TextChanged(sender As Object, e As EventArgs) Handles txtCharges.TextChanged
        Try
            If Val(Me.txtAmount.Text) > 0 AndAlso Val(Me.txtCharges.Text) > 0 Then
                Me.txtNetCostValue.Text = Val(Me.txtAmount.Text) + Val(Me.txtCharges.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecord(Optional Condition As String = "")
        Try
            If Not Me.grdSaved.RecordCount > 0 Then Exit Sub
            If Me.grdItems.RecordCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            'VendorQuotationId	int	Unchecked
            'VendorQuotationNo	nvarchar(100)	Checked
            'VendorQuotationDate	datetime	Checked
            'VendorQuotationExpiryDate	datetime	Checked
            'VendorId	int	Checked
            'PurchaseInquiryId	int	Checked
            'ReferenceNo	nvarchar(100)	Checked
            'Remarks	nvarchar(500)	Checked

            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
            IsEditMode = True
            If Not getConfigValueByType("VendorQuotationApproval") = "Error" Then
                ApprovalProcessId = getConfigValueByType("VendorQuotationApproval")
            End If
            If ApprovalProcessId = 0 Then
                Me.btnApprovalHistory.Visible = False
                Me.btnApprovalHistory.Enabled = False
            Else
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            End If
            ''Ayesha Rehman :TFS2375 :End
            Me.uitxtName.Text = Me.grdSaved.CurrentRow.Cells("VendorQuotationNo").Value.ToString
            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells("Remarks").Value.ToString
            Me.txtReferenceNo.Text = Me.grdSaved.CurrentRow.Cells("ReferenceNo").Value.ToString
            If IsDBNull(Me.grdSaved.CurrentRow.Cells("VendorQuotationDate").Value) Then
                Me.dtpVQDate.Value = Now
            Else
                Me.dtpVQDate.Value = Me.grdSaved.CurrentRow.Cells("VendorQuotationDate").Value
            End If
            If IsDBNull(Me.grdSaved.CurrentRow.Cells("VendorQuotationExpiryDate").Value) Then
                Me.dtpExpiryDate.Value = Now
                'Me.dtpOldInquiryDate.Value = Now
            Else
                Me.dtpExpiryDate.Value = Me.grdSaved.CurrentRow.Cells("VendorQuotationExpiryDate").Value

            End If
            Me.cmbReference.Value = Val(Me.grdSaved.CurrentRow.Cells("VendorId").Value.ToString)

            ''Start TFS3106
            Me.txtTotalAmount.Text = Val(Me.grdSaved.CurrentRow.Cells("Amount").Value.ToString)
            Me.txtNetDiscount.Text = Val(Me.grdSaved.CurrentRow.Cells("Discount").Value.ToString)
            Me.txtNetTotal.Text = Val(Me.grdSaved.CurrentRow.Cells("NetTotal").Value.ToString)
            ''End TFS3106

            'Me.cmbDelivery.Text = Me.grdSaved.CurrentRow.Cells("DeliveryPort").Value.ToString
            'Me.cmbGenuine.Text = Me.grdSaved.CurrentRow.Cells("GenuineOrReplacement").Value.ToString
            'Me.cmbLiterature.Text = Me.grdSaved.CurrentRow.Cells("LiteratureOrDatasheet").Value.ToString
            'Me.cmbRefurbish.Text = Me.grdSaved.CurrentRow.Cells("NewOrRefurbish").Value.ToString
            'Me.cmbReference.Enabled = False
            Me.cmbPurchaseInquiry.Value = Val(Me.grdSaved.CurrentRow.Cells("PurchaseInquiryId").Value.ToString)
            'Me.cmbPurchaseInquiry.Enabled = False
            VendorQuotationId = Val(Me.grdSaved.CurrentRow.Cells("VendorQuotationId").Value.ToString)
            Me.btnAttachment.Text = "Attachment (" & Me.grdSaved.GetRow.Cells("No Of Attachments").Value.ToString & ")"
            FillInwardExpense(grdSaved.CurrentRow.Cells("VendorQuotationId").Value, "VQ") ''TFS3109
            DisplayDetailForHistory(VendorQuotationId)
            Me.BtnSave.Text = "&Update"
            'Me.btnSave1.Text = "&Update"
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            ResetDetailControlsInUpdate()
            Me.btnAddPurchaseInquiryItems.Enabled = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub DisplayChargesTypes()
        Try
            Me.grdCharges.DataSource = DetailDAL.GetChargesTypes()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub DisplayDetailCharges(ByVal VendorQuoationDetailId As Integer)
        Try
            Me.grdCharges.DataSource = DetailDAL.GetDetailCharges(VendorQuoationDetailId)
            'Ali Faisal : TFS1314 : 11-Aug-2017 : Add new column as button in grid to remove the charges types
            Me.btnGetChargesTypes.Visible = True
            If Me.grdCharges.RootTable.Columns.Contains("Delete") = False Then
                Me.grdCharges.RootTable.Columns.Add("Delete")
                Me.grdCharges.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdCharges.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdCharges.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdCharges.RootTable.Columns("Delete").Key = "Delete"
                Me.grdCharges.RootTable.Columns("Delete").Caption = "Action"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ShowDetailRecord()
        Try
            SerialNo = Me.grdItems.GetRow.Cells("SerialNo").Value.ToString
            CurrentRowIndex = Me.grdItems.GetRow.RowIndex
            'VendorQuotationId = Val(Me.grdItems.GetRow.Cells("VendorQuotationId").Value.ToString)
            VendorQuotationDetailId = Val(Me.grdItems.GetRow.Cells("VendorQuotationDetailId").Value.ToString)
            If VendorQuotationDetailId > 0 Then
                Me.btnSave1.Text = "&Update"
            Else
                Me.btnSave1.Text = "&Save"
            End If
            ResetDetailControlsInUpdate()
            Me.txtRequirementDescription.Text = Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString
            Me.cmbItem.Value = Val(Me.grdItems.GetRow.Cells("ArticleId").Value.ToString)
            Me.cmbUnit.Value = Val(Me.grdItems.GetRow.Cells("UnitId").Value.ToString)
            Me.cmbType.Value = Val(Me.grdItems.GetRow.Cells("ItemTypeId").Value.ToString)
            Me.cmbCategory.Value = Val(Me.grdItems.GetRow.Cells("CategoryId").Value.ToString)
            Me.cmbSubCategory.Value = Val(Me.grdItems.GetRow.Cells("SubCategoryId").Value.ToString)
            Me.cmbOrigin.Value = Val(Me.grdItems.GetRow.Cells("OriginId").Value.ToString)
            Me.txtValidityOfQuotation.Text = Me.grdItems.GetRow.Cells("ValidityOfQuotation").Value.ToString
            Me.txtQty.Text = Me.grdItems.GetRow.Cells("Qty").Value.ToString
            Me.txtQuotation.Text = Me.grdItems.GetRow.Cells("QuotedTerms").Value.ToString
            Me.txtValidityOfQuotation.Text = Me.grdItems.GetRow.Cells("ValidityOfQuotation").Value.ToString
            Me.txtDeliveryPeriod.Text = Me.grdItems.GetRow.Cells("DeliveryPeriod").Value.ToString
            Me.txtWarranty.Text = Me.grdItems.GetRow.Cells("Warranty").Value.ToString
            Me.txtApproxgrossweight.Text = Me.grdItems.GetRow.Cells("ApproxGrossWeight").Value.ToString
            Me.txtHSCode.Text = Me.grdItems.GetRow.Cells("HSCode").Value.ToString
            Me.cmbDelivery.Text = Me.grdItems.GetRow.Cells("DeliveryPort").Value.ToString
            Me.cmbGenuine.Text = Me.grdItems.GetRow.Cells("GenuineOrReplacement").Value.ToString
            Me.cmbLiterature.Text = Me.grdItems.GetRow.Cells("LiteratureOrDatasheet").Value.ToString
            Me.cmbRefurbish.Text = Me.grdItems.GetRow.Cells("NewOrRefurbish").Value.ToString
            Me.cmbExWorks.Text = Me.grdItems.GetRow.Cells("ExWorks").Value.ToString
            Me.txtPrice.Text = Me.grdItems.GetRow.Cells("Price").Value.ToString
            Me.txtDiscount.Text = Me.grdItems.GetRow.Cells("DiscountPer").Value.ToString
            Me.txtDiscountAmount.Text = Me.grdItems.GetRow.Cells("DiscountAmount").Value.ToString
            Me.txtSaleTax.Text = Me.grdItems.GetRow.Cells("SalesTaxPer").Value.ToString
            Me.txtSaleTaxAmount.Text = Me.grdItems.GetRow.Cells("SalesTaxAmount").Value.ToString
            Me.txtAddTax.Text = Me.grdItems.GetRow.Cells("AddTaxPer").Value.ToString
            Me.txtAddTaxAmount.Text = Me.grdItems.GetRow.Cells("AddTaxAmount").Value.ToString
            Me.txtIncTax.Text = Me.grdItems.GetRow.Cells("IncTaxPer").Value.ToString
            Me.txtIncTaxAmount.Text = Me.grdItems.GetRow.Cells("IncTaxAmount").Value.ToString
            Me.txtCD.Text = Me.grdItems.GetRow.Cells("CDPer").Value.ToString
            Me.txtCDAmount.Text = Me.grdItems.GetRow.Cells("CDAmount").Value.ToString
            Me.txtNetPrice.Text = Me.grdItems.GetRow.Cells("NetPrice").Value.ToString
            Me.txtAmount.Text = Me.grdItems.GetRow.Cells("Amount").Value.ToString
            Me.txtCharges.Text = Me.grdItems.GetRow.Cells("OtherCharges").Value.ToString
            Me.txtNetCostValue.Text = Me.grdItems.GetRow.Cells("NetCostValue").Value.ToString
            PurchaseInquiryDetailId = Val(Me.grdItems.GetRow.Cells("PurchaseInquiryDetailId").Value.ToString)
            Me.txtReferenceNo.Text = Me.grdItems.GetRow.Cells("ReferenceNo").Value.ToString
            Me.txtComments.Text = Me.grdItems.GetRow.Cells("Comments").Value.ToString
            HeadArticleId = Me.grdItems.GetRow.Cells("HeadArticleId").Value
            If Val(Me.grdItems.GetRow.Cells("CurrencyId").Value.ToString) > 0 Then
                Me.cmbCurrency.SelectedValue = Val(Me.grdItems.GetRow.Cells("CurrencyId").Value.ToString)
            End If
            IsAlternate = Me.grdItems.GetRow.Cells("Alternate").Value
            DisplayDetailCharges(VendorQuotationDetailId)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub LoadDetailRecord()
        Try
            Me.grdItems.GetRow.Cells("SerialNo").Value = SerialNo
            Me.grdItems.GetRow.Cells("VendorQuotationId").Value = VendorQuotationId
            Me.grdItems.GetRow.Cells("VendorQuotationDetailId").Value = VendorQuotationDetailId
            VendorQuotationDetailId = 0
            Me.grdItems.GetRow.Cells("RequirementDescription").Value = Me.txtRequirementDescription.Text
            If Me.cmbItem.Value > 0 Then
                Me.grdItems.GetRow.Cells("ArticleId").Value = Me.cmbItem.Value
            End If
            Me.grdItems.GetRow.Cells("UnitId").Value = Me.cmbUnit.Value
            Me.grdItems.GetRow.Cells("ItemTypeId").Value = Me.cmbType.Value
            Me.grdItems.GetRow.Cells("CategoryId").Value = Me.cmbCategory.Value
            Me.grdItems.GetRow.Cells("SubCategoryId").Value = Me.cmbSubCategory.Value
            Me.grdItems.GetRow.Cells("OriginId").Value = Me.cmbOrigin.Value
            Me.grdItems.GetRow.Cells("ValidityOfQuotation").Value = Me.txtValidityOfQuotation.Text
            Me.grdItems.GetRow.Cells("Qty").Value = Val(Me.txtQty.Text)
            Me.grdItems.GetRow.Cells("QuotedTerms").Value = Me.txtQuotation.Text
            Me.grdItems.GetRow.Cells("ValidityOfQuotation").Value = Me.txtValidityOfQuotation.Text
            Me.grdItems.GetRow.Cells("DeliveryPeriod").Value = Me.txtDeliveryPeriod.Text
            Me.grdItems.GetRow.Cells("Warranty").Value = Me.txtWarranty.Text
            Me.grdItems.GetRow.Cells("ApproxGrossWeight").Value = Me.txtApproxgrossweight.Text
            Me.grdItems.GetRow.Cells("HSCode").Value = Me.txtHSCode.Text
            Me.grdItems.GetRow.Cells("DeliveryPort").Value = Me.cmbDelivery.Text
            Me.grdItems.GetRow.Cells("GenuineOrReplacement").Value = Me.cmbGenuine.Text
            Me.grdItems.GetRow.Cells("LiteratureOrDatasheet").Value = Me.cmbLiterature.Text
            Me.grdItems.GetRow.Cells("NewOrRefurbish").Value = Me.cmbRefurbish.Text
            Me.grdItems.GetRow.Cells("ExWorks").Value = Me.cmbExWorks.Text
            Me.grdItems.GetRow.Cells("Price").Value = Me.txtPrice.Text
            Me.grdItems.GetRow.Cells("DiscountPer").Value = Me.txtDiscount.Text
            Me.grdItems.GetRow.Cells("DiscountAmount").Value = Me.txtDiscountAmount.Text
            Me.grdItems.GetRow.Cells("SalesTaxPer").Value = Me.txtSaleTax.Text
            Me.grdItems.GetRow.Cells("SalesTaxAmount").Value = Me.txtSaleTaxAmount.Text
            Me.grdItems.GetRow.Cells("AddTaxPer").Value = Me.txtAddTax.Text
            Me.grdItems.GetRow.Cells("AddTaxAmount").Value = Me.txtAddTaxAmount.Text
            Me.grdItems.GetRow.Cells("IncTaxPer").Value = Me.txtIncTax.Text
            Me.grdItems.GetRow.Cells("IncTaxAmount").Value = Me.txtIncTaxAmount.Text
            Me.grdItems.GetRow.Cells("CDPer").Value = Me.txtCD.Text
            Me.grdItems.GetRow.Cells("CDAmount").Value = Me.txtCDAmount.Text
            Me.grdItems.GetRow.Cells("NetPrice").Value = Me.txtNetPrice.Text
            Me.grdItems.GetRow.Cells("Amount").Value = Me.txtAmount.Text
            Me.grdItems.GetRow.Cells("OtherCharges").Value = Me.txtCharges.Text
            Me.grdItems.GetRow.Cells("NetCostValue").Value = Me.txtNetCostValue.Text
            If PurchaseInquiryDetailId Then
                Me.grdItems.GetRow.Cells("PurchaseInquiryDetailId").Value = PurchaseInquiryDetailId
            End If
            Me.grdItems.GetRow.Cells("ReferenceNo").Value = Me.txtReferenceNo.Text
            Me.grdItems.GetRow.Cells("Comments").Value = Me.txtComments.Text
            Me.grdItems.GetRow.Cells("HeadArticleId").Value = HeadArticleId
            Me.grdItems.GetRow.Cells("BaseCurrencyId").Value = BaseCurrencyId
            Me.grdItems.GetRow.Cells("BaseCurrencyRate").Value = 1
            Me.grdItems.GetRow.Cells("CurrencyId").Value = Me.cmbCurrency.SelectedValue
            Me.grdItems.GetRow.Cells("Currency").Value = Me.cmbCurrency.Text
            Me.grdItems.GetRow.Cells("CurrencyRate").Value = Val(Me.txtCurrencyRate.Text)
            Me.grdItems.GetRow.Cells("CurrencySymbol").Value = Me.cmbCurrency.Text
            Me.grdItems.GetRow.Cells("Alternate").Value = IsAlternate
            'DisplayDetailCharges(VendorQuotationDetailId)
            DiscountCalculation()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Start TFS3106 :Ayesha Rehman : 23-04-2018
    Private Sub DiscountCalculation()
        Try
            Dim NetAmount As Double = 0D
            NetAmount = Val(Me.txtTotalAmount.Text)

            If Val(Me.txtNetDiscount.Text) <> 0 AndAlso NetAmount > 0 Then
                Dim discount As Double = 0D
                discount = Val(Me.txtNetDiscount.Text)
                Me.txtNetTotal.Text = Val(NetAmount) - Math.Round(discount, 2).ToString()
            Else
                Me.txtNetTotal.Text = Val(NetAmount)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''End TFS3106
    Function GetDocumentNo() As String
        Try
            'Dim strPrefix As String = getConfigValueByType("VendorQuotation").ToString
            'If strPrefix.Length > 0 Then
            '    If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
            '        Return GetSerialNo(strPrefix + "-" + Microsoft.VisualBasic.Right(Me.dtpVQDate.Value.Year, 2) + "-", "VendorQuotationMaster", "VendorQuotationNo")
            '    ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
            '        Return GetNextDocNo(strPrefix + "-" & Format(Me.dtpVQDate.Value, "yy") & Me.dtpVQDate.Value.Month.ToString("00"), 4, "VendorQuotationMaster", "VendorQuotationNo")
            '    Else
            '        Return GetNextDocNo(strPrefix + "-", 6, "VendorQuotationMaster", "VendorQuotationNo")
            '    End If
            'Else
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("VQ-" + Microsoft.VisualBasic.Right(Me.dtpVQDate.Value.Year, 2) + "-", "VendorQuotationMaster", "VendorQuotationNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("VQ-" & Format(Me.dtpVQDate.Value, "yy") & Me.dtpVQDate.Value.Month.ToString("00"), 4, "VendorQuotationMaster", "VendorQuotationNo")
            Else
                Return GetNextDocNo("VQ-", 6, "VendorQuotationMaster", "VendorQuotationNo")
            End If
            End
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            Me.txtTotalAmount.Text = ""
            Me.txtNetDiscount.Text = ""
            FillCombos("InwardExpense")
            FillInwardExpense(-1, "VQ") ''TFS3109
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            ResetDetailControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() Then
                If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Save() Then
                        GetAllRecords()
                        'Me.BtnSave.Text = "&Update"
                        ReSetControls()

                        '' Add by Mohsin on 18 Sep 2017

                        ' NOTIFICATION STARTS HERE FOR Save 

                        ' *** New Segment *** 
                        '// Adding notification

                        '// Creating new object of Notification configuration dal
                        '// Dal will be used to get users list for the notification 
                        Dim NDal1 As New NotificationConfigurationDAL
                        Dim objmod1 As New VouchersMaster
                        '// Creating new object of Agrius Notification class
                        objmod1.Notification = New AgriusNotifications

                        '// Reference document number
                        objmod1.Notification.ApplicationReference = Me.uitxtName.Text

                        '// Date of notification
                        objmod1.Notification.NotificationDate = Now

                        '// Preparing notification title string
                        objmod1.Notification.NotificationTitle = "Vendor Quotation number [" & Me.uitxtName.Text & "]  is created."

                        '// Preparing notification description string
                        objmod1.Notification.NotificationDescription = "Vendor Quotation number [" & Me.uitxtName.Text & "] is created by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                        '// Setting source application as refrence in the notification
                        objmod1.Notification.SourceApplication = "Vendor Quotation"



                        '// Starting to get users list to add child

                        '// Creating notification detail object list
                        Dim List1 As New List(Of NotificationDetail)

                        '// Getting users list
                        List1 = NDal1.GetNotificationUsers("Vendor Quotation Created")

                        '// Adding users list in the Notification object of current inquiry
                        objmod1.Notification.NotificationDetils.AddRange(List1)

                        '// Getting and adding user groups list in the Notification object of current inquiry
                        objmod1.Notification.NotificationDetils.AddRange(NDal1.GetNotificationGroups("Vendor Quotation Created"))

                        '// Not getting role list because no role is associated at the moment
                        '// We will need this in future and we can use it later
                        '// We can consult to Update function of this class


                        '// ***This segment will be used to save notification in database table

                        '// Creating new list of objects of Agrius Notification
                        Dim NList1 As New List(Of AgriusNotifications)

                        '// Copying notification object from current sales inquiry to newly defined instance
                        '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
                        NList1.Add(objmod1.Notification)

                        '// Creating object of Notification DAL
                        Dim GNotification1 As New NotificationDAL

                        '// Saving notification to database
                        GNotification1.AddNotification(NList1)

                        '// End Adding Notification

                        '// End Adding Notification
                        ' *** End Segment ***

                    End If
                Else
                    'If IsValidToDelete("PurchaseInquiryDetail", "SalesInquiryId", SalesInquiryId) = True Then
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Update1() Then
                        GetAllRecords()
                        ReSetControls()

                        '' Add by Mohsin on 18 Sep 2017

                        ' NOTIFICATION STARTS HERE FOR UPDATE

                        ' *** New Segment *** 
                        '// Adding notification

                        '// Creating new object of Notification configuration dal
                        '// Dal will be used to get users list for the notification 
                        Dim NDal1 As New NotificationConfigurationDAL
                        Dim objmod1 As New VouchersMaster
                        '// Creating new object of Agrius Notification class
                        objmod1.Notification = New AgriusNotifications

                        '// Reference document number
                        objmod1.Notification.ApplicationReference = Me.uitxtName.Text

                        '// Date of notification
                        objmod1.Notification.NotificationDate = Now

                        '// Preparing notification title string
                        objmod1.Notification.NotificationTitle = "Vendor Quotation number [" & Me.uitxtName.Text & "]  is changed."

                        '// Preparing notification description string
                        objmod1.Notification.NotificationDescription = "Vendor Quotation number [" & Me.uitxtName.Text & "] is changed by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                        '// Setting source application as refrence in the notification
                        objmod1.Notification.SourceApplication = "Vendor Quotation"



                        '// Starting to get users list to add child

                        '// Creating notification detail object list
                        Dim List1 As New List(Of NotificationDetail)

                        '// Getting users list
                        List1 = NDal1.GetNotificationUsers("Vendor Quotation Changed")

                        '// Adding users list in the Notification object of current inquiry
                        objmod1.Notification.NotificationDetils.AddRange(List1)

                        '// Getting and adding user groups list in the Notification object of current inquiry
                        objmod1.Notification.NotificationDetils.AddRange(NDal1.GetNotificationGroups("Vendor Quotation Changed"))

                        '// Not getting role list because no role is associated at the moment
                        '// We will need this in future and we can use it later
                        '// We can consult to Update function of this class


                        '// ***This segment will be used to save notification in database table

                        '// Creating new list of objects of Agrius Notification
                        Dim NList1 As New List(Of AgriusNotifications)

                        '// Copying notification object from current sales inquiry to newly defined instance
                        '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
                        NList1.Add(objmod1.Notification)

                        '// Creating object of Notification DAL
                        Dim GNotification1 As New NotificationDAL

                        '// Saving notification to database
                        GNotification1.AddNotification(NList1)

                        '// End Adding Notification

                        '// End Adding Notification
                        ' *** End Segment ***

                    End If
                    'Else
                    'msg_Error(str_ErrorDependentRecordFound)
                    'End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnSave1_Click(sender As Object, e As EventArgs) Handles btnSave1.Click
        Try
            If IsValidateDetail() Then
                'If Me.btnSave1.Text = "&Save" Or Me.btnSave1.Text = "Save" Then
                'If Not Me.BtnSave.Text = "&Save" Then
                'Ali FAisal : TFS1457 : Handles the error if there is exception in saving record
                If SaveDetail() = True Then
                    VendorQuotationId = MasterDAL.Validate(Me.uitxtName.Text)
                    ''Start TFS2375
                    If IsEditMode = False Then
                        ''insert Approval Log
                        SaveApprovalLog(EnumReferenceType.VendorQuotation, VendorQuotationId, Me.uitxtName.Text.Trim, Me.dtpVQDate.Value.Date, "Vendor Quotation", Me.Name)
                    End If
                    ''End TFS2375

                    ''Start TFS2989
                    If IsEditMode = True Then
                        If ValidateApprovalProcessMapped(Me.uitxtName.Text.Trim, Me.Name) Then
                            If ValidateApprovalProcessIsInProgressAgain(Me.uitxtName.Text.Trim, Me.Name) = False Then
                                SaveApprovalLog(EnumReferenceType.VendorQuotation, VendorQuotationId, Me.uitxtName.Text.Trim, Me.dtpVQDate.Value.Date, "Vendor Quotation", Me.Name)
                            End If
                        End If
                    End If
                    ''End TFS2989
                    LoadDetailRecord()
                    ResetDetailControls()
                    GetAllRecords()
                    FillAllCombos()
                    msg_Information("Record has been updated successfully.")
                Else
                    msg_Error("An error occured while saving records.")
                End If
                'Ali Faisal : TFS1457 : End
                'Else
                '    msg_Error("Please save master record first")
                'End If
                'GetAllRecords()
                'Else
                '    'If IsValidToDelete("PurchaseInquiryDetail", "SalesInquiryId", SalesInquiryId) = True Then
                '    If Update1() Then
                '        ResetDetailControls()
                '        GetAllRecords()
                '    End If
                'Else
                'msg_Error(str_ErrorDependentRecordFound)
                'End If
                'End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdItems_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.ColumnButtonClick
        Try
            If e.Column.Key = "Remove" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                DetailDAL.DeleteDetailWithPurhcase(Val(Me.grdItems.GetRow.Cells("VendorQuotationDetailId").Value.ToString))
                Me.grdItems.GetRow.Delete()
                Me.grdItems.UpdateData()
            End If

            ' Saad add vendor button in grid to see all vendors against particular item
            If e.Column.Key = "Vendors" Then
                If Val(Me.grdItems.GetRow.Cells("ArticleId").Value.ToString) > 0 Then

                    Try
                        If frmViewItemVendors.isFormOpen = True Then
                            frmViewItemVendors.Dispose()
                        End If

                        frmViewItemVendors.MasterArticleId = Val(Me.grdItems.GetRow.Cells("ArticleId").Value.ToString)
                        frmViewItemVendors.ShowDialog()
                    Catch ex As Exception
                        ShowErrorMessage(ex.Message)
                    End Try

                Else
                    ShowErrorMessage("Vendors is not defined with this Item")

                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtComments_MouseHover(sender As Object, e As EventArgs) Handles txtComments.MouseHover
        Try
            If Me.txtComments.Text = "Comments" Then
                Me.txtComments.Text = ""
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtComments_MouseLeave(sender As Object, e As EventArgs) Handles txtComments.MouseLeave
        Try
            If Me.txtComments.Text = "" Then
                Me.txtComments.Text = "Comments"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtReferenceNo_MouseHover(sender As Object, e As EventArgs) Handles txtReferenceNo.MouseHover
        Try
            If Me.txtReferenceNo.Text = "Reference Number" Then
                Me.txtReferenceNo.Text = ""
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtReferenceNo_MouseLeave(sender As Object, e As EventArgs) Handles txtReferenceNo.MouseLeave
        Try
            If Me.txtReferenceNo.Text = "" Then
                Me.txtReferenceNo.Text = "Reference Number"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNetDiscount.KeyPress, txtQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_MouseHover(sender As Object, e As EventArgs) Handles txtQty.MouseHover
        Try
            If Me.txtQty.Text = "Qty" Then
                Me.txtQty.Text = ""
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_MouseLeave(sender As Object, e As EventArgs) Handles txtQty.MouseLeave
        Try
            If Me.txtQty.Text = "" Then
                Me.txtQty.Text = "Qty"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbReference_ValueChanged(sender As Object, e As EventArgs) Handles cmbReference.ValueChanged
        Try
            If Not Me.cmbReference.ActiveRow Is Nothing Then
                FillCombos("InquiryNumberAgainstCustomer")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPurchaseInquiry_ValueChanged(sender As Object, e As EventArgs) Handles cmbPurchaseInquiry.ValueChanged
        'Try
        '    If Me.cmbPurchaseInquiry.Value > 0 Then
        '        DisplayDetailForPurchase(Me.cmbPurchaseInquiry.Value)
        '        Me.cmbPurchaseInquiry.Enabled = False
        '        Me.cmbReference.Enabled = False
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Delete() Then
                GetAllRecords()
                ReSetControls()

                '' Add by Mohsin on 18 Sep 2017

                ' NOTIFICATION STARTS HERE FOR DELETE

                ' *** New Segment *** 
                '// Adding notification

                '// Creating new object of Notification configuration dal
                '// Dal will be used to get users list for the notification 
                Dim NDal1 As New NotificationConfigurationDAL
                Dim objmod1 As New VouchersMaster
                '// Creating new object of Agrius Notification class
                objmod1.Notification = New AgriusNotifications

                '// Reference document number
                objmod1.Notification.ApplicationReference = Me.uitxtName.Text

                '// Date of notification
                objmod1.Notification.NotificationDate = Now

                '// Preparing notification title string
                objmod1.Notification.NotificationTitle = "Vendor Quotation number [" & Me.uitxtName.Text & "]  is deleted."

                '// Preparing notification description string
                objmod1.Notification.NotificationDescription = "Vendor Quotation number [" & Me.uitxtName.Text & "] is deleted by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                '// Setting source application as refrence in the notification
                objmod1.Notification.SourceApplication = "Vendor Quotation"



                '// Starting to get users list to add child

                '// Creating notification detail object list
                Dim List1 As New List(Of NotificationDetail)

                '// Getting users list
                List1 = NDal1.GetNotificationUsers("Vendor Quotation Deleted")

                '// Adding users list in the Notification object of current inquiry
                objmod1.Notification.NotificationDetils.AddRange(List1)

                '// Getting and adding user groups list in the Notification object of current inquiry
                objmod1.Notification.NotificationDetils.AddRange(NDal1.GetNotificationGroups("Vendor Quotation Deleted"))

                '// Not getting role list because no role is associated at the moment
                '// We will need this in future and we can use it later
                '// We can consult to Update function of this class


                '// ***This segment will be used to save notification in database table

                '// Creating new list of objects of Agrius Notification
                Dim NList1 As New List(Of AgriusNotifications)

                '// Copying notification object from current sales inquiry to newly defined instance
                '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
                NList1.Add(objmod1.Notification)

                '// Creating object of Notification DAL
                Dim GNotification1 As New NotificationDAL

                '// Saving notification to database
                GNotification1.AddNotification(NList1)

                '// End Adding Notification

                '// End Adding Notification
                ' *** End Segment ***

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub txtDiscount_TextChanged(sender As Object, e As EventArgs) Handles txtDiscount.TextChanged
        Try
            'If Val(txtPrice.Text) > 0 Then
            Me.txtDiscountAmount.Text = (Val(txtPrice.Text) * Val(Me.txtDiscount.Text) / 100)
            TaxAmounts()
            GetNetPrice()
            'Me.txtPriceafterdiscount.Text = Val(txtPrice.Text) - Val(txtDiscountAmount.Text)
            'Me.txtNetPrice.Text = Val(Me.txtPriceafterdiscount.Text)
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        Try
            If Val(Me.txtAmount.Text) > 0 Then
                Me.txtNetCostValue.Text = (Val(Me.txtAmount.Text) + Val(Me.txtCharges.Text))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdCharges_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdCharges.CellUpdated

    End Sub

    Private Sub grdCharges_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdCharges.CellValueChanged
        'Try
        '    If e.Column.Index = 4 Then
        '        Me.grdItems.UpdateData()
        '        Me.txtCharges.Text = Val(Me.grdCharges.GetTotal(Me.grdCharges.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum))
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub grdCharges_MouseLeave(sender As Object, e As EventArgs) Handles grdCharges.MouseLeave
        Try
            Me.grdItems.UpdateData()
            Me.txtCharges.Text = Val(Me.grdCharges.GetTotal(Me.grdCharges.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_Click(sender As Object, e As EventArgs) Handles grdItems.Click
        Try
            If Me.grdItems.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Me.grdItems.UpdateData()
                'Ali Faisal : TFS1310 : Restrict to again loading of selected row in detail controls
                CurrentRowIndex = -1
                CurrentRowIndex = grdItems.CurrentRow.RowIndex
                If CurrentRowIndex = PreviousRowIndex Then
                    Exit Sub
                End If
                PreviousRowIndex = CurrentRowIndex
                ShowDetailRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPriceafterdiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPriceafterdiscount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSaleTax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSaleTax.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAddTax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAddTax.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtIncTax_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtIncTax.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCD_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCD.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetNetPrice()
        Try
            Dim NetPrice As Double = 0
            Dim PriceAfterDiscount As Double = 0
            PriceAfterDiscount = Val(txtPrice.Text) - Val(txtDiscountAmount.Text)
            Me.txtPriceafterdiscount.Text = PriceAfterDiscount
            NetPrice = (PriceAfterDiscount + Val(txtSaleTaxAmount.Text) + Val(txtAddTaxAmount.Text) + Val(txtCDAmount.Text) + Val(txtIncTaxAmount.Text))
            Me.txtNetPrice.Text = NetPrice
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscount_Leave(sender As Object, e As EventArgs) Handles txtDiscount.Leave
        Try
            'If Val(txtPrice.Text) > 0 Then
            Me.txtDiscountAmount.Text = (Val(txtPrice.Text) * Val(Me.txtDiscount.Text) / 100)
            TaxAmounts()
            GetNetPrice()
            'Me.txtPriceafterdiscount.Text = Val(txtPrice.Text) - Val(txtDiscountAmount.Text)
            'Me.txtNetPrice.Text = Val(Me.txtPriceafterdiscount.Text)
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSaleTax_Leave(sender As Object, e As EventArgs) Handles txtSaleTax.Leave
        Try
            'If Val(txtPriceafterdiscount.Text) > 0 Then
            Me.txtSaleTaxAmount.Text = (Val(txtPriceafterdiscount.Text) * Val(Me.txtSaleTax.Text) / 100)
            'Me.txtNetPrice.Text = Val(Me.txtNetPrice.Text) + Val(Me.txtSaleTaxAmount.Text)
            GetNetPrice()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAddTax_Leave(sender As Object, e As EventArgs) Handles txtAddTax.Leave
        Try
            'If Val(txtAddTax.Text) > 0 Then
            Me.txtAddTaxAmount.Text = (Val(txtPriceafterdiscount.Text) * Val(Me.txtAddTax.Text) / 100)
            'Me.txtNetPrice.Text += Val(Me.txtAddTaxAmount.Text)
            GetNetPrice()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtIncTax_Leave(sender As Object, e As EventArgs) Handles txtIncTax.Leave
        Try
            'If Val(txtIncTax.Text) > 0 Then
            Me.txtIncTaxAmount.Text = (Val(txtPriceafterdiscount.Text) * Val(Me.txtIncTax.Text) / 100)
            'Me.txtNetPrice.Text += Val(Me.txtIncTaxAmount.Text)
            GetNetPrice()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCD_Leave(sender As Object, e As EventArgs) Handles txtCD.Leave
        Try
            'If Val(txtCD.Text) > 0 Then
            Me.txtCDAmount.Text = (Val(txtPriceafterdiscount.Text) * Val(Me.txtCD.Text) / 100)
            'Me.txtNetPrice.Text += Val(Me.txtCDAmount.Text)
            GetNetPrice()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrice_Leave(sender As Object, e As EventArgs) Handles txtPrice.Leave
        Try
            'If Val(Me.txtPrice.Text) > 0 Then
            Me.txtNetPrice.Text = Val(Me.txtPrice.Text)
            Me.txtPriceafterdiscount.Text = Val(Me.txtPrice.Text)
            TaxAmounts()
            GetNetPrice()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        If e.Tab.Index = 0 Then
            CtrlGrdBar1.Visible = True
            CtrlGrdBar1.Enabled = True
            CtrlGrdBar2.Visible = False
            CtrlGrdBar2.Enabled = False
            Me.BtnSave.Visible = False
            Me.BtnDelete.Visible = True
            ddbGetRecord.Visible = False
        ElseIf e.Tab.Index = 1 Then
            CtrlGrdBar2.Visible = True
            CtrlGrdBar2.Enabled = True
            CtrlGrdBar1.Visible = False
            CtrlGrdBar1.Enabled = False
            Me.BtnSave.Visible = False
            Me.BtnDelete.Visible = False
            Me.ddbGetRecord.Visible = True
        End If
    End Sub
    Private Sub BaseCurrency()
        Try
            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                Me.txtCurrencyRate.Text = dr.Row.Item("CurrencyRate").ToString

                ' R@!    11-Jun-2016 Dollor account
                ' Setting default rate to zero
                If Val(Me.txtCurrencyRate.Text) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAttachment_ButtonClick(sender As Object, e As EventArgs) Handles btnAttachment.ButtonClick
        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
           "All files (*.*)|*.*"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                If Me.BtnSave.Text <> "&Save" Then
                    If Me.grdSaved.RowCount > 0 Then
                        intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells("No Of Attachments").Value.ToString)
                    End If
                End If
                Me.btnAttachment.Text = "Attachment (" & arrFile.Count + intCountAttachedFiles & ")"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If e.Column.Key = "No Of Attachments" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Me.grdSaved.GetRow.Cells("VendorQuotationId").Value.ToString
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdItems.FormattingRow
        Try
            Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
            If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim IsAlternate As Boolean = e.Row.Cells("Alternate").Value
                If IsAlternate Then
                    e.Row.Cells("Alternate").Text = "Yes"
                    rowStyle.BackColor = Color.LightYellow
                    e.Row.RowStyle = rowStyle
                    rowStyle = Nothing
                Else
                    e.Row.Cells("Alternate").Text = "No"
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetTop50ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GetTop50ToolStripMenuItem.Click
        Try
            GetTop50()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GetAllToolStripMenuItem.Click
        Try
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPurchaseInquiry_Leave(sender As Object, e As EventArgs) Handles cmbPurchaseInquiry.Leave
        Try
            If Me.cmbPurchaseInquiry.Value > 0 Then
                DisplayDetailForPurchase(Me.cmbPurchaseInquiry.Value)
                Me.cmbPurchaseInquiry.Enabled = False
                Me.cmbReference.Enabled = False
                ''Start TFS3106 : Ayesha Rehman : 23-04-2018
                Dim NetAmount As Double = 0
                If grdItems.RowCount > 0 Then
                    For Each r As Janus.Windows.GridEX.GridEXRow In grdItems.GetRows
                        NetAmount += r.Cells("NetCostValue").Value
                    Next
                End If
                Me.txtTotalAmount.Text = NetAmount
                ''End TFS3106
                FillInwardExpense(-1, "VQ") ''TFS3109
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            MasterModel = New VendorQuotationMaster()
            MasterModel.VendorQuotationId = VendorQuotationId
            If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                MasterModel.VendorQuotationNo = GetDocumentNo()
            Else
                MasterModel.VendorQuotationNo = Me.uitxtName.Text
            End If
            MasterModel.VendorQuotationNo = Me.uitxtName.Text
            MasterModel.VendorQuotationDate = Me.dtpVQDate.Value
            MasterModel.VendorQuotationExpiryDate = Me.dtpExpiryDate.Value
            MasterModel.VendorId = Me.cmbReference.Value
            MasterModel.ReferenceNo = Me.txtRefNo.Text
            MasterModel.PurchaseInquiryId = Me.cmbPurchaseInquiry.Value
            MasterModel.Remarks = Me.txtRemarks.Text
            MasterModel.UserName = LoginUserName

            ''Start TFS3106
            MasterModel.Amount = Val(Me.txtTotalAmount.Text)
            MasterModel.Discount = Val(Me.txtNetDiscount.Text)
            MasterModel.NetTotal = Val(Me.txtNetTotal.Text)
            ''End TFS3106

            'For I As Int32 = 0 To Me.grdItems.RowCount - 1
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetDataRows
                DetailModel = New VendorQuotationDetail()
                DetailModel.VendorQuotationDetailId = Val(Row.Cells("VendorQuotationDetailId").Value.ToString)
                DetailModel.VendorQuotationId = Val(Row.Cells("VendorQuotationId").Value.ToString)
                DetailModel.SerialNo = Row.Cells("SerialNo").Value.ToString
                DetailModel.RequirementDescription = Row.Cells("RequirementDescription").Value.ToString
                DetailModel.ArticleId = Val(Row.Cells("ArticleId").Value.ToString)
                'DetailModel.Code = Row.Cells("Code").Value.ToString
                'DetailModel.ArticleDescription = Row.Cells("ArticleDescription").Value.ToString
                DetailModel.UnitId = Val(Row.Cells("UnitId").Value.ToString)
                DetailModel.ItemTypeId = Val(Row.Cells("ItemTypeId").Value.ToString)
                DetailModel.CategoryId = Val(Row.Cells("CategoryId").Value.ToString)
                DetailModel.SubCategoryId = Val(Row.Cells("SubCategoryId").Value.ToString)
                DetailModel.OriginId = Val(Row.Cells("OriginId").Value.ToString)
                DetailModel.Qty = Val(Row.Cells("Qty").Value.ToString)
                DetailModel.QuotedTerms = Row.Cells("QuotedTerms").Value.ToString
                DetailModel.ValidityOfQuotation = Row.Cells("ValidityOfQuotation").Value.ToString
                DetailModel.DeliveryPeriod = Row.Cells("DeliveryPeriod").Value.ToString
                DetailModel.Warranty = Row.Cells("Warranty").Value.ToString
                DetailModel.ApproxGrossWeight = Row.Cells("ApproxGrossWeight").Value.ToString
                DetailModel.HSCode = Row.Cells("HSCode").Value.ToString
                DetailModel.DeliveryPort = Row.Cells("DeliveryPort").Value.ToString
                DetailModel.GenuineOrReplacement = Row.Cells("GenuineOrReplacement").Value.ToString
                DetailModel.LiteratureOrDatasheet = Row.Cells("LiteratureOrDatasheet").Value.ToString
                DetailModel.NewOrRefurbish = Row.Cells("NewOrRefurbish").Value.ToString
                DetailModel.ExWorks = Row.Cells("ExWorks").Value.ToString
                DetailModel.Price = Val(Row.Cells("Price").Value.ToString)
                DetailModel.DiscountPer = Val(Row.Cells("DiscountPer").Value.ToString)
                'DetailModel.DiscountAmount = Val(Row.Cells("DiscountAmount").Value.ToString)
                DetailModel.SalesTaxPer = Val(Row.Cells("SalesTaxPer").Value.ToString)
                DetailModel.AddTaxPer = Val(Row.Cells("AddTaxPer").Value.ToString)
                DetailModel.IncTaxPer = Val(Row.Cells("IncTaxPer").Value.ToString)
                DetailModel.CDPer = Val(Row.Cells("CDPer").Value.ToString)
                DetailModel.NetPrice = Val(Row.Cells("NetPrice").Value.ToString)
                DetailModel.OtherCharges = Val(Row.Cells("OtherCharges").Value.ToString)
                DetailModel.PurchaseInquiryDetailId = Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString)
                DetailModel.ReferenceNo = Row.Cells("ReferenceNo").Value.ToString
                DetailModel.Comments = Row.Cells("Comments").Value.ToString
                DetailModel.HeadArticleId = Val(Row.Cells("HeadArticleId").Value.ToString)
                DetailModel.BaseCurrencyId = Val(Row.Cells("BaseCurrencyId").Value.ToString)
                DetailModel.BaseCurrencyRate = Val(Row.Cells("BaseCurrencyRate").Value.ToString)
                DetailModel.CurrencyId = Val(Row.Cells("CurrencyId").Value.ToString)
                DetailModel.CurrencyRate = Val(Row.Cells("CurrencyRate").Value.ToString)
                DetailModel.CurrencySymbol = Row.Cells("CurrencySymbol").Value.ToString
                If Not IsDBNull(Row.Cells("Alternate").Value) = True Then
                    DetailModel.IsAlternate = Row.Cells("Alternate").Value
                Else
                    DetailModel.IsAlternate = False
                End If
                MasterModel.DetailList.Add(DetailModel)
            Next
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetRows
            'DetailModel = New VendorQuotationDetail()
            'DetailModel.VendorQuotationDetailId = VendorQuotationDetailId
            'DetailModel.VendorQuotationId = VendorQuotationId
            'DetailModel.SerialNo = SerialNo
            'DetailModel.RequirementDescription = Me.txtRequirementDescription.Text
            'DetailModel.ArticleId = Me.cmbItem.Value
            ''DetailModel.Code = Row.Cells("Code").Value.ToString
            ''DetailModel.ArticleDescription = Row.Cells("ArticleDescription").Value.ToString
            'DetailModel.UnitId = Me.cmbUnit.Value
            'DetailModel.ItemTypeId = Me.cmbType.Value
            'DetailModel.CategoryId = Me.cmbCategory.Value
            'DetailModel.SubCategoryId = Me.cmbSubCategory.Value
            'DetailModel.OriginId = Me.cmbOrigin.Value
            'DetailModel.Qty = Val(Me.txtQty.Text)
            'DetailModel.QuotedTerms = Me.txtQuotation.Text
            'DetailModel.ValidityOfQuotation = Me.txtValidityOfQuotation.Text
            'DetailModel.DeliveryPeriod = Me.txtDeliveryPeriod.Text
            'DetailModel.Warranty = Me.txtWarranty.Text
            'DetailModel.ApproxGrossWeight = Me.txtApproxgrossweight.Text
            'DetailModel.HSCode = txtHSCode.Text
            'DetailModel.DeliveryPort = Me.txtDeliveryPort.Text
            'DetailModel.GenuineOrReplacement = Me.txtGenuineOrReplacement.Text
            'DetailModel.LiteratureOrDatasheet = Me.txtLiteratureDatasheet.Text
            'DetailModel.NewOrRefurbish = Me.txtNewRefurbish.Text
            'DetailModel.Price = Val(Me.txtPrice.Text)
            'DetailModel.DiscountPer = Val(Me.txtDiscount.Text)
            ''DetailModel.DiscountAmount = Val(Row.Cells("DiscountAmount").Value.ToString)
            'DetailModel.SalesTaxPer = Val(Me.txtSaleTax.Text)
            'DetailModel.AddTaxPer = Val(Me.txtAddTax.Text)
            'DetailModel.IncTaxPer = Val(Me.txtIncTax.Text)
            'DetailModel.CDPer = Val(Me.txtCD.Text)
            'DetailModel.NetPrice = Val(Me.txtNetPrice.Text)
            'DetailModel.OtherCharges = Val(Me.txtCharges.Text)
            'DetailModel.PurchaseInquiryDetailId = PurchaseInquiryDetailId
            'DetailModel.ReferenceNo = Me.txtReferenceNo.Text
            'DetailModel.Comments = Me.txtComments.Text
            'DetailModel.HeadArticleId = HeadArticleId
            'DetailModel.BaseCurrencyId = BaseCurrencyId
            'DetailModel.BaseCurrencyRate = 1
            'DetailModel.CurrencyId = Me.cmbCurrency.SelectedValue
            'DetailModel.CurrencyRate = Val(Me.txtCurrencyRate.Text)
            'DetailModel.CurrencySymbol = Me.cmbCurrency.Text
            'DetailModel.IsAlternate = IsAlternate
            'grdCharges.UpdateData()
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdCharges.GetRows
            '    If Val(Row.Cells("Amount").Value.ToString) > 0 Then
            '        Dim DetailCharges As VendorQuotationDetailCharges = New VendorQuotationDetailCharges
            '        DetailCharges.VendorQuotationDetailId = VendorQuotationDetailId
            '        DetailCharges.VendorQuotationChargesId = Val(Row.Cells("VendorQuotationDetailChargesId").Value.ToString)
            '        DetailCharges.VendorQuotationChargesTypeId = Val(Row.Cells("VendorQuotationChargesTypeId").Value.ToString)
            '        DetailCharges.Amount = Val(Row.Cells("Amount").Value.ToString)
            '        DetailModel.ChargesDetail.Add(DetailCharges)
            '    End If
            'Next

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1314 : 11-Aug-2017 : Remove the selected row from the grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdCharges_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdCharges.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                Me.grdCharges.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1314 : 11-Aug-2017 : Get the unsaved charges types to add new charges against selected Quotation
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGetChargesTypes_Click(sender As Object, e As EventArgs) Handles btnGetChargesTypes.Click
        Try
            Me.grdCharges.DataSource = DetailDAL.GetRemainingChargesTypes(VendorQuotationDetailId)
            Me.btnGetChargesTypes.Visible = True
            If Me.grdCharges.RootTable.Columns.Contains("Delete") = False Then
                Me.grdCharges.RootTable.Columns.Add("Delete")
                Me.grdCharges.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdCharges.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdCharges.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdCharges.RootTable.Columns("Delete").Key = "Delete"
                Me.grdCharges.RootTable.Columns("Delete").Caption = "Action"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Ayesha Rehman : TFS2375 : Show Approval History of the current Document
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnApprovalHistory_Click(sender As Object, e As EventArgs) Handles btnApprovalHistory.Click
        Try
            frmApprovalHistory.DocumentNo = Me.uitxtName.Text
            frmApprovalHistory.Source = Me.Name
            frmApprovalHistory.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtNetDiscount_Leave(sender As Object, e As EventArgs) Handles txtNetDiscount.Leave
        Try
            If Not (Val(txtNetDiscount.Text) >= 0 AndAlso Val(txtNetDiscount.Text) <= Val(txtNetTotal.Text)) Then
                ShowErrorMessage("Enter value according to the Amount")
                txtNetDiscount.SelectAll()
                Me.txtNetDiscount.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtNetDiscount_LostFocus(sender As Object, e As EventArgs) Handles txtNetDiscount.LostFocus
        Try
            DiscountCalculation()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalAmount_TextChanged(sender As Object, e As EventArgs) Handles txtTotalAmount.TextChanged
        Try
            txtNetTotal.Text = Val(txtTotalAmount.Text)
            DiscountCalculation()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtNetCostValue_TextChanged(sender As Object, e As EventArgs) Handles txtNetCostValue.TextChanged
        Try
            If Me.btnSave1.Text = "&Update" Then
                txtTotalAmount.Text = Val(txtNetCostValue.Text) + (Me.grdItems.GetTotal(Me.grdItems.RootTable.Columns("NetCostValue"), Janus.Windows.GridEX.AggregateFunction.Sum)) - (Me.grdItems.GetRow.Cells("NetCostValue").Value)
            Else
                txtTotalAmount.Text = Val(txtNetCostValue.Text) + (Me.grdItems.GetTotal(Me.grdItems.RootTable.Columns("NetCostValue"), Janus.Windows.GridEX.AggregateFunction.Sum))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub FillInwardExpense(VendorID As Integer, Optional DocType As String = "")
        Try
            'Dim dtInwardExpDetail As DataTable = GetDataTable("Select IsNull(Exp_D.PurchaseId,0) as PurchaseId, IsNull(coa.coa_detail_id,0)  as AccountId ,coa.detail_title, IsNull(Exp_Amount,0) as Exp_Amount From tblDefInwardAccounts INNER JOIN vwCOADetail coa on coa.coa_detail_id = tblDefInwardAccounts.InwardAccountId INNER JOIN (select PurchaseId, AccountId,Exp_Amount From InwardExpenseDetailTable WHERE PurchaseId=" & VendorID & " AND DocType=N'" & DocType & "') Exp_D  ON Exp_D.AccountId = COA.coa_detail_id")
            Dim dtInwardExpDetail As DataTable = GetDataTable("Select Exp.PurchaseId, tblDefInwardAccounts.InwardAccountId as AccountId,vw.Detail_Title,IsNull(Exp_Amount,0) as Exp_Amount From vwCoaDetail vw INNER JOIN tblDefInwardAccounts on tblDefInwardAccounts.InwardAccountId = vw.coa_detail_id LEFT OUTER JOIN (select PurchaseId,AccountId,Exp_Amount From InwardExpenseDetailTable WHERE PurchaseId=" & VendorID & " AND DocType=N'" & DocType & "') Exp ON Exp.AccountId = vw.coa_detail_id")
            Me.grdInwardExpDetail.DataSource = dtInwardExpDetail
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAddExp_Click_1(sender As Object, e As EventArgs) Handles btnAddExp.Click
        Try

            If Me.cmbInwardExpense.IsItemInList = False Then Exit Sub
            If Me.cmbInwardExpense.ActiveRow Is Nothing Then Exit Sub
            Dim i As Integer = 0
            For i = 0 To grdInwardExpDetail.GetRows.Length - 1
                If cmbInwardExpense.ActiveRow.Cells(0).Value = grdInwardExpDetail.GetRows(i).Cells(1).Value Then
                    ShowErrorMessage("This Account Already Exists")
                    Me.cmbInwardExpense.Focus()
                    Exit Sub
                End If
            Next

            If Con Is Nothing Then Exit Sub


            Dim objcon As New OleDbConnection(Con.ConnectionString)
            Dim cmd As New OleDbCommand

            If objcon.State = ConnectionState.Closed Then objcon.Open()
            Dim objtrans As OleDbTransaction = objcon.BeginTransaction
            cmd.Connection = objcon
            cmd.Transaction = objtrans
            cmd.CommandType = CommandType.Text

            cmd.CommandText = String.Empty
            cmd.CommandText = "INSERT INTO tblDefInwardAccounts(InwardAccountId,Active) VALUES(" & Me.cmbInwardExpense.Value & ",1) Select @@Identity"

            cmd.ExecuteNonQuery()
            objtrans.Commit()

            objcon.Close()
            objtrans.Dispose()
            objcon.Dispose()

            Dim dt As DataTable = CType(Me.grdInwardExpDetail.DataSource, DataTable)
            dt.AcceptChanges()
            Dim dr As DataRow
            dr = dt.NewRow
            If Me.btnSave1.Text <> "&Save" Then
                dr(0) = Val(Me.grdSaved.GetRow.Cells("VendorQuotationId").Value.ToString)
            Else
                dr(0) = 0
            End If
            dr(1) = Me.cmbInwardExpense.Value
            dr(2) = Me.cmbInwardExpense.ActiveRow.Cells("Account Title").Value.ToString
            dr(3) = 0
            dt.Rows.Add(dr)
            dt.AcceptChanges()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdInwardExpDetail_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdInwardExpDetail.ColumnButtonClick
        Try

            If Not IsValidToDeleteAccount() Then
                ShowErrorMessage("Dependent Record Exist Against This Account ,Can't be deleted")
                Exit Sub
            End If
            Dim objcon As New OleDbConnection(Con.ConnectionString)
            Dim cmd As New OleDbCommand

            If objcon.State = ConnectionState.Closed Then objcon.Open()
            Dim objtrans As OleDbTransaction = objcon.BeginTransaction
            cmd.Connection = objcon
            cmd.Transaction = objtrans
            cmd.CommandType = CommandType.Text

            cmd.CommandText = String.Empty
            cmd.CommandText = "Delete From tblDefInwardAccounts  WHERE InwardAccountId=" & Val(grdInwardExpDetail.GetRow.Cells("AccountId").Value.ToString) & ""

            cmd.ExecuteNonQuery()
            objtrans.Commit()

            objcon.Close()
            objtrans.Dispose()
            objcon.Dispose()

            Me.grdInwardExpDetail.GetRow.Delete()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Thsi Function is made to insure that ,dependent accounts cannot be deleted
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Ayesha Rehman : 02-05-2018 : TFS3142</remarks>
    Private Function IsValidToDeleteAccount() As Boolean
        Try
            Dim str As String = "Select AccountId from InwardExpenseDetailTable where DocType !='VQ' "
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    If Val(grdInwardExpDetail.GetRow.Cells("AccountId").Value.ToString) = Val(r.Item("AccountId").ToString) Then
                        Return False
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function


    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Vendor Quotation"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' TASK TFS3634
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAddPurchaseInquiryItems_Click(sender As Object, e As EventArgs) Handles btnAddPurchaseInquiryItems.Click
        Dim PurchaseInquiryIds As String = String.Empty
        Try
            If IsEditMode = True Then
                For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetRows
                    If PurchaseInquiryIds.Length > 0 Then
                        PurchaseInquiryIds = PurchaseInquiryIds & ", " & Row.Cells("PurchaseInquiryDetailId").Value.ToString
                    Else
                        PurchaseInquiryIds = Row.Cells("PurchaseInquiryDetailId").Value.ToString
                    End If
                Next
                Dim frmItems As New frmLoadPurchaseInquiryItems(Me.cmbPurchaseInquiry.Value, Me.cmbReference.Value, PurchaseInquiryIds)
                frmItems.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
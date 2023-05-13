''TASK TFS2019 Muhammad Ameen done on 04-01-2017. Added the option of generating Purchase Order from Comparison Statement.
''TASK TFS4661 Inquiry No should also be filtered against Date range. DONE BY MUHAMMAD AMIN ON 04-10-2018
''TASK TFS4667 Price and SalesTaxPercent should be saved separately in case TAXEXCLUDE configuration is on. Done by Muhammad Amin on 05-10-2018
Imports System.Windows.Forms
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class frmInquiryComparisonStatement
    Implements IGeneral
    Dim InquiryComparisonStatementDAL As New InquiryComparisonStatementDAL
    Dim Model As InquiryComparisonStatement
    Dim ModelList As List(Of InquiryComparisonStatement)
    Dim SalesQuotationModel As SalesQuotationMaster
    Dim SalesQuotationDetailModel As List(Of SalesQuotationDetail)
    Dim IsApproved As Boolean = True
    Dim flgCompanyRights As Boolean = False
    Dim EmailDAL As New EmailTemplateDAL
    Dim html As StringBuilder
    Dim VendorEmails As String = String.Empty
    Dim dtEmail As DataTable
    Dim AllFields As List(Of String)
    Dim EmailTemplate As String = String.Empty
    Dim AfterFieldsElement As String = String.Empty
    Dim VendorQuotationDetailId As Integer = 0
    Dim DetailDAL As New VendorQuotationDetailDAL
    Dim QuotedCostValue As Double = 0
    Dim Margin1 As Double = 0
    Dim flgAddingPO As Boolean = False
    Dim PurchaseOrder As PurchaseOrderMaster
    Dim DemandId As Integer = 0I
    Dim AutoEmail As Boolean = False
    Dim QuotationNo As String = String.Empty
    Dim flgExcludeTaxPrice As Boolean = False
    Dim EmailBody As String = String.Empty
    Dim EmailToUser As Boolean = False
    Dim TaxExcludeFromRateOnQuotationFromCS As Boolean = False
    Enum ComparisonStatement
        VendorQuotationDetailId
        VendorQuotationId
        VendorId
        VendorCode
        Vendor
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
        BaseCurrencyId
        BaseCurrencyRate
        CurrencyId
        Currency
        CurrencyRate
        CurrencySymbol
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
        Margin
        QuotedCostValue
        PurchaseInquiryId
        PurchaseInquiryDetailId
        ReferenceNo
        Comments
        HeadArticleId
        Approved
        InquiryComparisonStatementId
    End Enum

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub FillCombos(ByVal Condition As String)
        Dim strQuery As String = String.Empty
        Try
            If Condition = "Customer" Then
                strQuery = String.Empty
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    strQuery = "SELECT Distinct IsNull(PurchaseInquiryVendors.VendorId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join PurchaseInquiryVendors ON vwCOADetail.coa_detail_id =  PurchaseInquiryVendors.VendorId WHERE vwCOADetail.account_type in('Vendor', 'Customer') and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbReference, strQuery)
                    Me.cmbReference.Rows(0).Activate()
                    Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    strQuery = "SELECT Distinct IsNull(PurchaseInquiryVendors.VendorId,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail Inner Join PurchaseInquiryVendors ON vwCOADetail.coa_detail_id =  PurchaseInquiryVendors.VendorId WHERE vwCOADetail.account_type = 'Vendor' and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbReference, strQuery)
                    Me.cmbReference.Rows(0).Activate()
                    Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                End If
            ElseIf Condition = "SalesInquiryNumber" Then
                strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster Where SalesInquiryId In(Select SalesInquiryDetail.SalesInquiryId From SalesInquiryDetail INNER JOIN SalesInquiryMaster ON SalesInquiryDetail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId GROUP BY SalesInquiryDetail.SalesInquiryId Having Sum(IsNull(SalesInquiryDetail.Qty, 0)-IsNull(SalesInquiryDetail.PurchasedQty, 0))>0) Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbPurchaseInquiry, strQuery)
                Me.cmbPurchaseInquiry.Rows(0).Activate()
                Me.cmbPurchaseInquiry.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True
            ElseIf Condition = "InquiryNumberAgainstCustomer" Then
                strQuery = "Select SalesInquiryId, SalesInquiryNo, SalesInquiryDate From SalesInquiryMaster Where SalesInquiryId In(Select SalesInquiryDetail.SalesInquiryId From SalesInquiryDetail INNER JOIN SalesInquiryMaster ON SalesInquiryDetail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId Where SalesInquiryMaster.CustomerId =" & Me.cmbReference.Value & " GROUP BY SalesInquiryDetail.SalesInquiryId Having Sum(IsNull(SalesInquiryDetail.Qty, 0)-IsNull(SalesInquiryDetail.PurchasedQty, 0))>0) Order By SalesInquiryDate DESC"
                FillUltraDropDown(Me.cmbPurchaseInquiry, strQuery)
                Me.cmbPurchaseInquiry.Rows(0).Activate()
                Me.cmbPurchaseInquiry.DisplayLayout.Bands(0).Columns("SalesInquiryId").Hidden = True
            ElseIf Condition = "InquiryNumber" Then
                strQuery = " Select PurchaseInquiryId, PurchaseInquiryNo, PurchaseInquiryDate From PurchaseInquiryMaster WHERE PurchaseInquiryId > 0 " & IIf(Me.dtpInquiryFromDate.Checked = True, " AND CONVERT(VARCHAR, PurchaseInquiryDate, 102) >= CONVERT(DATETIME, N'" & dtpInquiryFromDate.Value.ToString("yyyy-M-dd 00:00:00") & "', 102) ", "") & " " & IIf(Me.dtpInquiryToDate.Checked = True, " AND CONVERT(VARCHAR, PurchaseInquiryDate, 102) <= CONVERT(DATETIME, N'" & dtpInquiryToDate.Value.ToString("yyyy-M-dd 23:59:59") & "', 102 )", "") & " Order By PurchaseInquiryDate DESC "
                FillUltraDropDown(Me.cmbPurchaseInquiry, strQuery)
                Me.cmbPurchaseInquiry.Rows(0).Activate()
                Me.cmbPurchaseInquiry.DisplayLayout.Bands(0).Columns("PurchaseInquiryId").Hidden = True
            ElseIf Condition = "InquiryNumberAgainstVendor" Then
                strQuery = " Select PurchaseInquiryMaster.PurchaseInquiryId, PurchaseInquiryMaster.PurchaseInquiryNo, PurchaseInquiryMaster.PurchaseInquiryDate From PurchaseInquiryMaster INNER JOIN PurchaseInquiryVendors ON PurchaseInquiryMaster.PurchaseInquiryId = PurchaseInquiryVendors.PurchaseInquiryId Where PurchaseInquiryVendors.VendorId =" & Me.cmbReference.Value & " " & IIf(Me.dtpInquiryFromDate.Checked = True, " AND CONVERT(VARCHAR, PurchaseInquiryDate, 102) >= CONVERT(DATETIME, N'" & dtpInquiryFromDate.Value.ToString("yyyy-M-dd 00:00:00") & "', 102) ", "") & " " & IIf(Me.dtpInquiryToDate.Checked = True, " AND CONVERT(VARCHAR, PurchaseInquiryDate, 102) <= CONVERT(DATETIME, N'" & dtpInquiryToDate.Value.ToString("yyyy-M-dd 23:59:59") & "', 102) ", "") & " Order By PurchaseInquiryMaster.PurchaseInquiryDate DESC"
                FillUltraDropDown(Me.cmbPurchaseInquiry, strQuery)
                Me.cmbPurchaseInquiry.Rows(0).Activate()
                Me.cmbPurchaseInquiry.DisplayLayout.Bands(0).Columns("PurchaseInquiryId").Hidden = True
            ElseIf Condition = "Company" Then
                strQuery = "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & " And CompanyId Is Not Null) " _
                               & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable WHERE CompanyName <> '' " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")" _
                               & "Else " _
                               & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
                FillToolStripDropDown(Me.cmbCompany, strQuery, False)
            ElseIf Condition = "Category" Then
                strQuery = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                       & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                       & " Else " _
                       & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
                FillToolStripDropDown(cmbCategory, strQuery, False)
            ElseIf Condition = "Currency" Then ''TASK-407
                strQuery = String.Empty
                strQuery = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
                FillDropDown(Me.cmbCurrency, strQuery)
                'Me.cmbCurrency.SelectedValue = BaseCurrencyId
            ElseIf Condition = "NewCurrency" Then ''TASK-407
                strQuery = String.Empty
                strQuery = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
                FillDropDown(Me.cmbNewCurrency, strQuery)
                'Me.cmbCurrency.SelectedValue = BaseCurrencyId
            ElseIf Condition = "TermsCondition" Then
                FillToolStripDropDown(Me.cmbTermAndCondition, "Select * From tblTermsAndConditionType", True)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Visible = True
            Me.lblProgress.Text = "Please wait........"
            DisplayDetail()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub DisplayDetail()
        Try
            Dim VendorId As Integer = 0
            Dim FromDate As New DateTime
            Dim ToDate As New DateTime
            Dim PurchaseInquiryId As Integer = 0
            If Me.cmbReference.Value > 0 Then
                VendorId = Me.cmbReference.Value
            End If
            If dtpInquiryFromDate.Checked = True Then
                FromDate = dtpInquiryFromDate.Value
            Else
                FromDate = DateTime.MinValue
            End If
            If dtpInquiryToDate.Checked = True Then
                ToDate = dtpInquiryToDate.Value
            Else
                ToDate = DateTime.MinValue
            End If
            If Me.cmbPurchaseInquiry.Value > 0 Then
                PurchaseInquiryId = Me.cmbPurchaseInquiry.Value
            End If
            Dim dt As New DataTable
            dt = InquiryComparisonStatementDAL.DisplayDetail(VendorId, PurchaseInquiryId, FromDate, ToDate)

            dt.AcceptChanges()
            Me.grdItems.DataSource = dt
            'If Me.grdItems.RowCount > 0 Then
            '    For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetDataRows
            '        If Row.RowType = Janus.Windows.GridEX.RowType.Record Then
            '            Dim IsTrue As Boolean = Row.Cells("Approved").Value
            '            If IsTrue = True Then
            '                Row.Cells("Approved").Column.ButtonText = "UpApprove"
            '            Else
            '                Row.Cells("Approved").Column.ButtonText = "Approve"
            '            End If
            '        End If
            '    Next
            'End If
            'If Me.grdItems.RowCount > 0 Then
            '    For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetDataRows
            '        If row.Cells("Genuine").Value = "Genuine" AndAlso row.Cells("Replacement").Value = "Genuine" Then
            '            row.BeginEdit()
            '            'row.Cells.Item("Genuine").Column.Visible = True
            '            row.Cells.Item("Replacement").Column.Visible = False
            '            row.EndEdit()
            '            'Me.grdItems.RootTable.Columns("Replacement").Visible = False
            '            'Me.grdItems.RootTable.Columns("Genuine").Visible = True
            '        ElseIf row.Cells("Replacement").Value = "Replacement" AndAlso row.Cells("Genuine").Value = "Replacement" Then
            '            row.BeginEdit()
            '            row.Cells.Item("Genuine").Column.Visible = False
            '            'row.Cells.Item("Replacement").Column.Visible = True
            '            row.EndEdit()
            '            'Me.grdItems.RootTable.Columns("Genuine").Visible = False
            '            'Me.grdItems.RootTable.Columns("Replacement").Visible = True
            '        End If
            '        'If Me.grdItems.GetRow.Cells("New").Value = "New" Then
            '        '    Me.grdItems.RootTable.Columns("Refurbish").Visible = False
            '        'Else
            '        '    Me.grdItems.RootTable.Columns("New").Visible = False
            '        'End If
            '    Next
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSearchSalesInquiry_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            FillCombos("Customer")
            FillCombos("InquiryNumber")
            FillCombos("Company")
            FillCombos("Category")
            FillCombos("Currency")
            FillCombos("NewCurrency")
            FillCombos("TermsCondition")
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            ''TASK TFS4667

            If Not getConfigValueByType("TaxExcludeFromRateOnQuotationFromCS").ToString = "Error" Then
                TaxExcludeFromRateOnQuotationFromCS = CBool(getConfigValueByType("TaxExcludeFromRateOnQuotationFromCS"))
            End If

            ''END TASK TFS4667
            GetAllRecords1()
            Me.BtnDelete.Enabled = False
            Me.BtnSave.Enabled = False
            Me.SplitContainer1.Panel2Collapsed = True
            ResetDetailControls()
            ''TASK TFS2019
            If Not getConfigValueByType("POFromCS").ToString = "Error" Then
                flgAddingPO = getConfigValueByType("POFromCS")
                If flgAddingPO = False Then
                    Me.btnCreateSalesQuotation.Text = "Sales Quotation"
                    Me.Panel1.Visible = True
                Else
                    Me.btnCreateSalesQuotation.Text = "Purchase Order"
                    Me.Panel1.Visible = False
                End If
            End If
            ''TASK TFS4437
            If getConfigValueByType("AutoEmail").ToString = "True" Then
                AutoEmail = True
            Else
                AutoEmail = False
            End If
            ''TASK TFS4659
            If getConfigValueByType("EmailToUser").ToString = "True" Then
                EmailToUser = True
            Else
                EmailToUser = False
            End If
            '' END TASK TFS4659
            If Not getConfigValueByType("ExcludeTaxPrice").ToString = "Error" Then
                flgExcludeTaxPrice = Convert.ToBoolean(getConfigValueByType("ExcludeTaxPrice").ToString)
            End If
            ''END TASK TFS4437
            ''END TASK TFS2019
            'Me.DisplayChargesTypes()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs)
        Try
            'If e.Column.Key = "Remove" Then
            '    If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            '    SalesInquiryRightsDAL.Delete(Val(Me.grdItems.GetRow.Cells("SalesInquiryRightsId").Value.ToString))
            '    Me.grdItems.GetRow.Delete()
            '    Me.grdItems.UpdateData()
            'End If
            'If e.Column.Key = "Approve" Then
            '    If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            '    SalesInquiryRightsDAL.Delete(Val(Me.grdItems.GetRow.Cells("SalesInquiryRightsId").Value.ToString))
            '    Me.grdItems.GetRow.Delete()
            '    Me.grdItems.UpdateData()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSearchSalesInquiry_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Me.grdItems.DataSource = Nothing
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbReference_ValueChanged(sender As Object, e As EventArgs) Handles cmbReference.ValueChanged
        Try
            If Me.cmbReference.Value > 0 Then
                FillCombos("InquiryNumberAgainstVendor")
            Else
                FillCombos("InquiryNumber")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos1(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            ModelList = New List(Of InquiryComparisonStatement)
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetCheckedRows
                Model = New InquiryComparisonStatement
                Model.InquiryComparisonStatementId = Val(Row.Cells("InquiryComparisonStatementId").Value.ToString)
                Model.VendorQuotationDetailId = Val(Row.Cells("VendorQuotationDetailId").Value.ToString)
                Model.VendorId = Val(Row.Cells("VendorId").Value.ToString)
                Model.PurchaseInquiryDetailId = Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString)
                Model.PurchaseInuiryId = Val(Row.Cells("PurchaseInquiryId").Value.ToString)
                Model.NetCostValue = Val(Row.Cells("NetCostValue").Value.ToString)
                Model.Margin = Val(Row.Cells("Margin").Value.ToString)
                Model.QuotedCostValue = Val(Row.Cells("QuotedCostValue").Value.ToString)
                Model.Approved = IIf(Row.Cells("Approved").Text = "Approve", True, False)
                Model.UserName = LoginUserName
                Model.Dated = Now
                Model.HeadArticleId = Val(Row.Cells("HeadArticleId").Value.ToString)
                ModelList.Add(Model)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillSingleModel(Optional Condition As String = "")
        Try
            Me.grdItems.UpdateData()
            ModelList = New List(Of InquiryComparisonStatement)
            Model = New InquiryComparisonStatement
            Model.InquiryComparisonStatementId = Val(Me.grdItems.GetRow.Cells("InquiryComparisonStatementId").Value.ToString)
            Model.VendorQuotationDetailId = Val(Me.grdItems.GetRow.Cells("VendorQuotationDetailId").Value.ToString)
            Model.VendorId = Val(Me.grdItems.GetRow.Cells("VendorId").Value.ToString)
            Model.PurchaseInquiryDetailId = Val(Me.grdItems.GetRow.Cells("PurchaseInquiryDetailId").Value.ToString)
            Model.PurchaseInuiryId = Val(Me.grdItems.GetRow.Cells("PurchaseInquiryId").Value.ToString)
            Model.NetCostValue = Val(Me.grdItems.GetRow.Cells("NetCostValue").Value.ToString)
            Model.Margin = Val(Me.grdItems.GetRow.Cells("Margin").Value.ToString)
            Model.QuotedCostValue = Val(Me.grdItems.GetRow.Cells("QuotedCostValue").Value.ToString)
            IsApproved = IIf(Me.grdItems.GetRow.Cells("Approved").Text = "Approve", True, False)
            Model.Approved = IsApproved
            Model.UserName = LoginUserName
            Model.Dated = Now
            Model.HeadArticleId = Val(Me.grdItems.GetRow.Cells("HeadArticleId").Value.ToString)
            Model.Qty = Val(Me.grdItems.GetRow.Cells("Qty").Value.ToString)
            Model.Price = Val(Me.grdItems.GetRow.Cells("Price").Value.ToString)
            Model.DiscountPer = Val(Me.grdItems.GetRow.Cells("DiscountPer").Value.ToString)
            Model.SalesTaxPer = Val(Me.grdItems.GetRow.Cells("SalesTaxPer").Value.ToString)
            Model.AddTaxPer = Val(Me.grdItems.GetRow.Cells("AddTaxPer").Value.ToString)
            Model.IncTaxPer = Val(Me.grdItems.GetRow.Cells("IncTaxPer").Value.ToString)
            Model.CDPer = Val(Me.grdItems.GetRow.Cells("CDPer").Value.ToString)
            Model.NetPrice = Val(Me.grdItems.GetRow.Cells("NetPrice").Value.ToString)
            Model.OtherCharges = Val(Me.grdItems.GetRow.Cells("OtherCharges").Value.ToString)
            Model.CurrencyId = Val(Me.grdItems.GetRow.Cells("CurrencyId").Value.ToString)
            Model.CurrencyRate = Val(Me.grdItems.GetRow.Cells("CurrencyRate").Value.ToString)
            Model.CurrencySymbol = Me.grdItems.GetRow.Cells("CurrencySymbol").Value.ToString
            'Ali Faisal : TFS1355 : Fill model for Comments
            Model.Comments = Me.grdItems.GetRow.Cells("Comments").Value.ToString
            'Ali Faisal : TFS1355 : End
            Model.RequirementDescription = Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString
            ModelList.Add(Model)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords1(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            'Convert(Bit, IsNull(Unit.Approved, 0)) As Approved
            Me.grdSaved.DataSource = InquiryComparisonStatementDAL.GetAllRecords()
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns.Add("Column1")
            Me.grdSaved.RootTable.Columns("Column1").ActAsSelector = True
            Me.grdSaved.RootTable.Columns("Column1").UseHeaderSelector = True
            Me.grdSaved.RootTable.Columns("InquiryComparisonStatementId").Visible = False
            Me.grdSaved.RootTable.Columns("VendorQuotationDetailId").Visible = False
            Me.grdSaved.RootTable.Columns("RequirementDescription").Caption = "Requirement Description"
            Me.grdSaved.RootTable.Columns("ArticleId").Visible = False
            Me.grdSaved.RootTable.Columns("Code").Visible = False
            Me.grdSaved.RootTable.Columns("ArticleDescription").Caption = "Article Description"
            Me.grdSaved.RootTable.Columns("VendorId").Visible = False
            Me.grdSaved.RootTable.Columns("CustomerId").Visible = False
            Me.grdSaved.RootTable.Columns("VendorCode").Visible = False
            Me.grdSaved.RootTable.Columns("PurchaseInquiryId").Visible = False
            Me.grdSaved.RootTable.Columns("PurchaseInquiryDetailId").Visible = False
            Me.grdSaved.RootTable.Columns("NetCostValue").Caption = "Net Cost Value"
            Me.grdSaved.RootTable.Columns("QuotedCostValue").Caption = "Quoted Value"
            Me.grdSaved.RootTable.Columns("UserName").Caption = "User Name"
            Me.grdSaved.RootTable.Columns("Approved").Width = 50
            Me.grdSaved.RootTable.Columns("Dated").Caption = "Date"
            Me.grdSaved.RootTable.Columns("Dated").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("InquiryComparisonStatementId").Visible = False
            Me.grdSaved.RootTable.Columns("VendorQuotationDetailId").Visible = False
            Me.grdSaved.RootTable.Columns("HeadArticleId").Visible = False
            Me.grdSaved.RootTable.Columns("RequirementDescription").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("ArticleId").Visible = False
            Me.grdSaved.RootTable.Columns("Code").Visible = False
            Me.grdSaved.RootTable.Columns("ArticleDescription").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("VendorId").Visible = False
            Me.grdSaved.RootTable.Columns("CustomerId").Visible = False
            Me.grdSaved.RootTable.Columns("VendorCode").Visible = False
            Me.grdSaved.RootTable.Columns("PurchaseInquiryId").Visible = False
            Me.grdSaved.RootTable.Columns("PurchaseInquiryDetailId").Visible = False
            Me.grdSaved.RootTable.Columns("VendorQuotationId").Visible = False
            Me.grdSaved.RootTable.Columns("NetCostValue").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("QuotedCostValue").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("UserName").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Approved").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Email").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdSaved.RootTable.Columns("Dated").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Vendor").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Customer").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Margin").EditType = Janus.Windows.GridEX.EditType.NoEdit
            grdSaved.RootTable.Columns("No Of Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdSaved.RootTable.Columns("Alternate").Visible = False
            Me.grdSaved.RootTable.Columns("SalesInquiryId").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.grdItems.GetCheckedRows.Length <= 0 Then
                msg_Error("No row is selected. At least one row is required")
                Me.grdItems.Focus() : IsValidate = False : Exit Function
            End If
            If Me.grdItems.RowCount = 0 Then
                msg_Error("No record is found in the grid")
                Me.grdItems.Focus() : IsValidate = False : Exit Function
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidateQuotation(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean
        Try
            If Me.grdSaved.GetCheckedRows.Length <= 0 Then
                msg_Error("No row is selected. At least one row is required")
                Me.grdSaved.Focus() : IsValidateQuotation = False : Exit Function
            End If
            If Me.grdSaved.RowCount = 0 Then
                msg_Error("No record is found in the grid")
                Me.grdSaved.Focus() : IsValidateQuotation = False : Exit Function
            End If
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            '    If InquiryComparisonStatementDAL.IsQuotationExists(Row.Cells("VendorQuotationDetailId").Value.ToString) Then
            '        msg_Error("Sales quotation has already been created")
            '        Me.grdSaved.Focus() : IsValidateQuotation = False : Exit Function
            '    End If
            'Next
            'FillSalesQuotationModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If ModelList.Count > 0 Then
                InquiryComparisonStatementDAL.Add(ModelList)
                msg_Information("Record has been updated successfully.")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() Then
                If Save() Then
                    DisplayDetail()
                    GetAllRecords1()
                    'Me.cmbReference.Rows(0).Activate()
                    'If Me.grdItems.RowCount > 0 Then
                    '    Me.grdItems.DataSource = Nothing
                    'End If
                    'msg_Information("Record has been updated successfully.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        'Dim Groups As String = String.Empty
        'Try
        '    'For Each SelectedItems As DataRowView In ListBox1.SelectedItems
        '    '    If Groups = String.Empty Then
        '    '        Groups = SelectedItems.Item(0).ToString
        '    '    Else
        '    '        Groups += "," & SelectedItems.Item(0).ToString
        '    '    End If
        '    'Next
        '    Me.grdItems.DataSource = SalesInquiryRightsDAL.GetAgainstGroup(Val(Me.ListBox1.SelectedValue.ToString))
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs)
        Dim Groups As String = String.Empty
        Try
            'For Each SelectedItems As DataRowView In ListBox1.SelectedItems
            '    If Groups = String.Empty Then
            '        Groups = SelectedItems.Item(0).ToString
            '    Else
            '        Groups += "," & SelectedItems.Item(0).ToString
            '    End If
            'Next
            'Me.grdItems.DataSource = SalesInquiryRightsDAL.GetAgainstGroups(Groups)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub GetSecurityRights()
    '    Try
    '        If LoginGroup = "Administrator" Then
    '            Me.BtnSave.Enabled = True
    '            Me.BtnDelete.Enabled = True
    '            'Me.BtnPrint.Enabled = True
    '            Exit Sub
    '        End If
    '        If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
    '            If RegisterStatus = EnumRegisterStatus.Expired Then
    '                Me.BtnSave.Enabled = False
    '                Me.BtnDelete.Enabled = False
    '                'Me.BtnPrint.Enabled = False
    '                'Me.PrintListToolStripMenuItem.Enabled = False
    '                'PrintToolStripMenuItem.Enabled = False
    '                Exit Sub
    '            End If
    '            Dim dt As DataTable = GetFormRights(EnumForms.frmSalesInquiryRights)
    '            If Not dt Is Nothing Then
    '                If Not dt.Rows.Count = 0 Then
    '                    If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
    '                        Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
    '                    Else
    '                        Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
    '                    End If
    '                    Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
    '                    'Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
    '                    Me.Visible = dt.Rows(0).Item("View_Rights").ToString
    '                End If
    '            End If


    '        Else
    '            'Me.Visible = False
    '            Me.BtnSave.Enabled = False
    '            Me.BtnDelete.Enabled = False
    '            'Me.BtnPrint.Enabled = False
    '            'CtrlGrdBar1.mGridPrint.Enabled = False
    '            'CtrlGrdBar1.mGridExport.Enabled = False
    '            'CtrlGrdBar1.mGridChooseFielder.Enabled = False

    '            For Each RightsDt As GroupRights In Rights
    '                If RightsDt.FormControlName = "View" Then
    '                    'Me.Visible = True
    '                ElseIf RightsDt.FormControlName = "Save" Then
    '                    If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
    '                ElseIf RightsDt.FormControlName = "Update" Then
    '                    If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
    '                ElseIf RightsDt.FormControlName = "Delete" Then
    '                    Me.BtnDelete.Enabled = True
    '                ElseIf RightsDt.FormControlName = "Print" Then
    '                    'Me.BtnPrint.Enabled = True
    '                    'CtrlGrdBar1.mGridPrint.Enabled = True
    '                ElseIf RightsDt.FormControlName = "Export" Then

    '                    'End Task:2395
    '                    'Task:2406 Added Field Chooser Rights
    '                ElseIf RightsDt.FormControlName = "Field Chooser" Then
    '                    'CtrlGrdBar1.mGridChooseFielder.Enabled = True
    '                    'End Task:2406
    '                End If
    '            Next
    '        End If
    '    Catch ex As Exception
    '        msg_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            Me.cmbReference.Rows(0).Activate()
            FillCombos("InquiryNumber")
            Me.SplitContainer1.Panel2Collapsed = True
            If Me.grdItems.RowCount > 0 Then
                Me.grdItems.DataSource = Nothing
            End If
            GetAllRecords1()
            ResetDetailControls()
            ''TASK TFS2019
            If Not getConfigValueByType("POFromCS").ToString = "Error" Then
                flgAddingPO = getConfigValueByType("POFromCS")
                If flgAddingPO = False Then
                    Me.btnCreateSalesQuotation.Text = "Sales Quotation"
                Else
                    Me.btnCreateSalesQuotation.Text = "Purchase Order"
                End If
            End If
            ''TASK TFS4437
            If getConfigValueByType("AutoEmail").ToString = "True" Then
                AutoEmail = True
            Else
                AutoEmail = False
            End If
            ''END TASK TFS4437
            ''END TASK TFS2019
            'Me.DisplayChargesTypes()
            ''TASK TFS4667
            If Not getConfigValueByType("TaxExcludeFromRateOnQuotationFromCS").ToString = "Error" Then
                TaxExcludeFromRateOnQuotationFromCS = CBool(getConfigValueByType("TaxExcludeFromRateOnQuotationFromCS"))
            End If
            ''END TASK TFS4667
            ''TASK TFS4659
            If getConfigValueByType("EmailToUser").ToString = "True" Then
                EmailToUser = True
            Else
                EmailToUser = False
            End If
            '' END TASK TFS4659
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdItems.FormattingRow
        'Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
        'Try
        '    If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
        '        Dim IsApprovedTrue As Boolean = e.Row.Cells("Approved").Value
        '        If IsApprovedTrue = True Then
        '            e.Row.Cells("Approved").Column.ButtonText = ""
        '            e.Row.Cells("Approved").Column.ButtonText = "Unapprove"
        '            rowStyle.BackColor = Color.GreenYellow
        '            e.Row.RowStyle = rowStyle
        '        Else
        '            e.Row.Cells("Approved").Column.ButtonText = ""
        '            e.Row.Cells("Approved").Text = "Approve"
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
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

    Private Sub grdItems_ColumnButtonClick_1(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.ColumnButtonClick
        Try
            If e.Column.Key = "Remove" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                InquiryComparisonStatementDAL.Delete(Val(Me.grdItems.GetRow.Cells("InquiryComparisonStatementId").Value.ToString))
                SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Purchase, "", True)
                Me.grdItems.GetRow.Delete()
                Me.grdItems.UpdateData()
            End If
            If e.Column.Key = "Approved" Then
                grdItems_LinkClicked(Nothing, Nothing)
            End If
            If e.Column.Key = "SendBack" Then
                If InquiryComparisonStatementDAL.UpdateVendorQuotationDetailWithComments(Val(Me.grdItems.GetRow.Cells("VendorQuotationDetailId").Value.ToString), 1, Me.grdItems.GetRow.Cells("Comments").Value.ToString) = True Then
                    msg_Information("Current Item is send back to Open Quotation.")
                    'TFS# 1443 : Notification on Approval by Ali Faisal on 16-Nov-2017

                    ' *** New Segment *** 
                    '// Adding notification

                    '// Creating new object of Notification configuration dal
                    '// Dal will be used to get users list for the notification 
                    Dim NDal As New NotificationConfigurationDAL
                    Dim objmod As New VouchersMaster
                    '// Creating new object of Agrius Notification class
                    objmod.Notification = New AgriusNotifications

                    '// Reference document number
                    objmod.Notification.ApplicationReference = Val(Me.grdItems.GetRow.Cells("VendorQuotationDetailId").Value.ToString)

                    '// Date of notification
                    objmod.Notification.NotificationDate = Now

                    '// Preparing notification title string
                    objmod.Notification.NotificationTitle = "Serial number : " & Me.grdItems.GetRow.Cells("SerialNo").Value.ToString & " of Requiremnet Description : " & Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString & " has been sent back for recommended modifications."

                    '// Preparing notification description string
                    objmod.Notification.NotificationDescription = "Serial number : " & Me.grdItems.GetRow.Cells("SerialNo").Value.ToString & " of Requiremnet Description : " & Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString & " has been sent back for recommended modifications."

                    '// Setting source application as refrence in the notification
                    objmod.Notification.SourceApplication = "CS Approval"



                    '// Starting to get users list to add child

                    '// Creating notification detail object list
                    Dim List As New List(Of NotificationDetail)

                    '// Getting users list
                    List = NDal.GetNotificationUsers("CS Approval")

                    '// Adding users list in the Notification object of current inquiry
                    objmod.Notification.NotificationDetils.AddRange(List)

                    '// Getting and adding user groups list in the Notification object of current inquiry
                    objmod.Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("Send Back"))

                    '// Not getting role list because no role is associated at the moment
                    '// We will need this in future and we can use it later
                    '// We can consult to Update function of this class


                    '// ***This segment will be used to save notification in database table

                    '// Creating new list of objects of Agrius Notification
                    Dim NList As New List(Of AgriusNotifications)

                    '// Copying notification object from current sales inquiry to newly defined instance
                    '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
                    NList.Add(objmod.Notification)

                    '// Creating object of Notification DAL
                    Dim GNotification As New NotificationDAL

                    '// Saving notification to database
                    GNotification.AddNotification(NList)

                    '// End Adding Notification

                    '// End Adding Notification
                    ' *** End Segment ***

                    'TFS# 1443 : End
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_LoadingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdItems.LoadingRow
        Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
        Try
            Me.grdItems.UpdateData()
            If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim IsApprovedTrue As Boolean = e.Row.Cells("Approved").Value
                If IsApprovedTrue = True Then
                    'e.Row.Cells("Approved").Column.ButtonText = ""
                    e.Row.Cells("Approved").Text = "Approved"
                    rowStyle.BackColor = Color.GreenYellow
                    e.Row.RowStyle = rowStyle
                Else
                    'e.Row.Cells("Approved").Column.ButtonText = ""
                    e.Row.Cells("Approved").Text = "Approve"
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.LinkClicked
        Try
            If Me.grdItems.RootTable.Columns("Approved").ButtonText = "Approve" Then

                'If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                'InquiryComparisonStatementDAL.Delete(Val(Me.grdItems.GetRow.Cells("InquiryComparisonStatementId").Value.ToString))
                'Me.grdItems.GetRow.Delete()
                'Me.grdItems.UpdateData()
                'GetAllRecords1()
                If InquiryComparisonStatementDAL.IsApprovedAlready(Val(Me.grdItems.GetRow.Cells("PurchaseInquiryDetailId").Value.ToString)) = False AndAlso Me.grdItems.GetRow.Cells("Approved").Text = "Approve" Then
                    FillSingleModel()
                    If IsApproved = False Then
                        msg_Information("Already approved.")
                    Else
                        If InquiryComparisonStatementDAL.Add(ModelList) Then
                            InquiryComparisonStatementDAL.UpdateVendorQuotationDetail(Val(Me.grdItems.GetRow.Cells("VendorQuotationDetailId").Value.ToString), 3)
                            Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
                            rowStyle.BackColor = Color.GreenYellow
                            Me.grdItems.GetRow.RowStyle = rowStyle
                            Me.grdItems.GetRow.Cells("Approved").Text = "Approved"
                            SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Purchase, "", True)
                            msg_Information("Item has been approved successfully.")

                            'TFS# 1443 : Notification on Approval by Ali Faisal on 16-Nov-2017

                            ' *** New Segment *** 
                            '// Adding notification

                            '// Creating new object of Notification configuration dal
                            '// Dal will be used to get users list for the notification 
                            Dim NDal As New NotificationConfigurationDAL
                            Dim objmod As New VouchersMaster
                            '// Creating new object of Agrius Notification class
                            objmod.Notification = New AgriusNotifications

                            '// Reference document number
                            objmod.Notification.ApplicationReference = Val(Me.grdItems.GetRow.Cells("VendorQuotationDetailId").Value.ToString)

                            '// Date of notification
                            objmod.Notification.NotificationDate = Now

                            '// Preparing notification title string
                            objmod.Notification.NotificationTitle = "Serial number : " & Me.grdItems.GetRow.Cells("SerialNo").Value.ToString & " of Requiremnet Description : " & Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString & " has been Approved for Offer."

                            '// Preparing notification description string
                            objmod.Notification.NotificationDescription = "Serial number : " & Me.grdItems.GetRow.Cells("SerialNo").Value.ToString & " of Requiremnet Description : " & Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString & " has been Approved for Offer."

                            '// Setting source application as refrence in the notification
                            objmod.Notification.SourceApplication = "CS Approval"



                            '// Starting to get users list to add child

                            '// Creating notification detail object list
                            Dim List As New List(Of NotificationDetail)

                            '// Getting users list
                            List = NDal.GetNotificationUsers("CS Approval")

                            '// Adding users list in the Notification object of current inquiry
                            objmod.Notification.NotificationDetils.AddRange(List)

                            '// Getting and adding user groups list in the Notification object of current inquiry
                            objmod.Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("Approval"))

                            '// Not getting role list because no role is associated at the moment
                            '// We will need this in future and we can use it later
                            '// We can consult to Update function of this class


                            '// ***This segment will be used to save notification in database table

                            '// Creating new list of objects of Agrius Notification
                            Dim NList As New List(Of AgriusNotifications)

                            '// Copying notification object from current sales inquiry to newly defined instance
                            '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
                            NList.Add(objmod.Notification)

                            '// Creating object of Notification DAL
                            Dim GNotification As New NotificationDAL

                            '// Saving notification to database
                            GNotification.AddNotification(NList)

                            '// End Adding Notification

                            '// End Adding Notification
                            ' *** End Segment ***

                            'TFS# 1443 : End

                            'DisplayDetail()
                            GetAllRecords1()
                        End If
                    End If
                Else
                    msg_Information("Following item has already been approved.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Function GetQuotationDocumentNo() As String
        Try
            Dim objCompany As Object = Me.cmbCompany.SelectedItem
            Dim DocPrefix As String = GetPrefix("frmQoutationNew")
            'If DocPrefix.Length > 0 Then
            '    If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
            '        Return GetSerialNo(DocPrefix + "" & Me.cmbCompany.SelectedValue & "" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "QuotationMasterTable", "QuotationNo")
            '    ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
            '        Return GetNextDocNo(DocPrefix & Me.cmbCompany.SelectedValue & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "QuotationMasterTable", "QuotationNo")
            '    Else
            '        Return GetNextDocNo(DocPrefix + "" & Me.cmbCompany.SelectedValue & "", 6, "QuotationMasterTable", "QuotationNo")
            '    End If
            'Else
            '    If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
            '        Return GetSerialNo("QUT" + "" & Me.cmbCompany.SelectedValue & "" + "-" + Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year, 2) + "-", "QuotationMasterTable", "QuotationNo")
            '    ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
            '        Return GetNextDocNo("QUT" & Me.cmbCompany.SelectedValue & "-" & Format(Me.dtpPODate.Value, "yy") & Me.dtpPODate.Value.Month.ToString("00"), 4, "QuotationMasterTable", "QuotationNo")
            '    Else
            '        Return GetNextDocNo("QUT" + "" & Me.cmbCompany.SelectedValue & "", 6, "QuotationMasterTable", "QuotationNo")
            '    End If
            'End If
            If DocPrefix.Length > 0 Then
                If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                    Return GetSerialNo(DocPrefix + "" & Val(objCompany(0).ToString) & "" + "-" + Microsoft.VisualBasic.Right(DateTime.Now.Year, 2) + "-", "QuotationMasterTable", "QuotationNo")
                ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                    Return GetNextDocNo(DocPrefix & Val(objCompany(0).ToString) & "-" & Format(DateTime.Now, "yy") & DateTime.Now.Month.ToString("00"), 4, "QuotationMasterTable", "QuotationNo")
                Else
                    Return GetNextDocNo(DocPrefix + "" & Val(objCompany(0).ToString) & "", 6, "QuotationMasterTable", "QuotationNo")
                End If
            Else
                If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                    Return GetSerialNo("QUT" + "" & Val(objCompany(0).ToString) & "" + "-" + Microsoft.VisualBasic.Right(DateTime.Now.Year, 2) + "-", "QuotationMasterTable", "QuotationNo")
                ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                    Return GetNextDocNo("QUT" & Val(objCompany(0).ToString) & "-" & Format(DateTime.Now, "yy") & DateTime.Now.Month.ToString("00"), 4, "QuotationMasterTable", "QuotationNo")
                Else
                    Return GetNextDocNo("QUT" + "" & Val(objCompany(0).ToString) & "", 6, "QuotationMasterTable", "QuotationNo")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub grdSaved_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSaved.FormattingRow
        Try
            If e.Row.RowType = Janus.Windows.GridEX.RowType.Record Then
                'e.Row.Cells("Approved").Text = "Yes"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            If Not Me.grdSaved.RecordCount > 0 Then Exit Sub
            If Me.grdItems.RecordCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Me.grdItems.DataSource = InquiryComparisonStatementDAL.EditRecord(Val(Me.grdSaved.GetRow.Cells("InquiryComparisonStatementId").Value.ToString))
            'Me.grdItems.DataSource = InquiryComparisonStatementDAL.GetEmailWithAlternate(Val(Me.grdSaved.GetRow.Cells("PurchaseInquiryDetailId").Value.ToString))
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillSalesQuotationModel()
        Try
            'VendorQuotationDetailId()
            'VendorQuotationId()
            'VendorId()
            'VendorCode()
            'Vendor()
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
            'BaseCurrencyId()
            'BaseCurrencyRate()
            'CurrencyId()
            'Currency()
            'CurrencyRate()
            'CurrencySymbol()
            'Price()
            'DiscountPer()
            'DiscountAmount()
            'SalesTaxPer()
            'SalesTaxAmount()
            'AddTaxPer()
            'AddTaxAmount()
            'IncTaxPer()
            'IncTaxAmount()
            'CDPer()
            'CDAmount()
            'NetPrice()
            'Amount()
            'OtherCharges()
            'NetCostValue()
            'Margin
            'QuotedCostValue()
            'PurchaseInquiryId()
            'PurchaseInquiryDetailId()
            'ReferenceNo()
            'Comments()
            'HeadArticleId()
            'Approved()
            'InquiryComparisonStatementId()

            SalesQuotationModel = New SalesQuotationMaster()
            SalesQuotationModel.QuotationNo = GetQuotationDocumentNo()
            SalesQuotationModel.UserName = LoginUserName
            SalesQuotationModel.QuotationDate = Now
            Dim objCompany As Object = Me.cmbCompany.SelectedItem
            SalesQuotationModel.LocationId = objCompany(0)

            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows

                Dim Detail As SalesQuotationDetail = New SalesQuotationDetail()
                Dim dt As DataTable = InquiryComparisonStatementDAL.EditRecord(Val(Row.Cells("InquiryComparisonStatementId").Value.ToString))
                If Val(Row.Cells("CustomerId").Value.ToString) > 0 Then
                    SalesQuotationModel.VendorId = Val(Row.Cells("CustomerId").Value.ToString)
                    Detail.QuotationDetailId = 0
                    Detail.QuotationId = 0
                    Dim objCategory As Object = Me.cmbCategory.SelectedItem
                    Detail.LocationId = objCategory(0)
                    Detail.ArticleDefId = Val(dt.Rows(0).Item("ArticleId").ToString)
                    Detail.ArticleSize = ""
                    Detail.Sz1 = Val(dt.Rows(0).Item("Qty").ToString)
                    Detail.Sz2 = 0
                    Detail.Sz3 = 0
                    Detail.Sz4 = 0
                    Detail.Sz5 = 0
                    Detail.Sz6 = 0
                    Detail.Sz7 = 0
                    Detail.Qty = Val(dt.Rows(0).Item("Qty").ToString)
                    Detail.Price = Val(dt.Rows(0).Item("NetPrice").ToString)
                    Detail.CurrentPrice = Val(dt.Rows(0).Item("NetPrice").ToString)
                    Detail.DeliveredQty = 0
                    Detail.SalesTax_Percentage = 0
                    Detail.SchemeQty = 0
                    Detail.Discount_Percentage = 0
                    Detail.DeliveredSchemeQty = 0
                    Detail.PurchasePrice = 0
                    Detail.PackPrice = 0
                    Detail.Pack_Desc = ""
                    Detail.Comments = ""
                    Detail.ItemDescription = ""
                    Detail.BrandName = ""
                    Detail.Specification = ""
                    Detail.RegistrationNo = ""
                    Detail.TradePrice = 0
                    Detail.Pack_40Kg_Weight = 0
                    Detail.TenderSrNo = ""
                    Detail.CostPrice = 0
                    Detail.SED_Tax_Percent = 0
                    Detail.SED_Tax_Amount = 0
                    Detail.SOQuantity = 0
                    Detail.DeliveredTotalQty = 0
                    Detail.Discount_Percentage = 0
                    Detail.Item_Info = True
                    Detail.DeliveredTotalQty = 0
                    Detail.BaseCurrencyId = Val(dt.Rows(0).Item("BaseCurrencyId").ToString)
                    Detail.BaseCurrencyRate = Val(dt.Rows(0).Item("BaseCurrencyRate").ToString)
                    Detail.CurrencyId = Val(dt.Rows(0).Item("CurrencyId").ToString)
                    Detail.CurrencyRate = Val(dt.Rows(0).Item("CurrencyRate").ToString)
                    Detail.CurrencyAmount = Val(dt.Rows(0).Item("CurrencyRate").ToString) * Val(dt.Rows(0).Item("Qty").ToString)
                    Detail.RequirementDescription = dt.Rows(0).Item("RequirementDescription").ToString
                    Detail.PurchaseInquiryDetailId = Val(dt.Rows(0).Item("PurchaseInquiryDetailId").ToString)
                    Detail.VendorQuotationDetailId = Val(dt.Rows(0).Item("VendorQuotationDetailId").ToString)
                    Detail.HeadArticleId = Val(dt.Rows(0).Item("HeadArticleId").ToString)
                    SalesQuotationModel.DetailList.Add(Detail)
                Else
                    msg_Error("Customer is required to create a Quotation")
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SaveQuotation()
        Dim CustomerId As Integer = 0
        Dim VendorQuotationId As Integer = 0
        Dim Customers As New List(Of Integer)
        Dim IsMasterFilled As Boolean = False
        Try
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            '    Customers.Add(Val(Row.Cells("CustomerId").Value.ToString))
            '    'Customers.Exists()
            'Next
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                Dim PurchaseInquiryDetailId As Integer = 0
                Dim ForHeadArticleId As Integer = 0

                If Val(Row.Cells("InquiryComparisonStatementId").Value.ToString) > 0 Then
                    PurchaseInquiryDetailId = Val(Row.Cells("InquiryComparisonStatementId").Value.ToString)
                    ForHeadArticleId = Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString)
                End If
                If InquiryComparisonStatementDAL.IsPurchaseInquiryExists(PurchaseInquiryDetailId) = False Then
                    'If Val(Row.Cells("CustomerId").Value.ToString) > 0 Then
                    'If CustomerId > 0 Then
                    'If CustomerId <> Val(Row.Cells("CustomerId").Value.ToString) Then
                    If IsMasterFilled = False Then
                        SalesQuotationModel = New SalesQuotationMaster()
                        SalesQuotationModel.QuotationNo = GetQuotationDocumentNo()
                        QuotationNo = SalesQuotationModel.QuotationNo
                        SalesQuotationModel.UserName = LoginUserName
                        SalesQuotationModel.QuotationDate = Now
                        Dim objCompany As Object = Me.cmbCompany.SelectedItem
                        SalesQuotationModel.LocationId = objCompany(0)
                        CustomerId = Val(Row.Cells("CustomerId").Value.ToString)

                        VendorQuotationId = Val(Row.Cells("VendorQuotationId").Value.ToString)
                        SalesQuotationModel.VendorId = CustomerId
                        'CustomerId = Val(Row.Cells("VendorId").Value.ToString)

                        SalesQuotationModel.Status = "Open"
                        If Me.cmbTermAndCondition.Text = ".... Select Any Value ...." Then
                            SalesQuotationModel.Terms_And_Condition = ""
                        Else
                            SalesQuotationModel.Terms_And_Condition = Me.cmbTermAndCondition.Text
                        End If
                        Dim objTerms As Object = Me.cmbTermAndCondition.SelectedItem
                        SalesQuotationModel.TermsAndConditionId = objTerms(0)
                        SalesQuotationModel.SalesInquiryId = Val(Row.Cells("SalesInquiryId").Value.ToString)
                        IsMasterFilled = True
                    End If
                    'End If
                    Dim dt As DataTable
                    If Row.Cells("Alternate").Value.ToString = "False" Then
                        dt = InquiryComparisonStatementDAL.GetApprovedOnes(PurchaseInquiryDetailId, ForHeadArticleId)
                    Else
                        dt = InquiryComparisonStatementDAL.GetApprovedOnesAlternate(PurchaseInquiryDetailId)
                    End If
                    For Each drRow As DataRow In dt.Rows
                        Dim Detail As SalesQuotationDetail = New SalesQuotationDetail()
                        Detail.QuotationDetailId = 0
                        Detail.QuotationId = 0
                        Dim objCategory As Object = Me.cmbCategory.SelectedItem
                        If objCategory Is Nothing Then
                            Detail.LocationId = 0
                        Else
                            Detail.LocationId = objCategory(0)
                        End If
                        Detail.ArticleDefId = Val(drRow("ArticleId").ToString)
                        Detail.ArticleSize = ""
                        Detail.Sz1 = Val(drRow("Qty").ToString)
                        Detail.Sz2 = 0
                        Detail.Sz3 = 0
                        Detail.Sz4 = 0
                        Detail.Sz5 = 0
                        Detail.Sz6 = 0
                        Detail.Sz7 = 0
                        Detail.Qty = Val(drRow("Qty").ToString)
                        'Detail.Price = Val(drRow("NetPrice").ToString)
                        If TaxExcludeFromRateOnQuotationFromCS = False Then
                            Detail.Price = Val(drRow("QuotedCostValue").ToString) / Val(drRow("Qty").ToString)
                            Detail.PostDiscountPrice = Val(drRow("QuotedCostValue").ToString) / Val(drRow("Qty").ToString) '' OTC Task : Ayesha Rehman
                            Detail.SalesTax_Percentage = 0
                            Detail.Margin = 0
                        Else
                            ''TASK TFS4667
                            Detail.Price = Val(drRow("Price").ToString) + ((Val(drRow("Price").ToString) * Val(drRow("Margin").ToString)) / 100)
                            Detail.PostDiscountPrice = Val(drRow("Price").ToString) + ((Val(drRow("Price").ToString) * Val(drRow("Margin").ToString)) / 100)
                            Detail.SalesTax_Percentage = Val(drRow("SalesTaxPer").ToString)
                            Detail.Margin = Val(drRow("Margin").ToString)
                            ''END TASK TFS4667
                        End If
                        Detail.CurrentPrice = Val(drRow("NetPrice").ToString)
                        Detail.DeliveredQty = 0
                        Detail.SchemeQty = 0
                        Detail.Discount_Percentage = 0
                        Detail.DeliveredSchemeQty = 0
                        Detail.PurchasePrice = 0
                        Detail.PackPrice = 0
                        Detail.Pack_Desc = ""
                        Detail.Comments = drRow("Comments").ToString
                        Detail.ItemDescription = ""
                        Detail.BrandName = ""
                        Detail.Specification = ""
                        Detail.RegistrationNo = ""
                        Detail.TradePrice = 0
                        Detail.Pack_40Kg_Weight = 0
                        Detail.TenderSrNo = ""
                        Detail.CostPrice = 0
                        Detail.SED_Tax_Percent = 0
                        Detail.SED_Tax_Amount = 0
                        Detail.SOQuantity = 0
                        Detail.DeliveredTotalQty = 0
                        Detail.Discount_Percentage = 0
                        Detail.Item_Info = True
                        Detail.DeliveredTotalQty = 0
                        Detail.BaseCurrencyId = Val(drRow("BaseCurrencyId").ToString)
                        Detail.BaseCurrencyRate = Val(drRow("BaseCurrencyRate").ToString)
                        Detail.CurrencyId = Val(drRow("CurrencyId").ToString)
                        If Val(drRow("CurrencyId").ToString) = Val(1) Then
                            Detail.CurrencyRate = 1
                        Else
                            Detail.CurrencyRate = Val(drRow("CurrencyRate").ToString)
                        End If
                        Detail.CurrencyAmount = Val(drRow("CurrencyRate").ToString) * Val(drRow("Qty").ToString)
                        Detail.RequirementDescription = drRow("RequirementDescription").ToString
                        Detail.PurchaseInquiryDetailId = Val(drRow("PurchaseInquiryDetailId").ToString)
                        Detail.VendorQuotationDetailId = Val(drRow("VendorQuotationDetailId").ToString)
                        Detail.HeadArticleId = Val(drRow("HeadArticleId").ToString)
                        Detail.SerialNo = drRow("SerialNo").ToString
                        Detail.PurchaseInquiryId = Val(drRow("PurchaseInquiryId").ToString)
                        Detail.Alternate = drRow("Alternate").ToString
                        SalesQuotationModel.DetailList.Add(Detail)
                    Next
                    'Else
                    '    msg_Error("Customer is required to create a Quotation")
                    '    Exit Sub
                    'End If

                    'If InquiryComparisonStatementDAL.AddQuotation(SalesQuotationModel, Val(Row.Cells("VendorQuotationId").Value.ToString)) Then
                    '    SaveActivityLog("POS", "Quotation", EnumActions.Save, LoginUserId, EnumRecordType.Sales, SalesQuotationModel.QuotationNo.Trim, True)
                    '    msg_Information("Quotation has been generated.")
                    'End If

                Else
                    msg_Information("Quotation has already been generated.")
                End If
            Next
            If Not SalesQuotationModel Is Nothing Then
                If InquiryComparisonStatementDAL.AddQuotation(SalesQuotationModel, VendorQuotationId) Then
                    SaveActivityLog("POS", "Quotation", EnumActions.Save, LoginUserId, EnumRecordType.Sales, SalesQuotationModel.QuotationNo.Trim, True)
                    msg_Information("Quotation has been generated.")
                    SalesQuotationModel = Nothing
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateQuotation()
        Dim VendorQuotationId As Integer = 0
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        SalesQuotationModel = New SalesQuotationMaster()
        Try
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                Dim PurchaseInquiryDetailId As Integer = 0
                VendorQuotationId = Val(frmSalesQuotationDailog.QuotationId)
                ''Commented By Ayesha Rehman : TFSOTC Issue : 05-07-2018
                'If Val(Row.Cells("InquiryComparisonStatementId").Value.ToString) > 0 Then
                '    PurchaseInquiryDetailId = Val(Row.Cells("InquiryComparisonStatementId").Value.ToString)
                'Else
                '    PurchaseInquiryDetailId = Val(Row.Cells("HeadArticleId").Value.ToString)
                'End If
                Dim ForHeadArticleId As Integer = 0

                If Val(Row.Cells("InquiryComparisonStatementId").Value.ToString) > 0 Then
                    PurchaseInquiryDetailId = Val(Row.Cells("InquiryComparisonStatementId").Value.ToString)
                    ForHeadArticleId = Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString)
                End If
                If InquiryComparisonStatementDAL.IsPurchaseInquiryExists(PurchaseInquiryDetailId) = False Then
                    Dim dt As DataTable
                    If Row.Cells("Alternate").Value.ToString = "False" Then
                        dt = InquiryComparisonStatementDAL.GetApprovedOnes(PurchaseInquiryDetailId, ForHeadArticleId)
                    Else
                        dt = InquiryComparisonStatementDAL.GetApprovedOnesAlternate(PurchaseInquiryDetailId)
                    End If
                    For Each drRow As DataRow In dt.Rows
                        Dim Detail As SalesQuotationDetail = New SalesQuotationDetail()
                        Detail.QuotationDetailId = 0
                        Detail.QuotationId = Val(frmSalesQuotationDailog.QuotationId)
                        Dim objCategory As Object = Me.cmbCategory.SelectedItem
                        If objCategory Is Nothing Then
                            Detail.LocationId = 0
                        Else
                            Detail.LocationId = objCategory(0)
                        End If
                        Detail.ArticleDefId = Val(drRow("ArticleId").ToString)
                        Detail.ArticleSize = ""
                        Detail.Sz1 = Val(drRow("Qty").ToString)
                        Detail.Sz2 = 0
                        Detail.Sz3 = 0
                        Detail.Sz4 = 0
                        Detail.Sz5 = 0
                        Detail.Sz6 = 0
                        Detail.Sz7 = 0
                        Detail.Qty = Val(drRow("Qty").ToString)
                        If TaxExcludeFromRateOnQuotationFromCS = False Then
                            Detail.Price = Val(drRow("QuotedCostValue").ToString) / Val(drRow("Qty").ToString)
                            Detail.PostDiscountPrice = Val(drRow("QuotedCostValue").ToString) / Val(drRow("Qty").ToString) '' OTC Task : Ayesha Rehman
                            Detail.SalesTax_Percentage = 0
                            Detail.Margin = 0
                        Else
                            ''TASK TFS4667
                            Detail.Price = Val(drRow("Price").ToString) + Val(drRow("Margin").ToString)
                            Detail.PostDiscountPrice = Val(drRow("Price").ToString) + Val(drRow("Margin").ToString)
                            Detail.SalesTax_Percentage = Val(drRow("SalesTaxPer").ToString)
                            Detail.Margin = Val(drRow("Margin").ToString)
                            ''END TASK TFS4667
                        End If
                        'Detail.Price = Val(drRow("QuotedCostValue").ToString) / Val(drRow("Qty").ToString)
                        'Detail.PostDiscountPrice = Val(drRow("QuotedCostValue").ToString) / Val(drRow("Qty").ToString) '' OTC Task : Ayesha Rehman
                        Detail.CurrentPrice = Val(drRow("NetPrice").ToString)
                        Detail.DeliveredQty = 0
                        Detail.SchemeQty = 0
                        Detail.Discount_Percentage = 0
                        Detail.DeliveredSchemeQty = 0
                        Detail.PurchasePrice = 0
                        Detail.PackPrice = 0
                        Detail.Pack_Desc = ""
                        Detail.Comments = drRow("Comments").ToString
                        Detail.ItemDescription = ""
                        Detail.BrandName = ""
                        Detail.Specification = ""
                        Detail.RegistrationNo = ""
                        Detail.TradePrice = 0
                        Detail.Pack_40Kg_Weight = 0
                        Detail.TenderSrNo = ""
                        Detail.CostPrice = 0
                        Detail.SED_Tax_Percent = 0
                        Detail.SED_Tax_Amount = 0
                        Detail.SOQuantity = 0
                        Detail.DeliveredTotalQty = 0
                        Detail.Discount_Percentage = 0
                        Detail.Item_Info = True
                        Detail.DeliveredTotalQty = 0
                        Detail.BaseCurrencyId = Val(drRow("BaseCurrencyId").ToString)
                        Detail.BaseCurrencyRate = Val(drRow("BaseCurrencyRate").ToString)
                        Detail.CurrencyId = Val(drRow("CurrencyId").ToString)
                        If Val(drRow("CurrencyId").ToString) = Val(1) Then
                            Detail.CurrencyRate = 1
                        Else
                            Detail.CurrencyRate = Val(drRow("CurrencyRate").ToString)
                        End If
                        Detail.CurrencyAmount = Val(drRow("CurrencyRate").ToString) * Val(drRow("Qty").ToString)
                        Detail.RequirementDescription = drRow("RequirementDescription").ToString
                        Detail.PurchaseInquiryDetailId = Val(drRow("PurchaseInquiryDetailId").ToString)
                        Detail.VendorQuotationDetailId = Val(drRow("VendorQuotationDetailId").ToString)
                        Detail.HeadArticleId = Val(drRow("HeadArticleId").ToString)
                        Detail.SerialNo = drRow("SerialNo").ToString
                        Detail.PurchaseInquiryId = Val(drRow("PurchaseInquiryId").ToString)
                        Detail.Alternate = drRow("Alternate").ToString
                        SalesQuotationModel.DetailList.Add(Detail)
                    Next
                End If
            Next
            If Not SalesQuotationModel Is Nothing Then
                If InquiryComparisonStatementDAL.AddQuotationDetailExisting(SalesQuotationModel.DetailList, VendorQuotationId, trans, getConfigValueByType("EnableDuplicateQuotation").ToString.ToUpper) Then
                    msg_Information("Quotation has been generated.")
                    SalesQuotationModel = Nothing
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
  
    Private Sub btnCreateSalesQuotation_Click(sender As Object, e As EventArgs) Handles btnCreateSalesQuotation.Click
        Try
            If IsValidateQuotation() Then
                If flgAddingPO = False Then
                    If Not msg_Confirm(str_ConfirmCreateVendorQuotation) = True Then Exit Sub
                    SaveQuotation()
                    If AutoEmail = True Then
                        SendAutoEmail("Save")
                    End If
                Else
                    If Not msg_Confirm("Do you want to create Purchase Order against selected items?") = True Then Exit Sub
                    SavePurchaseOrder()
                    ''TASK TFS4437
                    If AutoEmail = True Then
                        SendAutoEmail()
                    End If
                    ''END TASK TFS4437
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete1_Click(sender As Object, e As EventArgs) Handles btnDelete1.Click
        Try
            If Me.grdSaved.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                InquiryComparisonStatementDAL.Delete(Val(Me.grdSaved.GetRow.Cells("InquiryComparisonStatementId").Value.ToString))
                InquiryComparisonStatementDAL.DeleteChild(Val(Me.grdSaved.GetRow.Cells("PurchaseInquiryDetailId").Value.ToString))
                InquiryComparisonStatementDAL.DeleteCharges(Val(Me.grdSaved.GetRow.Cells("VendorQuotationDetailId").Value.ToString))
                InquiryComparisonStatementDAL.UpdateVendorQuotationDetail(Val(Me.grdSaved.GetRow.Cells("VendorQuotationDetailId").Value.ToString), 2)
                Me.grdSaved.GetRow.Delete()
                Me.grdSaved.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnEmail_Click(sender As Object, e As EventArgs) Handles btnEmail.Click
        Try
            GetTemplate(lblHeader.Text)
            If EmailTemplate.Length > 0 Then
                GetEmailData()
                GetVendorsEmails()
                FormatStringBuilder(dtEmail)
                CreateOutLookMail()
            Else
                msg_Error("No email template is found for comparison statement.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetTemplate(ByVal Title As String)
        Dim Fields As String = String.Empty

        Try
            dtEmail = New DataTable
            'GetEmailData()
            EmailTemplate = EmailDAL.GetTemplate(Title)
            If EmailTemplate.Length > 0 Then
                Dim i, j As Integer
                i = EmailTemplate.IndexOf("<Fields>") + "<Fields>".Length
                j = EmailTemplate.IndexOf("</Fields>") - i

                Dim Searched As String = "</Fields>"
                AfterFieldsElement = EmailTemplate.Substring(EmailTemplate.IndexOf(Searched) + Searched.Length)
                Fields = EmailTemplate.Substring(i, j)
                Dim WOAtTheRate As String = Fields.Replace("@", "")
                Dim WOSpace As String = WOAtTheRate.Replace(" ", "")
                Dim IndexOfFieldElement As Integer = EmailTemplate.IndexOf("<Fields>")
                If IndexOfFieldElement > 0 Then
                    EmailTemplate = EmailTemplate.Remove(IndexOfFieldElement)
                End If
                'EmailTemplate = EmailTemplate.Remove(i, j)
                AllFields = New List(Of String)

                dtEmail.Columns.Clear()
                For Each word As String In WOSpace.Split(",")
                    Dim TrimSpace As String = word.Trim()
                    If Me.grdItems.RootTable.Columns.Contains(TrimSpace) Then
                        dtEmail.Columns.Add(TrimSpace)
                        AllFields.Add(TrimSpace)
                        'Else
                        '    msg_Error("'" & TrimSpace & "'column does not exist")
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetTemplateForQuotation(ByVal Title As String)
        Dim Fields As String = String.Empty
        Try
            dtEmail = New DataTable
            'GetEmailData()
            EmailTemplate = EmailDAL.GetTemplate(Title)
            If EmailTemplate.Length > 0 Then
                Dim i, j As Integer
                i = EmailTemplate.IndexOf("<Fields>") + "<Fields>".Length
                j = EmailTemplate.IndexOf("</Fields>") - i

                Dim Searched As String = "</Fields>"
                AfterFieldsElement = EmailTemplate.Substring(EmailTemplate.IndexOf(Searched) + Searched.Length)
                Fields = EmailTemplate.Substring(i, j)
                Dim WOAtTheRate As String = Fields.Replace("@", "")
                Dim WOSpace As String = WOAtTheRate.Replace(" ", "")
                Dim IndexOfFieldElement As Integer = EmailTemplate.IndexOf("<Fields>")
                If IndexOfFieldElement > 0 Then
                    EmailTemplate = EmailTemplate.Remove(IndexOfFieldElement)
                End If
                'EmailTemplate = EmailTemplate.Remove(i, j)
                AllFields = New List(Of String)
                Dim tempdtEmail As DataTable
                tempdtEmail = dtEmail.Clone()
                dtEmail.Columns.Clear()

                For Each word As String In WOSpace.Split(",")
                    Dim TrimSpace As String = word.Trim()
                    'If tempdtEmail.Columns.Contains(TrimSpace) Then
                    'dtEmail.Columns.Add(TrimSpace)
                    'AllFields.Add(TrimSpace)

                    If dtEmail.Columns.Contains(TrimSpace) = False Then
                        dtEmail.Columns.Add(TrimSpace)
                    End If
                    If AllFields.Contains(TrimSpace) = False Then
                        AllFields.Add(TrimSpace)
                    End If
                    'Else
                    '    msg_Error("'" & TrimSpace & "'column does not exist")
                    'End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetEmailData()
        'dtEmail = New DataTable
        Dim Dr As DataRow
        Try
            'For Each Row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
            '    Dr = dtEmail.NewRow
            '    For Each col As String In AllFields
            '        If Row.Table.Columns.Contains(col) Then
            '            Dr.Item(col) = Row.Cells(col).Value.ToString
            '        End If
            '    Next
            '    dtEmail.Rows.Add(Dr)
            'Next
            For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetCheckedRows
                'Dim dt As DataTable = InquiryComparisonStatementDAL.EditRecord(Val(Row.Cells("InquiryComparisonStatementId").Value.ToString))
                Dim PurchaseInquiryDetailId As Integer = 0
                If Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString) > 0 Then
                    PurchaseInquiryDetailId = Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString)
                Else
                    PurchaseInquiryDetailId = Val(Row.Cells("HeadArticleId").Value.ToString)
                End If
                If InquiryComparisonStatementDAL.IsPurchaseInquiryExists(PurchaseInquiryDetailId) = True Then
                    Dim dt As DataTable = InquiryComparisonStatementDAL.EmailSalesQuotation(PurchaseInquiryDetailId)

                    For Each Row1 As DataRow In dt.Rows
                        Dr = dtEmail.NewRow
                        For Each col As String In AllFields
                            If Row1.Table.Columns.Contains(col) Then
                                Dr.Item(col) = Row1.Item(col).ToString
                            End If
                        Next
                        dtEmail.Rows.Add(Dr)
                    Next
                Else
                    msg_Information("No Sales Quotation exists against selected row.")
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetAutoEmailData()
        'dtEmail = New DataTable
        Dim Dr As DataRow
        Try
            'For Each Row As Janus.Windows.GridEX.GridEXRow In grdItems.GetCheckedRows
            '    Dr = dtEmail.NewRow
            '    For Each col As String In AllFields
            '        If Row.Table.Columns.Contains(col) Then
            '            Dr.Item(col) = Row.Cells(col).Value.ToString
            '        End If
            '    Next
            '    dtEmail.Rows.Add(Dr)
            'Next
            'For Each Row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetCheckedRows
            '    'Dim dt As DataTable = InquiryComparisonStatementDAL.EditRecord(Val(Row.Cells("InquiryComparisonStatementId").Value.ToString))
            '    Dim PurchaseInquiryDetailId As Integer = 0
            '    If Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString) > 0 Then
            '        PurchaseInquiryDetailId = Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString)
            '    Else
            '        PurchaseInquiryDetailId = Val(Row.Cells("HeadArticleId").Value.ToString)
            '    End If
            '    If InquiryComparisonStatementDAL.IsPurchaseInquiryExists(PurchaseInquiryDetailId) = True Then
            Dim dt As DataTable = InquiryComparisonStatementDAL.GetSalesQuotation(QuotationNo, flgExcludeTaxPrice)
            For Each Row1 As DataRow In dt.Rows
                Dr = dtEmail.NewRow
                For Each col As String In AllFields
                    If Row1.Table.Columns.Contains(col) Then
                        Dr.Item(col) = Row1.Item(col).ToString
                    End If
                Next
                dtEmail.Rows.Add(Dr)
            Next
            '    Else
            'msg_Information("No Sales Quotation exists against selected row.")
            '    End If
            'Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetVendorsEmails()
        Try
            Me.grdSaved.UpdateData()
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                If VendorEmails.Length > 0 Then
                    VendorEmails += "; " & Row.Cells("Email").Value.ToString & ""
                Else
                    VendorEmails = "" & Row.Cells("Email").Value.ToString & ""
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
 
    Private Sub FormatStringBuilder(ByVal dt As DataTable)
        Try
            html = New StringBuilder
            'Dim dt As DataTable = Me.GetData()

            'Building an HTML string.
            'Dim html As New StringBuilder()

            'Table start.
            'html.Append()
            html.Append(EmailTemplate)
            html.Append("<table border = '1'>")

            'Building the Header row.
            html.Append("<tr bgcolor='#58ACFA'>")
            For Each column As DataColumn In dt.Columns
                Dim ColumnName As String = ""
                Dim Pattern = "([a-z?])[_ ]?([A-Z])"
                If column.ColumnName = "SerialNo" Then
                    ColumnName = "Sr#"
                Else
                    ColumnName = Regex.Replace(column.ColumnName, Pattern, "$1 $2")
                End If
                html.Append("<th>")
                html.Append(ColumnName)
                html.Append("</th>")
            Next
            html.Append("</tr>")

            'Building the Data rows.
            For Each row As DataRow In dt.Rows
                If row.Table.Columns.Contains("Alternate") Then
                    If row.Item("Alternate") = "Yes" Then
                        html.Append("<tr bgcolor='#A9F5BC'>")
                        For Each column As DataColumn In dt.Columns
                            html.Append("<td>")
                            html.Append(row(column.ColumnName))
                            html.Append("</td>")
                        Next
                        html.Append("</tr>")
                    Else
                        html.Append("<tr>")
                        For Each column As DataColumn In dt.Columns
                            html.Append("<td>")
                            html.Append(row(column.ColumnName))
                            html.Append("</td>")
                        Next
                        html.Append("</tr>")
                    End If
                Else
                    html.Append("<tr>")
                    For Each column As DataColumn In dt.Columns
                        html.Append("<td>")
                        html.Append(row(column.ColumnName))
                        html.Append("</td>")
                    Next
                    html.Append("</tr>")
                End If
            Next
            'Table end.
            html.Append("</table>")
            html.Append(AfterFieldsElement)
            'html.Append("</body>")
            'html.Append("</html>")

            '     'Append the HTML string to Placeholder.
            'PlaceHolder1.Controls.Add(New Literal() With { _
            '   .Text = html.ToString() _


            'sb.Append("<table border=1 cellspacing=1 cellpadding=1><thead>")
            'For colIndx As Integer = 0 To colIndx < dt.Columns.Count
            '    sb.Append("<th>")
            '    sb.Append(dt.Columns(colIndx).ColumnName)
            '    sb.Append("</th>")
            'Next
            'sb.Append("</thead>")
            ''//add data rows to html table
            'For rowIndx As Integer = 0 To rowIndx < dt.Rows.Count Step 1
            '    sb.Append("<tr>")
            '    For colIndx As Integer = 0 To colIndx < dt.Columns.Count Step 1
            '        sb.Append("<td>")
            '        dt.Rows(rowIndx)(colIndx).ToString()
            '        sb.Append("</td>")
            '    Next
            '    sb.Append("</tr>")
            'Next
            'sb.Append("</table>")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail()
        Try

            Dim oApp As Outlook.Application = New Outlook.Application

            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = QuotationNo
            mailItem.To = VendorEmails
            'VendorEmails = String.Empty
            'mailItem.CC = txtCC.Text
            'Me.txtCC.Text = ""
            'mailItem.BCC = txtBCC.Text
            'Me.txtBCC.Text = ""
            'mailItem.Body = html.ToString
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            If AutoEmail = False Then
                mailItem.Display(mailItem)
                mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
                EmailBody = html.ToString
            Else
                mailItem.HTMLBody = html.ToString + mailItem.HTMLBody
                EmailBody = html.ToString
                mailItem.Send()
            End If
            mailItem = Nothing
            oApp = Nothing

            '     mailItem.Display(mailItem);
            'mailItem.HTMLBody = body + mailItem.HTMLBody;
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                'Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    'Me.BtnPrint.Enabled = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmInquiryComparisonStatement)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If


            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                'Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False
                'CtrlGrdBar1.mGridChooseFielder.Enabled = False

                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        'Me.BtnPrint.Enabled = True
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

    Private Sub grdSaved_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = "frmVendorQuotation"
                frm._VoucherId = Val(Me.grdSaved.GetRow.Cells("VendorQuotationId").Value.ToString)
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnQuotation_Click(sender As Object, e As EventArgs) Handles btnQuotation.Click
        Try
            frmQoutationNew.StartPosition = FormStartPosition.CenterScreen
            frmQoutationNew.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged

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
    Public Sub ShowDetailRecord()
        Try
            'SerialNo = Me.grdItems.GetRow.Cells("SerialNo").Value.ToString
            'CurrentRowIndex = Me.grdItems.GetRow.RowIndex
            ''VendorQuotationId = Val(Me.grdItems.GetRow.Cells("VendorQuotationId").Value.ToString)
            VendorQuotationDetailId = Val(Me.grdItems.GetRow.Cells("VendorQuotationDetailId").Value.ToString)
            'If VendorQuotationDetailId > 0 Then
            '    Me.btnSave1.Text = "&Update"
            'Else
            '    Me.btnSave1.Text = "&Save"
            'End If
            'Me.txtRequirementDescription.Text = Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString
            'Me.cmbItem.Value = Val(Me.grdItems.GetRow.Cells("ArticleId").Value.ToString)
            'Me.cmbUnit.Value = Val(Me.grdItems.GetRow.Cells("UnitId").Value.ToString)
            'Me.cmbType.Value = Val(Me.grdItems.GetRow.Cells("ItemTypeId").Value.ToString)
            'Me.cmbCategory.Value = Val(Me.grdItems.GetRow.Cells("CategoryId").Value.ToString)
            'Me.cmbSubCategory.Value = Val(Me.grdItems.GetRow.Cells("SubCategoryId").Value.ToString)
            'Me.cmbOrigin.Value = Val(Me.grdItems.GetRow.Cells("OriginId").Value.ToString)
            'Me.txtValidityOfQuotation.Text = Me.grdItems.GetRow.Cells("ValidityOfQuotation").Value.ToString
            Me.txtQty1.Text = Me.grdItems.GetRow.Cells("Qty").Value.ToString
            Me.txtDescription.Text = Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString
            'Me.txtQuotation.Text = Me.grdItems.GetRow.Cells("QuotedTerms").Value.ToString
            'Me.txtValidityOfQuotation.Text = Me.grdItems.GetRow.Cells("ValidityOfQuotation").Value.ToString
            'Me.txtDeliveryPeriod.Text = Me.grdItems.GetRow.Cells("DeliveryPeriod").Value.ToString
            'Me.txtWarranty.Text = Me.grdItems.GetRow.Cells("Warranty").Value.ToString
            'Me.txtApproxgrossweight.Text = Me.grdItems.GetRow.Cells("ApproxGrossWeight").Value.ToString
            'Me.txtHSCode.Text = Me.grdItems.GetRow.Cells("HSCode").Value.ToString
            'Me.txtDeliveryPort.Text = Me.grdItems.GetRow.Cells("DeliveryPort").Value.ToString
            'Me.txtGenuineOrReplacement.Text = Me.grdItems.GetRow.Cells("GenuineOrReplacement").Value.ToString
            'Me.txtLiteratureDatasheet.Text = Me.grdItems.GetRow.Cells("LiteratureOrDatasheet").Value.ToString
            'Me.txtNewRefurbish.Text = Me.grdItems.GetRow.Cells("NewOrRefurbish").Value.ToString
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
            'dt.Columns("NetCostValue").Expression = "(IsNull(Amount, 0)+IsNull(OtherCharges, 0))"
            'dt.Columns("QuotedCostValue").Expression = "(IsNull(NetCostValue, 0) + (IsNull(NetCostValue, 0)* IsNull(Margin, 0))/100)"
            'Ali Faisal : TFS1355 : Get value from grid to text Box
            Me.txtMargin.Text = Val(Me.grdItems.GetRow.Cells("Margin").Value.ToString)
            'Ali Faisal : TFS1355 : End
            QuotedCostValue = Val(Me.grdItems.GetRow.Cells("QuotedCostValue").Value.ToString)
            Me.cmbCurrency.SelectedValue = Val(Me.grdItems.GetRow.Cells("CurrencyId").Value.ToString)

            'PurchaseInquiryDetailId = Val(Me.grdItems.GetRow.Cells("PurchaseInquiryDetailId").Value.ToString)
            'Me.txtReferenceNo.Text = Me.grdItems.GetRow.Cells("ReferenceNo").Value.ToString
            'Ali Faisal : TFS1355 : Get value from grid to text box
            Me.txtComments.Text = Me.grdItems.GetRow.Cells("Comments").Value.ToString
            'Ali Faisal : TFS1355 : End
            'HeadArticleId = Me.grdItems.GetRow.Cells("HeadArticleId").Value
            'If Val(Me.grdItems.GetRow.Cells("CurrencyId").Value.ToString) > 0 Then
            '    Me.cmbCurrency.SelectedValue = Val(Me.grdItems.GetRow.Cells("CurrencyId").Value.ToString)
            'End If
            'IsAlternate = Me.grdItems.GetRow.Cells("Alternate").Value
            DisplayDetailCharges(VendorQuotationDetailId)
            Me.grdVendorCharges.DataSource = DetailDAL.GetVendorCharges(VendorQuotationDetailId)
            Me.UltraTabControl2.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub LoadDetailRecord()
        Try
            'Me.grdItems.GetRow.Cells("SerialNo").Value = SerialNo
            'Me.grdItems.GetRow.Cells("VendorQuotationId").Value = VendorQuotationId
            Me.grdItems.GetRow.BeginEdit()
            Me.grdItems.GetRow.Cells("VendorQuotationDetailId").Value = VendorQuotationDetailId
            VendorQuotationDetailId = 0
            'Me.grdItems.GetRow.Cells("RequirementDescription").Value = Me.txtRequirementDescription.Text
            'If Me.cmbItem.Value > 0 Then
            '    Me.grdItems.GetRow.Cells("ArticleId").Value = Me.cmbItem.Value
            'End If
            'Me.grdItems.GetRow.Cells("UnitId").Value = Me.cmbUnit.Value
            'Me.grdItems.GetRow.Cells("ItemTypeId").Value = Me.cmbType.Value
            'Me.grdItems.GetRow.Cells("CategoryId").Value = Me.cmbCategory.Value
            'Me.grdItems.GetRow.Cells("SubCategoryId").Value = Me.cmbSubCategory.Value
            'Me.grdItems.GetRow.Cells("OriginId").Value = Me.cmbOrigin.Value
            'Me.grdItems.GetRow.Cells("ValidityOfQuotation").Value = Me.txtValidityOfQuotation.Text
            Me.grdItems.GetRow.Cells("Qty").Value = Val(Me.txtQty1.Text)
            Me.grdItems.GetRow.Cells("RequirementDescription").Value = Me.txtDescription.Text
            'Me.grdItems.GetRow.Cells("QuotedTerms").Value = Me.txtQuotation.Text
            'Me.grdItems.GetRow.Cells("ValidityOfQuotation").Value = Me.txtValidityOfQuotation.Text
            'Me.grdItems.GetRow.Cells("DeliveryPeriod").Value = Me.txtDeliveryPeriod.Text
            'Me.grdItems.GetRow.Cells("Warranty").Value = Me.txtWarranty.Text
            'Me.grdItems.GetRow.Cells("ApproxGrossWeight").Value = Me.txtApproxgrossweight.Text
            'Me.grdItems.GetRow.Cells("HSCode").Value = Me.txtHSCode.Text
            'Me.grdItems.GetRow.Cells("DeliveryPort").Value = Me.txtDeliveryPort.Text
            'Me.grdItems.GetRow.Cells("GenuineOrReplacement").Value = Me.txtGenuineOrReplacement.Text
            'Me.grdItems.GetRow.Cells("LiteratureOrDatasheet").Value = Me.txtLiteratureDatasheet.Text
            'Me.grdItems.GetRow.Cells("NewOrRefurbish").Value = Me.txtNewRefurbish.Text
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
            'dt.Columns("Amount").Expression = "(IsNull(NetPrice, 0)*IsNull(Qty, 0))"
            Me.grdItems.GetRow.Cells("OtherCharges").Value = Me.txtCharges.Text
            Me.grdItems.GetRow.Cells("NetCostValue").Value = Me.txtNetCostValue.Text
            'Ali Faisal : TFS1355 : Get value from Text box to grid
            Me.grdItems.GetRow.Cells("Margin").Value = Me.txtMargin.Text
            'Ali Faisal : TFS1355 : End
            Me.grdItems.GetRow.Cells("QuotedCostValue").Value = (Val(Me.txtNetCostValue.Text) + (Val(Me.txtNetCostValue.Text) * Margin1) / 100)
            'dt.Columns("QuotedCostValue").Expression = "(IsNull(NetCostValue, 0) + (IsNull(NetCostValue, 0)* IsNull(Margin, 0))/100)"
            'If PurchaseInquiryDetailId Then
            '    Me.grdItems.GetRow.Cells("PurchaseInquiryDetailId").Value = PurchaseInquiryDetailId
            'End If
            'Me.grdItems.GetRow.Cells("ReferenceNo").Value = Me.txtReferenceNo.Text
            'Ali Faisal : TFS1355 : Get value from text box to grid
            Me.grdItems.GetRow.Cells("Comments").Value = Me.txtComments.Text
            'Ali Faisal : TFS1355 : End
            'Me.grdItems.GetRow.Cells("HeadArticleId").Value = HeadArticleId
            'Me.grdItems.GetRow.Cells("BaseCurrencyId").Value = BaseCurrencyId
            If Me.cmbNewCurrency.SelectedIndex > 0 Then
                Me.grdItems.GetRow.Cells("BaseCurrencyRate").Value = 1
                Me.grdItems.GetRow.Cells("CurrencyId").Value = Me.cmbNewCurrency.SelectedValue
                Me.grdItems.GetRow.Cells("Currency").Value = Me.cmbNewCurrency.Text
                Me.grdItems.GetRow.Cells("CurrencyRate").Value = Val(Me.txtCurrencyRate.Text)
                Me.grdItems.GetRow.Cells("CurrencySymbol").Value = Me.cmbNewCurrency.Text
            Else
                Me.grdItems.GetRow.Cells("BaseCurrencyRate").Value = 1
                Me.grdItems.GetRow.Cells("CurrencyId").Value = Me.cmbCurrency.SelectedValue
                Me.grdItems.GetRow.Cells("Currency").Value = Me.cmbCurrency.Text
                Me.grdItems.GetRow.Cells("CurrencyRate").Value = Val(Me.txtCurrencyRate.Text)
                Me.grdItems.GetRow.Cells("CurrencySymbol").Value = Me.cmbCurrency.Text
            End If
            Me.grdItems.GetRow.EndEdit()
            'Me.grdItems.UpdateData()
            'Me.grdItems.GetRow.Cells("Alternate").Value = IsAlternate
            'DisplayDetailCharges(VendorQuotationDetailId)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
    Public Sub DisplayDetailCharges(ByVal VendorQuoationDetailId As Integer)
        Try
            Me.grdCharges.DataSource = DetailDAL.GetDetailCharges(VendorQuoationDetailId)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbNewCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNewCurrency.SelectedIndexChanged
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

    Private Sub txtPrice_TextChanged(sender As Object, e As EventArgs) Handles txtPrice.TextChanged, txtQty1.TextChanged
        Try
            'If Val(Me.txtPrice.Text) > 0 Then
            Me.txtNetPrice.Text = Val(Me.txtPrice.Text)
            Me.txtPriceafterdiscount.Text = Val(Me.txtPrice.Text)
            Me.txtAmount.Text = Val(Me.txtNetPrice.Text) * Val(Me.txtQty1.Text) ') + Val(txtCharges.Text)
            'txtAmount_TextChanged(Nothing, Nothing)
            TaxAmounts()
            GetNetPrice()
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrice.KeyPress, txtQty1.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrice_Leave(sender As Object, e As EventArgs) Handles txtPrice.Leave, txtQty1.Leave
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

    Private Sub txtDiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscount.KeyPress
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

    Private Sub txtPriceafterdiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPriceafterdiscount.KeyPress
        Try
            NumValidation(sender, e)
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

    Private Sub txtNetPrice_TextChanged(sender As Object, e As EventArgs) Handles txtNetPrice.TextChanged
        Try
            If Val(Me.txtNetPrice.Text) > 0 AndAlso Val(Me.txtQty1.Text) > 0 Then
                If Me.cmbNewCurrency.SelectedValue > 0 Then
                    Me.txtAmount.Text = Val(Me.txtNetPrice.Text) * Val(Me.txtQty1.Text) * Val(Me.txtCurrencyRate.Text)
                Else
                    Me.txtAmount.Text = Val(Me.txtNetPrice.Text) * Val(Me.txtQty1.Text)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        Try
            If Val(Me.txtAmount.Text) > 0 Then
                Me.txtNetCostValue.Text = (Val(Me.txtAmount.Text) + Val(Me.txtCharges.Text))
                'Me.txtNetCostValue.Text = Val(Me.txtAmount.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCharges_TextChanged(sender As Object, e As EventArgs) Handles txtCharges.TextChanged
        Try
            'If Val(Me.txtAmount.Text) > 0 AndAlso Val(Me.txtCharges.Text) > 0 Then
            '    Me.txtNetCostValue.Text = Val(Me.txtAmount.Text) + Val(Me.txtCharges.Text)
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pnCosting_MouseLeave(sender As Object, e As EventArgs) Handles pnCosting.MouseLeave
        Try
            Me.grdItems.UpdateData()
            Me.txtCharges.Text = Val(Me.grdCharges.GetTotal(Me.grdCharges.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum))
            txtAmount_TextChanged(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pnCosting_Paint(sender As Object, e As PaintEventArgs) Handles pnCosting.Paint

    End Sub

    Private Sub btnLoadChanges_Click(sender As Object, e As EventArgs) Handles btnLoadChanges.Click
        Try
            LoadDetailRecord()
            AddCharges()
            If SplitContainer1.Panel2Collapsed = False Then
                SplitContainer1.Panel2Collapsed = True
            End If
            ResetDetailControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_DoubleClick(sender As Object, e As EventArgs) Handles grdItems.DoubleClick
        Try
            ShowDetailRecord()
            If SplitContainer1.Panel2Collapsed = True Then
                SplitContainer1.Panel2Collapsed = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblNewCurrency_Click(sender As Object, e As EventArgs) Handles lblNewCurrency.Click

    End Sub

    Private Sub GroupBox6_Enter(sender As Object, e As EventArgs) Handles GroupBox6.Enter

    End Sub

    Private Sub txtCurrencyRate_TextChanged(sender As Object, e As EventArgs) Handles txtCurrencyRate.TextChanged
        Try
            If Me.cmbNewCurrency.SelectedValue > 0 Then
                'If Val(Me.txtCurrencyRate.Text) > 0 Then
                '    Me.txtPrice.Text = Val(Me.txtCurrencyRate.Text) * Val(Me.txtPrice.Text)
                'End If
                If Val(Me.txtNetPrice.Text) > 0 AndAlso Val(Me.txtQty1.Text) > 0 Then
                    Me.txtAmount.Text = Val(Me.txtNetPrice.Text) * Val(Me.txtQty1.Text) * Val(Me.txtCurrencyRate.Text) ' + Val(Me.txtCharges.Text)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCurrencyRate_Leave(sender As Object, e As EventArgs) Handles txtCurrencyRate.Leave
        'Try
        '    If Me.cmbNewCurrency.SelectedValue > 0 Then
        '        Me.txtPrice.Text = Val(Me.txtCurrencyRate.Text) * Val(Me.txtPrice.Text)
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtCurrencyRate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCurrencyRate.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ResetDetailControls()
        Try
            'Me.btnSave1.Text = "&Save"
            'Me.txtQuotation.Text = ""
            'Me.txtValidityOfQuotation.Text = ""
            'Me.txtDeliveryPeriod.Text = ""
            'Me.txtWarranty.Text = ""
            'Me.txtApproxgrossweight.Text = ""
            'Me.txtHSCode.Text = ""
            'Me.txtDeliveryPort.Text = ""
            'Me.txtGenuineOrReplacement.Text = ""
            'Me.txtLiteratureDatasheet.Text = ""
            'Me.txtNewRefurbish.Text = ""
            If Not Me.cmbCurrency.SelectedIndex = -1 Then
                Me.cmbCurrency.SelectedIndex = 0
            End If
            If Not Me.cmbNewCurrency.SelectedIndex = -1 Then
                Me.cmbNewCurrency.SelectedIndex = 0
            End If
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
            'PurchaseInquiryDetailId = 0
            'Me.txtReferenceNo.Text = ""
            Me.txtPriceafterdiscount.Text = ""
            Me.txtQty1.Text = ""
            'HeadArticleId = 0
            'Me.txtRequirementDescription.Text = ""
            'Me.cmbItem.Rows(0).Activate()
            'Me.cmbUnit.Rows(0).Activate()
            'Me.cmbType.Rows(0).Activate()
            'Me.cmbCategory.Rows(0).Activate()
            'Me.cmbSubCategory.Rows(0).Activate()
            'Me.cmbOrigin.Rows(0).Activate()
            'Me.txtQty.Text = "Qty"
            'Ali Faisal : TFS1355 : Reset details
            Me.txtComments.Text = ""
            Me.txtMargin.Text = 0
            'Ali Faisal : TFS1355 : End
            'Me.txtReferenceNo.Text = "Reference No"
            'IsAlternate = False
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
    Public Sub DisplayChargesTypes()
        Try
            Me.grdCharges.DataSource = DetailDAL.GetChargesTypes()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Ali Faisal : TFS1355 : Net cost value calculation
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>'Ali Faisal : TFS1355 : 21-Aug-2017</remarks>
    Private Sub txtMargin_Leave(sender As Object, e As EventArgs) Handles txtMargin.Leave
        Try
            Dim NetCostValue As Double = Me.txtNetCostValue.Text
            Dim MarginPrice As Double = NetCostValue * Me.txtMargin.Text / 100
            Me.txtNetCostValue.Text = MarginPrice + NetCostValue
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnGetCustomerCharges_Click(sender As Object, e As EventArgs) Handles btnGetCustomerCharges.Click

        Try
            Me.grdCharges.DataSource = DetailDAL.GetCustomerRemainingChargesTypes(VendorQuotationDetailId)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnGetVendorCharges_Click(sender As Object, e As EventArgs) Handles btnGetVendorCharges.Click
        Try
            Me.grdVendorCharges.DataSource = DetailDAL.GetVendorRemainingChargesTypes(VendorQuotationDetailId)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function AddCharges() As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction
        Try
            VendorQuotationDetailId = Val(Me.grdItems.GetRow.Cells("VendorQuotationDetailId").Value.ToString)
            trans = Con.BeginTransaction
            Dim str As String = "DELETE FROM VendorQuotationCharges WHERE VendorQuotationDetailId = " & VendorQuotationDetailId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            For Each row As Janus.Windows.GridEX.GridEXRow In grdVendorCharges.GetDataRows
                Dim strSQL As String = String.Empty
                Me.grdVendorCharges.UpdateData()
                If Val(row.Cells("Amount").Value) > 0 Then
                    strSQL = "INSERT INTO VendorQuotationCharges(VendorQuotationDetailId, VendorQuotationChargesTypeId, Amount) " _
                    & " VALUES(" & VendorQuotationDetailId & ", " & Val(row.Cells("VendorQuotationChargesTypeId").Value) & ", " & Val(row.Cells("Amount").Value) & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Private Sub btnAddInExisting_Click(sender As Object, e As EventArgs) Handles btnAddInExisting.Click
        Try
            frmSalesQuotationDailog.ShowDialog()
            If Val(frmSalesQuotationDailog.QuotationId) > 0 Then
                If IsValidateQuotation() Then
                    UpdateQuotation()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TFS2019
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SavePurchaseOrder()
        Dim VendorId As Integer = 0
        Dim VendorQuotationId As Integer = 0
        Dim Customers As New List(Of Integer)
        Dim IsMasterFilled As Boolean = False
        Try
            'For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            '    Customers.Add(Val(Row.Cells("CustomerId").Value.ToString))
            '    'Customers.Exists()
            'Next
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
                Dim PurchaseInquiryDetailId As Integer = 0

                If Val(Row.Cells("InquiryComparisonStatementId").Value.ToString) > 0 Then
                    PurchaseInquiryDetailId = Val(Row.Cells("InquiryComparisonStatementId").Value.ToString)
                Else
                    PurchaseInquiryDetailId = Val(Row.Cells("HeadArticleId").Value.ToString)
                End If
                If InquiryComparisonStatementDAL.IsPurchaseInquiryExists(PurchaseInquiryDetailId) = False Then
                    'If Val(Row.Cells("CustomerId").Value.ToString) > 0 Then
                    'If CustomerId > 0 Then
                    'If CustomerId <> Val(Row.Cells("CustomerId").Value.ToString) Then
                    If IsMasterFilled = False Then
                        PurchaseOrder = New PurchaseOrderMaster()
                        PurchaseOrder.PurchaseOrderNo = GetPurchaseOrderDocumentNo()
                        PurchaseOrder.UserName = LoginUserName
                        PurchaseOrder.PurchaseOrderDate = Now
                        Dim objCompany As Object = Me.cmbCompany.SelectedItem
                        PurchaseOrder.LocationId = objCompany(0)
                        VendorId = Val(Row.Cells("VendorId").Value.ToString)
                        VendorQuotationId = Val(Row.Cells("VendorQuotationId").Value.ToString)
                        PurchaseOrder.VendorId = VendorId
                        PurchaseOrder.Status = "Open"
                        If Not Me.cmbTermAndCondition.SelectedIndex = -1 Then
                            If Me.cmbTermAndCondition.SelectedIndex > 0 Then
                                PurchaseOrder.Terms_And_Condition = Me.cmbTermAndCondition.Text
                            Else
                                PurchaseOrder.Terms_And_Condition = ""
                            End If
                        End If
                        IsMasterFilled = True
                    End If
                    'End If
                    Dim dt As DataTable
                    If Row.Cells("Alternate").Value.ToString = "False" Then
                        dt = InquiryComparisonStatementDAL.GetApprovedOnes(PurchaseInquiryDetailId)
                    Else
                        dt = InquiryComparisonStatementDAL.GetApprovedOnesAlternate(PurchaseInquiryDetailId)
                    End If
                    For Each drRow As DataRow In dt.Rows
                        Dim Detail As PurchaseOrderDetail = New PurchaseOrderDetail()
                        Detail.PurchaseOrderDetailId = 0
                        Detail.PurchaseOrderId = 0
                        Dim objCategory As Object = Me.cmbCategory.SelectedItem
                        If objCategory Is Nothing Then
                            Detail.LocationId = 0
                        Else
                            Detail.LocationId = objCategory(0)
                        End If
                        Detail.ArticleDefId = Val(drRow("ArticleId").ToString)
                        Detail.ArticleSize = ""
                        Detail.Sz1 = Val(drRow("Qty").ToString)
                        Detail.Sz2 = 0
                        Detail.Sz3 = 0
                        Detail.Sz4 = 0
                        Detail.Sz5 = 0
                        Detail.Sz6 = 0
                        Detail.Sz7 = 0
                        Detail.Qty = Val(drRow("Qty").ToString)
                        'Detail.Price = Val(drRow("NetPrice").ToString)
                        Detail.Price = Val(drRow("QuotedCostValue").ToString) / Val(drRow("Qty").ToString)
                        Detail.CurrentPrice = Val(drRow("NetPrice").ToString)
                        Detail.DeliveredQty = 0
                        Detail.PackPrice = 0
                        Detail.Pack_Desc = ""
                        Detail.Comments = drRow("Comments").ToString
                        Detail.Pack_40Kg_Weight = 0
                        Detail.BaseCurrencyId = Val(drRow("BaseCurrencyId").ToString)
                        Detail.BaseCurrencyRate = Val(drRow("BaseCurrencyRate").ToString)
                        Detail.CurrencyId = Val(drRow("CurrencyId").ToString)
                        If Val(drRow("CurrencyId").ToString) = Val(1) Then
                            Detail.CurrencyRate = 1
                        Else
                            Detail.CurrencyRate = Val(drRow("CurrencyRate").ToString)
                        End If
                        Detail.CurrencyAmount = Val(drRow("CurrencyRate").ToString) * Val(drRow("Qty").ToString)
                        PurchaseOrder.DetailList.Add(Detail)
                    Next
                    'Else
                    '    msg_Error("Customer is required to create a Quotation")
                    '    Exit Sub
                    'End If

                    'If InquiryComparisonStatementDAL.AddQuotation(SalesQuotationModel, Val(Row.Cells("VendorQuotationId").Value.ToString)) Then
                    '    SaveActivityLog("POS", "Quotation", EnumActions.Save, LoginUserId, EnumRecordType.Sales, SalesQuotationModel.QuotationNo.Trim, True)
                    '    msg_Information("Quotation has been generated.")
                    'End If

                Else
                    msg_Information("Purchase Order has already been generated.")
                End If
            Next
            If Not PurchaseOrder Is Nothing Then
                If InquiryComparisonStatementDAL.AddPurchaseOrder(PurchaseOrder, VendorQuotationId) Then
                    SaveActivityLog("POS", "Purchase Order", EnumActions.Save, LoginUserId, EnumRecordType.Purchase, PurchaseOrder.PurchaseOrderNo.Trim, True)
                    msg_Information("Purchase Order has been generated.")
                    PurchaseOrder = Nothing
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS2019
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPurchaseOrderDocumentNo() As String
        Try
            Dim objCompany As Object = Me.cmbCompany.SelectedItem
            Dim DocPrefix As String = GetPrefix("frmPurchaseOrderNew")
            If DocPrefix.Length > 0 Then
                If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                    Return GetSerialNo(DocPrefix + "" & Val(objCompany(0).ToString) & "" + "-" + Microsoft.VisualBasic.Right(DateTime.Now.Year, 2) + "-", "PurchaseOrderMasterTable", "PurchaseOrderNo")
                ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                    Return GetNextDocNo(DocPrefix & Val(objCompany(0).ToString) & "-" & Format(DateTime.Now, "yy") & DateTime.Now.Month.ToString("00"), 4, "PurchaseOrderMasterTable", "PurchaseOrderNo")
                Else
                    Return GetNextDocNo(DocPrefix + "" & Val(objCompany(0).ToString) & "", 6, "PurchaseOrderMasterTable", "PurchaseOrderNo")
                End If
            Else
                If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                    Return GetSerialNo("PO" + "" & Val(objCompany(0).ToString) & "" + "-" + Microsoft.VisualBasic.Right(DateTime.Now.Year, 2) + "-", "PurchaseOrderMasterTable", "PurchaseOrderNo")
                ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                    Return GetNextDocNo("PO" & Val(objCompany(0).ToString) & "-" & Format(DateTime.Now, "yy") & DateTime.Now.Month.ToString("00"), 4, "PurchaseOrderMasterTable", "PurchaseOrderNo")
                Else
                    Return GetNextDocNo("PO" + "" & Val(objCompany(0).ToString) & "", 6, "PurchaseOrderMasterTable", "PurchaseOrderNo")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnLoadDemand_Click(sender As Object, e As EventArgs) Handles btnLoadDemand.Click
        Try
            frmLoadPurchaseDemand.ShowDialog()
            DisplayDemand()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DisplayDemand()
        Try
            If Me.DemandId > 0 Then
                Dim dt As New DataTable
                dt = InquiryComparisonStatementDAL.DisplayDemand(Me.DemandId)
                dt.AcceptChanges()
                Me.grdItems.DataSource = dt
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub SetDemandId(ByVal Id As Integer)
        'This is called from an other form to load demandid
        Me.DemandId = Id
    End Sub
    ''' <summary>
    ''' TASK TFS4437
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplateForQuotation("Sales Quotation")
            If EmailTemplate.Length > 0 Then
                GetAutoEmailData()
                GetVendorsEmails()
                FormatStringBuilder(dtEmail)
                If EmailToUser = False Then
                    If VendorEmails.Length > 0 Then
                        CreateOutLookMail()
                        Dim Emails() As String = VendorEmails.Split(";")
                        For Each _email As String In Emails
                            SaveEmailLog(QuotationNo, _email, "frmQoutationNew", Activity)
                        Next
                    End If

                Else '' TASK TFS4659 
                    VendorEmails = LoginUser.LoginUserEmail
                    If VendorEmails.Length > 0 Then
                        CreateOutLookMail()
                        SaveEmailLog(QuotationNo, VendorEmails, "frmQoutationNew", Activity)
                    End If
                End If
                VendorEmails = String.Empty
            Else
                msg_Error("No email template is found for Sales Quotation.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpInquiryFromDate_ValueChanged(sender As Object, e As EventArgs)
        Try
            ''TASK TFS4661
            'If Me.dtpInquiryFromDate.Checked = True Or Me.dtpInquiryToDate.Checked = True Then
            '    If Me.cmbReference.Value > 0 Then
            '        FillCombos("InquiryNumberAgainstVendor")
            '    Else
            '        FillCombos("InquiryNumber")
            '    End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpInquiryToDate_ValueChanged(sender As Object, e As EventArgs)
        Try
            ''TASK TFS4661
            'If Me.dtpInquiryToDate.Checked = True Or Me.dtpInquiryFromDate.Checked = True Then
            '    If Me.cmbReference.Value > 0 Then
            '        FillCombos("InquiryNumberAgainstVendor")
            '    Else
            '        FillCombos("InquiryNumber")
            '    End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpInquiryFromDate_Leave(sender As Object, e As EventArgs) Handles dtpInquiryFromDate.Leave
        Try
            ''TASK TFS4661
            'If Me.dtpInquiryFromDate.Checked = True Or Me.dtpInquiryToDate.Checked = True Then
            If Me.cmbReference.Value > 0 Then
                FillCombos("InquiryNumberAgainstVendor")
            Else
                FillCombos("InquiryNumber")
            End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub dtpInquiryToDate_Leave(sender As Object, e As EventArgs) Handles dtpInquiryToDate.Leave
        Try
            ''TASK TFS4661
            'If Me.dtpInquiryFromDate.Checked = True Or Me.dtpInquiryToDate.Checked = True Then
            If Me.cmbReference.Value > 0 Then
                FillCombos("InquiryNumberAgainstVendor")
            Else
                FillCombos("InquiryNumber")
            End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub grdSaved_KeyDown(sender As Object, e As KeyEventArgs) Handles grdSaved.KeyDown
        Try
            If e.KeyCode = Keys.F2 Then
                frmInquiryItem.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class

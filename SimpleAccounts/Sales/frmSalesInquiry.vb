Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Imports System.Text

Public Class frmSalesInquiry
    Implements IGeneral
    Dim objModel As SalesInquiryDetail
    Dim Master As SalesInquiryMaster
    Dim SalesInquiryId As Integer = 0
    Dim MasterDAL As New SalesInquiryDAL
    Dim DetailDAL As New SalesInquiryDetailDAL
    Dim CustomerInquiryNo As String = String.Empty
    Dim SerialNo As String = String.Empty
    Dim arrFile As List(Of String)
    Dim objPath As String = String.Empty
    Dim IsDetailEdit As Boolean = False
    ''TFS2375 : Ayesha Rehman : This Variable is Added to check the document state ,if it is in Eidt mode or not
    Dim IsEditMode As Boolean = False
    Dim ApprovalProcessId As Integer = 0
    ''TFS2375 : Ayesha Rehman :End
    Dim Posted As Boolean = False
    Dim Posted_Username As String = ""
    Dim PostedDate As DateTime = Date.Now
    Dim IsFormLoaded As Boolean = False ''TFS3113
    Dim dtEmail As DataTable
    Dim EmailTemplate As String = String.Empty
    Dim AllFields As List(Of String)
    Dim AfterFieldsElement As String = String.Empty
    Dim EmailDAL As New EmailTemplateDAL
    Dim html As StringBuilder
    Dim CC As String = ""
    Dim BCC As String = ""

    Public Enum Detail
        SalesInquiryDetailId
        SalesInquiryId
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
        Comments
    End Enum

    Private Sub frmSalesInquiry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetAllRecords()
            FillAllCombos()
            IsFormLoaded = True ''TFS3113
            ReSetControls()
            Me.lblProgress.BackColor = Color.LightYellow
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            MasterDAL.Delete(SalesInquiryId)
            SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Sales, Me.uitxtName.Text.Trim, True)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String
            If Condition = "" Or Condition = "Customer" Then
                strSQL = String.Empty
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Vendor', 'Customer') and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbReference, strSQL)
                    Me.cmbReference.Rows(0).Activate()
                    Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Customer') and  vwCOADetail.coa_detail_id is not  null"
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
                'strSQL = "Select Distinct ArticlePackId, PackName As Name, PackQty, ArticleMasterId From ArticleDefPackTable order by ArticlePackId"
                strSQL = "select ArticleUnitId as Id, ArticleUnitName as Name from ArticleUnitDefTable where active=1 order by sortOrder"
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
            ElseIf Condition = "Location" Then
                strSQL = " SELECT tblDefCompanyLocations.LocationId, tblDefCompanyLocations.LocationTitle, tblDefCompanyLocationType.LocationType, tblListCity.CityName " & _
                        " FROM tblDefCompanyLocations INNER JOIN " & _
                        " tblListCity ON tblDefCompanyLocations.City = tblListCity.CityId INNER JOIN " & _
                        " tblDefCompanyLocationType ON tblDefCompanyLocations.LocationType = tblDefCompanyLocationType.LocationTypeId " & _
                        " where tblDefCompanyLocations.CompanyId=" & Me.cmbReference.ActiveRow.Cells(0).Value
                FillUltraDropDown(Me.cmbCompanyLocation, strSQL)
                If Me.cmbCompanyLocation.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbCompanyLocation.Rows(0).Activate()
                    Me.cmbCompanyLocation.DisplayLayout.Bands(0).Columns("LocationId").Hidden = True
                End If
            ElseIf Condition = "ContactPerson" Then
                strSQL = "select Pk_Id, ContactName, Designation, Mobile from TblCompanyContacts where RefCompanyId=" & Me.cmbReference.ActiveRow.Cells(0).Value
                FillUltraDropDown(Me.cmbContactPerson, strSQL)
                If Me.cmbContactPerson.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbContactPerson.Rows(0).Activate()
                    Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Pk_Id").Hidden = True
                    'Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Name").Hidden = True
                End If

            ElseIf Condition = "IndentDepartment" Then
                strSQL = "SELECT  Distinct IndentDepartment, IndentDepartment " & _
                    " FROM SalesInquiryMaster"
                FillDropDown(Me.cmbIndentDept, strSQL)
            ElseIf Condition = "SearchCustomer" Then
                strSQL = String.Empty
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Vendor', 'Customer') and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbSearchCustomer, strSQL)
                    Me.cmbSearchCustomer.Rows(0).Activate()
                    Me.cmbSearchCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Customer') and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbSearchCustomer, strSQL)
                    Me.cmbSearchCustomer.Rows(0).Activate()
                    Me.cmbSearchCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                End If
            End If
            'ElseIf Condition = "Customer" Then
            '    strSQL = "Select coa_detail_id, detail_title From vwCOADetail WHERE account_type='Vendor'"
            '    FillUltraDropDown(Me.cmbReference, strSQL)
            '    If Me.cmbReference.DisplayLayout.Bands.Count > 0 Then
            '        Me.cmbReference.Rows(0).Activate()
            '    End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub FillAllCombos()
        Try
            FillCombos("Customer")
            FillCombos("Item")
            FillCombos("Type")
            FillCombos("Origin")
            FillCombos("Unit")
            FillCombos("Category")
            FillCombos("SubCategory")
            FillCombos("IndentDepartment")
            FillCombos("ContactPerson")
            FillCombos("Location")
            FillCombos("SearchCustomer")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Me.grdItems.UpdateData()
            Master = New SalesInquiryMaster()
            Master.SalesInquiryId = SalesInquiryId
            Master.SalesInquiryNo = Me.uitxtName.Text
            Master.SalesInquiryDate = Me.dtpInquiryDate.Value
            Master.CustomerId = Me.cmbReference.Value
            Master.LocationId = Me.cmbCompanyLocation.Value
            Master.ContactPersonId = Me.cmbContactPerson.Value
            Master.CustomerInquiryNo = Me.txtCustomerInquiryNo.Text
            Master.IndentNo = Me.txtIndentNo.Text
            If Me.cmbIndentDept.Text.Contains(".... Select any Value ....") = False AndAlso Me.cmbIndentDept.Text.Contains("Select any value") = False Then
                Master.IndentDepartment = Me.cmbIndentDept.Text
            Else
                Master.IndentDepartment = ""
            End If
            Master.CustomerInquiryDate = Me.dtpCustomerInquiryDate.Value
            Master.OldInquiryNo = Me.txtInquiryNo.Text
            Master.DueDate = Me.dtpDueDate.Value
            If Me.dtpOldInquiryDate.Checked = True Then
                Master.OldInquiryDate = Me.dtpOldInquiryDate.Value
            Else
                Master.OldInquiryDate = DateTime.MinValue
            End If
            Master.Remarks = Me.txtRemarks.Text
            Master.UserName = LoginUserName
            ''Start TFS3113
            If Not getConfigValueByType("SalesInquiryApproval") = "Error" Then
                ApprovalProcessId = getConfigValueByType("SalesInquiryApproval")
            End If
            If IsEditMode = False And ApprovalProcessId <= 0 Then
                Master.Posted = True
                Master.Posted_UserName = LoginUserName
                Master.PostedDate = Date.Now
            ElseIf IsEditMode = False And ApprovalProcessId > 0 Then
                Master.Posted = False
                Master.Posted_UserName = " "
                Master.PostedDate = Date.Now
            ElseIf IsEditMode = True Then
                Master.Posted = Posted
                Master.Posted_UserName = Posted_Username
                Master.PostedDate = PostedDate
            End If
            ''End TFS3113
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetRows
                objModel = New SalesInquiryDetail()
                objModel.SalesInquiryDetailId = Val(Row.Cells("SalesInquiryDetailId").Value.ToString)
                ''Commented Against TFS3685 : Ayesha Rehman : 27-06-2018
                'objModel.SalesInquiryId = Val(Row.Cells("SalesInquiryId").Value.ToString)
                ''Start TFS3685 : Previously SalesInquiryId was 0 for newly added items in edit mode ,thus not Saving new items 
                objModel.SalesInquiryId = Master.SalesInquiryId
                ''End TFS3685
                objModel.SerialNo = Row.Cells("SerialNo").Value.ToString
                objModel.RequirementDescription = Row.Cells("RequirementDescription").Value.ToString
                objModel.ArticleId = Val(Row.Cells("ArticleId").Value.ToString)
                objModel.UnitId = Val(Row.Cells("UnitId").Value.ToString)
                objModel.ItemTypeId = Val(Row.Cells("ItemTypeId").Value.ToString)
                objModel.OriginId = Val(Row.Cells("OriginId").Value.ToString)
                objModel.CategoryId = Val(Row.Cells("CategoryId").Value.ToString)
                objModel.SubCategoryId = Val(Row.Cells("SubCategoryId").Value.ToString)
                objModel.Qty = Val(Row.Cells("Qty").Value.ToString)
                objModel.Comments = Row.Cells("Comments").Value.ToString
                Master.DetailList.Add(objModel)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If LoginUserName = "Administrator" Or LoginUserName = "Admin" Then
                Me.grdSaved.DataSource = New SalesInquiryDAL().GetAllRecords(Me.Name)
            Else
                Me.grdSaved.DataSource = New SalesInquiryDAL().GetAllRecords(Me.Name, LoginUserName)
            End If
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("SalesInquiryId").Visible = False
            Me.grdSaved.RootTable.Columns("CustomerId").Visible = False
            Me.grdSaved.RootTable.Columns("Code").Visible = False
            Me.grdSaved.RootTable.Columns("LocationId").Visible = False
            Me.grdSaved.RootTable.Columns("ContactPersonId").Visible = False
            Me.grdSaved.RootTable.Columns("SalesInquiryNo").Caption = "Inquiry No"
            Me.grdSaved.RootTable.Columns("SalesInquiryDate").Caption = "Inquiry Date"
            Me.grdSaved.RootTable.Columns("CustomerInquiryNo").Caption = "Customer Inquiry No"
            Me.grdSaved.RootTable.Columns("IndentNo").Caption = "Indent No"
            Me.grdSaved.RootTable.Columns("IndentDepartment").Caption = "Indent Department"
            Me.grdSaved.RootTable.Columns("CustomerInquiryDate").Caption = "Custm Inquiry Date"
            Me.grdSaved.RootTable.Columns("OldInquiryNo").Caption = "Old Inquiry No"
            Me.grdSaved.RootTable.Columns("DueDate").Caption = "Due Date"
            Me.grdSaved.RootTable.Columns("OldInquiryDate").Caption = "Old Inquiry Date"
            Me.grdSaved.RootTable.Columns("UserName").Caption = "User Name"
            Me.grdSaved.RootTable.Columns("SalesInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("DueDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("CustomerInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("OldInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("No Of Attachments").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            'Task:2759 Set rounded amount format
            'Me.grdSaved.RootTable.Columns("").FormatString = "N" & DecimalPointInValue

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetTop50(Optional Condition As String = "")
        Try
            Me.grdSaved.DataSource = New SalesInquiryDAL().GetTop50(Me.Name)
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("SalesInquiryId").Visible = False
            Me.grdSaved.RootTable.Columns("CustomerId").Visible = False
            Me.grdSaved.RootTable.Columns("Code").Visible = False
            Me.grdSaved.RootTable.Columns("LocationId").Visible = False
            Me.grdSaved.RootTable.Columns("ContactPersonId").Visible = False
            Me.grdSaved.RootTable.Columns("SalesInquiryNo").Caption = "Inquiry No"
            Me.grdSaved.RootTable.Columns("SalesInquiryDate").Caption = "Inquiry Date"
            Me.grdSaved.RootTable.Columns("CustomerInquiryNo").Caption = "Customer Inquiry No"
            Me.grdSaved.RootTable.Columns("IndentNo").Caption = "Indent No"
            Me.grdSaved.RootTable.Columns("IndentDepartment").Caption = "Indent Department"
            Me.grdSaved.RootTable.Columns("CustomerInquiryDate").Caption = "Custm Inquiry Date"
            Me.grdSaved.RootTable.Columns("OldInquiryNo").Caption = "Old Inquiry No"
            Me.grdSaved.RootTable.Columns("DueDate").Caption = "Due Date"
            Me.grdSaved.RootTable.Columns("OldInquiryDate").Caption = "Old Inquiry Date"
            Me.grdSaved.RootTable.Columns("UserName").Caption = "User Name"
            Me.grdSaved.RootTable.Columns("SalesInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("DueDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("CustomerInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("OldInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("No Of Attachments").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            'Task:2759 Set rounded amount format
            'Me.grdSaved.RootTable.Columns("").FormatString = "N" & DecimalPointInValue

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' TFS2648 : Ayesha Rehman : This function is added to get the document in edit mode when open from anotherside
    ''' </summary>
    ''' <param name="SalesInquiryNo"></param>
    ''' <returns></returns>
    ''' <remarks>TFS2648 : Ayesha Rehman : 26-004-2018</remarks>
    Public Function Get_All(ByVal SalesInquiryNo As String)
        Try
            Get_All = Nothing
            If IsFormLoaded = True Then
                If SalesInquiryNo.Length > 0 Then
                    Dim str As String = "Select * from SalesInquiryMaster where SalesInquiryNo =N'" & SalesInquiryNo & "'"
                    Dim dt As DataTable = GetDataTable(str)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then

                            GetAllRecords()
                            ''This LOC is Added to check if Record exist agianst the login user
                            If Not Me.grdSaved.RecordCount > 0 Then Exit Function
                            Dim flag As Boolean = False
                            flag = Me.grdSaved.FindAll(Me.grdSaved.RootTable.Columns("SalesInquiryNo"), Janus.Windows.GridEX.ConditionOperator.Equal, SalesInquiryNo)
                            Me.uitxtName.Text = Me.grdSaved.CurrentRow.Cells("SalesInquiryNo").Value.ToString
                            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
                            IsEditMode = True
                            If Not getConfigValueByType("SalesInquiryApproval") = "Error" Then
                                ApprovalProcessId = getConfigValueByType("SalesInquiryApproval")
                            End If
                            If ApprovalProcessId = 0 Then
                                Me.btnApprovalHistory.Visible = False
                                Me.btnApprovalHistory.Enabled = False
                            Else
                                Me.btnApprovalHistory.Visible = True
                                Me.btnApprovalHistory.Enabled = True
                            End If
                            ''Ayesha Rehman :TFS2375 :End
                            Me.txtInquiryNo.Text = Me.grdSaved.CurrentRow.Cells("OldInquiryNo").Value.ToString
                            Me.txtIndentNo.Text = Me.grdSaved.CurrentRow.Cells("IndentNo").Value.ToString
                            CustomerInquiryNo = Me.grdSaved.CurrentRow.Cells("CustomerInquiryNo").Value.ToString
                            Me.txtCustomerInquiryNo.Text = CustomerInquiryNo
                            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells("Remarks").Value.ToString
                            'Me.txtComments.Text = Me.grdSaved.CurrentRow.Cells("CustomerInquiryNo").Value.ToString
                            If IsDBNull(Me.grdSaved.CurrentRow.Cells("SalesInquiryDate").Value) Then
                                Me.dtpInquiryDate.Value = Now
                            Else
                                Me.dtpInquiryDate.Value = Me.grdSaved.CurrentRow.Cells("SalesInquiryDate").Value
                            End If
                            If IsDBNull(Me.grdSaved.CurrentRow.Cells("CustomerInquiryDate").Value) Then
                                Me.dtpCustomerInquiryDate.Value = Now
                            Else
                                Me.dtpCustomerInquiryDate.Value = Me.grdSaved.CurrentRow.Cells("CustomerInquiryDate").Value
                            End If
                            If IsDBNull(Me.grdSaved.CurrentRow.Cells("DueDate").Value) Then
                                Me.dtpDueDate.Value = Now
                            Else
                                Me.dtpDueDate.Value = Me.grdSaved.CurrentRow.Cells("DueDate").Value
                            End If

                            If IsDBNull(Me.grdSaved.CurrentRow.Cells("OldInquiryDate").Value) Then
                                Me.dtpOldInquiryDate.Checked = False
                            Else
                                Me.dtpOldInquiryDate.Checked = True
                                Me.dtpOldInquiryDate.Value = Me.grdSaved.CurrentRow.Cells("OldInquiryDate").Value

                            End If
                            Me.cmbReference.Value = Val(Me.grdSaved.CurrentRow.Cells("CustomerId").Value.ToString)
                            Me.cmbContactPerson.Value = Val(Me.grdSaved.CurrentRow.Cells("ContactPersonId").Value.ToString)
                            Me.cmbCompanyLocation.Value = Val(Me.grdSaved.CurrentRow.Cells("LocationId").Value.ToString)
                            Me.cmbIndentDept.Text = Me.grdSaved.CurrentRow.Cells("IndentDepartment").Value.ToString
                            SalesInquiryId = Val(Me.grdSaved.CurrentRow.Cells("SalesInquiryId").Value.ToString)
                            Me.btnAttachment.Text = "Attachment (" & Me.grdSaved.GetRow.Cells("No Of Attachments").Value.ToString & ")"
                            DisplayDetail(SalesInquiryId)
                            Me.BtnSave.Text = "&Update"
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
    Public Sub GetAll(Optional Condition As String = "")
        Try
            Me.grdSaved.DataSource = New SalesInquiryDAL().GetAll(Me.Name)
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("SalesInquiryId").Visible = False
            Me.grdSaved.RootTable.Columns("CustomerId").Visible = False
            Me.grdSaved.RootTable.Columns("Code").Visible = False
            Me.grdSaved.RootTable.Columns("LocationId").Visible = False
            Me.grdSaved.RootTable.Columns("ContactPersonId").Visible = False
            Me.grdSaved.RootTable.Columns("SalesInquiryNo").Caption = "Inquiry No"
            Me.grdSaved.RootTable.Columns("SalesInquiryDate").Caption = "Inquiry Date"
            Me.grdSaved.RootTable.Columns("CustomerInquiryNo").Caption = "Customer Inquiry No"
            Me.grdSaved.RootTable.Columns("IndentNo").Caption = "Indent No"
            Me.grdSaved.RootTable.Columns("IndentDepartment").Caption = "Indent Department"
            Me.grdSaved.RootTable.Columns("CustomerInquiryDate").Caption = "Custm Inquiry Date"
            Me.grdSaved.RootTable.Columns("OldInquiryNo").Caption = "Old Inquiry No"
            Me.grdSaved.RootTable.Columns("DueDate").Caption = "Due Date"
            Me.grdSaved.RootTable.Columns("OldInquiryDate").Caption = "Old Inquiry Date"
            Me.grdSaved.RootTable.Columns("UserName").Caption = "User Name"
            Me.grdSaved.RootTable.Columns("SalesInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("DueDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("CustomerInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("OldInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("No Of Attachments").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            'Task:2759 Set rounded amount format
            'Me.grdSaved.RootTable.Columns("").FormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbReference.Value <= 0 Then
                msg_Error("Customer is required")
                Me.cmbReference.Focus() : IsValidate = False : Exit Function
            End If
            If Not grdItems.RowCount > 0 Then
                msg_Error("Items grid is empty")
                Me.grdItems.Focus() : IsValidate = False : Exit Function
            End If
            If Me.txtCustomerInquiryNo.Text.Length <= 0 Then
                msg_Error("Customer inquiry number is required")
                Me.txtCustomerInquiryNo.Focus() : IsValidate = False : Exit Function
            End If
            If Me.txtCustomerInquiryNo.Text <> CustomerInquiryNo Then
                Dim str2 As String = Regex.Replace(Me.txtCustomerInquiryNo.Text, "[\[\]\\\^\$\.\|\?\*\+\(\)\{\}%,;><!@#&\-\+]", "")
                Dim CustomerInquiryNo As String = str2.Replace(" ", String.Empty)
                If MasterDAL.ValidateCustomerInquiryNo(CustomerInquiryNo, Me.cmbReference.Value) = False Then
                    msg_Error("Customer inquiry number is already issued")
                    Me.txtCustomerInquiryNo.Focus() : IsValidate = False : Exit Function
                End If
            End If
            ''Start TFS2988
            If IsEditMode = True Then
                If ValidateApprovalProcessMapped(Me.uitxtName.Text.Trim) Then
                    If ValidateApprovalProcessInProgress(Me.uitxtName.Text.Trim) Then
                        msg_Error("Document is in Approval Process") : Exit Function
                    End If
                End If
            End If
            ''End TFS2988
            objPath = getConfigValueByType("FileAttachmentPath").ToString
            FillModel()

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidateAddToGrid(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean
        Try
            If Val(Me.txtQty.Text) <= 0 Then
                msg_Error("Qty is required")
                Me.txtQty.Focus() : IsValidateAddToGrid = False : Exit Function
            End If
            If cmbItem.Value = 0 AndAlso Me.txtCustomerRequirement.Text = "" Then
                msg_Error("Item description is required")
                Me.cmbItem.Focus() : IsValidateAddToGrid = False : Exit Function
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.uitxtName.Text = GetDocumentNo()
            GetSecurityRights()
            Me.txtInquiryNo.Text = ""
            Me.txtIndentNo.Text = ""
            Me.txtCustomerInquiryNo.Text = ""
            Me.txtRemarks.Text = ""
            Me.txtComments.Text = ""
            Me.BtnSave.Text = "&Save"
            Me.cmbIndentDept.Text = "Select any value"
            CustomerInquiryNo = ""
            Me.dtpInquiryDate.Value = Now
            Me.dtpCustomerInquiryDate.Value = Now
            Me.dtpDueDate.Value = Now
            Me.dtpOldInquiryDate.Checked = False
            Me.cmbReference.Rows(0).Activate()
            Me.cmbContactPerson.Rows(0).Activate()
            Me.cmbCompanyLocation.Rows(0).Activate()
            'If Not Me.cmbIndentDept.SelectedIndex = -1 Then
            '    Me.cmbIndentDept.SelectedIndex = 0
            'End If
            Me.cmbItem.Rows(0).Activate()
            Me.cmbOrigin.Rows(0).Activate()
            Me.cmbType.Rows(0).Activate()
            Me.cmbSubCategory.Rows(0).Activate()
            Me.cmbUnit.Rows(0).Activate()
            Me.cmbCategory.Rows(0).Activate()
            arrFile = New List(Of String)
            Me.btnAttachment.Text = "Attachment"
            Me.SplitContainer1.Panel1Collapsed = True
            DisplayDetail(-1)
            ResetDetailControls()
            'Ayesha Rehman : TFS2375 : Enable Approval History button only in Eidt Mode
            If IsEditMode = True Then
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            Else
                Me.btnApprovalHistory.Visible = False
            End If
            'Ayesha Rehman : TFS2375 : End
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ResetDetailControls()
        Try
            Me.txtCustomerRequirement.Text = ""
            Me.cmbItem.Rows(0).Activate()
            'Me.cmbUnit.Rows(0).Activate()
            'Me.cmbType.Rows(0).Activate()
            'Me.cmbCategory.Rows(0).Activate()
            Me.cmbSubCategory.Rows(0).Activate()
            Me.cmbOrigin.Rows(0).Activate()
            Me.txtQty.Text = "Qty"
            Me.txtComments.Text = "Comments"
            Me.cmbUnit.Enabled = True
            Me.cmbType.Enabled = True
            Me.cmbCategory.Enabled = True
            Me.cmbSubCategory.Enabled = True
            Me.cmbOrigin.Enabled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub EditRecord(Optional Condition As String = "")
        Try

            If Not Me.grdSaved.RecordCount > 0 Then Exit Sub

            If Me.grdItems.RecordCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If

            'SalesInquiry.SalesInquiryId, SalesInquiry.SalesInquiryNo, SalesInquiry.SalesInquiryDate, IsNull(COA.coa_detail_id,0) as CustomerId,
            'COA.detail_code As Code, COA.detail_title As Customer, SalesInquiry.LocationId, SalesInquiry.ContactPersonId, SalesInquiry.CustomerInquiryNo,
            'SalesInquiry.IndentNo, SalesInquiry.IndentDepartment, SalesInquiry.CustomerInquiryDate, SalesInquiry.OldInquiryNo, SalesInquiry.DueDate, SalesInquiry.OldInquiryDate, SalesInquiry.Remarks
            Me.uitxtName.Text = Me.grdSaved.CurrentRow.Cells("SalesInquiryNo").Value.ToString
            'GetSecurityRights()
            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
            IsEditMode = True
            If Not getConfigValueByType("SalesInquiryApproval") = "Error" Then
                ApprovalProcessId = getConfigValueByType("SalesInquiryApproval")
            End If
            If ApprovalProcessId = 0 Then
                Me.btnApprovalHistory.Visible = False
                Me.btnApprovalHistory.Enabled = False
            Else
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            End If
            ''Ayesha Rehman :TFS2375 :End
            Me.txtInquiryNo.Text = Me.grdSaved.CurrentRow.Cells("OldInquiryNo").Value.ToString
            Me.txtIndentNo.Text = Me.grdSaved.CurrentRow.Cells("IndentNo").Value.ToString
            CustomerInquiryNo = Me.grdSaved.CurrentRow.Cells("CustomerInquiryNo").Value.ToString
            Me.txtCustomerInquiryNo.Text = CustomerInquiryNo
            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells("Remarks").Value.ToString
            'Me.txtComments.Text = Me.grdSaved.CurrentRow.Cells("CustomerInquiryNo").Value.ToString
            If IsDBNull(Me.grdSaved.CurrentRow.Cells("SalesInquiryDate").Value) Then
                Me.dtpInquiryDate.Value = Now
            Else
                Me.dtpInquiryDate.Value = Me.grdSaved.CurrentRow.Cells("SalesInquiryDate").Value
            End If
            If IsDBNull(Me.grdSaved.CurrentRow.Cells("CustomerInquiryDate").Value) Then
                Me.dtpCustomerInquiryDate.Value = Now
            Else
                Me.dtpCustomerInquiryDate.Value = Me.grdSaved.CurrentRow.Cells("CustomerInquiryDate").Value
            End If
            If IsDBNull(Me.grdSaved.CurrentRow.Cells("DueDate").Value) Then
                Me.dtpDueDate.Value = Now
            Else
                Me.dtpDueDate.Value = Me.grdSaved.CurrentRow.Cells("DueDate").Value
            End If

            If IsDBNull(Me.grdSaved.CurrentRow.Cells("OldInquiryDate").Value) Then
                Me.dtpOldInquiryDate.Checked = False
            Else
                Me.dtpOldInquiryDate.Checked = True
                Me.dtpOldInquiryDate.Value = Me.grdSaved.CurrentRow.Cells("OldInquiryDate").Value

            End If
            Me.cmbReference.Value = Val(Me.grdSaved.CurrentRow.Cells("CustomerId").Value.ToString)
            'Ali Faisal : TFS3962 : Sales Inquiry Locations and Contact Persons not selected when open for editing.
            FillCombos("Location")
            FillCombos("ContactPerson")
            'Ali Faisal : TFS3962 : End
            Me.cmbContactPerson.Value = Val(Me.grdSaved.CurrentRow.Cells("ContactPersonId").Value.ToString)
            Me.cmbCompanyLocation.Value = Val(Me.grdSaved.CurrentRow.Cells("LocationId").Value.ToString)
            Me.cmbIndentDept.Text = Me.grdSaved.CurrentRow.Cells("IndentDepartment").Value.ToString
            SalesInquiryId = Val(Me.grdSaved.CurrentRow.Cells("SalesInquiryId").Value.ToString)
            ''Start TFS3087 : Ayesha Rehman : 20-04-2018
            Posted = CBool(Me.grdSaved.CurrentRow.Cells("Post").Value.ToString)
            Posted_Username = Me.grdSaved.CurrentRow.Cells("Posted_Username").Value.ToString
            'PostedDate = Me.grdSaved.CurrentRow.Cells("PostedDate").Value
            If IsDBNull(Me.grdSaved.CurrentRow.Cells("PostedDate").Value) Then
                PostedDate = Date.Now
            Else
                PostedDate = Me.grdSaved.CurrentRow.Cells("PostedDate").Value
            End If
            ''End TFS3087 
            Me.btnAttachment.Text = "Attachment (" & Me.grdSaved.GetRow.Cells("No Of Attachments").Value.ToString & ")"
            DisplayDetail(SalesInquiryId)
            Me.BtnSave.Text = "&Update"
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("SINQ-" + Microsoft.VisualBasic.Right(Me.dtpInquiryDate.Value.Year, 2) + "-", "SalesInquiryMaster", "SalesInquiryNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("SINQ-" & Format(Me.dtpInquiryDate.Value, "yy") & Me.dtpInquiryDate.Value.Month.ToString("00"), 4, "SalesInquiryMaster", "SalesInquiryNo")
            Else
                Return GetNextDocNo("SINQ-", 6, "SalesInquiryMaster", "SalesInquiryNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Master.SalesInquiryNo = GetDocumentNo()
            Dim SalesInquiryId As Integer
            MasterDAL.Add(Master, Me.Name, objPath, arrFile, SalesInquiryId)
            SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Sales, Me.uitxtName.Text.Trim, True)
            ''Start TFS2375
            ''insert Approval Log
            SaveApprovalLog(EnumReferenceType.SalesInquiry, SalesInquiryId, Me.uitxtName.Text.Trim, Me.dtpInquiryDate.Value.Date, "Sales Inquiry ", Me.Name)
            ''End TFS2375
            msg_Information("Record has been saved successfully.")
            Return True

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
        Try
            MasterDAL.Update(Master, Me.Name, objPath, arrFile)
            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, Me.uitxtName.Text.Trim, True)
            ''Start TFS2989
            If ValidateApprovalProcessMapped(Me.uitxtName.Text.Trim, Me.Name) Then
                If ValidateApprovalProcessIsInProgressAgain(Me.uitxtName.Text.Trim, Me.Name) = False Then
                    SaveApprovalLog(EnumReferenceType.SalesInquiry, SalesInquiryId, Me.uitxtName.Text.Trim, Me.dtpInquiryDate.Value.Date, "Sales Inquiry ", Me.Name)
                End If
            End If
            ''End TFS2989
            msg_Information("Record has been updated successfully.")
            '' Add by Mohsin on 18 Sep 2017

            ' NOTIFICATION STARTS HERE FOR UPDATE - ADDED BY MOHSIN 14-9-2017 '

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
            objmod1.Notification.NotificationTitle = "Sales Inquiry number [" & Me.uitxtName.Text & "]  is changed."

            '// Preparing notification description string
            objmod1.Notification.NotificationDescription = "Sales Inquiry Note number [" & Me.uitxtName.Text & "] is changed by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

            '// Setting source application as refrence in the notification
            objmod1.Notification.SourceApplication = "Sales Inquiry"



            '// Starting to get users list to add child

            '// Creating notification detail object list
            Dim List1 As New List(Of NotificationDetail)

            '// Getting users list
            List1 = NDal1.GetNotificationUsers("Sales Inquiry Changed")

            '// Adding users list in the Notification object of current inquiry
            objmod1.Notification.NotificationDetils.AddRange(List1)

            '// Getting and adding user groups list in the Notification object of current inquiry
            objmod1.Notification.NotificationDetils.AddRange(NDal1.GetNotificationGroups("Sales Inquiry"))

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
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            GetAllRecords()
            FillCombos()
            ReSetControls()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub cmbReference_Leave(sender As Object, e As EventArgs)
        Try
            If Val(cmbReference.ActiveRow.Cells(0).Value) > 0 Then

                Dim strSQL As String = String.Empty

                '// Loading company locations
                strSQL = " SELECT        tblDefCompanyLocations.LocationId, tblDefCompanyLocations.LocationTitle, tblDefCompanyLocationType.LocationType, tblListCity.CityName " & _
                        " FROM            tblDefCompanyLocations INNER JOIN " & _
                        " tblListCity ON tblDefCompanyLocations.City = tblListCity.CityId INNER JOIN " & _
                        " tblDefCompanyLocationType ON tblDefCompanyLocations.LocationType = tblDefCompanyLocationType.LocationTypeId " & _
                        " where tblDefCompanyLocations.CompanyId=" & Me.cmbReference.ActiveRow.Cells(0).Value
                FillUltraDropDown(Me.cmbCompanyLocation, strSQL)
                Me.cmbCompanyLocation.Rows(0).Activate()
                Me.cmbCompanyLocation.DisplayLayout.Bands(0).Columns("LocationId").Hidden = True


                '// Loading company contacts
                strSQL = " select Pk_Id, ContactName, Designation, Mobile from TblCompanyContacts where RefCompanyId=" & Me.cmbReference.ActiveRow.Cells(0).Value
                FillUltraDropDown(Me.cmbContactPerson, strSQL)
                Me.cmbContactPerson.Rows(0).Activate()
                Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Pk_Id").Hidden = True
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerRequirement.TextChanged

    End Sub
    Public Sub AddToGrid()
        Dim dt As New DataTable
        Try
            If IsDetailEdit Then
                AddEditGrid()
                Exit Sub
            End If
            Dim sNo As String = Me.grdItems.GetRows.GetLength(0)
            dt = CType(Me.grdItems.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item(Detail.SalesInquiryDetailId) = Val(0)
            dr.Item(Detail.SalesInquiryId) = Val(0)
            dr.Item(Detail.SerialNo) = sNo + 1
            dr.Item(Detail.RequirementDescription) = Me.txtCustomerRequirement.Text
            'dr.Item(Detail.RequirementDescription) = Me.txtCustomerRequirement.Text.Replace(vbCrLf, "<br />")
            If Me.cmbItem.Value > 0 Then
                dr.Item(Detail.ArticleId) = Me.cmbItem.Value
                dr.Item(Detail.Code) = Me.cmbItem.ActiveRow.Cells("Code").Value.ToString
                dr.Item(Detail.ArticleDescription) = Me.cmbItem.ActiveRow.Cells("Item").Value.ToString
            Else
                dr.Item(Detail.ArticleId) = 0
                dr.Item(Detail.Code) = ""
                dr.Item(Detail.ArticleDescription) = ""
            End If
            If Not Me.cmbUnit.ActiveRow Is Nothing AndAlso cmbUnit.Value > 0 Then
                dr.Item(Detail.UnitId) = Me.cmbUnit.Value
                dr.Item(Detail.Unit) = Me.cmbUnit.ActiveRow.Cells("Name").Value.ToString
            End If
            If Not Me.cmbType.ActiveRow Is Nothing AndAlso Me.cmbType.Value > 0 Then
                dr.Item(Detail.ItemTypeId) = Me.cmbType.Value
                dr.Item(Detail.Type) = Me.cmbType.ActiveRow.Cells("Name").Value.ToString
            End If
            If Not Me.cmbCategory.ActiveRow Is Nothing AndAlso Me.cmbCategory.Value > 0 Then
                dr.Item(Detail.CategoryId) = Me.cmbCategory.Value
                dr.Item(Detail.Category) = Me.cmbCategory.ActiveRow.Cells("Name").Value.ToString
            End If
            If Not Me.cmbSubCategory.ActiveRow Is Nothing AndAlso Me.cmbSubCategory.Value > 0 Then
                dr.Item(Detail.SubCategoryId) = Me.cmbSubCategory.Value
                dr.Item(Detail.SubCategory) = Me.cmbSubCategory.ActiveRow.Cells("Name").Value.ToString
            End If
            If Not Me.cmbOrigin.ActiveRow Is Nothing AndAlso Me.cmbOrigin.Value > 0 Then
                dr.Item(Detail.OriginId) = Me.cmbOrigin.Value
                dr.Item(Detail.Origin) = Me.cmbOrigin.ActiveRow.Cells("Name").Value.ToString
            End If
            'dr.Item(Detail.UnitId) = Me.cmbUnit.Value
            'dr.Item(Detail.Unit) = Me.cmbUnit.ActiveRow.Cells("Name").Value.ToString
            'dr.Item(Detail.ItemTypeId) = Me.cmbType.Value
            'dr.Item(Detail.Type) = Me.cmbType.ActiveRow.Cells("Name").Value.ToString
            'dr.Item(Detail.CategoryId) = Me.cmbCategory.Value
            'dr.Item(Detail.Category) = Me.cmbCategory.ActiveRow.Cells("Name").Value.ToString
            'dr.Item(Detail.SubCategoryId) = Me.cmbSubCategory.Value
            'dr.Item(Detail.SubCategory) = Me.cmbSubCategory.ActiveRow.Cells("Name").Value.ToString
            'dr.Item(Detail.OriginId) = Me.cmbOrigin.Value
            'dr.Item(Detail.Origin) = Me.cmbOrigin.ActiveRow.Cells("Name").Value.ToString
            dr.Item(Detail.Qty) = Val(Me.txtQty.Text)
            dr.Item(Detail.Comments) = IIf(Me.txtComments.Text = "Comments", "", Me.txtComments.Text)
            dt.Rows.Add(dr)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.BtnPrint.Enabled = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmSalesInquiry)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
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

    Private Sub grdItems_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.ColumnButtonClick
        Try
            If e.Column.Key = "Remove" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                ' Ali Faisal : TFS3960 : Sales Inquiry item delete from grid does not removed from Database.
                DetailDAL.DeleteSalesInquiryDetail(Me.grdItems.GetRow.Cells("SalesInquiryDetailId").Value.ToString)
                Me.grdItems.GetRow.Delete()
                Me.grdItems.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DisplayDetail(ByVal SalesInquiryId As Integer)
        Try
            Me.grdItems.DataSource = DetailDAL.GetSingle(SalesInquiryId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() Then
                If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    'Ali Faisal : TFS1696 : CS approval workflow : Code was at wrong place now commented
                    ' '' Add by Mohsin on 18 Sep 2017

                    '' NOTIFICATION STARTS HERE FOR UPDATE - ADDED BY MOHSIN 14-9-2017 '

                    '' *** New Segment *** 
                    ''// Adding notification

                    ''// Creating new object of Notification configuration dal
                    ''// Dal will be used to get users list for the notification 
                    'Dim NDal1 As New NotificationConfigurationDAL
                    'Dim objmod1 As New VouchersMaster
                    ''// Creating new object of Agrius Notification class
                    'objmod1.Notification = New AgriusNotifications

                    ''// Reference document number
                    'objmod1.Notification.ApplicationReference = Me.uitxtName.Text

                    ''// Date of notification
                    'objmod1.Notification.NotificationDate = Now

                    ''// Preparing notification title string
                    'objmod1.Notification.NotificationTitle = "Sales Inquiry number [" & Me.uitxtName.Text & "]  is created."

                    ''// Preparing notification description string
                    'objmod1.Notification.NotificationDescription = "Sales Inquiry Note number [" & Me.uitxtName.Text & "] is created by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                    ''// Setting source application as refrence in the notification
                    'objmod1.Notification.SourceApplication = "Sales Inquiry"



                    ''// Starting to get users list to add child

                    ''// Creating notification detail object list
                    'Dim List1 As New List(Of NotificationDetail)

                    ''// Getting users list
                    'List1 = NDal1.GetNotificationUsers("Sales Inquiry Created")

                    ''// Adding users list in the Notification object of current inquiry
                    'objmod1.Notification.NotificationDetils.AddRange(List1)

                    ''// Getting and adding user groups list in the Notification object of current inquiry
                    'objmod1.Notification.NotificationDetils.AddRange(NDal1.GetNotificationGroups("Sales Inquiry"))

                    ''// Not getting role list because no role is associated at the moment
                    ''// We will need this in future and we can use it later
                    ''// We can consult to Update function of this class


                    ''// ***This segment will be used to save notification in database table

                    ''// Creating new list of objects of Agrius Notification
                    'Dim NList1 As New List(Of AgriusNotifications)

                    ''// Copying notification object from current sales inquiry to newly defined instance
                    ''// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
                    'NList1.Add(objmod1.Notification)

                    ''// Creating object of Notification DAL
                    'Dim GNotification1 As New NotificationDAL

                    ''// Saving notification to database
                    'GNotification1.AddNotification(NList1)

                    ''// End Adding Notification

                    ''// End Adding Notification
                    '' *** End Segment ***
                    'Ali Faisal : TFS1696 : End

                    If Save() Then
                        GetAllRecords()
                        ReSetControls()
                    End If
                Else
                    'Aashir:3966 : commented because Sales Inquiry Update issue when Purchase Inquiry is made against any Sales Inquiry.
                    'If IsValidToDelete("PurchaseInquiryDetail", "SalesInquiryId", SalesInquiryId) = True Then
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Update1() Then
                        GetAllRecords()
                        ReSetControls()
                    End If
                    'Else
                    '    msg_Error(str_ErrorDependentUpdateRecordFound)
                    'End If

                End If
            End If

            'ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If IsValidateAddToGrid() Then
                AddToGrid()
                ResetDetailControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        If e.Tab.Index = 0 Then
            CtrlGrdBar1.Visible = True
            CtrlGrdBar1.Enabled = True
            CtrlGrdBar2.Visible = False
            CtrlGrdBar2.Enabled = False
            Me.BtnSave.Visible = True
            Me.BtnDelete.Visible = True
            Me.ddbRecord.Visible = False
        ElseIf e.Tab.Index = 1 Then
            CtrlGrdBar2.Visible = True
            CtrlGrdBar2.Enabled = True
            CtrlGrdBar1.Visible = False
            CtrlGrdBar1.Enabled = False
            Me.BtnSave.Visible = False
            Me.BtnDelete.Visible = False
            Me.ddbRecord.Visible = True

        End If
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Dim Id As Integer = 0
        Try
            Id = Me.cmbReference.Value
            FillCombos("Customer")
            Me.cmbReference.Value = Id
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
            Dim Department As String = Me.cmbIndentDept.Text
            FillCombos("IndentDepartment")
            Me.cmbIndentDept.Text = Department
            Id = Me.cmbContactPerson.Value
            FillCombos("ContactPerson")
            Me.cmbContactPerson.Value = Id
            Id = Me.cmbCompanyLocation.Value
            FillCombos("Location")
            Me.cmbCompanyLocation.Value = Id
            'GetSecurityRights()
            'ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Try
            If IsValidToDelete("PurchaseInquiryDetail", "SalesInquiryId", SalesInquiryId) = True Then
                ''Start TFS2988
                If IsEditMode = True Then
                    If ValidateApprovalProcessMapped(Me.uitxtName.Text.Trim) Then
                        If ValidateApprovalProcessInProgress(Me.uitxtName.Text.Trim) Then
                            msg_Error("Document is in Approval Process") : Exit Sub
                        End If
                    End If
                End If
                ''End TFS2988
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Delete() Then
                    GetAllRecords()
                    ReSetControls()
                    '' Add by Mohsin on 18 Sep 2017

                    ' NOTIFICATION STARTS HERE FOR UPDATE - ADDED BY MOHSIN 14-9-2017 '

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
                    objmod1.Notification.NotificationTitle = "Sales Inquiry number [" & Me.uitxtName.Text & "]  is deleted."

                    '// Preparing notification description string
                    objmod1.Notification.NotificationDescription = "Sales Inquiry Note number [" & Me.uitxtName.Text & "] is deleted by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

                    '// Setting source application as refrence in the notification
                    objmod1.Notification.SourceApplication = "Sales Inquiry"



                    '// Starting to get users list to add child

                    '// Creating notification detail object list
                    Dim List1 As New List(Of NotificationDetail)

                    '// Getting users list
                    List1 = NDal1.GetNotificationUsers("Sales Inquiry Deleted")

                    '// Adding users list in the Notification object of current inquiry
                    objmod1.Notification.NotificationDetils.AddRange(List1)

                    '// Getting and adding user groups list in the Notification object of current inquiry
                    objmod1.Notification.NotificationDetils.AddRange(NDal1.GetNotificationGroups("Sales Inquiry"))

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
                msg_Error(str_ErrorDependentRecordFound)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub cmbItem_ValueChanged(sender As Object, e As EventArgs) Handles cmbItem.RowSelected
        Try
            If Not Me.cmbItem.Text = "Select an item" And cmbItem.IsItemInList = True And Not cmbItem.SelectedRow Is Nothing Then
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
            Else
                'Me.cmbUnit.Rows(0).Activate()
                'Me.cmbType.Rows(0).Activate()
                'Me.cmbCategory.Rows(0).Activate()
                'Me.cmbSubCategory.Rows(0).Activate()
                'Me.cmbOrigin.Rows(0).Activate()


                Me.cmbUnit.Enabled = True
                Me.cmbType.Enabled = True
                Me.cmbCategory.Enabled = True
                Me.cmbSubCategory.Enabled = True
                Me.cmbOrigin.Enabled = True

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

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbReference_ValueChanged(sender As Object, e As EventArgs)
        Try
            If Val(cmbReference.ActiveRow.Cells(0).Value) > 0 Then

                Dim strSQL As String = String.Empty

                '// Loading company locations
                strSQL = " SELECT        tblDefCompanyLocations.LocationId, tblDefCompanyLocations.LocationTitle, tblDefCompanyLocationType.LocationType, tblListCity.CityName " & _
                        " FROM            tblDefCompanyLocations INNER JOIN " & _
                        " tblListCity ON tblDefCompanyLocations.City = tblListCity.CityId INNER JOIN " & _
                        " tblDefCompanyLocationType ON tblDefCompanyLocations.LocationType = tblDefCompanyLocationType.LocationTypeId " & _
                        " where tblDefCompanyLocations.CompanyId=" & Me.cmbReference.ActiveRow.Cells(0).Value
                FillUltraDropDown(Me.cmbCompanyLocation, strSQL)
                Me.cmbCompanyLocation.Rows(0).Activate()
                Me.cmbCompanyLocation.DisplayLayout.Bands(0).Columns("LocationId").Hidden = True


                '// Loading company contacts
                strSQL = " select Pk_Id, ContactName, Designation, Mobile from TblCompanyContacts where RefCompanyId=" & Me.cmbReference.ActiveRow.Cells(0).Value
                FillUltraDropDown(Me.cmbContactPerson, strSQL)
                Me.cmbContactPerson.Rows(0).Activate()
                Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Pk_Id").Hidden = True
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtCustomerInquiryNo_Leave(sender As Object, e As EventArgs)
        'Try
        '    If Me.txtCustomerInquiryNo.Text <> CustomerInquiryNo Then
        '        If MasterDAL.ValidateCustomerInquiryNo(Me.txtCustomerInquiryNo.Text) = False Then
        '            msg_Error("Customer inquiry number is issued")
        '            'Me.txtCustomerInquiryNo.Text = ""
        '            Me.txtCustomerInquiryNo.Focus()
        '            Exit Sub
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtCustomerInquiryNo_KeyPress(sender As Object, e As KeyPressEventArgs)
        'Try
        '    If Me.txtCustomerInquiryNo.Text <> CustomerInquiryNo Then
        '        If MasterDAL.ValidateCustomerInquiryNo(Me.txtCustomerInquiryNo.Text) = False Then
        '            msg_Error("Customer inquiry number is already issued")
        '            'Me.txtCustomerInquiryNo.Text = ""
        '            Me.txtCustomerInquiryNo.Focus()
        '            Exit Sub
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtCustomerInquiryNo_TextChanged(sender As Object, e As EventArgs)
        'Try
        '    If Me.txtCustomerInquiryNo.Text <> CustomerInquiryNo Then
        '        If MasterDAL.ValidateCustomerInquiryNo(Me.txtCustomerInquiryNo.Text) = False Then
        '            msg_Error("Customer inquiry number is already issued")
        '            Me.txtCustomerInquiryNo.Text = ""
        '            Me.txtCustomerInquiryNo.Focus()
        '            Exit Sub
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
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

    Private Sub grdItems_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.CellUpdated
        Try
            'Me.grdItems.UpdateData()
            Dim dt As DataTable = CType(Me.grdItems.DataSource, DataTable)
            If e.Column.Index = 3 Then
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If dr.Item("SerialNo").ToString = Me.grdItems.GetRow.Cells("SerialNo").Value.ToString Then
                            Me.grdItems.GetRow.Cells("SerialNo").Value = SerialNo
                            SerialNo = ""
                            msg_Error("Duplicate Serial Number is not allowed.")
                        End If
                    Next

                End If
            End If
            'dtSelectedRows = grdItems.GetDataSource().AsEnumerable().ToList()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.CellValueChanged
        Try
            SerialNo = Me.grdItems.GetRow.Cells("SerialNo").Value.ToString
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

    Private Sub txtCustomerRequirement_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCustomerRequirement.KeyDown
        'Try
        '    If e.KeyCode = Keys.Enter Then
        '        Me.txtCustomerRequirement.Text.Replace(Environment.NewLine, "")
        '        Me.txtCustomerRequirement.AcceptsTab = False
        '        Me.txtCustomerRequirement.Focus()
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
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
                frm._VoucherId = Val(Me.grdSaved.GetRow.Cells("SalesInquiryId").Value.ToString)
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub LoadFromGrid()
        Dim dt As New DataTable
        Try
            SerialNo = Me.grdItems.GetRow.Cells("SerialNo").Value.ToString
            Me.txtCustomerRequirement.Text = Me.grdItems.GetRow.Cells("RequirementDescription").Value.ToString
            If Val(Me.grdItems.GetRow.Cells("ArticleId").Value.ToString) > 0 Then
                Me.cmbItem.Value = Val(Me.grdItems.GetRow.Cells("ArticleId").Value.ToString)
            End If
            If Val(Me.grdItems.GetRow.Cells("UnitId").Value.ToString) > 0 Then
                Me.cmbUnit.Value = Val(Me.grdItems.GetRow.Cells("UnitId").Value.ToString)
            End If
            If Val(Me.grdItems.GetRow.Cells("ItemTypeId").Value.ToString) > 0 Then
                Me.cmbType.Value = Val(Me.grdItems.GetRow.Cells("ItemTypeId").Value.ToString)
            End If
            If Val(Me.grdItems.GetRow.Cells("CategoryId").Value.ToString) > 0 Then
                Me.cmbCategory.Value = Val(Me.grdItems.GetRow.Cells("CategoryId").Value.ToString)
            End If
            If Val(Me.grdItems.GetRow.Cells("SubCategoryId").Value.ToString) > 0 Then
                Me.cmbSubCategory.Value = Val(Me.grdItems.GetRow.Cells("SubCategoryId").Value.ToString)
            End If
            If Val(Me.grdItems.GetRow.Cells("OriginId").Value.ToString) > 0 Then
                Me.cmbOrigin.Value = Val(Me.grdItems.GetRow.Cells("OriginId").Value.ToString)
            End If
            Me.txtQty.Text = Val(Me.grdItems.GetRow.Cells("Qty").Value.ToString)
            Me.txtComments.Text = Me.grdItems.GetRow.Cells("Comments").Value.ToString
            IsDetailEdit = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub AddEditGrid()
        Dim dt As New DataTable
        Try
            Me.grdItems.GetRow.Cells("SerialNo").Value = SerialNo
            SerialNo = ""
            Me.grdItems.GetRow.Cells("RequirementDescription").Value = Me.txtCustomerRequirement.Text
            If Me.cmbItem.Value > 0 Then
                Me.grdItems.GetRow.Cells("ArticleId").Value = Me.cmbItem.Value
                Me.grdItems.GetRow.Cells("Code").Value = Me.cmbItem.ActiveRow.Cells("Code").Value.ToString
                Me.grdItems.GetRow.Cells("ArticleDescription").Value = Me.cmbItem.ActiveRow.Cells("Item").Value.ToString
            Else
                Me.grdItems.GetRow.Cells("ArticleId").Value = 0
                Me.grdItems.GetRow.Cells("Code").Value = ""
                Me.grdItems.GetRow.Cells("ArticleDescription").Value = ""
            End If
            If Not Me.cmbUnit.ActiveRow Is Nothing AndAlso Me.cmbUnit.Value > 0 Then
                Me.grdItems.GetRow.Cells("UnitId").Value = Me.cmbUnit.Value
                Me.grdItems.GetRow.Cells("Unit").Value = Me.cmbUnit.ActiveRow.Cells("Name").Value.ToString
            End If
            If Not Me.cmbType.ActiveRow Is Nothing AndAlso Me.cmbType.Value > 0 Then
                Me.grdItems.GetRow.Cells("ItemTypeId").Value = Me.cmbType.Value
                Me.grdItems.GetRow.Cells("Type").Value = Me.cmbType.ActiveRow.Cells("Name").Value.ToString
            End If
            If Not Me.cmbCategory.ActiveRow Is Nothing AndAlso Me.cmbCategory.Value > 0 Then
                Me.grdItems.GetRow.Cells("CategoryId").Value = Me.cmbCategory.Value
                Me.grdItems.GetRow.Cells("Category").Value = Me.cmbCategory.ActiveRow.Cells("Name").Value.ToString
            End If
            If Not Me.cmbSubCategory.ActiveRow Is Nothing AndAlso Me.cmbSubCategory.Value > 0 Then
                Me.grdItems.GetRow.Cells("SubCategoryId").Value = Me.cmbSubCategory.Value
                Me.grdItems.GetRow.Cells("SubCategory").Value = Me.cmbSubCategory.ActiveRow.Cells("Name").Value.ToString
            End If
            If Not Me.cmbOrigin.ActiveRow Is Nothing AndAlso Me.cmbOrigin.Value > 0 Then
                Me.grdItems.GetRow.Cells("OriginId").Value = Me.cmbOrigin.Value
                Me.grdItems.GetRow.Cells("Origin").Value = Me.cmbOrigin.ActiveRow.Cells("Name").Value.ToString
            End If
            Me.grdItems.GetRow.Cells("Qty").Value = Me.txtQty.Text
            Me.grdItems.GetRow.Cells("Comments").Value = Me.txtComments.Text
            IsDetailEdit = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdItems_DoubleClick(sender As Object, e As EventArgs) Handles grdItems.DoubleClick
        Try
            LoadFromGrid()
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

    Private Sub btnSearchDocument_Click(sender As Object, e As EventArgs) Handles btnSearchDocument.Click
        Try
            If Me.SplitContainer1.Panel1Collapsed = True Then
                Me.SplitContainer1.Panel1Collapsed = False
            Else
                Me.SplitContainer1.Panel1Collapsed = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetSearch()
        Try
            Dim dt As New DataTable
            Dim str As String = String.Empty
            str = "SELECT     SalesInquiry.SalesInquiryId, SalesInquiry.SalesInquiryNo, SalesInquiry.SalesInquiryDate, ISNULL(COA.coa_detail_id, 0) AS CustomerId, COA.detail_code AS Code, COA.detail_title AS Customer, SalesInquiry.LocationId, SalesInquiry.ContactPersonId, SalesInquiry.CustomerInquiryNo, SalesInquiry.IndentNo, SalesInquiry.IndentDepartment, SalesInquiry.CustomerInquiryDate, SalesInquiry.OldInquiryNo, SalesInquiry.DueDate, SalesInquiry.OldInquiryDate, SalesInquiry.Remarks, SalesInquiry.UserName, Doc_Att.NoOfAttachment AS [No Of Attachments] FROM SalesInquiryMaster AS SalesInquiry LEFT OUTER JOIN SalesInquiryDetail ON SalesInquiry.SalesInquiryId = SalesInquiryDetail.SalesInquiryId LEFT OUTER JOIN vwCOADetail AS COA ON SalesInquiry.CustomerId = COA.coa_detail_id LEFT OUTER JOIN (SELECT     COUNT(*) AS NoOfAttachment, DocId FROM DocumentAttachment GROUP BY DocId, Source) AS Doc_Att ON Doc_Att.DocId = SalesInquiry.SalesInquiryId Where SalesInquiry.SalesInquiryId <> 0 "
            If Me.dtpFrom.Checked = True Then
                str += " AND SalesInquiry.SalesInquiryDate >= Convert(Datetime, N'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
            End If
            If Me.dtpTo.Checked = True Then
                str += " AND SalesInquiry.SalesInquiryDate <= Convert(Datetime, N'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
            End If
            If Me.txtSearchInquiryNo.Text <> String.Empty Then
                str += " AND SalesInquiry.SalesInquiryNo LIKE '%" & Me.txtSearchInquiryNo.Text & "%'"
            End If
            If Me.txtSearchCustInquiryNo.Text <> String.Empty Then
                str += " AND SalesInquiry.CustomerInquiryNo LIKE '%" & Me.txtSearchCustInquiryNo.Text & "%'"
            End If
            If Me.cmbSearchCustomer.Value > 1 Then
                str += " AND COA.coa_detail_id = " & Me.cmbSearchCustomer.Value & ""
            End If
            If Me.txtSeachItemDescription.Text <> String.Empty Then
                str += " AND SalesInquiryDetail.RequirementDescription LIKE '%" & Me.txtSeachItemDescription.Text & "%'"
            End If
            str += " GROUP BY SalesInquiry.SalesInquiryId, SalesInquiry.SalesInquiryNo, SalesInquiry.SalesInquiryDate, COA.coa_detail_id, COA.detail_code, COA.detail_title , SalesInquiry.LocationId, SalesInquiry.ContactPersonId, SalesInquiry.CustomerInquiryNo, SalesInquiry.IndentNo, SalesInquiry.IndentDepartment, SalesInquiry.CustomerInquiryDate, SalesInquiry.OldInquiryNo, SalesInquiry.DueDate, SalesInquiry.OldInquiryDate, SalesInquiry.Remarks, SalesInquiry.UserName, Doc_Att.NoOfAttachment ORDER BY SalesInquiry.SalesInquiryDate DESC"
            dt = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            GetSearch()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click

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


    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs)
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdItems.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdItems.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grdItems.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Inquiry"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdItems.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdItems.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grdItems.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Inquiry"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load_1(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Inquiry"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbReference_Leave1(sender As Object, e As EventArgs) Handles cmbReference.Leave
        Try
            FillCombos("ContactPerson")
            FillCombos("Location")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS4456
    ''' </summary>
    ''' <param name="Activity"></param>
    ''' <remarks></remarks>

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
    Private Sub GetAutoEmailData()
        'dtEmail = New DataTable
        Dim Dr As DataRow
        Try
            For Each Row As Janus.Windows.GridEX.GridEXRow In grdItems.GetRows
                Dr = dtEmail.NewRow
                For Each col As String In AllFields
                    If Row.Table.Columns.Contains(col) Then
                        Dr.Item(col) = Row.Cells(col).Value.ToString
                    End If
                Next
                dtEmail.Rows.Add(Dr)
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
            html.Append("<tr>")
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
            'string[] textLines = text.Split(new[]{ Environment.NewLine }, StringSplitOptions.None);
            'var result = input.Split(System.Environment.NewLine.ToCharArray());
            'Building the Data rows.
            For Each row As DataRow In dt.Rows
                html.Append("<tr>")
                For Each column As DataColumn In dt.Columns
                    html.Append("<td>")
                    If column.ColumnName = "RequirementDescription" Then
                        Dim var = row(column.ColumnName).ToString.Split(System.Environment.NewLine.ToCharArray())
                        Dim Lines As String = ""
                        For Each Line As String In var
                            'Dim Line1 As String = Line.Replace(" ", "")
                            If Line.Length > 0 Then
                                If Lines.Length > 0 Then
                                    Lines += "<br/>" & Line
                                Else
                                    Lines = Line
                                End If
                            End If
                        Next
                        html.Append(Lines)
                    Else
                        html.Append(row(column.ColumnName))
                    End If
                    html.Append("</td>")
                Next
                html.Append("</tr>")
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
    Private Sub SaveCCBCC(ByVal CC As String, ByVal BCC As String)
        Try
            Dim EmailList As New List(Of CCBCCEmails)
            Dim Email As CCBCCEmails
            'Dim CC As String = Me.txtCC.Text
            'Dim BCC As String = Me.txtBCC.Text
            Dim arCC() As String = CC.Split(";")
            Dim arBCC() As String = BCC.Split(";")
            If arCC.Length > 0 Then
                For Each c As String In arCC
                    If c.Length > 0 Then
                        Email = New CCBCCEmails
                        Email.EmailAddress = c
                        EmailList.Add(Email)
                    End If
                Next
            End If
            If arBCC.Length > 0 Then
                For Each b As String In arBCC
                    If b.Length > 0 Then
                        Email = New CCBCCEmails
                        Email.EmailAddress = b
                        EmailList.Add(Email)
                    End If
                Next
            End If
            'If EmailList.Count > 0 Then
            '    MasterDAL.AddCCBCCEmails(EmailList)
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''Aashir Added code and name fileter on item 
    'start
    Private Sub rdoCode_CheckedChanged(sender As Object, e As EventArgs) Handles RdoCode.CheckedChanged
        If IsFormLoaded = False Then Exit Sub
        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
        Me.cmbItem.Rows(0).Activate()
    End Sub

    Private Sub rdoName_CheckedChanged(sender As Object, e As EventArgs) Handles rdoName.CheckedChanged
        If isFormLoaded = False Then Exit Sub
        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
        Me.cmbItem.Rows(0).Activate()
    End Sub
    'end
End Class
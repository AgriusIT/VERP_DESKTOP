'' TASK TFS2375 Ayesha Rehman on 26-02-2018 Approval Hierarchy for All Transactional documents 
''TFS2988 Ayesha Rehman : 09-04-2018 : If document is approved from one stage then it should not change able for previous stage
''TFS2989 Ayesha Rehman : 10-04-2018 : If document is rejected from one stage then it should open for previous stage for approval
''TFS3239 Ayesha Rehman : 3-05-2018 : Send email to different vendors with multiple outlook
''TFS4655 Ayesha Rehman : 01-10-2018 : Vendor Searching by Name or Other Comments based on Purchase Inquiry.
''TFS4663 duplicate vendor should not be allowed. Done by Muhammad Amin on 04-10-2018
Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Imports System.Net.Mail
Imports System.Reflection
Imports Microsoft.Office.Interop
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.IO

Public Class frmPurchaseInquiry
    Implements IGeneral
    Dim objModel As PurchaseInquiryDetail
    Dim objVendors As PurchaseInquiryVendors
    Dim Master As PurchaseInquiryMaster
    Dim PurchaseInquiryId As Integer = 0
    Dim MasterDAL As New PurchaseInquiryDAL
    Dim DetailDAL As New PurchaseInquiryDetailDAL
    Dim VendorsDAL As New PurchaseInquiryVendorsDAL
    Dim IsFormLoaded As Boolean = False ''TFS2648
    Dim SerialNo As String = String.Empty
    Dim EmailDAL As New EmailTemplateDAL
    Dim html As StringBuilder
    Dim VendorEmails As String = String.Empty
    Dim dtEmail As DataTable
    Dim AllFields As List(Of String)
    Dim EmailTemplate As String = String.Empty
    Dim arrFile As List(Of String)
    Dim objPath As String = String.Empty
    Dim AfterFieldsElement As String = String.Empty
    Dim lbox As ListBox
    Dim WithEvents lv As LVX
    Dim CC As String = ""
    Dim BCC As String = ""
    ''TFS2375 : Ayesha Rehman : This Variable is Added to check the document state ,if it is in Eidt mode or not
    Dim IsEditMode As Boolean = False
    Dim ApprovalProcessId As Integer = 0
    ''TFS2375 : Ayesha Rehman :End
    Dim Posted As Boolean = False
    Dim Posted_Username As String = ""
    Dim PostedDate As DateTime = Date.Now
    Dim AutoEmail As Boolean = False
    Dim EmailBody As String = String.Empty
    Dim IsEmailSent As Boolean = False
    Enum Detail
        PurchaseInquiryDetailId
        PurchaseInquiryId
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
        ReferenceNo
        Comments
        SalesInquiryId
        SalesInquiryDetailId
        'Detail.PurchaseInquiryDetailId,  Detail.PurchaseInquiryId, Detail.RequirementDescription,  Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription,  Detail.UnitId, Unit.PackName As Unit,  Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, IsNull( Detail.Qty, 0) As Qty, Detail.ReferenceNo, Detail.Comments
    End Enum
    Enum Vendors
        PurchaseInquiryVendorsId
        PurchaseInquiryId
        VendorId
        VendorName
        Email
    End Enum

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            frmSearchSalesInquiry.ShowDialog()
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
            ''Start TFS2988
            If IsEditMode = True Then
                If ValidateApprovalProcessMapped(Me.uitxtName.Text.Trim) Then
                    If ValidateApprovalProcessInProgress(Me.uitxtName.Text.Trim) Then
                        msg_Error("Document is in Approval Process") : Exit Function
                    End If
                End If
            End If
            ''End TFS2988
            FillModel()
            MasterDAL.Delete(Master, LoginGroupId)
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
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email, vwCOADetail.Comments, vwCOADetail.brand As [Other Comments] " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Vendor', 'Customer') and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbReference, strSQL)
                    Me.cmbReference.Rows(0).Activate()
                    Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email, vwCOADetail.Comments, vwCOADetail.brand As [Other Comments] " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type = 'Vendor' and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbReference, strSQL)
                    If Me.cmbReference.DisplayLayout.Bands.Count > 0 Then
                        Me.cmbReference.Rows(0).Activate()
                        Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                    End If
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

            ElseIf Condition = "IndentDepartment" Then
                strSQL = "SELECT Distinct IndentingDepartment, IndentingDepartment " & _
                    " FROM PurchaseInquiryMaster"
                FillDropDown(Me.cmbIndentDept, strSQL)
            ElseIf Condition = "CC" Then
                ''filling Unit combo
                strSQL = "Select Distinct EmailAddress, EmailAddress As [Email Address] From CCBCCEmailsTable"
                'strSQL = "Select Distinct ArticlePackId, PackName As Name, PackQty, ArticleMasterId From ArticleDefPackTable order by ArticlePackId"
                FillUltraDropDown(Me.cmbCC, strSQL)
                If Me.cmbCC.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbCC.Rows(0).Activate()
                    Me.cmbCC.DisplayLayout.Bands(0).Columns("EmailAddress").Hidden = True
                    Me.cmbCC.DisplayLayout.Bands(0).Columns("Email Address").Width = 400
                End If
            ElseIf Condition = "BCC" Then
                ''filling Unit combo
                strSQL = "Select Distinct EmailAddress, EmailAddress As [Email Address] From CCBCCEmailsTable"
                'strSQL = "Select Distinct ArticlePackId, PackName As Name, PackQty, ArticleMasterId From ArticleDefPackTable order by ArticlePackId"
                FillUltraDropDown(Me.cmbBCC, strSQL)
                If Me.cmbBCC.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbBCC.Rows(0).Activate()
                    Me.cmbBCC.DisplayLayout.Bands(0).Columns("EmailAddress").Hidden = True
                    Me.cmbBCC.DisplayLayout.Bands(0).Columns("Email Address").Width = 400
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
            FillCombos("CC")
            FillCombos("BCC")
            'FillCombos("ContactPerson")
            'FillCombos("Location")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Me.grdItems.UpdateData()
            Me.grdVendors.UpdateData()
            Master = New PurchaseInquiryMaster()
            Master.PurchaseInquiryId = PurchaseInquiryId
            Master.PurchaseInquiryNo = Me.uitxtName.Text
            Master.PurchaseInquiryDate = Me.dtpInquiryDate.Value
            Master.IndentNo = Me.txtIndentNo.Text
            If Not getConfigValueByType("PurchaseInquiryApproval") = "Error" Then
                ApprovalProcessId = getConfigValueByType("PurchaseInquiryApproval")
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
            'Master.IndentingDepartment = Me.cmbIndentDept.Text
            If Me.cmbIndentDept.Text.Contains(".... Select any Value ....") = False AndAlso Me.cmbIndentDept.Text.Contains("Select any value") = False Then
                Master.IndentingDepartment = Me.cmbIndentDept.Text
            Else
                Master.IndentingDepartment = ""
            End If
            'Master.PurchaseInquiryDate = Me.dtp.Value
            Master.OldInquiryNo = Me.txtOldInquiryNo.Text
            Master.DueDate = Me.dtpDueDate.Value
            'Master.OldInquiryDate = Me.dtpOldInquiryDate.Value
            If Me.dtpOldInquiryDate.Checked = True Then
                Master.OldInquiryDate = Me.dtpOldInquiryDate.Value
            Else
                Master.OldInquiryDate = DateTime.MinValue
            End If
            Master.Remarks = Me.txtRemarks.Text
            Master.UserName = LoginUserName
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetRows
                objModel = New PurchaseInquiryDetail()
                objModel.PurchaseInquiryDetailId = Val(Row.Cells("PurchaseInquiryDetailId").Value.ToString)
                objModel.PurchaseInquiryId = Val(Row.Cells("PurchaseInquiryId").Value.ToString)
                objModel.SerialNo = Row.Cells("SerialNo").Value.ToString
                objModel.RequirementDescription = ReplaceNewLine(Row.Cells("RequirementDescription").Value.ToString, False)
                objModel.ArticleId = Val(Row.Cells("ArticleId").Value.ToString)
                objModel.UnitId = Val(Row.Cells("UnitId").Value.ToString)
                objModel.ItemTypeId = Val(Row.Cells("ItemTypeId").Value.ToString)
                objModel.OriginId = Val(Row.Cells("OriginId").Value.ToString)
                objModel.CategoryId = Val(Row.Cells("CategoryId").Value.ToString)
                objModel.SubCategoryId = Val(Row.Cells("SubCategoryId").Value.ToString)
                objModel.Qty = Val(Row.Cells("Qty").Value.ToString)
                objModel.ReferenceNo = Val(Row.Cells("ReferenceNo").Value.ToString)
                objModel.Comments = Row.Cells("Comments").Value.ToString
                objModel.SalesInquiryId = Val(Row.Cells("SalesInquiryId").Value.ToString)
                objModel.SalesInquiryDetailId = Val(Row.Cells("SalesInquiryDetailId").Value.ToString)
                Master.DetailList.Add(objModel)
            Next
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdVendors.GetRows
                objVendors = New PurchaseInquiryVendors()
                objVendors.PurchaseInquiryVendorsId = Val(Row.Cells("PurchaseInquiryVendorsId").Value.ToString)
                objVendors.PurchaseInquiryId = Val(Row.Cells("PurchaseInquiryId").Value.ToString)
                objVendors.VendorId = Val(Row.Cells("VendorId").Value.ToString)
                objVendors.Email = Row.Cells("Email").Value.ToString
                Master.VendorsList.Add(objVendors)
            Next
            If Me.grdItems.RowCount > 0 Then
                Master.SalesInquiryId = Val(grdItems.GetRow.Cells("SalesInquiryId").Value.ToString)
            Else
                Master.SalesInquiryId = Val(0)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If LoginUserName = "Admin" Or LoginUserName = "Administrator" Then
                Me.grdSaved.DataSource = New PurchaseInquiryDAL().GetAllRecords(Me.Name)
            Else
                Me.grdSaved.DataSource = New PurchaseInquiryDAL().GetAllRecords(Me.Name, LoginUserName)
            End If
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("PurchaseInquiryId").Visible = False
            Me.grdSaved.RootTable.Columns("PurchaseInquiryNo").Caption = "Inquiry No"
            Me.grdSaved.RootTable.Columns("PurchaseInquiryDate").Caption = "Inquiry Date"
            Me.grdSaved.RootTable.Columns("IndentNo").Caption = "Indent No"
            Me.grdSaved.RootTable.Columns("IndentingDepartment").Caption = "Indent Department"
            Me.grdSaved.RootTable.Columns("OldInquiryNo").Caption = "Old Inquiry No"
            Me.grdSaved.RootTable.Columns("DueDate").Caption = "Due Date"
            Me.grdSaved.RootTable.Columns("OldInquiryDate").Caption = "Old Inquiry Date"
            Me.grdSaved.RootTable.Columns("UserName").Caption = "User Name"
            Me.grdSaved.RootTable.Columns("PurchaseInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("DueDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("OldInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("No Of Attachments").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdSaved.RootTable.Columns("SalesInquiryNo").Caption = "Sales Inquiry No"

            ''TASK TFS4454
            Me.grdSaved.RootTable.Columns("Email Status").Width = 70
            Me.grdSaved.RootTable.Columns("Email Sent").Width = 70
            Me.grdSaved.RootTable.Columns("Email Status").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Email Sent").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdSaved.RootTable.Columns("Email Sent").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Email Sent").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            If Me.grdSaved.RootTable.Columns.Contains("Email") = False Then
                Me.grdSaved.RootTable.Columns.Add("Email").EditType = Janus.Windows.GridEX.EditType.NoEdit
                Me.grdSaved.RootTable.Columns("Email").Key = "Email"
                Me.grdSaved.RootTable.Columns("Email").Caption = "Action"
                Me.grdSaved.RootTable.Columns("Email").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdSaved.RootTable.Columns("Email").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grdSaved.RootTable.Columns("Email").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdSaved.RootTable.Columns("Email").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdSaved.RootTable.Columns("Email").ButtonText = "Email"
                Me.grdSaved.RootTable.Columns("Email").Width = 60

            End If
            ''END TASK TFS4454
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetTop50(Optional Condition As String = "")
        Try
            Me.grdSaved.DataSource = New PurchaseInquiryDAL().GetTop50(Me.Name)
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("PurchaseInquiryId").Visible = False
            Me.grdSaved.RootTable.Columns("PurchaseInquiryNo").Caption = "Inquiry No"
            Me.grdSaved.RootTable.Columns("PurchaseInquiryDate").Caption = "Inquiry Date"
            Me.grdSaved.RootTable.Columns("IndentNo").Caption = "Indent No"
            Me.grdSaved.RootTable.Columns("IndentingDepartment").Caption = "Indent Department"
            Me.grdSaved.RootTable.Columns("OldInquiryNo").Caption = "Old Inquiry No"
            Me.grdSaved.RootTable.Columns("DueDate").Caption = "Due Date"
            Me.grdSaved.RootTable.Columns("OldInquiryDate").Caption = "Old Inquiry Date"
            Me.grdSaved.RootTable.Columns("UserName").Caption = "User Name"
            Me.grdSaved.RootTable.Columns("PurchaseInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("DueDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("OldInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("No Of Attachments").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdSaved.RootTable.Columns("SalesInquiryNo").Caption = "Sales Inquiry No"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAll(Optional Condition As String = "")
        Try
            Me.grdSaved.DataSource = New PurchaseInquiryDAL().GetAll(Me.Name)
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("PurchaseInquiryId").Visible = False
            Me.grdSaved.RootTable.Columns("PurchaseInquiryNo").Caption = "Inquiry No"
            Me.grdSaved.RootTable.Columns("PurchaseInquiryDate").Caption = "Inquiry Date"
            Me.grdSaved.RootTable.Columns("IndentNo").Caption = "Indent No"
            Me.grdSaved.RootTable.Columns("IndentingDepartment").Caption = "Indent Department"
            Me.grdSaved.RootTable.Columns("OldInquiryNo").Caption = "Old Inquiry No"
            Me.grdSaved.RootTable.Columns("DueDate").Caption = "Due Date"
            Me.grdSaved.RootTable.Columns("OldInquiryDate").Caption = "Old Inquiry Date"
            Me.grdSaved.RootTable.Columns("UserName").Caption = "User Name"
            Me.grdSaved.RootTable.Columns("PurchaseInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("DueDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("OldInquiryDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("No Of Attachments").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdSaved.RootTable.Columns("SalesInquiryNo").Caption = "Sales Inquiry No"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Not grdVendors.RowCount > 0 Then
                msg_Error("Vender grid is empty. At least one vendor is required")
                Me.grdVendors.Focus() : IsValidate = False : Exit Function
            End If
            If Not grdItems.RowCount > 0 Then
                msg_Error("Items grid is empty")
                Me.grdItems.Focus() : IsValidate = False : Exit Function
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
                Me.cmbReference.Focus() : IsValidateAddToGrid = False : Exit Function
            End If
            If cmbItem.Value = 0 AndAlso Me.txtRequirementDescription.Text = "" Then
                msg_Error("An item is required")
                Me.cmbItem.Focus() : IsValidateAddToGrid = False : Exit Function
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidateToAddVendor(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean
        Try
            If cmbReference.Value = 0 Then
                msg_Error("Vendor is required")
                Me.cmbItem.Focus() : IsValidateToAddVendor = False : Exit Function
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
            IsEditMode = False  ''TFS3087
            Me.txtOldInquiryNo.Text = ""
            Me.txtIndentNo.Text = ""
            Me.txtRemarks.Text = ""
            Me.txtComments.Text = ""
            Me.BtnSave.Text = "&Save"
            Me.cmbIndentDept.Text = "Select any value"
            Me.txtCC.Text = ""
            Me.txtBCC.Text = ""
            Me.dtpInquiryDate.Value = Now
            Me.dtpDueDate.Value = Now
            Me.dtpOldInquiryDate.Checked = False
            If cmbReference.ActiveRow IsNot Nothing Then
                Me.cmbReference.Rows(0).Activate()
            End If
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
            DisplayDetail(-1)
            DisplayVendors(-1)
            ResetDetailControls()
            'FillCCBCCEmails()
            'Ali Faisal : TFS2014 : Add demand load button config based
            If getConfigValueByType("POFromCS").ToString = "True" Then
                Me.btnLoadDemand.Visible = True
            Else
                Me.btnLoadDemand.Visible = False
            End If
            'Ali Faisal : TFS2014 : End

            ''TASK TFS2383

            ''TASK TFS4437
            If getConfigValueByType("AutoEmail").ToString = "True" Then
                AutoEmail = True
            Else
                AutoEmail = False
            End If
            ''END TASK TFS4437
            EmailBody = String.Empty
            If getConfigValueByType("ShowIdentRelatedFields").ToString = "True" Then
                Me.cmbIndentDept.Visible = True
                Me.cmbIndentDept.Location = New Point(635, 17)
                Me.txtIndentNo.Visible = True
                Me.txtIndentNo.Location = New Point(480, 17)
                Me.Label3.Visible = True
                Me.Label3.Location = New Point(477, 1)
                Me.Label9.Visible = True
                Me.Label9.Location = New Point(632, 1)
                ''
                Label12.Location = New Point(805, 1)
                Label13.Location = New Point(961, 1)
                Me.txtOldInquiryNo.Location = New Point(813, 17)
                Me.dtpOldInquiryDate.Location = New Point(968, 17)
            Else
                'Me.cmbIndentDept.Location = New Point(545, 17)
                'Me.txtIndentNo.Location = New Point(410, 17)
                'Me.Label3.Location = New Point(407, 1)
                'Me.Label9.Location = New Point(542, 1)
                Label12.Location = New Point(475, 1)
                Label13.Location = New Point(650, 1)
                Me.txtOldInquiryNo.Location = New Point(478, 17)
                Me.dtpOldInquiryDate.Location = New Point(633, 17)
                Me.Label3.Location = New Point(694, 1)
                Me.txtIndentNo.Location = New Point(697, 1)
                Me.Label9.Location = New Point(829, 1)
                Me.cmbIndentDept.Location = New Point(847, 17)

                Me.cmbIndentDept.Visible = False
                Me.txtIndentNo.Visible = False
                Me.Label3.Visible = False
                Me.Label9.Visible = False
            End If
            ''END TASK TFS2383

            'Ayesha Rehman : TFS2375 : Enable Approval History button only in Eidt Mode
            If IsEditMode = True Then
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            Else
                Me.btnApprovalHistory.Visible = False
            End If
            'Ayesha Rehman : TFS2375 : End
            IsEmailSent = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ResetDetailControls()
        Try
            Me.txtRequirementDescription.Text = ""
            Me.cmbItem.Rows(0).Activate()
            Me.cmbUnit.Rows(0).Activate()
            Me.cmbType.Rows(0).Activate()
            Me.cmbCategory.Rows(0).Activate()
            Me.cmbSubCategory.Rows(0).Activate()
            Me.cmbOrigin.Rows(0).Activate()
            Me.txtQty.Text = "Qty"
            Me.txtComments.Text = "Comments"
            Me.txtReferenceNo.Text = "Reference No"
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
            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
            IsEditMode = True
            If Not getConfigValueByType("PurchaseInquiryApproval") = "Error" Then
                ApprovalProcessId = getConfigValueByType("PurchaseInquiryApproval")
            End If
            If ApprovalProcessId = 0 Then
                Me.btnApprovalHistory.Visible = False
                Me.btnApprovalHistory.Enabled = False
            Else
                Me.btnApprovalHistory.Visible = True
                Me.btnApprovalHistory.Enabled = True
            End If
            ''Ayesha Rehman :TFS2375 :End
            Me.uitxtName.Text = Me.grdSaved.CurrentRow.Cells("PurchaseInquiryNo").Value.ToString
            Me.txtOldInquiryNo.Text = Me.grdSaved.CurrentRow.Cells("OldInquiryNo").Value.ToString
            Me.txtIndentNo.Text = Me.grdSaved.CurrentRow.Cells("IndentNo").Value.ToString
            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells("Remarks").Value.ToString
            If IsDBNull(Me.grdSaved.CurrentRow.Cells("PurchaseInquiryDate").Value) Then
                Me.dtpInquiryDate.Value = Now
            Else
                Me.dtpInquiryDate.Value = Me.grdSaved.CurrentRow.Cells("PurchaseInquiryDate").Value
            End If
            If IsDBNull(Me.grdSaved.CurrentRow.Cells("OldInquiryDate").Value) Then
                Me.dtpOldInquiryDate.Checked = False
                'Me.dtpOldInquiryDate.Value = Now
            Else
                Me.dtpOldInquiryDate.Checked = True
                Me.dtpOldInquiryDate.Value = Me.grdSaved.CurrentRow.Cells("OldInquiryDate").Value
            End If
            If IsDBNull(Me.grdSaved.CurrentRow.Cells("DueDate").Value) Then
                Me.dtpDueDate.Value = Now
            Else
                Me.dtpDueDate.Value = Me.grdSaved.CurrentRow.Cells("DueDate").Value
            End If
            Me.cmbIndentDept.Text = Me.grdSaved.CurrentRow.Cells("IndentingDepartment").Value.ToString
            PurchaseInquiryId = Val(Me.grdSaved.CurrentRow.Cells("PurchaseInquiryId").Value.ToString)
            ''Start TFS3087 : Ayesha Rehman : 20-04-2018
            Posted = CBool(Me.grdSaved.CurrentRow.Cells("Posted").Value.ToString)
            Posted_Username = Me.grdSaved.CurrentRow.Cells("Posted_Username").Value.ToString
            'PostedDate = Me.grdSaved.CurrentRow.Cells("PostedDate").Value
            If IsDBNull(Me.grdSaved.CurrentRow.Cells("PostedDate").Value) Then
                PostedDate = Date.Now
            Else
                PostedDate = Me.grdSaved.CurrentRow.Cells("PostedDate").Value
            End If
            ''End TFS3087 
            Me.btnAttachment.Text = "Attachment (" & Me.grdSaved.GetRow.Cells("No Of Attachments").Value.ToString & ")"
            DisplayDetail(PurchaseInquiryId)
            DisplayVendors(PurchaseInquiryId)
            Me.BtnSave.Text = "&Update"
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' TFS2648 : Ayesha Rehman : This function is added to get the document in edit mode when open from anotherside
    ''' </summary>
    ''' <param name="PurchaseInquiryNo"></param>
    ''' <returns></returns>
    ''' <remarks>TFS2648 : Ayesha Rehman : 07-03-2018</remarks>
    Public Function Get_All(ByVal PurchaseInquiryNo As String)
        Try
            Get_All = Nothing
            If IsFormLoaded = True Then
                If PurchaseInquiryNo.Length > 0 Then
                    Dim str As String = "Select * from PurchaseInquiryMaster where PurchaseInquiryNo =N'" & PurchaseInquiryNo & "'"
                    Dim dt As DataTable = GetDataTable(str)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then
                            GetAllRecords()
                            ''This LOC is Added to check if Record exist agianst the login user
                            If Not Me.grdSaved.RecordCount > 0 Then Exit Function
                            Dim flag As Boolean = False
                            flag = Me.grdSaved.FindAll(Me.grdSaved.RootTable.Columns("PurchaseInquiryNo"), Janus.Windows.GridEX.ConditionOperator.Equal, PurchaseInquiryNo)

                            ''Ayesha Rehman :TFS2375 :Making Approval Button Enable in Edit Mode
                            IsEditMode = True
                            If Not getConfigValueByType("PurchaseInquiryApproval") = "Error" Then
                                ApprovalProcessId = getConfigValueByType("PurchaseInquiryApproval")
                            End If
                            If ApprovalProcessId = 0 Then
                                Me.btnApprovalHistory.Visible = False
                                Me.btnApprovalHistory.Enabled = False
                            Else
                                Me.btnApprovalHistory.Visible = True
                                Me.btnApprovalHistory.Enabled = True
                            End If
                            ''Ayesha Rehman :TFS2375 :End
                            Me.uitxtName.Text = Me.grdSaved.CurrentRow.Cells("PurchaseInquiryNo").Value.ToString
                            Me.txtOldInquiryNo.Text = Me.grdSaved.CurrentRow.Cells("OldInquiryNo").Value.ToString
                            Me.txtIndentNo.Text = Me.grdSaved.CurrentRow.Cells("IndentNo").Value.ToString
                            Me.txtRemarks.Text = Me.grdSaved.CurrentRow.Cells("Remarks").Value.ToString
                            If IsDBNull(Me.grdSaved.CurrentRow.Cells("PurchaseInquiryDate").Value) Then
                                Me.dtpInquiryDate.Value = Now
                            Else
                                Me.dtpInquiryDate.Value = Me.grdSaved.CurrentRow.Cells("PurchaseInquiryDate").Value
                            End If
                            If IsDBNull(Me.grdSaved.CurrentRow.Cells("OldInquiryDate").Value) Then
                                Me.dtpOldInquiryDate.Checked = False
                                'Me.dtpOldInquiryDate.Value = Now
                            Else
                                Me.dtpOldInquiryDate.Checked = True
                                Me.dtpOldInquiryDate.Value = Me.grdSaved.CurrentRow.Cells("OldInquiryDate").Value
                            End If
                            If IsDBNull(Me.grdSaved.CurrentRow.Cells("DueDate").Value) Then
                                Me.dtpDueDate.Value = Now
                            Else
                                Me.dtpDueDate.Value = Me.grdSaved.CurrentRow.Cells("DueDate").Value
                            End If
                            Me.cmbIndentDept.Text = Me.grdSaved.CurrentRow.Cells("IndentingDepartment").Value.ToString
                            PurchaseInquiryId = Val(Me.grdSaved.CurrentRow.Cells("PurchaseInquiryId").Value.ToString)
                            Me.btnAttachment.Text = "Attachment (" & Me.grdSaved.GetRow.Cells("No Of Attachments").Value.ToString & ")"
                            DisplayDetail(PurchaseInquiryId)
                            DisplayVendors(PurchaseInquiryId)
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
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("RFQ-" + Microsoft.VisualBasic.Right(Me.dtpInquiryDate.Value.Year, 2) + "-", "PurchaseInquiryMaster", "PurchaseInquiryNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("RFQ-" & Format(Me.dtpInquiryDate.Value, "yy") & Me.dtpInquiryDate.Value.Month.ToString("00"), 4, "PurchaseInquiryMaster", "PurchaseInquiryNo")
            Else
                Return GetNextDocNo("RFQ-", 6, "PurchaseInquiryMaster", "PurchaseInquiryNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Master.PurchaseInquiryNo = GetDocumentNo()
            Dim PurchaseInquiryId As Integer
            MasterDAL.Add(Master, Me.Name, objPath, arrFile, LoginGroupId, LoginUserId, PurchaseInquiryId)
            SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Purchase, Me.uitxtName.Text.Trim, True)
            ''Start TFS2375
            ''insert Approval Log
            SaveApprovalLog(EnumReferenceType.PurchaseInquiry, PurchaseInquiryId, Me.uitxtName.Text.Trim, Me.dtpInquiryDate.Value.Date, "Purchase Inquiry ", Me.Name)
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
            MasterDAL.Update(Master, Me.Name, objPath, arrFile, LoginGroupId)
            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, Me.uitxtName.Text.Trim, True)
            ''Start TFS2989
            If ValidateApprovalProcessMapped(Me.uitxtName.Text.Trim, Me.Name) Then
                If ValidateApprovalProcessIsInProgressAgain(Me.uitxtName.Text.Trim, Me.Name) = False Then
                    SaveApprovalLog(EnumReferenceType.PurchaseInquiry, PurchaseInquiryId, Me.uitxtName.Text.Trim, Me.dtpInquiryDate.Value.Date, "Purchase Inquiry ", Me.Name)
                End If
            End If
            ''End TFS2989
            msg_Information("Record has been updated successfully.")
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            GetAllRecords()
            FillAllCombos()
            ReSetControls()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    'Private Sub cmbReference_Leave(sender As Object, e As EventArgs) Handles cmbReference.Leave
    '    Try
    '        If Val(cmbReference.ActiveRow.Cells(0).Value) > 0 Then

    '            Dim strSQL As String = String.Empty

    '            '// Loading company locations
    '            strSQL = " SELECT        tblDefCompanyLocations.LocationId, tblDefCompanyLocations.LocationTitle, tblDefCompanyLocationType.LocationType, tblListCity.CityName " & _
    '                    " FROM            tblDefCompanyLocations INNER JOIN " & _
    '                    " tblListCity ON tblDefCompanyLocations.City = tblListCity.CityId INNER JOIN " & _
    '                    " tblDefCompanyLocationType ON tblDefCompanyLocations.LocationType = tblDefCompanyLocationType.LocationTypeId " & _
    '                    " where tblDefCompanyLocations.CompanyId=" & Me.cmbReference.ActiveRow.Cells(0).Value
    '            FillUltraDropDown(Me.cmbCompanyLocation, strSQL)
    '            Me.cmbCompanyLocation.Rows(0).Activate()
    '            Me.cmbCompanyLocation.DisplayLayout.Bands(0).Columns("LocationId").Hidden = True


    '            '// Loading company contacts
    '            strSQL = " select Pk_Id, ContactName, Designation, Mobile from TblCompanyContacts where RefCompanyId=" & Me.cmbReference.ActiveRow.Cells(0).Value
    '            FillUltraDropDown(Me.cmbContactPerson, strSQL)
    '            Me.cmbContactPerson.Rows(0).Activate()
    '            Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Pk_Id").Hidden = True
    '        End If
    '    Catch ex As Exception
    '        msg_Error(ex.Message)
    '    End Try
    'End Sub


    Public Sub AddToGrid()
        Dim dt As New DataTable
        Try
            Dim sNo As String = Me.grdItems.GetRows.GetLength(0)
            dt = CType(Me.grdItems.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item(Detail.PurchaseInquiryDetailId) = Val(0)
            dr.Item(Detail.PurchaseInquiryId) = Val(0)
            dr.Item(Detail.SerialNo) = sNo + 1

            Dim WithNewLines As String = ReplaceNewLine(Me.txtRequirementDescription.Text, False)
            dr.Item(Detail.RequirementDescription) = WithNewLines
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
            dr.Item(Detail.Qty) = Val(Me.txtQty.Text)
            dr.Item(Detail.ReferenceNo) = IIf(Me.txtReferenceNo.Text = "Reference No", "", Me.txtReferenceNo.Text)
            dr.Item(Detail.Comments) = IIf(Me.txtComments.Text = "Comments", "", Me.txtComments.Text)
            dr.Item(Detail.SalesInquiryId) = Val(0)
            dr.Item(Detail.SalesInquiryDetailId) = Val(0)
            dt.Rows.Add(dr)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub AddToGridFromSalesInquiry(ByVal Row As Janus.Windows.GridEX.GridEXRow)
        Dim dt As New DataTable
        Try
            'SalesInquiryDetailId()
            'SalesInquiryId()
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
            'Comments()
            'Dim sNo As String = Me.grdItems.GetRows.GetLength(0)

            dt = CType(Me.grdItems.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr.Item(Detail.PurchaseInquiryDetailId) = Val(0)
            dr.Item(Detail.PurchaseInquiryId) = Val(0)
            dr.Item(Detail.SerialNo) = Row.Cells("SerialNo").Value.ToString
            dr.Item(Detail.RequirementDescription) = Row.Cells("RequirementDescription").Value.ToString
            dr.Item(Detail.ArticleId) = Val(Row.Cells("ArticleId").Value.ToString)
            dr.Item(Detail.Code) = Row.Cells("Code").Value.ToString
            dr.Item(Detail.ArticleDescription) = Row.Cells("ArticleDescription").Value.ToString
            dr.Item(Detail.UnitId) = Val(Row.Cells("UnitId").Value.ToString)
            dr.Item(Detail.Unit) = Row.Cells("Unit").Value.ToString
            dr.Item(Detail.ItemTypeId) = Val(Row.Cells("ItemTypeId").Value.ToString)
            dr.Item(Detail.Type) = Row.Cells("Type").Value.ToString
            dr.Item(Detail.CategoryId) = Val(Row.Cells("CategoryId").Value.ToString)
            dr.Item(Detail.Category) = Row.Cells("Category").Value.ToString
            dr.Item(Detail.SubCategoryId) = Val(Row.Cells("SubCategoryId").Value.ToString)
            dr.Item(Detail.SubCategory) = Row.Cells("SubCategory").Value.ToString
            dr.Item(Detail.OriginId) = Val(Row.Cells("OriginId").Value.ToString)
            dr.Item(Detail.Origin) = Row.Cells("Origin").Value.ToString
            dr.Item(Detail.Qty) = Val(Row.Cells("Qty").Value.ToString)
            dr.Item(Detail.ReferenceNo) = ""
            dr.Item(Detail.Comments) = Row.Cells("Comments").Value.ToString
            dr.Item(Detail.SalesInquiryId) = Val(Row.Cells("SalesInquiryId").Value.ToString)
            dr.Item(Detail.SalesInquiryDetailId) = Val(Row.Cells("SalesInquiryDetailId").Value.ToString)
            If dr.Item(Detail.Qty) > 0 Then
                dt.Rows.Add(dr)
            End If
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
                Dim dt As DataTable = GetFormRights(EnumForms.frmPurchaseInquiry)
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
                DetailDAL.UpdateSalesInquiryStatusForOneDelete(0, Val(Me.grdItems.GetRow.Cells("SalesInquiryDetailId").Value.ToString), Val(Me.grdItems.GetRow.Cells("Qty").Value.ToString), Val(Me.grdItems.GetRow.Cells("PurchaseInquiryDetailId").Value.ToString))
                DetailDAL.Delete(Val(Me.grdItems.GetRow.Cells("PurchaseInquiryDetailId").Value.ToString))
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
    Public Sub DisplayDetail(ByVal PurchaseInquiryId As Integer)
        Try
            Me.grdItems.DataSource = Nothing
            Me.grdItems.DataSource = DetailDAL.GetSingle(PurchaseInquiryId)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayVendors(ByVal PurchaseInquiryId As Integer)
        Try
            Me.grdVendors.DataSource = Nothing
            Me.grdVendors.DataSource = VendorsDAL.GetVendors(PurchaseInquiryId)
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
                    If Save() Then
                        ''TASK TFS4437
                        'If AutoEmail = True Then
                        '    SendAutoEmail("Save")
                        'End If
                        ''END TASK TFS4437
                        GetAllRecords()
                        ReSetControls()

                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Update1() Then
                        ''TASK TFS4437
                        'If AutoEmail = True Then
                        '    If msg_Confirm("Do you want to send email?") = True Then
                        '        SendAutoEmail("Update")
                        '    End If
                        'End If
                        ''END TASK TFS4437
                        GetAllRecords()

                        ReSetControls()



                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
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
            Me.ddbGetRecord.Visible = False
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
            Dim CC = Me.cmbCC.Text
            FillCombos("CC")
            Me.cmbCC.Text = CC

            Dim BCC = Me.cmbBCC.Text
            FillCombos("BCC")
            Me.cmbBCC.Text = BCC
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
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Delete() Then
                GetAllRecords()
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub frmPurchaseInquiry_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetAllRecords()
            FillAllCombos()
            ReSetControls()
            IsFormLoaded = True ''TFS2648
            Me.lblProgress.BackColor = Color.LightYellow
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdVendors_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdVendors.ColumnButtonClick
        Try
            If e.Column.Key = "Remove" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                VendorsDAL.Delete(Val(Me.grdVendors.GetRow.Cells("PurchaseInquiryVendorsId").Value.ToString))
                Me.grdVendors.GetRow.Delete()
                Me.grdVendors.UpdateData()
            ElseIf e.Column.Key = "Email" Then
                SendAutoReminderEmail()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            If IsValidateToAddVendor() Then
                Dim dt As New DataTable
                dt = CType(Me.grdVendors.DataSource, DataTable)
                ''TASK TFS4663
                If dt.Rows.Count > 0 Then
                    Dim vendorsArray() As DataRow = dt.Select("VendorId = " & Me.cmbReference.Value & "")
                    If vendorsArray.Length > 0 Then
                        ShowErrorMessage("Same vendor already exists.")
                        Me.cmbReference.Focus()
                        Exit Sub
                    End If
                End If
                ''END TASK TFS4663
                Dim dr As DataRow
                dr = dt.NewRow
                dr.Item(Vendors.PurchaseInquiryVendorsId) = Val(0)
                dr.Item(Vendors.PurchaseInquiryId) = PurchaseInquiryId
                dr.Item(Vendors.VendorId) = Me.cmbReference.Value
                dr.Item(Vendors.VendorName) = Me.cmbReference.ActiveRow.Cells("Name").Value.ToString
                dr.Item(Vendors.Email) = Me.cmbReference.ActiveRow.Cells("Email").Value.ToString
                dt.Rows.Add(dr)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If IsValidateAddToGrid() Then
                AddToGrid()
                ResetDetailControls()
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

    Private Sub txtReferenceNo_MouseHover(sender As Object, e As EventArgs) Handles txtReferenceNo.MouseHover
        Try
            If Me.txtReferenceNo.Text = "Reference No" Then
                Me.txtReferenceNo.Text = ""
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtReferenceNo_MouseLeave(sender As Object, e As EventArgs) Handles txtReferenceNo.MouseLeave
        Try
            If Me.txtReferenceNo.Text = "" Then
                Me.txtReferenceNo.Text = "Reference No"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub EmailToVendors(ByVal Subject As String, ByVal Message As String, ByVal Credentialmail As String, ByVal Credentialpassword As String, ByVal Emailfrom As String, ByVal EmailList As List(Of String))
        Try
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New Net.NetworkCredential(Credentialmail, Credentialpassword)
            SmtpServer.Port = 587
            SmtpServer.Host = "smtp.gmail.com"
            mail = New MailMessage()
            mail.From = New MailAddress(Emailfrom)
            For Each EmailId As String In EmailList
                mail.To.Add(EmailId)
            Next
            mail.Subject = Subject
            mail.Body = Message
            SmtpServer.Send(mail)
            msg_Information("Email has been sent successfully")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CreateOutLookMail(Optional ByVal _AutoEmail As Boolean = False)
        Try
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = Me.uitxtName.Text
            mailItem.To = VendorEmails
            'VendorEmails = String.Empty
            CC = txtCC.Text
            mailItem.CC = CC
            'Me.txtCC.Text = "" ''TFS3239
            BCC = txtBCC.Text
            mailItem.BCC = BCC
            'Me.txtBCC.Text = "" ''TFS3239
            Dim dtAttachments As DataTable = MasterDAL.GetAttachments(Me.Name, PurchaseInquiryId)
            If dtAttachments.Rows.Count > 0 Then
                For Each Row As DataRow In dtAttachments.Rows
                    'Dim byte1() As Byte = Row.Item("Attachment_Image")
                    'Dim St As MemoryStream = New MemoryStream(byte1)
                    'Dim Attachment As New System.Net.Mail.Attachment()
                    mailItem.Attachments.Add(Row.Item("Path").ToString & "\" & Row.Item("FileName").ToString, Outlook.OlAttachmentType.olEmbeddeditem, Nothing, "Picture")
                Next
            End If
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            'mailItem.
            If _AutoEmail = False Then
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

    Private Sub CreateOutLookMailReminder(Optional ByVal _AutoEmail As Boolean = False)
        Try
            Dim oApp As Outlook.Application = New Outlook.Application
            Dim mailItem As Outlook.MailItem = oApp.CreateItem(Outlook.OlItemType.olMailItem)
            mailItem.Subject = "Reminder " + Me.uitxtName.Text
            mailItem.To = VendorEmails
            'VendorEmails = String.Empty
            CC = txtCC.Text
            mailItem.CC = CC
            'Me.txtCC.Text = "" ''TFS3239
            BCC = txtBCC.Text
            mailItem.BCC = BCC
            'Me.txtBCC.Text = "" ''TFS3239
            Dim dtAttachments As DataTable = MasterDAL.GetAttachments(Me.Name, PurchaseInquiryId)
            If dtAttachments.Rows.Count > 0 Then
                For Each Row As DataRow In dtAttachments.Rows
                    'Dim byte1() As Byte = Row.Item("Attachment_Image")
                    'Dim St As MemoryStream = New MemoryStream(byte1)
                    'Dim Attachment As New System.Net.Mail.Attachment()
                    mailItem.Attachments.Add(Row.Item("Path").ToString & "\" & Row.Item("FileName").ToString, Outlook.OlAttachmentType.olEmbeddeditem, Nothing, "Picture")
                Next
            End If
            mailItem.Importance = Outlook.OlImportance.olImportanceNormal
            'mailItem.
            If _AutoEmail = False Then
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

    Private Sub btnSendEmail_Click(sender As Object, e As EventArgs) Handles btnSendEmail.Click
        Try
            GetTemplate(lblHeader.Text)
            If EmailTemplate.Length > 0 Then
                GetEmailData()
                'GetVendorsEmails() ''Commented Against TFS3239
                FormatStringBuilder(dtEmail)
                For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdVendors.GetCheckedRows
                    VendorEmails = "" & Row.Cells("Email").Value.ToString & ""
                    CreateOutLookMail()
                Next
                SaveCCBCC(CC, BCC)
                CC = ""
                BCC = ""
            Else
                msg_Error("No email template is found for Purchase Inquiry.")
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
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetVendorsEmails()
        Try
            Me.grdVendors.UpdateData()
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdVendors.GetCheckedRows
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
    Private Sub GetEmailData()
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
                frm._VoucherId = Val(Me.grdSaved.GetRow.Cells("PurchaseInquiryId").Value.ToString)
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function ReplaceNewLine(ByVal selContent As String, ByVal isReplacingNewLineWithChar As Boolean, Optional ByVal selNewLineStringToUse As String = ".:.myCooLvbNewLine.:.") As String
        Try
            If isReplacingNewLineWithChar Then : Return selContent.Replace(vbNewLine, selNewLineStringToUse)
            Else : Return selContent.Replace(selNewLineStringToUse, vbNewLine)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub FillCCBCCEmails()
        Try
            txtBCC.AutoCompleteCustomSource.Clear()
            Dim dt As DataTable = MasterDAL.GetCCBCCEmails
            If dt.Rows.Count > 0 Then
                lbox = New ListBox()
                For count As Integer = 0 To dt.Rows.Count - 1
                    'lv.Items.Add(dt.Rows(count)("EmailAddress").ToString())
                    txtCC.AutoCompleteCustomSource.Add(dt.Rows(count)("EmailAddress").ToString())

                Next
            End If
            txtCC.AutoCompleteMode = AutoCompleteMode.Suggest
            txtCC.AutoCompleteSource = AutoCompleteSource.CustomSource
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'private void txtAdres_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    '{
    '    var x = txtAdres.Left;
    '    var y = txtAdres.Top + txtAdres.Height;
    '    var width = txtAdres.Width;
    '    const int height = 120;

    '    lbox.SetBounds(x, y, width, height);
    '    lbox.KeyDown += lbox_SelectedIndexChanged;
    '    lbox.DoubleClick += lbox_DoubleClick;
    '    gbxAdres.Controls.Add(lbox);
    '    lbox.BringToFront();
    '    lbox.Show();
    '    ActiveControl = txtAdres;
    '}

    Private Sub lbox_DoubleClick(sender As Object, e As EventArgs)
        Try
            txtCC.Text = CType(sender, ListBox).SelectedItem.ToString()
            lbox.Hide()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtCC_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCC.PreviewKeyDown
        'Try
        '    Dim x = txtCC.Left
        '    Dim y = txtCC.Top + txtCC.Height
        '    Dim width = txtCC.Width
        '    Dim height As Integer = 120

        '    lbox.SetBounds(x, y, width, height)
        '    'lbox.KeyDown += lbox_SelectedIndexChanged
        '    'lbox.DoubleClick += lbox_DoubleClick(Nothing, Nothing)
        '    AddHandler lbox.DoubleClick, AddressOf lbox_DoubleClick
        '    gbCC.Controls.Add(lbox)
        '    lbox.BringToFront()
        '    lbox.Show()
        '    ActiveControl = txtCC
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    'Private Sub txtCC_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCC.KeyDown
    '    lv = New LVX(txtCC)
    '    If e.KeyCode = Keys.Escape Then
    '        lv.Hide()
    '    End If
    '    If (e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down) And lv.Visible Then
    '        lv.Focus()
    '        Exit Sub
    '    End If
    '    If e.Control Then
    '        If e.KeyCode = Keys.Space Then
    '            e.SuppressKeyPress = True
    '            lv.Location = New Point(Windows.Forms.Cursor.Position.X - Me.Left, Windows.Forms.Cursor.Position.Y - Me.Top)
    '            lv.Height = 100
    '            lv.BringToFront()
    '            lv.Show()
    '            Dim prevtext = txtCC.Text.Substring(InStrRev(txtCC.Text, " "))
    '            If prevtext = "" Then
    '                txtCC.Text.Substring(InStrRev(txtCC.Text, vbLf))
    '            End If
    '            If prevtext <> "" Then
    '                lv.SendKeyString(prevtext)
    '            Else
    '                lv.SelectedIndex = 0
    '            End If
    '        End If
    '    End If
    'End Sub

    Private Sub btnAddCC_Click(sender As Object, e As EventArgs) Handles btnAddCC.Click
        Try
            If Not cmbCC.Text = ".... Select any Value ...." Then
                'If Me.txtCC.Text.Length > 0 Then
                Me.txtCC.Text += Me.cmbCC.Text & ";"
                'End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddBCC_Click(sender As Object, e As EventArgs) Handles btnAddBCC.Click
        Try
            If Not cmbBCC.Text = ".... Select any Value ...." Then
                'If Me.txtBCC.Text.Length > 0 Then
                Me.txtBCC.Text += Me.cmbBCC.Text & ";"
                'End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
            If EmailList.Count > 0 Then
                MasterDAL.AddCCBCCEmails(EmailList)
            End If
        Catch ex As Exception
            Throw ex
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

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Ali Faisal : TFS2014 : Show Purchase Demand search screen as dialog
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLoadDemand_Click(sender As Object, e As EventArgs) Handles btnLoadDemand.Click
        Try
            frmSearchPurchaseDemand.ShowDialog()
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
    ''' <summary>
    ''' Muhammad Aashir : TFS3223 : Show Print Preview At Purchase Inquiry
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Try
            GetCrystalReportRights()
            AddRptParam("@PurchaseInquiryId", Me.grdSaved.CurrentRow.Cells("PurchaseInquiryId").Value)
            ShowReport("PurchaseInquiry")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Purchase Inquiry"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub SendAutoEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate(lblHeader.Text)
            If EmailTemplate.Length > 0 Then
                GetAutoEmailData()
                'GetVendorsEmails() ''Commented Against TFS3239
                FormatStringBuilder(dtEmail)
                For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdVendors.GetRows
                    VendorEmails = "" & Row.Cells("Email").Value.ToString & ""
                    If Not VendorEmails = "" Then
                        CreateOutLookMail(True)
                        SaveEmailLog(Me.uitxtName.Text, VendorEmails, "frmPurchaseInquiry", Activity)
                        IsEmailSent = True
                        VendorEmails = ""
                    End If
                Next
                SaveCCBCC(CC, BCC)
                CC = ""
                BCC = ""
            Else
                msg_Error("No email template is found for Purchase Inquiry.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SendAutoReminderEmail(Optional ByVal Activity As String = "")
        Try
            GetTemplate(lblHeader.Text)
            If EmailTemplate.Length > 0 Then
                GetAutoEmailData()
                'GetVendorsEmails() ''Commented Against TFS3239
                FormatStringBuilder(dtEmail)
                VendorEmails = Me.grdVendors.GetRow.Cells("Email").Value.ToString()
                    If Not VendorEmails = "" Then
                    CreateOutLookMailReminder(True)
                        SaveEmailLog(Me.uitxtName.Text, VendorEmails, "frmPurchaseInquiry", Activity)
                        IsEmailSent = True
                        VendorEmails = ""
                    End If
                SaveCCBCC(CC, BCC)
                CC = ""
                BCC = ""
            Else
                msg_Error("No email template is found for Purchase Inquiry.")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub grdSaved_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.ColumnButtonClick
        Try
            ''TASK TFS4454
            If Me.grdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If e.Column.Key = "Email" Then
                    DisplayDetail(Me.grdSaved.GetRow.Cells("PurchaseInquiryId").Value)
                    DisplayVendors(Me.grdSaved.GetRow.Cells("PurchaseInquiryId").Value)
                    Me.uitxtName.Text = Me.grdSaved.GetRow.Cells("PurchaseInquiryNo").Value.ToString
                    SendAutoEmail()
                    If IsEmailSent = True Then
                        msg_Information("Email has been sent successfully.")
                    Else
                        msg_Information("Email has not been sent.")
                    End If
                    ReSetControls()
                    GetAllRecords()
                End If
            End If
            ''TASK TFS4454
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoName_CheckedChanged(sender As Object, e As EventArgs) Handles rdoName.CheckedChanged
        Try
            ''TFS4655
            If Not Me.IsFormLoaded = True Then Exit Sub
            Me.cmbReference.DisplayMember = Me.cmbReference.Rows(0).Cells(1).Column.Key.ToString
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RdoBrand_CheckedChanged(sender As Object, e As EventArgs) Handles RdoBrand.CheckedChanged
        Try
            ''TFS4655
            If Not Me.IsFormLoaded = True Then Exit Sub
            Me.cmbReference.DisplayMember = Me.cmbReference.Rows(0).Cells("Other Comments").Column.Key.ToString
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class


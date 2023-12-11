Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class frmAgreement
    Implements IGeneral
    Dim AgreementId As Integer = 0I
    Dim Agreement As AgreementMasterBE
    Dim flgCompanyRights As Boolean = False
    Dim IsOpenForm As Boolean = False
    Dim arrFile As List(Of String) ''TFS1854
    Public Enum enmAgreement
        AgreementId
        AgreementNo
        AgreementDate
        Delivery_Date
        FirstPayment
        AgreementType
        Business_Name
        Contact_Name
        Business_Type
        Address
        Phone
        FaxNo
        Email
        StateID
        CityID
        TerritoryID
        Product_Category_Condition
        Term_Condition
        Warranty_Condition
        Termination_Condition
        Status
        'AgreementDetail ''TFS1854 :Added in the consequences of TFS1854
        Total_Qty
        Total_Amount
        User_Name
        Discount
        Customer_Name ''TFS1854
        'arrFile  ''TFS1854
        'Source ''TFS1854
        'AttachmentPath ''TFS1854'
        No_of_Attachment ''TFS1854
    End Enum
    Enum enmGrdDetail
        LocationId
        ArticleDefId
        ArticleCode
        Item
        Unit
        Qty
        Price
        Total
        CurrentPrice
        PackQty
        Comments
        Pack_Desc
    End Enum
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "Master" Then
                Me.grdSaved.RootTable.Columns(enmAgreement.AgreementId).Visible = False
                Me.grdSaved.RootTable.Columns(enmAgreement.StateID).Visible = False
                Me.grdSaved.RootTable.Columns(enmAgreement.CityID).Visible = False
                Me.grdSaved.RootTable.Columns(enmAgreement.TerritoryID).Visible = False
                Me.grdSaved.RootTable.Columns(enmAgreement.Product_Category_Condition).Visible = False
                Me.grdSaved.RootTable.Columns(enmAgreement.Warranty_Condition).Visible = False
                Me.grdSaved.RootTable.Columns(enmAgreement.Term_Condition).Visible = False
                Me.grdSaved.RootTable.Columns(enmAgreement.Termination_Condition).Visible = False
                Me.grdSaved.RootTable.Columns(enmAgreement.AgreementDate).FormatString = "dd/MMM/yyyy"
                Me.grdSaved.RootTable.Columns(enmAgreement.No_of_Attachment).ColumnType = Janus.Windows.GridEX.ColumnType.Link
                Me.grdSaved.RootTable.Columns(enmAgreement.No_of_Attachment).Caption = "Attachments"
                Me.grdSaved.RootTable.Columns(enmAgreement.No_of_Attachment).Key = "No Of Attachment"

            ElseIf Condition = "Detail" Then
                For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                    If Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.LocationId AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Qty AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Price AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Comments Then
                        grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
            End If
            Me.grd.RootTable.Columns("Pack_Desc").Position = Me.grd.RootTable.Columns("Unit").Index
            Me.grd.RootTable.Columns("Unit").Position = Me.grd.RootTable.Columns("Pack_Desc").Index
            Me.grd.RootTable.Columns("Pack_Desc").Visible = True
            Me.grd.RootTable.Columns("Unit").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    'Me.btnSearchDelete.Enabled = False
                    Me.btnPrint.Enabled = False
                    ' Me.btnSearchPrint.Enabled = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmSalesReturn)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                UserPostingRights = GetUserPostingRights(LoginUserId)
                If UserPostingRights = True Then
                    Me.chkPost.Visible = True
                Else
                    Me.chkPost.Visible = False
                    Me.chkPost.Checked = False
                End If
                GetSecurityByPostingUser(UserPostingRights, btnSave, btnDelete)
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                'Me.btnSearchDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'Me.btnSearchPrint.Enabled = False
                Me.chkPost.Visible = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For Each Rightsdt As GroupRights In Rights
                    If Rightsdt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rightsdt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                        'Me.btnSearchDelete.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        'Me.btnSearchPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf Rightsdt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True
                    End If
                Next
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If Me.grdSaved.RootTable Is Nothing Then Return False
            Agreement = New AgreementMasterBE
            Agreement.AgreementId = Me.grdSaved.GetRow.Cells(enmAgreement.AgreementId).Value
            If New AgreementDAL().Delete(Agreement) Then
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
            If Condition = "BusinessType" Then
                Me.cmbBusinessType.ValueMember = "Business_Type"
                Me.cmbBusinessType.DisplayMember = "Business_Type"
                Me.cmbBusinessType.DataSource = New AgreementDAL().getBusinessType
            ElseIf Condition = "State" Then
                FillDropDown(Me.cmbState, "Select StateId, StateName From tblListState")
            ElseIf Condition = "City" Then
                FillDropDown(Me.cmbCity, "Select CityId, CityName From tblListCity WHERE StateId=" & IIf(Me.cmbState.SelectedIndex - 1, 0, "" & Me.cmbState.SelectedValue & "") & "")
            ElseIf Condition = "Territory" Then
                FillDropDown(Me.cmbTerritory, "Select TerritoryId, TerritoryName From tblListTerritory WHERE CityId=" & IIf(Me.cmbCity.SelectedIndex - 1, 0, "" & Me.cmbCity.SelectedValue & "") & "")
            ElseIf Condition = "Item" Then
                str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock, Isnull(PurchasePrice,0) as PurchasePrice FROM ArticleDefView where Active=1"
                If flgCompanyRights = True Then
                    str += " AND ArticleDefView.CompanyId=" & MyCompanyId
                End If
                str += " ORDER By ArticleDefView.SortOrder Asc "
                FillUltraDropDown(Me.cmbItem, str)
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
                If rdoName.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                End If
            ElseIf Condition = "Location" Then
                FillDropDown(Me.cmbCategory, "Select Location_Id, Location_Name From tblDefLocation", False)
            ElseIf Condition = "ArticlePack" Then
                Me.cmbUnit.ValueMember = "ArticlePackId"
                Me.cmbUnit.DisplayMember = "PackName"
                Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
            ElseIf Condition = "AgreementType" Then
                FillDropDown(Me.cmbAgreementType, "Select DISTINCT AgreementType, AgreementType From AgreementMasterTable", False)
            ElseIf Condition = "CustomerName" Then

                str = "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, vwCOADetail.detail_code as [Code],tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                   "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses, tblCustomer.Fuel as Fuel, tblCustomer.CNG as CNG , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, vwCOADetail.Contact_Email as Email, vwCOADetail.Contact_Phone as Phone, vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(vwCOADetail.SaleMan,0) as SaleManId,IsNull(vwCOADetail.CreditDays,0) as CreditDays " & _
                                   "FROM  tblCustomer LEFT OUTER JOIN " & _
                                   "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                   "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                   "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                   "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                   "WHERE (vwCOADetail.account_type in( 'Customer','Vendor' )) and  vwCOADetail.coa_detail_id is not  null "


                FillUltraDropDown(cmbVendor, str)
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("City").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("ExpiryDate").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Discount").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("otherexpanses").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Fuel").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("CNG").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Limit").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Phone").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("SaleManId").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("CreditDays").Hidden = True
                'If cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                '    'cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True

                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns().Hidden = True
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = False
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                '    ' CNG Changes
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.CNG).Hidden = True
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Hidden = True
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Hidden = True
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Type).Hidden = True
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
                '    'Task:2373 Column Formating
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.SubSubTitle).Header.Caption = "Ac Head"
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.SubSubTitle).Width = 120
                '    'End Task:2373
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.SaleMan).Hidden = True
                '    Me.cmbVendor.DisplayLayout.Bands(0).Columns("CreditDays").Hidden = True

                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Agreement = New AgreementMasterBE
            Agreement.AgreementId = AgreementId
            Agreement.AgreementNo = Me.txtAgreementNo.Text
            Agreement.AgreementDate = Me.dtpAgreementDate.Value
            Agreement.Delivery_Date = Me.dtpDeliveryDate.Value
            Agreement.First_Payment = Val(Me.txtFirstPayment.Text)
            Agreement.AgreementType = IIf(Me.cmbAgreementType.Text.Length > 0, Me.cmbAgreementType.Text, "")
            Agreement.Business_Name = Me.txtBusinessName.Text
            Agreement.Contact_Name = Me.txtContactName.Text
            ''Start TFS1854
            'Agreement.Customer_Name = Me.txtCustomerName.Text
            Agreement.Customer_Name = Me.cmbVendor.Text

            '------------------------------------------

            Agreement.ArrFile = arrFile
            Agreement.Source = Me.Name
            If Not getConfigValueByType("FileAttachmentPath").ToString = "Error" Then
                Agreement.AttachmentPath = getConfigValueByType("FileAttachmentPath").ToString
            Else
                Agreement.AttachmentPath = ""
            End If
            ''End TFS1854
            Agreement.StateID = Me.cmbState.SelectedValue
            Agreement.CityID = Me.cmbCity.SelectedValue
            Agreement.TerritoryID = Me.cmbTerritory.SelectedValue
            Agreement.Business_Type = Me.cmbBusinessType.Text
            Agreement.Address = Me.txtAddress.Text
            Agreement.Phone = Me.txtPhone.Text
            Agreement.FaxNo = Me.txtFaxNo.Text
            Agreement.Email = Me.txtEmail.Text
            Agreement.Status = IIf(Me.chkPost.Visible = True, Me.chkPost.Checked, False)
            Agreement.Product_Category_Condition = Me.txtProudctAndCategory.Text
            Agreement.Term_Condition = Me.txtTermsAndCondition.Text
            Agreement.Warranty_Condition = Me.txtWarnty.Text
            Agreement.Termination_Condition = Me.txtDeployementSchedule.Text
            Agreement.Total_Qty = Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGrdDetail.Qty), Janus.Windows.GridEX.AggregateFunction.Sum)
            Agreement.Total_Amount = Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGrdDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)
            Agreement.User_Name = LoginUserName
            Agreement.Discount = Val(Me.txtDiscount.Text)
            Agreement.AgreementDetail = New List(Of AgreementDetailBE)
            Dim AgreementDt As AgreementDetailBE
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                With Agreement.AgreementDetail
                    AgreementDt = New AgreementDetailBE
                    AgreementDt.AgreementId = AgreementId
                    AgreementDt.LocationId = row.Cells("LocationId").Value
                    AgreementDt.ArticleDefId = row.Cells("ArticleDefId").Value
                    AgreementDt.ArticleSize = row.Cells("Unit").Value.ToString
                    AgreementDt.Sz1 = Val(row.Cells("Qty").Value.ToString)
                    AgreementDt.Sz2 = row.Cells("PackQty").Value
                    AgreementDt.Qty = IIf(row.Cells("Unit").Value.ToString = "Loose", row.Cells("Qty").Value, (Val(row.Cells("Qty").Value.ToString) * Val(row.Cells("PackQty").Value.ToString)))
                    AgreementDt.Price = Val(row.Cells("Price").Value.ToString)
                    AgreementDt.CurrentPrice = Val(row.Cells("CurrentPrice").Value.ToString)
                    AgreementDt.Comments = row.Cells("Comments").Value.ToString
                    AgreementDt.Pack_Desc = row.Cells("Pack_Desc").Value.ToString
                    .Add(AgreementDt)
                End With
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Condition = "Master" Then
                Me.grdSaved.DataSource = New AgreementDAL().GetAll
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings("Master")
                Me.grdSaved.AutoSizeColumns()
            ElseIf Condition = "Detail" Then
                Dim dt As New DataTable
                dt = New AgreementDAL().GetDetailRecords(AgreementId)
                dt.Columns("Total").Expression = "iif(Unit='Loose', (Qty*Price), ((PackQty*Qty)*Price))"
                Me.grd.DataSource = dt
                Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(New AgreementDAL().GetLocation.DefaultView, "Location_Id", "Location_Name")
                'Me.grd.AutoSizeColumns()
                ApplyGridSettings("Detail")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try


            If Me.txtBusinessName.Text = String.Empty Then
                ShowErrorMessage("Please enter business name")
                Me.txtBusinessName.Focus()
                Return False
            End If
            If txtContactName.Text = String.Empty Then
                ShowErrorMessage("Please enter contact name")
                Me.txtContactName.Focus()
                Return False
            End If
            If Me.txtAddress.Text = String.Empty Then
                ShowErrorMessage("Please enter address")
                Me.txtAddress.Focus()
                Return False
            End If
            If Me.txtAddress.Text = String.Empty Then
                ShowErrorMessage("Please enter phone")
                Me.txtPhone.Focus()
                Return False
            End If
            If Me.grd.RowCount = 0 Then
                ShowErrorMessage("Record not in grid")
                Me.grd.Focus()
                Return False
            End If

            'check if destination directory uin configuration exists or not


            FillModel() 'Call Fillmodel 
            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            AgreementId = 0I
            Me.btnSave.Text = "&Save"
            Me.txtAgreementNo.Text = GetDocumentNo()
            Me.dtpAgreementDate.Value = Date.Now
            Me.dtpDeliveryDate.Value = Date.Now
            Me.txtFirstPayment.Text = String.Empty
            Me.txtBusinessName.Text = String.Empty
            Me.txtContactName.Text = String.Empty
            'Me.txtCustomerName.Text = String.Empty ''TFS1854
            FillCombos("CustomerName")
            Me.txtAddress.Text = String.Empty
            Me.txtPhone.Text = String.Empty
            Me.txtFaxNo.Text = String.Empty
            Me.txtEmail.Text = String.Empty
            Me.cmbUnit.SelectedIndex = 0
            Me.cmbVendor.Rows(0).Activate()
            Me.txtBusinessName.Focus()
            Me.txtTermsAndCondition.Text = New AgreementDAL().GetTermAndCondition
            Me.txtDeployementSchedule.Text = New AgreementDAL().GetTerminationCondition
            Me.txtWarnty.Text = New AgreementDAL().GetWarrantyCondition
            Me.txtProudctAndCategory.Text = New AgreementDAL().GetProductionCategoryCondition
            arrFile = New List(Of String) ''TFS1854
            FillCombos("BusinessType")
            FillCombos("AgreementType")
            If Not Me.cmbAgreementType.SelectedIndex = -1 Then Me.cmbAgreementType.Text = String.Empty
            GetAllRecords("Master")
            GetAllRecords("Detail")
            arrFile = New List(Of String) ''TFS1854
            Me.btnAttachments.Text = "Attachment (" & arrFile.Count & ")" ''TFS1854
            ApplySecurity(EnumDataMode.[New])
            'ClearData()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
          

            If New AgreementDAL().Add(Agreement) Then
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
           
            If New AgreementDAL().Update(Agreement) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmAgreement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If
            FillCombos("State")
            FillCombos("City")
            FillCombos("Territory")
            FillCombos("Location")
            FillCombos("Item")
            IsOpenForm = True
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCode.CheckedChanged
        Try
            If Not IsOpenForm = True Then Exit Sub
            If rdoName.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoName.CheckedChanged
        Try
            If Not IsOpenForm = True Then Exit Sub
            If rdoName.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try
            If Not IsOpenForm = True Then Exit Sub
            If Me.cmbUnit.Text = "Loose" Then
                txtTotal.Text = Val(txtQty.Text) * Val(txtRate.Text)
                txtPackQty.Text = 1
                Me.txtPackQty.Enabled = False
                Me.txtPackQty.TabStop = False
            Else
                Me.txtPackQty.Enabled = True
                Me.txtPackQty.TabStop = True
                Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Item("PackQty").ToString)
                txtTotal.Text = Val(txtQty.Text) * Val(txtPackQty.Text) * Val(txtRate.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If GetConfigValue("VoucherNo").ToString = "Yearly" Then
                ' Return GetSerialNo(IIf(CompanyBasePrefix = False & IIf(Me.GetPrefix(IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue)).Length = 0, "SI" & Me.cmbCompany.SelectedValue & "-" & Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year & "-", "SalesMasterTable", "SalesNo")
                Return GetSerialNo("AG" & "-" + Microsoft.VisualBasic.Right(Me.dtpAgreementDate.Value.Year, 2) + "-", "AgreementMasterTable", "AgreementNo")
            Else
                Return GetNextDocNo("AG", 6, "AgreementMasterTable", "AgreementNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Sub AddItemToGrid(Optional ByVal Condition As String = "")
        Try

            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            If dt IsNot Nothing Then
                dr = dt.NewRow
                dr(enmGrdDetail.LocationId) = Me.cmbCategory.SelectedValue
                dr(enmGrdDetail.ArticleDefId) = Me.cmbItem.Value
                dr(enmGrdDetail.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Text
                dr(enmGrdDetail.Item) = Me.cmbItem.ActiveRow.Cells("Item").Text
                dr(enmGrdDetail.Unit) = IIf(Me.cmbUnit.Text <> "Loose", "Pack", Me.cmbUnit.Text)
                dr(enmGrdDetail.Qty) = Val(Me.txtQty.Text)
                dr(enmGrdDetail.Price) = Val(Me.txtRate.Text)
                dr(enmGrdDetail.Total) = 0
                dr(enmGrdDetail.CurrentPrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
                dr(enmGrdDetail.PackQty) = Val(Me.txtPackQty.Text)
                dr(enmGrdDetail.Comments) = String.Empty
                dr(enmGrdDetail.Pack_Desc) = Me.cmbUnit.Text
                dt.Rows.InsertAt(dr, 0)
                dt.AcceptChanges()
                dt.Columns("Total").Expression = "iif(Unit='Loose', (Qty*Price), ((PackQty*Qty)*Price))"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function GridIsValidate() As Boolean
        Try

            If Me.cmbCategory.SelectedIndex = -1 Then
                ShowErrorMessage("Please define location")
                Me.cmbCategory.Focus()
                Return False
            End If
            If Me.cmbItem.ActiveRow Is Nothing Then
                ShowErrorMessage("Please select item")
                Me.cmbItem.Focus()
                Return False
            End If



            Return True

        Catch ex As Exception

        End Try
    End Function


    Sub ClearData()
        Try
            Me.cmbCategory.SelectedIndex = 0
            Me.cmbItem.Rows(0).Activate()
            Me.cmbUnit.SelectedIndex = 0
            Me.txtQty.Text = String.Empty
            Me.txtRate.Text = String.Empty
            Me.txtTotal.Text = String.Empty
            Me.cmbItem.Focus()
            Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try

            If GridIsValidate() = True Then
                AddItemToGrid()
            End If

            ClearData()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informSave)
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informUpdate)
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            msg_Information(str_informDelete)
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AgreementId = Me.grdSaved.GetRow.Cells(enmAgreement.AgreementId).Value
            Me.txtAgreementNo.Text = Me.grdSaved.GetRow.Cells(enmAgreement.AgreementNo).Value.ToString
            Me.dtpAgreementDate.Value = Me.grdSaved.GetRow.Cells(enmAgreement.AgreementDate).Value
            Me.dtpDeliveryDate.Value = Me.grdSaved.GetRow.Cells(enmAgreement.Delivery_Date).Value
            Me.txtFirstPayment.Text = Me.grdSaved.GetRow.Cells(enmAgreement.FirstPayment).Value
            Me.txtBusinessName.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Business_Name).Value.ToString
            Me.txtContactName.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Contact_Name).Value.ToString
            'Me.txtCustomerName.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Customer_Name).Value.ToString ''TFS1854
            Me.cmbVendor.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Customer_Name).Value.ToString

            '------------------------------------------------


            Me.cmbState.SelectedValue = Me.grdSaved.GetRow.Cells(enmAgreement.StateID).Value
            Me.cmbCity.SelectedValue = Me.grdSaved.GetRow.Cells(enmAgreement.CityID).Value
            Me.cmbTerritory.SelectedValue = Me.grdSaved.GetRow.Cells(enmAgreement.TerritoryID).Value
            Me.cmbBusinessType.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Business_Type).Value.ToString
            Me.txtAddress.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Address).Value.ToString
            Me.txtPhone.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Phone).Value.ToString
            Me.txtFaxNo.Text = Me.grdSaved.GetRow.Cells(enmAgreement.FaxNo).Value.ToString
            Me.txtEmail.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Email).Value.ToString
            Me.txtProudctAndCategory.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Product_Category_Condition).Value.ToString
            Me.txtTermsAndCondition.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Term_Condition).Value.ToString
            Me.txtWarnty.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Warranty_Condition).Value.ToString
            Me.txtDeployementSchedule.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Termination_Condition).Value.ToString
            Me.txtDiscount.Text = Me.grdSaved.GetRow.Cells(enmAgreement.Discount).Value.ToString
            If IsDBNull(Me.grdSaved.GetRow.Cells("AgreementType").Value) Then
                Me.cmbAgreementType.Text = String.Empty
            Else
                Me.cmbAgreementType.Text = Me.grdSaved.GetRow.Cells("AgreementType").Value
            End If
            Dim intCountAttachedFiles As Integer = 0I
            If Me.btnSave.Text <> "&Save" Then
                If Me.grdSaved.RowCount > 0 Then
                    intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells(enmAgreement.No_of_Attachment).Value)
                    Me.btnAttachments.Text = "Attachment (" & intCountAttachedFiles & ")"
                End If
            End If
            Me.btnSave.Text = "&Update"
            GetAllRecords("Detail")
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.txtBusinessName.Focus()
            ApplySecurity(EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            grdSaved_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbState_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbState.SelectedIndexChanged
        Try
            If IsOpenForm = True Then FillCombos("City")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCity.SelectedIndexChanged
        Try
            If IsOpenForm = True Then FillCombos("Territory")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If Me.UltraTabControl1.Tabs(1).Selected = True Then
                Me.btnLoadAll.Visible = False
            Else
                Me.btnLoadAll.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoadAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
        Try
            Me.grdSaved.DataSource = New AgreementDAL().GetAll("ALL")
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings("Master")
            Me.grdSaved.AutoSizeColumns()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItem.Click
        Try
            Call frmAddItem.ShowDialog()
            If Not DialogResult = Windows.Forms.DialogResult.OK Then FillCombos("Item")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim Id As Integer = 0
            Id = Me.cmbState.SelectedIndex
            FillCombos("State")
            Me.cmbState.SelectedIndex = Id

            Id = Me.cmbCity.SelectedIndex
            FillCombos("City")
            Me.cmbCity.SelectedIndex = Id

            Id = Me.cmbTerritory.SelectedIndex
            FillCombos("Territory")
            Me.cmbTerritory.SelectedIndex = Id

            Id = Me.cmbCategory.SelectedIndex
            FillCombos("Location")
            Me.cmbCategory.SelectedIndex = Id


            Id = Me.cmbItem.ActiveRow.Cells(0).Value
            FillCombos("Item")
            Me.cmbItem.ActiveRow.Cells(0).Value = Id


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_RowSelected(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If Me.cmbItem.IsItemInList = False Then
                'Me.txtStock.Text = 0
                Exit Sub
            End If
            'If Not GetConfigValue("StockViewOnSale").ToString = "True" Then
            '    If Me.cmbItem.Value Is Nothing Then Exit Sub
            '    'Me.txtStock.Text = Convert.ToDouble(GetStockById(Me.cmbItem.ActiveRow.Cells(0).Value, Me.cmbCategory.SelectedValue))
            '    If IsSalesOrderAnalysis = True Then
            '        Dim dt As DataTable = GetCostManagement(Me.cmbItem.Value)
            '        If dt IsNot Nothing Then
            '            TradePrice = dt.Rows(0).Item("TradePrice")
            '            Freight_Rate = dt.Rows(0).Item("Freight")
            '            MarketReturns_Rate = dt.Rows(0).Item("MarketReturns")
            '            GST_Applicable = dt.Rows(0).Item("Gst_Applicable")
            '            FlatRate_Applicable = dt.Rows(0).Item("FlatRate_Applicable")
            '            FlatRate = dt.Rows(0).Item("FlatRate")
            '            Freight = Freight_Rate
            '            MarketReturns = MarketReturns_Rate
            '        End If
            '        Dim dtDiscount As DataTable = GetAnalysisLastDiscount(Me.cmbVendor.Value, Me.cmbItem.Value)
            '        If dtDiscount IsNot Nothing Then
            '            If dtDiscount.Rows.Count > 0 Then
            '                Me.txtDisc.Text = dtDiscount.Rows(0).Item(0)
            '            Else
            '                Me.txtDisc.Text = 0
            '            End If
            '        Else
            '            txtDisc.Text = 0
            '        End If
            '    Else
            '        txtDisc.Text = GetLastDiscount(IIf(Me.cmbItem.IsItemInList = True, Me.cmbVendor.Value, 0), Me.cmbItem.Value)
            '    End If
            'Else
            '    txtDisc.Text = GetLastDiscount(IIf(Me.cmbItem.IsItemInList = True, Me.cmbVendor.Value, 0), Me.cmbItem.Value)
            'End If
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.Value Is Nothing Then Exit Sub
            FillCombos("ArticlePack")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        Try
            'Me.cmbBatchNo.LimitToList = True
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            'Me.txtStock.Text = UiGetStockDataTable(Me.cmbItem.Value)
            Me.txtRate.Text = Me.cmbItem.ActiveRow.Cells("Price").Value.ToString
            'Me.txtStock.Text = Me.cmbItem.ActiveRow.Cells("Stock").Value.ToString
            ' Me.txtBatchNo.Text = Me.cmbItem.ActiveRow.Cells("BatchNo").Value.ToString
            'If Not GetConfigValue("ServiceItem").ToString = "True" Then
            If Val(Me.txtQty.Text) <= 0 Then Me.txtQty.Text = 1
            'Else
            'Me.txtQty.Text = 0
            'If Val(Me.txtServiceQty.Text) <= 0 Then Me.txtServiceQty.Text = 1
            'End If
            Dim strSQl As String = String.Empty
            'If GetConfigValue("WithSizeRange") = "False" Then '& "   FROM vw_Batch_Stock " _
            'If GetConfigValue("WithSizeRange") = "True" Then
            '    strSQl = " SELECT ISNULL(a.Stock, 0) AS Stock, PurchaseBatchTable.BatchNo AS [Batch No], PurchaseBatchTable.BatchID" _
            '            & " FROM SizeRangeTable INNER JOIN" _
            '            & " PurchaseBatchTable ON SizeRangeTable.BatchID = PurchaseBatchTable.BatchID LEFT OUTER JOIN " _
            '            & " (SELECT     * " _
            '            & " From vw_Batch_Stock_ByLocation " _
            '            & " WHERE      articleID = " & Me.cmbItem.Value & ") a ON PurchaseBatchTable.BatchID = a.BatchID " _
            '            & " WHERE(SizeRangeTable.SizeID = " & IIf(Val(Me.cmbItem.ActiveRow.Cells("Size ID").Value.ToString) > 0, Me.cmbItem.ActiveRow.Cells("Size ID").Value, 0) & ") and LocationId = " & Me.cmbCategory.SelectedValue & " "
            'Else
            '    'strSQl = "SELECT Stock, BatchNo as [Batch No], BatchID FROM  dbo.vw_Batch_Stock WHERE     (NOT (Stock = 0))and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            '    'strSQl = "SELECT Stock, BatchNo as [Batch No] , BatchID FROM         dbo.vw_Batch_Stock WHERE     (NOT (Stock = 0))and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            '    strSQl = "SELECT Stock, BatchNo as [Batch No] , BatchID FROM         dbo.vw_Batch_Stock_ByLocation WHERE     (NOT (Stock = 0))and LocationId = " & Me.cmbCategory.SelectedValue & " and articleid= " & Me.cmbItem.ActiveRow.Cells(0).Value
            'End If

            'FillUltraDropDown(cmbBatchNo, strSQl, False)

            'cmbBatchNo.DataSource = Nothing

            'Dim objCommand As New System.Data.OleDb.OleDbCommand
            'Dim objDataAdapter As New System.Data.OleDb.OleDbDataAdapter
            'Dim objDataSet As New DataSet

            'If Con.State = ConnectionState.Open Then Con.Close()

            'Con.Open()
            'objCommand.Connection = Con
            'objCommand.CommandType = CommandType.Text


            'objCommand.CommandText = strSQl


            'Dim dt As New DataTable
            'Dim dr As DataRow

            'objDataAdapter.SelectCommand = objCommand
            'objDataAdapter.Fill(dt)

            'dr = dt.NewRow
            'dr.Item(1) = ""
            'dr.Item(0) = 0
            'dr.Item(2) = 0
            'dt.Rows.Add(dr)

            'cmbBatchNo.DisplayMember = "Batch No"
            'cmbBatchNo.ValueMember = "BatchID"
            'cmbBatchNo.DataSource = dt

            'cmbBatchNo.DisplayLayout.Bands(0).Columns("BatchId").Hidden = True

            'Con.Close()

            'Dim i As Integer
            'For i = 0 To Me.cmbBatchNo.Rows.Count - 2
            '    Dim Gi As Integer
            '    For Gi = 0 To Me.grd.GetRows.Length - 1
            '        If Me.cmbBatchNo.Rows(i).Cells("Batch No").Value = Me.grd.GetRow(Gi).Cells(EnumGridDetail.BatchNo).Value AndAlso Me.grd.GetRow(Gi).Cells(EnumGridDetail.SaleDetailID).Value.ToString = "0" Then
            '            Me.cmbBatchNo.Rows(i).Cells("Stock").Value = Me.cmbBatchNo.Rows(i).Cells("Stock").Value - Me.grd.GetRow(Gi).Cells(EnumGridDetail.Qty).Value
            '        End If
            '    Next
            '    'me.cmbBatchNo.Rows(i).Cells("BatchNo)
            'Next
            'Me.cmbBatchNo.Rows(0).Activate()


            'objCommand.CommandText = " SELECT   Price" _
            '                     & " FROM SalesDetailTable" _
            '                     & " WHERE     SaleDetailId =" _
            '                     & " (SELECT     MAX(SaleDetailID) " _
            '                     & "   FROM SalesDetailTable, SalesMasterTable " _
            '                     & " WHERE(SalesdetailTable.Salesid = SalesMasterTable.SalesId) " _
            '                     & " and SalesMasterTable.CustomerCode=" & Val(Me.cmbVendor.ActiveRow.Cells(0).Value) & " and ArticleDefID =" & Me.cmbItem.ActiveRow.Cells(0).Value _
            '                     & " )"

            'objDataAdapter.Fill(objDataSet)


            'If objDataSet.Tables(0).Rows.Count > 0 Then
            '    '    Me.txtLastPrice.Text = objDataSet.Tables(0).Rows(0)(0)
            '    'Else
            '    '    Me.txtLastPrice.Text = 0
            'End If

            ''calculate customer base discont
            'Me.txtDisc.TabStop = True
            'Try
            '    If Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then

            '        strSQl = "select discount from tbldefcustomerbasediscounts where articledefid = " & Me.cmbItem.ActiveRow.Cells(0).Value _
            '        & " and typeid = " & Me.cmbVendor.ActiveRow.Cells("typeid").Value & "  and discount > 0 "

            '        Dim dtdiscount As DataTable = GetDataTable(strSQl)

            '        If Not dtdiscount Is Nothing Then
            '            If dtdiscount.Rows.Count <> 0 Then

            '                Dim disc As Double = 0D
            '                Double.TryParse(dtdiscount.Rows(0)(0).ToString, disc)

            '                Dim price As Double = 0D
            '                Double.TryParse(Me.txtRate.Text, price)

            '                Me.txtRate.Text = price - ((price / 100) * disc)

            '                Me.txtDisc.TabStop = False
            '            Else
            '                If IsSalesOrderAnalysis = True Then
            '                    Me.txtDisc.TabStop = True
            '                End If
            '            End If
            '        End If

            '    End If
        Catch ex As Exception

        End Try


        'objCommand = Nothing
        'objDataAdapter = Nothing
        'objDataSet = Nothing


        'Catch ex As Exception
        '    '  cmbItem.Focus()
        '    ShowErrorMessage(ex.Message)

        'End Try

        'If Not Me.cmbBatchNo.Rows.Count > 0 Then Me.cmbBatchNo.LimitToList = False
        'Me.txtStock.Text = GetItemStock(Me.cmbItem.ActiveRow.Cells(0).Value.ToString)
        'Me.cmbVendor.DisplayLayout.Grid.Show( me.cmbVendor.contr)
        'Me.cmbBatchNo.Enabled = True
    End Sub
    Private Sub cmbItem_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbItem.EnabledChanged
        Try
            Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.CloseDropdown)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRate.LostFocus
        Try
            If cmbUnit.SelectedIndex = 0 Then
                txtPackQty.Text = 1
                txtTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text))
            Else
                txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text))
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.LostFocus
        Try
            If cmbUnit.SelectedIndex = 0 Then
                txtPackQty.Text = 1
                txtTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text))
            Else
                txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPackQty.LostFocus
        Try
            If cmbUnit.SelectedIndex = 0 Then
                txtPackQty.Text = 1
                txtTotal.Text = (Val(txtQty.Text) * Val(txtRate.Text))
            Else
                txtTotal.Text = ((Val(txtQty.Text) * Val(txtPackQty.Text)) * Val(txtRate.Text))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@AgreementId", Me.grdSaved.GetRow.Cells(enmAgreement.AgreementId).Value)
            ShowReport("rptAgreement")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWarnty.TextChanged

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

   
    Private Sub btnAttachments_Click(sender As Object, e As EventArgs) Handles btnAttachments.Click
        Try
            SetAttachments()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS1854
    ''' </summary>
    ''' <remarks> This function handle Attachment on Agreemnet Screen </remarks>
    Private Sub SetAttachments()
        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
            "All files (.)|*.*"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                If Me.BtnSave.Text <> "&Save" Then
                    If Me.grdSaved.RowCount > 0 Then
                        intCountAttachedFiles = Val(grdSaved.CurrentRow.Cells(enmAgreement.No_of_Attachment).Value)
                    End If
                End If
                Me.btnAttachments.Text = "Attachment (" & arrFile.Count + intCountAttachedFiles & ")"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Try
            If e.Column.Key = "No Of Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Val(Me.grdSaved.GetRow.Cells(enmAgreement.AgreementId).Value.ToString)
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVendor_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbVendor.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    frmAccountSearch.AccountType = "'Vendor','Customer' "
                Else
                    frmAccountSearch.AccountType = "'Customer'"
                End If
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbVendor.Value = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
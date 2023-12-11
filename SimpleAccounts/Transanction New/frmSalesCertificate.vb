''11-June-2014 TASK:2678 Imran Ali Add new fields Invoice Amount And Tax Amount On Sales Certificate
''12-June-2014 TASK:2680 Imran Ali Sales Certificate Issued Report
''01-Jul-2014 TASK:2708 Imran Ali Add new fields Registrtion for and sales tax % in Sales Certificate
''05-Jul-2014 TAS:2717 Imran Ali Add new field Reference No In Sales Certificate
''12-Sep-2014 Task:2841 Imran Ali Retrive Last Record In Sales Certificate
''2-Oct-2014 Task:M102141 Imran Ali Add new field of net amount and rounding format
''2015-02-20 Task No #4 Ali Ansari Gettinhg Auto Reference No
'2015-06-12 Task#2015060010 to reset remarks and dc and calculation of net amoutn
''16-6-2015 TASKM166151 Imran Ali Sales Certificate Ledger Problem
'14-Jul-2015 Task#14072015 Ahmad Sharif: Get prefix from database
'29-Aug-2015 Task#29082015 Ahmad Sharif Generate Reference NO from last entered Sales Certificate RefNo

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports SBUtility.Utility
Public Class frmSalesCertificate

    Implements IGeneral
    Dim SalesCertificateId As Integer = 0I
    Dim SalesCertificate As SalesCertificateBE
    Dim SalesId As Integer = 0I
    Dim SalesDetailId As Integer = 0I
    Dim ArticleDefId As Integer = 0I
    Dim IsOpenForm As Boolean = False
    Dim strNoOfRecord As String = String.Empty
    Dim TopRecord As String = String.Empty
    Enum enmSalesCertificate
        SaleCertificateId
        SaleCertificateNo
        SaleCertificateDate
        DeliveredTo
        Engine_No
        Chassis_No
        ArticleDefId
        Model_Desc
        Max_Laden_Weight
        Max_Weight_Front_Axel
        Max_Weight_Rear_Axel
        Tyre_Front_Wheel
        Tyre_Rear_Wheel
        Base_Wheel
        Comments
        SalesId
        SalesDetailId
        UserName
        'Append One New Field ModelCode in Enumeration
        ModelCode
        'Task:2678 Added Index
        InvoiceAmount
        SalesTax
        Address
        NTN
        'Task:2678
        RegistrationFor
        Tax_Percent
        Reference_No 'Task:2717 Added Index
        'Ahmad Sharif : added DC_NO and Remarks
        DC_No
        Remarks
        FatherName
        Person_Cast
        AdvanceAmount
        MeterNo
        Installment
        RegistrationNo
        ContractDate
        Color ''Change color Index bcz giving error
    End Enum

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Me.grdSaved.RootTable.Columns("CustomerCode").Visible = False
            Me.grdSaved.RootTable.Columns("SalesId").Visible = False
            Me.grdSaved.RootTable.Columns("SaleDetailId").Visible = False
            Me.grdSaved.RootTable.Columns("ArticleDefId").Visible = False
            Me.grdSaved.RootTable.Columns("SaleCertificateId").Visible = False
            Me.grdSaved.RootTable.Columns("Detail_Code").Visible = False
            Me.grdSaved.RootTable.Columns("IsReturnedSalesCertificate").Visible = False
            Me.grdSaved.RootTable.Columns("SalesNo").Caption = "Inv No"
            Me.grdSaved.RootTable.Columns("SalesDate").Caption = "Inv Date"
            Me.grdSaved.RootTable.Columns("detail_code").Caption = "Account Code"
            Me.grdSaved.RootTable.Columns("detail_title").Caption = "Customer"
            Me.grdSaved.RootTable.Columns("ArticleCode").Caption = "Item Code"
            Me.grdSaved.RootTable.Columns("ArticleDescription").Caption = "Item Description"
            Me.grdSaved.AutoSizeColumns()
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
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmSales)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                CtrlGrdBar3.mGridChooseFielder.Enabled = False
                'For i As Integer = 0 To Rights.Count - 1
                ''ReqId-924
                If Rights Is Nothing Then Exit Sub
                For Each RightstDt As GroupRights In Rights
                    If RightstDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightstDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightstDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightstDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightstDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf RightstDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar3.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

            '#TODO Delete Record
            '#Delete Function Call And Set Status
            SalesCertificate = New SalesCertificateBE
            SalesCertificate.SaleCertificateId = Me.grdSaved.GetRow.Cells("SaleCertificateId").Value
            SalesCertificate.SalesId = Me.grdSaved.GetRow.Cells("SalesId").Value
            SalesCertificate.SalesDetailId = Me.grdSaved.GetRow.Cells("SaleDetailId").Value
            SalesCertificate.ArticleDefId = Me.grdSaved.GetRow.Cells("ArticleDefId").Value
            If New SalesCertificateDAL().Delete(SalesCertificate) = True Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' This Function is Made to Return the Issued certificate
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReturnSalesCertificate(Optional ByVal Condition As String = "") As Boolean
        Try

            '#TODO Delete Record
            '#Delete Function Call And Set Status
            SalesCertificate = New SalesCertificateBE
            SalesCertificate.SaleCertificateId = Me.grdSaved.GetRow.Cells("SaleCertificateId").Value
            SalesCertificate.SalesId = Me.grdSaved.GetRow.Cells("SalesId").Value
            SalesCertificate.SalesDetailId = Me.grdSaved.GetRow.Cells("SaleDetailId").Value
            SalesCertificate.ArticleDefId = Me.grdSaved.GetRow.Cells("ArticleDefId").Value
            SalesCertificate.SaleCertificateNo = Me.txtCertificateNo.Text
            SalesCertificate.SaleCertificateDate = Me.dtpCertificateDate.Value
            SalesCertificate.ModelCode = Me.txtModelCode.Text
            SalesCertificate.DeliveredTo = Me.txtDeliveredTo.Text
            SalesCertificate.Engine_No = Me.txtCrtEnginNo.Text
            SalesCertificate.Chassis_No = Me.txtCrtChassisNo.Text
            SalesCertificate.Model_Desc = Me.txtModelDesc.Text
            SalesCertificate.Max_Laden_Weight = Me.txtLadenWt.Text
            SalesCertificate.Max_Weight_Front_Axel = Me.txtFrontAxl.Text
            SalesCertificate.Max_Weight_Rear_Axel = Me.txtRearAxel.Text
            SalesCertificate.Tyre_Front_Wheel = Me.txtFrontWheel.Text
            SalesCertificate.Tyre_Rear_Wheel = Me.txtRearWheel.Text
            SalesCertificate.Base_Wheel = Me.txtBaseWheel.Text
            SalesCertificate.UserName = LoginUserName
            SalesCertificate.Comments = Me.txtComments.Text
            'Task:2678 Input Value By User
            SalesCertificate.InvoiceAmount = Val(Me.txtInvoiceAmount.Text)
            SalesCertificate.SalesTax = Val(Me.txtSalesTax.Text)
            SalesCertificate.Address = Me.txtAddress.Text
            SalesCertificate.NTN = Me.txtNTN.Text

            SalesCertificate.DC_NO = Me.txtDC.Text 'Ahmad Sharif: set DCNo property
            SalesCertificate.Remarks = Me.txtRemarks.Text 'Ahmad Sharif: set remarks property
            SalesCertificate.Color = Me.txtColor.Text 'Task:2788 Input Color Name Through User 
            'End Task:2678
            'End Fill Prooperties
            ''01-Jul-2014 TASK:2708 Imran Ali Add new fields Registrtion for and sales tax % in Sales Certificate
            SalesCertificate.RegistrationFor = Me.txtRegistrationFor.Text
            SalesCertificate.Tax_Percent = Val(Me.txtTaxPercent.Text)
            'End Task:2708
            SalesCertificate.Reference_No = Me.txtReferenceNo.Text ' Task:2717 Set Reference No
            SalesCertificate.FatherName = Me.txtFatherName.Text
            If Not Me.cmbCast.Text.Contains("''") Or Not Me.cmbCast.Text.Contains(".... Select Any Value ....") Then
                SalesCertificate.Person_Cast = Me.cmbCast.Text
            Else
                SalesCertificate.Person_Cast = String.Empty
            End If
            SalesCertificate.AdvanceAmount = Val(Me.txtAdvanceAmount.Text)
            SalesCertificate.MeterNo = Me.txtMeterNo.Text
            SalesCertificate.Installment = Val(Me.txtInstallment.Text)
            SalesCertificate.RegistrationNo = Me.txtRegistrationNo.Text
            SalesCertificate.ContractDate = IIf(Me.dtpContractDate.Checked = True, dtpContractDate.Value, DateTime.MinValue)
            If New SalesCertificateDAL().ReturnCertificate(SalesCertificate) = True Then
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

            '@Filled Dropdown
            'FillDropDown(Me.cmbInvoiceNo, "Select SalesId, SalesNo + '/' + Convert(varchar,SalesDate, 102) + '/' + detail_title as SalesNo From SalesMasterTable INNER JOIN vwCOADetail vwCOA on vwCOA.coa_detail_id = SalesMasterTable.CustomerCode ORDER By SalesId DESC ")
            'End Filled Dropdown
            Dim str As String = String.Empty

            If Condition = String.Empty Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                             "dbo.tblListTerritory.TerritoryName as Territory, dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                             "FROM dbo.tblVendor INNER JOIN " & _
                             "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                             "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                             "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                             "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id "
                If getConfigValueByType("Show Vendor On Sales") = "True" Then
                    str += "WHERE  (dbo.vwCOADetail.account_type in('Vendor','Customer')) "
                Else
                    str += "WHERE  (dbo.vwCOADetail.account_type = 'Customer') "
                End If
                'End Task:2637
                'If flgCompanyRights = True Then
                '    Str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                'End If
                'If IsEditMode = False Then
                '    Str += " AND vwCOADetail.Active=1"
                'Else
                '    Str += " AND vwCOADetail.Active in(0,1,Null)"
                'End If
                str += " order by tblVendor.Sortorder, vwCOADetail.detail_title "
                FillUltraDropDown(cmbVendor, str)
                If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" 'Task:2373 Change Caption

                End If
            ElseIf Condition = "Cast" Then
                FillDropDown(Me.cmbCast, "Select Distinct Person_Cast, Person_Cast From SalesCertificateTable where Person_Cast <> ''", False)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            SalesCertificate = New SalesCertificateBE 'Create Instance
            '@ Properties Filled
            SalesCertificate.SaleCertificateId = SalesCertificateId
            SalesCertificate.SaleCertificateNo = Me.txtCertificateNo.Text
            SalesCertificate.SaleCertificateDate = Me.dtpCertificateDate.Value
            SalesCertificate.SalesId = SalesId
            SalesCertificate.SalesDetailId = SalesDetailId
            SalesCertificate.ArticleDefId = ArticleDefId
            'Append one Line Of code for filiing the Model Code Value From Text Box
            SalesCertificate.ModelCode = Me.txtModelCode.Text
            SalesCertificate.DeliveredTo = Me.txtDeliveredTo.Text
            SalesCertificate.Engine_No = Me.txtCrtEnginNo.Text
            SalesCertificate.Chassis_No = Me.txtCrtChassisNo.Text
            SalesCertificate.Model_Desc = Me.txtModelDesc.Text
            SalesCertificate.Max_Laden_Weight = Me.txtLadenWt.Text
            SalesCertificate.Max_Weight_Front_Axel = Me.txtFrontAxl.Text
            SalesCertificate.Max_Weight_Rear_Axel = Me.txtRearAxel.Text
            SalesCertificate.Tyre_Front_Wheel = Me.txtFrontWheel.Text
            SalesCertificate.Tyre_Rear_Wheel = Me.txtRearWheel.Text
            SalesCertificate.Base_Wheel = Me.txtBaseWheel.Text
            SalesCertificate.UserName = LoginUserName
            SalesCertificate.Comments = Me.txtComments.Text
            'Task:2678 Input Value By User
            SalesCertificate.InvoiceAmount = Val(Me.txtInvoiceAmount.Text)
            SalesCertificate.SalesTax = Val(Me.txtSalesTax.Text)
            SalesCertificate.Address = Me.txtAddress.Text
            SalesCertificate.NTN = Me.txtNTN.Text

            SalesCertificate.DC_NO = Me.txtDC.Text 'Ahmad Sharif: set DCNo property
            SalesCertificate.Remarks = Me.txtRemarks.Text 'Ahmad Sharif: set remarks property
            SalesCertificate.Color = Me.txtColor.Text 'Task:2788 Input Color Name Through User 
            'End Task:2678
            'End Fill Prooperties
            ''01-Jul-2014 TASK:2708 Imran Ali Add new fields Registrtion for and sales tax % in Sales Certificate
            SalesCertificate.RegistrationFor = Me.txtRegistrationFor.Text
            SalesCertificate.Tax_Percent = Val(Me.txtTaxPercent.Text)
            'End Task:2708
            SalesCertificate.Reference_No = Me.txtReferenceNo.Text ' Task:2717 Set Reference No
            SalesCertificate.FatherName = Me.txtFatherName.Text
            If Not Me.cmbCast.Text.Contains("''") Or Not Me.cmbCast.Text.Contains(".... Select Any Value ....") Then
                SalesCertificate.Person_Cast = Me.cmbCast.Text
            Else
                SalesCertificate.Person_Cast = String.Empty
            End If
            SalesCertificate.AdvanceAmount = Val(Me.txtAdvanceAmount.Text)
            SalesCertificate.MeterNo = Me.txtMeterNo.Text
            SalesCertificate.Installment = Val(Me.txtInstallment.Text)
            SalesCertificate.RegistrationNo = Me.txtRegistrationNo.Text
            SalesCertificate.ContractDate = IIf(Me.dtpContractDate.Checked = True, dtpContractDate.Value, DateTime.MinValue)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As New DataTable
            'If Me.cmbInvoiceNo.SelectedIndex = -1 Then Exit Sub
            'dt = New SalesCertificateDAL().GetAllRecords(IIf(Me.cmbInvoiceNo.SelectedIndex > 0, Me.cmbInvoiceNo.SelectedValue, 0), Me.txtEngine_No.Text, Me.txtChassisNo.Text)
            ''12-Sep-2014 Task:2841 Imran Ali Retrive Last Record In Sales Certificate
            ''Commented Aganist TFS3524
            ''dt = New SalesCertificateDAL().GetAllRecords(Me.txtInvoiceNo.Text, Me.txtEngine_No.Text, Me.txtChassisNo.Text, Me.rbtPending.Checked, Me.rbtIssued.Checked, Me.cmbVendor.Value, Me.txtSearchRemarks.Text, TopRecord)
            ''Call Changes Against TFS3524 : Ayesha Rehman : Added one more parameter of (Returned Sales Certificate)
            dt = New SalesCertificateDAL().GetAllRecords(Me.txtInvoiceNo.Text, Me.txtEngine_No.Text, Me.txtChassisNo.Text, Me.rbtPending.Checked, Me.rbtIssued.Checked, Me.rbtReturned.Checked, Me.cmbVendor.Value, Me.txtSearchRemarks.Text, TopRecord)
            strNoOfRecord = String.Empty
            'End Task:2841
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns.Add("Column1")
            Me.grdSaved.RootTable.Columns("Column1").UseHeaderSelector = True
            Me.grdSaved.RootTable.Columns("Column1").ActAsSelector = True
            ApplyGridSettings()
            'FormatRows()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            '@To do Validation 
            If Me.dtpCertificateDate.Value = Date.MinValue Then
                ShowErrorMessage("Pleae enter valid date.")
                Me.dtpCertificateDate.Focus()
                Return False
            End If
            If Me.txtCertificateNo.Text = String.Empty Then
                ShowErrorMessage("Please enter valid sale certificate no.")
                Me.txtCertificateNo.Focus()
                Return False
            End If
            If Me.txtDeliveredTo.Text = String.Empty Then
                ShowErrorMessage("Please enter delivered to name.")
                Me.txtDeliveredTo.Focus()
                Return False
            End If
            If Me.txtCrtEnginNo.Text = String.Empty Then
                ShowErrorMessage("Engine No not found.")
                Me.txtCrtEnginNo.Focus()
                Return False
            End If
            'Append New Check Of ModelCode Empty Field 
            If Me.txtModelCode.Text = String.Empty Then
                ShowErrorMessage("ModelCode not found.")
                Me.txtModelCode.Focus()
                Return False
            End If
            'If Me.txtCrtChassisNo.Text = String.Empty Then
            '    ShowErrorMessage("Chassis No not found.")
            '    Me.txtCrtChassisNo.Focus()
            '    Return False
            'End If
            'End Validation
            '''''''''''''''''''''''''''''''''''''''''''''
            'Changes Against Task # 4 Add Auto Ref#
            Dim dt As New DataTable
            dt = GetDataTable("select count(*) from SalesCertificateTable where SaleCertificateId <> " & SalesCertificateId & " and Reference_No = '" & txtReferenceNo.Text & "'")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    ShowErrorMessage("Reference No Already Exists, Enter Valid Reference No")
                    Me.txtReferenceNo.Focus()
                    Return False
                End If
            End If
            'Changes Against Task # 4 Add Auto Ref#
            '''''''''''''''''''''''''''''''''''''''''''         

            FillModel() 'FillModel method call and set filled properties
            Return True '@To do Status Return
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            '@To do reset controls
            Me.btnSave.Text = "&Save"
            SalesCertificateId = 0I
            SalesId = 0I
            SalesDetailId = 0I
            ArticleDefId = 0I
            Me.txtInvoiceNo.Text = String.Empty '"SI1-" & Date.Now.ToString("yy-") & ""
            Me.txtEngine_No.Text = String.Empty
            Me.txtChassisNo.Text = String.Empty
            Me.txtSearchRemarks.Text = String.Empty
            Me.rbtAll.Checked = True
            'If Not Me.cmbInvoiceNo.SelectedIndex = -1 Then Me.cmbInvoiceNo.SelectedIndex = 0
            Me.cmbVendor.Rows(0).Activate()
            Me.dtpCertificateDate.Value = Now.Date
            Me.txtCertificateNo.Text = New SalesCertificateDAL().GetNextDocNo(getConfigValueByType("SaleCertificatePreFix") & "/" & Microsoft.VisualBasic.Right(Me.dtpCertificateDate.Value.Year, 2) & "/")
            ''Task No #4 Ali Ansari Gettinhg Auto Reference No
            'Me.txtReferenceNo.Text = New SalesCertificateDAL().GetNextRefNo("SAPL" & "-" & Microsoft.VisualBasic.Right(Me.dtpCertificateDate.Value.Year, 4) & "-")

            'Task#14072015 Getting Prefix between range
            Dim prefix As String = GetPrefix(Me.dtpCertificateDate.Value, "SC")
            If prefix.Length > 0 Then
                'Me.txtReferenceNo.Text = New SalesCertificateDAL().GetNextRefNo(prefix & "-")
                'Task#29082015 by ahmad sharif
                Me.txtReferenceNo.Text = GetLastSalesCertificate(prefix & "-")
            Else
                'Me.txtReferenceNo.Text = New SalesCertificateDAL().GetNextRefNo("SAPL" & "-" & Microsoft.VisualBasic.Right(Me.dtpCertificateDate.Value.Year, 4) & "-")
                'Task#29082015 by ahmad sharif
                Me.txtReferenceNo.Text = GetLastSalesCertificate("SAPL" & "-" & Microsoft.VisualBasic.Right(Me.dtpCertificateDate.Value.Year, 7) & "-")
            End If
            'End Task#14072015

            ''Task No #4 Ali Ansari Gettinhg Auto Reference No
            Me.txtDeliveredTo.Text = String.Empty
            Me.txtFatherName.Text = String.Empty
            FillCombos("Cast")
            If Not Me.cmbCast.SelectedIndex = -1 Then Me.cmbCast.SelectedIndex = 0
            Me.txtAdvanceAmount.Text = String.Empty
            Me.txtCrtEnginNo.Text = String.Empty
            Me.txtCrtChassisNo.Text = String.Empty
            Me.txtModelDesc.Text = String.Empty
            'Add Code To reset the ModelCode Feild 
            Me.txtModelCode.Text = String.Empty
            Me.txtLadenWt.Text = String.Empty
            Me.txtFrontAxl.Text = String.Empty
            Me.txtRearAxel.Text = String.Empty
            Me.txtFrontWheel.Text = String.Empty
            Me.txtRearWheel.Text = String.Empty
            Me.txtBaseWheel.Text = String.Empty
            Me.txtComments.Text = String.Empty
            'Task:2678 Reseting Values
            Me.txtInvoiceAmount.Text = String.Empty
            Me.txtSalesTax.Text = String.Empty
            Me.txtAddress.Text = String.Empty
            Me.txtNTN.Text = String.Empty
            'End Task:2678
            Me.btnDelete.Enabled = False
            Me.btnPrint.Enabled = False
            'TAsk:2708 Resting Controls 
            Me.txtRegistrationFor.Text = String.Empty
            Me.txtTaxPercent.Text = String.Empty
            'End Task:2708
            ' Me.txtReferenceNo.Text = String.Empty
            Me.txtColor.Text = String.Empty 'Task:2788 Reseting Color Control
            Me.txtDeliveredTo.Focus()
            Me.txtRemarks.Text = String.Empty ' Task#2015060010 to reset remarks and dc
            Me.txtDC.Text = String.Empty ' Task#2015060010 to reset remarks and dc
            Me.txtInstallment.Text = String.Empty
            Me.txtRegistrationNo.Text = String.Empty
            Me.dtpContractDate.Value = DateTime.Now
            Me.dtpContractDate.Checked = False
            Me.txtMeterNo.Text = String.Empty
            Me.btnAgreementLetter.Enabled = True
            TopRecord = "50"
            GetAll()
            ApplySecurity(EnumDataMode.[New])
            'End reset controls
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            '@TODO Save Record
            '@Save Function And Set Status
            If New SalesCertificateDAL().Save(SalesCertificate) = True Then
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
            '@TODO Update Record
            '@Update Function Call And Set Status
            If New SalesCertificateDAL().Update(SalesCertificate) = True Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then 'IsValidate Function Call And Set Status
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then 'Set Save Status
                    '@To do Save Record
                    '@Save Function Call And Set Status
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls() ' Reset Controls
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub '@To do Confirmation
                    '@To do Save Record
                    '@Update Function Call And Set Status
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls() ' Reset Controls
                End If
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If SalesCertificateId = 0 Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub '@To do Confirmation
            '@To do delete record
            '@Delete function call and set status
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls() 'Reset Controls

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmSalesCertificate_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim lbl As New Label
        lbl.Dock = DockStyle.Fill
        lbl.Text = "Loading, please wait..."
        Me.Controls.Add(lbl)
        lbl.Visible = True
        lbl.BringToFront()
        Application.DoEvents()
        Try
            FillCombos()
            ReSetControls()
            IsOpenForm = True
            'Task:2680 Load Certificate Data From Certificate Issued Report
            If Me.Tag IsNot Nothing Then
                If Me.Tag.ToString.Length > 0 Then
                    SalesCertificateId = Val(Me.Tag.ToString)
                    Me.grdSaved_DoubleClick(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Tag = String.Empty
            'End Task:2680
            lbl.Visible = False
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            TopRecord = String.Empty
            GetAllRecords()
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

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombos()
            Me.cmbVendor.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_Click(sender As Object, e As EventArgs) ''Handles grdSaved.Click
        Try
            ' ''Start TFS3524
            'If CBool(Me.grdSaved.GetRow.Cells("IsReturnedSalesCertificate").Value) = False And Me.grdSaved.GetRow.Cells("SaleCertificateId").Value = 0 Then
            '    Me.btnReturn.Enabled = False
            '    Me.btnDelete.Enabled = True
            '    Me.btnSave.Enabled = True
            'ElseIf CBool(Me.grdSaved.GetRow.Cells("IsReturnedSalesCertificate").Value) = False And Me.grdSaved.GetRow.Cells("SaleCertificateId").Value > 0 Then
            '    Me.btnReturn.Enabled = True
            '    Me.btnDelete.Enabled = True
            '    Me.btnSave.Enabled = True
            'ElseIf CBool(Me.grdSaved.GetRow.Cells("IsReturnedSalesCertificate").Value) = True And Me.grdSaved.GetRow.Cells("SaleCertificateId").Value = 0 Then
            '    Me.btnReturn.Enabled = False
            '    Me.btnDelete.Enabled = False
            '    Me.btnSave.Enabled = False
            'Else
            '    Me.btnReturn.Enabled = False
            'End If
            ' ''End TFS3524
            ''Start TFS3524
            If Me.grdSaved.GetRow.Cells("InReturn").Value > 0 Then
                Me.btnReturn.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnSave.Enabled = False
            ElseIf Me.grdSaved.GetRow.Cells("SaleCertificateId").Value = 0 Then
                Me.btnReturn.Enabled = False
                Me.btnDelete.Enabled = True
                Me.btnSave.Enabled = True
            ElseIf Me.grdSaved.GetRow.Cells("SaleCertificateId").Value > 0 Then
                Me.btnReturn.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnSave.Enabled = True
            End If
            'End TFS3524
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Dim strSQL As String = String.Empty
            'If SalesCertificateId > 0 Then
            '    Me.grdSaved.GetRow.Cells("SaleCertificateId").Value = SalesCertificateId
            'End If

            ' ''Start TFS3524
            'If Not IsDBNull(Me.grdSaved.GetRow.Cells("IsReturnedSalesCertificate").Value) Then
            '    If CBool(Me.grdSaved.GetRow.Cells("IsReturnedSalesCertificate").Value) = True Then
            '        Me.btnReturn.Enabled = False
            '    Else
            '        Me.btnReturn.Enabled = True
            '    End If
            'Else
            '    Me.btnReturn.Enabled = True
            'End If
            ' ''End TFS3524
            If Me.grdSaved.GetRow.Cells("InReturn").Value <= 0 Then
                strSQL = "Select * From SalesCertificateTable WHERE SaleCertificateId=" & Val(Me.grdSaved.GetRow.Cells("SaleCertificateId").Value.ToString)
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                strSQL = String.Empty
                strSQL = "Select * from SalesCertificateTable WHERE SaleCertificateId IN(Select Max(SaleCertificateId) From SalesCertificateTable)"
                Dim dt1 As New DataTable
                dt1 = GetDataTable(strSQL)

                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.btnSave.Text = "&Update"
                        Me.btnAgreementLetter.Enabled = True
                        Me.btnDelete.Enabled = True
                        Me.btnPrint.Enabled = True
                        SalesCertificateId = Val(dt.Rows(0).Item(enmSalesCertificate.SaleCertificateId).ToString)
                        Me.txtCertificateNo.Text = dt.Rows(0).Item(enmSalesCertificate.SaleCertificateNo).ToString
                        Me.dtpCertificateDate.Value = dt.Rows(0).Item(enmSalesCertificate.SaleCertificateDate)
                        Me.txtDeliveredTo.Text = dt.Rows(0).Item(enmSalesCertificate.DeliveredTo).ToString
                        Me.txtCrtEnginNo.Text = dt.Rows(0).Item(enmSalesCertificate.Engine_No).ToString
                        Me.txtCrtChassisNo.Text = dt.Rows(0).Item(enmSalesCertificate.Chassis_No).ToString
                        'Append One Line Code For ModelCode Field to Get the Dtaa In Text Box Of Model Code 
                        Me.txtModelCode.Text = dt.Rows(0).Item(enmSalesCertificate.ModelCode).ToString
                        Me.txtModelDesc.Text = dt.Rows(0).Item(enmSalesCertificate.Model_Desc).ToString
                        Me.txtLadenWt.Text = dt.Rows(0).Item(enmSalesCertificate.Max_Laden_Weight).ToString
                        Me.txtFrontAxl.Text = dt.Rows(0).Item(enmSalesCertificate.Max_Weight_Front_Axel).ToString
                        Me.txtRearAxel.Text = dt.Rows(0).Item(enmSalesCertificate.Max_Weight_Rear_Axel).ToString
                        Me.txtFrontWheel.Text = dt.Rows(0).Item(enmSalesCertificate.Tyre_Front_Wheel).ToString
                        Me.txtRearWheel.Text = dt.Rows(0).Item(enmSalesCertificate.Tyre_Rear_Wheel).ToString
                        Me.txtBaseWheel.Text = dt.Rows(0).Item(enmSalesCertificate.Base_Wheel).ToString
                        SalesId = Val(dt.Rows(0).Item(enmSalesCertificate.SalesId).ToString)
                        'TASKM166151 SaleDetailId which is sales invoice
                        SalesDetailId = Val(Me.grdSaved.GetRow.Cells("SaleDetailId").Value.ToString) 'Val(dt.Rows(0).Item(enmSalesCertificate.SalesDetailId).ToString)
                        ArticleDefId = Val(dt.Rows(0).Item(enmSalesCertificate.ArticleDefId).ToString)
                        Me.txtComments.Text = dt.Rows(0).Item(enmSalesCertificate.Comments).ToString
                        'Task:2678 Get Values From Double Clicking
                        Me.txtInvoiceAmount.Text = Val(dt.Rows(0).Item(enmSalesCertificate.InvoiceAmount).ToString)
                        Me.txtSalesTax.Text = Val(dt.Rows(0).Item(enmSalesCertificate.SalesTax).ToString)
                        Me.txtAddress.Text = dt.Rows(0).Item(enmSalesCertificate.Address).ToString
                        Me.txtNTN.Text = dt.Rows(0).Item(enmSalesCertificate.NTN).ToString
                        'End Task:2678
                        'Ahmad(Sharif) : added(txtDC And txtRemarks)
                        Me.txtDC.Text = IIf(dt.Rows(0).Item(enmSalesCertificate.DC_No).ToString = "", Me.grdSaved.GetRow.Cells("DcNo").Value.ToString, dt.Rows(0).Item(enmSalesCertificate.DC_No).ToString)
                        Me.txtRemarks.Text = IIf(dt.Rows(0).Item(enmSalesCertificate.Remarks).ToString = "", Me.grdSaved.GetRow.Cells("Remarks").Value.ToString, dt.Rows(0).Item(enmSalesCertificate.Remarks).ToString)
                        ''01-Jul-2014 TASK:2708 Imran Ali Add new fields Registrtion for and sales tax % in Sales Certificate
                        Me.txtRegistrationFor.Text = dt.Rows(0).Item(enmSalesCertificate.RegistrationFor).ToString
                        Me.txtTaxPercent.Text = Val(dt.Rows(0).Item(enmSalesCertificate.Tax_Percent).ToString)
                        'End Task:2708
                        Me.txtReferenceNo.Text = dt.Rows(0).Item(enmSalesCertificate.Reference_No).ToString
                        Me.txtColor.Text = dt.Rows(0).Item(enmSalesCertificate.Color).ToString 'Task:2788 Get Color Name By Double Clicking Grid
                        Me.txtFatherName.Text = dt.Rows(0).Item(enmSalesCertificate.FatherName).ToString
                        Me.cmbCast.Text = dt.Rows(0).Item(enmSalesCertificate.Person_Cast).ToString
                        Me.txtAdvanceAmount.Text = Val(dt.Rows(0).Item(enmSalesCertificate.AdvanceAmount).ToString)
                        Me.txtMeterNo.Text = dt.Rows(0).Item(enmSalesCertificate.MeterNo).ToString
                        Me.txtRegistrationNo.Text = dt.Rows(0).Item(enmSalesCertificate.RegistrationNo).ToString
                        Me.txtInstallment.Text = Val(dt.Rows(0).Item(enmSalesCertificate.Installment).ToString)
                        If Not IsDBNull(dt.Rows(0).Item(enmSalesCertificate.ContractDate)) Then
                            Me.dtpContractDate.Value = dt.Rows(0).Item(enmSalesCertificate.ContractDate)
                            Me.dtpContractDate.Checked = True
                        Else
                            Me.dtpContractDate.Value = DateTime.Now
                            Me.dtpContractDate.Checked = False
                        End If
                        Me.txtDeliveredTo.Focus()
                    Else
                        Me.btnSave.Text = "&Save"
                        Me.btnDelete.Enabled = False
                        Me.btnPrint.Enabled = False
                        SalesCertificateId = Val(Me.grdSaved.GetRow.Cells("SaleCertificateId").Value.ToString)
                        'Me.txtCertificateNo.Text = dt.Rows(0).Item(enmSalesCertificate.SaleCertificateNo).ToString
                        'Me.dtpCertificateDate.Value = dt.Rows(0).Item(enmSalesCertificate.SaleCertificateDate)
                        'Me.txtDeliveredTo.Text = dt.Rows(0).Item(enmSalesCertificate.DeliveredTo).ToString

                        Me.txtDC.Text = Me.grdSaved.GetRow.Cells("DcNO").Value.ToString
                        Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString
                        Me.txtCrtEnginNo.Text = Me.grdSaved.GetRow.Cells("Engine_No").Value.ToString
                        Me.txtCrtChassisNo.Text = Me.grdSaved.GetRow.Cells("Chassis_No").Value.ToString
                        Me.txtColor.Text = Me.grdSaved.GetRow.Cells("Other Comments").Value.ToString
                        'Add Code OF One Line To Add Filed of ModelCode
                        'Me.txtModelCode.Text = Me.grdSaved.GetRow.Cells("ModelCode").Value.ToString
                        'Me.txtModelDesc.Text = dt.Rows(0).Item(enmSalesCertificate.Model_Desc).ToString
                        If dt1 IsNot Nothing Then
                            If dt1.Rows.Count > 0 Then

                                'Me.txtCrtEnginNo.Text = Me.grdSaved.GetRow.Cells("Engine_No").Value.ToString 'dt1.Rows(0).Item(enmSalesCertificate.Engine_No).ToString
                                'Me.txtCrtChassisNo.Text = Me.grdSaved.GetRow.Cells("Chassis_No").Value.ToString 'dt1.Rows(0).Item(enmSalesCertificate.Chassis_No).ToString
                                Me.txtRegistrationFor.Text = dt1.Rows(0).Item(enmSalesCertificate.RegistrationFor).ToString
                                'Me.txtColor.Text = dt1.Rows(0).Item(enmSalesCertificate.Color).ToString
                                Me.txtModelCode.Text = dt1.Rows(0).Item(enmSalesCertificate.ModelCode).ToString
                                Me.txtModelDesc.Text = dt1.Rows(0).Item(enmSalesCertificate.Model_Desc).ToString
                                Me.txtComments.Text = dt1.Rows(0).Item(enmSalesCertificate.Comments).ToString
                                Me.txtTaxPercent.Text = Val(dt1.Rows(0).Item(enmSalesCertificate.Tax_Percent).ToString)
                                Me.txtInvoiceAmount.Text = Val(dt1.Rows(0).Item(enmSalesCertificate.InvoiceAmount).ToString)
                                Me.txtSalesTax.Text = Val(dt1.Rows(0).Item(enmSalesCertificate.SalesTax).ToString)

                                Me.txtLadenWt.Text = dt1.Rows(0).Item(enmSalesCertificate.Max_Laden_Weight).ToString
                                Me.txtFrontAxl.Text = dt1.Rows(0).Item(enmSalesCertificate.Max_Weight_Front_Axel).ToString
                                Me.txtRearAxel.Text = dt1.Rows(0).Item(enmSalesCertificate.Max_Weight_Rear_Axel).ToString
                                Me.txtFrontWheel.Text = dt1.Rows(0).Item(enmSalesCertificate.Tyre_Front_Wheel).ToString
                                Me.txtRearWheel.Text = dt1.Rows(0).Item(enmSalesCertificate.Tyre_Rear_Wheel).ToString
                                Me.txtBaseWheel.Text = dt1.Rows(0).Item(enmSalesCertificate.Base_Wheel).ToString

                            End If
                        End If
                        SalesId = Val(Me.grdSaved.GetRow.Cells("SalesId").Value.ToString)
                        SalesDetailId = Val(Me.grdSaved.GetRow.Cells("SaleDetailId").Value.ToString)
                        ArticleDefId = Val(Me.grdSaved.GetRow.Cells("ArticleDefId").Value.ToString)
                        Me.txtDeliveredTo.Focus()
                    End If
                End If
            Else
                strSQL = "Select * From SalesCertificateReturnTable WHERE SaleCertificateReturnId=" & Val(Me.grdSaved.GetRow.Cells("SaleCertificateReturnId").Value.ToString)
                Dim dtReturn As New DataTable
                dtReturn = GetDataTable(strSQL)
                strSQL = String.Empty
                strSQL = "Select * from SalesCertificateReturnTable WHERE SaleCertificateReturnId IN(Select Max(SaleCertificateReturnId) From SalesCertificateReturnTable)"
                Dim dt1Return As New DataTable
                dt1Return = GetDataTable(strSQL)

                If dtReturn IsNot Nothing Then
                    If dtReturn.Rows.Count > 0 Then
                        Me.btnSave.Text = "&Update"
                        Me.btnAgreementLetter.Enabled = True
                        SalesCertificateId = Val(dtReturn.Rows(0).Item(enmSalesCertificate.SaleCertificateId).ToString)
                        Me.txtCertificateNo.Text = dtReturn.Rows(0).Item(enmSalesCertificate.SaleCertificateNo).ToString
                        Me.dtpCertificateDate.Value = dtReturn.Rows(0).Item(enmSalesCertificate.SaleCertificateDate)
                        Me.txtDeliveredTo.Text = dtReturn.Rows(0).Item(enmSalesCertificate.DeliveredTo).ToString
                        Me.txtCrtEnginNo.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Engine_No).ToString
                        Me.txtCrtChassisNo.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Chassis_No).ToString
                        'Append One Line Code For ModelCode Field to Get the Dtaa In Text Box Of Model Code 
                        Me.txtModelCode.Text = dtReturn.Rows(0).Item(enmSalesCertificate.ModelCode).ToString
                        Me.txtModelDesc.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Model_Desc).ToString
                        Me.txtLadenWt.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Max_Laden_Weight).ToString
                        Me.txtFrontAxl.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Max_Weight_Front_Axel).ToString
                        Me.txtRearAxel.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Max_Weight_Rear_Axel).ToString
                        Me.txtFrontWheel.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Tyre_Front_Wheel).ToString
                        Me.txtRearWheel.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Tyre_Rear_Wheel).ToString
                        Me.txtBaseWheel.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Base_Wheel).ToString
                        SalesId = Val(dtReturn.Rows(0).Item(enmSalesCertificate.SalesId).ToString)
                        'TASKM166151 SaleDetailId which is sales invoice
                        SalesDetailId = Val(Me.grdSaved.GetRow.Cells("SaleDetailId").Value.ToString) 'Val(dt.Rows(0).Item(enmSalesCertificate.SalesDetailId).ToString)
                        ArticleDefId = Val(dtReturn.Rows(0).Item(enmSalesCertificate.ArticleDefId).ToString)
                        Me.txtComments.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Comments).ToString
                        'Task:2678 Get Values From Double Clicking
                        Me.txtInvoiceAmount.Text = Val(dtReturn.Rows(0).Item(enmSalesCertificate.InvoiceAmount).ToString)
                        Me.txtSalesTax.Text = Val(dtReturn.Rows(0).Item(enmSalesCertificate.SalesTax).ToString)
                        Me.txtAddress.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Address).ToString
                        Me.txtNTN.Text = dtReturn.Rows(0).Item(enmSalesCertificate.NTN).ToString
                        'End Task:2678
                        'Ahmad(Sharif) : added(txtDC And txtRemarks)
                        Me.txtDC.Text = IIf(dtReturn.Rows(0).Item(enmSalesCertificate.DC_No).ToString = "", Me.grdSaved.GetRow.Cells("DcNo").Value.ToString, dtReturn.Rows(0).Item(enmSalesCertificate.DC_No).ToString)
                        Me.txtRemarks.Text = IIf(dtReturn.Rows(0).Item(enmSalesCertificate.Remarks).ToString = "", Me.grdSaved.GetRow.Cells("Remarks").Value.ToString, dtReturn.Rows(0).Item(enmSalesCertificate.Remarks).ToString)
                        ''01-Jul-2014 TASK:2708 Imran Ali Add new fields Registrtion for and sales tax % in Sales Certificate
                        Me.txtRegistrationFor.Text = dtReturn.Rows(0).Item(enmSalesCertificate.RegistrationFor).ToString
                        Me.txtTaxPercent.Text = Val(dtReturn.Rows(0).Item(enmSalesCertificate.Tax_Percent).ToString)
                        'End Task:2708
                        Me.txtReferenceNo.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Reference_No).ToString
                        Me.txtColor.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Color).ToString 'Task:2788 Get Color Name By Double Clicking Grid
                        Me.txtFatherName.Text = dtReturn.Rows(0).Item(enmSalesCertificate.FatherName).ToString
                        Me.cmbCast.Text = dtReturn.Rows(0).Item(enmSalesCertificate.Person_Cast).ToString
                        Me.txtAdvanceAmount.Text = Val(dtReturn.Rows(0).Item(enmSalesCertificate.AdvanceAmount).ToString)
                        Me.txtMeterNo.Text = dtReturn.Rows(0).Item(enmSalesCertificate.MeterNo).ToString
                        Me.txtRegistrationNo.Text = dtReturn.Rows(0).Item(enmSalesCertificate.RegistrationNo).ToString
                        Me.txtInstallment.Text = Val(dtReturn.Rows(0).Item(enmSalesCertificate.Installment).ToString)
                        If Not IsDBNull(dtReturn.Rows(0).Item(enmSalesCertificate.ContractDate)) Then
                            Me.dtpContractDate.Value = dtReturn.Rows(0).Item(enmSalesCertificate.ContractDate)
                            Me.dtpContractDate.Checked = True
                        Else
                            Me.dtpContractDate.Value = DateTime.Now
                            Me.dtpContractDate.Checked = False
                        End If
                        Me.txtDeliveredTo.Focus()
                    End If
                End If
            End If

            ApplySecurity(EnumDataMode.Edit)
            ' ''Start TFS3524
            'If CBool(Me.grdSaved.GetRow.Cells("IsReturnedSalesCertificate").Value) = False And Me.grdSaved.GetRow.Cells("SaleCertificateId").Value = 0 And Me.grdSaved.GetRow.Cells("InReturn").Value = 0 Then
            '    Me.btnReturn.Enabled = False
            '    Me.btnDelete.Enabled = True
            '    Me.btnSave.Enabled = True
            'ElseIf CBool(Me.grdSaved.GetRow.Cells("IsReturnedSalesCertificate").Value) = False And Me.grdSaved.GetRow.Cells("SaleCertificateId").Value > 0 And rbtIssued.Checked = True Then
            '    Me.btnReturn.Enabled = True
            '    Me.btnDelete.Enabled = True
            '    Me.btnSave.Enabled = True
            'ElseIf CBool(Me.grdSaved.GetRow.Cells("IsReturnedSalesCertificate").Value) = True And Me.grdSaved.GetRow.Cells("SaleCertificateId").Value > 0 And rbtReturned.Checked = False Then
            '    Me.btnReturn.Enabled = True
            '    Me.btnDelete.Enabled = True
            '    Me.btnSave.Enabled = True
            'ElseIf CBool(Me.grdSaved.GetRow.Cells("IsReturnedSalesCertificate").Value) = True And Me.grdSaved.GetRow.Cells("SaleCertificateId").Value = 0 And rbtReturned.Checked = True Then
            '    Me.btnReturn.Enabled = False
            '    Me.btnDelete.Enabled = False
            '    Me.btnSave.Enabled = False
            'ElseIf CBool(Me.grdSaved.GetRow.Cells("IsReturnedSalesCertificate").Value) = True And Me.grdSaved.GetRow.Cells("SaleCertificateId").Value = 0 And rbtPending.Checked = True Then
            '    Me.btnReturn.Enabled = False
            '    Me.btnDelete.Enabled = True
            '    Me.btnSave.Enabled = True
            'ElseIf CBool(Me.grdSaved.GetRow.Cells("IsReturnedSalesCertificate").Value) = True And Me.grdSaved.GetRow.Cells("SaleCertificateReturnId").Value > 0 And rbtReturned.Checked = True Then
            '    Me.btnReturn.Enabled = False
            '    Me.btnDelete.Enabled = False
            '    Me.btnSave.Enabled = False
            'ElseIf Me.grdSaved.GetRow.Cells("InReturn").Value > 0 And rbtAll.Checked = True Then
            '    Me.btnReturn.Enabled = False
            '    Me.btnDelete.Enabled = False
            '    Me.btnSave.Enabled = False
            'Else
            '    Me.btnReturn.Enabled = False
            '    Me.btnDelete.Enabled = True
            '    Me.btnSave.Enabled = True
            'End If
            ''End TFS3524

            ''Start TFS3524
            If Me.grdSaved.GetRow.Cells("InReturn").Value > 0 Then
                Me.btnReturn.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnSave.Enabled = False
            ElseIf Me.grdSaved.GetRow.Cells("SaleCertificateId").Value = 0 Then
                Me.btnReturn.Enabled = False
                Me.btnDelete.Enabled = True
                Me.btnSave.Enabled = True
            ElseIf Me.grdSaved.GetRow.Cells("SaleCertificateId").Value > 0 Then
                Me.btnReturn.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnSave.Enabled = True
            End If
            'End TFS3524


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FormatRows()
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
                r.BeginEdit()
                Dim rowStyle As Janus.Windows.GridEX.GridEXFormatStyle
                If r.Cells("SaleCertificateId").Value <> 0 Then
                    rowStyle = New Janus.Windows.GridEX.GridEXFormatStyle
                    rowStyle.BackColor = Color.LightGreen
                    r.RowStyle = rowStyle
                Else
                    rowStyle = New Janus.Windows.GridEX.GridEXFormatStyle
                    rowStyle.BackColor = Color.White
                    r.RowStyle = rowStyle
                End If
                r.EndEdit()
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPrintCertificate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintCertificate.ButtonClick
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            If Me.grdSaved.GetRow.Cells("InReturn").Value <= 0 Then
                AddRptParam("@CertificateId", Val(Me.grdSaved.GetRow.Cells("SaleCertificateId").Value.ToString))
                ShowReport("RptSalesCertificate")
            Else
                AddRptParam("@CertificateReturnId", Val(Me.grdSaved.GetRow.Cells("SaleCertificateReturnId").Value.ToString))
                ShowReport("RptSalesCertificateReturn")
            End If
            'Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If grdSaved.RowCount = 0 Then Exit Sub
            If Me.grdSaved.GetRow.Cells("InReturn").Value <= 0 Then
                AddRptParam("@CertificateId", Val(Me.grdSaved.GetRow.Cells("SaleCertificateId").Value.ToString))
                ShowReport("RptSalesCertificate", , , , False)
            Else
                AddRptParam("@CertificateReturnId", Val(Me.grdSaved.GetRow.Cells("SaleCertificateReturnId").Value.ToString))
                ShowReport("RptSalesCertificateReturn", , , , False)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintSalesInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintSalesInvoiceToolStripMenuItem.Click
        Try

            If grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@CertificateId", Val(Me.grdSaved.GetRow.Cells("SaleCertificateId").Value.ToString))
            ShowReport("rptManualSalesInvoice", , , , False)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PrintSalesInvoiceWithDealerToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PrintSalesInvoiceWithDealerToolStripMenuItem.Click
        Try

            If grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@CertificateId", Val(Me.grdSaved.GetRow.Cells("SaleCertificateId").Value.ToString))
            ShowReport("RptSalesCertificateDealer", , , , False)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtTaxPercent_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTaxPercent.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTaxPercent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTaxPercent.TextChanged
        Try

            If Me.IsOpenForm = True Then
                Me.txtSalesTax.Text = Math.Round((Val(Me.txtTaxPercent.Text) / 100) * Val(Me.txtInvoiceAmount.Text), DecimalPointInValue)
                ''2-Oct-2014 Task:M102141 Imran Ali Add new field of net amount and rounding format
                Me.txtNetAmount.Text = Math.Round((Val(Me.txtSalesTax.Text) + Val(txtInvoiceAmount.Text)), DecimalPointInValue)
                'End Task:M102141
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtInvoiceAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInvoiceAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtSalesTax_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSalesTax.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''12-Sep-2014 Task:2841 Imran Ali Retrive Last Record In Sales Certificate
    Private Sub grdSaved_Error(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ErrorEventArgs) Handles grdSaved.Error
        Try
            e.DisplayErrorMessage = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rbtIssued_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtIssued.CheckedChanged
        Try
            If IsOpenForm = True Then
                GetAllRecords()
                If Me.rbtIssued.Checked = True Then
                    Me.btnReturn.Enabled = True  ''TFS3524
                    Me.btnSave.Enabled = True
                    Me.btnDelete.Enabled = True
                Else
                    Me.btnReturn.Enabled = False   ''TFS3524
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtPending_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtPending.CheckedChanged
        Try
            If IsOpenForm = True Then
                GetAllRecords()
                If Me.rbtPending.Checked = True Then
                    Me.btnReturn.Enabled = False ''TFS3524
                    Me.btnSave.Enabled = True
                    Me.btnDelete.Enabled = True
                Else
                    Me.btnReturn.Enabled = True
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2841


    Private Sub txtInvoiceAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInvoiceAmount.TextChanged
        'Altered against Task 2015060010 to rectify total error
        Try
            If Me.IsOpenForm = True Then
                Me.txtSalesTax.Text = Math.Round((Val(Me.txtTaxPercent.Text) / 100) * Val(Me.txtInvoiceAmount.Text), DecimalPointInValue)
                ''2-Oct-2014 Task:M102141 Imran Ali Add new field of net amount and rounding format
                Me.txtNetAmount.Text = Math.Round((Val(Me.txtSalesTax.Text) + Val(txtInvoiceAmount.Text)), DecimalPointInValue)
                'End Task:M102141
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Altered against Task 2015060010 to rectify total error
    'Task#14072015 On leave date picker, get prefix
    Private Sub dtpCertificateDate_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpCertificateDate.Leave
        Try
            Dim prefix As String = GetPrefix(Me.dtpCertificateDate.Value, "SC")
            If prefix.Length > 0 Then
                'Me.txtReferenceNo.Text = New SalesCertificateDAL().GetNextRefNo(prefix & "-")
                'Task#29082015 by ahmad sharif
                Me.txtReferenceNo.Text = GetLastSalesCertificate(prefix & "-")
            Else
                'Me.txtReferenceNo.Text = New SalesCertificateDAL().GetNextRefNo("SAPL" & "-" & Microsoft.VisualBasic.Right(Me.dtpCertificateDate.Value.Year, 4) & "-")
                'Task#29082015 by ahmad sharif
                Me.txtReferenceNo.Text = GetLastSalesCertificate("SAPL" & "-" & Microsoft.VisualBasic.Right(Me.dtpCertificateDate.Value.Year, 4) & "-")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task#14072015
    Private Sub btnAgreementLetter_Click(sender As Object, e As EventArgs) Handles btnAgreementLetter.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@CertificateId", Val(Me.grdSaved.GetRow.Cells("SaleCertificateId").Value.ToString))
            ShowReport("rptVehicleAgreementLatter", , , , False)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtAdvanceAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAdvanceAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtInstallment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInstallment.KeyPress
        Try
            NumValidation(sender, e)
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
                control = "frmSalesCertificat"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdSaved.CurrentRow.Cells(6).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Sale Certificate (" & frmtask.Ref_No & ") "
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
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Sales
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Certificate"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtReturned_CheckedChanged(sender As Object, e As EventArgs) Handles rbtReturned.CheckedChanged
        Try
            If IsOpenForm = True Then
                If Me.rbtReturned.Checked = True Then
                    Dim dt As New DataTable
                    dt = New SalesCertificateDAL().GetAllRecordsofReturn(Me.txtInvoiceNo.Text, Me.txtEngine_No.Text, Me.txtChassisNo.Text, Me.cmbVendor.Value, Me.txtSearchRemarks.Text, TopRecord)
                    strNoOfRecord = String.Empty
                    'End Task:2841
                    Me.grdSaved.DataSource = dt
                    Me.grdSaved.RetrieveStructure()
                    Me.grdSaved.RootTable.Columns.Add("Column1")
                    Me.grdSaved.RootTable.Columns("Column1").UseHeaderSelector = True
                    Me.grdSaved.RootTable.Columns("Column1").ActAsSelector = True
                    ApplyGridSettings()
                Else
                    GetAllRecords()
                End If

                ''Start TFS3524
                If Me.rbtReturned.Checked = True Then
                    Me.btnReturn.Enabled = False
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False

                Else
                    Me.rbtReturned.Enabled = True
                End If
                ''End TFS3524
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnReturn_Click(sender As Object, e As EventArgs) Handles btnReturn.Click
        Try

            If SalesCertificateId = 0 Then Exit Sub
            If Not msg_Confirm("Do you want to Return") = True Then Exit Sub '@To do Confirmation
            '@To do delete record
            '@Delete function call and set status
            If ReturnSalesCertificate() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls() 'Reset Controls

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSaved.FormattingRow
        Try
            'If e.Row.Cells("IsReturnedSalesCertificate").Value = True And e.Row.Cells("SaleCertificateReturnId").Value > 0 And rbtReturned.Checked = True Then
            '    Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
            '    rowstyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
            '    e.Row.RowStyle = rowstyle
            'ElseIf e.Row.Cells("InReturn").Value > 0 And rbtAll.Checked = True Then
            '    Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
            '    rowstyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
            '    e.Row.RowStyle = rowstyle
            'End If
            If e.Row.Cells("InReturn").Value > 0 Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.FontStrikeout = Janus.Windows.GridEX.TriState.True
                e.Row.RowStyle = rowstyle
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtAll_CheckedChanged(sender As Object, e As EventArgs) Handles rbtAll.CheckedChanged
        Try

            If Me.rbtAll.Checked = True Then
                GetAll()
            Else
                GetAllRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetAll()
        Try
            Dim dt As New DataTable
            dt = New SalesCertificateDAL().GetAll(Me.txtInvoiceNo.Text, Me.txtEngine_No.Text, Me.txtChassisNo.Text, Me.cmbVendor.Value, Me.txtSearchRemarks.Text, TopRecord)
            strNoOfRecord = String.Empty
            'End Task:2841
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns.Add("Column1")
            Me.grdSaved.RootTable.Columns("Column1").UseHeaderSelector = True
            Me.grdSaved.RootTable.Columns("Column1").ActAsSelector = True
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' This function is created against TASK TFS4044 ON 01-08-18
    ''' </summary>
    ''' <param name="SalesCertificateId"></param>
    ''' <remarks></remarks>
    Public Sub EditRecord(ByVal SalesCertificateId As Integer)
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Select * From SalesCertificateTable WHERE SaleCertificateId=" & SalesCertificateId
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            'strSQL = String.Empty
            'strSQL = "Select * from SalesCertificateTable WHERE SaleCertificateId IN(Select Max(SaleCertificateId) From SalesCertificateTable)"
            'Dim dt1 As New DataTable
            'dt1 = GetDataTable(strSQL)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Me.btnSave.Text = "&Update"
                    Me.rbtIssued.Checked = True
                    Me.btnAgreementLetter.Enabled = True
                    Me.btnDelete.Enabled = True
                    Me.btnPrint.Enabled = True
                    SalesCertificateId = Val(dt.Rows(0).Item(enmSalesCertificate.SaleCertificateId).ToString)
                    Me.txtCertificateNo.Text = dt.Rows(0).Item(enmSalesCertificate.SaleCertificateNo).ToString
                    Me.dtpCertificateDate.Value = dt.Rows(0).Item(enmSalesCertificate.SaleCertificateDate)
                    Me.txtDeliveredTo.Text = dt.Rows(0).Item(enmSalesCertificate.DeliveredTo).ToString
                    Me.txtCrtEnginNo.Text = dt.Rows(0).Item(enmSalesCertificate.Engine_No).ToString
                    Me.txtCrtChassisNo.Text = dt.Rows(0).Item(enmSalesCertificate.Chassis_No).ToString
                    'Append One Line Code For ModelCode Field to Get the Dtaa In Text Box Of Model Code 
                    Me.txtModelCode.Text = dt.Rows(0).Item(enmSalesCertificate.ModelCode).ToString
                    Me.txtModelDesc.Text = dt.Rows(0).Item(enmSalesCertificate.Model_Desc).ToString
                    Me.txtLadenWt.Text = dt.Rows(0).Item(enmSalesCertificate.Max_Laden_Weight).ToString
                    Me.txtFrontAxl.Text = dt.Rows(0).Item(enmSalesCertificate.Max_Weight_Front_Axel).ToString
                    Me.txtRearAxel.Text = dt.Rows(0).Item(enmSalesCertificate.Max_Weight_Rear_Axel).ToString
                    Me.txtFrontWheel.Text = dt.Rows(0).Item(enmSalesCertificate.Tyre_Front_Wheel).ToString
                    Me.txtRearWheel.Text = dt.Rows(0).Item(enmSalesCertificate.Tyre_Rear_Wheel).ToString
                    Me.txtBaseWheel.Text = dt.Rows(0).Item(enmSalesCertificate.Base_Wheel).ToString
                    SalesId = Val(dt.Rows(0).Item(enmSalesCertificate.SalesId).ToString)
                    'TASKM166151 SaleDetailId which is sales invoice
                    SalesDetailId = Val(Me.grdSaved.GetRow.Cells("SaleDetailId").Value.ToString) 'Val(dt.Rows(0).Item(enmSalesCertificate.SalesDetailId).ToString)
                    ArticleDefId = Val(dt.Rows(0).Item(enmSalesCertificate.ArticleDefId).ToString)
                    Me.txtComments.Text = dt.Rows(0).Item(enmSalesCertificate.Comments).ToString
                    'Task:2678 Get Values From Double Clicking
                    Me.txtInvoiceAmount.Text = Val(dt.Rows(0).Item(enmSalesCertificate.InvoiceAmount).ToString)
                    Me.txtSalesTax.Text = Val(dt.Rows(0).Item(enmSalesCertificate.SalesTax).ToString)
                    Me.txtAddress.Text = dt.Rows(0).Item(enmSalesCertificate.Address).ToString
                    Me.txtNTN.Text = dt.Rows(0).Item(enmSalesCertificate.NTN).ToString
                    'End Task:2678
                    'Ahmad(Sharif) : added(txtDC And txtRemarks)
                    Me.txtDC.Text = IIf(dt.Rows(0).Item(enmSalesCertificate.DC_No).ToString = "", Me.grdSaved.GetRow.Cells("DcNo").Value.ToString, dt.Rows(0).Item(enmSalesCertificate.DC_No).ToString)
                    Me.txtRemarks.Text = IIf(dt.Rows(0).Item(enmSalesCertificate.Remarks).ToString = "", Me.grdSaved.GetRow.Cells("Remarks").Value.ToString, dt.Rows(0).Item(enmSalesCertificate.Remarks).ToString)
                    ''01-Jul-2014 TASK:2708 Imran Ali Add new fields Registrtion for and sales tax % in Sales Certificate
                    Me.txtRegistrationFor.Text = dt.Rows(0).Item(enmSalesCertificate.RegistrationFor).ToString
                    Me.txtTaxPercent.Text = Val(dt.Rows(0).Item(enmSalesCertificate.Tax_Percent).ToString)
                    'End Task:2708
                    Me.txtReferenceNo.Text = dt.Rows(0).Item(enmSalesCertificate.Reference_No).ToString
                    Me.txtColor.Text = dt.Rows(0).Item(enmSalesCertificate.Color).ToString 'Task:2788 Get Color Name By Double Clicking Grid
                    Me.txtFatherName.Text = dt.Rows(0).Item(enmSalesCertificate.FatherName).ToString
                    Me.cmbCast.Text = dt.Rows(0).Item(enmSalesCertificate.Person_Cast).ToString
                    Me.txtAdvanceAmount.Text = Val(dt.Rows(0).Item(enmSalesCertificate.AdvanceAmount).ToString)
                    Me.txtMeterNo.Text = dt.Rows(0).Item(enmSalesCertificate.MeterNo).ToString
                    Me.txtRegistrationNo.Text = dt.Rows(0).Item(enmSalesCertificate.RegistrationNo).ToString
                    Me.txtInstallment.Text = Val(dt.Rows(0).Item(enmSalesCertificate.Installment).ToString)
                    If Not IsDBNull(dt.Rows(0).Item(enmSalesCertificate.ContractDate)) Then
                        Me.dtpContractDate.Value = dt.Rows(0).Item(enmSalesCertificate.ContractDate)
                        Me.dtpContractDate.Checked = True
                    Else
                        Me.dtpContractDate.Value = DateTime.Now
                        Me.dtpContractDate.Checked = False
                    End If
                    Me.txtDeliveredTo.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
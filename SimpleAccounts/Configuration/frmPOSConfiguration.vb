Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient
Public Class frmPOSConfiguration
    Implements IGeneral
    Dim objModel As POSConfigurationBE
    Dim objDAL As POSConfigurationDAL
    Dim list As List(Of POSDetailConfigurationBE)
    Enum grdPOS
        POSId
        POSTitle
        CompanyId
        Company
        LocationId
        Location
        CostCenterId
        CostCenter
        CashAccountId
        CashAccount
        BankAccountId
        BankAccount
        SalesPersonId
        SalesPerson
        DeliveryOption
        Active
        DiscountPer
    End Enum

    Enum grdCredit
        CreditCardId
        POSID
        MachineTitle
        BankAccountId2
    End Enum
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns(grdPOS.POSId).Visible = False
            Me.grd.RootTable.Columns(grdPOS.CompanyId).Visible = False
            Me.grd.RootTable.Columns(grdPOS.LocationId).Visible = False
            Me.grd.RootTable.Columns(grdPOS.CostCenterId).Visible = False
            Me.grd.RootTable.Columns(grdPOS.CashAccountId).Visible = False
            Me.grd.RootTable.Columns(grdPOS.BankAccountId).Visible = False
            Me.grd.RootTable.Columns(grdPOS.SalesPersonId).Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL As String = String.Empty

            Select Case Condition

                Case "Company"
                    strSQL = "SELECT * FROM CompanyDefTable"
                    FillDropDown(Me.cmbCompany, strSQL, False)
                Case "Location"
                    strSQL = "SELECT * FROM tblDefLocation"
                    FillDropDown(Me.cmbLocation, strSQL, False)
                Case "CostCenter"
                    strSQL = "Select CostCenterId, Name From tblDefCostCenter WHERE Active=1"
                    FillDropDown(Me.cmbCostCenter, strSQL, False)
                Case "CashAccount"
                    strSQL = "SELECT coa_detail_id, detail_title FROM vwCoaDetail where Active=1 and account_type = 'Cash'"
                    FillDropDown(Me.cmbCashAccount, strSQL, False)
                Case "BankAccount"
                    strSQL = "SELECT coa_detail_id, detail_title FROM vwCoaDetail where Active=1 and account_type = 'Bank'"
                    FillDropDown(Me.cmbBankAccount, strSQL, False)
                    FillDropDown(Me.cmbCreditCardAccount, strSQL, False)
                Case "SM"
                    strSQL = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE SalePerson <> 0 And Active = 1"
                    FillDropDown(Me.cmbSalesPerson, strSQL, False)

            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub EditRecords()
        Try
            txtPOSTitle.Text = Me.grd.CurrentRow.Cells(grdPOS.POSTitle).Value.ToString
            cmbCompany.SelectedValue = Val(Me.grd.CurrentRow.Cells(grdPOS.CompanyId).Value.ToString)
            cmbLocation.SelectedValue = Val(Me.grd.CurrentRow.Cells(grdPOS.LocationId).Value.ToString)
            cmbCostCenter.SelectedValue = Val(Me.grd.CurrentRow.Cells(grdPOS.CostCenterId).Value.ToString)
            cmbCashAccount.SelectedValue = Val(Me.grd.CurrentRow.Cells(grdPOS.CashAccountId).Value.ToString)
            cmbBankAccount.SelectedValue = Val(Me.grd.CurrentRow.Cells(grdPOS.BankAccountId).Value.ToString)
            cmbSalesPerson.SelectedValue = Val(Me.grd.CurrentRow.Cells(grdPOS.SalesPersonId).Value.ToString)
            'txtMachineNo.Text = Me.grdCreditDetail.CurrentRow.Cells(grdCredit.MachineTitle).Value.ToString
            'cmbCreditCardAccount.SelectedValue = Val(Me.grdCreditDetail.CurrentRow.Cells(grdCredit.BankAccountId2).Value.ToString)
            chkDeliveryOption.Checked = IIf(grd.CurrentRow.Cells(grdPOS.DeliveryOption).Value = 0, False, True)
            chkActive.Checked = IIf(grd.CurrentRow.Cells(grdPOS.Active).Value = 0, False, True)
            txtDiscountPer.Text = Val(Me.grd.CurrentRow.Cells(grdPOS.DiscountPer).Value.ToString)
            GetAllRecords("CreditCard")
            Me.btnSave.Text = "&Update"
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New POSConfigurationBE
            If Me.btnSave.Text = "&Save" Then
                objModel.POSId = 0
                Else
                objModel.POSId = Val(Me.grd.CurrentRow.Cells(grdPOS.POSId).Value.ToString)
                End If

            objModel.POSTitle = txtPOSTitle.Text
            objModel.CompanyId = Me.cmbCompany.SelectedValue
            objModel.LocationId = Me.cmbLocation.SelectedValue
            objModel.CostCenterId = Me.cmbCostCenter.SelectedValue
            objModel.CashAccountId = Me.cmbCashAccount.SelectedValue
            objModel.BankAccountId = Me.cmbBankAccount.SelectedValue
            objModel.SalesPersonId = Me.cmbSalesPerson.SelectedValue
            objModel.DeliveryOption = Me.chkDeliveryOption.CheckState
            objModel.Active = Me.chkActive.CheckState
            objModel.DiscountPer = txtDiscountPer.Text
            list = New List(Of POSDetailConfigurationBE)
            For i As Integer = 0 To grdCreditDetail.RowCount - 1
                Dim Detail As New POSDetailConfigurationBE
                Detail.CreditCardId = grdCreditDetail.GetRows(i).Cells("CreditCardID").Value
                Detail.POSTitle = grdCreditDetail.GetRows(i).Cells("POSTitle").Value.ToString
                Detail.MachineTitle = grdCreditDetail.GetRows(i).Cells("MachineTitle").Value.ToString
                Detail.BankAccountId2 = grdCreditDetail.GetRows(i).Cells("BankAccountId").Value
                list.Add(Detail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnSave.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.BtnSave.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Condition = "POS" Then
                Dim str As String = "SELECT tblPOSConfiguration.POSId, tblPOSConfiguration.POSTitle, tblPOSConfiguration.CompanyId, CompanyDefTable.CompanyName AS Company, tblPOSConfiguration.LocationId,tblDefLocation.location_name AS Location, tblPOSConfiguration.CostCenterId, tblDefCostCenter.Name AS CostCenter, tblPOSConfiguration.CashAccountId, vwCOADetail.detail_title AS CashAccount, tblPOSConfiguration.BankAccountId, vwCOADetail_1.detail_title AS BankAccount, tblPOSConfiguration.SalesPersonId, tblDefEmployee.Employee_Name, tblPOSConfiguration.DeliveryOption, tblPOSConfiguration.Active, tblPOSConfiguration.DiscountPer as DiscountPercentage FROM tblPOSConfiguration LEFT OUTER JOIN tblDefEmployee ON tblPOSConfiguration.SalesPersonId = tblDefEmployee.Employee_ID LEFT OUTER JOIN vwCOADetail AS vwCOADetail_1 ON tblPOSConfiguration.BankAccountId = vwCOADetail_1.coa_detail_id LEFT OUTER JOIN vwCOADetail ON tblPOSConfiguration.CashAccountId = vwCOADetail.coa_detail_id LEFT OUTER JOIN tblDefCostCenter ON tblPOSConfiguration.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN tblDefLocation ON tblPOSConfiguration.LocationId = tblDefLocation.location_id LEFT OUTER JOIN CompanyDefTable ON tblPOSConfiguration.CompanyId = CompanyDefTable.CompanyId"
                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                Me.grd.RootTable.Columns("POSId").Visible = False
                Me.grd.RootTable.Columns("CompanyId").Visible = False
                Me.grd.RootTable.Columns("LocationId").Visible = False
                Me.grd.RootTable.Columns("CostCenterId").Visible = False
                Me.grd.RootTable.Columns("CashAccountId").Visible = False
                Me.grd.RootTable.Columns("BankAccountId").Visible = False
                Me.grd.RootTable.Columns("SalesPersonId").Visible = False
                ElseIf Condition = "CreditCard" Then
                Dim str As String = "SELECT tblCreditCardAccount.CreditCardId, tblCreditCardAccount.POSTitle, tblCreditCardAccount.MachineTitle, tblCreditCardAccount.BankAccountId, vwCOADetail.detail_title as CardAccount FROM tblCreditCardAccount LEFT OUTER JOIN tblPOSConfiguration ON tblCreditCardAccount.POSTitle = tblPOSConfiguration.POSTitle LEFT OUTER JOIN vwCOADetail ON tblCreditCardAccount.BankAccountId = vwCOADetail.coa_detail_id where tblCreditCardAccount.POSTitle = '" & txtPOSTitle.Text & " '"
                Dim dt As DataTable
                dt = GetDataTable(str)
                Me.grdCreditDetail.DataSource = dt
                Me.grdCreditDetail.RetrieveStructure()
                If Me.grdCreditDetail.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdCreditDetail.RootTable.Columns.Add("Delete")
                    Me.grdCreditDetail.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdCreditDetail.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdCreditDetail.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdCreditDetail.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdCreditDetail.RootTable.Columns("Delete").Caption = "Action"
                End If
                Me.grdCreditDetail.RootTable.Columns("CreditCardId").Visible = False
                Me.grdCreditDetail.RootTable.Columns("BankAccountId").Visible = False
            End If
            
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If txtPOSTitle.Text = "" Then
                msg_Error("Please Write a POS Title")
                Return False
            End If
            If btnSave.Text = "&Save" Then 'Or btnSave.Text = "&Update" Then
                Dim str As String = ""
                str = "Select POSTitle from tblPOSConfiguration where POSTitle = '" & txtPOSTitle.Text & "'"
                Dim dt As DataTable = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    msg_Error("This POS Title Already Exists")
                    Return False
                End If
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            If Me.btnSave.Text = "&Update" Then
                Me.btnSave.Text = "&Save"
            End If
            txtPOSTitle.Text = ""
            txtDiscountPer.Text = 0
            cmbCompany.SelectedIndex = 0
            cmbLocation.SelectedIndex = 0
            cmbCostCenter.SelectedIndex = 0
            cmbCashAccount.SelectedIndex = 0
            cmbBankAccount.SelectedIndex = 0
            cmbCreditCardAccount.SelectedIndex = 0
            cmbSalesPerson.SelectedIndex = 0
            txtMachineNo.Text = ""
            chkDeliveryOption.Checked = False
            chkActive.Checked = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New POSConfigurationDAL
            If IsValidate() = True Then
                If objDAL.Save(objModel) = True Then
                    If objDAL.SaveCreditDetail(list) = True Then
                        SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtPOSTitle.Text, True)
                        Return True
                    Else
                        Return False
                    End If
                    Return True
                Else
                    Return False
                End If
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
        Try
            Try
                If IsValidate() = True Then
                    objDAL = New POSConfigurationDAL
                    FillModel()
                    objDAL.Update(objModel)
                    objDAL.UpdateCreditDetail(list)
                    SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtPOSTitle.Text, True)
                    Return True
                End If
                Return False
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Private Sub frmPOSConfiguration_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Try
            If e.KeyCode = Keys.F4 Then
                If btnSave.Enabled = True Then
                    btnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.F5 Then
                If btnSave.Enabled = True Then
                    btnRefresh_Click(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPOSConfiguration_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            GetAllRecords("POS")
            GetAllRecords("CreditCard")
            FillCombos("POS")
            FillCombos("Company")
            FillCombos("Location")
            FillCombos("CostCenter")
            FillCombos("CashAccount")
            FillCombos("BankAccount")
            FillCombos("SM")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Me.btnSave.Text = "&Save" Then
                If Save() = True Then
                    msg_Information(str_informSave)
                    btnNew_Click(Nothing, Nothing)
                End If
            Else
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                If Update1() = True Then
                    msg_Information(str_informUpdate)
                    btnNew_Click(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
            GetAllRecords("POS")
            GetAllRecords("CreditCard")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grd.RowDoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim dt As DataTable
            dt = CType(Me.grdCreditDetail.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dr("CreditCardID") = 0
            dr("POSTitle") = txtPOSTitle.Text
            dr("MachineTitle") = txtMachineNo.Text
            dr("BankAccountId") = cmbCreditCardAccount.SelectedValue
            dr("CardAccount") = cmbCreditCardAccount.Text
            dt.Rows.Add(dr)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdCreditDetail_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdCreditDetail.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                objDAL = New POSConfigurationDAL
                objDAL.DeleteDetail(Val(Me.grdCreditDetail.GetRow.Cells("CreditCardID").Value.ToString))
                Me.grdCreditDetail.GetRow.Delete()
                grdCreditDetail.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            FillCombos("POS")
            FillCombos("Company")
            FillCombos("Location")
            FillCombos("CostCenter")
            FillCombos("CashAccount")
            FillCombos("BankAccount")
            FillCombos("SM")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
    Private Sub txtDiscountPer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscountPer.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
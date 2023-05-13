''11-June-2014 TASK:2677 Imran Ali Commercial Invoice Configuration On Company Information (Ravi)
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data.OleDb

'Imports SIRIUSUtility
Public Class frmCompanyInformation
    Implements IGeneral
    Dim ModObj As CompanyInfo
    Dim IsLoadedForm As Boolean = False
    Enum enmComp
        CompanyID
        CompanyName
        LegalName
        Phone
        Fax
        Email
        Webpage
        Address
        CostCenterId
        CostCenter
        SalesTaxAccountId
        SalesTaxAccount
        Prefix
        CommerialInvoice
    End Enum
    Dim CompanyID As Integer
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings


    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        If New CompanyInfoDAL().Delete_Record(ModObj) Then Return True
    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "CostCenter" Then
                str = "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") " _
                  & " Select CostCenterID, Name As [Cost Center] from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights  where UserID = " & LoginUserId & ") And IsNull(Active, 0) =1 ORDER BY 2 ASC " _
                  & " Else " _
                  & " Select CostCenterID, Name As [Cost Center] from tblDefCostCenter Where IsNull(Active, 0) = 1 ORDER BY 2 ASC "
                'str = "Select CostCenterId, Name as [Cost Center] From tblDefCostCenter Where IsNull(Active, 0) = 1"
                FillDropDown(Me.cmbCostCenter, str, True)
            ElseIf Condition = "SalesTaxAccount" Then
                str = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
                FillDropDown(Me.cmbSalesTaxAccount, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            ModObj = New CompanyInfo
            ModObj.CompanyID = CompanyID
            ModObj.CompanyName = Me.txtCompanyName.Text.ToString.Replace("'", "''")
            ModObj.LegalName = Me.txtLegalName.Text.ToString.Replace("'", "''")
            ModObj.Phone = Me.txtPhone.Text.ToString.Replace("'", "''")
            ModObj.Fax = Me.txtFax.Text.ToString.Replace("'", "''")
            ModObj.Email = Me.txtEmail.Text.ToString.Replace("'", "''")
            ModObj.WebPage = Me.txtWebPage.Text.ToString.Replace("'", "''")
            ModObj.Address = Me.txtAddress.Text.ToString.Replace("'", "''")
            ModObj.CostCenterId = Me.cmbCostCenter.SelectedValue
            ModObj.Prefix = Me.txtPrefix.Text.ToString.Replace("'", "''")
            ModObj.ActivityLog = New ActivityLog
            ModObj.ActivityLog.ApplicationName = "Config"
            ModObj.ActivityLog.FormCaption = Me.Text
            ModObj.ActivityLog.UserID = 1
            ModObj.ActivityLog.LogDateTime = Date.Now
            ModObj.CommercialInvoice = Me.chkCommercialInvoice.Checked 'Task:2677 Enable Commercial Invoice By User
            ModObj.SalesTaxAccountId = Val(Me.cmbSalesTaxAccount.SelectedValue)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.GridEX1.DataSource = New CompanyInfoDAL().GetAllRecord
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.AutoSizeColumns()
            Me.GridEX1.RootTable.Columns(enmComp.CompanyID).Visible = False
            Me.GridEX1.RootTable.Columns(enmComp.CostCenterId).Visible = False
            Me.GridEX1.RootTable.Columns(enmComp.SalesTaxAccountId).Visible = False
            Dim dt As New DataTable
            dt = New CompanyInfoDAL().GetCompCode
            Me.TextBox1.Text = dt.Rows(0).Item(0).ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtCompanyName.Text.Trim = String.Empty Then
                ShowErrorMessage("Please Enter Company Name")
                Return False
            End If
            Call FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            CompanyID = 0
            Me.txtCompanyName.Text = String.Empty
            Me.txtLegalName.Text = String.Empty
            Me.txtPhone.Text = String.Empty
            Me.txtFax.Text = String.Empty
            Me.txtEmail.Text = String.Empty
            Me.txtWebPage.Text = String.Empty
            Me.txtAddress.Text = String.Empty
            Me.cmbCostCenter.SelectedValue = Convert.ToInt32(0)
            Me.txtPrefix.Text = String.Empty
            Me.BtnSave.Text = "&Save"
            Me.chkCommercialInvoice.Checked = False 'Task:2677 Set Default Status
            Me.cmbSalesTaxAccount.SelectedValue = 0
            GetAllRecords()
            GetSecurityRights()
            Me.chkCommercialInvoice.Focus() 'Task:2677 Set Default Focus Currsor.
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New CompanyInfoDAL().AddRecord(ModObj) Then Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub
    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting
        Try
            Dim dt As DataTable = GetFormRights(EnumForms.frmDefSize)
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
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New CompanyInfoDAL().Update_Record(ModObj) Then Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmCompanyInformation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 10 -1-14
            If e.KeyCode = Keys.F4 Then
                BtnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmCompanyInformation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            ReSetControls()
            SetConfigurationBaseSetting()
            FillCombos("CostCenter")
            FillCombos("SalesTaxAccount")
            GetAllRecords()
            IsLoadedForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click

        Me.Cursor = Cursors.WaitCursor
        Try
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Not IsValidate() Then Exit Sub
            'If Not msg_Confirm(str_ConfirmSave) Then Exit Sub
            If BtnSave.Text = "&Save" Or BtnSave.Text = "Save" Then
                'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Save() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
            Else
                'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Not msg_Confirm(str_ConfirmUpdate) Then Exit Sub
                If Update1() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
            End If
            'MsgBox("Record Has Been Saved", MsgBoxStyle.Information, str_MessageHeader)
            'msg_Information(str_informSave)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            'Task#20082015 calling verification function by Ahmad Sharif
            If VerifyRecordAgainstCompanyExist(Me.GridEX1.GetRow.Cells("CompanyId").Value) = False Then
                ShowErrorMessage("You can't delete this company, Dependent Record exists against this company")
                Exit Sub
            End If
            'End Task#20082015

            If Not IsValidate() Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            'ApplyGridSettings()
            If Delete() Then Me.DialogResult = Windows.Forms.DialogResult.Yes
            'MsgBox("Record Hase Been Deleted", MsgBoxStyle.Information, str_MessageHeader)
            ' msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    'Task#20082015 Verify if record exist against company then stop to delete company by Ahmad Sharif
    Private Function VerifyRecordAgainstCompanyExist(ByVal companyId As Integer) As Boolean
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            Dim strSQL As String = String.Empty
            strSQL = "select LocationId from salesmastertable where LocationId=" & companyId
            cmd.CommandText = strSQL

            Dim comp As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            trans.Commit()

            If comp > 0 Then
                Return False
            End If

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function
    'End Task#20082015

    Private Sub GridEX1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Label10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label10.Click

    End Sub
    Private Sub EditRecord()
        Try
            Me.CompanyID = Me.GridEX1.GetRow.Cells(enmComp.CompanyID).Value
            Me.txtCompanyName.Text = Me.GridEX1.GetRow.Cells(enmComp.CompanyName).Value.ToString
            Me.txtLegalName.Text = Me.GridEX1.GetRow.Cells(enmComp.LegalName).Value.ToString
            Me.txtPhone.Text = Me.GridEX1.GetRow.Cells(enmComp.Phone).Value.ToString
            Me.txtFax.Text = Me.GridEX1.GetRow.Cells(enmComp.Email).Value.ToString
            Me.txtEmail.Text = Me.GridEX1.GetRow.Cells(enmComp.Email).Value.ToString
            Me.txtWebPage.Text = Me.GridEX1.GetRow.Cells(enmComp.Webpage).Value.ToString
            Me.txtAddress.Text = Me.GridEX1.GetRow.Cells(enmComp.Address).Value.ToString
            Me.cmbCostCenter.SelectedValue = Me.GridEX1.GetRow.Cells(enmComp.CostCenterId).Value
            Me.TextBox1.Text = CompanyID
            Me.txtPrefix.Text = GridEX1.GetRow.Cells(enmComp.Prefix).Value.ToString
            Me.chkCommercialInvoice.Checked = GridEX1.GetRow.Cells(enmComp.CommerialInvoice).Value 'Task:2677 Get Value.
            Me.cmbSalesTaxAccount.SelectedValue = Val(Me.GridEX1.GetRow.Cells(enmComp.SalesTaxAccountId).Value.ToString)
            Me.BtnSave.Text = "&Update"
            GetSecurityRights()
            Me.txtCompanyName.Focus()
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
            If getConfigValueByType("NewSecurityRights").ToString = "False" Then
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefArea)
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
                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From CompanyDefTable WHERE CompanyId='" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.TextBox1.Text = dt.Rows(0).Item("CompanyId").ToString
                        Me.txtCompanyName.Text = dt.Rows(0).Item("CompanyName").ToString
                        Me.txtLegalName.Text = dt.Rows(0).Item("LegalName").ToString
                        Me.txtPhone.Text = dt.Rows(0).Item("Phone").ToString
                        Me.txtFax.Text = dt.Rows(0).Item("Fax").ToString
                        Me.txtEmail.Text = dt.Rows(0).Item("EMail").ToString
                        Me.txtWebPage.Text = dt.Rows(0).Item("webpage").ToString
                        Me.txtAddress.Text = dt.Rows(0).Item("address").ToString
                        Me.cmbCostCenter.SelectedValue = dt.Rows(0).Item("CostCenterId").ToString
                        'TAsk:2677 Get Commercial Invoice Status By User
                        If Not IsDBNull(dt.Rows(0).Item("CommercialInvoice")) Then
                            Me.chkCommercialInvoice.Checked = dt.Rows(0).Item("CommercialInvoice")
                        Else
                            Me.chkCommercialInvoice.Checked = False
                        End If
                        'End Task:2677
                        CompanyID = Me.TextBox1.Text
                        Me.BtnSave.Text = "&Update"
                    End If
                End If
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Me.cmbCostCenter.SelectedIndex = -1 Then Exit Sub
            Dim id As Integer = 0I
            id = Me.cmbCostCenter.SelectedValue
            FillCombos("CostCenter")
            Me.cmbCostCenter.SelectedValue = id
            id = Me.cmbSalesTaxAccount.SelectedValue
            FillCombos("SalesTaxAccount")
            Me.cmbSalesTaxAccount.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub GridEX1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 10-1-14
        If e.KeyCode = Keys.F2 Then
            BtnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            BtnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub cmbCostCenter_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCostCenter.SelectedIndexChanged

    End Sub

    Private Sub txtAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAddress.TextChanged

    End Sub

    Private Sub txtWebPage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWebPage.TextChanged

    End Sub

    Private Sub txtEmail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub txtFax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFax.TextChanged

    End Sub

    Private Sub txtPhone_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPhone.TextChanged

    End Sub

    Private Sub txtLegalName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLegalName.TextChanged

    End Sub

    Private Sub txtCompanyName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompanyName.TextChanged

    End Sub
End Class
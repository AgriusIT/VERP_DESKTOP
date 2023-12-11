'' 21-12-2013 ReqID-957   M Ijaz Javed      Bank information entry option
'' 28-Dec-2013 R:M6           Release 2.1.0.0 Bug
'' 4-Jan-2014 Tsk:2368      Imran Ali         Multi Cheque Layout
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal

Public Class frmBranchNew
    Implements IGeneral
    Dim bank As BankInfo_BE
    Dim bid As Integer = 0I

    Private Sub frmBranchNew_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
        If e.KeyCode = Keys.F4 Then
            btnSave_Click(Nothing, Nothing)

        End If
        If e.KeyCode = Keys.Escape Then
            btnNew_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmBranchNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            FillCombos() 'Task:2368 Call Cheque Layout Method
            ReSetControls()     ' Reset all controls
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading record " & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        
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
                Dim dt As DataTable = GetFormRights(EnumForms.frmDetailAccount)
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
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        'Me.btnDelete.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Print" Then
                        'Me.btnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            ''Tsk:2368 Fill Layout DropDown
            FillDropDown(cmbBankType, "select  distinct Bank_Type,Bank_Type from tblBankInfo  ", False)
            FillDropDown(Me.cmbChequeLayout, "Select ChequeLayoutId, LayoutName From tblChequeLayouts")
                'End Task:2368
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            bank = New BankInfo_BE
            bank.AccountHolder_Id = bid
            bank.Branch_Area = Me.txtBrabchCode.Text.Replace("'", "''")
            bank.Account_No = Me.txtAccountNumber.Text.Replace("'", "''")
            bank.Holder_Name = Me.txtHolderName.Text.Replace("'", "''")
            bank.Bank_Id = Me.grdBranch.CurrentRow.Cells("coa_detail_id").Value
            bank.BranchPhoneNo = Me.txtBranchPhoneNo.Text.Replace("'", "''") 'R:M6 Set Value Branch Phone No 
            bank.ChequeLayoutId = IIf(Me.cmbChequeLayout.SelectedIndex > 0, Me.cmbChequeLayout.SelectedValue, 0) 'Task:2368 Set Value ChequeLayoutId
            bank.BankType = Me.cmbBankType.Text.ToString.Replace("'", "''")
            bank.DesignatedTo = Me.txtDesignatedTo.Text.Replace("'", "''")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdBranch.DataSource = New BankInfo_DAL().GetAllRecords()
            Me.grdBranch.RetrieveStructure()
            Me.grdBranch.RootTable.Columns("coa_detail_Id").Visible = False
            Me.grdBranch.RootTable.Columns("AccountHolder_Id").Visible = False
            Me.grdBranch.RootTable.Columns("Bank_Id").Visible = False
            Me.grdBranch.RootTable.Columns("ChequeLayoutId").Visible = False 'Task:2368 Hide ChequeLayoutId Column
            Me.grdBranch.RootTable.Columns("detail_title").Caption = "Bank"
            Me.grdBranch.RootTable.Columns("Holder_Name").Caption = "Account Holder Name"
            Me.grdBranch.RootTable.Columns("Account_No").Caption = "Account No"
            Me.grdBranch.RootTable.Columns("Branch_Area").Caption = "Branch Code"
            Me.grdBranch.RootTable.Columns("Branch_PhoneNo").Caption = "Branch Phone" 'R:M6 Column Rename
            Me.grdBranch.RootTable.Columns("Bank_Type").Caption = "Bank Type"
            Me.grdBranch.RootTable.Columns("Designated_To").Caption = "Designated To"
            Me.grdBranch.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.txtBrabchCode.Text = String.Empty Then
                ShowErrorMessage("Please write branch area/code")
                Me.txtBrabchCode.Focus()
                Return False
            End If

            If Me.txtAccountNumber.Text = String.Empty Then
                ShowErrorMessage("")
                Me.txtAccountNumber.Focus()
                Return False
            End If

            If Me.txtHolderName.Text = String.Empty Then
                ShowErrorMessage("")
                Me.txtHolderName.Focus()
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
            GetAllRecords()  'Get All Existing Bank Records
            Me.txtBankName.Text = String.Empty
            Me.txtAccountNumber.Text = String.Empty
            Me.txtBrabchCode.Text = String.Empty
            Me.txtHolderName.Text = String.Empty
            Me.txtBranchPhoneNo.Text = String.Empty
            Me.txtDesignatedTo.Text = String.Empty
            Me.cmbBankType.Text = String.Empty
            Me.btnSave.Text = "&Save"

            Me.btnSave.Visible = False
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
            If Not Me.cmbChequeLayout.SelectedIndex = -1 Then Me.cmbChequeLayout.SelectedIndex = 0 'Task:2368 Reset Cheque Layout DropDown
            Me.txtAccountNumber.Focus()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If New BankInfo_DAL().Save(bank) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
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
            If New BankInfo_DAL().Update(bank) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Update" Then

                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        ReSetControls()
                    End If
                Else
                    'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        ReSetControls()
                    End If

                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdBranch_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdBranch.DoubleClick
        Try
            If Me.grdBranch.RowCount = 0 Then Exit Sub
            If IsDBNull(Me.grdBranch.CurrentRow.Cells("AccountHolder_Id").Value) Then
                bid = 0
                Me.btnSave.Text = "&Save"
            Else
                bid = Me.grdBranch.CurrentRow.Cells("AccountHolder_Id").Value
                Me.btnSave.Text = "&Update"
            End If
            Me.txtBankName.Text = Me.grdBranch.CurrentRow.Cells("detail_title").Value.ToString
            Me.txtAccountNumber.Text = Me.grdBranch.CurrentRow.Cells("Account_No").Value.ToString
            Me.txtBrabchCode.Text = Me.grdBranch.CurrentRow.Cells("Branch_Area").Value.ToString
            Me.txtHolderName.Text = Me.grdBranch.CurrentRow.Cells("Holder_Name").Value.ToString
            Me.txtBranchPhoneNo.Text = Me.grdBranch.CurrentRow.Cells("Branch_PhoneNo").Value.ToString
            Me.cmbChequeLayout.SelectedValue = Val(Me.grdBranch.GetRow.Cells("ChequeLayoutId").Value.ToString) 'Task:2368 Retrieve Value ChequeLayoutId
            Me.cmbBankType.Text = Me.grdBranch.CurrentRow.Cells("Bank_Type").Value.ToString
            Me.txtDesignatedTo.Text = Me.grdBranch.CurrentRow.Cells("Designated_To").Value.ToString
            Me.btnSave.Visible = True
            Me.txtAccountNumber.Focus()
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            'Task:2368 Reload Cheque Layout Dropdown
            'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim id As Integer = 0I
            id = Me.cmbChequeLayout.SelectedIndex
            FillCombos()
            'End Task:2368
            Me.cmbChequeLayout.SelectedIndex = id
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdBranch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdBranch.KeyDown

    End Sub
End Class
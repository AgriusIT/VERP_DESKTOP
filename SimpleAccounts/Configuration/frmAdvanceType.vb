Imports SBModel
Imports SBDal

Public Class frmAdvanceType

    Implements IGeneral
    Dim Id As Integer = 0I
    Dim AdvanceType As AdvanceTypeBE
    Public objDAL As AdvanceTypeDAL = New AdvanceTypeDAL()

    Enum enmAdvanceType
        Id
        Title
        Comments
        SalaryDeduct
        SortOrder
        Active
        AccountId
    End Enum

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns("AccountId").Visible = False
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1

                grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit

            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                '    Exit Sub
                'End If
            Else
                If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                    Dim dt As DataTable = GetFormRights(EnumForms.frmAdvanceType)
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
                    'GetSecurityByPostingUser(UserPostingRights, BtnSave, BtnDelete)
                Else
                    'Me.Visible = False
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.BtnPrint.Enabled = False
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
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub


    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If Me.grd.RootTable Is Nothing Then Return False
            AdvanceType = New AdvanceTypeBE
            AdvanceType.id = Me.grd.GetRow.Cells(enmAdvanceType.Id).Value
            If New AdvanceTypeDAL().Delete(AdvanceType) Then
                'Insert Activity Log by Ali Faisal 
                SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, Me.txtAdvanceType.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Dim str As String = String.Empty
        If Condition = "Account" Then

            str = "SELECT dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title, dbo.vwCOADetail.detail_code, dbo.vwCOADetail.account_type " & _
                   "FROM dbo.vwCOADetail " & _
                   "WHERE(dbo.vwCOADetail.coa_detail_id > 0) "

            str += " AND vwCOADetail.Active=1 "
            str += " AND vwCOADetail.Active in(0,1,NULL) "
            str += " ORDER BY dbo.vwCOADetail.detail_title"
        End If

        FillUltraDropDown(cmbAccount, str)
        Me.cmbAccount.Rows(0).Activate()

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            AdvanceType = New AdvanceTypeBE
            AdvanceType.id = id
            AdvanceType.Title = Me.txtAdvanceType.Text
            AdvanceType.Comments = Me.txtAdvanceDetails.Text
            AdvanceType.Active = IIf(Me.chkActive.Checked = True, 1, 0)
            AdvanceType.SalaryDeduct = IIf(Me.chkDeductFromSalary.Checked = True, 1, 0)
            AdvanceType.SortOrder = Me.txtSortOrder.Text
            AdvanceType.AccountId = Me.cmbAccount.ActiveRow.Cells(0).Value

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Me.grd.DataSource = New AdvanceTypeDAL().GetAll
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
            Me.grd.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try


            If Me.txtAdvanceType.Text = String.Empty Then
                ShowErrorMessage("Please enter Advance Type")
                Me.txtAdvanceType.Focus()
                Return False
            End If
            ''Ali Faisal : Added to check if type is exists already
            'Dim str As String = "SELECT * FROM tblDefAdvancesType WHERE Title = '" & Me.txtAdvanceType.Text & "'"
            'Dim dt As DataTable = GetDataTable(str)
            'If dt IsNot Nothing Then
            '    If dt.Rows.Count > 0 Then
            '        ShowErrorMessage("Advance type exists already")
            '        Me.txtAdvanceType.Focus()
            '        Return False
            '    End If
            'End If
            If cmbAccount.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select an Account")
                cmbAccount.Focus() : IsValidate = False : Exit Function
            End If

            FillModel() 'Call Fillmodel 
            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.BtnSave.Text = "&Save"
            txtAdvanceType.Text = ""
            txtAdvanceDetails.Text = ""
            chkDeductFromSalary.Checked = True
            Me.chkDeductFromSalary.Enabled = True
            txtSortOrder.Text = "1"
            Me.cmbAccount.Rows(0).Activate()
            GetAllRecords()
            ApplyGridSettings()
            CtrlGrdBar1_Load(Nothing, Nothing)
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New AdvanceTypeDAL().Add(AdvanceType) Then
                'Insert Activity Log by Ali Faisal 
                SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.txtAdvanceType.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw (ex)
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
            If New AdvanceTypeDAL().Update(AdvanceType) Then
                'Insert Activity Log by Ali Faisal 
                SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, Me.txtAdvanceType.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Function

    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        ReSetControls()

    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsValidate() = True Then
                If BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save" Then
                    If ValidateAdvanceType() = True Then
                        If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                        If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                        msg_Information(str_informSave)
                        ReSetControls()
                    End If
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

    Public Function ValidateAdvanceType() As Boolean
        Try

            'Ali Faisal : Added to check if type is exists already
            Dim str As String = "SELECT * FROM tblDefAdvancesType WHERE Title = '" & Me.txtAdvanceType.Text & "'"
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    ShowErrorMessage("Advance type exists already")
                    Me.txtAdvanceType.Focus()
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception

        End Try
    End Function
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Try
            If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
            Delete()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click

    End Sub

    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmAdvanceType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombos("Account")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecords()
        Try

            If Me.grd.RowCount = 0 Then Exit Sub
            Id = Me.grd.GetRow.Cells(enmAdvanceType.Id).Value
            Me.txtAdvanceType.Text = Me.grd.GetRow.Cells(enmAdvanceType.Title).Value.ToString
            Me.txtAdvanceDetails.Text = Me.grd.GetRow.Cells(enmAdvanceType.Comments).Value.ToString
            Me.txtSortOrder.Text = Me.grd.GetRow.Cells(enmAdvanceType.SortOrder).Value.ToString
            Me.cmbAccount.Value = Me.grd.GetRow.Cells(enmAdvanceType.AccountId).Value
            If IsDBNull(Me.grd.GetRow().Cells(enmAdvanceType.Active).Value) Then
                Me.chkActive.Checked = False
            Else
                Me.chkActive.Checked = Me.grd.GetRow().Cells(enmAdvanceType.Active).Value
            End If
            If IsDBNull(Me.grd.GetRow().Cells(enmAdvanceType.SalaryDeduct).Value) Then
                Me.chkDeductFromSalary.Checked = False
            Else
                Me.chkDeductFromSalary.Checked = Me.grd.GetRow().Cells(enmAdvanceType.SalaryDeduct).Value
            End If
            Me.btnSave.Text = "&Update"
            Me.txtAdvanceType.Focus()
            Me.chkDeductFromSalary.Enabled = False
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try

            If Me.grd.Row < 0 Then
                Exit Sub
            Else
                EditRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmAdvanceType_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try

            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Advance Type"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
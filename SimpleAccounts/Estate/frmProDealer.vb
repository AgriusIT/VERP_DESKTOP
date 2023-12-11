Imports SBModel
Imports SBDal
Imports System.Data
Imports System.Data.SqlClient
Public Class frmProDealer
    Implements IGeneral
    Public Shared DealerId As Integer = 0
    Dim DetailAcccountId As Integer = 0
    Dim Dealer As DealerBE
    Public DealerDAL As DealerDAL = New DealerDAL()
    Dim DetailAcccount As COADeail
    Public DetailAcccountDAL As COADetailDAL = New COADetailDAL()
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Enum enmDealer
        DealerId
        Name
        PrimaryMobile
        SecondaryMobile
        Email
        EstateId
        coa_detail_id
        SpecialityId
        Remarks
        Active
        DesignationId
    End Enum

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub New(ByVal DealerId As Integer)


        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        FillCombos("Designation")
        FillCombos("Estate")
        FillCombos("Speciality")
        FillCombos("COA")
        GetById(DealerId)
    End Sub
    Private Sub GetById(ByVal DealerId As Integer)
        Try
            Dim dt As DataTable = New DealerDAL().GetById(DealerId)

            If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("DealerId") > 0 Then
                ShowData(dt)
            Else
                ReSetControls()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ShowData(ByVal dt As DataTable)
        Try

            DealerId = dt.Rows(0).Item("DealerId")
            Me.txtName.Text = dt.Rows(0).Item("Name").ToString
            Me.txtEmailID.Text = dt.Rows(0).Item("Email").ToString
            Me.txtPrimaryMobile.Text = dt.Rows(0).Item("PrimaryMobile").ToString
            Me.txtSecondaryMobile.Text = dt.Rows(0).Item("SecondaryMobile").ToString
            Me.cmbSpeciality.Value = Val(dt.Rows(0).Item("SpecialityId").ToString)
            'Me.cmbCOA.Value = Val(dt.Rows(0).Item("coa_detail_id").ToString)
            Me.txtCOA.Text = GetAccountById(Val(dt.Rows(0).Item("coa_detail_id").ToString))
            Me.cmbEstate.Value = Val(dt.Rows(0).Item("EstateId").ToString)
            Me.cmbDesignation.Value = Val(dt.Rows(0).Item("DesignationId").ToString)
            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
            Me.chkActive.Checked = dt.Rows(0).Item("Active")
            btnSave.Enabled = DoHaveUpdateRights
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty

            If Condition = "Designation" Then

                str = "Select EmployeeDesignationId ,EmployeeDesignationName As Designation,SortOrder  from EmployeeDesignationDefTable Where Active = 1 order by SortOrder "
                FillUltraDropDown(cmbDesignation, str)
                Me.cmbDesignation.Rows(0).Activate()
                Me.cmbDesignation.DisplayLayout.Bands(0).Columns("EmployeeDesignationId").Hidden = True
                Me.cmbDesignation.DisplayLayout.Bands(0).Columns("Designation").Width = 500
            ElseIf Condition = "Estate" Then
                str = "Select EstateId , Name  from Estate where Active = 1  "
                FillUltraDropDown(cmbEstate, str)
                Me.cmbEstate.Rows(0).Activate()
                Me.cmbEstate.DisplayLayout.Bands(0).Columns("EstateId").Hidden = True
                Me.cmbEstate.DisplayLayout.Bands(0).Columns("Name").Width = 500

            ElseIf Condition = "Speciality" Then
                str = "Select SpecialityId , Speciality  from Speciality where Active = 1 order by SortOrder  "
                FillUltraDropDown(cmbSpeciality, str)
                Me.cmbSpeciality.Rows(0).Activate()
                Me.cmbSpeciality.DisplayLayout.Bands(0).Columns("SpecialityId").Hidden = True
                Me.cmbSpeciality.DisplayLayout.Bands(0).Columns("Speciality").Width = 500


            ElseIf Condition = "COA" Then
                str = "Select coa_detail_id , detail_code As Code , detail_title As Title from tblCOAMainSubSubDetail "
                FillUltraDropDown(cmbCOA, str)
                Me.cmbCOA.Rows(0).Activate()
                Me.cmbCOA.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Me.cmbCOA.DisplayLayout.Bands(0).Columns("Code").Width = 250
                Me.cmbCOA.DisplayLayout.Bands(0).Columns("Title").Width = 250
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Dealer = New DealerBE
            Dealer.DealerId = DealerId
            Dealer.Name = Me.txtName.Text
            Dealer.PrimaryMobile = Me.txtPrimaryMobile.Text
            Dealer.SecondaryMobile = Me.txtSecondaryMobile.Text
            Dealer.SpecialityId = Val(Me.cmbSpeciality.ActiveRow.Cells(0).Value.ToString)
            'Dealer.coa_detail_id = Val(Me.cmbCOA.ActiveRow.Cells(0).Value.ToString)
            Dealer.DesignationId = Val(Me.cmbDesignation.ActiveRow.Cells(0).Value.ToString)
            Dealer.EstateId = Val(Me.cmbEstate.ActiveRow.Cells(0).Value.ToString)
            Dealer.Email = Me.txtEmailID.Text
            Dealer.Active = IIf(Me.chkActive.Checked = True, 1, 0)
            Dealer.Remarks = txtRemarks.Text
            Dealer.ActivityLog = New ActivityLog
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub FillModelForAccount(Optional Condition As String = "")
        Try

            DetailAcccount = New COADeail
            DetailAcccount.COADetailID = DetailAcccountId
            DetailAcccount.DetailTitle = Me.txtName.Text
            DetailAcccount.Active = IIf(Me.chkActive.Checked = True, 1, 0)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Function GetAccountById(ByVal ID As Integer) As String
        Dim strSQL As String = String.Empty
        Dim DetailAccountTitle As String = String.Empty
        Try

            strSQL = " Select coa_detail_id , detail_code from tblCOAMainSubsubDetail where coa_detail_id = " & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)

            If dt.Rows.Count > 0 Then
                DetailAccountTitle = dt.Rows(0).Item("detail_code").ToString()
                DetailAcccountId = Val(dt.Rows(0).Item("coa_detail_id").ToString())
            End If
            Return DetailAccountTitle
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try


            If Me.txtName.Text = String.Empty Then
                ShowErrorMessage("Please enter Name")
                Me.txtName.Focus()
                Return False
            End If

            FillModel() 'Call Fillmodel 
            FillModelForAccount() 'CallFillModelForAccount
            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            DealerId = 0
            txtName.Text = ""
            txtEmailID.Text = ""
            txtPrimaryMobile.Text = ""
            txtSecondaryMobile.Text = ""
            txtRemarks.Text = ""
            txtCOA.Text = ""
            chkActive.Checked = True
            btnSave.Enabled = DoHaveSaveRights
            'Me.cmbCOA.Rows(0).Activate()
            Me.cmbSpeciality.Rows(0).Activate()
            Me.cmbEstate.Rows(0).Activate()
            Me.cmbDesignation.Rows(0).Activate()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            Dim AccountSubSubId As Integer = Val(getConfigValueByType("DealerSubSub").ToString)

            If COADetailDAL.GetAccountName(DetailAcccount.DetailTitle, AccountSubSubId) = True Then

                If msg_Confirm("Account name already exist do you want create account with same name") = True Then

                    If AccountSubSubId <= 0 Then
                        ShowErrorMessage("Please Select a sub sub Account to map Against the Dealer")
                        Exit Function
                    End If
                    DetailAcccountId = New COADetailDAL().Add(DetailAcccount, AccountSubSubId)
                    Dealer.coa_detail_id = DetailAcccountId
                    If New DealerDAL().Add(Dealer) Then
                        SaveActivityLog("Configuration", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.txtName.Text, True)
                        Return True
                    Else
                        Return False
                    End If

                End If

            Else

                If AccountSubSubId <= 0 Then
                    ShowErrorMessage("Please Select a sub sub Account to map Against the Dealer")
                    Exit Function
                End If
                DetailAcccountId = New COADetailDAL().Add(DetailAcccount, AccountSubSubId)
                Dealer.coa_detail_id = DetailAcccountId
                If New DealerDAL().Add(Dealer) Then
                    SaveActivityLog("Configuration", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.txtName.Text, True)
                    Return True
                Else
                    Return False
                End If

            End If

            'If New COADetailDAL().Add(DetailAcccount, AccountSubSubId) Then

            '    SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.txtName.Text, True)
            '    Return True
            'Else
            '    Return False
            'End If
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

    Private Sub frmDealer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub frmDealer_Load(sender As Object, e As EventArgs) Handles Me.Load


        '' This call is required by the designer.
        'InitializeComponent()
        '' Add any initialization after the InitializeComponent() call.

        Dim AccountSubSubId As Integer = Val(getConfigValueByType("DealerSubSub").ToString)

        If AccountSubSubId <= 0 Then

            ShowErrorMessage("Please Select a sub sub Account to map Against the Dealer")

        End If

        FillCombos("Designation")
        FillCombos("Estate")
        FillCombos("Speciality")
        FillCombos("COA")
        GetById(DealerId)

        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
    End Sub

    'Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
    '    Try
    '        If New DealerDAL().Update(Dealer) Then

    '            SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, Me.txtAccountGroupTitle.Text, True)
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        msg_Error(ex.Message)
    '    End Try

    'End Function

    'Public Sub EditRecords()
    '    Try

    '        If Me.grd.RowCount = 0 Then Exit Sub
    '        Id = Me.grd.GetRow.Cells(enmCOAGroups.COAGroupId).Value
    '        Me.txtAccountGroupTitle.Text = Me.grd.GetRow.Cells(enmCOAGroups.Title).Value.ToString
    '        Me.txtAccountGroupDetails.Text = Me.grd.GetRow.Cells(enmCOAGroups.Details).Value.ToString
    '        Me.txtSortOrder.Text = Me.grd.GetRow.Cells(enmCOAGroups.SortOrder).Value.ToString

    '        If IsDBNull(Me.grd.GetRow().Cells(enmCOAGroups.Active).Value) Then
    '            Me.chkActive.Checked = False
    '        Else
    '            Me.chkActive.Checked = Me.grd.GetRow().Cells(enmCOAGroups.Active).Value
    '        End If

    '        Me.btnSave.Text = "&Update"
    '        Me.txtAccountGroupTitle.Focus()

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub








    Private Sub frmDealer_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Try
        '    FillCombos("Designation")
        '    FillCombos("Estate")
        '    FillCombos("Speciality")
        '    FillCombos("COA")
        '    ReSetControls()
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub



    Private Sub btnSave_Click_1(sender As Object, e As EventArgs) Handles btnSave.Click

        Try
            If IsValidate() = True Then

                If DealerId = 0 Then

                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informSave)
                    ReSetControls()
                    Me.Close()
                Else

                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        msg_Information(str_informUpdate)
                        ReSetControls()
                        Me.Close()
                    End If
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
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

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try

            Dim AccountSubSubId As Integer = Val(getConfigValueByType("DealerSubSub").ToString)

            If COADetailDAL.GetAccountName(DetailAcccount.DetailTitle, AccountSubSubId) = True Then

                If msg_Confirm("Account name already exist do you want create account with same name") = True Then

                    If conn.State = ConnectionState.Closed Then
                        conn.Open()
                    End If
                    trans = conn.BeginTransaction

                    If New COADetailDAL().Update(DetailAcccount, trans) Then
                        ShowInformationMessage("Account Title has Updated Successfully")
                    End If
                    trans.Commit()
                    Dealer.coa_detail_id = DetailAcccountId
                    If New DealerDAL().Update(Dealer) Then
                        SaveActivityLog("Configuration", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, Me.txtName.Text, True)
                        Return True
                    Else
                        Return False
                    End If

                Else
                    Return False

                End If

            Else

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                trans = conn.BeginTransaction

                If New COADetailDAL().Update(DetailAcccount, trans) Then
                    ShowInformationMessage("Account Title has Updated Successfully")
                End If
                trans.Commit()
                Dealer.coa_detail_id = DetailAcccountId
                If New DealerDAL().Update(Dealer) Then
                    SaveActivityLog("Configuration", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, Me.txtName.Text, True)
                    Return True
                Else
                    Return False
                End If

            End If
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Private Sub txtPrimaryMobile_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrimaryMobile.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub txtSecondaryMobile_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSecondaryMobile.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
End Class
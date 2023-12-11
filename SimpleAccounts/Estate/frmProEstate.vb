Imports SBModel
Imports SBDal
Public Class frmProEstate
    Implements IGeneral
    Public Shared EstateId As Integer = 0
    Dim Estate As EstateBE
    Public EstateDAL As EstateDAL = New EstateDAL()
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Dim IsFormOpend As Boolean = False
    Enum enmDealer
        EstateId
        Name
        coa_detail_id
        Remarks
        Active

    End Enum

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Sub New(ByVal EstateId As Integer)


        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        FillCombos("COA")
        GetById(EstateId)
    End Sub
    Private Sub GetById(ByVal EstateId As Integer)
        Try
            Dim dt As DataTable = New EstateDAL().GetById(EstateId)

            If dt.Rows.Count > 0 AndAlso dt.Rows(0).Item("EstateId") > 0 Then
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

            EstateId = dt.Rows(0).Item("EstateId")
            Me.txtName.Text = dt.Rows(0).Item("Name").ToString
            Me.cmbCOA.Value = Val(dt.Rows(0).Item("coa_detail_id").ToString)
            Me.txtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
            Me.cbActive.Checked = dt.Rows(0).Item("Active")
            btnSave.Enabled = DoHaveUpdateRights
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty

            If Condition = "COA" Then
                str = "Select coa_detail_id , detail_code As Code , detail_title As Title from tblCOAMainSubSubDetail "
                FillUltraDropDown(cmbCOA, str)
                If rdoName.Checked = True Then
                    Me.cmbCOA.DisplayMember = Me.cmbCOA.Rows(0).Cells(2).Column.Key.ToString
                Else
                    Me.cmbCOA.DisplayMember = Me.cmbCOA.Rows(0).Cells(1).Column.Key.ToString
                End If
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
            Estate = New EstateBE
            Estate.EstateId = EstateId
            Estate.Name = Me.txtName.Text
            Estate.coa_detail_id = Val(Me.cmbCOA.ActiveRow.Cells(0).Value.ToString)
            Estate.Active = IIf(Me.cbActive.Checked = True, 1, 0)
            Estate.Remarks = Me.txtRemarks.Text
            Estate.ActivityLog = New ActivityLog

        Catch ex As Exception
            Throw ex
        End Try

    End Sub



    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try


            If Me.txtName.Text = String.Empty Then
                ShowErrorMessage("Please enter Name")
                Me.txtName.Focus()
                Return False
            End If

            FillModel() 'Call Fillmodel 
            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            txtName.Focus()
            EstateId = 0
            txtName.Text = ""
            txtRemarks.Text = ""
            cbActive.Checked = True
            btnSave.Enabled = DoHaveSaveRights
            'Me.cmbCOA.Rows(0).Activate()
            If Me.cmbCOA.Rows.Count > 0 Then cmbCOA.Rows(0).Activate()
            If getConfigValueByType("ItemFilterByName") = "True" Then
                Me.rdoName.Checked = True
            Else
                Me.RdoCode.Checked = True
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New EstateDAL().Add(Estate) Then

                SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.txtName.Text, True)
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

    Private Sub frmProEstateList_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub frmProEstate_Load(sender As Object, e As EventArgs) Handles Me.Load

        FillCombos("COA")
        GetById(EstateId)
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor
        Me.IsFormOpend = True
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








    Private Sub frmProEstateList_Shown(sender As Object, e As EventArgs) Handles Me.Shown
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

                If EstateId = 0 Then

                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informSave)
                    ReSetControls()
                    Me.Close()
                Else

                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    msg_Information(str_informUpdate)
                    ReSetControls()
                    Me.Close()
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
        Try
            If New EstateDAL().Update(Estate) Then

                SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.txtName.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function

    Private Sub rdoName_CheckedChanged(sender As Object, e As EventArgs) Handles rdoName.CheckedChanged
        If Not Me.IsFormOpend = True Then Exit Sub
        'Me.FillCombo("Item")
        Me.cmbCOA.DisplayMember = Me.cmbCOA.Rows(0).Cells(2).Column.Key.ToString

    End Sub

    Private Sub RdoCode_CheckedChanged(sender As Object, e As EventArgs) Handles RdoCode.CheckedChanged
        If Not Me.IsFormOpend = True Then Exit Sub 'Me.FillCombo("Item")
        Me.cmbCOA.DisplayMember = Me.cmbCOA.Rows(0).Cells(1).Column.Key.ToString

    End Sub

    Private Sub cbActive_CheckedChanged(sender As Object, e As EventArgs) Handles cbActive.CheckedChanged

    End Sub
End Class
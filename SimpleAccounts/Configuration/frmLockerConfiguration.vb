Public Class frmLockerConfiguration
    Implements IGeneral

    Private Sub frmLockerConfiguration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            FillCombos()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try

            If Condition = "" Or Condition = "LockerList" Then
                FillListBox(Me.lstLockers, "select LockerId, LockerName from tblDefLockers where Active=1")
            End If

            If Condition = "" Or Condition = "LockerAccounts" Then
                FillDropDown(Me.cmbLockerAccounts, "select coa_detail_id, detail_title from vwCOADetail where vwCOADetail.account_type in ('Cash','Bank')")
            End If

  Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdLockerList.DataSource = GetDataTable(" SELECT tblDefLockerDetails.LockerDetailId, vwCOADetail.detail_title FROM tblDefLockerDetails INNER JOIN tblDefLockers ON tblDefLockerDetails.LockerId = tblDefLockers.LockerId INNER JOIN vwCOADetail ON tblDefLockerDetails.AccountId = vwCOADetail.coa_detail_id where tblDefLockerDetails.LockerId=" & Me.lstLockers.SelectedValue)

            If Condition = "LockerSummary" Or Condition = "" Then
                Me.grdLockerSummary.DataSource = GetDataTable(" Sp_GetLockerSummary " & Me.lstLockers.SelectedValue & ", '" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy") & "', '" & dtpToDate.Value.ToString("dd-MMM-yyyy") & "'")
                Me.Chart1.DataSource = grdLockerSummary.DataSource
                Me.Chart1.Series(0).XValueMember = "AmountType"
                Me.Chart1.Series(0).YValueMembers = "Amount"
                Me.Chart1.DataBind()

                Me.Chart2.DataSource = grdLockerSummary.DataSource
                Me.Chart2.Series(0).XValueMember = "Account"
                Me.Chart2.Series(0).YValueMembers = "Amount"
                Me.Chart2.DataBind()


            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            If Condition = "Locker" Then
                If Me.txtLockerName.Text.Trim.Length = 0 Then
                    msg_Information("Please enter valid locker name")
                    Me.txtLockerName.Focus()
                    Return False
                End If

                SBDal.SQLHelper.ExecuteScaler(SBDal.SQLHelper.CON_STR, CommandType.Text, "insert into tblDefLockers (LockerName, Active, SortOrder) Values('" & Me.txtLockerName.Text & "', 1, 1)")

                FillCombos("LockerList")

                Me.txtLockerName.Text = String.Empty
                Return True

            End If

            If Condition = "LockerAccounts" Then
                If Not lstLockers.SelectedValue > 0 Then
                    msg_Information("Please select a locker")
                    Me.lstLockers.Focus()
                    Return False
                End If

                If cmbLockerAccounts.SelectedIndex = 0 Then
                    msg_Information("Please select cash or bank account")
                    Me.cmbLockerAccounts.Focus()
                    Return False
                End If

                SBDal.SQLHelper.ExecuteScaler(SBDal.SQLHelper.CON_STR, CommandType.Text, "insert into tblDefLockerDetails (LockerId, AccountId) Values('" & Me.lstLockers.SelectedValue & "', '" & Me.cmbLockerAccounts.SelectedValue & "' )")

                GetAllRecords()
                Me.cmbLockerAccounts.SelectedIndex = 0
                Return True

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

    End Function

    Private Sub btnAddLocker_Click(sender As Object, e As EventArgs) Handles btnAddLocker.Click
        Try
            If msg_Confirm("Do you want to save new locker") = True Then
                Me.Save("Locker")
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAddAccount_Click(sender As Object, e As EventArgs) Handles btnAddAccount.Click
        Try
            If GetDataTable("select LockerDetailId from tblDefLockerDetails where LockerId=" & Me.lstLockers.SelectedValue & " and AccountId=" & Me.cmbLockerAccounts.SelectedValue & " ").Rows.Count > 0 Then
                msg_Error("Account already exists in this briefcase, please select another account")
                cmbLockerAccounts.Focus()
                Exit Sub
            End If

            If msg_Confirm("Do you want to add this account in locker") = True Then
                Me.Save("LockerAccounts")
            End If



        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub lstLockers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstLockers.SelectedIndexChanged
        Try
            GetAllRecords()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            GetAllRecords("LockerSummary")
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
End Class
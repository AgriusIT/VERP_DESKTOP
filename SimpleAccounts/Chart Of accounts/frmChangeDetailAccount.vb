Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports SBDal
Imports SBModel
Imports SBUtility

Public Class frmChangeDetailAccount
    Implements IGeneral




    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty
            If Condition = "Account" Then
                FillUltraDropDown(Me.cmbVendor, "Select coa_detail_id,detail_title as [Account Name], detail_code as [Account Code], sub_sub_title as [Account Head], sub_sub_code as [Account Head Code], Account_type as [Type], main_sub_sub_id From vwCOADetail  WHERE detail_title <> '' ORDER BY detail_title ASC")
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("main_sub_sub_id").Hidden = True
            ElseIf Condition = "MainSubSub" Then
                FillUltraDropDown(Me.cmbSubSubAccount, "Select main_sub_sub_id, sub_sub_title as [Account Head], sub_sub_code as [Account Head Code] From tblCOAMainSubSub WHERE sub_sub_title <> '' ORDER BY Sub_Sub_Title ASC ")
                Me.cmbSubSubAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.cmbVendor.Value = 0 Then
                ShowErrorMessage("Please select account.")
                Me.cmbVendor.Focus()
                Return False
            End If
            If Me.cmbSubSubAccount.Value = 0 Then
                ShowErrorMessage("Please select sub sub account.")
                Me.cmbSubSubAccount.Focus()
                Return False
            End If

            If Me.cmbVendor.ActiveRow.Cells("Account Head Code").Value = Me.cmbSubSubAccount.ActiveRow.Cells("Account Head Code").Value Then
                ShowErrorMessage("Not Allowed Same Account Head.")
                Me.cmbSubSubAccount.Focus()
                Return False
            End If

            If Me.txtDetailAccountCode.Text = String.Empty Then
                ShowErrorMessage("New Detail Account Code Is Empty.")
                Me.txtDetailAccountCode.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.cmbVendor.Rows(0).Activate()
            Me.cmbSubSubAccount.Rows(0).Activate()
            Me.txtDetailAccountCode.Text = String.Empty
            Me.cmbVendor.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Dim objcon As New OleDbConnection(Con.ConnectionString)
        If objcon.State = ConnectionState.Closed Then objcon.Open()
        Dim trans As OleDbTransaction = objcon.BeginTransaction

        Try

            Dim cmd As New OleDbCommand

            cmd.Connection = objcon
            cmd.Transaction = trans
            cmd.CommandTimeout = 300
            cmd.CommandType = CommandType.Text


            Dim strDetailCode As String = String.Empty
            strDetailCode = GetLatestDetailCode(Me.cmbSubSubAccount.ActiveRow.Cells("Account Head Code").Value.ToString, trans)
            cmd.CommandText = "Update tblCOAMainSubSubDetail Set main_sub_sub_id=" & Me.cmbSubSubAccount.Value & ", detail_code='" & strDetailCode.Replace("'", "''") & "', old_main_sub_sub_id=" & Val(Me.cmbVendor.ActiveRow.Cells("main_sub_sub_id").Value.ToString) & ", old_detail_code='" & Me.cmbVendor.ActiveRow.Cells("Account Code").Value.ToString.Replace("'", "''") & "' WHERE coa_detail_id=" & Me.cmbVendor.Value & ""

            cmd.ExecuteNonQuery()


            trans.Commit()


            Return True




        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objcon.Close()
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If IsValidate() = True Then
                If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                ShowInformationMessage("Detail Account Has Been Changed.")
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbSubSubAccount_Leave(sender As Object, e As EventArgs) Handles cmbSubSubAccount.Leave
        Try
            Me.txtDetailAccountCode.Text = GetLatestDetailCode(Me.cmbSubSubAccount.ActiveRow.Cells("Account Head Code").Value.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function GetLatestDetailCode(mainCode As String, Optional trans As OleDbTransaction = Nothing) As String
        Try
            Dim strquery As String = String.Empty
            strquery = "Select IsNull(Max(Replace(Right(detail_code, CharIndex('-',reverse(detail_code),-1)),'-','')),0)+1 as LastCode From tblCOAMainSubSubDetail WHERE main_sub_sub_Id=" & Me.cmbSubSubAccount.Value & ""
            Dim dtAcDetailCode As New DataTable
            dtAcDetailCode = GetDataTable(strquery, trans)
            dtAcDetailCode.AcceptChanges()

            Dim intSerial As Integer = 0I
            If dtAcDetailCode.Rows.Count > 0 Then
                intSerial = Val(dtAcDetailCode.Rows(0).Item(0).ToString)
            Else
                intSerial = 1
            End If

            Return CStr(mainCode & "-" & Microsoft.VisualBasic.Right("00000" + CStr(intSerial), 5))

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmChangeDetailAccount_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            FillCombos("Account")
            FillCombos("MainSubSub")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0I



            If Me.cmbVendor.ActiveRow Is Nothing Then
                Me.cmbVendor.Rows(0).Activate()
            End If

            id = Me.cmbVendor.Value
            FillCombos("Account")
            Me.cmbVendor.ActiveRow.Cells(0).Value = id



            If Me.cmbSubSubAccount.ActiveRow Is Nothing Then
                Me.cmbSubSubAccount.Rows(0).Activate()
            End If

            id = Me.cmbSubSubAccount.Value
            FillCombos("MainSubSub")
            Me.cmbSubSubAccount.ActiveRow.Cells(0).Value = id


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
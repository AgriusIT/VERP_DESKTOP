Imports SBDal
Imports SBModel
Imports System.Data
Imports System.Data.SqlClient
Public Class frmProInvestor
    Implements IGeneral
    Dim Id As Integer = 0I
    Dim DetailAcccountId As Integer = 0
    Public Shared InvestorId As Integer
    Dim Investor As InvestorBE
    Public InvestorDAL As InvestorDAL = New InvestorDAL()
    Dim DetailAcccount As COADeail
    Public DetailAcccountDAL As COADetailDAL = New COADetailDAL()
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Private Sub frmProInvestor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim AccountSubSubId As Integer = Val(getConfigValueByType("InvestorSubSub").ToString)

            If AccountSubSubId <= 0 Then

                ShowErrorMessage("Please Select a sub sub Account to map Against the Investor")

            End If

            'FillCombos("COA")
            FillCombos("City")
            Dim dt As DataTable = New InvestorDAL().GetById(InvestorId)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    InvestorId = dt.Rows(0).Item("InvestorId")
                    txtName.Text = dt.Rows(0).Item("Name")
                    txtPrimaryMobile.Text = dt.Rows(0).Item("PrimaryMobile")
                    txtSecondaryMobile.Text = dt.Rows(0).Item("SecondaryMobile")
                    Me.txtCOA.Text = GetAccountById(Val(dt.Rows(0).Item("coa_detail_id").ToString))
                    txtProfitRatio.Text = dt.Rows(0).Item("ProfitRatio")
                    txtRemarks.Text = dt.Rows(0).Item("Remarks")
                    cbActive.Checked = dt.Rows(0).Item("Active")
                    txtCNIC.Text = dt.Rows(0).Item("CNIC")
                    txtEmailID.Text = dt.Rows(0).Item("Email")
                    txtAddressLine1.Text = dt.Rows(0).Item("AddressLine1")
                    txtAddressLine2.Text = dt.Rows(0).Item("AddressLine2")
                    cmbCity.SelectedValue = dt.Rows(0).Item("CityId")
                    btnSave.Enabled = DoHaveUpdateRights
                Next
            Else
                ReSetControls()
            End If

            'If InvestorId > 0 Then
            '    EditRecord()
            '    Me.btnSave.Text = "&Update"
            'End If
            btnSave.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

            btnCancel.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModelForAccount(Optional Condition As String = "")
        Try

            DetailAcccount = New COADeail
            DetailAcccount.COADetailID = DetailAcccountId
            DetailAcccount.DetailTitle = Me.txtName.Text
            DetailAcccount.Active = IIf(Me.cbActive.Checked = True, 1, 0)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Function GetAccountById(ByVal ID As Integer) As String
        Dim strSQL As String = String.Empty
        Dim DetailAccountTitle As String = String.Empty
        Try

            strSQL = " Select coa_detail_id , detail_title from tblCOAMainSubsubDetail where coa_detail_id = " & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)

            If dt.Rows.Count > 0 Then
                DetailAccountTitle = dt.Rows(0).Item("detail_title").ToString()
                DetailAcccountId = Val(dt.Rows(0).Item("coa_detail_id").ToString())
            End If
            Return DetailAccountTitle
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmProInvestor_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.KeyCode = Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            'If Condition = "COA" Then
            '    str = "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc"
            '    FillDropDown(Me.cmbCOA, str, False)
            'Else
            If Condition = "City" Then
                str = "Select CityID, CityName from tblListCity"
                FillDropDown(Me.cmbCity, str, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try

            Investor = New InvestorBE
            Investor.InvestorId = InvestorId
            Investor.Name = Me.txtName.Text
            Investor.PrimaryMobile = Me.txtPrimaryMobile.Text
            Investor.SecondaryMobile = Me.txtSecondaryMobile.Text
            Investor.ProfitRatio = txtProfitRatio.Text
            Investor.Remarks = txtRemarks.Text
            Investor.Active = cbActive.CheckState
            Investor.CNIC = txtCNIC.Text
            Investor.Email = txtEmailID.Text
            Investor.AddressLine1 = txtAddressLine1.Text
            Investor.AddressLine2 = txtAddressLine2.Text
            Investor.CityId = cmbCity.SelectedValue
            Investor.ActivityLog = New ActivityLog

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtName.Text = String.Empty Then
                ShowErrorMessage("Please enter Name")
                Me.txtName.Focus()
                Return False
            End If

            If Me.txtPrimaryMobile.Text = String.Empty Then
                ShowErrorMessage("Primary mobile is required")
                Me.txtPrimaryMobile.Focus()
                Return False
            End If
            If Me.txtAddressLine1.Text = String.Empty Then
                ShowErrorMessage("Address Line 1 is required")
                Me.txtAddressLine1.Focus()
                Return False
            End If
            FillModel()
            FillModelForAccount()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            txtName.Focus()
            InvestorId = 0
            'Me.btnSave.Text = "&Save"
            txtName.Text = ""
            txtEmailID.Text = ""
            txtPrimaryMobile.Text = ""
            txtSecondaryMobile.Text = ""
            txtCOA.Text = ""
            txtProfitRatio.Text = 0
            txtRemarks.Text = ""
            cbActive.Checked = True
            txtCNIC.Text = ""
            txtEmailID.Text = ""
            txtAddressLine1.Text = ""
            txtAddressLine2.Text = ""
            cmbCity.SelectedIndex = 0
            btnSave.Enabled = DoHaveSaveRights
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If InvestorId = 0 Then
                    Dim AccountSubSubId As Integer = Val(getConfigValueByType("InvestorSubSub").ToString)

                    If COADetailDAL.GetAccountName(DetailAcccount.DetailTitle, AccountSubSubId) = True Then

                        If msg_Confirm("Account name already exist do you want create account with same name") = True Then

                            DetailAcccountId = New COADetailDAL().Add(DetailAcccount, AccountSubSubId)
                            Investor.coa_detail_id = DetailAcccountId
                            If InvestorDAL.Add(Investor) Then
                                msg_Information("Record has been saved successfully.")
                                ReSetControls()
                                Me.Close()
                            End If

                        End If

                    Else

                        DetailAcccountId = New COADetailDAL().Add(DetailAcccount, AccountSubSubId)
                        Investor.coa_detail_id = DetailAcccountId
                        If InvestorDAL.Add(Investor) Then
                            msg_Information("Record has been saved successfully.")
                            ReSetControls()
                            Me.Close()
                        End If

                    End If
                Else

                    Dim AccountSubSubId As Integer = Val(getConfigValueByType("InvestorSubSub").ToString)

                    If COADetailDAL.GetAccountName(DetailAcccount.DetailTitle, AccountSubSubId) = True Then

                        If msg_Confirm("Account name already exist do you want create Account with same name") = True Then

                            Dim str As String = ""
                            Dim conn As New SqlConnection(SQLHelper.CON_STR)
                            Dim trans As SqlTransaction
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            trans = conn.BeginTransaction
                            If New COADetailDAL().Update(DetailAcccount, trans) Then
                                ShowInformationMessage("Account Title has Updated Successfully")
                            End If
                            trans.Commit()
                            Investor.coa_detail_id = DetailAcccountId
                            If InvestorDAL.Update(Investor) Then
                                msg_Information("Record has been updated successfully.")
                                ReSetControls()
                                Me.Close()
                            End If

                        End If

                    Else

                        Dim str As String = ""
                        Dim conn As New SqlConnection(SQLHelper.CON_STR)
                        Dim trans As SqlTransaction
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        trans = conn.BeginTransaction
                        If New COADetailDAL().Update(DetailAcccount, trans) Then
                            ShowInformationMessage("Account Title has Updated Successfully")
                        End If
                        trans.Commit()
                        Investor.coa_detail_id = DetailAcccountId
                        If InvestorDAL.Update(Investor) Then
                            msg_Information("Record has been updated successfully.")
                            ReSetControls()
                            Me.Close()
                        End If

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

    Private Sub txtPrimaryMobile_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrimaryMobile.KeyPress, txtSecondaryMobile.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
End Class
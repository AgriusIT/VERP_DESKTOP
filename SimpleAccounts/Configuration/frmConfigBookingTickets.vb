Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq


Public Class frmConfigBookingTickets

    Public isFormOpen As Boolean = False


    Private Sub frmConfigBookingTickets_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        FillCombos("TravelingAccount")
        getConfigValueList()
        GetAllRecords()
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")
        Try
            Me.cmbTravelingAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("TravelingAccountBookingTickets").ToString))

            Me.chkTravelingAccountFrontEnd.Checked = Convert.ToBoolean(getConfigValueByType("TravelingAccountFrontEndBookingTickets").ToString)

            If Me.chkTravelingAccountFrontEnd.Checked = False Then
                Me.chkTravelingAccountFrontEndNot.Checked = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillCombos(Optional ByVal Condition As String = "")

        Try
            Dim strSQL As String = String.Empty
            Select Case Condition

                Case "TravelingAccount"
                    FillDropDown(Me.cmbTravelingAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 And account_type = 'Expense' ORDER BY detail_title Asc")
            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTravelingAccount_Leave(sender As Object, e As EventArgs) Handles cmbTravelingAccount.Leave
        Try
            frmConfigCompany.saveComboBoxValueConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkTravelingAccountFrontEnd_CheckedChanged(sender As Object, e As EventArgs) Handles chkTravelingAccountFrontEnd.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

End Class
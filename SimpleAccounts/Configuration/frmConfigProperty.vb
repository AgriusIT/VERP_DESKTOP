Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Public Class frmConfigProperty

    Public isFormOpen As Boolean = False
    Private Sub frmConfigProperty_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.isFormOpen = True
        FillCombos("Agent")
        FillCombos("Dealer")
        FillCombos("Investor")
        FillCombos("Buyer")
        FillCombos("Seller")
        FillCombos("CommissionAccount")
        FillCombos("PropertySalesAccount")
        FillCombos("PropertyPurchaseAccount")
        FillCombos("InvestmentBookingAccount")
        FillCombos("ProfitExpenseAccount")
        'CostOfSalesAccount
        FillCombos("CostOfSalesAccount")
        FillCombos("CommissionAccountForPropertyProfile")
        FillCombos("OtherExpenseAccount")
        FillCombos("NDCExpenseAccount")
        getConfigValueList()
        GetAllRecords()
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "")
        Try
            Me.cmbAgent.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("AgentSubSub").ToString))
            Me.cmbDealer.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("DealerSubSub").ToString))
            Me.cmbInvestor.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("InvestorSubSub").ToString))
            Me.cmbSeller.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("SellerSubSub").ToString))
            Me.cmbBuyer.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("BuyerSubSub").ToString))
            Me.cmbCommisionAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CommissionAccount").ToString))
            Me.cmbSaleAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PropertySalesAccount").ToString))
            Me.cmbSysPuchaseAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("PropertyPurchaseAccount").ToString))
            Me.cmbInvestmentAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("InvestmentBookingAccount").ToString))
            Me.cmbProfitExpenseAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("ProfitExpenseAccount").ToString))
            ''TASK TFS4412
            Me.cmbCostOfSalesAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CostOfSalesAccountProperty").ToString))
            ''END TASK TFS4412
            ''Start TFS4496
            Me.cmbCommissionAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("CommissionAccountForPropertyProfile").ToString))
            Me.cmbNDCAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("NDCAccountForPropertyProfile").ToString))
            Me.cmbOtherExpenseAccount.SelectedValue = Convert.ToInt32(Val(getConfigValueByType("OtherExpenseAccountForPropertyProfile").ToString))
            ''End TFS4496
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "")

        Try
            Dim strSQL As String = String.Empty
            Select Case Condition

                Case "Agent"
                    FillDropDown(Me.cmbAgent, "select main_sub_sub_id, sub_sub_title +' - '+ sub_sub_code from tblCoaMainsubSub order by 2")
                Case "Dealer"
                    FillDropDown(Me.cmbDealer, "select main_sub_sub_id, sub_sub_title +' - '+ sub_sub_code from tblCoaMainsubSub order by 2")
                Case "Investor"
                    FillDropDown(Me.cmbInvestor, "select main_sub_sub_id, sub_sub_title +' - '+ sub_sub_code from tblCoaMainsubSub order by 2")
                Case "Seller"
                    FillDropDown(Me.cmbSeller, "select main_sub_sub_id, sub_sub_title +' - '+ sub_sub_code from tblCoaMainsubSub order by 2")
                Case "Buyer"
                    FillDropDown(Me.cmbBuyer, "select main_sub_sub_id, sub_sub_title +' - '+ sub_sub_code from tblCoaMainsubSub order by 2")
                Case "CommissionAccount"
                    FillDropDown(Me.cmbCommisionAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "PropertySalesAccount"
                    FillDropDown(Me.cmbSaleAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "PropertyPurchaseAccount"
                    FillDropDown(Me.cmbSysPuchaseAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "InvestmentBookingAccount"
                    FillDropDown(Me.cmbInvestmentAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "ProfitExpenseAccount"
                    FillDropDown(Me.cmbProfitExpenseAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "CostOfSalesAccount"
                    FillDropDown(Me.cmbCostOfSalesAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1  AND account_type ='Expense' ORDER BY detail_title Asc")
                Case "CommissionAccountForPropertyProfile"
                    FillDropDown(Me.cmbCommissionAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "OtherExpenseAccount"
                    FillDropDown(Me.cmbOtherExpenseAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
                Case "NDCExpenseAccount"
                    FillDropDown(Me.cmbNDCAccount, "Select coa_Detail_id as ID, detail_title as [Account Description] From vwCOADetail WHERE detail_title is not null AND Active=1 ORDER BY detail_title Asc")
            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbAgent_Leave(sender As Object, e As EventArgs) Handles cmbAgent.Leave, cmbSeller.Leave, cmbProfitExpenseAccount.Leave, cmbInvestor.Leave, _
        cmbBuyer.Leave, cmbDealer.Leave, cmbCommisionAccount.Leave, cmbSysPuchaseAccount.Leave, _
        cmbInvestmentAccount.Leave, cmbSaleAccount.Leave, cmbCostOfSalesAccount.Leave, cmbCommissionAccount.Leave, cmbNDCAccount.Leave, cmbOtherExpenseAccount.Leave

        Try
            frmConfigCompany.saveComboBoxValueConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkDB_Click(sender As Object, e As EventArgs) Handles linkDB.Click
        Try
            If frmConfigDB.isFormOpen = True Then
                frmConfigDB.Dispose()
            End If

            frmConfigDB.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
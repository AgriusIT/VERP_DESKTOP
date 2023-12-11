Imports System.Windows.Forms
Imports SBDal
Imports SBModel
Imports System.Data.OleDb

Public Class frmLoadPurchaseDemand
    Private Sub FillCombos(ByVal Condition As String)
        Dim strQuery As String = String.Empty
        Try
            If Condition = "PurchaseDemand" Then
                strQuery = "SELECT PurchaseDemandId, PurchaseDemandNo, PurchaseDemandDate FROM PurchaseDemandMasterTable WHERE Status = 'Open' ORDER BY PurchaseDemandId DESC"
                FillUltraDropDown(Me.cmbInquiryNumber, strQuery)
                Me.cmbInquiryNumber.Rows(0).Activate()
                Me.cmbInquiryNumber.DisplayLayout.Bands(0).Columns("PurchaseDemandId").Hidden = True
                Me.cmbInquiryNumber.DisplayMember = Me.cmbInquiryNumber.Rows(0).Cells(1).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSearchSalesInquiry_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombos("PurchaseDemand")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmSearchSalesInquiry_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            frmInquiryComparisonStatement.SetDemandId(Me.cmbInquiryNumber.Value)
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            frmInquiryComparisonStatement.SetDemandId(0)
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class

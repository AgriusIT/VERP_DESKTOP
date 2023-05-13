Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Public Class frmConfigInvSecurity

    Public isFormOpen As Boolean = False

    Private Sub frmConfigInvSecurity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try
            Me.chkItemFilterByLocation.Checked = Convert.ToBoolean(getConfigValueByType("ArticleFilterByLocation").ToString)

            If Me.chkItemFilterByLocation.Checked = False Then
                Me.chkItemFilterByLocationNot.Checked = True
            End If

            Me.chkCheckCurrentStockByItem.Checked = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)

            If Me.chkCheckCurrentStockByItem.Checked = False Then
                Me.chkCheckCurrentStockByItemNot.Checked = True
            End If

            Me.chkCheckCurrentStockByLocation.Checked = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByLocation").ToString)

            If Me.chkCheckCurrentStockByLocation.Checked = False Then
                Me.chkCheckCurrentStockByLocationNot.Checked = True
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkInventory_Click(sender As Object, e As EventArgs) Handles linkInventory.Click
        Try
            If frmConfigInventory.isFormOpen = True Then
                frmConfigInventory.Dispose()
            End If

            frmConfigInventory.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkAccounts_Click(sender As Object, e As EventArgs) Handles linkAccounts.Click
        Try
            If frmConfigInvAccounts.isFormOpen = True Then
                frmConfigInvAccounts.Dispose()
            End If

            frmConfigInvAccounts.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkItemFilterByLocation_CheckedChanged(sender As Object, e As EventArgs) Handles chkItemFilterByLocation.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkCheckCurrentStockByItem_CheckedChanged(sender As Object, e As EventArgs) Handles chkCheckCurrentStockByItem.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkCheckCurrentStockByLocation_CheckedChanged(sender As Object, e As EventArgs) Handles chkCheckCurrentStockByLocation.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

End Class
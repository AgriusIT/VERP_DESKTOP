Imports System
Imports System.Data
Imports System.Data.OleDb
Imports SBModel
Imports SBDal
Imports SBUtility

Public Class frmMapArticleAliasOnSalesOrder
    Public GetSalesOrderId As Integer = 0I
    Public GetSalesOrderDetailId As Integer = 0I
    Public GetSalesOrderNo As String = String.Empty
    Public GetSalesOrderDate As DateTime
    Public GetArticleAliasName As String = String.Empty
    Public GetArtileId As Integer = 0I
    Private IsOpenForm As Boolean = False
    Public SetArticleDefId As Integer = 0I
    Public SetArticleCode As String = String.Empty
    Public SetArticleDescription As String = String.Empty
    Sub FillCombo(Optional Condition As String = "")
        Try

            FillUltraDropDown(Me.cmbItem, "Select ArticleId,ArticleDescription as [Item], ArticleCode as [Code], ArticleGroupName as [Department],ArticleTypeName as [Type], ArticleLPOName as [Category] From ArticleDefView WHERE ArticleDescription != '' Order By ArticleDescription ASC")
            Me.cmbItem.Rows(0).Activate()
            Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmMapArticleAliasOnSalesOrder_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombo()
            Me.lblHeading.Text = "Sales Order No: " & GetSalesOrderNo.ToString() & ", Sales Order Date: " & Me.GetSalesOrderDate.ToString() & ""
            Me.txtArticleAlias.Text = Me.GetArticleAliasName.ToString
            Me.cmbItem.Value = GetArtileId
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnMap_Click(sender As Object, e As EventArgs) Handles btnMap.Click
        Try
            SetArticleCode = Me.cmbItem.ActiveRow.Cells("Code").Value.ToString
            SetArticleDescription = Me.cmbItem.ActiveRow.Cells("Item").Value.ToString
            SetArticleDefId = Me.cmbItem.Value
            GetArticleAliasName = Me.txtArticleAlias.Text

            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
Imports System.Windows.Forms
Imports SBDal
Imports SBModel
Imports System.Data.OleDb

Public Class frmSearchPurchaseDemand

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Dim UnCheckRows() As Janus.Windows.GridEX.GridEXRow = Me.grdItems.GetUncheckedRows
            Dim CheckRows() As Janus.Windows.GridEX.GridEXRow = Me.grdItems.GetCheckedRows
            If CheckRows.Length <= 0 Then
                msg_Error("At least one row is required. Please select rows")
            Else
                If frmPurchaseInquiry.grdItems.DataSource Is Nothing Then
                    frmPurchaseInquiry.DisplayDetail(-1)
                End If
                For Each ROW As Janus.Windows.GridEX.GridEXRow In Me.grdItems.GetCheckedRows
                    frmPurchaseInquiry.AddToGridFromSalesInquiry(ROW)
                Next
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub FillCombos(ByVal Condition As String)
        Dim strQuery As String = String.Empty
        Try
            If Condition = "Customer" Then
                strQuery = String.Empty
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    strQuery = "SELECT vwCOADetail.coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Vendor', 'Customer') and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbReference, strQuery)
                    Me.cmbReference.Rows(0).Activate()
                    Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Else
                    strQuery = "SELECT vwCOADetail.coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type = 'Vendor' and  vwCOADetail.coa_detail_id is not  null"
                    FillUltraDropDown(Me.cmbReference, strQuery)
                    Me.cmbReference.Rows(0).Activate()
                    Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                End If
            ElseIf Condition = "InquiryNumber" Then
                strQuery = "SELECT PurchaseDemandId, PurchaseDemandNo, PurchaseDemandDate FROM PurchaseDemandMasterTable ORDER BY PurchaseDemandId DESC"
                FillUltraDropDown(Me.cmbInquiryNumber, strQuery)
                Me.cmbInquiryNumber.Rows(0).Activate()
                Me.cmbInquiryNumber.DisplayLayout.Bands(0).Columns("PurchaseDemandId").Hidden = True
                Me.cmbInquiryNumber.DisplayMember = Me.cmbInquiryNumber.Rows(0).Cells(1).Column.Key.ToString
            ElseIf Condition = "InquiryNumberAgainstCustomer" Then
                strQuery = "SELECT PurchaseDemandId, PurchaseDemandNo, PurchaseDemandDate FROM PurchaseDemandMasterTable WHERE PurchaseDemandMasterTable.VendorId =" & Me.cmbReference.Value & "  ORDER BY PurchaseDemandId DESC"
                FillUltraDropDown(Me.cmbInquiryNumber, strQuery)
                Me.cmbInquiryNumber.Rows(0).Activate()
                Me.cmbInquiryNumber.DisplayLayout.Bands(0).Columns("PurchaseDemandId").Hidden = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            DisplayDetail()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub DisplayDetail()
        Try
            Dim IsAdmin As Boolean = False
            Dim GroupId As Integer = 0
            Dim UserId As Integer = 0
            GroupId = LoginGroupId
            UserId = LoginUserId
            Dim CustomerId As Integer = 0
            Dim FromDate As New DateTime
            Dim ToDate As New DateTime
            Dim SalesInquiryId As Integer = 0
            If Me.cmbReference.Value > 0 Then
                CustomerId = Me.cmbReference.Value
            End If
            If dtpInquiryFromDate.Checked = True Then
                FromDate = dtpInquiryFromDate.Value
            Else
                FromDate = DateTime.MinValue
            End If
            If dtpInquiryToDate.Checked = True Then
                ToDate = dtpInquiryToDate.Value
            Else
                ToDate = DateTime.MinValue
            End If
            If Me.cmbInquiryNumber.Value > 0 Then
                SalesInquiryId = Me.cmbInquiryNumber.Value
            End If
            Dim SalesInquiryDetailDAL As New SalesInquiryDetailDAL()
            Dim dt As New DataTable
            If Not Me.grdItems.DataSource Is Nothing Then
                dt = CType(Me.grdItems.DataSource, DataTable)
            End If
            Dim dt1 As DataTable = GetAgainstSearch(UserId, CustomerId, SalesInquiryId, FromDate, ToDate, IsAdmin)
            dt1.AcceptChanges()
            dt.Merge(dt1)
            dt.AcceptChanges()
            Me.grdItems.DataSource = dt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function GetAgainstSearch(ByVal UserId As Integer, ByVal CustomerId As Integer, ByVal SalesInquiryId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsAdmin As Boolean) As DataTable
        Try
            Dim dt As New DataTable
            Dim strSQl As String = "SELECT PurchaseDemandDetailTable.PurchaseDemandDetailId AS SalesInquiryDetailId, PurchaseDemandDetailTable.PurchaseDemandId AS SalesInquiryId, 0 AS SerialNo, Article.ArticleDescription AS RequirementDescription, PurchaseDemandDetailTable.ArticleDefId AS ArticleId, Article.ArticleCode AS Code, Article.ArticleDescription, Article.ArticleUnitId AS UnitId, Unit.ArticleUnitName AS Unit, Article.ArticleTypeId AS ItemTypeId, Type.ArticleTypeName AS Type, Article.ArticleCategoryId AS CategoryId, Category.ArticleCompanyName AS Category, Article.ArticleLPOId AS SubCategoryId, SubCategory.ArticleLpoName AS SubCategory, Article.ArticleGenderId AS OriginId, Origin.ArticleGenderName AS Origin, PurchaseDemandDetailTable.Qty, PurchaseDemandDetailTable.Comments FROM ArticleTypeDefTable AS Type RIGHT OUTER JOIN ArticleCompanyDefTable AS Category RIGHT OUTER JOIN ArticleDefTable AS Article ON Category.ArticleCompanyId = Article.ArticleCategoryId LEFT OUTER JOIN ArticleGenderDefTable AS Origin ON Article.ArticleGenderId = Origin.ArticleGenderId LEFT OUTER JOIN ArticleLpoDefTable AS SubCategory ON Article.ArticleLPOId = SubCategory.ArticleLpoId ON Type.ArticleTypeId = Article.ArticleTypeId LEFT OUTER JOIN ArticleUnitDefTable AS Unit ON Article.ArticleUnitId = Unit.ArticleUnitId RIGHT OUTER JOIN PurchaseDemandDetailTable INNER JOIN PurchaseDemandMasterTable ON PurchaseDemandDetailTable.PurchaseDemandId = PurchaseDemandMasterTable.PurchaseDemandId ON Article.ArticleId = PurchaseDemandDetailTable.ArticleDefId WHERE PurchaseDemandDetailTable.PurchaseDemandDetailId <> 0"
            If CustomerId > 0 Then
                strSQl += " And PurchaseDemandMasterTable.VendorId = " & CustomerId & ""
            End If
            If SalesInquiryId > 0 Then
                strSQl += " And PurchaseDemandDetailTable.PurchaseDemandId = " & SalesInquiryId & ""
            End If
            If Not FromDate = DateTime.MinValue Then
                strSQl += " And PurchaseDemandMasterTable.PurchaseDemandDate >= Convert(DateTime, '" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102)"
            End If
            If Not ToDate = DateTime.MinValue Then
                strSQl += " And PurchaseDemandMasterTable.PurchaseDemandDate <= Convert(DateTime, '" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)"
            End If
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmSearchSalesInquiry_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombos("Customer")
            FillCombos("InquiryNumber")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdItems_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdItems.ColumnButtonClick
        Try
            If e.Column.Key = "Remove" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                Me.grdItems.GetRow.Delete()
                Me.grdItems.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSearchSalesInquiry_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            Me.grdItems.DataSource = Nothing
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbReference_ValueChanged(sender As Object, e As EventArgs) Handles cmbReference.ValueChanged
        Try
            If Me.cmbReference.Value > 0 Then
                FillCombos("InquiryNumberAgainstCustomer")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbInquiryNumber_Enter(sender As Object, e As EventArgs) Handles cmbInquiryNumber.Enter
        Me.cmbInquiryNumber.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
    End Sub
End Class

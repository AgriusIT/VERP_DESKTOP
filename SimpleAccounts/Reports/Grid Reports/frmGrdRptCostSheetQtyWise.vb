Imports System
Imports System.Data

Public Class frmGrdRptCostSheetQtyWise
    Dim IsOpenForm As Boolean = False
    Private Sub frmGrdRptCostSheetQtyWise_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            FillCombo()
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try

            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            If IsOpenForm = True Then Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "List of Material Detail" & Chr(10) & "Quotation No. " & IIf(Me.cmbQuotation.ActiveRow Is Nothing, "", Me.cmbQuotation.ActiveRow.Cells("Quotation No").Value.ToString) & ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            FillUltraDropDown(Me.cmbQuotation, "SELECT Quot.QuotationId, Quot.QuotationNo as [Quotation No], Quot.QuotationDate as [Date], Quot.VendorId, COA.detail_title as [Customer], COA.detail_code as [Account Code], COA.account_type as [Type]  FROM dbo.QuotationMasterTable AS Quot LEFT OUTER JOIN dbo.vwCOADetail AS COA ON Quot.VendorId = COA.coa_detail_id Order By QuotationNo DESC")
            Me.cmbQuotation.Rows(0).Activate()
            Me.cmbQuotation.DisplayLayout.Bands(0).Columns("QuotationId").Hidden = True
            Me.cmbQuotation.DisplayLayout.Bands(0).Columns("VendorId").Hidden = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FillGrid()
        Try

            Dim strSQL As String = "SP_QuotationItemSpecification " & Me.cmbQuotation.Value & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()


            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()

            grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            grd.GroupByBoxVisible = True
            Me.grd.RootTable.Columns("QuotationId").Visible = False
            Me.grd.RootTable.Columns("QuotationDetailId").Visible = False
            Me.grd.RootTable.Columns("CostSheetId").Visible = False
            Me.grd.RootTable.Columns("ArticleId").Visible = False
            Me.grd.RootTable.Columns("ArticleCode").Visible = False
            Me.grd.RootTable.Columns("ArticleSizeName").Visible = False
            Me.grd.RootTable.Columns("ArticleColorName").Visible = False
            Me.grd.RootTable.Columns("Qty").Visible = False
            Me.grd.RootTable.Columns("Specification_Qty").Visible = False
            Me.grd.RootTable.Columns("PurchasePrice").Visible = False
            Me.grd.RootTable.Columns("SalePrice").Visible = False
            Me.grd.RootTable.Columns("Cost Value").Visible = False
            Me.grd.RootTable.Columns("Discount").Visible = False
            Me.grd.RootTable.Columns("Total Amount").Visible = False
            Me.grd.RootTable.Columns("Quoted").Visible = False
            Me.grd.RootTable.Columns("MasterID").Visible = False
            Me.grd.RootTable.Columns("After Discount Value").Visible = False
            Me.grd.RootTable.Columns("Item Category").Visible = False
            Me.grd.RootTable.Columns("Tax").Visible = False
            Me.grd.RootTable.Columns("Net Amount").Visible = False

            Me.grd.RootTable.Columns("Detail_Code").Caption = "A/Code"
            Me.grd.RootTable.Columns("Detail_Title").Caption = "Customer"

            Me.grd.RootTable.Columns("CityName").Caption = "City"
            Me.grd.RootTable.Columns("StateName").Caption = "Region"
            Me.grd.RootTable.Columns("TerritoryName").Caption = "Area"


            Me.grd.RootTable.Columns("Specification_Item_Description").Caption = "Item"
            Me.grd.RootTable.Columns("Specification_Item_Code").Caption = "Item Code"
            Me.grd.RootTable.Columns("CostQty").Caption = "Qty"


            Me.grd.RootTable.Columns("CostQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("CostQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("CostQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("CostQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            Me.grd.RootTable.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True
            Dim grpArticleDescription As New Janus.Windows.GridEX.GridEXGroup
            grpArticleDescription.Column = Me.grd.RootTable.Columns("ArticleDescription")
            Me.grd.RootTable.Groups.Add(grpArticleDescription)

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.cmbQuotation.IsItemInList = False Then Exit Sub
            If Me.cmbQuotation.ActiveRow Is Nothing Then
                ShowErrorMessage("Invalid Quotation No.")
                Me.cmbQuotation.Focus()
                Exit Sub
            End If
            If Me.cmbQuotation.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select quotation.")
                Me.cmbQuotation.Focus()
                Exit Sub
            End If
            If Me.grd.RowCount > 0 Then
                Me.grd.ClearStructure()
            End If
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            If Me.cmbQuotation.ActiveRow Is Nothing Then
                Me.cmbQuotation.Rows(0).Activate()
            End If
            id = Me.cmbQuotation.ActiveRow.Cells(0).Value
            FillCombo()
            Me.cmbQuotation.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
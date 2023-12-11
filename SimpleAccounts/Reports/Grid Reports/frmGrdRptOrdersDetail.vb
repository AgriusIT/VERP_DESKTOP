Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Public Class frmGrdRptOrdersDetail

    Enum Customer
        Id
        Name
        State
        City
        Territory
        ExpiryDate
        Discount
        Other_Exp
        Fuel
        TransitInsurance
        Credit_Limit
        Type
        Email
        PhoneNo
    End Enum
    Private _IsFormOpen As Boolean = False
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            FillGrid()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub frmGrdRptSalesByGender_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.tbProgressbar.Visible = False
            Call fill()
            FillCombo()
            _IsFormOpen = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0
            id = Me.cmbAccount.Value
            fill()
            Me.cmbAccount.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillGrid()
        Me.tbProgressbar.Visible = True
        Try

            If OptSalesOrder.Checked = True AndAlso rbtLoose.Checked = True Then
                Dim dt As New DataTable
                ''TASK TFS1574 Added columns like size, color, D Date, PO DATE and PONo
                Dim str As String = "select SalesOrderDate,a.salesorderno as SalesOrderNo,a.salesorderid as SalesOrderId, a.Delivery_Date As [D Date], a.PO_Date As [PO Date], a.PONo As [PO No], Case When IsNull(Qty, 0) > IsNull(DeliveredTotalQty,0) Then 'Open' Else 'Close' End as Status,a.vendorid as Customer_ID,f.detail_title as Customer_Name " _
                                & ",a.locationid as Location_ID,c.companyname as Company_Name," _
                                & "b.articledefid as Article_Id,e.articledescription as Article_Desc" _
                                & ",b.articlesize as Unit, e.ArticleSizeName As Size, e.ArticleColorName As Color, IsNull(qty,0) as Qty,IsNull(price,0) as Price,(Isnull(qty,0) * IsNull(price,0)) as Gross" _
                                & ",a.costcenterid as CostCenter_ID,IsNull(SalesTax_Percentage,0) as STPer" _
                                & ",((IsNull(SalesTax_Percentage,0)/100)*((IsNull(qty,0) *  IsNull(price,0)))) as SalesTaxAmount, " _
                                & "  ((IsNull(SalesTax_Percentage,0)/100)*((IsNull(qty,0) *  IsNull(price,0))))  + (IsNull(qty,0) * IsNull(price,0)) as NetAmount ,IsNull(DeliveredTotalQty,0) as [DeliveredQty], (IsNull(Qty, 0)-IsNull(DeliveredTotalQty, 0)) As [PendingQty], a.Remarks, b.Comments " _
                                & " from SalesOrderMasterTable a INNER JOIN SalesOrderDetailTable b on a.salesorderid = b.salesorderid LEFT OUTER JOIN CompanyDefTable c on c.CompanyId =a.LocationId LEFT OUTER JOIN tblDefLocation d   on d.Location_Id = b.LocationId INNER JOIN ArticleDefView e on e.ArticleId = b.ArticleDefId LEFT OUTER JOIN vwCOADetail f on f.coa_detail_id = a.vendorId " _
                                & " where(a.salesorderno <> '') "
                If Me.cmbStatus.SelectedIndex >= 0 Then
                    str = str & "And " & IIf(Me.cmbStatus.Text = "All", "a.Status In('Open', 'Close', 'Reject', 'DeActive')", "a.Status = '" & Me.cmbStatus.SelectedItem.ToString & "' ") & " "
                End If
                'Dim fromdate As String
                'Dim todate As String

                'fromdate = Me.dtpFrom.Value.Year & "-" & Me.dtpFrom.Value.Month & "-" & Me.dtpFrom.Value.Day & " 00:00:00"
                'todate = Me.dtpTo.Value.Year & "-" & Me.dtpTo.Value.Month & "-" & Me.dtpTo.Value.Day & " 23:59:59"
                'fromdate = Format(dtpFrom.Value, "dd/MMM/yyyy")
                'todate = Format(dtpTo.Value, "dd/MMM/yyyy")
                'str = str & "and a.salesorderdate between '" & fromdate & "' and '" & todate & "'"
                'str = str & " and (Convert(varchar,a.salesorderdate,102) between Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))"
                If Me.dtpFrom.Checked = True Then
                    str = str & " and (Convert(varchar,a.salesorderdate,102) >= Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)) "
                End If
                If Me.dtpTo.Checked = True Then
                    str = str & " and (Convert(varchar,a.salesorderdate,102) <= Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)) "
                End If

                str = str & IIf(Me.cmbAccount.SelectedRow.Cells(0).Value > 0, " AND a.vendorid=" & Me.cmbAccount.Value & "", "") & ""
                str = str & " order by a.salesorderdate,a.salesorderid "



                Dim adp As SqlDataAdapter
                adp = New SqlDataAdapter(str, SQLHelper.CON_STR)
                adp.Fill(dt)
                dt.AcceptChanges()
                Me.grd.DataSource = dt
                Me.grd.SetDataBinding(dt, "")
                Me.grd.RetrieveStructure()
                ApplyGridSetting()

            ElseIf OptSalesOrder.Checked = True AndAlso rbtPack.Checked = True Then
                Dim dt As New DataTable
                ''TASK TFS1574 Added columns like size, color, D Date, PO DATE and PONo
                Dim str As String = "select SalesOrderDate,a.salesorderno as SalesOrderNo,a.salesorderid as SalesOrderId, a.Delivery_Date As [D Date], a.PO_Date As [PO Date], a.PONo As [PO No], Case When IsNull(Qty, 0) > IsNull(DeliveredTotalQty,0) Then 'Open' Else 'Close' End as Status,a.vendorid as Customer_ID,f.detail_title as Customer_Name " _
                                & ",a.locationid as Location_ID,c.companyname as Company_Name," _
                                & " b.articledefid as Article_Id,e.articledescription as Article_Desc" _
                                & ", b.articlesize as Unit, e.ArticleSizeName As Size, e.ArticleColorName As Color, IsNull(qty,0) as Qty,IsNull(qty/e.PackQty,0) As PackQty,IsNull(price,0) as Price,(Isnull(qty,0) * IsNull(price,0)) as Gross" _
                                & " ,a.costcenterid as CostCenter_ID,IsNull(SalesTax_Percentage,0) as STPer" _
                                & " ,((IsNull(SalesTax_Percentage,0)/100)*((IsNull(qty,0) *  IsNull(price,0)))) as SalesTaxAmount, " _
                                & "  ((IsNull(SalesTax_Percentage,0)/100)*((IsNull(qty,0) *  IsNull(price,0))))  + (IsNull(qty,0) * IsNull(price,0)) as NetAmount ,IsNull(DeliveredTotalQty/e.PackQty,0) as [DeliveredQty], (IsNull(Qty, 0)-IsNull(DeliveredTotalQty, 0))/e.PackQty As [PendingQty], a.Remarks, b.Comments " _
                                & " from SalesOrderMasterTable a INNER JOIN SalesOrderDetailTable b on a.salesorderid = b.salesorderid LEFT OUTER JOIN CompanyDefTable c on c.CompanyId =a.LocationId LEFT OUTER JOIN tblDefLocation d   on d.Location_Id = b.LocationId INNER JOIN ArticleDefView e on e.ArticleId = b.ArticleDefId LEFT OUTER JOIN vwCOADetail f on f.coa_detail_id = a.vendorId " _
                                & " where(a.salesorderno <> '') "
                If Me.cmbStatus.SelectedIndex >= 0 Then
                    str = str & "And " & IIf(Me.cmbStatus.Text = "All", "a.Status In('Open', 'Close', 'Reject', 'DeActive')", "a.Status = '" & Me.cmbStatus.SelectedItem.ToString & "' ") & " "
                End If
                'Dim fromdate As String
                'Dim todate As String

                'fromdate = Me.dtpFrom.Value.Year & "-" & Me.dtpFrom.Value.Month & "-" & Me.dtpFrom.Value.Day & " 00:00:00"
                'todate = Me.dtpTo.Value.Year & "-" & Me.dtpTo.Value.Month & "-" & Me.dtpTo.Value.Day & " 23:59:59"
                'fromdate = Format(dtpFrom.Value, "dd/MMM/yyyy")
                'todate = Format(dtpTo.Value, "dd/MMM/yyyy")
                'str = str & "and a.salesorderdate between '" & fromdate & "' and '" & todate & "'"
                'str = str & " and (Convert(varchar,a.salesorderdate,102) between Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))"
                If Me.dtpFrom.Checked = True Then
                    str = str & " and (Convert(varchar,a.salesorderdate,102) >= Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)) "
                End If
                If Me.dtpTo.Checked = True Then
                    str = str & " and (Convert(varchar,a.salesorderdate,102) <= Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)) "
                End If

                str = str & IIf(Me.cmbAccount.SelectedRow.Cells(0).Value > 0, " AND a.vendorid=" & Me.cmbAccount.Value & "", "") & ""
                str = str & " order by a.salesorderdate,a.salesorderid "



                Dim adp As SqlDataAdapter
                adp = New SqlDataAdapter(str, SQLHelper.CON_STR)
                adp.Fill(dt)
                dt.AcceptChanges()
                Me.grd.DataSource = dt
                Me.grd.SetDataBinding(dt, "")
                Me.grd.RetrieveStructure()
                ApplyGridSetting()

            ElseIf OptPurchasOrder.Checked = True AndAlso rbtLoose.Checked = True Then
                Dim dt As New DataTable
                ''TASK TFS1574 Added columns like size, color and detail
                Dim str As String = "select PurchaseOrderDate,a.purchaseorderid as PurchaseOrderId,a.PurchaseOrderNo as PurchaseOrderNo, Case When IsNull(b.Qty, 0) > IsNull(b.ReceivedTotalQty, 0) Then 'Open' Else 'Close' End as Status,a.vendorid as Supplier_ID,f.detail_title as Supplier_Name,f.CustomerType As SupplierType " _
                                    & ",a.locationid as Location_ID,c.companyname as Company_Name, '' As Detail, " _
                                    & "b.articledefid as Article_Id,e.articledescription as Article_Desc " _
                                    & " , b.articlesize as Unit, e.ArticleSizeName As Size, e.ArticleColorName As Color, IsNull(qty,0) as Qty,IsNull(price,0) as Price,(IsNull(qty,0) * IsNull(price,0)) as Gross " _
                                    & ",IsNull(TaxPercent,0) as TaxPer " _
                                    & ",((IsNull(TaxPercent,0)/100)*((IsNull(qty,0) *  IsNull(price,0)))) as SalesTaxAmount, " _
                                    & "  ((IsNull(TaxPercent,0)/100)*((IsNull(qty,0) *  IsNull(price,0))))  + (IsNull(qty,0) * IsNull(price,0)) as NetAmount, IsNull(b.ReceivedTotalQty, 0) As [DeliveredQty], (IsNull(b.Qty, 0)-IsNull(b.ReceivedTotalQty, 0)) As [PendingQty], a.POType As [PO Type], IsNull(tblStockDispatchStatus.StockDispatchStatusName, 0) As [Dispatch Status], a.Remarks, b.Comments " _
                                    & " from PurchaseOrderMasterTable a INNER JOIN PurchaseOrderDetailTable b on b.PurchaseOrderId = a.purchaseorderid  LEFT OUTER JOIN CompanyDefTable c On c.CompanyId = a.LocationId LEFT OUTER JOIN tblDefLocation d on d.location_id = b.LocationId INNER JOIN ArticleDefView e on e.ArticleId = b.ArticleDefId LEFT OUTER JOIN tblStockDispatchStatus ON a.POStockDispatchStatus = tblStockDispatchStatus.StockDispatchStatusID LEFT OUTER JOIN vwCOADetail f on f.coa_detail_id = a.VendorId " _
                                    & " where(a.Purchaseorderno <> '') "
                If Me.cmbStatus.SelectedIndex >= 0 Then
                    str = str & "And " & IIf(Me.cmbStatus.Text = "All", "a.Status In('Open', 'Close', 'Reject', 'DeActive')", "a.Status = '" & Me.cmbStatus.SelectedItem.ToString & "' ") & " "
                End If
                'str = str & " and (Convert(varchar,a.purchaseorderdate,102) between Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))"
                If Me.dtpFrom.Checked = True Then
                    str = str & " And (Convert(varchar,a.purchaseorderdate,102) >= Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)) "
                End If
                If Me.dtpTo.Checked = True Then
                    str = str & " And (Convert(varchar,a.purchaseorderdate,102) <= Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)) "
                End If
                str = str & IIf(Me.cmbAccount.SelectedRow.Cells(0).Value > 0, " AND a.vendorid=" & Me.cmbAccount.Value & " ", "") & ""
                str = str & " order by a.purchaseorderdate,a.purchaseorderid "
                Dim adp As SqlDataAdapter
                adp = New SqlDataAdapter(str, SQLHelper.CON_STR)
                adp.Fill(dt)
                ''TASK TFS1574
                For Each Row As DataRow In dt.Rows
                    Dim strDetail As String = ""
                    Dim strPurchaseOrder As String = "Select  IsNull(PurchaseOrderId, 0) As PurchaseOrderId, + ' GRN No:' + IsNull(ReceivingNo, '') + ', Date : ' + Convert(varchar(12), ReceivingDate, 113) + ', IGP No: ' + IsNull(IGPNo, '') + ', Vendor invoice: ' + IsNull(vendor_invoice_no, '') + ', Party invoice: ' + IsNull(PartyInvoiceNo, '') As Detail FROM ReceivingNoteMasterTable Where PurchaseOrderID =" & Row.Item("PurchaseOrderId") & " "
                    Dim dtDetail As DataTable = GetDataTable(strPurchaseOrder)
                    If dtDetail.Rows.Count > 0 Then
                        For Each Row1 As DataRow In dtDetail.Rows
                            If strDetail.Length > 0 Then
                                strDetail = strDetail & ", " & Row1.Item(1).ToString
                            Else
                                strDetail = Row1.Item(1).ToString
                            End If
                        Next
                        Row.BeginEdit()
                        Row("Detail") = strDetail
                        Row.EndEdit()
                    End If
                Next
                ''END TASK TFS1574
                Me.grd.DataSource = dt
                Me.grd.SetDataBinding(dt, "")
                Me.grd.RetrieveStructure()
                ApplyGridSetting()
            ElseIf OptPurchasOrder.Checked = True AndAlso rbtPack.Checked = True Then
                Dim dt As New DataTable
                ''TASK TFS1574 Added columns like size, color and detail
                Dim str As String = "select PurchaseOrderDate,a.purchaseorderid as PurchaseOrderId,a.PurchaseOrderNo as PurchaseOrderNo, Case When IsNull(b.Qty, 0) > IsNull(b.ReceivedTotalQty, 0) Then 'Open' Else 'Close' End as Status,a.vendorid as Supplier_ID,f.detail_title as Supplier_Name,f.CustomerType As SupplierType " _
                                    & ",a.locationid as Location_ID,c.companyname as Company_Name, '' As Detail, " _
                                    & "b.articledefid as Article_Id,e.articledescription as Article_Desc " _
                                    & ", b.articlesize as Unit, e.ArticleSizeName As Size, e.ArticleColorName As Color, IsNull(qty,0) as Qty,IsNull(qty/e.PackQty,0) As PackQty,IsNull(price,0) as Price,(IsNull(qty,0) * IsNull(price,0)) as Gross " _
                                    & ",IsNull(TaxPercent,0) as TaxPer " _
                                    & ",((IsNull(TaxPercent,0)/100)*((IsNull(qty,0) *  IsNull(price,0)))) as SalesTaxAmount, " _
                                    & "  ((IsNull(TaxPercent,0)/100)*((IsNull(qty,0) *  IsNull(price,0))))  + (IsNull(qty,0) * IsNull(price,0)) as NetAmount, IsNull(b.DeliveredQty/e.PackQty, 0) As [DeliveredQty], ((IsNull(b.Sz1, 0)-IsNull(b.DeliveredQty, 0))/e.PackQty) As [PendingQty], a.POType As [PO Type], IsNull(tblStockDispatchStatus.StockDispatchStatusName, 0) As [Dispatch Status], a.Remarks, b.Comments " _
                                    & " from PurchaseOrderMasterTable a INNER JOIN PurchaseOrderDetailTable b on b.PurchaseOrderId = a.purchaseorderid  LEFT OUTER JOIN CompanyDefTable c On c.CompanyId = a.LocationId LEFT OUTER JOIN tblDefLocation d on d.location_id = b.LocationId INNER JOIN ArticleDefView e on e.ArticleId = b.ArticleDefId LEFT OUTER JOIN tblStockDispatchStatus ON a.POStockDispatchStatus = tblStockDispatchStatus.StockDispatchStatusID LEFT OUTER JOIN vwCOADetail f on f.coa_detail_id = a.VendorId " _
                                    & " where(a.Purchaseorderno <> '') "
                If Me.cmbStatus.SelectedIndex >= 0 Then
                    str = str & "And " & IIf(Me.cmbStatus.Text = "All", "a.Status In('Open', 'Close', 'Reject', 'DeActive')", "a.Status = '" & Me.cmbStatus.SelectedItem.ToString & "' ") & " "
                End If
                'str = str & " and (Convert(varchar,a.purchaseorderdate,102) between Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) and Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))"
                If Me.dtpFrom.Checked = True Then
                    str = str & " And (Convert(varchar,a.purchaseorderdate,102) >= Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102)) "
                End If
                If Me.dtpTo.Checked = True Then
                    str = str & " And (Convert(varchar,a.purchaseorderdate,102) <= Convert(DateTime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)) "
                End If
                str = str & IIf(Me.cmbAccount.SelectedRow.Cells(0).Value > 0, " AND a.vendorid=" & Me.cmbAccount.Value & " ", "") & ""
                str = str & " order by a.purchaseorderdate,a.purchaseorderid "
                Dim adp As SqlDataAdapter
                adp = New SqlDataAdapter(str, SQLHelper.CON_STR)
                adp.Fill(dt)
                ''TASK TFS1574
                For Each Row As DataRow In dt.Rows
                    Dim strDetail As String = ""
                    Dim strPurchaseOrder As String = "Select  IsNull(PurchaseOrderId, 0) As PurchaseOrderId, + ' GRN No:' + IsNull(ReceivingNo, '') + ', Date : ' + Convert(varchar(12), ReceivingDate, 113) + ', IGP No: ' + IsNull(IGPNo, '') + ', Vendor invoice: ' + IsNull(vendor_invoice_no, '') + ', Party invoice: ' + IsNull(PartyInvoiceNo, '') As Detail FROM ReceivingNoteMasterTable Where PurchaseOrderID =" & Row.Item("PurchaseOrderId") & " "
                   

                    Dim dtDetail As DataTable = GetDataTable(strPurchaseOrder)
                    If dtDetail.Rows.Count > 0 Then
                        For Each Row1 As DataRow In dtDetail.Rows
                            If strDetail.Length > 0 Then
                                strDetail = strDetail & ", " & Row1.Item(1).ToString
                            Else
                                strDetail = Row1.Item(1).ToString
                            End If
                        Next
                        Row.BeginEdit()
                        Row("Detail") = strDetail
                        Row.EndEdit()
                    End If
                Next
                ''END TASK TFS1574
                Me.grd.DataSource = dt
                Me.grd.SetDataBinding(dt, "")
                Me.grd.RetrieveStructure()
                ApplyGridSetting()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.tbProgressbar.Visible = False
        End Try
    End Sub
    Public Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            If Me.grd.RootTable Is Nothing Then Exit Sub
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed


            If OptSalesOrder.Checked = True Then
                Me.grd.RootTable.Columns("SalesOrderID").Visible = False
                Me.grd.RootTable.Columns("Customer_ID").Visible = False
                Me.grd.RootTable.Columns("CostCenter_ID").Visible = False
                Me.grd.RootTable.Columns("SalesOrderDate").FormatString = "dd/MMM/yyyy"

                Me.grd.RootTable.Columns("PO Date").FormatString = "dd/MMM/yyyy"
                Me.grd.RootTable.Columns("D Date").FormatString = "dd/MMM/yyyy"


            ElseIf OptPurchasOrder.Checked = True Then
                Me.grd.RootTable.Columns("purchaseorderid").Visible = False
                Me.grd.RootTable.Columns("Supplier_ID").Visible = False
                Me.grd.RootTable.Columns("PurchaseOrderDate").FormatString = "dd/MMM/yyyy"
            End If
            If OptPurchasOrder.Checked = True Or OptSalesOrder.Checked = True Then
                If rbtPack.Checked = True Then
                    Me.grd.RootTable.Columns("PackQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns("PackQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns("PackQty").FormatString = "N" & DecimalPointInQty
                    Me.grd.RootTable.Columns("PackQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns("PackQty").TotalFormatString = "N" & DecimalPointInQty
                    'Me.grd.RootTable.Columns("qty").Visible = False
                End If
            End If
            'Applied Total of PendindQty, DeliveredQty in Grid by Waqar Raza on 06-10-2016 
            Me.grd.RootTable.Columns("Location_ID").Visible = False
            Me.grd.RootTable.Columns("Article_Id").Visible = False
            Me.grd.RootTable.Columns("qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            'Me.grd.RootTable.Columns("price").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Gross").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("SalesTaxAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("NetAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("DeliveredQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("PendingQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            Me.grd.RootTable.Columns("qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Gross").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("SalesTaxAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("NetAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DeliveredQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PendingQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("qty").FormatString = "N" & DecimalPointInQty

            Me.grd.RootTable.Columns("price").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Gross").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("SalesTaxAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("NetAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("DeliveredQty").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("PendingQty").FormatString = "N" & DecimalPointInValue


            Me.grd.RootTable.Columns("qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Gross").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("SalesTaxAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("NetAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("DeliveredQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("PendingQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("qty").TotalFormatString = "N" & DecimalPointInQty

            'Me.grd.RootTable.Columns("price").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Gross").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("SalesTaxAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("NetAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("DeliveredQty").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("PendingQty").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = "Orders Details"
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs1 As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs1)
                fs1.Dispose()
                fs1.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Orders Detail" & Chr(10) & " Dealer/Vendor (" & IIf(Me.cmbAccount.Value > 0, Me.cmbAccount.Text, "All") & ")" & Chr(10) & "Date From: " & Me.dtpFrom.Value.ToString("dd-MM-yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd-Mm-yyyy") & " "
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub fill()
        Try

            FillUltraDropDown(Me.cmbAccount, "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                            "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone " & _
                                                            "FROM tblCustomer LEFT OUTER JOIN " & _
                                                            "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                            "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                            "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                            "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                            "WHERE (vwCOADetail.account_type=" & IIf(Me.OptSalesOrder.Checked = True, "'Customer'", "'Vendor'") & " and  vwCOADetail.coa_detail_id is not  null)")
            Me.cmbAccount.Rows(0).Activate()
            If cmbAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub OptSalesOrder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptSalesOrder.CheckedChanged
        Try
            If _IsFormOpen = False Then Exit Sub
            Call fill()
            LblType.Text = "Customer"
            Me.lblHeader.Text = "Sales Order Detail"
            Me.Text = "Sales Order Detail"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub OptPurchasOrder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptPurchasOrder.CheckedChanged
        Try
            If _IsFormOpen = False Then Exit Sub
            Call fill()
            LblType.Text = "Vendor"
            Me.lblHeader.Text = "Purchase Order Detail"
            Me.Text = "Purchase Order Detail"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombo()
        Try
            Dim strStatus() As String = System.Enum.GetNames(GetType(EnumStatus))
            Me.cmbStatus.Items.Clear()
            For Each sts As String In strStatus
                Me.cmbStatus.Items.Add(sts)
            Next
            Me.cmbStatus.SelectedIndex = -1
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class

Public Class frmHistorysalespurchase

    Dim BaseCurrencyId As Integer = 0
    Dim BaseCurrencyName As String = String.Empty
    'TFS1201: Rai Haider:29-Aug-17:Add Grid on Qoutation Screen for last 5 sales and purchase
    'Start Task
    Public Function GetItemPurchase(ByVal ArticleDefId As Integer) As DataTable
        Try
            Dim str As String = "SELECT  Top (5)   ReceivingMasterTable.ReceivingNo As DOCNO, ReceivingMasterTable.ReceivingDate AS Date, vwCOADetail.detail_title As Vendor, ReceivingDetailTable.Price," & _
            "ReceivingDetailTable.ReceivedQty As Qty,(ReceivingDetailTable.Price*ReceivingDetailTable.ReceivedQty*ISNULL((ReceivingDetailTable.CurrencyRate),1)) As NetValue," & _
            "Case When tblcurrency.currency_code is null then 'PKR' else tblcurrency.currency_code End as Currency,ISNULL((ReceivingDetailTable.CurrencyRate),1) as CurrencyRate FROM vwCOADetail " & _
            "INNER JOIN ReceivingMasterTable ON vwCOADetail.coa_detail_id = ReceivingMasterTable.VendorId " & _
            "RIGHT OUTER JOIN ReceivingDetailTable LEFT OUTER JOIN tblcurrency ON ReceivingDetailTable.CurrencyId = tblcurrency.currency_id ON ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId " & _
            "where ReceivingDetailTable.ArticleDefId=" & ArticleDefId & " Order by ReceivingMasterTable.ReceivingDate Desc"
            Dim dtPurchase As DataTable = GetDataTable(str)
            Me.grdItemPurchse.DataSource = dtPurchase
            Me.grdItemPurchse.RetrieveStructure()
            Me.grdItemPurchse.RootTable.Columns("CurrencyRate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdItemPurchse.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdItemPurchse.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdItemPurchse.RootTable.Columns("NetValue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub FillCombo()
        Try
            Dim str As String
            str = "Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(tblCurrencyRate.CurrencyRate, 0) As CurrencyRate From tblCurrency Left Outer Join(Select * FROM tblCurrencyRate Where CurrencyRateId in (Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) tblCurrencyRate On tblCurrency.currency_id = tblCurrencyRate.CurrencyId "
            FillDropDown(Me.cmbCurrency, str, False)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetItemSales(ByVal ArticleDefId As Integer) As DataTable
        Try
            Dim str As String = "SELECT TOP (5) SalesMasterTable.SalesNo as DOCNO,SalesMasterTable.SalesDate as Date,vwCOADetail.detail_title As Customer,SalesDetailTable.Price," & _
            "SalesDetailTable.Qty,(SalesDetailTable.Price*SalesDetailTable.Qty*ISNULL((SalesDetailTable.CurrencyRate),1)) as NetValue," & _
            "Case When tblcurrency.currency_code is null then 'PKR' else tblcurrency.currency_code End as Currency,ISNULL((SalesDetailTable.CurrencyRate),1) as CurrencyRate From SalesMasterTable INNER JOIN " & _
            "vwCOADetail ON SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id RIGHT OUTER JOIN " & _
            "SalesDetailTable LEFT OUTER JOIN tblcurrency ON SalesDetailTable.CurrencyId = tblcurrency.currency_id ON SalesMasterTable.SalesId = SalesDetailTable.SalesId " & _
            "where SalesDetailTable.ArticleDefId=" & ArticleDefId & " ORDER BY SalesMasterTable.SalesDate DESC"
            Dim dtSales As DataTable = GetDataTable(str)
            Me.grdItemSales.DataSource = dtSales
            Me.grdItemSales.RetrieveStructure()
            Me.grdItemSales.RootTable.Columns("CurrencyRate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdItemSales.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdItemSales.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdItemSales.RootTable.Columns("NetValue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmHistorysalespurchase_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.grdItemSales.DataSource = Nothing
            Me.grdItemPurchse.DataSource = Nothing
            FillCombo()
            BaseCurrencyId = Val(getConfigValueByType("Currency").ToString)
            BaseCurrencyName = GetBasicCurrencyName(BaseCurrencyId)
            If frmQoutationNew.cmbItem.Value > 0 Then
                GetItemSales(QoutationItemID)
                GetItemPurchase(QoutationItemID)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        End Sub
    Public Function GetAllItemPurchase(ByVal ArticleDefId As Integer) As DataTable
        Try
            Dim str As String = "SELECT ReceivingMasterTable.ReceivingNo As DOCNO, ReceivingMasterTable.ReceivingDate AS Date, vwCOADetail.detail_title As Vendor, ReceivingDetailTable.Price," & _
            "ReceivingDetailTable.ReceivedQty As Qty,(ReceivingDetailTable.Price*ReceivingDetailTable.ReceivedQty*ISNULL((ReceivingDetailTable.CurrencyRate),1)) As NetValue," & _
            "Case When tblcurrency.currency_code is null then 'PKR' else tblcurrency.currency_code End as Currency,ISNULL((ReceivingDetailTable.CurrencyRate),1) as CurrencyRate FROM vwCOADetail " & _
            "INNER JOIN ReceivingMasterTable ON vwCOADetail.coa_detail_id = ReceivingMasterTable.VendorId " & _
            "RIGHT OUTER JOIN ReceivingDetailTable LEFT OUTER JOIN tblcurrency ON ReceivingDetailTable.CurrencyId = tblcurrency.currency_id ON ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId " & _
            "where ReceivingDetailTable.ArticleDefId=" & ArticleDefId & " Order by ReceivingMasterTable.ReceivingDate Desc"
            Dim dtPurchase As DataTable = GetDataTable(str)
            Me.grdItemPurchse.DataSource = dtPurchase
            Me.grdItemPurchse.RetrieveStructure()
            Me.grdItemPurchse.RootTable.Columns("CurrencyRate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdItemPurchse.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdItemPurchse.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdItemPurchse.RootTable.Columns("NetValue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllItemSales(ByVal ArticleDefId As Integer) As DataTable
        Try
            Dim str As String = "SELECT SalesMasterTable.SalesNo as DOCNO,SalesMasterTable.SalesDate as Date,vwCOADetail.detail_title As Customer,SalesDetailTable.Price," & _
            "SalesDetailTable.Qty,(SalesDetailTable.Price*SalesDetailTable.Qty*ISNULL((SalesDetailTable.CurrencyRate),1)) as NetValue," & _
            "Case When tblcurrency.currency_code is null then 'PKR' else tblcurrency.currency_code End as Currency,ISNULL((SalesDetailTable.CurrencyRate),1) as CurrencyRate From SalesMasterTable INNER JOIN " & _
            "vwCOADetail ON SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id RIGHT OUTER JOIN " & _
            "SalesDetailTable LEFT OUTER JOIN tblcurrency ON SalesDetailTable.CurrencyId = tblcurrency.currency_id ON SalesMasterTable.SalesId = SalesDetailTable.SalesId " & _
            "where SalesDetailTable.ArticleDefId=" & ArticleDefId & " ORDER BY SalesMasterTable.SalesDate DESC"
            Dim dtSales As DataTable = GetDataTable(str)
            Me.grdItemSales.DataSource = dtSales
            Me.grdItemSales.RetrieveStructure()
            Me.grdItemSales.RootTable.Columns("CurrencyRate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdItemSales.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdItemSales.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdItemSales.RootTable.Columns("NetValue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnLoadAll.Click
        GetAllItemSales(QoutationItemID)
        GetAllItemPurchase(QoutationItemID)
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Try
            Dim dt As DataTable = CType(Me.grdItemSales.DataSource, DataTable)
            dt.AcceptChanges()
            For Each r As DataRow In dt.Rows
                r.BeginEdit()
                If Not Me.cmbCurrency.Text = "" Then
                    r("Currency") = Me.cmbCurrency.Text
                End If
                If Not Me.txtCurrencyRate.Text = "" Then
                    r("CurrencyRate") = Me.txtCurrencyRate.Text
                End If
                r("NetValue") = r("Price") * r("Qty") * r("CurrencyRate")
                r.EndEdit()
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
        Try
            If Not Me.cmbCurrency.SelectedItem Is Nothing Then
                Dim dr As DataRowView = CType(cmbCurrency.SelectedItem, DataRowView)
                Me.txtCurrencyRate.Text = dr.Row.Item("CurrencyRate").ToString

                ''TASK TFS1474
                If Me.cmbCurrency.SelectedValue = BaseCurrencyId Then
                    Me.txtCurrencyRate.Enabled = False
                Else
                    Me.txtCurrencyRate.Enabled = True
                End If
                ''END TASK TFS1474
                If Val(Me.txtCurrencyRate.Text) = 0 Then
                    Me.txtCurrencyRate.Text = 1
                End If
                'Me.grdItemSales.RootTable.Columns("CurrencyRate").Caption = "Currency Rate (" & Me.cmbCurrency.Text & ")"
               
                grdItemSales.AutoSizeColumns()
                'If cmbCurrency.SelectedValue = Me.BaseCurrencyId Then
                '    Me.grdItemSales.RootTable.Columns("CurrencyRate").Visible = False
                '    Else
                '    Me.grdItemSales.RootTable.Columns("CurrencyRate").Visible = True
                'End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
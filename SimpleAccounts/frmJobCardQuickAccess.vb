Public Class frmJobCardQuickAccess

    Private Sub btnInventory_Click(sender As Object, e As EventArgs) Handles btnInventory.Click
        Try
            frmMain.LoadControl("frmNewInvItem")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPurchase_Click(sender As Object, e As EventArgs) Handles btnPurchase.Click
        Try
            frmMain.LoadControl("frmPurchase")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPurchaseReturn_Click(sender As Object, e As EventArgs) Handles btnPurchaseReturn.Click
        Try
            frmMain.LoadControl("PurchaseReturn")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSales_Click(sender As Object, e As EventArgs) Handles btnSales.Click
        Try
            frmMain.LoadControl("RecordSales")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSalesReturn_Click(sender As Object, e As EventArgs) Handles btnSalesReturn.Click
        Try
            frmMain.LoadControl("SalesReturn")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnStockDispatch_Click(sender As Object, e As EventArgs) Handles btnStockDispatch.Click
        Try
            frmMain.LoadControl("Stock Dispatch")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnStockReceiving_Click(sender As Object, e As EventArgs) Handles btnStockReceiving.Click
        Try
            frmMain.LoadControl("Stock Receiving")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnStoreIssuence_Click(sender As Object, e As EventArgs) Handles btnStoreIssuence.Click
        Try
            frmMain.LoadControl("StoreIssuence")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPayment_Click(sender As Object, e As EventArgs) Handles btnPayment.Click
        Try
            frmMain.LoadControl("VendorPayments")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnReceipt_Click(sender As Object, e As EventArgs) Handles btnReceipt.Click
        Try
            frmMain.LoadControl("CustomerCollection")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnStockStatement_Click(sender As Object, e As EventArgs) Handles btnStockStatement.Click
        Try
            frmMain.LoadControl("frmRptGrdStockStatement")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnJobCard_Click(sender As Object, e As EventArgs) Handles btnJobCard.Click
        Try
            frmMain.LoadControl("frmDefJobCard")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnJCAssignment_Click(sender As Object, e As EventArgs) Handles btnJCAssignment.Click
        Try
            frmMain.LoadControl("frmLiftAssociation")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmJobCardQuickAccess_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.btnInventory.Visible = True
            Me.btnSales.Visible = True
            Me.btnSalesReturn.Visible = True
            Me.btnStoreIssuence.Visible = True
            Me.btnStockReceiving.Visible = True
            Me.btnStockDispatch.Visible = True
            Me.btnPayment.Visible = True
            Me.btnPurchase.Visible = True
            Me.btnPurchaseReturn.Visible = True
            Me.btnReceipt.Visible = True
            Me.btnStockStatement.Visible = True
            Me.btnJobCard.Visible = True
            Me.btnJCAssignment.Visible = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
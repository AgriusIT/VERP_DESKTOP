Public Class frmRptItemWiseStockLedger

    
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub

            AddRptParam("@CustomerCode", Val(Me.cmbCustomer.Value.ToString))
            AddRptParam("@ItemCode", Val(Me.cmbItem.Value.ToString))
            AddRptParam("@FromDate", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptItemWiseStockLedger")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "Item" Then
                Dim strSQL As String = String.Empty
                strSQL = "select ArticleId,ArticleDescription from ArticleDefTable where ArticleDescription <> ''"
                FillUltraDropDown(Me.cmbItem, strSQL)
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleId").Hidden = True
            ElseIf Condition = "Customers" Then
                Dim strSQL As String = String.Empty
                strSQL = "Select coa_detail_id, detail_title as [Customer], detail_code as [Code], Account_Type as [Type], Contact_Mobile as Mobile, Contact_Email as Email From vwCOADetail where detail_title <> '' and Account_Type in ('Customer','Vendor')"
                FillUltraDropDown(Me.cmbCustomer, strSQL)
                Me.cmbCustomer.Rows(0).Activate()
                Me.cmbCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmRptItemWiseStockLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ResetControls()
        Try
            Me.dtpFrom.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpTo.Value = Date.Now
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmRptItemWiseStockLedger_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombo("Item")
            FillCombo("Customers")
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
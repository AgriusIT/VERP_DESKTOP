Public Class frmRptSalesCertificateLedger

    Private Sub frmRptSalesCertificateLedger_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            Me.dtpFromDate.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpToDate.Value = Date.Now
            FillCombo()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillCombo()
        Try

            FillUltraDropDown(Me.cmbVendor, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], sub_sub_title as [Account Head] From vwCOADetail WHERE detail_title <> '' and account_type in('Customer','Vendor') ")
            Me.cmbVendor.Rows(0).Activate()
            If Me.cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            If Me.cmbVendor.IsItemInList = False Then Exit Sub
            If Me.cmbVendor.Value <= 0 Then
                ShowErrorMessage("Please select customer")
                Me.cmbVendor.Focus()
                Exit Sub
            End If
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("@CustomerId", Me.cmbVendor.Value)
            ShowReport("rptSalesCertificateLedger")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
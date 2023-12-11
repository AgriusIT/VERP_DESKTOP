Public Class frmRptCashDetail
    Public Sub FillList()
        Try

            FillListBox(Me.lstCashAccount.ListItem, "Select coa_detail_id, Detail_title, detail_code From vwCOADetail WHERE Account_Type in('Cash','Bank') AND detail_title <> ''")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmRptCashDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
           
            Me.dtpFromDate.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpToDate.Value = Date.Now
            FillList()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("@IDs", Me.lstCashAccount.SelectedIDs)
            AddRptParam("@BankOpening", GetOpeningBalance(Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"), Me.lstCashAccount.SelectedIDs, "Bank"))
            AddRptParam("@CashOpening", GetOpeningBalance(Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"), Me.lstCashAccount.SelectedIDs, "Cash"))
            ShowReport("rptCashDetail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetOpeningBalance(ByVal OpeningDate As DateTime, ByVal IDs As String, ByVal AcType As String) As Double
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select SUM(IsNull(debit_amount,0)-IsNull(credit_amount,0)) as Opening From tblVoucherDetail INNER JOIN vwCOADetail COA On COA.coa_detail_id = tblVoucherDetail.coa_detail_id INNER JOIN tblVoucher on tblVoucher.Voucher_id = tblVoucherDetail.Voucher_Id WHERE tblVoucherDetail.coa_detail_id IN(" & IDs & ") and COA.Account_Type='" & AcType & "' AND (Convert(varchar,tblVoucher.Voucher_date,102) < Convert(Datetime,'" & OpeningDate.ToString("yyyy-M-d 00:00:00") & "',102)) ")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows(0).Item(0).ToString)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
End Class
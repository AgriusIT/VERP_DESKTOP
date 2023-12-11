Public Class frmRptPostDatedCheques
    Public Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            Dim str As String = String.Empty
            If Condition = "Accounts" Then
                'str = String.Empty
                'str = "Select coa_detail_id, detail_title from vwcoadetail WHERE detail_title  is not null AND Account_Type='Bank'"
                'FillDropDown(Me.cmbAccounts, str)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmRptPostDatedCheques_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombo("Accounts")
            Me.cmbTransaction.Text = "Received Cheques"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function ReportQuery() As DataTable
        Try
            Dim str As String = String.Empty
            Dim dt As New DataTable
            Dim adp As OleDb.OleDbDataAdapter
            str = "SELECT Isnull(Opening.opning,0) as Opening,  Coa.Detail_Code, V_D.coa_detail_id, " _
                   & "   COA.detail_title, V.voucher_No as voucher_code, V_Type.voucher_type, V.voucher_date, V_D.comments, V_D.debit_amount, V_D.credit_amount, COA.account_type, COA.main_sub_id, coa.CityName,  isnull(V_D.CostCenterID,0) as CostCenterID, V.Cheque_No, V.Cheque_Date FROM dbo.tblVoucher V INNER JOIN " _
                   & "   dbo.tblVoucherDetail V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
                   & "   dbo.vwcoadetail COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                   & "   dbo.tblDefVoucherType V_Type ON V.voucher_type_id = V_Type.voucher_type_id left outer join " _
                   & "  (SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning FROM dbo.tblVoucher V INNER JOIN " _
                   & "   dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                   & "   WHERE CONVERT(Datetime, V.voucher_date, 102) < GetDate() " _
                   & "   GROUP BY VD.coa_detail_id) Opening On OPening.COA_Detail_ID = V_D.COA_Detail_ID " _
                   & "   Where convert(Datetime, v.cheque_date,102) >= Convert(Datetime, GetDate(), 102)"
            If Me.cmbTransaction.Text = "Received Cheques" Then
                str += " AND V.Voucher_Type_Id=5"
            ElseIf Me.cmbTransaction.Text = "Issued Cheques" Then
                str += " AND V.Voucher_Type_ID=4"
            ElseIf Me.cmbTransaction.Text = "Both" Then
                str += " AND V.Voucher_Type_ID IN(4,5)"
            End If
            str += " ORDER BY V.Voucher_code asc"
            adp = New OleDb.OleDbDataAdapter(str, Con)
            adp.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub BtnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGenerate.Click
        Try
            ShowReport("PostDatedCheques", , , , False, , , Me.ReportQuery())
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
''3-Sep-2014 Task:2823 Imran Ali Invoice Aging Formated Report Revised (Ravi)
Public Class frmRptInvoiceAgingFormated
    Public Sub FillCombo()
        Try
            Dim str As String = String.Empty
            str = "SELECT coa_detail_id as ID,  detail_title as Account, detail_code as [Code] , account_type as [Type], cityname as City FROM dbo.vwCOADetail WHERE (coa_detail_id > 0) AND vwCOADetail.Account_Type IN('Customer','Vendor')"
            str = str & " order by detail_title"
            Me.cmbAccounts.DataSource = Nothing
            FillUltraDropDown(Me.cmbAccounts, str)
            Me.cmbAccounts.Rows(0).Activate()
            Me.cmbAccounts.DisplayLayout.Bands(0).Columns("ID").Hidden = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmRptInvoiceAgingFormated_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombo()
            Me.cmbAccounts.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            ''3-Sep-2014 Task:2823 Imran Ali Invoice Aging Formated Report Revised (Ravi)
            If Me.cmbAccounts.ActiveRow Is Nothing Then Exit Sub
            Dim objCMD As New OleDb.OleDbCommand
            Dim strSQL As String = String.Empty
            strSQL = "SP_AdjustmentBalance '" & Date.Now.ToString("yyyy-M-d 00:00:00") & "', " & Me.cmbAccounts.Value & ""
            Dim objDT As New DataTable
            objDT = GetDataTable(strSQL)

            If objDT IsNot Nothing Then
                If objDT.Rows.Count > 0 Then
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    objCMD.Connection = Con
                    objCMD.CommandType = CommandType.Text
                    objCMD.CommandText = "Truncate Table tblTempInvoiceAdjustmentBalances"
                    objCMD.ExecuteNonQuery()
                    Con.Close()
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    Dim objTrans As OleDb.OleDbTransaction = Con.BeginTransaction
                    objCMD.Transaction = objTrans
                    objCMD.Connection = Con
                    Try
                        For Each r As DataRow In objDT.Rows
                            objCMD.CommandText = String.Empty
                            objCMD.CommandType = CommandType.Text
                            objCMD.CommandText = "INSERT INTO tblTempInvoiceAdjustmentBalances(AccountId, DocNo,Amount) VALUES(" & Val(r.Item("AccountId").ToString) & ", '" & r.Item("DocNo").ToString.Replace("'", "''") & "', " & Val(r.Item("Amount").ToString) & ")"
                            objCMD.ExecuteNonQuery()
                        Next
                        objTrans.Commit()
                    Catch ex As Exception
                        Throw ex
                        objTrans.Rollback()
                    Finally
                        Con.Close()
                    End Try
                End If
            End If
            'End Task
            AddRptParam("@ToDate", Date.Now.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@AccountId", Me.cmbAccounts.Value)
            ShowReport("rptInvoiceAgingFormated")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
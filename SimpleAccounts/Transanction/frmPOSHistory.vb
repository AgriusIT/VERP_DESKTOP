Imports System.Data.OleDb
Imports SBModel
Imports SBDal
Imports System
Public Class frmPOSHistory

    Dim PrintLog As PrintLogBE
    Dim flgGetAllPOS As Boolean = False
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmPOSHistory_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
                Print()
            ElseIf e.KeyCode = Keys.F2 Then
                frmPOSEntry.EditRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPOSHistory_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If frmPOSEntry.IsGetAllPOSRights = True Then
                Me.btnGetAllPOS.Enabled = True
            Else
                Me.btnGetAllPOS.Enabled = False
            End If
            flgGetAllPOS = False
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub GetAll()
        Try
            Try
                Dim str As String = String.Empty
                str = "SELECT Top " & If(txtNo.Text = "", 50, txtNo.Text) & " SalesId, LocationId, SalesNo, SalesDate, SalesTime, SalesMasterTable.CustomerCode, SalesMasterTable.CustomerName as CustomerName, EmployeeCode, SalesQty, SalesAmount,InvoiceType, CashPaid, Balance, Salesmastertable.UserName, PreviousBalance, CostCenterId, MobileNo, Salesmastertable.Remarks1, Adjustment, Adj_Percentage, CNIC, tblVoucher.voucher_id As VoucherId, tblVoucher.voucher_no As VoucherNo, tblVoucher.voucher_date As VoucherDate, SalesStartTime, SalesEndTime, HoldStartTime, HoldEndTime, POSFlag, Adj_Flag, HoldFlag, PackingManId, BillMakerId FROM SalesMasterTable LEFT OUTER JOIN vwCOADetail ON SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN tblVoucher ON SalesMasterTable.SalesNo = tblVoucher.voucher_code WHERE LocationId = " & frmPOSEntry.CID & " And (POSFlag = 1) " & IIf(rdoNonPaid.Checked = True, "AND (isnull(SalesMasterTable.SalesNo, '') not in (SELECT isnull(voucher_code, '') from tblvoucher where voucher_type_id <> 7) AND isnull(SalesMasterTable.SalesId, 0) not in (SELECT ISNULL(InvoiceId, 0) from tblVoucherDetail) AND HoldFlag = 0 AND InvoiceType = 'Credit') ", "" & IIf(rdoClosed.Checked = True, "AND (tblVoucher.voucher_no = tblVoucher.voucher_code) ", "AND HoldFlag = " & 1 & "") & "") & " "
                If Me.dtpFromDate.Checked = True Then
                    str += " AND SalesDate >= Convert(Datetime, N'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)"
                End If
                If Me.dtpToDate.Checked = True Then
                    str += " AND SalesDate <= Convert(Datetime, N'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)"
                End If
                If flgGetAllPOS = False Then
                    If frmPOSEntry.Title <> "" AndAlso frmPOSEntry.CID <> 0 Then
                        str += " AND SalesNo Like '%" & frmPOSEntry.Title & frmPOSEntry.CID & "%'"
                    End If
                Else
                    Dim str1 As String = "SELECT tblPOSConfiguration.POSTitle FROM tblUserPOSRights INNER JOIN tblPOSConfiguration ON tblUserPOSRights.POSId = tblPOSConfiguration.POSId WHERE (tblUserPOSRights.UserId = " & LoginUserId & ") AND (tblUserPOSRights.Rights = 1)"
                    Dim dt1 As DataTable = GetDataTable(str1)
                    If dt1.Rows.Count > 0 Then
                        str += " AND( "
                        Dim i As Integer = 0
                        For Each r As DataRow In dt1.Rows
                            If i <> 0 Then
                                str += " OR "
                            End If
                            str += " SalesNo Like '%" & r.Item(0).ToString & frmPOSEntry.CID & "%'"
                            i += i + 1
                        Next
                        str += " ) "
                    End If
                End If
                str += " ORDER BY SalesMasterTable.SalesNo DESC"
                Dim dt As DataTable = GetDataTable(str)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                Me.grd.RootTable.Columns("SalesStartTime").FormatString = "dd/MMM/yyyy hh:mm:ss tt"
                Me.grd.RootTable.Columns("SalesEndTime").FormatString = "dd/MMM/yyyy hh:mm:ss tt"
                Me.grd.RootTable.Columns("HoldStartTime").Visible = False
                Me.grd.RootTable.Columns("SalesQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("SalesAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("CashPaid").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("PreviousBalance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Adjustment").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("Adj_Percentage").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("CNIC").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns("SalesId").Visible = False
                Me.grd.RootTable.Columns("LocationId").Visible = False
                Me.grd.RootTable.Columns("CostCenterId").Visible = False
                Me.grd.RootTable.Columns("CustomerCode").Visible = False
                Me.grd.RootTable.Columns("EmployeeCode").Visible = False
                Me.grd.RootTable.Columns("VoucherId").Visible = False
                Me.grd.RootTable.Columns("VoucherNo").Visible = False
                Me.grd.RootTable.Columns("VoucherDate").Visible = False
                Me.grd.RootTable.Columns("PackingManId").Visible = False
                Me.grd.RootTable.Columns("BillMakerId").Visible = False
                Me.grd.RootTable.Columns("Remarks1").Caption = "Remarks"
                'If grd.RowCount > 0 Then
                '    frmPOSEntry.LocationId = grd.CurrentRow.Cells("LocationId").Value
                'End If
                If Me.grd.RootTable.Columns.Contains("Column1") = False Then
                    Me.grd.RootTable.Columns.Add("Column1")
                    Me.grd.RootTable.Columns("Column1").ActAsSelector = True
                    Me.grd.RootTable.Columns("Column1").UseHeaderSelector = True
                End If
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grd.RowDoubleClick
        Try
            frmPOSEntry.EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'TFS3665: 28/June/2018: Waqar Raza: Added these lines to delete the POS entry from Sales, Voucher, BArdana and Stock Table
    'Start TFS3665:
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If frmPOSEntry.IsDeleteRights = True Then
                Dim cm As New OleDbCommand
                If Con.State = ConnectionState.Closed Then Con.Open()
                cm.Connection = Con

                cm.CommandText = "DELETE from tblVoucherdetail where voucher_id in (SELECT voucher_id FROM tblVoucher WHERE voucher_code = '" & grd.CurrentRow.Cells("SalesNo").Value.ToString & "')"
                cm.ExecuteNonQuery()
                cm.CommandText = "DELETE from tblVoucher where voucher_id in (SELECT voucher_id FROM tblVoucher WHERE voucher_code = '" & grd.CurrentRow.Cells("SalesNo").Value.ToString & "')"
                cm.ExecuteNonQuery()
                cm.CommandText = "DELETE from SalesDetailTable where SalesId = " & Val(grd.CurrentRow.Cells("SalesId").Value.ToString) & ""
                cm.ExecuteNonQuery()
                cm.CommandText = "DELETE from SalesMasterTable where SalesId = " & Val(grd.CurrentRow.Cells("SalesId").Value.ToString) & ""
                cm.ExecuteNonQuery()
                cm.CommandText = "DELETE from StockDetailTable where StockTransId in (Select StockTransId from StockMasterTable Where DocNo = '" & grd.CurrentRow.Cells("SalesNo").Value.ToString & "')"
                cm.ExecuteNonQuery()
                cm.CommandText = "DELETE from StockMasterTable where DocNo = '" & grd.CurrentRow.Cells("SalesNo").Value.ToString & "'"
                cm.ExecuteNonQuery()
                Dim str1 As String
                Dim dt1 As DataTable
                str1 = "SELECT SalesDetailId FROM tblBardanaAdjustment where SalesDetailId = " & Val(grd.CurrentRow.Cells("SalesId").Value.ToString) & ""
                dt1 = GetDataTable(str1)
                Dim SalesId As Integer
                If dt1.Rows.Count > 0 Then
                    SalesId = dt1.Rows(0).Item("SalesDetailId")
                End If
                cm.CommandText = "DELETE from tblBardanaAdjustment where SalesDetailId = " & SalesId & ""
                cm.ExecuteNonQuery()
                msg_Information(str_informDelete)
                SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Sales, grd.CurrentRow.Cells("SalesNo").Value.ToString, True)
                GetAll()
                'End TFS3665:
            Else
                msg_Error("Sorry! You don't have rights to delete this entry")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoClosed_CheckedChanged(sender As Object, e As EventArgs) Handles rdoClosed.CheckedChanged, RdoHold.CheckedChanged, rdoNonPaid.CheckedChanged, rdoNonPaid.CheckedChanged
        Try
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Print()
        Dim IsPreviewSaleInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
        Dim newinvoice As Boolean = False
        Dim strCriteria As String = "Nothing"
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = Me.grd.GetRow.Cells("SalesNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            newinvoice = getConfigValueByType("NewInvoice")
            If newinvoice = True Then
                str_ReportParam = "@SaleID|" & Me.grd.CurrentRow.Cells("SalesId").Value
            Else
                str_ReportParam = String.Empty
                strCriteria = "{SalesDetailTable.SalesId} = " & Val(Me.grd.CurrentRow.Cells("SalesId").Value)
            End If
            ShowReport(IIf(newinvoice = False, "SalesInvoice", "SalesInvoiceNew") & Me.grd.CurrentRow.Cells("LocationId").Value, strCriteria, "Nothing", "Nothing", True, , "New", , , , , )
            SaveActivityLog("POS", Me.Text, EnumActions.Print, LoginUserId, EnumRecordType.Sales, Me.grd.GetRow.Cells("SalesNo").Value.ToString, True)
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnUpdateAll_Click(sender As Object, e As EventArgs) Handles btnUpdateAll.Click
        Try
            If frmPOSEntry.IsUpdateAllRights = True Then
                If Me.grd.GetCheckedRows.Length > 0 Then
                    Dim blnStatus As Boolean = False
                    Me.btnUpdateAll.Enabled = False
                    For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
                        Me.grd.Row = r.Position
                        frmPOSEntry.EditRecord()
                        If frmPOSEntry.Update1() = True Then
                            blnStatus = True
                        Else
                            blnStatus = False
                        End If
                    Next
                    If blnStatus = True Then msg_Information("Selected records updated successfully.") : Me.btnUpdateAll.Enabled = True
                    Me.Close()
                Else
                    msg_Error("No rows are selected to update.")
                End If
            Else
                msg_Error("Sorry! You don't have rights to Update all.")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnGetAllPOS_Click(sender As Object, e As EventArgs) Handles btnGetAllPOS.Click
        Try
            flgGetAllPOS = True
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
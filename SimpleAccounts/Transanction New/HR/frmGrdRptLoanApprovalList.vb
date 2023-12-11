Imports System.Data.OleDb
Imports SBModel
Public Class frmGrdRptLoanApprovalList
    'TFS3565: Waqar Raza: Added this Variables to apply right based Cost Center
    Dim flgCostCenterRights As Boolean = False
    Private Sub frmGrdRptLoanApprovalList_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            'TFS3565: 21/Jun/2018: Waqar Raza: Added these to get the configuration
            'Start TFS3564
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            'End TFS3565
            'ApplySecurityRights()
            GetSecurityRights()
            Call DisplayApprovedLoanRequests()
            Call GetAllRecords()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ApplySecurityRights()
        'Try
        '    If Me.UltraTabControl1.SelectedTab.Index = 0 Then
        '        CtrlGrdBar1.Enabled = True
        '        CtrlGrdBar2.Enabled = False
        '        Me.btnRefresh.Visible = True
        '        Call grdLoandRequests_RowCheckStateChanged(Nothing, Nothing)
        '        Me.btnDelete.Visible = False
        '    Else
        '        CtrlGrdBar2.Enabled = True
        '        CtrlGrdBar1.Enabled = False
        '        Me.btnDelete.Visible = True
        '        Me.btnRefresh.Visible = False
        '        Me.btnApproveRequest.Visible = False
        '        Me.btnRejectRequest.Visible = False
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                btnDelete.Enabled = True
                CtrlGrdBar2.mGridPrint.Enabled = True
                CtrlGrdBar2.mGridExport.Enabled = True
                CtrlGrdBar2.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            btnDelete.Enabled = False
            CtrlGrdBar2.mGridPrint.Enabled = True
            CtrlGrdBar2.mGridExport.Enabled = True
            CtrlGrdBar2.mGridChooseFielder.Enabled = True
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "delete" Then
                    Me.btnDelete.Enabled = True
                ElseIf RightsDt.FormControlName = "Approved Print" Then
                    CtrlGrdBar2.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Approved Export" Then
                    CtrlGrdBar2.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Approved Field Chooser" Then
                    CtrlGrdBar2.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub DisplayApprovedLoanRequests()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "select tblDefEmployee.Employee_Name as Name,tblDefEmployee.Employee_Code as Code, tblDefCostCenter.Name AS [Cost Centre], AdvanceRequestTable.RequestID, " _
                & " AdvanceRequestTable.RequestNo, AdvanceRequestTable.RequestDate, AdvanceRequestTable.ApprovedDate, AdvanceRequestTable.EmployeeID, IsNull(AdvanceRequestTable.AdvanceAmount,0) as RequestedAmount, IsNull(AdvanceRequestTable.ApprovedAmount,0) ApprovedAmount , " _
                & " AdvanceRequestTable.Loan_Details as Description, AdvanceRequestTable.RequestStatus as Status, AdvanceRequestTable.UserId, AdvanceRequestTable.UserName as [Approved By] " _
                & " from AdvanceRequestTable inner Join tblDefEmployee on AdvanceRequestTable.EmployeeID = tblDefEmployee.Employee_ID LEFT JOIN tblDefCostCenter ON AdvanceRequestTable.CostCentre = tblDefCostCenter.CostCenterID" _
                & " where (RequestStatus = 'Approved' or RequestStatus ='Reject') " & IIf(flgCostCenterRights = True, "AND AdvanceRequestTable.CostCentre IN (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ")", "") & ""

            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            Me.grdApprovedHistory.DataSource = dt
            Me.grdApprovedHistory.RetrieveStructure()

            'Add Checkbox Column in grdApprovedHistory grid
            Me.grdApprovedHistory.RootTable.Columns.Add("Column1")
            Me.grdApprovedHistory.RootTable.Columns("Column1").UseHeaderSelector = True
            Me.grdApprovedHistory.RootTable.Columns("Column1").ActAsSelector = True

            'Hide Columns from grdApprovedHistory
            Me.grdApprovedHistory.RootTable.Columns("RequestID").Visible = False
            Me.grdApprovedHistory.RootTable.Columns("EmployeeID").Visible = False
            Me.grdApprovedHistory.RootTable.Columns("UserId").Visible = False

            'Alignments set and Value Format
            Me.grdApprovedHistory.RootTable.Columns("RequestedAmount").FormatString = "N" & DecimalPointInValue
            Me.grdApprovedHistory.RootTable.Columns("RequestedAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdApprovedHistory.RootTable.Columns("ApprovedAmount").FormatString = "N" & DecimalPointInValue
            Me.grdApprovedHistory.RootTable.Columns("ApprovedAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            'Me.grdApprovedHistory.RootTable.Columns("").CellStyle = BackColor

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetAllRecords()
        Try
            Dim strSQL As String = String.Empty

            strSQL = "select tblDefEmployee.Employee_Name as Name,tblDefEmployee.Employee_Code as Code, IsNull(tblDefEmployee.CostCentre, 0) As CostCentre, tblDefCostCenter.Name AS [Cost Center Name], AdvanceRequestTable.RequestID, " _
                & " AdvanceRequestTable.RequestNo, AdvanceRequestTable.RequestDate,AdvanceRequestTable.EmployeeID, IsNull(AdvanceRequestTable.AdvanceAmount,0) as RequestedAmount,IsNull(AdvanceRequestTable.ApprovedAmount,0) as ApprovedAmount,AdvanceRequestTable.Loan_Details as Description, IsNull(AdvanceRequestTable.Advance_TypeID, 0) AS AdvanceTypeId, AdvanceType.Title AS [Advance Type], ISNULL(AdvanceType.SalaryDeduct, 0) AS SalaryDeduct " _
                & " from AdvanceRequestTable inner Join tblDefEmployee on AdvanceRequestTable.EmployeeID = tblDefEmployee.Employee_ID LEFT JOIN tblDefCostCenter ON AdvanceRequestTable.CostCentre = tblDefCostCenter.CostCenterID LEFT OUTER JOIN tblDefAdvancesType AS AdvanceType ON  AdvanceRequestTable.Advance_TypeID = AdvanceType.Id " _
                & " Left Outer Join ApprovalHistory on ApprovalHistory.DocumentNo = AdvanceRequestTable.RequestNo where RequestStatus = 'Open' And  AdvanceRequestTable.RequestID not in (Select ReferenceId from ApprovalHistory where ApprovalHistory.DocumentNo = AdvanceRequestTable.RequestNo And AprovalProcessId <> 0 ) " & IIf(flgCostCenterRights = True, "AND AdvanceRequestTable.CostCentre IN (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ")", "") & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            Me.grdLoandRequests.DataSource = dt
            Me.grdLoandRequests.RetrieveStructure()
            Me.grdLoandRequests.RootTable.Columns("RequestID").Visible = False
            Me.grdLoandRequests.RootTable.Columns("EmployeeID").Visible = False
            Me.grdLoandRequests.RootTable.Columns("CostCentre").Visible = False


            Me.grdLoandRequests.RootTable.Columns("AdvanceTypeId").Visible = False
            Me.grdLoandRequests.RootTable.Columns("SalaryDeduct").Visible = False
            'Add Checkbox Column in grdLoandRequests grid
            Me.grdLoandRequests.RootTable.Columns.Add("Column1")
            Me.grdLoandRequests.RootTable.Columns("Column1").UseHeaderSelector = True
            Me.grdLoandRequests.RootTable.Columns("Column1").ActAsSelector = True

            'Add Button Approve Column in grdLoandRequests grid
            Dim ButtonApprove As New Janus.Windows.GridEX.GridEXColumn
            ButtonApprove.Key = "btnAprrove"
            ButtonApprove.ButtonText = "Approve"
            ButtonApprove.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ButtonApprove.ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            ButtonApprove.ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            ButtonApprove.Width = 100
            Me.grdLoandRequests.RootTable.Columns.Add(ButtonApprove)

            'Add Button Reject Column in grdLoandRequests grid
            Dim ButtonReject As New Janus.Windows.GridEX.GridEXColumn
            ButtonReject.Key = "btnReject"
            ButtonReject.ButtonText = "Reject"
            ButtonReject.TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ButtonReject.ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
            ButtonReject.ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
            ButtonReject.Width = 100
            Me.grdLoandRequests.RootTable.Columns.Add(ButtonReject)

            'Alignments set and Value Format
            Me.grdLoandRequests.RootTable.Columns("RequestedAmount").FormatString = "N" & DecimalPointInValue
            Me.grdLoandRequests.RootTable.Columns("RequestedAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdLoandRequests.RootTable.Columns("ApprovedAmount").FormatString = "N" & DecimalPointInValue
            Me.grdLoandRequests.RootTable.Columns("ApprovedAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            'Making Column un editable in grdLoandRequests
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdLoandRequests.RootTable.Columns
                If col.Index <> Me.grdLoandRequests.RootTable.Columns("ApprovedAmount").Index Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdLoandRequests_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdLoandRequests.ColumnButtonClick
        Try
            If e.Column.ButtonText = "Approve" Then

                Dim Id As Integer = 0I
                Id = Me.grdLoandRequests.CurrentRow.Cells("RequestID").Value.ToString
                If ApproveLoan(Id) = True Then
                    Call GetAllRecords()
                    Call DisplayApprovedLoanRequests()
                End If
            ElseIf e.Column.ButtonText = "Reject" Then
                Dim Id As Integer = 0I
                Id = Me.grdLoandRequests.CurrentRow.Cells("RequestID").Value.ToString
                If RejectLoan(Id) = True Then
                    Call GetAllRecords()
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Approve Loan Request
    Private Function ApproveLoan(ByVal ReqID As Integer) As Boolean
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            Dim strSQL As String = String.Empty

            'Update AdvanceRequestTable table with Approved Status for Loan Request
            strSQL = "Update AdvanceRequestTable set RequestStatus = 'Approved', ApprovedAmount = " & Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString) & ", UserId = " & LoginUserId & ", UserName = '" & LoginUserName & "', ApprovedDate = '" & GettingServerDate().ToString("dd/MMM/yyyy") & "' where RequestID = " & ReqID
            cmd.CommandText = String.Empty
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()


            'Get Receiveable Account Id of Employee
            ''
            If CBool(Me.grdLoandRequests.CurrentRow.Cells("SalaryDeduct").Value) = True Then
                Dim RecvId As Integer = 0I
                strSQL = String.Empty
                strSQL = "select IsNull(ReceiveableAccountId,0) as RecvId from tblDefEmployee where Employee_ID = " & Me.grdLoandRequests.CurrentRow.Cells("EmployeeID").Value.ToString
                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                RecvId = Convert.ToInt32(cmd.ExecuteScalar())
                'Get Salary Account Id of Employee
                Dim AccId As Integer = 0I
                strSQL = String.Empty
                strSQL = "select IsNull(EmpSalaryAccountId,0) as AccId from tblDefEmployee where Employee_ID = " & Me.grdLoandRequests.CurrentRow.Cells("EmployeeID").Value.ToString
                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                AccId = Convert.ToInt32(cmd.ExecuteScalar())

                'Saving Voucher Of Loan Request in Voucher Master Table
                Dim VoucherID As Integer = 0I
                strSQL = String.Empty
                strSQL = "Insert into tblVoucher(voucher_no,location_id,voucher_type_id,voucher_date,Employee_Id,Source, Remarks, Reference, post, Posted_UserName, UserName, Checked, CheckedByUser, Posting_date)" _
                    & " Values(N'" & Me.grdLoandRequests.CurrentRow.Cells("RequestNo").Value.ToString & "'," _
                    & " 1,1," _
                    & " N'" & Me.grdLoandRequests.CurrentRow.Cells("RequestDate").Value.ToString & "'," _
                    & " " & Val(Me.grdLoandRequests.CurrentRow.Cells("EmployeeID").Value.ToString) & "," _
                    & " N'" & Me.Name.ToString & "'," _
                    & " N'" & Me.grdLoandRequests.CurrentRow.Cells("Description").Value.ToString & "' , 'Employee loan of amount " & IIf(Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString) > 0, Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString), Val(Me.grdLoandRequests.CurrentRow.Cells("RequestedAmount").Value.ToString)) & " is approved against request number " & grdLoandRequests.CurrentRow.Cells("RequestNo").Value.ToString & " for " & grdLoandRequests.CurrentRow.Cells("Name").Value & " [" & grdLoandRequests.CurrentRow.Cells("Code").Value.ToString & "] by " & LoginUserName & "', " _
                    & " 1, '" & LoginUserName & "', '" & LoginUserName & "', 1, '" & LoginUserName & "', getdate() ) " _
                    & " SELECT @@IDENTITY"

                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                VoucherID = Convert.ToInt32(cmd.ExecuteScalar())

                'Deleting Voucher From Voucher Detail Table if Exist already
                strSQL = String.Empty
                strSQL = "Delete from tblVoucherDetail where voucher_id = " & VoucherID
                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()

                'Saving Voucher Details Of Loan Request in Voucher Detail Table (Debit aginst Receiveable Accont Id )
                strSQL = String.Empty
                strSQL = "Insert into tblVoucherDetail(Voucher_id,location_id,coa_detail_id,debit_amount, credit_amount, comments,EmpID, CostCenterID)" _
                    & " Values(" & VoucherID & ",1," _
                    & " " & RecvId & "," _
                    & " " & IIf(Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString) > 0, Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString), Val(Me.grdLoandRequests.CurrentRow.Cells("RequestedAmount").Value.ToString)) & ", " _
                    & " null, " _
                    & " 'Loan approved for " & grdLoandRequests.CurrentRow.Cells("Name").Value & " [" & grdLoandRequests.CurrentRow.Cells("Code").Value.ToString & "] (Details :" & grdLoandRequests.CurrentRow.Cells("Description").Value.ToString & ") '," _
                    & " " & Val(Me.grdLoandRequests.CurrentRow.Cells("EmployeeID").Value.ToString) & ", " & Val(Me.grdLoandRequests.CurrentRow.Cells("CostCentre").Value.ToString) & ")"
                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()

                'Saving Voucher Details Of Loan Request in Voucher Detail Table (Credit aginst Salary Accont Id )
                strSQL = String.Empty
                strSQL = "Insert into tblVoucherDetail(Voucher_id,location_id,coa_detail_id,debit_amount, credit_amount, comments,EmpID, CostCenterID)" _
                    & " Values(" & VoucherID & ",1," _
                    & " " & AccId & "," _
                    & " null, " _
                    & " " & IIf(Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString) > 0, Val(Me.grdLoandRequests.CurrentRow.Cells("ApprovedAmount").Value.ToString), Val(Me.grdLoandRequests.CurrentRow.Cells("RequestedAmount").Value.ToString)) & "," _
                    & " 'Loan approved for " & grdLoandRequests.CurrentRow.Cells("Name").Value & " [" & grdLoandRequests.CurrentRow.Cells("Code").Value.ToString & "] (Details :" & grdLoandRequests.CurrentRow.Cells("Description").Value.ToString & ") '," _
                    & " " & Val(Me.grdLoandRequests.CurrentRow.Cells("EmployeeID").Value.ToString) & ", " & Val(Me.grdLoandRequests.CurrentRow.Cells("CostCentre").Value.ToString) & ")"
                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()
            End If
            objTrans.Commit()
            Return True

        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    'Reject Loan Request
    Private Function RejectLoan(ByVal ReqID As Integer) As Boolean
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            Dim strSQL As String = String.Empty
            strSQL = "Update AdvanceRequestTable set RequestStatus = 'Reject'  where RequestID = " & ReqID
            cmd.CommandText = String.Empty
            cmd.CommandText = strSQL

            cmd.ExecuteNonQuery()

            objTrans.Commit()
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Private Sub btnApproveRequest_Click(sender As Object, e As EventArgs) Handles btnApproveRequest.Click
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            Dim strSQL As String = String.Empty


            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdLoandRequests.GetCheckedRows
                'Update AdvanceRequestTable table with Approved Status for Loan Request
                strSQL = "Update AdvanceRequestTable set RequestStatus = 'Approved', ApprovedAmount = " & Val(r.Cells("ApprovedAmount").Value.ToString) & ", UserId = " & LoginUserId & ", UserName = '" & LoginUserName & "', ApprovedDate = '" & GettingServerDate().ToString("dd/MMM/yyyy") & "' where RequestID = " & Val(r.Cells("RequestID").Value.ToString)
                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()


                'Get Receiveable Account Id of Employee
                If CBool(r.Cells("SalaryDeduct").Value) = True Then
                    Dim RecvId As Integer = 0I
                    strSQL = String.Empty
                    strSQL = "select IsNull(ReceiveableAccountId,0) as RecvId from tblDefEmployee where Employee_ID = " & Val(r.Cells("EmployeeID").Value.ToString)
                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    RecvId = Convert.ToInt32(cmd.ExecuteScalar())

                    'Get Salary Account Id of Employee
                    Dim AccId As Integer = 0I
                    strSQL = String.Empty
                    strSQL = "select IsNull(EmpSalaryAccountId,0) as AccId from tblDefEmployee where Employee_ID = " & Val(r.Cells("EmployeeID").Value.ToString)
                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    AccId = Convert.ToInt32(cmd.ExecuteScalar())

                    'Saving Voucher Of Loan Request in Voucher Master Table
                    Dim VoucherID As Integer = 0I
                    strSQL = String.Empty
                    strSQL = "Insert into tblVoucher(voucher_no,location_id,voucher_type_id,voucher_date,Employee_Id,Source, Remarks, Reference, post, Posted_UserName, UserName, Checked, CheckedByUser, Posting_date)" _
                        & " Values(N'" & r.Cells("RequestNo").Value.ToString & "'," _
                        & " 1,1," _
                        & " N'" & r.Cells("RequestDate").Value.ToString & "'," _
                        & " " & Val(r.Cells("EmployeeID").Value.ToString) & "," _
                        & " N'" & Me.Name.ToString & "'," _
                        & " N'" & r.Cells("Description").Value.ToString & "', 'Employee loan of amount " & IIf(Val(r.Cells("ApprovedAmount").Value.ToString) > 0, Val(r.Cells("ApprovedAmount").Value.ToString), Val(r.Cells("RequestedAmount").Value.ToString)) & " is approved against request number " & r.Cells("RequestNo").Value.ToString & " for " & r.Cells("Name").Value & " [" & r.Cells("Code").Value.ToString & "] by " & LoginUserName & "' , " _
                        & " 1, '" & LoginUserName & "', '" & LoginUserName & "', 1, '" & LoginUserName & "', getdate() ) " _
                        & " SELECT @@IDENTITY"

                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    VoucherID = Convert.ToInt32(cmd.ExecuteScalar())

                    'Deleting Voucher From Voucher Detail Table if Exist already
                    strSQL = String.Empty
                    strSQL = "Delete from tblVoucherDetail where voucher_id = " & VoucherID
                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()

                    'Saving Voucher Details Of Loan Request in Voucher Detail Table (Debit aginst Receiveable Accont Id )
                    strSQL = String.Empty
                    strSQL = "Insert into tblVoucherDetail(Voucher_id,location_id,coa_detail_id,debit_amount, credit_amount, comments,EmpID, CostCenterID)" _
                        & " Values(" & VoucherID & ",1," _
                        & " " & RecvId & "," _
                        & " " & IIf(Val(r.Cells("ApprovedAmount").Value.ToString) > 0, Val(r.Cells("ApprovedAmount").Value.ToString), Val(r.Cells("RequestedAmount").Value.ToString)) & ", " _
                        & " null, " _
                        & " 'Loan approved for " & r.Cells("Name").Value & " [" & r.Cells("Code").Value.ToString & "] (Details :" & r.Cells("Description").Value.ToString & ") '," _
                        & " " & Val(r.Cells("EmployeeID").Value.ToString) & ", " & Val(r.Cells("CostCentre").Value.ToString) & ")"
                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()

                    'Saving Voucher Details Of Loan Request in Voucher Detail Table (Credit aginst Salary Accont Id )
                    strSQL = String.Empty
                    strSQL = "Insert into tblVoucherDetail(Voucher_id,location_id,coa_detail_id,debit_amount, credit_amount, comments,EmpID, CostCenterID)" _
                        & " Values(" & VoucherID & ",1," _
                        & " " & AccId & "," _
                        & " null, " _
                        & " " & IIf(Val(r.Cells("ApprovedAmount").Value.ToString) > 0, Val(r.Cells("ApprovedAmount").Value.ToString), Val(r.Cells("RequestedAmount").Value.ToString)) & "," _
                        & " 'Loan approved for " & r.Cells("Name").Value & " [" & r.Cells("Code").Value.ToString & "] (Details :" & r.Cells("Description").Value.ToString & ") '," _
                        & " " & Val(r.Cells("EmployeeID").Value.ToString) & ", " & Val(r.Cells("CostCentre").Value.ToString) & ")"
                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()
                End If
            Next

            Me.btnApproveRequest.Visible = False
            Me.btnRejectRequest.Visible = False


            objTrans.Commit()

            Call GetAllRecords()
            Call DisplayApprovedLoanRequests()

        Catch ex As Exception
            objTrans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
        End Try
    End Sub

    Private Sub btnRejectRequest_Click(sender As Object, e As EventArgs) Handles btnRejectRequest.Click
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            Dim strSQL As String = String.Empty

            'Checked rows int grdLoandRequests, Reject all loan requests
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdLoandRequests.GetCheckedRows
                strSQL = "Update AdvanceRequestTable set RequestStatus = 'Reject'  where RequestID = " & Val(r.Cells("RequestID").Value.ToString)
                cmd.CommandText = String.Empty
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()
            Next

            Me.btnApproveRequest.Visible = False
            Me.btnRejectRequest.Visible = False


            objTrans.Commit()
            Call GetAllRecords()
            Call DisplayApprovedLoanRequests()
        Catch ex As Exception
            objTrans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            'TFS3565: 21/Jun/2018: Waqar Raza: Added these to get the configuration
            'Start TFS3564
            If Not getConfigValueByType("RightBasedCostCenters").ToString = "Error" Then
                flgCostCenterRights = getConfigValueByType("RightBasedCostCenters")
            End If
            'End TFS3565
            Call DisplayApprovedLoanRequests()
            Call GetAllRecords()
            grdLoandRequests_RowCheckStateChanged(Nothing, Nothing)   'hide Approve and Reject buttons on Refresh Grid

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdLoandRequests_RowCheckStateChanged(sender As Object, e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles grdLoandRequests.RowCheckStateChanged
        Try
            Dim CheckedRows As Integer = 0I
            CheckedRows = Convert.ToInt32(Me.grdLoandRequests.GetCheckedRows.Length)    'Getting count of checked rows from grdLoandRequests 

            If Me.grdLoandRequests.RowCount = 0 Then
                Me.btnApproveRequest.Visible = False
                Me.btnApproveRequest.Text = "Approve"
                Me.btnRejectRequest.Visible = False
                Me.btnRejectRequest.Text = "Reject"
                'When all rows checked
            ElseIf Me.grdLoandRequests.RowCount = CheckedRows Then
                Me.btnApproveRequest.Visible = True
                Me.btnApproveRequest.Text = "Approve(All)"
                Me.btnRejectRequest.Visible = True
                Me.btnRejectRequest.Text = "Reject(All)"
                'when one row checked
            ElseIf CheckedRows > 0 Then
                Me.btnApproveRequest.Visible = True
                Me.btnApproveRequest.Text = "Approve(" & CheckedRows & ")"
                Me.btnRejectRequest.Visible = True
                Me.btnRejectRequest.Text = "Reject(" & CheckedRows & ")"
            Else
                Me.btnApproveRequest.Visible = False
                Me.btnRejectRequest.Visible = False
                Me.btnApproveRequest.Text = "Approve"
                Me.btnRejectRequest.Text = "Reject"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If Not grdApprovedHistory.GetCheckedRows.Length > 0 Then
            msg_Information("No record is selected, please select atleast one record")
            Exit Sub
        End If

        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = objTrans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text
            Dim strSQL As String = String.Empty

            'Delete Approved Requests also Voucher Detele if Loan Request Approved by mistake
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdApprovedHistory.GetCheckedRows
                If row.Cells("Status").Value.ToString = "Reject" Then   'Just Open the status of Reject Request
                    strSQL = "Update AdvanceRequestTable set RequestStatus = 'Open'  where RequestID = " & row.Cells("RequestID").Value.ToString
                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()
                Else
                    strSQL = String.Empty
                    strSQL = "Update AdvanceRequestTable set RequestStatus = 'Open'  where RequestID = " & row.Cells("RequestID").Value.ToString
                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()

                    'Getting Voucher Id against Voucher No
                    Dim VoucherId As Integer = 0I
                    strSQL = "Select Voucher_id,Voucher_No from tblVoucher where Voucher_No = '" & row.Cells("RequestNo").Value.ToString & "'"
                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    VoucherId = Convert.ToInt32(cmd.ExecuteScalar())

                    'Delete Detail Vouchers
                    strSQL = String.Empty
                    strSQL = "Delete from tblVoucherDetail where Voucher_Id = " & VoucherId
                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()

                    'Delete Master Voucher
                    strSQL = String.Empty
                    strSQL = "Delete from tblVoucher where Voucher_No = '" & row.Cells("RequestNo").Value.ToString & "'"
                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()

                End If
            Next

            objTrans.Commit()
            Call DisplayApprovedLoanRequests()
            GetAllRecords()
        Catch ex As Exception
            objTrans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            Call ApplySecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdApprovedHistory_RowCheckStateChanged(sender As Object, e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles grdApprovedHistory.RowCheckStateChanged
        Try
            Dim CheckedRows As Integer = 0I
            CheckedRows = Convert.ToInt32(Me.grdApprovedHistory.GetCheckedRows.Length)    'Getting count of checked rows from grdLoandRequests 

            If CheckedRows = 0 Then
                Me.btnDelete.Text = "Delete"
            ElseIf Me.grdApprovedHistory.RowCount = CheckedRows Then
                Me.btnDelete.Text = "Delete (All)"
                'when one row checked
            ElseIf CheckedRows > 0 Then
                Me.btnDelete.Text = "Delete(" & CheckedRows & ")"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdLoandRequests.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdLoandRequests.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdLoandRequests.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "EmployeeAdvanceRequest"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
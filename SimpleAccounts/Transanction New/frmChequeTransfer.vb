''TASK TFS1458 Muhammad Ameen on 13-09-2017: Addition of voucher print and direct voucher print option


Imports System.Windows.Forms
Imports System.Data
Imports System.Data.OleDb
Imports Neodynamic.SDK.Barcode
Public Class frmChequeTransfer

    Dim strChqueList As String = String.Empty

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            If Me.grdChqs.RowCount = 0 Then Exit Sub
            If Not Me.IsValidate() Then Exit Sub
            Me.Hide()
            Me.Close()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub DisplayRecord()
        Try

            'Dim str As String

            'str = " SELECT     dbo.tblVoucher.voucher_id, dbo.tblVoucher.voucher_no, dbo.tblVoucher.voucher_date, dbo.vwCOADetail.detail_title ,tblVoucher.Cheque_no , tblvoucher.cheque_date, tblvoucher.coa_detail_id" _
            '      & "   FROM         dbo.tblVoucher LEFT OUTER JOIN " _
            '      & "     dbo.vwCOADetail ON dbo.tblVoucher.coa_detail_id = dbo.vwCOADetail.coa_detail_id " _
            '      & " Where tblVoucher.cheque_paid = 0 and voucher_type_id  = 5 " _
            '      & " order by voucher_id desc"

            Dim Str As String = " SELECT  " & IIf(Val(Me.txtTopDetailRecords.Text) > 0, " top " & Val(Me.txtTopDetailRecords.Text), " ") & "   dbo.tblVoucher.voucher_id, tblVoucherDetail.voucher_detail_id, dbo.tblVoucher.voucher_no, dbo.tblVoucher.voucher_date, tblVoucherDetail.Cheque_No, dbo.vwCOADetail.detail_title, tblvoucherdetail.cheque_date, tblVoucherDetail.Credit_Amount, tblVoucherDetail.Comments, tblVoucherDetail.coa_detail_id" _
              & "   FROM         tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id INNER JOIN vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id " _
              & " Where tblVoucherDetail.Cheque_No <> '' and  tblVoucherDetail.credit_amount > 0 and isnull(tblVoucherDetail.cheque_paid ,0) = 0 and voucher_type_id  = 5 " & IIf(Me.cmbCustomer.ActiveRow.Cells(0).Value > 0, " and tblVoucher.coa_detail_id = " & Me.cmbCustomer.ActiveRow.Cells(0).Value & "", "") & " " & _
              IIf(Me.dtpCustomerFrom.Checked = True, "and tblvoucher.voucher_date >= '" & Me.dtpCustomerFrom.Value.Date & " 00:00:00:000'", "") & IIf(dtpCustomerTo.Checked = True, "and tblvoucher.voucher_date <= '" & Me.dtpCustomerTo.Value.Date & " 23:59:59:000' ", " ")

            If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                Str = Str & " and tblvoucher.coa_detail_id is not null"
            End If

            If Me.cmbBanksSearch.ActiveRow.Cells(0).Value > 0 Then
                Str = Str & " and tblVoucher.Voucher_id in (select distinct voucher_id from tblVoucherDetail where debit_amount > 0 and isnull(tblVoucherDetail.cheque_paid ,0) = 0 and coa_detail_id =" & Me.cmbBanksSearch.ActiveRow.Cells(0).Value & " ) "
            End If

            Str = Str & " order by tblVoucher.voucher_id desc "


            FillGridEx(grdChqs, Str, True)
            grdChqs.RootTable.Columns("voucher_detail_id").Visible = False
            grdChqs.RootTable.Columns("voucher_id").Visible = False
            grdChqs.RootTable.Columns("coa_detail_id").Visible = False
            grdChqs.RootTable.Columns("voucher_no").Caption = "Voucher No"
            grdChqs.RootTable.Columns("voucher_date").Caption = "Voucher Date"
            grdChqs.RootTable.Columns("voucher_date").FormatString = "dd-MMM-yyyy"
            grdChqs.RootTable.Columns("detail_title").Caption = "Received From"
            grdChqs.RootTable.Columns("cheque_no").Caption = "Cheque No"
            'grdChqs.RootTable.Columns("cheque_no").EditType = Janus.Windows.GridEX.EditType.CalendarCombo
            'grdChqs.RootTable.Columns("cheque_no").FormatString = "dd-MMM-yyyy"
            grdChqs.RootTable.Columns("cheque_date").Caption = "Cheque Date"
            grdChqs.RootTable.Columns("cheque_date").FormatString = "dd-MMM-yyyy"
            grdChqs.RootTable.Columns("credit_amount").Caption = "Amount"
            grdChqs.RootTable.Columns("credit_amount").FormatString = "N"
            grdChqs.RootTable.Columns("credit_amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grdChqs.RootTable.Columns("credit_amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Dim SCol As New Janus.Windows.GridEX.GridEXColumn("Selection")
            SCol.ActAsSelector = True
            grdChqs.RootTable.Columns.Insert(0, SCol)


            'grdChqs.RowHeadersVisible = False

            'For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdChqs.RootTable.Columns
            '    'col.ReadOnly = True
            '    'col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            '    col.AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DiaplayedCells
            'Next

            'Me.grdSaved.Columns(0).ReadOnly = False
            grdChqs.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar2_Load(Nothing, Nothing)
            CtrlGrdBar3_Load(Nothing, Nothing)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmChequeTransfer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(btnNew, Nothing)
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub frmChequeTransfer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.FillCombo()
            Me.RefreshControls()
            Me.DisplayRecord()
            Me.DisplayMasterRecords()
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default


            'TFS3360_Aashir_Added select/contain filters on account feilds in all transaction screens
            UltraDropDownSearching(cmbAccount, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbCostCenter, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub frmPrintInvoice_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Ali Faisal : TFS1487 : Call security rights on form load
        GetSecurityRights()
        'Ali Faisal : TFS1487 : End
    End Sub

    Private Sub FillCombo()
        Try
            ''filling vendor
            Dim Str As String = "SELECT     TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0)   " & IIf(Me.rdbAll.Checked, "", " and account_type ='Bank'") & " order by detail_title"
            FillUltraDropDown(cmbAccount, Str)
            Me.cmbAccount.Rows(0).Activate()
            ''filling  Customer
            Str = "SELECT     TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0)   " & IIf(Me.optAllCustomer.Checked, "", " and account_type ='Customer'") & " order by detail_title"
            FillUltraDropDown(Me.cmbCustomer, Str)
            Me.cmbCustomer.Rows(0).Activate()
            ''filling  search vendor
            Str = "SELECT     TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0)   " & IIf(Me.optAllVendor.Checked, "", " and account_type ='Bank'") & " order by detail_title"
            FillUltraDropDown(Me.cmbSearchVendor, Str)
            Me.cmbSearchVendor.Rows(0).Activate()
            ''filling  search banks
            Str = "SELECT     TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0)  and account_type ='Bank' order by detail_title"
            FillUltraDropDown(Me.cmbBanksSearch, Str)
            Me.cmbBanksSearch.Rows(0).Activate()
            FillUltraDropDown(Me.cmbCostCenter, "Select CostCenterId, Name From tblDefCostCenter WHERE Active=1")
            Me.cmbCostCenter.Rows(0).Activate()
            Me.cmbCostCenter.DisplayLayout.Bands(0).Columns("CostCenterId").Hidden = True
            Me.cmbCostCenter.DisplayLayout.Bands(0).Columns("Name").Width = 300

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub RefreshControls()
        Try

            Me.txtJvNo.Text = GetVoucherNo() 'GetNextDocNo("JV", 6, "tblVoucher", "Voucher_No")
            Me.txtVoucherNo.Text = String.Empty
            Me.cmbAccount.Rows(0).Activate()
            Me.cmbCostCenter.Rows(0).Activate()
            Me.txtCustomer.Text = String.Empty
            Me.dtpVoucherDate.Value = Date.Today
            Me.txtVoucherID.Text = String.Empty
            Me.txtParentVoucherID.Text = String.Empty
            Me.cmbAccount.Focus()
            Me.dtpVoucherDate.Enabled = True
            Me.strChqueList = String.Empty
            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar2_Load(Nothing, Nothing)
            CtrlGrdBar3_Load(Nothing, Nothing)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function GetVoucherNo() As String
        Try
            'If Me.cmbVoucherType.SelectedIndex > 0 Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("JV" + "-" + Microsoft.VisualBasic.Right(Me.dtpVoucherDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
            Else
                Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
                Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
                If Not dr Is Nothing Then
                    If dr("config_Value") = "Monthly" Then
                        Return GetNextDocNo("JV" & "-" & Format(Me.dtpVoucherDate.Value, "yy") & Me.dtpVoucherDate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
                    Else
                        Return GetNextDocNo("JV", 6, "tblVoucher", "voucher_no")
                    End If
                Else
                    Return GetNextDocNo("JV", 6, "tblVoucher", "voucher_no")
                End If
            End If
            'Else
            'Return ""
            'End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Private Sub btnSearh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            Me.DisplayRecord()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub
    Private Function IsValidate() As Boolean
        Try
            Dim intIsChecked As Integer = 0
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdChqs.GetRows
                If r.Cells(0).Value = True Then
                    intIsChecked = 1
                    Exit For
                End If
            Next
            If intIsChecked = 0 Then
                msg_Error("please select record from grid")
                Me.grdChqs.Focus()
                Return False
            End If
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                'Ali Faisal : TFS1487 : Apply rights on All accounts button
                Me.rdbAll.Enabled = True
                'Ali Faisal : TFS1487 : End
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmChequeTransfer)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False
                'Ali Faisal : TFS1487 : Apply rights on All accounts button
                Me.rdbAll.Enabled = False
                'Ali Faisal : TFS1487 : End
                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                        'Ali Faisal : TFS1487 : Apply rights on All accounts button
                    ElseIf Rights.Item(i).FormControlName = "All Accounts" Then
                        Me.rdbAll.Enabled = True
                        'Ali Faisal : TFS1487 : End
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub optByCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optByCode.CheckedChanged
        If Me.cmbAccount.Rows.Count = 0 Then Exit Sub
        Try
            Dim Str As String = "SELECT     TOP 100 PERCENT coa_detail_id, detail_code, detail_title,  account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0)   " & IIf(Me.rdbAll.Checked, "", " and account_type ='Bank'") & " order by detail_title"
            FillUltraDropDown(cmbAccount, Str)
            Me.cmbAccount.Rows(0).Activate()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub optByName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optByName.CheckedChanged
        Try
            If Me.cmbAccount.Rows.Count = 0 Then Exit Sub
            Dim Str As String = "SELECT     TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0)   " & IIf(Me.rdbAll.Checked, "", " and account_type ='Bank'") & " order by detail_title"
            FillUltraDropDown(cmbAccount, Str)
            Me.cmbAccount.Rows(0).Activate()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdbVendor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbVendor.CheckedChanged
        Try
            If Me.cmbAccount.Rows.Count = 0 Then Exit Sub
            If Me.optByCode.Checked Then
                Dim Str As String = "SELECT     TOP 100 PERCENT coa_detail_id, detail_code, detail_title,  account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0)   " & IIf(Me.rdbAll.Checked, "", " and account_type ='Bank'") & " order by detail_title"
                FillUltraDropDown(cmbAccount, Str)
                Me.cmbAccount.Rows(0).Activate()
            Else
                Dim Str As String = "SELECT     TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0)   " & IIf(Me.rdbAll.Checked, "", " and account_type ='Bank'") & " order by detail_title"
                FillUltraDropDown(cmbAccount, Str)
                Me.cmbAccount.Rows(0).Activate()
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub optAllCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAllCustomer.CheckedChanged
        Try

            If Me.cmbAccount.Rows.Count = 0 Then Exit Sub
            ''filling  Customer
            Dim Str As String = "SELECT     TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0)   " & IIf(Me.optAllCustomer.Checked, "", " and account_type ='Customer'") & " order by detail_title"
            FillUltraDropDown(Me.cmbCustomer, Str)
            Me.cmbCustomer.Rows(0).Activate()


        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub optCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optCustomer.CheckedChanged
        Try
            If Me.cmbAccount.Rows.Count = 0 Then Exit Sub
            ''filling  Customer
            Dim Str As String = "SELECT     TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0)   " & IIf(Me.optAllCustomer.Checked, "", " and account_type ='Customer'") & " order by detail_title"
            FillUltraDropDown(Me.cmbCustomer, Str)
            Me.cmbCustomer.Rows(0).Activate()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub optAllVendor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAllVendor.CheckedChanged

        Try
            If Me.cmbAccount.Rows.Count = 0 Then Exit Sub
            ''filling  search vendor
            Dim Str As String = "SELECT     TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0)   " & IIf(Me.optAllVendor.Checked, "", " and account_type ='Bank'") & " order by detail_title"
            FillUltraDropDown(Me.cmbSearchVendor, Str)
            Me.cmbSearchVendor.Rows(0).Activate()


        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub
    Private Sub optVendor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optVendor.CheckedChanged

        If Me.cmbAccount.Rows.Count = 0 Then Exit Sub
        ''filling  search vendor
        Dim Str As String = "SELECT     TOP 100 PERCENT coa_detail_id, detail_title, detail_code, account_type, sub_sub_title, sub_title, main_title, main_type,cityname as City FROM         dbo.vwCOADetail WHERE     (coa_detail_id > 0)   " & IIf(Me.optAllVendor.Checked, "", " and account_type ='Bank'") & " order by detail_title"
        FillUltraDropDown(Me.cmbSearchVendor, Str)
        Me.cmbSearchVendor.Rows(0).Activate()
    End Sub
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Me.Cursor = Cursors.WaitCursor
        Me.RefreshControls()
        Me.Cursor = Cursors.Default
    End Sub
    Private Function Save() As Boolean

        Dim objCommand As New OleDbCommand
        If Con.State = ConnectionState.Open Then Con.Close()
        Con.Open()
        objCommand.Connection = Con
        objCommand.CommandText = "SELECT     coa_detail_id, comments, debit_amount" _
                                 & " FROM tblVoucherDetail" _
                                 & " WHERE voucher_id = " & Me.grdChqs.CurrentRow.Cells("Voucher_id").Value & " and debit_amount > 0 and tblVoucherDetail.coa_detail_id in (select coa_detail_id from vwCOADetail where account_type='Bank') "
        Dim da As New OleDbDataAdapter(objCommand)
        Dim dt As New DataTable
        da.Fill(dt)

        Dim trans As OleDbTransaction = Con.BeginTransaction


        Try
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans

            'objCommand.CommandText = "update tblvoucher set cheque_paid = 1 where voucher_id = " & Me.grdChqs.CurrentRow.Cells(0).Value & ""
            'objCommand.ExecuteNonQuery()

            objCommand.CommandText = "Insert into tblVoucher (location_id,finiancial_year_id,voucher_type_id,voucher_no,voucher_date, cheque_no, cheque_date,source,UserName, paid_to, cheque_paid, coa_detail_id) values( " _
                                    & " 1,1, 1 ,'" & txtJvNo.Text & "', '" & Me.dtpVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "','" & strChqueList & "', '" & Me.grdChqs.CurrentRow.Cells("cheque_date").Value.ToString.Trim & " ', '" & Me.Name & "','" & LoginUserName & "', '" & Me.txtCustomer.Text & "' ,  1, " & Me.cmbAccount.ActiveRow.Cells(0).Value & ")"

            objCommand.ExecuteNonQuery()

            If dt.Rows.Count > 0 Then
                For Each r As Janus.Windows.GridEX.GridEXRow In grdChqs.GetCheckedRows


                    objCommand.CommandText = "update tblvoucherDetail set cheque_paid = 1 where voucher_Detail_id = " & r.Cells("voucher_Detail_id").Value & ""
                    objCommand.ExecuteNonQuery()

                    ''inserting debit amount
                    objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments, cheque_no, cheque_date, debit_amount, credit_amount, Parent_Id, CostCenterID) values( " _
                                                            & " ident_current('tblVoucher'), 1, " & Me.cmbAccount.ActiveRow.Cells(0).Value & ",'" & r.Cells("comments").Value & ", Cheque transferred to " & Me.cmbAccount.Text & " from voucher no [" & r.Cells("voucher_no").Value.ToString & "] of account [" & r.Cells("detail_title").Value & "] ', '" & r.Cells("cheque_no").Value & "', '" & r.Cells("cheque_date").Value.ToString.Trim & " '," & r.Cells("credit_amount").Value & ", " _
                                                            & " 0, " & r.Cells("voucher_Detail_id").Value & ", " & Me.cmbCostCenter.Value & " ) "

                    objCommand.ExecuteNonQuery()


                    ''inserting credit amount
                    objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments, cheque_no, cheque_date, debit_amount,credit_amount, parent_id, CostCenterID ) values( " _
                                                            & " ident_current('tblVoucher'), 1, " & Val(dt.Rows(0).Item("coa_detail_id").ToString) & ",'" & r.Cells("comments").Value.ToString & ", Cheque transferred to " & Me.cmbAccount.Text & " from voucher no [" & r.Cells("voucher_no").Value.ToString & "] of account [" & r.Cells("detail_title").Value & "] ', '" & r.Cells("cheque_no").Value & "', '" & r.Cells("cheque_date").Value.ToString.Trim & " ', 0 , " _
                                                            & " " & r.Cells("credit_amount").Value.ToString & ", " & r.Cells("voucher_Detail_id").Value & ", " & Me.cmbCostCenter.Value & " ) "

                    objCommand.ExecuteNonQuery()


                Next
            End If


            trans.Commit()
            Save = True

            SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.txtJvNo.Text.Trim, True)

        Catch ex As Exception
            trans.Rollback()
            Save = False
        Finally
            'Con = Nothing
            objCommand = Nothing

        End Try

    End Function
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.Cursor = Cursors.WaitCursor
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    ShowErrorMessage("Previous date work not allowed") : Exit Sub
                End If
            End If

            If Me.dtpVoucherDate.Value <= Convert.ToDateTime((getConfigValueByType("EndOfDate").ToString)) Then
                ShowErrorMessage("You can not change this becuase financial year is closed")
                Me.dtpVoucherDate.Focus()
                Exit Sub
            End If

            If FormValidate() Then
                If Me.btnSave.Text = "&Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() Then
                        'ShowErrorMessage("Record added successfully")

                        ''TASK TFS1458
                        Dim Printing As Boolean
                        'Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        Printing = False

                        Dim DirectVoucherPrinting As Boolean
                        DirectVoucherPrinting = Convert.ToBoolean(getConfigValueByType("DirectVoucherPrinting").ToString)
                        If Printing = True Or DirectVoucherPrinting = True Then
                            If msg_Confirm("Do you want to print", Printing, DirectVoucherPrinting) = True Then
                                Dim VoucherPrint As Boolean = frmMessages.DirectVoucherPrinting
                                If VoucherPrint = True Then
                                    GetVoucherPrint(Me.txtJvNo.Text, Me.Name, , , True)
                                End If
                            End If
                        End If
                        ''END TASK TFS1458

                        RefreshControls()
                        DisplayRecord()
                        Me.DisplayMasterRecords()
                    Else
                        ShowErrorMessage(" Error! Record not added")

                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update_Record() Then
                        'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
                        'ShowErrorMessage("Record updated successfully")


                        ''TASK TFS1458
                        Dim Printing As Boolean
                        'Printing = Convert.ToBoolean(getConfigValueByType("Print").ToString)
                        Printing = False

                        Dim DirectVoucherPrinting As Boolean
                        DirectVoucherPrinting = Convert.ToBoolean(getConfigValueByType("DirectVoucherPrinting").ToString)
                        If Printing = True Or DirectVoucherPrinting = True Then
                            If msg_Confirm("Do you want to print", Printing, DirectVoucherPrinting) = True Then
                                Dim VoucherPrint As Boolean = frmMessages.DirectVoucherPrinting
                                If VoucherPrint = True Then
                                    GetVoucherPrint(Me.txtJvNo.Text, Me.Name, , , True)
                                End If
                            End If
                        End If
                        ''END TASK TFS1458
                        RefreshControls()
                        DisplayRecord()
                        Me.DisplayMasterRecords()
                    Else
                        ShowErrorMessage("Error! Record not updated")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function FormValidate() As Boolean
        If Me.cmbAccount.ActiveRow.Cells(0).Value = 0 Then
            ShowErrorMessage("Please enter Voucher Type")
            cmbAccount.Focus() : FormValidate = False : Exit Function
        End If

        Return True

    End Function
    Private Function Update_Record() As Boolean

        Dim objCommand As New OleDbCommand
        If Con.State = ConnectionState.Open Then Con.Close()
        Con.Open()
        objCommand.Connection = Con
        objCommand.CommandText = "SELECT     coa_detail_id, comments, debit_amount" _
                                 & " FROM tblVoucherDetail" _
                                 & " WHERE voucher_id = " & Me.txtParentVoucherID.Text & " and debit_amount > 0"
        Dim da As New OleDbDataAdapter(objCommand)
        Dim dt As New DataTable
        da.Fill(dt)

        Dim trans As OleDbTransaction = Con.BeginTransaction


        Try
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans


            objCommand.CommandText = "Update tblVoucher set voucher_date='" & dtpVoucherDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                  & " UserName =  '" & LoginUserName & "', paid_to = '" & Me.txtCustomer.Text & "',  coa_detail_id = " & Me.cmbAccount.ActiveRow.Cells(0).Value & "  Where Voucher_ID= " & txtVoucherID.Text & " "

            objCommand.ExecuteNonQuery()

            objCommand.CommandText = "Delete from tblVoucherDetail where voucher_id = " & txtVoucherID.Text
            objCommand.ExecuteNonQuery()

            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows

                    ''inserting debit amount
                    objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments,debit_amount,credit_amount, CostCenterID) values( " _
                                                            & " ident_current('tblVoucher'), 1, " & Me.cmbAccount.ActiveRow.Cells(0).Value & ",'" & r.Item("comments") & "'," & r.Item("debit_amount") & ", " _
                                                            & " 0, " & Me.cmbCostCenter.Value & " ) "

                    objCommand.ExecuteNonQuery()


                    ''inserting credit amount
                    objCommand.CommandText = "Insert into tblVoucherDetail (voucher_id, location_id,coa_detail_id, comments,debit_amount,credit_amount, CostCenterID) values( " _
                                                            & " ident_current('tblVoucher'), 1, " & r.Item("coa_detail_id") & ",'" & r.Item("comments") & "', 0 , " _
                                                            & " " & r.Item("debit_amount") & ", " & Me.cmbCostCenter.Value & " ) "

                    objCommand.ExecuteNonQuery()


                Next
            End If




            trans.Commit()
            Update_Record = True

            SaveActivityLog("Accounts", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, txtVoucherNo.Text.Trim, True)

        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
        Finally
            'Con = Nothing
            objCommand = Nothing
        End Try

    End Function


    Private Sub DisplayMasterRecords()
        Try

            'Dim str As String = "SELECT     tblVoucher.voucher_id, tblVoucher.voucher_no, tblVoucher.voucher_date, tblVoucher.paid_to, tblVoucher.coa_detail_id, tblVoucher.cheque_no, " _
            '                  & "   tblVoucher.cheque_date, vwCOADetail.detail_title, Parent.voucher_id as ParentVoucherID " _
            '                  & " FROM         tblVoucher LEFT OUTER JOIN" _
            '                  & "  vwCOADetail ON tblVoucher.coa_detail_id = vwCOADetail.coa_detail_id inner join tblvoucher as Parent on Parent.cheque_no = tblvoucher.cheque_no " _
            '                  & " WHERE     (tblVoucher.source = 'frmChequeTransfer') AND (tblVoucher.voucher_date IS NOT NULL) and parent.voucher_type_id = 5 order by tblvoucher.voucher_id desc"

            'FillGridEx(Me.grdSaved, str, True)


            'Me.grdSaved.RootTable.Columns(0).Visible = False
            'Me.grdSaved.RootTable.Columns("coa_detail_id").Visible = False
            'Me.grdSaved.RootTable.Columns("ParentVoucherID").Visible = False

            'Me.grdSaved.RootTable.Columns("voucher_no").Caption = "Voucher No"
            'Me.grdSaved.RootTable.Columns("voucher_date").Caption = "Voucher Date"
            'Me.grdSaved.RootTable.Columns("paid_to").Caption = "Reference"
            'Me.grdSaved.RootTable.Columns("cheque_no").Caption = "Cheque No"
            'Me.grdSaved.RootTable.Columns("detail_title").Caption = "Vendor"
            'grdSaved.RootTable.Columns("cheque_date").Caption = "Cheque Date"

            ''grdChqs.RowHeadersVisible = False

            'For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdSaved.RootTable.Columns
            '    'col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            '    'col.ReadOnly = True
            'Next

            Dim str As String

            str = "select " & If(Val(Me.txtTopMasterRecords.Text) > 0, " top " & Val(Me.txtTopMasterRecords.Text), "") & " tblVoucher.voucher_id, tblVoucher.Voucher_No, tblVoucher.voucher_date as TransferDate, vwCOADetail.detail_title as [Transferred To], tblVoucher.paid_to, tblVoucher.coa_detail_id, tblVoucher.Cheque_No, tblVoucher.Cheque_Date " _
                & "  from tblVoucher LEFT OUTER JOIN " _
                & " vwCOADetail ON tblVoucher.coa_detail_id = vwCOADetail.coa_detail_id " _
                & " WHERE     (tblVoucher.source = 'frmChequeTransfer') AND (tblVoucher.voucher_date IS NOT NULL) " & IIf(Me.cmbSearchVendor.ActiveRow.Cells(0).Value > 0, " and tblVoucher.coa_detail_id = " & Me.cmbSearchVendor.ActiveRow.Cells(0).Value & "", "") & " " & IIf(Me.dtpFrom.Checked = True, " and tblvoucher.voucher_date >= '" & Me.dtpFrom.Value.Date & " 00:00:00:000 ' ", " ") & "  " & IIf(dtpTo.Checked = True, "  and tblvoucher.voucher_date <= '" & Me.dtpTo.Value.Date & " 23:59:59:000'", "") _
                & " order by tblVoucher.voucher_no"
            FillGridEx(Me.grdSaved, str, True)
            grdSaved.RootTable.Columns(0).Visible = False
            grdSaved.RootTable.Columns("paid_to").Visible = False
            grdSaved.RootTable.Columns("coa_detail_id").Visible = False
            grdSaved.RootTable.Columns("Cheque_Date").Visible = False
            grdSaved.RootTable.Columns("TransferDate").FormatString = "dd-MMM-yyyy"
            grdSaved.AutoSizeColumns()

        Catch ex As SqlClient.SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub EditRecord()
        Try

            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.dtpVoucherDate.Enabled = False
                Else
                    Me.dtpVoucherDate.Enabled = True
                End If
            Else
                Me.dtpVoucherDate.Enabled = True
            End If

            Me.txtVoucherNo.Text = Me.grdSaved.CurrentRow.Cells("voucher_no").Value
            Me.rdbAll.Checked = True
            Me.cmbAccount.Value = Me.grdSaved.CurrentRow.Cells("coa_detail_id").Value
            dtpVoucherDate.Value = grdSaved.CurrentRow.Cells("voucher_date").Value
            txtVoucherID.Text = grdSaved.CurrentRow.Cells("voucher_id").Value
            Me.txtCustomer.Text = grdSaved.CurrentRow.Cells("paid_to").Value
            Me.txtParentVoucherID.Text = grdSaved.CurrentRow.Cells("ParentVoucherID").Value
            Me.btnSave.Text = "&Update"
            GetSecurityRights()
            CtrlGrdBar1_Load(Nothing, Nothing)
            CtrlGrdBar2_Load(Nothing, Nothing)
            CtrlGrdBar3_Load(Nothing, Nothing)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub grdSaved_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            'If Me.grdSaved.GetRows.Length = 0 Then Exit Sub
            'Me.EditRecord()
            'Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub
    Private Sub grdChqs_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            Me.RefreshControls()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub cmbAccount_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAccount.Enter
        Try


            Me.cmbAccount.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.Dropdown)

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'ValidateDateLock()
        'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
        If flgDateLock = True Then
            If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
                ShowErrorMessage("Previous date work not allowed") : Exit Sub
            End If
        End If

        If grdSaved.CurrentRow.RowType <> Janus.Windows.GridEX.RowType.Record Then
            Exit Sub
        End If

        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

        Dim lngVoucherMasterId As Integer = grdSaved.CurrentRow.Cells(0).Value 'GetVoucherId(Me.Name, grdSaved.CurrentRow.Cells(0).Value.ToString)
        'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim cm As New OleDbCommand
            Dim objTrans As OleDbTransaction
            If Con.State = ConnectionState.Closed Then Con.Open()
            objTrans = Con.BeginTransaction
            cm = New OleDbCommand
            cm.Connection = Con

            cm.CommandText = "update tblvoucherDetail set cheque_paid = 0 where voucher_Detail_id in( select parent_id from tblVoucherDetail where voucher_id=" & Me.grdSaved.CurrentRow.Cells("voucher_id").Value & ") "
            cm.Transaction = objTrans
            cm.ExecuteNonQuery()


            cm.CommandText = "delete from tblvoucherdetail where voucher_id=" & Me.grdSaved.CurrentRow.Cells("voucher_id").Value
            cm.ExecuteNonQuery()

            cm.CommandText = "delete from tblvoucher where voucher_id=" & Me.grdSaved.CurrentRow.Cells("voucher_id").Value
            cm.ExecuteNonQuery()

            objTrans.Commit()

            'R-974 Ehtisham ul Haq user friendly system modification on 7-1-14
            ' msg_Information(str_informDelete)

            SaveActivityLog("Accounts", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, grdSaved.CurrentRow.Cells(1).Value & "", True)


        Catch ex As Exception
            msg_Error("Error occured while deleting record: " & ex.Message)

        Finally
            Con.Close()
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
        Me.RefreshControls()
        Me.DisplayMasterRecords()
        Me.DisplayRecord()
    End Sub
    Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
        Try

            Me.Cursor = Cursors.WaitCursor

            DisplayMasterRecords()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub cmdClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        Try
            Me.cmbSearchVendor.Rows(0).Activate()
            Me.DisplayMasterRecords()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    'Sub GetPendingVouchers()
    '    'Try
    '    '    Dim Str As String = " SELECT     dbo.tblVoucher.voucher_id, dbo.tblVoucher.voucher_no, dbo.tblVoucher.voucher_date, tblVoucher.Cheque_no, dbo.vwCOADetail.detail_title, tblvoucher.cheque_date, tblvoucher.coa_detail_id" _
    '    '      & "   FROM         tblVoucher INNER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id INNER JOIN vwCOADetail ON tblVoucher.coa_detail_id = vwCOADetail.coa_detail_id " _
    '    '      & " Where tblVoucherDetail.debit_amount > 0 and tblVoucher.cheque_paid = 0 and voucher_type_id  = 5 " & IIf(Me.cmbCustomer.ActiveRow.Cells(0).Value > 0, " and tblVoucher.coa_detail_id = " & Me.cmbCustomer.ActiveRow.Cells(0).Value & "", "") & " and tblvoucher.voucher_date between '" & Me.dtpCustomerFrom.Value.Date & " 00:00:00:000' and '" & Me.dtpCustomerTo.Value.Date & " 23:59:59:000' "

    '    '    If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
    '    '        Str = Str & " and tblvoucher.coa_detail_id is not null"
    '    '    End If

    '    '    Str = Str & " order by voucher_no"

    '    '    FillGridEx(grdChqs, Str, True)
    '    '    grdChqs.RootTable.Columns(0).Visible = False

    '    'Catch ex As Exception
    '    '    Throw ex
    '    'End Try
    'End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            DisplayRecord()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.cmbCustomer.Rows(0).Activate()
            Me.DisplayRecord()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.dtpVoucherDate.Value.ToString("yyyy-M-d 00:00:00") Then
                If DateLock.Lock = True Then
                    Return True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ValidateDateLock()
        Try
            Dim dateLock As New SBModel.DateLockBE
            dateLock = DateLockList.Find(AddressOf chkDateLock)
            If dateLock IsNot Nothing Then
                If dateLock.DateLock.ToString.Length > 0 Then
                    flgDateLock = True
                Else
                    flgDateLock = False
                End If
            Else
                flgDateLock = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub grdChqs_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdChqs.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13

        'Try
        '    If e.KeyCode = Keys.Delete Then
        '        DeleteToolStripButton_Click(btnDelete, Nothing)
        '        Exit Sub
        '    End If

        'Catch ex As Exception
        '    msg_Error(ex.Message)
        'End Try
    End Sub

    Private Sub grdChqs_RowCheckStateChanged(sender As Object, e As Janus.Windows.GridEX.RowCheckStateChangeEventArgs) Handles grdChqs.RowCheckStateChanged
        Try
            If grdChqs.GetCheckedRows.Length > 0 Then

                Me.strChqueList = String.Empty
                strChqueList = grdChqs.GetCheckedRows.Length & " cheques ["
                For Each row As Janus.Windows.GridEX.GridEXRow In grdChqs.GetCheckedRows
                    Me.strChqueList = strChqueList & row.Cells("Cheque_no").Value.ToString & ", "
                Next
                strChqueList = strChqueList & "]"
            Else
                strChqueList = String.Empty
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnSearchCheque_Click(sender As Object, e As EventArgs) Handles btnSearchCheque.Click
        Try
            Dim strSQL As String
            strSQL = "SELECT       tblVoucher.voucher_id, tblVoucherDetail.voucher_detail_id, tblVoucher.voucher_no, tblVoucher.voucher_date, tblVoucherDetail.Cheque_No, vwCOADetail.detail_title, tblVoucherDetail.Cheque_Date,  " &
            " tblVoucherDetail.credit_amount, tblVoucherDetail.comments, tblVoucherDetail.coa_detail_id, tblVoucherDetail.Parent_Id, tblVoucherDetail.cheque_paid, 'Preview' as Preview " &
            " FROM            tblVoucher INNER JOIN " &
            " tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id INNER JOIN " &
            " vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id " &
            " WHERE        (tblVoucherDetail.Cheque_No <> '') AND (tblVoucherDetail.credit_amount > 0) and tblVoucherDetail.Cheque_No like '%" & Me.txtChequSerch.Text.Trim & "%' "
            FillGridEx(grdChequeDetail, strSQL)
            grdChequeDetail.AutoSizeColumns()
            '  grdChequeDetail.ExpandRecords()
            grdChequeDetail.RootTable.Columns("detail_title").Caption = "Received from"
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Private Sub grdChequeDetail_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdChequeDetail.ColumnButtonClick
        Try
            ShowReport("rptVoucher", , , , , , , DTFromGrid(Val(Me.grdChequeDetail.GetRow.Cells("Voucher_Id").Value.ToString)), , , True)

            If ReportViewerForContainer IsNot Nothing Then
                ReportViewerForContainer.Show()
                ReportViewerForContainer.BringToFront()
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Function DTFromGrid(ByVal voucherID As Int32) As DataTable 'TASK42
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grdChequeDetail.RowCount = 0 Then Exit Function
            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetCheckedRows
            Dim DT As New DataTable
            DT = GetDataTable("SP_RptVoucher " & voucherID & "") ' r.Cells(EnumGridMaster.Voucher_Id).Value
            DT.AcceptChanges()
            'DT.Columns.Add("Convert(image, null) as BarCode")
            'Next
            For Each DR As DataRow In DT.Rows
                DR.BeginEdit()
                Dim bcp As New BarcodeProfessional()
                'bcp.Symbology = Symbology.Code39
                bcp.Symbology = Symbology.Code128
                'bcp.Symbology = Symbology.Code93



                bcp.Extended = True
                bcp.DisplayCode = False

                ' bcp.Text=Symbology.
                'bcp.Text = String.Empty
                'bcp.Symbology = Symbology.Code128

                bcp.AddChecksum = False

                'bcp.BarWidth = 3
                'bcp.BarHeight = 0.04F
                'DR.Item("Convert(image, null) as BarCode")
                bcp.Code = "?" & DR.Item("voucher_no").ToString
                'bcp.Code = DR.Item("Employee_Code").ToString
                DR.Item("BarCode") = bcp.GetBarcodeImage(System.Drawing.Imaging.ImageFormat.Png)
                'LoadPicture(DR, "Picture", DR.Item("EmpPicture"))


                DR.EndEdit()


            Next
            Return DT
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function

    Private Sub btnPrintVoucher_Click(sender As Object, e As EventArgs) Handles btnPrintVoucher.Click
        Try
            If grdSaved.RowCount > 0 Then
                GetVoucherPrint(Me.grdSaved.CurrentRow.Cells("Voucher_No").Value.ToString, Me.Name)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbAccount_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbAccount.KeyDown
        ''TFS1781 : Ayesha Rehman :Added for Selection of Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.AccountType = IIf(Me.rdbAll.Checked, "", "'Bank'")
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbAccount.Value = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbCustomer_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbCustomer.KeyDown
        ''TFS1781 : Ayesha Rehman :Added for Selection of Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.AccountType = IIf(Me.optAllCustomer.Checked, "", "'Customer'")
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbAccount.Value = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdChqs.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdChqs.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdChqs.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Cheque Transfer"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Cheque Transfer"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Lsayouts\" & Me.Name & "_" & Me.grdChequeDetail.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdChequeDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdChequeDetail.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Cheque Transfer"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanging(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        If e.Tab.Index = 1 Then
            Me.CtrlGrdBar1.Visible = False
            Me.CtrlGrdBar2.Visible = True
        Else
            Me.CtrlGrdBar1.Visible = True
            Me.CtrlGrdBar2.Visible = False
        End If
    End Sub
End Class

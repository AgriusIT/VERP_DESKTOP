Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class frmOpening
    Dim Con As SqlConnection

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            GetData()

            If Val(CType(grdStock.DataSource, DataTable).Compute("Sum(Balance)", "").ToString) <> 0 Then
                msg_Error("Balance are not accurate. Debit and Credit amount must be equal")
                Exit Sub
            End If

            msg_Information("Before proceeding please make sure opening balances are accurate!" & Chr(10) & Chr(10) & "Once opening balance will be saved then you will not be able to change or reverse them. ")

            If msg_Confirm("Are you sure you want to proceed with current opening balances") = False Then
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim VId As Integer = 0
            Con = New SqlConnection(SQLHelper.CON_STR)
            Dim cm As New SqlCommand
            cm.Connection = Con
            Con.Open()

            '// Deleting Opening
            cm.CommandText = "DELETE FROM tblVoucherDetail WHERE     (voucher_id in (select voucher_id from tblVoucher where voucher_code='Opening'))"
            cm.ExecuteNonQuery()

            cm.CommandText = "DELETE FROM tblVoucher WHERE     (voucher_code='Opening')"
            cm.ExecuteNonQuery()

            '// Entering Opening Master
            cm.CommandText = "INSERT INTO tblVoucher " _
                            & " (location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, cheque_no, post, source, Reference, " _
                            & " UserName) " _
                            & " VALUES     (1, 'Opening', 1, 1, 'Opening', CONVERT(DATETIME, '" & Me.DateTimePicker1.Value.Date.ToString("yyyy-MM-dd") & " 00:00:00', 102), '', 1, 'frmOpening', 'frmOpening', 'Opening') " _
                            & "Select Ident_Current('tblVoucher') "

            VId = Val(cm.ExecuteScalar())

            cm.CommandText = "INSERT INTO tblVoucherDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount) ( " _
            & " select " & VId & ",1,AcId, 'Opening As On " & Me.DateTimePicker1.Value.Date.ToString("dd/MMM/yyyy") & "' as Comments,Dr,Cr " _
            & " from( SELECT     coa_detail_id as AcId, CASE WHEN  ISNULL(OpeningBalance, 0)> 0 THEN ISNULL(OpeningBalance, 0) ELSE 0 END AS Dr, CASE WHEN  ISNULL(OpeningBalance, 0)< 0 THEN ISNULL(OpeningBalance, 0) * -1 ELSE 0 END AS Cr " _
            & " FROM         tblCOAMainSubSubDetail WHERE     (ISNULL(OpeningBalance, 0) <> 0)) Opening)"

            cm.ExecuteNonQuery()

            'msg_Information("Opening balance saved successfully.")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Me.lblProgress.BackColor = Color.LightYellow

            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim VId As Integer = 0
            Con = New SqlConnection(SQLHelper.CON_STR)
            Dim cm As New SqlCommand
            cm.Connection = Con
            Con.Open()

            '// Deleting Opening
            cm.CommandText = "DELETE FROM tblVoucherDetail WHERE     (voucher_id in (select voucher_id from tblVoucher where voucher_code='Opening'))"
            cm.ExecuteNonQuery()

            cm.CommandText = "DELETE FROM tblVoucher WHERE     (voucher_code='Opening')"
            cm.ExecuteNonQuery()

            ''// Entering Opening Master
            'cm.CommandText = "INSERT INTO tblVoucher " _
            '                & " (location_id, voucher_code, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, cheque_no, post, source, Reference, " _
            '                & " UserName) " _
            '                & " VALUES     (1, 'Opening', 1, 1, 'Opening', CONVERT(DATETIME, '" & Me.DateTimePicker1.Value.Date.ToString("yyyy-mm-dd") & " 00:00:00', 102), '', 1, 'frmOpening', 'frmOpening', 'Opening') " _
            '                & "Select Ident_Current('tblVoucher') "

            'VId = cm.ExecuteScalar()

            'cm.CommandText = "INSERT INTO tblVoucherDetail (voucher_id, location_id, coa_detail_id, comments, debit_amount, credit_amount) ( " _
            '& " select (" & VId & ",1,AcId, 'Opening As On ' as Comments,Dr,Cr " _
            '& " from( SELECT     coa_detail_id as AcId, CASE WHEN  ISNULL(OpeningBalance, 0)> 0 THEN ISNULL(OpeningBalance, 0) ELSE 0 END AS Dr, CASE WHEN  ISNULL(OpeningBalance, 0)< 0 THEN ISNULL(OpeningBalance, 0) * -1 ELSE 0 END AS Cr " _
            '& " FROM         tblCOAMainSubSubDetail WHERE     (ISNULL(OpeningBalance, 0) <> 0)) Opening)"

            'cm.ExecuteNonQuery()

            'msg_Information("Opening balance deleted successfully.")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally

            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If

            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                'Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmExpense)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        'If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                        '    Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        'Else
                        '    Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        'End If
                        'Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                'UserPostingRights = GetUserPostingRights(LoginUserId)
                'If UserPostingRights = True Then
                '    Me.chkPost.Visible = True
                'Else
                '    Me.chkPost.Visible = False
                '    Me.chkPost.Checked = False
                'End If
                'GetSecurityByPostingUser(UserPostingRights, BtnSave, BtnDelete)
            Else
                'Me.Visible = False
                'Me.BtnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
                ''Me.BtnPrint.Enabled = False
                'Me.chkPost.Visible = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                        'ElseIf Rights.Item(i).FormControlName = "Save" Then
                        '    If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Update" Then
                        '    If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.BtnDelete.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Print" Then
                        '    Me.BtnPrint.Enabled = True
                        '    CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        '    CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        '    Me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmOpening_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            btnSave_Click(Nothing, Nothing)

        End If
        If e.KeyCode = Keys.Escape Then
            btnDelete_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmOpening_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Me.WindowState = FormWindowState.Maximized

            Dim dt As New DataTable
            dt = GetDataTable("select voucher_id, voucher_date from tblVoucher where voucher_code='Opening'")
            If dt.Rows.Count = 0 Then
                Me.btnSave.Enabled = True
                GetSecurityRights()
            Else
                btnSave.Enabled = False
                Me.DateTimePicker1.Value = CType(dt.Rows(0).Item("voucher_date").ToString, DateTime)
                Me.DateTimePicker1.Enabled = False
            End If

            GetData()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Sub GetData()

        Try
            Dim dtDetail As New DataTable
            dtDetail = GetDataTable("select coa_detail_id, main_sub_sub_id, detail_code as Code, detail_title, end_date, Active, SortOrder, IsDate, OpeningBalance as Balance, old_main_sub_sub_id, old_detail_code, Parent_Id, " & _
                                    " Case When OpeningBalance > 0 Then OpeningBalance else 0 End as Debit_Amount, Case When OpeningBalance < 0 Then OpeningBalance*-1 else 0 End as Credit_Amount " & _
                                    " from tblCOAMainSubSubDetail")
            Me.grdStock.DataSource = dtDetail
            grdStock.ExpandRecords()
            grdStock.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        GetData()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        frmMain.LoadControl("frmDetailAccount")
    End Sub
End Class
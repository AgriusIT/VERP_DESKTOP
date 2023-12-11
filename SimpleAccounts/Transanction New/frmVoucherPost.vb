Imports System.Data.OleDb
Public Class frmVoucherPost
    Private Sub TxtVoucherCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtVoucherCode.KeyDown
        Try

            If e.KeyCode = Keys.Enter Then
                If e.KeyCode = Keys.OemQuestion Then
                    e.SuppressKeyPress = True
                End If
                If Me.TxtVoucherCode.Text.Length > 0 Then
                    If GetVoucherInfo(Me.TxtVoucherCode.Text) = True Then
                        If Save() = True Then
                            Me.Timer1.Start()
                            Me.Timer1.Enabled = True
                            FillGrid()
                            Me.TxtVoucherCode.Text = String.Empty

                        End If
                    End If
                End If

            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

 
    Public Function GetVoucherInfo(ByVal RegCode As String) As Boolean
        Try


            Dim strSQL As String = String.Empty
            strSQL = "Select a.voucher_no, a.voucher_Id,convert(varchar,a.voucher_date,105) as voucher_Date ,a.username,isnull(a.checkedbyuser,'')  as checkedbyuser,sum(debit_amount) as Amount,post from tblvoucher a left outer join tblvoucherdetail b on a.voucher_id = b.voucher_id " _
            & " where a.voucher_no='" & TxtVoucherCode.Text.Replace("?", "").Replace("'", "''") & "' group by a.voucher_Id,a.username,checkedbyuser,post,voucher_date,a.voucher_no"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.TableName = "VouchertData"
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("post").ToString = "True" Then
                        ShowErrorMessage("Voucher had already been posted")
                        TxtVoucherCode.Text = String.Empty
                        Return False
                    Else
                        Me.LblVoucherNo.Text = dt.Rows(0).Item("Voucher_No").ToString
                        Me.LblVoucherDate.Text = dt.Rows(0).Item("Voucher_Date").ToString
                        Me.LblVoucherAmount.Text = dt.Rows(0).Item("Amount").ToString
                        Me.LblUserName.Text = dt.Rows(0).Item("username").ToString
                        Me.LblCheckedBy.Text = dt.Rows(0).Item("checkedbyuser").ToString
                        'Application.DoEvents()
                        Panel1.Height = 175
                        Timer1.Interval = 10000
                        Return True
                    End If
                End If
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            Me.Timer1.Stop()
            Me.Timer1.Enabled = False
            '   FillGrid()
            Me.LblVoucherDate.Text = String.Empty
            Me.LblVoucherNo.Text = String.Empty
            Me.LblVoucherAmount.Text = String.Empty
            Me.LblUserName.Text = String.Empty
            Me.LblCheckedBy.Text = String.Empty
            Me.TxtVoucherCode.Focus()
            Panel1.Height = 45

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillGrid(Optional ByVal Condition As String = "")
        Try
            Dim strSQL As String = String.Empty
            Dim datefrom As Date
            Me.GetSecurityRights()

            datefrom = Date.Today

            strSQL = "SELECT distinct  Top 50 a.voucher_Id,convert(datetime,a.voucher_date,102) as voucher_Date ,a.Voucher_No,sum(debit_amount) as Amount,a.username,isnull(a.checkedbyuser,'')  as 'Checked By',posted_username as 'Posted By' from tblvoucher a left outer join tblvoucherdetail b on a.voucher_id = b.voucher_id" _
            & " where CONVERT(varchar, posting_date, 102) between  CONVERT(datetime, '" & datefrom.ToString("yyyy-M-d 00:00:00") & "', 102) and CONVERT(datetime, '" & datefrom.ToString("yyyy-M-d 23:59:59") & "', 102)  group by a.voucher_Id,a.Voucher_No,a.username,checkedbyuser,post,voucher_date,posted_username order by voucher_date desc"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()


            Me.Grd.DataSource = dt
            Me.Grd.RetrieveStructure()
            Me.Grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.Grd.RootTable.Columns("voucher_Id").Visible = False
            Me.Grd.RootTable.Columns("voucher_date").Caption = "Voucher Date"
            Me.Grd.RootTable.Columns("voucher_No").Caption = "Voucher Code"
            Me.Grd.RootTable.Columns("voucher_date").FormatString = "dd/MMM/yyyy"
            Me.Grd.RootTable.Columns("UserName").Caption = "User Name"
            Me.Grd.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.Grd.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.Grd.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.Grd.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.Grd.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.Grd.RootTable.Columns("Amount").TotalFormatString = "N" & DecimalPointInValue

            Me.Grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.Grd.RootTable.Columns("Amount").Width = 150
            Me.Grd.RootTable.Columns("voucher_date").Width = 160
            Me.Grd.RootTable.Columns("Voucher_No").Width = 160
            Me.Grd.RootTable.Columns("UserName").Width = 175
            Me.Grd.RootTable.Columns("Checked By").Width = 175
            Me.Grd.RootTable.Columns("Posted By").Width = 175
            Me.Grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.Grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function Save() As Boolean
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection

        objCon = Con
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        objCommand.Connection = objCon
        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans

            objCommand.CommandText = "Update tblvoucher set post = 1, posted_username = '" & LoginUserName & "',Posting_Date =N'" & Date.Now.ToString("yyyy-M-d h:mm:ss tt") & "'  where voucher_No=N'" & TxtVoucherCode.Text.Replace("?", "").Replace("'", "''") & "'"

            Dim identity As Integer = Convert.ToInt32(objCommand.ExecuteScalar())
            trans.Commit()
            Save = True

        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try

    End Function

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Try
            Me.GetSecurityRights()
            FillGrid()
            Me.TxtVoucherCode.Text = String.Empty
            Me.LblVoucherDate.Text = String.Empty
            Me.LblVoucherNo.Text = String.Empty
            Me.LblVoucherAmount.Text = String.Empty
            Me.LblUserName.Text = String.Empty
            Me.LblCheckedBy.Text = String.Empty

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmVoucherPost_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.OemQuestion Then
                e.SuppressKeyPress = True
                Me.TxtVoucherCode.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmVoucherPost_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.GetSecurityRights()
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            Me.BtnDelete.Visible = False
            Me.BtnSave.Visible = False
            Me.BtnNew.Visible = False
            Me.BtnPrint.Visible = False
            Me.BtnEdit.Visible = False

            If LoginGroup = "Administrator" Then
                'Me.BtnSave.Enabled = True
                'Me.BtnDelete.Enabled = True
                'Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefCity)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub lbldcno_Click(sender As Object, e As EventArgs) Handles lbldcno.Click

    End Sub

    Private Sub TxtVoucherCode_TextChanged(sender As Object, e As EventArgs) Handles TxtVoucherCode.TextChanged

    End Sub
End Class
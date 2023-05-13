Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class frmYearClose
    Implements IGeneral
    Dim fstart_Date As DateTime = "2007-1-1 00:00:00"
    Dim startDate As DateTime
    Dim PLVoucher As PLVoucher
    Dim PLVoucherDetail As PLVoucherDetail
    Dim PLDebit_Amount As Double
    Dim ClosingYearId As Integer = 0
    Dim VoucherId As Integer = 0
    Dim grdStartDate As DateTime = Nothing
    Private Sub frmYearClose_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Label2.Text = String.Empty
            Me.pgbyearclose.Value = 0
            Dim str As String = "Select voucher_no From tblVoucher WHERE LEFT(voucher_no,3)='CLS'"
            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    startDate = Convert.ToDateTime(CDate(GetConfigValue("EndOfDate").ToString))
                ElseIf dt.Rows.Count = 0 Then
                    startDate = fstart_Date
                End If
            End If
            Me.DtpYearClose.Value = Date.Now
            ReSetControls()
            Call DtpYearClose_ValueChanged(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DtpYearClose_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DtpYearClose.ValueChanged
        Try
            Me.txtdcno.Text = "CLS-" + "" & Me.DtpYearClose.Value.Date.ToString("ddMMyyyy") & ""
            If Me.btnSave.Text = "&Save" Then
                If CDate(fstart_Date.ToString("yyyy-M-d 00:00:00")) > CDate(Me.DtpYearClose.Value.ToString("yyyy-M-d 23:59:59")) Then
                    ShowErrorMessage("Please enter valid closing date")
                    Exit Sub
                End If
                If CDate(Date.Now.ToString("yyyy-M-d 00:00:00")) < CDate(Me.DtpYearClose.Value.ToString("yyyy-M-d 00:00:00")) Then
                    ShowErrorMessage("Please enter valid closing date")
                    Exit Sub
                End If
            End If
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If startDate > Me.DtpYearClose.Value Then
                ShowErrorMessage("Please enter valid closing date")
                Exit Sub
            End If
            If Me.SplitContainer1.Panel2Collapsed = True Then
                Me.SplitContainer1.Panel2Collapsed = False
                GetAll()
            Else
                Me.SplitContainer1.Panel2Collapsed = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.Cursor = Cursors.WaitCursor
        Try

            'If Date.Now.ToString("yyyy-M-d 00:00:00") < Me.DtpYearClose.Value.ToString("yyyy-M-d 23:59:59") Then
            '    ShowErrorMessage("Please enter valid closing date")
            '    Exit Sub
            'End If
            If Me.btnSave.Text = "&Save" Then
                Dim dt As DataTable = GetDataTable("Select voucher_no from tblVoucher WHERE voucher_no='" & Me.txtdcno.Text & "'")
                If dt Is Nothing Then Exit Sub
                If dt.Rows.Count > 0 Then
                    ShowErrorMessage("Financial year closed!")
                    Me.DtpYearClose.Focus()
                    Exit Sub
                End If
            Else
                Dim dt As DataTable = GetDataTable("SELECT * From tblFinancialYearCloseStatus")
                Dim dr() As DataRow
                dr = dt.Select("YearCloseId > " & ClosingYearId & "")
                If dr.Length > 0 Then
                    ShowErrorMessage("Voucher can not be updated")
                    Exit Sub
                End If
            End If
            If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
            Me.Label2.Text = "Start Process...."
            Application.DoEvents()
            Do Until Me.pgbyearclose.Value >= 15
                Me.pgbyearclose.Value = Me.pgbyearclose.Value + 1
                Application.DoEvents()
            Loop
            Me.Label2.Text = "Saving record..."
            Do Until Me.pgbyearclose.Value >= 70
                Me.pgbyearclose.Value = Me.pgbyearclose.Value + 1
                Application.DoEvents()
            Loop
            If VoucherId = 0 Then
                If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            Else
                If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            End If
            Me.Label2.Text = "Update closing date"
            Do Until Me.pgbyearclose.Value >= 95
                Me.pgbyearclose.Value = Me.pgbyearclose.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            Call New PLVoucherDAL().UpdateClosingDate(Me.DtpYearClose.Value)
            Me.Label2.Text = "Process completed successfully."
            Do Until Me.pgbyearclose.Value >= 98
                Me.pgbyearclose.Value = Me.pgbyearclose.Value + 1
                Application.DoEvents()
                System.Threading.Thread.Sleep(50)
            Loop
            msg_Information(str_informSave)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            FillModel()
            Call New PLVoucherDAL().Delete(PLVoucher)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            'frmViewPLVoucher._startDate = fstart_Date
            'frmViewPLVoucher._endDate = Me.DtpYearClose.Value

            Dim str As String = "Select voucher_no From tblVoucher WHERE LEFT(voucher_no,3)='CLS'"
            Dim dtDate As DataTable = GetDataTable(str)
            If Not dtDate Is Nothing Then
                If Not grdStartDate = Nothing Then
                    startDate = grdStartDate
                ElseIf dtDate.Rows.Count > 0 Then
                    startDate = Convert.ToDateTime(GetConfigValue("EndOfDate").ToString)
                ElseIf dtDate.Rows.Count = 0 Then
                    startDate = fstart_Date
                End If
            End If

            PLDebit_Amount = Val(Me.grdPLVoucher.GetTotal(Me.grdPLVoucher.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grdPLVoucher.GetTotal(Me.grdPLVoucher.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) 'frmViewPLVoucher.GetAll.GetTotal(frmViewPLVoucher.GetAll.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - frmViewPLVoucher.GetAll.GetTotal(frmViewPLVoucher.GetAll.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
            PLVoucher = New PLVoucher
            PLVoucher.YearCloseId = ClosingYearId
            PLVoucher.voucher_id = VoucherId
            PLVoucher.voucher_type_id = 1
            PLVoucher.voucher_code = Me.txtdcno.Text.ToString.Replace("'", "''")
            PLVoucher.voucher_date = Me.DtpYearClose.Value
            PLVoucher.voucher_no = Me.txtdcno.Text.ToString.Replace("'", "''")
            PLVoucher.source = "frmVoucher"
            PLVoucher.post = True
            PLVoucher.UserName = LoginUserName
            PLVoucher.PLDebitAmount = PLDebit_Amount
            PLVoucher.RefDate = startDate.Date
            PLVoucher.PLVoucherDetail = New List(Of PLVoucherDetail)
            Dim dt As DataTable = CType(GetAll.DataSource, DataTable)
            For Each r As DataRow In dt.Rows
                PLVoucherDetail = New PLVoucherDetail
                PLVoucherDetail.voucher_id = 0
                PLVoucherDetail.coa_detail_id = Val(r.Item("coa_detail_id"))
                PLVoucherDetail.credit_amount = Val(r.Item("credit_amount"))
                PLVoucherDetail.debit_amount = Val(r.Item("debit_amount"))
                PLVoucher.PLVoucherDetail.Add(PLVoucherDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As DataTable = New PLVoucherDAL().GetMasterRecord
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            grd.RetrieveStructure()
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns(1).Visible = False
            Me.grd.RootTable.Columns("Post").Visible = False
            grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            grdStartDate = Nothing
            Dim str As String = "Select voucher_no From tblVoucher WHERE LEFT(voucher_no,3)='CLS'"
            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If Not grdStartDate = Nothing Then
                    startDate = grdStartDate
                ElseIf dt.Rows.Count > 0 Then
                    startDate = Convert.ToDateTime(GetConfigValue("EndOfDate").ToString).AddDays(1)
                ElseIf dt.Rows.Count = 0 Then
                    startDate = fstart_Date
                End If
            End If
            VoucherId = 0
            Me.DtpYearClose.Value = Date.Now
            GetAllRecords()
            GetAll()
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            FillModel()
            Call New PLVoucherDAL().Add(PLVoucher)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            FillModel()
            Call New PLVoucherDAL().Update(PLVoucher)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub grd_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            ClosingYearId = Me.grd.GetRow.Cells("YearCloseId").Value
            VoucherId = Me.grd.GetRow.Cells("Voucher_Id").Value
            grdStartDate = Me.grd.GetRow.Cells("Start Date").Value
            Me.DtpYearClose.Value = Me.grd.GetRow.Cells("Closing Date").Value
            Me.txtdcno.Text = Me.grd.GetRow.Cells("RefVoucher_No").Value.ToString
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            'Me.SplitContainer2.Panel1Collapsed = True
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim dt As DataTable = GetDataTable("SELECT * From tblFinancialYearCloseStatus")
            Dim dr() As DataRow
            dr = dt.Select("YearCloseId > " & ClosingYearId & "")
            If dr.Length > 0 Then
                ShowErrorMessage("Voucher can not be deleted")
                Exit Sub
            End If
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            msg_Information(str_informDelete)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            grd_DoubleClick(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
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
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public _startDate As DateTime
    Public _endDate As DateTime
    Public Function GetAll() As Janus.Windows.GridEX.GridEX
        Try

            Dim str As String = "Select voucher_no From tblVoucher WHERE LEFT(voucher_no,3)='CLS'"
            Dim dtDate As DataTable = GetDataTable(str)
            If Not dtDate Is Nothing Then
                If Not grdStartDate = Nothing Then
                    startDate = grdStartDate
                ElseIf dtDate.Rows.Count > 0 Then
                    startDate = Convert.ToDateTime(GetConfigValue("EndOfDate").ToString).AddDays(1)
                ElseIf dtDate.Rows.Count = 0 Then
                    startDate = fstart_Date
                End If
            End If

            Dim dt As DataTable = New PLVoucherDAL().GetAllRecords(startDate.ToString("yyyy-M-d 00:00:00"), Me.DtpYearClose.Value.ToString("yyyy-M-d 23:59:59"))
                dt.AcceptChanges()
                Me.grdPLVoucher.DataSource = Nothing
                Me.grdPLVoucher.DataSource = dt
                Me.grdPLVoucher.RetrieveStructure()
                Me.grdPLVoucher.RootTable.Columns("coa_detail_id").Visible = False
                Me.grdPLVoucher.RootTable.Columns("Gl_note_id").Visible = False
                Me.grdPLVoucher.RootTable.Columns("Balance").Visible = False

                Me.grdPLVoucher.RootTable.Columns("detail_title").Caption = "Account Description"
                Me.grdPLVoucher.RootTable.Columns("note_title").Caption = "PL Note"
                Me.grdPLVoucher.RootTable.Columns("Debit_Amount").Caption = "Debit"
                Me.grdPLVoucher.RootTable.Columns("Credit_Amount").Caption = "Credit"
                Me.grdPLVoucher.RootTable.Columns("Debit_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdPLVoucher.RootTable.Columns("Credit_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdPLVoucher.RootTable.Columns("Balance").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                Me.grdPLVoucher.RootTable.Columns("Debit_Amount").FormatString = "N"
                Me.grdPLVoucher.RootTable.Columns("Credit_Amount").FormatString = "N"
                Me.grdPLVoucher.RootTable.Columns("Balance").FormatString = "N"

                Me.grdPLVoucher.RootTable.Columns("Debit_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdPLVoucher.RootTable.Columns("Credit_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdPLVoucher.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grdPLVoucher.RootTable.Columns("Debit_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdPLVoucher.RootTable.Columns("Credit_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdPLVoucher.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdPLVoucher.AutoSizeColumns()
                Me.TextBox1.Text = Val(Me.grdPLVoucher.GetTotal(Me.grdPLVoucher.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) - Val(Me.grdPLVoucher.GetTotal(Me.grdPLVoucher.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)) 'frmViewPLVoucher.GetAll.GetTotal(frmViewPLVoucher.GetAll.RootTable.Columns("Debit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum) - frmViewPLVoucher.GetAll.GetTotal(frmViewPLVoucher.GetAll.RootTable.Columns("Credit_Amount"), Janus.Windows.GridEX.AggregateFunction.Sum)
                Return grdPLVoucher
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
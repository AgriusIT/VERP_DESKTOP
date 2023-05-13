Imports SBModel
Imports SBDal
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class frmGrdDailySupply
    Dim Status As String = String.Empty
    Dim DailySupplyDetail As List(Of DailySupplyAndGatePass)
    Sub ApplySecurity()
        Try
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDetailAccount)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        'Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                'Me.btnDelete.Enabled = False
                'Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                        ' ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.btnDelete.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Print" Then
                        '   Me.btnPrint.Enabled = True
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
    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFromDate.ValueChanged, dtpToDate.ValueChanged
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = "Daily Supply And GatePass From " & Me.dtpFromDate.Value.Date.ToString("ddMMyyyy") & " To " & Me.dtpToDate.Value.Date.ToString("ddMMyyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdDailySupply_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            btnSave_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmGrdDailySupply_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            FillBind()
            ApplySecurity()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillBind()
        Try
            Dim str As String = String.Empty
            str = "SP_UpdateDailySupplyAndGatePass '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', '" & Status & "'"
            Dim dt As DataTable = GetDataTable(str)
            dt.AcceptChanges()
            Me.GridEX1.DataSource = Nothing
            Me.GridEX1.DataSource = dt
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns("SalesDate").Caption = "Delivery Date"
            Me.GridEX1.RootTable.Columns("SalesNo").Caption = "Invoice No"
            Me.GridEX1.RootTable.Columns("TransporterName").Caption = "Driver Name"
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                If Not col.Caption = "Status" Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            Me.GridEX1.RootTable.Columns("Status").HasValueList = True
            Me.GridEX1.RootTable.Columns("Status").EditType = Janus.Windows.GridEX.EditType.Combo
            Dim dtStatus As New DataTable
            dtStatus.Columns.Add("Status", GetType(System.String))
            Dim dr As DataRow
            dr = dtStatus.NewRow
            dr(0) = "Pending"
            dtStatus.Rows.Add(dr)
            dr = dtStatus.NewRow
            dr(0) = "Ok"
            dtStatus.Rows.Add(dr)
            Me.GridEX1.RootTable.Columns("Status").ValueList.PopulateValueList(dtStatus.DefaultView, "Status", "Status")
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rdoPending_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoPending.CheckedChanged, rdoOk.CheckedChanged, rdoAll.CheckedChanged
        Try

            If Me.rdoPending.Checked = True Then
                Status = "Pending"
            ElseIf Me.rdoOk.Checked = True Then
                Status = "Ok"
            ElseIf Me.rdoAll.Checked = True Then
                Status = "All"
            End If
            FillBind()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try
            FillBind()
            Me.GridEX1.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.GridEX1.RowCount = Nothing Then
                ShowErrorMessage("Please generate record")
                Exit Sub
            End If
            DailySupplyDetail = New List(Of DailySupplyAndGatePass)
            Dim DailySupply As DailySupplyAndGatePass
            Dim dt As DataTable = CType(Me.GridEX1.DataSource, DataTable)
            For Each r As DataRow In dt.GetChanges.Rows
                DailySupply = New DailySupplyAndGatePass
                DailySupply.BiltyNo = r("BiltyNo").ToString
                DailySupply.DeliveryDate = r("SalesDate")
                DailySupply.SalesNo = r("SalesNo").ToString
                DailySupply.Status = r("Status").ToString
                DailySupply.UserName = LoginUserName
                DailySupply.EntryDate = Date.Now
                DailySupplyDetail.Add(DailySupply)
            Next
            If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
            Call New DailySupplyAndGatePassDAL().Add(DailySupplyDetail)
            msg_Information(str_informSave)
            FillBind()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Daily Supply" & Chr(10) & "From Date: " & dtpFromDate.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpToDate.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFromDate.Value = Date.Today
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-1)
            Me.dtpToDate.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpToDate.Value = Date.Today
        End If
    End Sub
End Class
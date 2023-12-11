Imports System.Data.OleDb
Public Class frmDefTaxSlabs
    Dim CurrentId As Integer
    Dim IsLoadedForm As Boolean = False
    Dim dt As DataTable
    Dim IsEditMode As Boolean = False
    Sub ClearDetailControls()
        Try
            'Me.cmbTaxType.SelectedIndex = 0
            Me.TxtValueFrom.Text = String.Empty
            Me.TxtValueTo.Text = String.Empty
            Me.TxtTaxPer.Text = String.Empty
            Me.TxtFixed.Text = String.Empty
            Me.TxtPerMonth.Text = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub RefreshControls()
        Try
            Me.BtnSave.Text = "&Save"
            Me.dtpFromDate.Value = Date.Now
            Me.DtpToDate.Value = Date.Now
            Me.cmbTaxType.SelectedIndex = 0
            Me.TxtValueFrom.Text = String.Empty
            Me.TxtValueTo.Text = String.Empty
            Me.TxtTaxPer.Text = String.Empty
            Me.TxtFixed.Text = String.Empty
            Me.TxtPerMonth.Text = String.Empty
            Me.GetSecurityRights()
            Me.BindGrid()
            Me.GrdData.DataSource = Nothing
            IsEditMode = False
            Me.cmbTaxType.Enabled = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Sub BindGrid()
        Try
            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim str As String = String.Empty
            str = "SELECT slabid,FromDate,ToDate,taxType as 'Tax Type'  from tblDEFTAXSLAB group by slabid,FromDate,ToDate,taxType order by slabid desc"
            adp = New OleDbDataAdapter(str, Con)
            adp.Fill(dt)

            Me.GrdHistory.DataSource = dt
            Me.GrdHistory.RetrieveStructure()
            Me.GrdHistory.RootTable.Columns(0).Visible = False
            Me.GrdHistory.RootTable.Columns("FromDate").Caption = "From Date"
            Me.GrdHistory.RootTable.Columns("ToDate").Caption = "To Date"
            Me.GrdHistory.RootTable.Columns("FromDate").FormatString = "dd/MMMM/yyyy"
            Me.GrdHistory.RootTable.Columns("ToDate").FormatString = "dd/MMMM/yyyy"
            Me.GrdHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
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
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Function ValidateForm() As Boolean
        'Try
        '    Dim dt As New DataTable
        '    Dim str As String = "Select * from tblDEFTAXSLAB"
        '    dt = GetDataTable(str)
        '    dt.AcceptChanges()

        '    Dim StartValue As DateTime
        '    Dim EndValue As DateTime
        '    Dim ValueFrom As DateTime = dtpFromDate.Value
        '    Dim Valueto As DateTime = DtpToDate.Value

        '    For a As Integer = 0 To dt.Rows.Count - 1
        '        StartValue.Date = CDate(dt.Rows(a).Item("ValueFrom").ToString("yyyy-M-d"))
        '        EndValue.Date = CDate(dt.Rows(a).Item("ValueTo").ToString("yyyy-M-d"))
        '        If (ValueFrom > StartValue And ValueFrom < EndValue) Then

        '            msg_Error("From Value already exists in Slab")
        '            TxtValueFrom.Focus() : ValidateForm = False : Exit Function
        '        End If

        '        If (Valueto > StartValue And Valueto < EndValue) Then
        '            msg_Error("To Value already exists in Slab")
        '            TxtValueTo.Focus() : ValidateForm = False : Exit Function
        '        End If


        '    Next


        '    'For Each row As DataRow In dt.Rows
        '    '    StartValue = row("FromDate").ToString("yyyy-M-d")
        '    '    StartValue = row("FromDate").ToString("yyyy-M-d")
        '    'Next
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Function
    Private Function Validate_AddToGrid() As Boolean
        Try
            If cmbTaxType.SelectedIndex = 0 Then
                msg_Error("Please select tax type")
                cmbTaxType.Focus() : Validate_AddToGrid = False : Exit Function
            End If


            If Val(TxtValueFrom.Text) <= 0 Then
                msg_Error("Value from is not greater than 0")
                TxtValueFrom.Focus() : Validate_AddToGrid = False : Exit Function
            End If


            If Val(TxtValueTo.Text) <= 0 Then
                msg_Error("Value from is not greater than 0")
                TxtValueTo.Focus() : Validate_AddToGrid = False : Exit Function
            End If


            If Val(TxtValueTo.Text) <= Val(TxtValueFrom.Text) Then
                msg_Error("Value to slab should be greater than value from slab")
                TxtValueTo.Focus() : Validate_AddToGrid = False : Exit Function
            End If


            If Val(TxtValueFrom.Text) <= 0 Then
                msg_Error("Value from is not greater than 0")
                TxtValueFrom.Focus() : Validate_AddToGrid = False : Exit Function
            End If

            If dtpFromDate.Value > DtpToDate.Value Then
                msg_Error("To date  is not greater than from date")
                dtpFromDate.Focus() : Validate_AddToGrid = False : Exit Function
            End If


            Dim StartValue As Integer = 0I
            Dim EndValue As Integer = 0I
            Dim ValueFrom As Integer = Val(Me.TxtValueFrom.Text)
            Dim Valueto As Integer = Val(Me.TxtValueTo.Text)

            For a As Integer = 0 To GrdData.RowCount - 1
                StartValue = Val(GrdData.GetRow(a).Cells("ValueFrom").Value.ToString)
                EndValue = Val(GrdData.GetRow(a).Cells("ValueTo").Value.ToString)
                If (ValueFrom > StartValue And ValueFrom < EndValue) Then
                    msg_Error("From Value already exists in Slab")
                    TxtValueFrom.Focus() : Validate_AddToGrid = False : Exit Function
                End If

                If (Valueto > StartValue And Valueto < EndValue) Then
                    msg_Error("To Value already exists in Slab")
                    TxtValueTo.Focus() : Validate_AddToGrid = False : Exit Function
                End If



            Next



            Validate_AddToGrid = True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If Validate_AddToGrid() Then
                AddItemToGrid()
                ClearDetailControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub AddItemToGrid()
        Try
            Dim dr As DataRow

            GetApplicableAmount()
            PerMonthValue()
            Dim dt As DataTable

            dt = CType(GrdData.DataSource, DataTable)
            If dt Is Nothing Then
                dt = New DataTable
                dt.Columns.Add("TaxType")
                dt.Columns.Add("ValueFrom")
                dt.Columns.Add("ValueTo")
                dt.Columns.Add("ApplicableAmount")
                dt.Columns.Add("TaxPer")
                dt.Columns.Add("Fixed")
                dt.Columns.Add("ValuePerMonth")
            End If
            
            dr = dt.NewRow
            dr.Item("TaxType") = Me.cmbTaxType.Text
            dr.Item("ValueFrom") = Val(TxtValueFrom.Text)
            dr.Item("ValueTo") = Val(TxtValueTo.Text)
            dr.Item("ApplicableAmount") = Val(TxtApplicableValue.Text)
            dr.Item("TaxPer") = Val(TxtTaxPer.Text)
            dr.Item("Fixed") = Val(TxtFixed.Text)
            dr.Item("ValuePerMonth") = Val(TxtPerMonth.Text)
            dt.Rows.Add(dr)
            dt.AcceptChanges()
            Me.GrdData.DataSource = dt

            Me.cmbTaxType.Enabled = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TxtValueFrom_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtValueFrom.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub TxtFixed_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFixed.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub TxtPerMonth_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtPerMonth.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub TxtTaxPer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtTaxPer.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub TxtValueTo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtValueTo.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Me.GrdData.UpdateData()
            If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                Save()
                RefreshControls()

            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub

                Update_Record()

            End If
            lblProgress.Visible = False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Function Save() As Boolean
        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer

        objCon = Con
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            Dim serialno As Integer = GetSerialNo()
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            For i = 0 To GrdData.RowCount - 1
                objCommand.CommandText = "Insert into tblDEFTAXSLAB (slabid,FromDate,ToDate,TaxType,ValueFrom,ValueTo,ApllicableAmount,TaxPer,Fixed,PerMonthVaue) values( " & Val(serialno) & ", '" & Me.dtpFromDate.Value.Date.ToString & "','" & Me.DtpToDate.Value.Date.ToString & "','" & Me.GrdData.GetRows(i).Cells("TaxType").Value.ToString & "', " & Val(Me.GrdData.GetRows(i).Cells("ValueFrom").Value.ToString) & "," & Val(Me.GrdData.GetRows(i).Cells("ValueTo").Value.ToString) & "," & Val(Me.GrdData.GetRows(i).Cells("ApplicableAmount").Value.ToString) & "," & Val(Me.GrdData.GetRows(i).Cells("TaxPer").Value.ToString) & "," & Val(Me.GrdData.GetRows(i).Cells("Fixed").Value.ToString) & "," & Val(Me.GrdData.GetRows(i).Cells("ValuePerMonth").Value.ToString) & " ) "
                objCommand.ExecuteNonQuery()
            Next
            trans.Commit()

        Catch EX As Exception
            Throw EX
        End Try
    End Function
    Private Sub Update_Record()
        If Not GrdHistory.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        Try
            If Delete() = True Then
                Save()
                RefreshControls()
            Else
                Exit Sub
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Function Delete() As Boolean

        If Con.State = ConnectionState.Closed Then Con.Open()
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Dim cmd As New OleDbCommand

        cmd.Connection = Con


        Dim trans As OleDbTransaction = Con.BeginTransaction
        Try
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans
            cmd.CommandText = "delete from tblDEFTAXSLAB where slabid = " & Me.GrdHistory.CurrentRow.Cells("SlabId").Value & ""
            cmd.ExecuteNonQuery()
            trans.Commit()
            Return True
        Catch EX As Exception
            trans.Rollback()
            Throw EX
        Finally
            Con.Close()
        End Try
    End Function

    Private Sub frmDefTaxSlabs_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                BtnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                BtnNew_click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.F5 Then
                'BtnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Insert Then
                btnAdd_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDefTaxSlabs_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            RefreshControls()
            Me.lblProgress.Visible = False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Sub EditRecord()
        Try
           dtpFromDate.Value = GrdHistory.CurrentRow.Cells("FromDate").Value.ToString
            DtpToDate.Value = GrdHistory.CurrentRow.Cells("ToDate").Value.ToString
            Call DisplayDetail(GrdHistory.CurrentRow.Cells("SlabId").Value)
            Me.cmbTaxType.Text = GrdHistory.CurrentRow.Cells("Tax Type").Value.ToString
            Me.cmbTaxType.Enabled = False

            Me.BtnSave.Text = "&Update"
            Me.GetSecurityRights()
            Me.BtnDelete.Visible = True
            Me.BtnDelete.Enabled = True
            Me.BtnPrint.Visible = True
            IsEditMode = False
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            '''''''''''''''''''''''''''
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DisplayDetail(ByVal SlabId As Integer)
        Try
            Dim str As String

            str = "SELECT TaxType, ValueFrom,ValueTo,ApllicableAmount as ApplicableAmount ,TaxPer,Fixed,PerMonthVaue as ValuePerMonth from tblDEFTAXSLAB where slabid = " & SlabId & ""
            Dim dtDisplayDetail As DataTable = GetDataTable(str)
            dtDisplayDetail.AcceptChanges()
            Me.GrdData.DataSource = dtDisplayDetail


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GrdHistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdHistory.DoubleClick
        Try
            If Me.GrdHistory.Row < 0 Then
                Exit Sub
            Else
                EditRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not GrdHistory.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If

        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() Then
                RefreshControls()
            Else
                Exit Sub
            End If
            lblProgress.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try

    End Sub

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            RefreshControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetApplicableAmount()
        Try
            If Val(TxtValueFrom.Text) > 0 And Val(TxtValueTo.Text) > 0 Then
                TxtApplicableValue.Text = Val(TxtValueTo.Text) - Val(TxtValueFrom.Text)
            Else
                TxtApplicableValue.Text = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PerMonthValue()
        Try
            If Val(TxtApplicableValue.Text) > 0 Then
                TxtPerMonth.Text = Math.Round(Val(TxtApplicableValue.Text) / 12, 0)
            Else
                TxtPerMonth.Text = 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TxtValueFrom_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtValueFrom.LostFocus
        Try
            GetApplicableAmount()
            PerMonthValue()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TxtValueFrom_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtValueFrom.TextChanged
        Try
            GetApplicableAmount()
            PerMonthValue()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TxtValueTo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtValueTo.LostFocus
        Try
            GetApplicableAmount()
            PerMonthValue()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub TxtValueTo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtValueTo.TextChanged

    End Sub

    Private Sub TxtTaxPer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtTaxPer.TextChanged

    End Sub

    Private Sub TxtFixed_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFixed.LostFocus
        Try
            GetApplicableAmount()
            PerMonthValue()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GrdData_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GrdData.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.GrdData.GetRow.Delete()
                GrdData.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If e.Tab.Index = 1 Then
                BindGrid()
                Me.BtnDelete.Visible = True
            Else
                If IsEditMode = False Then Me.BtnDelete.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetSerialNo() As String
        Try
            Dim Serial As Integer = 0

            Dim str As String
            str = "Select ISNULL(slabid,0) as Serial From tblDEFTAXSLAB "
            Dim dt As DataTable = GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count = 0 Then
                    Serial = 0
                Else
                    Serial = CInt(dt.Rows(0).Item(0))
                End If
            End If
            Serial = Serial + 1
            Return Serial
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function

    Private Sub UltraTabPageControl1_Paint(sender As Object, e As PaintEventArgs) Handles UltraTabPageControl1.Paint

    End Sub
End Class
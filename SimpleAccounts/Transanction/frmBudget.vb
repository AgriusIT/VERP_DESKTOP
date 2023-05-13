Imports System.Data.OleDb
Public Class frmBudget

    Dim dt As DataTable
    Dim IsFormOpend As Boolean = False
    Dim IsEditMode As Boolean = False

    Private Sub frmBudget_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            SaveToolStripButton_Click(btnSave, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.Escape Then

            NewToolStripButton_Click(btnnew, Nothing)
            Exit Sub
        End If
    End Sub


    Private Sub frmBudget_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            RefreshControls()
            Me.FillPlNotes()
            'Me.DisplayRecord()
            '//This will hide Master grid
            Me.DisplayRecord()
            Me.grdSaved.Visible = CType(GetConfigValue("ShowMasterGrid"), Boolean)
            Me.IsFormOpend = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub

    Private Sub DisplayRecord()
        Dim str As String

        str = " SELECT  BudgetID, BudgetNo AS [Budget No], BudgetName AS [Budget Name], StartDate AS [Start Date], EndDate AS [End Date], SortOrder AS [Sort Order], " _
                      & " Remarks AS Remarks, UserName AS [User Name]" _
                      & "   FROM BudgetMasterTable" _
                      & " ORDER BY SortOrder "

        FillGridEx(grdSaved, str, True)
        grdSaved.RootTable.Columns(0).Visible = False

        For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdSaved.RootTable.Columns
            'col.ReadOnly = True
            col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            'col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            col.AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DiaplayedCells
        Next
        
        'grdSaved.RowHeadersVisible = False
        grdSaved.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.False
        Me.grdSaved.RootTable.Columns("Start Date").FormatString = str_DisplayDateFormat

    End Sub

    Private Sub RefreshControls()
        Me.btnSave.Text = "&Save"
        If GetConfigValue("VoucherNo").ToString <> "Yearly" Then
            Me.txtBudgetNo.Text = GetNextDocNo("BD", 6, "BudgetMasterTable", "BudgetNo")
        Else
            Me.txtBudgetNo.Text = GetSerialNo("BD" + "-" + Microsoft.VisualBasic.Right(Me.dtpFrom.Value.Year, 2) + "-", "BudgetMasterTable", " BudgetNo")
        End If
        Me.txtBudgetName.Text = String.Empty
        Me.txtSortOrder.Text = 0
        Me.txtRemarks.Text = String.Empty
        Me.IsEditMode = False
        'Me.txtAmount.Text = String.Empty
        Me.txtBudgetName.Focus()
        GetSecurityRights()
    End Sub

    Private Sub ClearDetailControls()

        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            r.BeginEdit()
            r.Cells(3).Value = 0
            r.EndEdit()
        Next

    End Sub

    Private Sub GetTotal()
        Dim i As Integer
        Dim dblTotalAmount As Double
        For i = 0 To grd.RowCount - 1
            dblTotalAmount = dblTotalAmount + Val(grd.GetRows(i).Cells("Budget").Value)
        Next
        'txtAmount.Text = dblTotalAmount
    End Sub



    Private Function Save() As Boolean

        Me.txtBudgetNo.Text = GetNextDocNo("BD", 6, "BudgetMasterTable", "BudgetNo")

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        objCon = Con

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13 
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            objCommand.CommandText = "Insert into BudgetMasterTable (BudgetNo,BudgetName,StartDate,EndDate,SortOrder,Remarks, UserName) values( " _
                                    & "'" & Me.txtBudgetNo.Text & "' , '" & Me.txtBudgetName.Text.ToString.Replace("'", "''") & "','" & Me.dtpFrom.Value.ToString("yyyy-M-d h:mm:ss tt") & "','" & Me.dtpTo.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & Me.txtSortOrder.Text.Trim & ",'" & txtRemarks.Text & "','" & LoginUserName & "') Select @@Identity"
            Dim MasterID As Integer = objCommand.ExecuteScalar()


            '***********************
            'Inserting Detail Records
            '***********************

            For i = 0 To grd.RowCount - 1
                objCommand.CommandText = "INSERT INTO BudgetDetailTable(BudgetID, GLNoteID, Amount) " _
                                       & " VALUES(" & MasterID & ", " & Me.grd.GetRows(i).Cells(0).Value & ", " & Val(Me.grd.GetRows(i).Cells(3).Value) & ")"

                objCommand.ExecuteNonQuery()
            Next

            trans.Commit()
            Save = True

            SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Budget, Me.txtBudgetNo.Text)

        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        Finally
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13 
            Me.lblProgress.Visible = False
        End Try
    End Function


    Private Function FormValidate() As Boolean

        If Me.txtBudgetName.Text.Trim = String.Empty Then
            msg_Error("You must enter  Budget Name")
            Me.txtBudgetName.Focus()
            FormValidate = False : Exit Function
        ElseIf Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(3), Janus.Windows.GridEX.AggregateFunction.Sum)) = 0 Then
            msg_Error("You must enter budget amount of at least one PL Note")
            Me.grd.Focus()
            FormValidate = False : Exit Function
        End If

        Return True

    End Function

    Sub EditRecord()

        If Not Me.grdSaved.RowCount > 0 Then Exit Sub

        Me.IsEditMode = True

        txtBudgetNo.Text = grdSaved.CurrentRow.Cells("Budget No").Value.ToString
        Me.txtBudgetName.Text = grdSaved.CurrentRow.Cells("Budget Name").Value.ToString
        dtpFrom.Value = CType(grdSaved.CurrentRow.Cells("Start Date").Value, Date)
        dtpTo.Value = CType(grdSaved.CurrentRow.Cells("End Date").Value, Date)
        txtBudgetID.Text = grdSaved.CurrentRow.Cells("BudgetID").Value
        Me.txtSortOrder.Text = grdSaved.CurrentRow.Cells("Sort Order").Value
        Me.txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value
        Me.DisplayDetail(Me.txtBudgetID.Text)
        Me.btnSave.Text = "&Update"
        Me.GetSecurityRights()
        Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
    End Sub

    Private Sub DisplayDetail(ByVal BudgetID As Integer)
        Dim str As String
        Dim i As Integer = 0

        str = "SELECT     GLNoteID, Amount FROM BudgetDetailTable   WHERE BudgetID = " & BudgetID & ""

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet

        objCon = Con 'New SqlConnection("Password=sa;Integrated Security Info=False;User ID=sa;Initial Catalog=SimplePos;Data Source=MKhalid")

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon
        objCommand.CommandType = CommandType.Text


        objCommand.CommandText = str

        objDataAdapter.SelectCommand = objCommand
        objDataAdapter.Fill(objDataSet)

        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            For Each dr As DataRow In objDataSet.Tables(0).Rows
                If r.Cells(0).Value = dr.Item(0) Then
                    r.BeginEdit()
                    r.Cells("Budget Amount").Value = dr.Item(1)
                    r.EndEdit()
                End If
            Next
        Next
        Me.GetTotal()
    End Sub

    Private Function Update_Record() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        Dim i As Integer
        objCon = Con

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text


            objCommand.Transaction = trans
            objCommand.CommandText = "update BudgetMasterTable set BudgetName = '" & Me.txtBudgetName.Text & "' , " _
                                     & " StartDate = '" & Me.dtpFrom.Value.ToString("yyyy-M-d h:mm:ss tt") & "' ," _
                                     & " EndDate = '" & Me.dtpTo.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," _
                                     & " SortOrder = " & Me.txtSortOrder.Text.Trim & "," _
                                     & " Remarks = '" & txtRemarks.Text & "' ," _
                                     & " UserName = '" & LoginUserName & "'" _
                                     & " Where BudgetID = " & Me.txtBudgetID.Text
            objCommand.ExecuteNonQuery()

            'delete detail
            objCommand.CommandText = "Delete  BudgetDetailTable where BudgetID = " & Me.txtBudgetID.Text
            objCommand.ExecuteNonQuery()


            '***********************
            'Inserting Detail Records
            '***********************

            For i = 0 To grd.RowCount - 1
                objCommand.CommandText = "INSERT INTO BudgetDetailTable(BudgetID, GLNoteID, Amount) " _
                                       & " VALUES(" & Me.txtBudgetID.Text & ", " & Me.grd.GetRows(i).Cells(0).Value & ", " & Me.grd.GetRows(i).Cells(3).Value & ")"

                objCommand.ExecuteNonQuery()
            Next

            trans.Commit()
            Update_Record = True

            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Budget, Me.txtBudgetNo.Text)

        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
            ShowErrorMessage("An error occured while updating record" & ex.Message)
        End Try


    End Function

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If FormValidate() Then
            If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                If Save() Then
                    'msg_Information(str_informSave)
                    RefreshControls()
                    ClearDetailControls()
                    'DisplayRecord()
                Else
                    Exit Sub 'MsgBox("Record has not been added")

                End If
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                If Update_Record() Then
                    'msg_Information(str_informUpdate)
                    RefreshControls()
                    ClearDetailControls()
                    'DisplayRecord()
                Else
                    Exit Sub 'MsgBox("Record has not been updated")

                End If
            End If

        End If
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click

        If Me.grd.RowCount > 0 Then
            If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
        End If

        RefreshControls()
        Me.ClearDetailControls()
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        EditRecord()
    End Sub

    Private Sub grdSaved_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        EditRecord()
    End Sub


    Private Sub grd_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        With Me.grd.CurrentRow
            GetTotal()
        End With
    End Sub

    
    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Not Me.grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

        Try

            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13 
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim cm As New OleDbCommand
            Dim objTrans As OleDbTransaction
            If Con.State = ConnectionState.Closed Then Con.Open()

            objTrans = Con.BeginTransaction
            cm.Connection = Con
            cm.CommandText = "delete from BudgetDetailTable where BudgetID=" & Me.grdSaved.CurrentRow.Cells(0).Value.ToString
            cm.Transaction = objTrans
            cm.ExecuteNonQuery()

            cm = New OleDbCommand
            cm.Connection = Con
            cm.CommandText = "delete from BudgetMasterTable where BudgetID=" & Me.grdSaved.CurrentRow.Cells(0).Value.ToString

            cm.Transaction = objTrans
            cm.ExecuteNonQuery()



            objTrans.Commit()
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13 
            'msg_Information(str_informDelete)
            Me.txtBudgetID.Text = 0

            SaveActivityLog("POS", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Sales, Me.grdSaved.CurrentRow.Cells(0).Value, True)


        Catch ex As Exception
            msg_Error("Error occured while deleting record " & ex.Message)

        Finally
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13 
            Me.lblProgress.Visible = False

            Con.Close()
        End Try
        Me.DisplayRecord() 'Task 2389 Ehtisham ul Haq, reload history after delete record 21-1-14
        Me.RefreshControls()
        Me.ClearDetailControls()



    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If Me.grdSaved.RowCount = 0 Then Exit Sub
        AddRptParam("@BudgetID", Me.grdSaved.CurrentRow.Cells(0).Value)
        AddRptParam("prm_FromDate", Me.grdSaved.CurrentRow.Cells("Start Date").Value)
        AddRptParam("prm_ToDate", Me.grdSaved.CurrentRow.Cells("End Date").Value)
        AddRptParam("prm_Budget", Me.grdSaved.CurrentRow.Cells("Budget Name").Value)
        ShowReport("rptBudgetComparison", , , , False)
    End Sub

    Private Sub grd_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs)
        Me.GetTotal()
    End Sub


    Private Sub PrintListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frmPrintList As New frmPrintInvoice
        frmPrintList.ShowDialog()
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
                Dim dt As DataTable = GetFormRights(EnumForms.frmBudget)
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


    Private Sub FillPlNotes()
        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = Con
            cmd.CommandType = CommandType.Text
            cmd.CommandText = " SELECT     Gl_note_id, note_no as [Note No], note_title as [Note Title],0 as [Budget Amount] " _
                            & " FROM tblDefGLNotes" _
                            & " WHERE     (note_type = 'PL') " _
                            & " ORDER BY sort_order"
            Dim da As New OleDbDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            grd.RootTable.Columns(0).Visible = False
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                'col.ReadOnly = True
                If Not col.Index = 3 Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
                'col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                col.AutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DiaplayedCells

            Next
            Me.grd.RootTable.Columns(3).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grd.RootTable.Columns(3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                DisplayRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
End Class

Public Class frmYearlySaleTarget

    Private Sub txtYear_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtYear.KeyDown
        If e.KeyCode <> Keys.Back Then
            Dim iNum As String = "0123456789"
            If InStr(1, iNum, Chr(e.KeyCode)) = 0 Then e.Handled = True
        End If
    End Sub

    Private Sub BtnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoad.Click
        Try
            Dim objadp As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter("SELECT Month(TargetMonth) TMonth,SUM(MonthQty) Qty,SUM(MonthValue) Value FROM SaleTargetTable WHERE YEAR(TargetMonth) = '" & Me.txtYear.Text & "' And Salesman_ID = 0 GROUP BY Month(TargetMonth) ORDER BY Month(TargetMonth)", Con)
            Dim dt As New DataTable
            Me.grdTarget.ClearItems()
            objadp.Fill(dt)
            If dt.Rows.Count = 0 Then
                For j As Integer = 1 To 12
                    grdTarget.AddItem(j, Microsoft.VisualBasic.MonthName(j), 0, 0)
                Next
                Me.BtnSave.Text = "&Save"
            Else
                For Each dr As DataRow In dt.Rows
                    grdTarget.AddItem(dr("TMonth").ToString, Microsoft.VisualBasic.MonthName(dr("TMonth")), dr("Qty").ToString, Math.Round(dr("Value"), 0))
                Next
                Me.BtnSave.Text = "&Update"
            End If
            Me.grdTarget.RootTable.Columns(1).EditType = Janus.Windows.GridEX.EditType.NoEdit
            GetSecurityRights()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmYearlySaleTarget_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
     


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub frmYearlySaleTarget_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtYear.Text = Now.Date.Year
        BtnLoad_Click(sender, EventArgs.Empty)
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim objCommand As New OleDb.OleDbCommand
        Dim objCon As OleDb.OleDbConnection

        objCon = Con

        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
        objCommand.CommandType = CommandType.Text
        objCommand.Transaction = trans
        Try

            objCommand.CommandText = "SELECT Month(TargetMonth) TMonth,SUM(MonthQty) Qty,SUM(MonthValue) Value FROM SaleTargetTable WHERE YEAR(TargetMonth) = '" & Me.txtYear.Text & "' And Salesman_ID = 0 GROUP BY Month(TargetMonth) ORDER BY Month(TargetMonth)"
            Dim objdr As OleDb.OleDbDataReader = objCommand.ExecuteReader

            If objdr.HasRows = True Then
                objdr.Close()

                objCommand.CommandText = "Delete from SaleTargetTable Where Year(TargetMonth) = '" & Me.txtYear.Text.ToString.Replace("'", "''") & "' And Salesman_ID = 0"
                objCommand.ExecuteNonQuery()
            Else
                objdr.Close()
            End If

            Dim perDayQty As Double = 0
            Dim perDayValue As Double = 0
            Dim MonthDays As Integer
            Dim varDate As Date

            For Each dr As Janus.Windows.GridEX.GridEXRow In grdTarget.GetRows
                MonthDays = Date.DaysInMonth(Me.txtYear.Text, dr.Cells(0).Value)
                perDayQty = Math.Round(Val(dr.Cells(2).Value) / MonthDays, 0)
                perDayValue = Math.Round(Val(dr.Cells(3).Value) / MonthDays, 2)
                For Days As Integer = 1 To MonthDays
                    varDate = Convert.ToDateTime(dr.Cells(0).Value & "/" & Days & "/" & Me.txtYear.Text)
                    'varDate = CDate(Days & "/" & dr.Cells(0).Value & "/" & Me.txtYear.Text)
                    objCommand.CommandText = "Insert Into SaleTargetTable (TargetMonth,MonthValue,MonthQty,SalesMan_ID) Values ('" & Format(varDate, "yyyy-MM-dd") & _
                        "'," & perDayValue & "," & perDayQty & ",0)"

                    objCommand.ExecuteNonQuery()
                Next
            Next
            trans.Commit()
            'Me.Close()
        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                ' Me.btnDelete.Enabled = True
                'Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        'Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
                'Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.BtnDelete.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Print" Then
                        '    Me.BtnPrint.Enabled = True
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
End Class
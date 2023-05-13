'' 25-11-2015 TASKTFS34 Muhammad Ameen: Print on Production Plan Status is not functional and also need to apply filter and aggregate function on Detail Grid.
Public Class frmProductionPlanStatus
    Private _dt As New DataTable
    Private Sub frmProductionPlanStatus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 8 -1-14
        If e.KeyCode = Keys.F4 Then
            SaveToolStripButton_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmProductionPlanStatus_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.DateTimePicker1.Value = Now.AddMonths(-1)
            Me.DateTimePicker2.Value = Now
            If Me.SplitContainer1.Panel2Collapsed = False Then
                Me.SplitContainer1.Panel2Collapsed = True
            End If
            FillCombo()
            Me.cmbStatus.SelectedIndex = 0
            FillGrid("Plan")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub frmProductionPlanStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            Dim strStatus() As String = System.Enum.GetNames(GetType(EnumStatus))
            Me.cmbStatus.Items.Clear()
            For Each sts As String In strStatus
                ' If sts <> Me.cmbStatus.SelectedItem.ToString Then
                Me.cmbStatus.Items.Add(sts)
                'End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStatus.SelectedIndexChanged
        Try


            cmbSetTo.Items.Clear()
            Dim strStatus() As String = System.Enum.GetNames(GetType(EnumStatus))
            For Each sts As String In strStatus
                'If sts <> EnumStatus.Close.ToString AndAlso sts <> Me.cmbStatus.SelectedItem.ToString Then
                '    cmbSetTo.Items.Add(sts)
                'End If

                If sts <> Me.cmbStatus.SelectedItem.ToString AndAlso sts <> EnumStatus.All.ToString Then
                    cmbSetTo.Items.Add(sts)
                End If
            Next
            If cmbSetTo.Items.Count > 0 Then
                cmbSetTo.SelectedIndex = 0
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.SelectionChanged
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            If Me.SplitContainer1.Panel2Collapsed = True Then
                Me.SplitContainer1.Panel2Collapsed = False
            End If
            FillGrid("Issuance")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Sub FillGrid(Optional ByVal Condition As String = "")
        Try

            _dt.Clear()
            _dt.Dispose()

            If Condition = "Plan" Then
                Dim strSQL As String = "SELECT  PlanId, PlanNo, PlanDate, Remarks, Status FROM dbo.PlanMasterTable WHERE PlanNo <> '' " & IIf(Me.cmbStatus.Text = "All", "", " AND Status='" & Me.cmbStatus.Text & "'") & " " & IIf(Me.DateTimePicker1.Checked = True, " AND (Convert(Varchar, PlanDate, 102) >= Convert(DateTime, '" & Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "',102))", "") & IIf(Me.DateTimePicker2.Checked = True, " AND (Convert(Varchar, PlanDate, 102) <= Convert(DateTime, '" & Me.DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "',102))", "") & ""
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                Me.grd.DataSource = dt
                Me.grd.AutoSizeColumns()
            ElseIf Condition = "Issuance" Then
                Dim dtData As New DataTable
                Dim strSQL1 As String = String.Empty
                Dim PlanId As Integer = -1
                If Not Me.grd.RowCount = 0 Then
                    PlanId = Me.grd.GetRow.Cells("PlanId").Value
                    strSQL1 = "SELECT dbo.PlanMasterTable.PlanNo, dbo.PlanMasterTable.PlanDate, dbo.ArticleDefView.ArticleCode AS Code, dbo.ArticleDefView.ArticleDescription AS Article, " _
                  & "  dbo.PlanCostSheetDetailTable.Price, dbo.PlanCostSheetDetailTable.CurrentPrice, ISNULL(dbo.PlanCostSheetDetailTable.PlanQty, 0) AS PlanQty,   " _
                  & "  ISNULL(dbo.PlanCostSheetDetailTable.IssuedQty, 0) AS IssuedQty, ISNULL(dbo.PlanCostSheetDetailTable.PlanQty, 0)  " _
                  & "  - ISNULL(dbo.PlanCostSheetDetailTable.IssuedQty, 0) AS Diff " _
                  & "  FROM  dbo.PlanCostSheetDetailTable INNER JOIN " _
                  & "  dbo.PlanMasterTable ON dbo.PlanCostSheetDetailTable.PlanId = dbo.PlanMasterTable.PlanId INNER JOIN " _
                  & "  dbo.ArticleDefView ON dbo.PlanCostSheetDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE PlanCostSheetDetailTable.PlanId=" & PlanId & " AND ISNULL(dbo.PlanCostSheetDetailTable.PlanQty, 0) <> 0"
                    dtData = GetDataTable(strSQL1)
                    Me.grdDetail.DataSource = dtData
                    Me.grdDetail.RetrieveStructure()
                   
                    ''TASKTFS34
                    Me.grdDetail.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("CurrentPrice").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("PlanQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("IssuedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("Diff").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

                    'Me.grdDetail.RootTable.Columns("Price").LineAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    'Me.grdDetail.RootTable.Columns("CurrentPrice").LineAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    'Me.grdDetail.RootTable.Columns("PlanQty").LineAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    'Me.grdDetail.RootTable.Columns("IssuedQty").LineAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    'Me.grdDetail.RootTable.Columns("Diff").LineAlignment = Janus.Windows.GridEX.TextAlignment.Far

                    Me.grdDetail.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("CurrentPrice").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("PlanQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("IssuedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("Diff").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far




                    'Me.grdDetail.RootTable.Columns("Price").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    'Me.grdDetail.RootTable.Columns("CurrentPrice").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("PlanQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grdDetail.RootTable.Columns("IssuedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grdDetail.RootTable.Columns("Diff").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                    CtrlGrdBar1_Load(Nothing, Nothing)

                    Me.grdDetail.AutoSizeColumns()
                Else
                    strSQL1 = "SELECT dbo.PlanMasterTable.PlanNo, dbo.PlanMasterTable.PlanDate, dbo.ArticleDefView.ArticleCode AS Code, dbo.ArticleDefView.ArticleDescription AS Article, " _
               & "  dbo.PlanCostSheetDetailTable.Price, dbo.PlanCostSheetDetailTable.CurrentPrice, ISNULL(dbo.PlanCostSheetDetailTable.PlanQty, 0) AS PlanQty,   " _
               & "  ISNULL(dbo.PlanCostSheetDetailTable.IssuedQty, 0) AS IssuedQty, ISNULL(dbo.PlanCostSheetDetailTable.PlanQty, 0)  " _
               & "  - ISNULL(dbo.PlanCostSheetDetailTable.IssuedQty, 0) AS Diff " _
               & "  FROM  dbo.PlanCostSheetDetailTable INNER JOIN " _
               & "  dbo.PlanMasterTable ON dbo.PlanCostSheetDetailTable.PlanId = dbo.PlanMasterTable.PlanId INNER JOIN " _
               & "  dbo.ArticleDefView ON dbo.PlanCostSheetDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE PlanCostSheetDetailTable.PlanId=" & -1 & ""
                    dtData = GetDataTable(strSQL1)
                    Me.grdDetail.DataSource = dtData
                    Me.grdDetail.RetrieveStructure()
                    ''TASKTFS34
                    Me.grdDetail.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("CurrentPrice").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("PlanQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("IssuedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("Diff").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

                    'Me.grdDetail.RootTable.Columns("Price").LineAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    'Me.grdDetail.RootTable.Columns("CurrentPrice").LineAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    'Me.grdDetail.RootTable.Columns("PlanQty").LineAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    'Me.grdDetail.RootTable.Columns("IssuedQty").LineAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    'Me.grdDetail.RootTable.Columns("Diff").LineAlignment = Janus.Windows.GridEX.TextAlignment.Far

                    Me.grdDetail.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("CurrentPrice").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("PlanQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("IssuedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("Diff").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far




                    'Me.grdDetail.RootTable.Columns("Price").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    'Me.grdDetail.RootTable.Columns("CurrentPrice").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grdDetail.RootTable.Columns("PlanQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grdDetail.RootTable.Columns("IssuedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grdDetail.RootTable.Columns("Diff").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                    CtrlGrdBar1_Load(Nothing, Nothing)
                    Me.grdDetail.AutoSizeColumns()
                    Exit Sub
                End If
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            FillGrid("Plan")
            FillGrid("Issuance")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
        'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
        If Con.State = ConnectionState.Closed Then Con.Open()
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand
        Try

            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
                cmd.CommandText = ""
                cmd.CommandText = "UPDATE PlanMasterTable SET Status='" & Me.cmbSetTo.Text & "' WHERE PlanId=" & r.Cells("PlanId").Value & ""
                cmd.ExecuteNonQuery()
            Next
            trans.Commit()
            'msg_Information(str_informSave)
            FillGrid("Plan")
            FillGrid("Issuance")
        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        Try
            ''TASKTFS34
            Dim bs As New BindingSource
            bs.DataSource = grdDetail.DataSource

            _dt = bs.DataSource
            ShowReport("rptPlanStatus", "Nothing", "Nothing", "Nothing", False, , , _dt)
            '_dt.Clear()
            '_dt.Dispose()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow

    End Sub

    Private Sub grdDetail_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdDetail.FormattingRow

    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdDetail.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdDetail.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = "Production Plan Status" & Chr(10) & CompanyTitle & Chr(10) & "Date From: " & Me.DateTimePicker1.Value.ToString("yyyy-M-d") & " Date To: " & Me.DateTimePicker2.Value.ToString("yyyy-M-d") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
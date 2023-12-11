Imports SBModel
Public Class rptGridItemWiseLC
    Public Sub GetAll()
        Dim DT As New DataTable
        Try
            DT = GetDataTable("Execute SP_AllItemWiseLC")
            DT.AcceptChanges()
            Me.GridEX1.DataSource = DT
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns("LCID").Visible = False
            Me.GridEX1.RootTable.Columns("ArticleDefId").Visible = False
            Me.GridEX1.RootTable.Columns("CostCenterId").Visible = False
            Me.GridEX1.RootTable.Columns("Article Description").Visible = False
            Me.GridEX1.RootTable.Columns("LC No").ColumnType = Janus.Windows.GridEX.ColumnType.Link
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetDateWise()
        Dim DT As New DataTable
        Try
            DT = GetDataTable("Execute SP_ItemWiseLC '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "'")
            DT.AcceptChanges()
            Me.GridEX1.DataSource = DT
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns("LCID").Visible = False
            Me.GridEX1.RootTable.Columns("ArticleDefId").Visible = False
            Me.GridEX1.RootTable.Columns("CostCenterId").Visible = False
            Me.GridEX1.RootTable.Columns("Article Description").Visible = True
            Me.GridEX1.RootTable.Columns("Article Description").Width = 200
            Me.GridEX1.RootTable.Columns("Import No").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.GridEX1.RootTable.Columns("LC Document Date").FormatString = str_DisplayDateFormat
            Me.GridEX1.RootTable.Columns("LC Import Date").FormatString = str_DisplayDateFormat
            Me.GridEX1.RootTable.Columns("LC Cost").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("LC Cost").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("LC Cost").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("LC Cost").TotalFormatString = "N" & DecimalPointInValue

            Me.GridEX1.RootTable.Columns("Expense Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Expense Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Expense Amount").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Expense Amount").TotalFormatString = "N" & DecimalPointInValue

            Me.GridEX1.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.GridEX1.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInValue
            Me.GridEX1.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInValue
            Me.GridEX1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Grid Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Grid Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub rptGridItemWiseLC_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            'GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            GetDateWise()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Me.dtpFrom.Value = Now
            Me.dtpTo.Value = Now
            'GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GridEX1_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.LinkClicked
        Try
            If e.Column.Key = "Import No" Then
                'Me.Close()
                frmImport.IsFromItemWiseLC = True
                'frmImport.LoadLCFromItemWiseLC(Me.GridEX1.GetRow.Cells("LCID").Value)
                frmImport.GetLCFromItemWiseLC(Me.GridEX1.GetRow.Cells("LCID").Value)
                frmMain.LoadControl("frmImport")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
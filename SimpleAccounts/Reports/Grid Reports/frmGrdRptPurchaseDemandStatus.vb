
''TFS1769 : AyeshaRehman : 09-01-2018 : Purchase Demand Status right Problem
Public Class frmGrdRptPurchaseDemandStatus

    Public Sub FillGrid()
        Try

            Dim strSQL As String = String.Empty
            ''dbo.PurchaseDemandMasterTable.Status replaced with 'Close', 'Open' TASK-408 on 17-06-2016
            'strSQL = "SELECT dbo.PurchaseDemandMasterTable.PurchaseDemandNo AS [Doc No], dbo.PurchaseDemandMasterTable.PurchaseDemandDate AS [Doc Date], " _
            '          & " dbo.vwCOADetail.sub_sub_code AS [A/H Code], dbo.vwCOADetail.sub_sub_title AS [Account Head], dbo.vwCOADetail.detail_code AS [Account Code],  " _
            '          & " dbo.vwCOADetail.detail_title AS Vendor, dbo.ArticleDefView.ArticleCode AS Code, dbo.ArticleDefView.ArticleDescription AS Item,  " _
            '          & " dbo.ArticleDefView.ArticleSizeName AS Size, dbo.ArticleDefView.ArticleColorName AS Color, dbo.PurchaseDemandDetailTable.ArticleSize AS Unit,  " _
            '          & " dbo.PurchaseDemandDetailTable.Qty AS Demand, dbo.PurchaseDemandDetailTable.DeliveredTotalQty AS [Order], ISNULL(dbo.PurchaseDemandDetailTable.Qty, 0)  " _
            '          & " - ISNULL(dbo.PurchaseDemandDetailTable.DeliveredTotalQty, 0) AS Balance, Case When (ISNULL(dbo.PurchaseDemandDetailTable.Qty, 0) > ISNULL(dbo.PurchaseDemandDetailTable.DeliveredTotalQty, 0)) Then 'Open' Else 'Close' End As Status, dbo.PurchaseDemandMasterTable.Remarks, dbo.PurchaseDemandDetailTable.Comments As Detail  " _
            '          & " FROM         dbo.PurchaseDemandDetailTable INNER JOIN " _
            '          & " dbo.PurchaseDemandMasterTable ON dbo.PurchaseDemandDetailTable.PurchaseDemandId = dbo.PurchaseDemandMasterTable.PurchaseDemandId INNER JOIN " _
            '          & " dbo.ArticleDefView ON dbo.PurchaseDemandDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId LEFT OUTER JOIN " _
            '          & " dbo.vwCOADetail ON dbo.PurchaseDemandMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id " _
            '          & " WHERE (Convert(varchar, dbo.PurchaseDemandMasterTable.PurchaseDemandDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(Datetime,'" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  " & IIf(Me.cmbStatus.SelectedIndex > 0, "  AND dbo.PurchaseDemandMasterTable.Status='" & Me.cmbStatus.Text & "'", "") & " " _
            '          & " ORDER BY dbo.PurchaseDemandMasterTable.PurchaseDemandNo, dbo.PurchaseDemandMasterTable.PurchaseDemandDate, dbo.ArticleDefView.ArticleCode,  " _
            '          & " dbo.ArticleDefView.ArticleDescription "
            '& "  AS Balance, CASE WHEN (ISNULL(dbo.PurchaseDemandDetailTable.Qty, 0) > ISNULL(dbo.PurchaseDemandDetailTable.DeliveredTotalQty, 0)) THEN 'Open' ELSE 'Close' END AS Status, " _
            'To Add Artical Department In GRID by Waqar Raza on 06-10-2016

            strSQL = "SELECT PurchaseDemandMasterTable.PurchaseDemandID,PurchaseDemandMasterTable.PurchaseDemandNo AS [Doc No], PurchaseDemandMasterTable.PurchaseDemandDate AS [Doc Date], vwCOADetail.sub_sub_code AS [A/H Code], " _
                         & "vwCOADetail.sub_sub_title As [Account Head], vwCOADetail.detail_code As [Account Code], vwCOADetail.detail_title As Vendor, ArticleGroupDefTable.ArticleGroupName, ArticleDefView.ArticleCode As Code, " _
                         & " ArticleDefView.ArticleDescription As Item, ArticleDefView.ArticleSizeName As Size, ArticleDefView.ArticleColorName As Color, PurchaseDemandDetailTable.ArticleSize As Unit, " _
                         & " PurchaseDemandDetailTable.Qty As Demand, PurchaseDemandDetailTable.DeliveredTotalQty As [Order], ISNULL(PurchaseDemandDetailTable.Qty, 0) - ISNULL(PurchaseDemandDetailTable.DeliveredTotalQty, 0) " _
                         & " AS Balance, PurchaseDemandMasterTable.Status, " _
                         & " PurchaseDemandMasterTable.Remarks, PurchaseDemandDetailTable.Comments AS Detail " _
                         & " FROM    PurchaseDemandDetailTable INNER JOIN" _
                         & " PurchaseDemandMasterTable ON PurchaseDemandDetailTable.PurchaseDemandId = PurchaseDemandMasterTable.PurchaseDemandId INNER JOIN" _
                         & " ArticleDefView ON PurchaseDemandDetailTable.ArticleDefId = ArticleDefView.ArticleId INNER JOIN" _
                         & "  ArticleGroupDefTable ON ArticleDefView.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId LEFT OUTER JOIN" _
                         & " vwCOADetail ON PurchaseDemandMasterTable.VendorId = vwCOADetail.coa_detail_id" _
                         & " WHERE  (CONVERT(varchar, PurchaseDemandMasterTable.PurchaseDemandDate, 102) BETWEEN CONVERT(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) And " _
                         & " " & IIf(Me.cmbStatus.Text = "All", " PurchaseDemandMasterTable.Status In('Open', 'Close', 'Reject', 'DeActive')", " dbo.PurchaseDemandMasterTable.Status = '" & Me.cmbStatus.SelectedItem.ToString & "' ") & " " _
                         & " ORDER BY [Doc No], [Doc Date], Code, Item "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns("PurchaseDemandID").Visible = False
            Me.grd.RootTable.Columns("Doc Date").FormatString = "dd/MMM/yyyy"

            Me.grd.RootTable.Columns("Demand").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Order").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInQty

            Me.grd.RootTable.Columns("Demand").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Order").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("Balance").TotalFormatString = "N" & DecimalPointInQty


            Me.grd.RootTable.Columns("Demand").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Order").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Demand").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Order").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns.Add("Column1")
            Me.grd.RootTable.Columns("Column1").UseHeaderSelector = True
            Me.grd.RootTable.Columns("Column1").ActAsSelector = True
            'Me.grd.RootTable.Columns("Column1").Position = -1
            'Me.grd.RootTable.Columns("Column1")
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '' start TFS1769
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                'Me.btnDelete.Enabled = True
                'Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptPurchaseDemandStatus)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
                'Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As SBModel.GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
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


    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFrom.Value = Date.Today
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-1)
            Me.dtpTo.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpTo.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpTo.Value = Date.Today
        End If
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try

            If Me.grd.RowCount > 0 Then
                Me.grd.ClearStructure()

            End If

            FillGrid()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Purchase Demand Status" & vbCrLf & "Date From: " & Me.dtpFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptPurchaseDemandStatus_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.cmbPeriod.Text = "Current Month"

            Dim strStatus() As String = System.Enum.GetNames(GetType(EnumStatus))
            Me.cmbStatus.Items.Clear()
            For Each sts As String In strStatus
                Me.cmbStatus.Items.Add(sts)
            Next
            Me.cmbStatus.SelectedIndex = 0
            GetSecurityRights() ''1769

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RefreshControls()
        Try
            Me.dtpFrom.Value = Date.Today.AddDays(-30)
            Me.dtpTo.Value = Date.Today
            Me.cmbStatus.SelectedIndex = 0
            FillGrid()
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
        'GetSecurityRights()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim trans As OleDb.OleDbTransaction
        If Con.State = ConnectionState.Open Then Con.Close()
        Con.Open()
        trans = Con.BeginTransaction
        Me.Cursor = Cursors.WaitCursor
        Try

            Dim cmd As New OleDb.OleDbCommand
            cmd.Connection = Con

            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans

            If Me.grd.RowCount = 0 Then Exit Sub
            If Not Me.IsValidate() Then Exit Sub
            If Not msg_Confirm("Are you sure you want to update the status of selected records") Then Exit Sub
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
                If r.Cells("Column1").Value = True Then
                    cmd.CommandText = "Update PurchaseDemandMasterTable set Status = '" & Me.cmbSetTo.SelectedItem.ToString & "' where PurchaseDemandID = " & r.Cells("PurchaseDemandID").Value & ""
                    cmd.ExecuteNonQuery()
                End If
            Next
            trans.Commit()
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
            ' msg_Information("Records has been updated successfully")

            ''insert Activity Log
            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Purchase, "abc", True)

            Me.FillGrid()
            'Me.GetSecurityRights()
        Catch ex As Exception
            trans.Rollback()
            msg_Error(ex.Message)
        Finally
            Con.Close()
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Private Function IsValidate() As Boolean


        Dim intIsChecked As Integer = 0

        For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
            If r.Cells("Column1").Value = True Then
                r.BeginEdit()
                intIsChecked = 1
                r.EndEdit()
                Exit For
            End If
        Next

        If intIsChecked = 0 Then
            msg_Error("You must select at least one record from grid")
            Me.grd.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub cmbStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbStatus.SelectedIndexChanged
        Try
            Me.cmbSetTo.Items.Clear()
            Dim strStatus() As String = System.Enum.GetNames(GetType(EnumStatus))
            For Each sts As String In strStatus
                If sts <> Me.cmbStatus.SelectedItem.ToString Then
                    Me.cmbSetTo.Items.Add(sts)
                End If
            Next
            If Me.cmbSetTo.Items.Count > 0 Then
                Me.cmbSetTo.SelectedIndex = 0
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        Try
            RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
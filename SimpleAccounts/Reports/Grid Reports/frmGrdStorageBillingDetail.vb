Public Class frmGrdStorageBillingDetail

    Private Sub frmGrdStorageBillingDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmGrdStorageBillingDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillDropDown(Me.cmbLocation, "Select Location_Id, Location_Name From tblDefLocation", False)
            Me.cmbLocation.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try

            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

            Me.grd.RootTable.Columns("LocationId").Visible = False
            Me.grd.RootTable.Columns("Arrival_Date").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("Filling_Date").FormatString = "dd/MMM/yyyy"

            Me.grd.RootTable.Columns("Acc_Dispatch").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("CF").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Recv").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Billing_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("Acc_Dispatch").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("CF").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Recv").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Billing_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Balance").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Rate").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Acc_Dispatch").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("CF").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Recv").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Billing_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Balance").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Days").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Rate").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grd.RootTable.Columns("Bowser_Arrival_No").CellStyle.BackColor = Color.AliceBlue
            Me.grd.RootTable.Columns("Arrival_Date").CellStyle.BackColor = Color.AliceBlue
            Me.grd.RootTable.Columns("Recv").CellStyle.BackColor = Color.AliceBlue
            Me.grd.RootTable.Columns("CF").CellStyle.BackColor = Color.AliceBlue

            Me.grd.RootTable.Columns("Bowser_Issued_No").CellStyle.BackColor = Color.Snow
            Me.grd.RootTable.Columns("Filling_Date").CellStyle.BackColor = Color.Snow
            Me.grd.RootTable.Columns("Acc_Dispatch").CellStyle.BackColor = Color.Snow

            Me.grd.RootTable.Columns("Balance").CellStyle.BackColor = Color.Ivory
            Me.grd.RootTable.Columns("Days").CellStyle.BackColor = Color.Ivory
            Me.grd.RootTable.Columns("Rate").CellStyle.BackColor = Color.Ivory
            Me.grd.RootTable.Columns("Billing_Amount").CellStyle.BackColor = Color.Ivory
            For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If c.DataMember <> "Rate" Then
                    c.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FillGrid()
        Dim cmd As New OleDb.OleDbCommand
        Try
            If Me.cmbLocation.SelectedIndex = -1 Then Exit Sub
            Dim strSQL As String = String.Empty

            'strSQL = "Truncate Table tblStorageBilling  INSERT INTO tblStorageBilling(LocationId, Bowser_Issued_No, Filling_Date,Acc_Dispatch) SELECT a.LocationId, a.Remarks AS Bowser_Arrival_No, a.ReceivingDate AS Arrival_Date, a.Recv " _
            '        & " FROM (SELECT dbo.ReceivingMasterTable.ReceivingNo, dbo.ReceivingMasterTable.Remarks,   " _
            '        & " dbo.ReceivingMasterTable.ReceivingDate, ISNULL(Receiving.Recv, 0) AS Recv, ISNULL(Receiving.LocationId,0) as LocationId  " _
            '        & " FROM dbo.ReceivingMasterTable LEFT OUTER JOIN (SELECT     ReceivingId, LocationId, SUM(ISNULL(Qty, 0)) AS Recv  " _
            '        & " FROM dbo.ReceivingDetailTable GROUP BY ReceivingId,LocationId) AS Receiving ON Receiving.ReceivingId = dbo.ReceivingMasterTable.ReceivingId  WHERE LEFT(ReceivingMasterTAble.ReceivingNo,3) <> 'Pur' AND ReceivingMasterTAble.LocationId=" & Me.cmbLocation.SelectedValue & ")a Order By a.ReceivingDate Asc"

            strSQL = "Truncate Table tblStorageBilling  INSERT INTO tblStorageBilling(LocationId, Bowser_Arrival_No, Arrival_Date,Recv,CF) Select Isnull(disp.LocationId,0) as LocationId, dbo.DispatchMasterTable.Remarks,dbo.DispatchMasterTable.DispatchDate, ISNULL(Disp.Disp, 0) as Disp, 0 as CF  " _
                   & " FROM  dbo.DispatchMasterTable LEFT OUTER JOIN (SELECT DispatchId, LocationId, SUM(ISNULL(Qty, 0)) AS Disp  " _
                   & " FROM  dbo.DispatchDetailTable  WHERE dbo.DispatchDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & "  GROUP BY DispatchId, LocationId) AS Disp ON Disp.DispatchId = dbo.DispatchMasterTable.DispatchId " _
                   & " WHERE  (LEFT(dbo.DispatchMasterTable.DispatchNo, 1) <> 'I') and disp.LocationId <> 0 ORDER BY DispatchDate ASC "

            If Con.State = ConnectionState.Closed Then Con.Open()
            cmd.Connection = Con
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()



            strSQL = String.Empty
            strSQL = "SELECT a.LocationId, a.Remarks AS Bowser_Arrival_No, a.ReceivingDate AS Arrival_Date, a.Recv " _
                    & " FROM (SELECT dbo.ReceivingMasterTable.ReceivingNo, dbo.ReceivingMasterTable.Remarks,   " _
                    & " dbo.ReceivingMasterTable.ReceivingDate, ISNULL(Receiving.Recv, 0) AS Recv, ISNULL(Receiving.LocationId,0) as LocationId  " _
                    & " FROM dbo.ReceivingMasterTable LEFT OUTER JOIN (SELECT ReceivingId, LocationId, SUM(ISNULL(Qty, 0)) AS Recv  " _
                    & " FROM dbo.ReceivingDetailTable WHERE ReceivingDetailTable.LocationId=" & Me.cmbLocation.SelectedValue & " GROUP BY ReceivingId,LocationId) AS Receiving ON Receiving.ReceivingId = dbo.ReceivingMasterTable.ReceivingId  WHERE LEFT(ReceivingMasterTAble.ReceivingNo,3) <> 'Pur')a WHERE a.LocationId <> 0 Order By a.ReceivingDate Asc "
            Dim da As New OleDb.OleDbDataAdapter
            Dim dt As New DataTable
            If Con.State = ConnectionState.Closed Then Con.Open()
            cmd.Connection = Con
            cmd.CommandText = strSQL
            da.SelectCommand = cmd
            da.Fill(dt)

            strSQL = String.Empty
            strSQL = "Select * From tblStorageBilling"
            Dim dtDetail As New DataTable
            If Con.State = ConnectionState.Closed Then Con.Open()
            cmd.Connection = Con
            cmd.CommandText = strSQL
            da.SelectCommand = cmd
            da.Fill(dtDetail)
            Dim intRecordNo As Integer = dtDetail.Rows.Count
            Dim a As Integer = 1
            For Each r As DataRow In dt.Rows
                If a > intRecordNo Then Exit For
                strSQL = "Update tblStorageBilling SET Bowser_Issued_No='" & r("Bowser_Arrival_No").ToString & "', Filling_Date='" & r("Arrival_Date") & "', Acc_Dispatch=" & Val(r("Recv").ToString) & " WHERE Id=" & a & ""
                If Con.State = ConnectionState.Closed Then Con.Open()
                cmd.Connection = Con
                cmd.CommandText = strSQL
                cmd.ExecuteNonQuery()
                a += 1
            Next
            strSQL = "SP_Storage_Billing " & Me.cmbLocation.SelectedValue & ""
            Dim dtData As DataTable = GetDataTable(strSQL)

            Dim dblDispatch As Double = 0D

            Dim i As Integer = 0
            For Each r As DataRow In dtData.Rows
                dblDispatch += Val(r("Acc_Dispatch").ToString)
                r.BeginEdit()
                If i > 0 Then
                    r("Balance") = dblDispatch
                    r("CF") = (Val(r("Balance").ToString) - Val(r("Acc_Dispatch").ToString))
                Else
                    r("Balance") = dblDispatch
                    r("CF") = Val(r("Acc_Dispatch"))
                End If
                r.EndEdit()
                i += 1
            Next
            dtData.Columns("Billing_Amount").Expression = "((Balance*Days)*Rate)"
            Me.grd.DataSource = dtData
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "LPG Storage Billing For " & cmbLocation.Text.ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_ColumnHeaderClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnHeaderClick
        Try
            If e.Column.DataMember = "Rate" Then
                Dim i As Integer = 0
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    r.BeginEdit()
                    If i > 0 Then
                        r.Cells("Rate").Value = (Me.grd.GetRows(0).Cells("Rate").Value.ToString)
                    End If
                    r.EndEdit()
                    i += 1
                Next
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

    End Sub
End Class
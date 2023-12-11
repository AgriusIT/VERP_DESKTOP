Public Class frmGrdRptSalesPriceChange

    Private Sub frmGrdRptSalesPriceChange_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.dtpDateFrom.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpDateTo.Value = Date.Now
            FillCombo()
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            FillUltraDropDown(Me.cmbAccount, "Select coa_detail_id, detail_title as [Customer], detail_code as [Code], sub_sub_title as [Main Account] From vwCOADetail WHERE detail_title <> '' AND Account_Type In('Customer','Vendor') Order by detail_title ASC")
            Me.cmbAccount.Rows(0).Activate()
            If Me.cmbAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Customer").Width = 200
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Code").Width = 125
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Main Account").Width = 150
            End If
            FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleDescription as Item, ArticleCode as Code,ArticleGroupName as Dept, ArticleTypeName as Type From ArticleDefView WHERE ArticleDescription <> '' AND Active=1")
            Me.cmbItem.Rows(0).Activate()
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            If Me.cmbAccount.IsItemInList = False Then Exit Sub
            id = Me.cmbAccount.ActiveRow.Cells(0).Value
            FillCombo()
            Me.cmbAccount.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillGrid(Optional ByVal Condition As String = "")
        Try

            Dim strSQL As String = "SP_PriceHistory '" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "', " & Me.cmbAccount.Value & "," & Me.cmbItem.Value & " "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            dt.TableName = "Default"
            Dim dtComp As New DataTable
            For c As Integer = 0 To dt.Columns.Count - 1
                dtComp.Columns.Add(dt.Columns(c).ColumnName, dt.Columns(c).DataType)
            Next

            Dim ItemId As Integer = 0I
            Dim Price As Double = 0D
            Dim j As Integer = 1I
            For Each r As DataRow In dt.Rows
                If Val(r.Item("ArticleDefId").ToString) <> ItemId Or (Val(r.Item("Price").ToString) <> Price AndAlso Val(r.Item("ArticleDefId").ToString) = ItemId) Then
                    Dim dr As DataRow
                    dr = dtComp.NewRow
                    dr(enmField.ArticleDefId) = r.Item("ArticleDefId").ToString
                    dr(enmField.ItemType) = r.Item("Item Type").ToString
                    dr(enmField.ItemDept) = r.Item("Item Dept").ToString
                    dr(enmField.ItemCode) = r.Item("Item Code").ToString
                    dr(enmField.Item) = r.Item("Item").ToString
                    dr(enmField.Color) = r.Item("Color").ToString
                    dr(enmField.Size) = r.Item("Size").ToString
                    If (dt.Rows.Count - 1) >= j Then
                        dr(enmField.FromDate) = dt.Rows(j).Item("Date").ToString
                    Else
                        dr(enmField.FromDate) = r.Item("Date").ToString
                    End If
                    dr(enmField.Date) = r.Item("Date").ToString
                    dr(enmField.Price) = Val(r.Item("Price").ToString)
                    dtComp.Rows.Add(dr)
                    dtComp.AcceptChanges()
                End If
                ItemId = Val(r.Item("ArticleDefId").ToString)
                Price = Val(r.Item("Price").ToString)
                j += 1
            Next
                Me.grd.DataSource = dtComp
                Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try

            If Me.cmbAccount.IsItemInList = False Then Exit Sub
            If Me.cmbAccount.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select customer.")
                Exit Sub
            End If
            FillGrid()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                Me.grd.RootTable.Columns(c).Visible = False
            Next
            Me.grd.RootTable.Columns("FromDate").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("Date").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("FromDate").Visible = True
            Me.grd.RootTable.Columns("Date").Visible = True
            Me.grd.RootTable.Columns("Item Code").Visible = True
            Me.grd.RootTable.Columns("Item").Visible = True
            Me.grd.RootTable.Columns("Color").Visible = True
            Me.grd.RootTable.Columns("Size").Visible = True
            Me.grd.RootTable.Columns("Price").Visible = True
            Me.grd.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
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
            If Me.cmbAccount.ActiveRow Is Nothing Then Exit Sub
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Price Compare" & Chr(10) & "Customer: " & Me.cmbAccount.ActiveRow.Cells("Customer").Value.ToString & Chr(10) & "Date From:" & Me.dtpDateFrom.Value & " Date To: " & Me.dtpDateTo.Value & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Enum enmField
        ArticleDefId
        ItemType
        ItemDept
        ItemCode
        Item
        Color
        Size
        FromDate
        [Date]
        Price
    End Enum
End Class
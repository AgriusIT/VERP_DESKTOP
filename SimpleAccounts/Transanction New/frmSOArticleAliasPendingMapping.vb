Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports SBDal
Imports SBModel
Imports SBUtility

Public Class frmSOArticleAliasPendingMapping
    Dim IsOpenForm As Boolean = False
    Private blnDisplayAll As Boolean = False
    Private Sub FillCombo()
        Try
            FillUltraDropDown(Me.cmbVendor, "Select coa_detail_id, detail_title as [Customer],detail_code as [Code], account_type as [Type] From vwCOADetail where Account_type in ('Customer','Vendor') and detail_title <> ''")
            Me.cmbVendor.Rows(0).Activate()
            Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmSOArticleAliasPendingMapping_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.dtpDateFrom.Value = Date.Now
            Me.dtpDateTo.Value = Today
            Me.dtpDateFrom.Checked = False
            Me.dtpDateTo.Checked = False
            FillCombo()
            FillHistoryGrid()
            Me.SplitContainer1.Panel2Collapsed = True
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombo()
            Me.cmbVendor.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            FillHistoryGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillHistoryGrid()
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Select SalesOrderMasterTable.SalesOrderID,SalesOrderMasterTable.LocationId, SalesOrderNo as [SO No],SalesOrderDate as [SO Date], SalesOrderQty as [Qty], SalesOrderAmount, [Status], SalesOrderTypeTable.SalesOrderTypeName as [Order Type],IsNull(SalesOrderMasterTable.VendorId,0) as VendorID  from SalesOrderMasterTable  " _
                    & " LEFT OUTER JOIN vwCOADetail on vwCOADetail.coa_detail_id  = SalesOrderMasterTable.VendorId  " _
                    & " LEFT OUTER JOIN SalesOrderTypeTable on SalesOrderTypeTable.SaleOrderTypeID = SalesOrderMasterTable.SaleOrderTypeId  " _
                    & " where SalesOrderMasterTable.SalesOrderId in(select SalesOrderId from SalesOrderDetailTable where ArticleDefId is null And SalesOrderDetailTable.ArticleAliasName <> '') "
            If Me.dtpDateFrom.Checked = True Then
                strSQL += " AND (Convert(varchar, SalesOrderMasterTable.SalesOrderDate, 102) >= Convert(dateTime,'" & dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102))"
            End If
            If Me.dtpDateTo.Checked = True Then
                strSQL += " AND (Convert(varchar, SalesOrderMasterTable.SalesOrderDate, 102) <= Convert(dateTime,'" & dtpDateTo.Value.ToString("yyyy-M-d 00:00:00") & "',102))"
            End If
            If Me.cmbVendor.Value > 0 Then
                strSQL += " AND SalesOrderMasterTable.VendorID=" & Me.cmbVendor.Value & ""
            End If
            If Me.txtSpecificOrderNo.Text.Length > 0 Then
                strSQL += " AND SalesORderMasterTable.SalesOrderNo LIKE '%" & Me.txtSpecificOrderNo.Text.Replace("'", "''") & "%'"
            End If
            strSQL += " Order By SalesOrderMasterTable.SalesOrderId DESC"

            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            Me.grdHistory.DataSource = dt
            Me.grdHistory.RetrieveStructure()
            Me.grdHistory.RootTable.Columns("SalesOrderID").Visible = False
            Me.grdHistory.RootTable.Columns("LocationId").Visible = False
            Me.grdHistory.RootTable.Columns("VendorID").Visible = False
            Me.grdHistory.RootTable.Columns("SO No").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grdHistory.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdHistory_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdHistory.LinkClicked
        Try
            'frmSalesOrder
            If Not frmMain.Panel2.Controls.Contains(frmSalesOrderNew) Then
                frmMain.LoadControl("frmSalesOrder")
            End If
            frmModProperty.Tags = Me.grdHistory.GetRow.Cells("SO No").Value.ToString
            frmMain.LoadControl("frmSalesOrder")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdHistory_SelectionChanged(sender As Object, e As EventArgs) Handles grdHistory.SelectionChanged
        Try
            If IsOpenForm = False Then Exit Sub
            If Me.SplitContainer1.Panel2Collapsed = True Then
                Me.SplitContainer1.Panel2Collapsed = False
            End If
            Me.lblSONo.Text = String.Empty
            If Me.grdHistory.Row < 0 Then Exit Sub
            Me.lblSONo.Text = "Sale Order No:" & Me.grdHistory.GetRow.Cells("SO No").Value.ToString & ", Sale Order Date: " & Me.grdHistory.GetRow.Cells("SO Date").Value.ToString & ""
            Me.lblSONo.Tag = Val(Me.grdHistory.GetRow.Cells("VendorID").Value.ToString)
            FillSOGrid(Val(Me.grdHistory.GetRow.Cells("SalesOrderID").Value.ToString))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillSOGrid(SalesOrderId As Integer)
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select SalesOrderDetailId, SalesOrderID, ArticleDefId as Item, ArticleAliasName,Qty,Price,(IsNull(Qty,0)*IsNull(Price,0)) as Total From SalesOrderDetailTable WHERE Isnull(ArticleDefId,0)=0 AND ArticleAliasName <> '' AND SalesOrderID=" & SalesOrderId & "")
            dt.AcceptChanges()
            Me.grdSaleOrder.DataSource = dt
            Me.grdSaleOrder.RetrieveStructure()
            Me.grdSaleOrder.RootTable.Columns("SalesOrderDetailId").Visible = False
            Me.grdSaleOrder.RootTable.Columns("SalesOrderId").Visible = False
            Me.grdSaleOrder.RootTable.Columns("Item").HasValueList = True
            Me.grdSaleOrder.RootTable.Columns("Item").EditType = Janus.Windows.GridEX.EditType.Combo
            Dim dtItem As New DataTable
            dtItem = GetDataTable("Select ArticleId, ArticleDescription,ArticleCode From ArticleDefTable WHERE ArticleDescription <> '' Order By ArticleDescription ASC")
            Me.grdSaleOrder.RootTable.Columns("Item").ValueList.PopulateValueList(dtItem.DefaultView, "ArticleId", "ArticleDescription")
            Me.grdSaleOrder.RootTable.Columns("Item").Width = 250
            Me.grdSaleOrder.RootTable.Columns("ArticleAliasName").Width = 250
            'Me.grdSaleOrder.AutoSizeColumns()
            For c As Integer = 0 To Me.grdSaleOrder.RootTable.Columns.Count - 1
                If Me.grdSaleOrder.RootTable.Columns(c).DataMember <> "Item" Then
                    Me.grdSaleOrder.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
            UpdateRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UpdateRecord()
        Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand

        cmd.Connection = objCon
        cmd.Transaction = trans
        cmd.CommandTimeout = 120
        cmd.CommandType = CommandType.Text
        Try
            For Each jsRow As Janus.Windows.GridEX.GridEXRow In Me.grdSaleOrder.GetRows
                cmd.CommandText = ""
                cmd.CommandText = "Update SalesOrderDetailTable Set ArticleDefId=" & Val(jsRow.Cells("Item").Value.ToString) & " WHERE SalesOrderDetailTable.SalesOrderDetailId=" & Val(jsRow.Cells("SalesOrderDetailId").Value.ToString) & " AND SalesOrderDetailTable.SalesOrderId=" & Val(jsRow.Cells("SalesOrderId").Value.ToString) & ""
                cmd.ExecuteNonQuery()

                SaveArticleAlias(jsRow, trans)
            Next
            trans.Commit()
            FillHistoryGrid()
            FillSOGrid(-1)
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Sub
    Private Sub SaveArticleAlias(grdRow As Janus.Windows.GridEX.GridEXRow, trans As OleDb.OleDbTransaction)
        Dim cmd As New OleDb.OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandTimeout = 120
        Try
            cmd.CommandText = ""
            cmd.CommandText = "Select Distinct MasterID  From ArticleDefTable where ArticleId=" & Val(grdRow.Cells("Item").Value.ToString) & ""
            Dim intArticleMasterID As Integer = cmd.ExecuteScalar
            Dim dt As New DataTable
            dt = GetDataTable("Select Distinct ArticleAliasName From SalesOrderDetailTable WHERE ArticleDefId=" & Val(grdRow.Cells("Item").Value.ToString) & " AND ArticleAliasName <> '' And ArticleAliasName not in (Select ArticleAliasName From ArticleAliasDefTable WHERE ArticleMasterId=" & intArticleMasterID & ") ", trans)
            dt.AcceptChanges()
            Dim currentID As Integer = 0I
            For Each r As DataRow In dt.Rows
                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO ArticleAliasDefTable(ArticleMasterID, ArticleAliasCode,ArticleAliasName,VendorId,Active,SortOrder) VALUES(" & intArticleMasterID & ",N'" & r.Item("ArticleAliasName").ToString.Replace("'", "''") & "',N'" & r.Item("ArticleAliasName").ToString.Replace("'", "''") & "'," & Val(Me.lblSONo.Tag.ToString) & ",1,1)Select @@Identity"
                currentID = cmd.ExecuteScalar()
            Next
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
        End Try
    End Sub
End Class
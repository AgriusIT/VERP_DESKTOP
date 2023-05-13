Imports System.Data.OleDb

Public Class frmDashboard
    Private _dtCashBalance As New DataTable
    Private _dtSalesPurchase As New DataTable
    Private _dtExpense As New DataTable
    Private _dtPaybleReceivable As New DataTable
    Private _dtPostDatedCheque As New DataTable
    Private _dtAttendance As New DataTable
    Private _dtTasks As New DataTable
    Private _dtStockLevel As New DataTable
    Private _dtNotifications As New DataTable
    Private _strSMSBalance As String = String.Empty
    Private _FromDate As DateTime = DateTime.Now.AddDays(-30)
    Private _ToDate As DateTime = DateTime.Now
    Private _IncludeUnPosted As Boolean = True
    Private _IncludeTax As Boolean = False
    Private _Invoke As Boolean = False
    Private _IsOpenForm As Boolean = False
    Private _StockLevel As String = "Optimal"
    Dim dtForList As New DataTable
    Private _dtStockValue As New DataTable
    Private Sub bgwCash_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwCash.DoWork
        Try
            GetCashBalances()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Sub GetCashBalances()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SP_CashBankBalances'" & _FromDate.ToString("yyyy-M-d 00:00:00") & "', '" & _ToDate.ToString("yyyy-M-d 23:59:59") & "', " & IIf(_IncludeUnPosted = True, 1, 0) & ""
            _dtCashBalance = GetDataTable(strSQL)
            _dtCashBalance.AcceptChanges()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetPayableReceivable()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SP_PayableReceivable '" & _ToDate.ToString("yyyy-M-d 00:00:00") & "', " & IIf(_IncludeUnPosted = True, 1, 0) & ""
            _dtPaybleReceivable = GetDataTable(strSQL)
            _dtPaybleReceivable.AcceptChanges()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetExpense()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SP_DashboardExpense '" & _FromDate.ToString("yyyy-M-d 00:00:00") & "', '" & _ToDate.ToString("yyyy-M-d 23:59:59") & "', " & IIf(_IncludeUnPosted = True, 1, 0) & ""
            _dtExpense = GetDataTable(strSQL)
            _dtExpense.AcceptChanges()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetStockValue()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT       'Stock Value' as Trans_Type ,SUM((ISNULL(OpenStock.OpenQty, 0) + ISNULL(InStock.InQty, 0) - ISNULL(OutStock.OutQty, 0)) * ISNULL(Price.Price, 0)) AS Amount  FROM ArticleDefView INNER JOIN " _
& "                      ArticleGroupDefTable AS Grp ON Grp.ArticleGroupId = ArticleDefView.ArticleGroupId LEFT OUTER JOIN" _
& "                          (SELECT     StockDetailTable.ArticleDefId, SUM(ISNULL(StockDetailTable.InQty, 0) - ISNULL(StockDetailTable.OutQty, 0)) AS OpenQty, SUM(ISNULL(StockDetailTable.InAmount, 0) " _
& "                                                   - ISNULL(StockDetailTable.OutAmount, 0)) AS OpenAmount" _
& "                            FROM          StockMasterTable INNER JOIN" _
& "                                                   StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN" _
& "                                                   tblDefLocation AS lc ON lc.location_id = StockDetailTable.LocationId" _
& "                            WHERE      (CONVERT(varchar, StockMasterTable.DocDate, 102) < CONVERT(Datetime, '" & _FromDate.ToString("yyyy-M-d 00:00:00") & "', 102))" _
& "                            GROUP BY StockDetailTable.ArticleDefId) AS OpenStock ON OpenStock.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN" _
& "                          (SELECT     StockDetailTable.ArticleDefId, SUM(ISNULL(StockDetailTable.OutQty, 0)) AS OutQty, SUM(ISNULL(StockDetailTable.OutAmount, 0)) AS OutAmount" _
& "                            FROM          StockMasterTable INNER JOIN" _
& "                                                   StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN" _
& "                                                   tblDefLocation AS lc ON lc.location_id = StockDetailTable.LocationId" _
& "                            WHERE      (CONVERT(varchar, StockMasterTable.DocDate, 102) BETWEEN CONVERT(Datetime, '" & _FromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & _ToDate.ToString("yyyy-M-d 23:59:59") & "', 102))" _
& "                           GROUP BY StockDetailTable.ArticleDefId " _
& "                            HAVING      (SUM(ISNULL(StockDetailTable.OutQty, 0)) <> 0)) AS OutStock ON OutStock.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN" _
& "                          (SELECT     StockDetailTable.ArticleDefId, SUM(ISNULL(StockDetailTable.InQty, 0)) AS InQty, SUM(ISNULL(StockDetailTable.InAmount, 0)) AS InAmount" _
& "                            FROM          StockMasterTable INNER JOIN " _
& "                                                   StockDetailTable ON StockMasterTable.StockTransId = StockDetailTable.StockTransId LEFT OUTER JOIN" _
& "                                                   tblDefLocation AS lc ON lc.location_id = StockDetailTable.LocationId " _
& "                            WHERE      (CONVERT(varchar, StockMasterTable.DocDate, 102) BETWEEN CONVERT(datetime, '" & _FromDate.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(Datetime, '" & _ToDate.ToString("yyyy-M-d 23:59:59") & "', 102))" _
& "                            GROUP BY StockDetailTable.ArticleDefId" _
& "                            HAVING      (SUM(ISNULL(StockDetailTable.InQty, 0)) <> 0)) AS InStock ON InStock.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN" _
& "                          (SELECT     ArticleDefId, Rate AS Price" _
& "            FROM StockDetailTable WHERE      (StockTransDetailId IN" _
& "                                                       (SELECT     MAX(StockDetailTable.StockTransDetailId) AS Expr1" _
& "                                                         FROM          StockDetailTable INNER JOIN" _
& "                                                                                StockMasterTable ON StockDetailTable.StockTransId = StockMasterTable.StockTransId" _
& "                                                         WHERE      (StockDetailTable.InQty > 0) AND (StockMasterTable.DocNo NOT LIKE '%SRN%') AND (CONVERT(varchar, StockMasterTable.DocDate, 102) <= CONVERT(Datetime,'" & _ToDate.ToString("yyyy-M-d 23:59:59") & "', 102))" _
& "                                                         GROUP BY StockDetailTable.ArticleDefId))) AS Price ON Price.ArticleDefId = ArticleDefView.ArticleId WHERE     (ArticleDefView.Active = 1) AND (ISNULL(Grp.ServiceItem, 0) <> 1)"
            _dtStockValue = GetDataTable(strSQL)
            _dtStockValue.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetSalesPurchase()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SP_SalesPurchase'" & _FromDate.ToString("yyyy-M-d 00:00:00") & "', '" & _ToDate.ToString("yyyy-M-d 23:59:59") & "', " & IIf(_IncludeUnPosted = True, 1, 0) & "," & IIf(_IncludeTax = True, 1, 0) & ""
            _dtSalesPurchase = GetDataTable(strSQL)
            _dtSalesPurchase.AcceptChanges()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetPostDatedCheque()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SP_PostDatedCheque '" & _ToDate.ToString("yyyy-M-d 00:00:00") & "'"
            _dtPostDatedCheque = GetDataTable(strSQL)
            _dtPostDatedCheque.AcceptChanges()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAttendance()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SP_DashboardAttendance'" & _FromDate.ToString("yyyy-M-d 00:00:00") & "', '" & _ToDate.ToString("yyyy-M-d 23:59:59") & "'   "
            _dtAttendance = GetDataTable(strSQL)
            _dtAttendance.AcceptChanges()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetTasks()
        Try
            ServerDate()

            Dim strQuery As String = String.Empty
            strQuery = " Select 'Today' as Trans_Type, Count(IsNull(AssignedTo, 0)) As AssignedCount From dbo.TblDefTasks Where AssignedTo ='" & LoginUserId & "' And Convert(varchar, ClosingDate, 102) Is Null And Convert(varchar, TaskDate, 102) = Convert(datetime, '" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102)  " _
                & " Union All Select 'Previous' as Trans_Type, Count(IsNull(AssignedTo, 0)) As AssignedCount From dbo.TblDefTasks Where AssignedTo ='" & LoginUserId & "' And Convert(varchar, ClosingDate, 102) Is Null And Convert(varchar, TaskDate, 102) < Convert(datetime, '" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102) " _
                & " Union All Select  'Future' as Trans_Type, Count(IsNull(AssignedTo, 0)) As AssignedCount From dbo.TblDefTasks Where AssignedTo ='" & LoginUserId & "' And Convert(varchar, ClosingDate, 102) Is Null And Convert(varchar, TaskDate, 102) > Convert(datetime, '" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102) "
            _dtTasks = GetDataTable(strQuery)
            _dtTasks.AcceptChanges()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetStockLevel(Optional StockLevel As String = "")
        Try
            Dim strSQL As String = String.Empty
            If StockLevel = "Minimum" Then
                'strSQL = "Select Count(*) From (Select ArticleId,IsNull(StockLevel,0) StockLevel,Case When IsNull(Stock.CurrentStock,0) < IsNull(StockLevel,0) Then IsNull(Stock.CurrentStock,0) Else 0 End  as [Min Level Stock]  From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-IsNull(OutQty,0)) <> 0) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId ) a WHERE a.[Min Level Stock] <> 0  AND a.StockLevel <> 0"
                strSQL = "Select COUNT(*) From (Select ArticleId,ArticleDescription as Item, ArticleCode as Code, ArticleSizeName as Size, ArticleColorName as Color, ArticleUnitName As Unit,ArticleGroupName as Department, ArticleTypeName as Type, IsNull(StockLevel,0) as [Minimum Stock Level],  IsNull(Stock.CurrentStock,0) as [Current Stock] From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable Group By ArticleDefId) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId) a  WHERE (a.[Minimum Stock Level] - a.[Current Stock]) > 0 AND a.[Minimum Stock Level] > 0 "
            ElseIf StockLevel = "Maximum" Then
                strSQL = "Select Count(*) From (Select ArticleId,IsNull(StockLevelMax,0) StockLevel,Case When IsNull(Stock.CurrentStock,0) >= IsNull(StockLevelMax,0)  Then IsNull(Stock.CurrentStock,0) Else 0 End  as [Max Level Stock]  From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-IsNull(OutQty,0)) <> 0) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId) a WHERE a.[Max Level Stock] <> 0  AND a.StockLevel <> 0"
            ElseIf StockLevel = "Optimal" Then
                strSQL = "Select Count(*) From (Select ArticleId,IsNull(StockLevelOpt,0) StockLevel,Case When IsNull(Stock.CurrentStock,0) >= IsNull(StockLevelOpt,0) And IsNull(Stock.CurrentStock,0) <= IsNull(StockLevelOpt,0) Then IsNull(Stock.CurrentStock,0) Else 0 End  as [Opt Level Stock]  From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-IsNull(OutQty,0)) <> 0) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId) a WHERE a.[Opt Level Stock] <> 0  AND a.StockLevel <> 0"
            Else
                strSQL = "Select Count(*) From (Select ArticleId,IsNull(StockLevel,0) StockLevel,Case When IsNull(Stock.CurrentStock,0) < IsNull(StockLevel,0) Then IsNull(Stock.CurrentStock,0) Else 0 End  as [Min Level Stock]  From ArticleDefView LEFT OUTER JOIN (Select ArticleDefId, SUM(IsNull(InQty,0)-IsNull(OutQty,0)) CurrentStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-IsNull(OutQty,0)) <> 0) Stock On  Stock.ArticleDefId = ArticleDefView.ArticleId ) a WHERE a.[Min Level Stock] <> 0  AND a.StockLevel <> 0"
            End If

            _dtStockLevel = GetDataTable(strSQL)
            _dtStockLevel.AcceptChanges()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub bgwCash_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwCash.RunWorkerCompleted
        Try

            If _dtCashBalance.Rows.Count = 0 Then Exit Sub

            Dim dr() As DataRow
            dr = _dtCashBalance.Select("Trans_Type='Cash Balance'")
            If dr.Length > 0 Then
                Me.lblCashBalance.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblCashBalance.Text = 0
            End If
            dr = _dtCashBalance.Select("Trans_Type='Bank Balance'")
            If dr.Length > 0 Then
                Me.lblBankBalance.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblBankBalance.Text = 0
            End If
            dr = _dtCashBalance.Select("Trans_Type='Cash Receipt'")
            If dr.Length > 0 Then
                Me.lblCashReceipt.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblCashReceipt.Text = 0
            End If
            dr = _dtCashBalance.Select("Trans_Type='Cash Payment'")
            If dr.Length > 0 Then
                Me.lblCashPayment.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                lblCashPayment.Text = 0
            End If
            dr = _dtCashBalance.Select("Trans_Type='Bank Receipt'")
            If dr.Length > 0 Then
                Me.lblBankReceipt.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblBankReceipt.Text = 0
            End If
            dr = _dtCashBalance.Select("Trans_Type='Bank Payment'")
            If dr.Length > 0 Then
                Me.lblBankPayment.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblBankPayment.Text = 0
            End If

            Me.lblTotalReceipt.Text = Math.Round(Val(Me.lblCashReceipt.Text) + Val(Me.lblBankReceipt.Text), 0)
            Me.lblTotalPayment.Text = Math.Round(Val(Me.lblCashPayment.Text) + Val(Me.lblBankPayment.Text), 0)
            Me.lblTotalBalance.Text = Math.Round(Val(Me.lblTotalReceipt.Text) - Val(Me.lblTotalPayment.Text), 0)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmDashboard_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.pnlDateRange.Visible = False
            If _Invoke = False Then
                ServerDate()
                Me._FromDate = GetServerDate
                Me._ToDate = GetServerDate
                Me.ComboBox1.SelectedIndex = 0
            Else
                Me._FromDate = Me.dtpFromDate.Value
                Me._ToDate = Me.dtpToDate.Value
                Me._IncludeUnPosted = Me.chkIncludeUnpostedVoucher.Checked
                Me._IncludeTax = Me.chkIncludeTaxAmount.Checked
            End If

            If bgwCash.IsBusy Then Exit Sub
            bgwCash.RunWorkerAsync()
            Do While bgwCash.IsBusy
                Application.DoEvents()
            Loop

            If bgwPostDatedCheque.IsBusy Then Exit Sub
            bgwPostDatedCheque.RunWorkerAsync()
            Do While bgwPostDatedCheque.IsBusy
                Application.DoEvents()
            Loop

            If bgwPayableReceivable.IsBusy Then Exit Sub
            bgwPayableReceivable.RunWorkerAsync()
            Do While bgwPayableReceivable.IsBusy
                Application.DoEvents()
            Loop

            If bgwSalePurchase.IsBusy Then Exit Sub
            bgwSalePurchase.RunWorkerAsync()
            Do While bgwSalePurchase.IsBusy
                Application.DoEvents()
            Loop


            If bgwStockValue.IsBusy Then Exit Sub
            bgwStockValue.RunWorkerAsync()
            Do While bgwStockValue.IsBusy
                Application.DoEvents()
            Loop



            If bgwTasks.IsBusy Then Exit Sub
            bgwTasks.RunWorkerAsync()
            Do While bgwTasks.IsBusy
                Application.DoEvents()
            Loop

            If bgwAttendance.IsBusy Then Exit Sub
            Me.bgwAttendance.RunWorkerAsync()
            Do While Me.bgwAttendance.IsBusy
                Application.DoEvents()
            Loop

            _StockLevel = Me.ComboBox1.Text
            If bgwStockLevel.IsBusy Then Exit Sub
            bgwStockLevel.RunWorkerAsync()
            Do While bgwStockLevel.IsBusy
                Application.DoEvents()
            Loop

            If bgwExpense.IsBusy Then Exit Sub
            bgwExpense.RunWorkerAsync()
            Do While bgwExpense.IsBusy
                Application.DoEvents()
            Loop


            If bgwSMSBalance.IsBusy Then Exit Sub
            bgwSMSBalance.RunWorkerAsync()
            Do While bgwSMSBalance.IsBusy
                Application.DoEvents()
            Loop

            Me.tmrAlerts.Interval = 100
            Me.tmrAlerts.Enabled = True

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub bgwSalePurchase_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwSalePurchase.DoWork
        Try
            GetSalesPurchase()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub bgwSalePurchase_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwSalePurchase.RunWorkerCompleted
        Try
            If _dtSalesPurchase.Rows.Count = 0 Then Exit Sub
            Dim dr() As DataRow
            dr = _dtSalesPurchase.Select("Trans_Type='Sales'")
            If dr.Length > 0 Then
                Me.lblSales.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblSales.Text = 0
            End If
            dr = _dtSalesPurchase.Select("Trans_Type='Sales Return'")
            If dr.Length > 0 Then
                Me.lblSalesReturn.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblSalesReturn.Text = 0
            End If
            Me.lblTotalSales.Text = Math.Round(Val(lblSales.Text) - Val(Me.lblSalesReturn.Text), 0)
            dr = _dtSalesPurchase.Select("Trans_Type='Purchase'")
            If dr.Length > 0 Then
                Me.lblPurchase.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblPurchase.Text = 0
            End If
            dr = _dtSalesPurchase.Select("Trans_Type='Purchase Return'")
            If dr.Length > 0 Then
                Me.lblPurchaseReturn.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblPurchaseReturn.Text = 0
            End If
            Me.lblTotalPurchase.Text = Math.Round(Val(lblPurchase.Text) - Val(Me.lblPurchaseReturn.Text), 0)

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub bgwExpense_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwExpense.DoWork
        Try

            GetExpense()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub bgwExpense_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwExpense.RunWorkerCompleted
        Try
            If _dtExpense.Rows.Count = 0 Then Exit Sub
            Dim dr() As DataRow
            dr = _dtExpense.Select("Trans_Type='Expense'")
            If dr.Length > 0 Then
                Me.lblExpense.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblExpense.Text = 0
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub bgwPayableReceivable_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwPayableReceivable.DoWork
        Try
            GetPayableReceivable()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub bgwPayableReceivable_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwPayableReceivable.RunWorkerCompleted
        Try

            If _dtPaybleReceivable.Rows.Count = 0 Then Exit Sub
            Dim dr() As DataRow
            dr = _dtPaybleReceivable.Select("Trans_Type='Payable'")
            If dr.Length > 0 Then
                Me.lblPayable.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblPayable.Text = 0
            End If
            dr = _dtPaybleReceivable.Select("Trans_Type='Receivable'")
            If dr.Length > 0 Then
                Me.lblReceivable.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblReceivable.Text = 0
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub bgwSMSBalance_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwSMSBalance.DoWork
        Try
            _strSMSBalance = GetBrandedSMSBalance()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub bgwSMSBalance_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwSMSBalance.RunWorkerCompleted
        Try
            Me.lblSMSBalance.Text = _strSMSBalance
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Sub pnlRefresh_Click(sender As Object, e As EventArgs) Handles pnlRefresh.Click
        Try
            Me.frmDashboard_Shown(Nothing, Nothing)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub pnlDate_Click(sender As Object, e As EventArgs) Handles pnlDate.Click
        Try
            If Me.lstNotifications.Visible = True Then
                Me.lstNotifications.Visible = False
            End If
            If Me.pnlDateRange.Visible = False Then
                pnlDateRange.BringToFront()
                Me.pnlDateRange.Visible = True
            Else
                pnlDateRange.SendToBack()
                Me.pnlDateRange.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            _Invoke = True
            Me.frmDashboard_Shown(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub bgwPostDatedCheque_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwPostDatedCheque.DoWork
        Try
            GetPostDatedCheque()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub bgwPostDatedCheque_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwPostDatedCheque.RunWorkerCompleted
        Try
            If _dtPostDatedCheque.Rows.Count = 0 Then Exit Sub
            Dim dr() As DataRow
            dr = _dtPostDatedCheque.Select("Trans_Type='Tomorrow'")
            If dr.Length > 0 Then
                Me.lblchequeTommorow.Text = Val(dr(0).Item(1).ToString)
            Else
                Me.lblchequeTommorow.Text = 0
            End If
            dr = _dtPostDatedCheque.Select("Trans_Type='Today'")
            If dr.Length > 0 Then
                Me.lblchequetoday.Text = Val(dr(0).Item(1).ToString)
            Else
                Me.lblchequetoday.Text = 0
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub bgwAttendance_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwAttendance.DoWork
        Try
            GetAttendance()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub bgwAttendance_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwAttendance.RunWorkerCompleted
        Try
            If _dtAttendance.Rows.Count = 0 Then Exit Sub
            Dim dr() As DataRow
            dr = Me._dtAttendance.Select("Trans_Type='Present'")
            If dr.Length > 0 Then
                Me.lblPresent.Text = Val(dr(0).Item(1).ToString)
            Else
                Me.lblPresent.Text = 0
            End If

            dr = Me._dtAttendance.Select("Trans_Type='Absent'")
            If dr.Length > 0 Then
                Me.lblAbsent.Text = Val(dr(0).Item(1).ToString)
            Else
                Me.lblAbsent.Text = 0
            End If
            dr = Me._dtAttendance.Select("Trans_Type='Absent1'")
            If dr.Length > 0 Then
                Me.lblAbsent.Text += Val(dr(0).Item(1).ToString)
            Else
                Me.lblAbsent.Text = 0
            End If
            Me.lblTotalAttendance.Text = Val(Me.lblPresent.Text) + Val(Me.lblAbsent.Text)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub bgwTasks_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwTasks.DoWork
        Try
            GetTasks()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub bgwTasks_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwTasks.RunWorkerCompleted
        Try
            If Me._dtTasks.Rows.Count = 0 Then Exit Sub
            Dim dr() As DataRow
            dr = _dtTasks.Select("Trans_Type='Today'")
            If dr.Length > 0 Then
                Me.lblTodayTasks.Text = Val(dr(0).Item(1).ToString)
            Else
                Me.lblTodayTasks.Text = 0
            End If
            dr = _dtTasks.Select("Trans_Type='Previous'")
            If dr.Length > 0 Then
                Me.lblOverdueTasks.Text = Val(dr(0).Item(1).ToString)
            Else
                Me.lblOverdueTasks.Text = 0
            End If
            dr = _dtTasks.Select("Trans_Type='Future'")
            If dr.Length > 0 Then
                Me.lblUpcomingTasks.Text = Val(dr(0).Item(1).ToString)
            Else
                Me.lblUpcomingTasks.Text = 0
            End If

            Me.lblTotalTasks.Text = Val(Me.lblTodayTasks.Text) + Val(Me.lblOverdueTasks.Text) + Val(Me.lblUpcomingTasks.Text)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            'If _IsOpenForm = False Then Exit Sub
            _StockLevel = Me.ComboBox1.Text
            If bgwStockLevel.IsBusy Then Exit Sub
            bgwStockLevel.RunWorkerAsync()
            Do While bgwStockLevel.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub bgwStockLevel_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwStockLevel.DoWork
        Try

            GetStockLevel(_StockLevel)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub bgwStockLevel_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwStockLevel.RunWorkerCompleted
        Try
            If _dtStockLevel.Rows.Count = 0 Then Exit Sub
            Me.lblStockCount.Text = Val(_dtStockLevel.Rows(0).Item(0).ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblPayable_Click(sender As Object, e As EventArgs) Handles lblPayable.Click
        Try
            BalancesReport("Vendor")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub BalancesReport(ByVal Condition As String)

        Try
            frmMain.LoadControl("rptTrial")
            rptTrialBalance.NoteId = 9
            rptTrialBalance.cmbAccountFrom.Rows(0).Activate()
            rptTrialBalance.cmbAccountTo.Rows(0).Activate()
            rptTrialBalance.cmbAcLevel.Text = "Detail A/c"
            rptTrialBalance.DateTimePicker1.Value = CDate("2001-1-1 00:00:00") ' Me.dtpFromDate.Value
            rptTrialBalance.DateTimePicker2.Value = Date.Now.ToString("yyyy-M-d 23:59:59")
            rptTrialBalance.chkIncludeUnPostedVouchers.Checked = False
            rptTrialBalance.GetDetailAccountsTrial(Condition)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lblReceivable_Click(sender As Object, e As EventArgs) Handles lblReceivable.Click
        Try
            BalancesReport("Customer")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function ReportQuery(Optional ByVal Condition As String = "") As DataTable
        Try
            Dim str As String = String.Empty
            Dim dt As New DataTable
            If Condition = "Today" Then
                str = "SELECT Isnull(Opening.opning,0) as Opening,  Coa.Detail_Code, V_D.coa_detail_id, " _
                       & "   COA.detail_title, V.voucher_No as voucher_code, V_Type.voucher_type, V.voucher_date, V_D.comments, V_D.debit_amount, V_D.credit_amount, COA.account_type, COA.main_sub_id, coa.CityName,  isnull(V_D.CostCenterID,0) as CostCenterID, V_D.Cheque_No, V_D.Cheque_Date FROM dbo.tblVoucher V INNER JOIN " _
                       & "   dbo.tblVoucherDetail V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
                       & "   dbo.vwcoadetail COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                       & "   dbo.tblDefVoucherType V_Type ON V.voucher_type_id = V_Type.voucher_type_id left outer join " _
                       & "  (SELECT VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning FROM dbo.tblVoucher V INNER JOIN " _
                       & "   dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                       & "   WHERE (CONVERT(varchar, V.voucher_date, 102) < Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " _
                       & "   GROUP BY VD.coa_detail_id) Opening On OPening.COA_Detail_ID = V_D.COA_Detail_ID " _
                       & "   Where (convert(varchar, V_D.Cheque_Date,102) = Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) ORDER BY V.Voucher_code asc"
            ElseIf Condition = "Tomorrow" Then
                str = "SELECT Isnull(Opening.opning,0) as Opening,  Coa.Detail_Code, V_D.coa_detail_id, " _
                                       & "   COA.detail_title, V.voucher_No as voucher_code, V_Type.voucher_type, V.voucher_date, V_D.comments, V_D.debit_amount, V_D.credit_amount, COA.account_type, COA.main_sub_id, coa.CityName,  isnull(V_D.CostCenterID,0) as CostCenterID, V_D.Cheque_No, V_D.Cheque_Date FROM dbo.tblVoucher V INNER JOIN " _
                                       & "   dbo.tblVoucherDetail V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
                                       & "   dbo.vwcoadetail COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                                       & "   dbo.tblDefVoucherType V_Type ON V.voucher_type_id = V_Type.voucher_type_id left outer join " _
                                       & "  (SELECT     VD.coa_detail_id, SUM(ISNULL(VD.debit_amount, 0)) - SUM(ISNULL(VD.credit_amount, 0)) AS Opning FROM dbo.tblVoucher V INNER JOIN " _
                                       & "   dbo.tblVoucherDetail VD ON V.voucher_id = VD.voucher_id " _
                                       & "   WHERE (CONVERT(varchar, V.voucher_date, 102) < Convert(Datetime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) " _
                                       & "   GROUP BY VD.coa_detail_id) Opening On OPening.COA_Detail_ID = V_D.COA_Detail_ID " _
                                       & "   Where (convert(varchar, V_D.Cheque_Date,102) = Convert(Datetime, '" & Me.dtpFromDate.Value.AddDays(1).ToString("yyyy-M-d 00:00:00") & "', 102)) ORDER BY V.Voucher_code asc"

            End If
            dt = GetDataTable(str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub lblchequeTommorow_Click(sender As Object, e As EventArgs) Handles lblchequeTommorow.Click
        Try
            ShowReport("PostDatedCheques", , , , False, , , Me.ReportQuery("Tomorrow"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblchequetoday_Click(sender As Object, e As EventArgs) Handles lblchequetoday.Click
        Try
            ShowReport("PostDatedCheques", , , , False, , , Me.ReportQuery("Today"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblCashReceipt_Click(sender As Object, e As EventArgs) Handles lblCashReceipt.Click

        Try
            ServerDate()
            Dim opening As Integer = GetAccountOpeningBalance(1, GetServerDate.Year & "-" & GetServerDate.Month & "-" & GetServerDate.Day & " 00:00:00", "Cash")
            AddRptParam("FromDate", dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptReceiptDetail", , , , , Val(opening).ToString, , GetCash("Receipt", "Cash"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Function GetCash(ByVal TransType As String, Optional ByVal Condition As String = "") As DataTable
        Try

            Dim dt As New DataTable
            Dim str As String = String.Empty
            str = "SELECT dbo.tblDefVoucherType.voucher_type, dbo.tblVoucher.voucher_no, dbo.tblVoucher.voucher_date, dbo.tblVoucherDetail.coa_detail_id, " _
            & "           " & IIf(TransType = "Payment", "tblvoucherdetail.debit_amount, tblvoucherdetail.credit_amount", "tblvoucherdetail.debit_amount, tblvoucherdetail.credit_amount") & ",  dbo.vwCOADetail.detail_code, dbo.vwCOADetail.detail_title,  " _
            & "           ISNULL(dbo.tblVoucherDetail.CostCenterID, 0) AS CostCenterID, dbo.tblDefCostCenter.Name AS CostCenter, dbo.tblVoucher.post,  " _
            & "           dbo.tblVoucher.cheque_no, dbo.tblVoucher.cheque_date, tblVoucherDetail.Comments as Description " _
            & "           FROM dbo.tblVoucherDetail INNER JOIN " _
            & "        dbo.tblVoucher ON dbo.tblVoucherDetail.voucher_id = dbo.tblVoucher.voucher_id INNER JOIN " _
            & "   dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id INNER JOIN  " _
            & " dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN    " _
            & " dbo.tblDefCostCenter ON dbo.tblVoucherDetail.CostCenterID = dbo.tblDefCostCenter.CostCenterID 	"
            str += " WHERE (Convert(varchar, dbo.tblVoucher.voucher_date, 102) BETWEEN Convert(Datetime, '" & dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND dbo.tblVoucher.Post In(" & IIf(Me.chkIncludeUnpostedVoucher.Checked = True, "1,0,NULL", "1") & ") " & IIf(MyCompanyId > 0, " AND vwCoaDetail.CompanyId=" & MyCompanyId & "", "") & ""
            str += " " & IIf(Condition = "Cash", " AND vwcoadetail.account_type = 'Cash' " & " ", "") & " "
            str += " " & IIf(Condition = "Bank", " AND vwcoadetail.account_type = 'Bank' " & " ", "") & " "
            str += " " & IIf(TransType = "Payment", "AND tblvoucherdetail.credit_amount <> 0", " AND tblvoucherdetail.debit_amount <> 0") & " "
            'str += " " & IIf(Condition = "All", " AND(vwcoadetail.account_type = 'Cash' OR vwcoadetail.account_type = 'Bank'))", "") & ""
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub lblCashPayment_Click(sender As Object, e As EventArgs) Handles lblCashPayment.Click
        Try
            ServerDate()
            Dim opening As Integer = GetAccountOpeningBalance(1, GetServerDate.Year & "-" & GetServerDate.Month & "-" & GetServerDate.Day & " 00:00:00", "Cash")
            AddRptParam("FromDate", dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptPaymentDetail", , , , , Val(opening).ToString, , GetCash("Payment", "Cash"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblBankReceipt_Click(sender As Object, e As EventArgs) Handles lblBankReceipt.Click
        Try
            ServerDate()
            Dim opening As Integer = GetAccountOpeningBalance(1, GetServerDate.Year & "-" & GetServerDate.Month & "-" & GetServerDate.Day & " 00:00:00", "Bank")
            AddRptParam("FromDate", dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptReceiptDetail", , , , , Val(opening).ToString, , GetCash("Receipt", "Bank"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblBankPayment_Click(sender As Object, e As EventArgs) Handles lblBankPayment.Click
        Try
            ServerDate()
            Dim opening As Integer = GetAccountOpeningBalance(1, GetServerDate.Year & "-" & GetServerDate.Month & "-" & GetServerDate.Day & " 00:00:00", "Bank")
            AddRptParam("FromDate", dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptPaymentDetail", , , , , Val(opening).ToString, , GetCash("Payment", "Bank"))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblSales_Click(sender As Object, e As EventArgs) Handles lblSales.Click
        Try

            Dim fromDate As String = dtpFromDate.Value.Year & "," & dtpFromDate.Value.Month & "," & dtpFromDate.Value.Day & ",0,0,0"
            Dim ToDate As String = dtpToDate.Value.Year & "," & dtpToDate.Value.Month & "," & dtpToDate.Value.Day & ",23,59,59"
            AddRptParam("FromDate", dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("SummaryOfInvoices", "{SP_SalesOfInvoiceSummary;1.SalesDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblSalesReturn_Click(sender As Object, e As EventArgs) Handles lblSalesReturn.Click
        Try
            Dim fromDate As String = dtpFromDate.Value.Year & "," & dtpFromDate.Value.Month & "," & dtpFromDate.Value.Day & ",0,0,0"
            Dim ToDate As String = dtpToDate.Value.Year & "," & dtpToDate.Value.Month & "," & dtpToDate.Value.Day & ",23,59,59"
            AddRptParam("@FromDate", dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("SaleReturnSummary", "{SaleReturnSummary;1.SalesReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblPurchase_Click(sender As Object, e As EventArgs) Handles lblPurchase.Click
        Try
            Dim fromDate As String = dtpFromDate.Value.Year & "," & dtpFromDate.Value.Month & "," & dtpFromDate.Value.Day & ",0,0,0"
            Dim ToDate As String = dtpToDate.Value.Year & "," & dtpToDate.Value.Month & "," & dtpToDate.Value.Day & ",23,59,59"
            AddRptParam("@DateFrom", Me.dtpFromDate.Value.Date.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@DateTo", Me.dtpToDate.Value.Date.ToString("yyyy-MM-dd 23:59:59"))
            ShowReport("SummaryOfPurchaseInvoices", "{SP_SummaryOfPurchaseInvoices;1.ReceivingDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblExpense_Click(sender As Object, e As EventArgs) Handles lblExpense.Click
        Try
            Dim opening As Integer = 0 'GetAccountOpeningBalance(0, Me.DateTimePicker1.Value.Year & "-" & Me.DateTimePicker1.Value.Month & "-" & Me.DateTimePicker1.Value.Day & " 00:00:00")
            AddRptParam("FromDate", dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("ToDate", dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            Dim fromDate As String = dtpFromDate.Value.Year & "," & dtpFromDate.Value.Month & "," & dtpFromDate.Value.Day & ",0,0,0"
            Dim ToDate As String = dtpToDate.Value.Year & "," & dtpToDate.Value.Month & "," & dtpToDate.Value.Day & ",23,59,59"
            ShowReport("rptExpenseStatment", "{vw_Expenses.voucher_date} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") " & IIf(Me.chkIncludeUnpostedVoucher.Checked = True, " and {vw_Expenses.post} IN [True,False]", "  and {vw_Expenses.post} IN [True]") & "", "Nothing", "Nothing", , Val(opening).ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblTodayTasks_Click(sender As Object, e As EventArgs) Handles lblTodayTasks.Click
        Try
            frmMain.LoadControl("Tasks")
            frmTasks.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblStockCount_Click(sender As Object, e As EventArgs) Handles lblStockCount.Click
        Try
            If Not frmMain.Panel2.Controls.Contains(frmGrdRptMinimumStockLevel) Then
                frmMain.LoadControl("frmGrdRptMinimumStockLevel")
            Else
                frmMain.LoadControl("frmGrdRptMinimumStockLevel")
            End If
            If Me.ComboBox1.SelectedIndex = 0 Then
                frmGrdRptMinimumStockLevel.StockLevel = "MinLevel"
            ElseIf Me.ComboBox1.SelectedIndex = 1 Then
                frmGrdRptMinimumStockLevel.StockLevel = "OptLevel"
            ElseIf Me.ComboBox1.SelectedIndex = 2 Then
                frmGrdRptMinimumStockLevel.StockLevel = "MaxLevel"
            End If
            frmGrdRptMinimumStockLevel.FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub lblPurchaseReturn_Click(sender As Object, e As EventArgs) Handles lblPurchaseReturn.Click
        Try
            Dim fromDate As String = dtpFromDate.Value.Year & "," & dtpFromDate.Value.Month & "," & dtpFromDate.Value.Day & ",0,0,0"
            Dim ToDate As String = dtpToDate.Value.Year & "," & dtpToDate.Value.Month & "," & dtpToDate.Value.Day & ",23,59,59"
            AddRptParam("@DateFrom", Me.dtpFromDate.Value.Date.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@DateTo", Me.dtpToDate.Value.Date.ToString("yyyy-MM-dd 23:59:59"))
            ShowReport("SummaryOfPurchaseReturn", "{SP_SummaryOfPurchaseReturnInvoices;1.PurchaseReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")", "Nothing", "Nothing")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub bgwNotificationCount_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwNotificationCount.DoWork
        Try

            ServerDate()

            Dim dt As New DataTable
            _dtNotifications = GetDataTable("Select Count(*) From tblNotificationsDetail WHERE IsNull(ReadMsg,0)=0 AND UserId=" & LoginUserId & "") ' AND (Convert(varchar,NotificationDate,102)=Convert(Datetime,'" & GetServerDate.ToString("yyyy-M-d 00:00:00") & "',102))")
            _dtNotifications.AcceptChanges()
            'Dim Str As String = String.Empty
            'Str = "Select NotificationId, NotificationDate, Msg, UserId, MsgRead, EntryDate From tblNotifications Where UserId= " & LoginUserId & " And IsNull(MsgRead, 0) = 0 "
            'dt = GetDataTable(Str)
            'dt.AcceptChanges()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pnlAlert_Click(sender As Object, e As EventArgs)
        'Try
        '    If Me.pnlNotifications.Visible = False Then
        '        Me.pnlNotifications.BringToFront()
        '        Me.pnlNotifications.Visible = True
        '    Else
        '        Me.pnlNotifications.SendToBack()
        '        Me.pnlNotifications.Visible = False
        '    End If
        '    ShowNotificationsList(LoginUserId)
        '    Me.tmrAlerts.Enabled = False
        '    If bgwUpdateNotifications.IsBusy Then Exit Sub
        '    bgwUpdateNotifications.RunWorkerAsync()

        '    Do While bgwUpdateNotifications.IsBusy
        '        Application.DoEvents()
        '    Loop
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles tmrAlerts.Tick
        Try
            Me.tmrAlerts.Enabled = False
            If bgwNotificationCount.IsBusy Then Exit Sub
            Me.bgwNotificationCount.RunWorkerAsync()
            Do While bgwNotificationCount.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            tmrAlerts.Enabled = True
        End Try
    End Sub

    Private Sub bgwUpdateNotifications_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwUpdateNotifications.DoWork
        Try

            'ShowNotificationsList(LoginUserId)
            UpdateReadNotifications()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub bgwNotificationCount_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwNotificationCount.RunWorkerCompleted
        Try
            If _dtNotifications.Rows.Count = 0 Then Exit Sub
            If _dtNotifications.Rows.Count > 0 Then
                If Val(_dtNotifications.Rows(0).Item(0).ToString) > 0 Then
                    Me.lblNotificationCount.Text = Val(_dtNotifications.Rows(0).Item(0).ToString)
                    'Me.lblNotifications.Text = Val(_dtNotifications.Rows(0).Item(0).ToString)
                Else
                    Me.lblNotificationCount.Text = String.Empty
                    'Me.lblNotifications.Text = String.Empty
                End If
            Else
                Me.lblNotificationCount.Text = String.Empty
                'Me.lblNotifications.Text = String.Empty
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pnlDate_Paint(sender As Object, e As PaintEventArgs) Handles pnlDate.Paint

    End Sub


    Private Sub ShowNotificationsList(ByVal userId As Integer)
        Dim lstView As New ListView
        'Dim lstViewitem As New ListViewItem
        Dim str As String = String.Empty
        Try
            str = "Select NotificationActivityName, tblNotifications.DocNo, tblNotifications.EntryDate,tblNotifications.Remarks From tblNotifications INNER JOIN tblNotificationsDetail on tblNotificationsDetail.NotificationId = tblNotifications.Id INNER JOIN tblNotificationActivity on tblNotificationActivity.NotificationActivityId = tblNotifications.NotificationActivityId WHERE tblNotificationsDetail.UserId=" & LoginUserId & " AND IsNull(tblNotificationsDetail.ReadMsg,0)=0 "
            dtForList = GetDataTable(str)
            dtForList.AcceptChanges()
            Dim lstViewitem As New ListViewItem
            'Me.lstNotifications.Visible = True
            Me.lstNotifications.Clear()
            If dtForList.Rows.Count > 0 Then
                For Each dr As DataRow In dtForList.Rows
                    Me.lstNotifications.Items.Add(dr.Item("NotificationActivityName").ToString() & " ~ " & dr.Item("DocNo").ToString() & " ~ " & dr.Item("EntryDate").ToString() & " , " & dr.Item("Remarks").ToString())
                Next
            End If
            'Me.lstNotifications.Visible = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub UpdateReadNotifications()
        Dim objcon As New OleDbConnection(Con.ConnectionString)
        If objcon.State = ConnectionState.Closed Then objcon.Open()
        Dim trans As OleDb.OleDbTransaction = objcon.BeginTransaction
        Dim cmd As New OleDbCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandTimeout = 300
        cmd.CommandType = CommandType.Text
        Try

            cmd.CommandText = String.Empty
            cmd.CommandText = "Update tblNotificationsDetail Set ReadMsg=1 WHERE UserId =" & LoginUserId & " AND IsNull(ReadMsg,0) <> 1"
            cmd.ExecuteNonQuery()

            trans.Commit()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub bgwUpdateNotifications_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwUpdateNotifications.RunWorkerCompleted
        'Dim lstViewitem As New ListViewItem
        'Me.lstNotifications.Visible = True
        'If dt.Rows.Count > 0 Then
        '    For Each dr As DataRow In dt.Rows
        '        'Me.lstNotifications.
        '        lstViewitem.Text = dr.Item("Msg").ToString()
        '        Me.lstNotifications.Items.Add(lstViewitem)
        '        'Me.lstNotifications.
        '    Next
        'End If

    End Sub

    Private Sub pnlAlert_Click1(sender As Object, e As EventArgs) Handles pnlAlert.Click
        Try
            If Me.lstNotifications.Visible = True Then
                Me.lstNotifications.Visible = False
            Else
                Me.lstNotifications.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pnlAlert_MouseHover(sender As Object, e As EventArgs) Handles pnlAlert.MouseHover
        Me.pnlAlert.BackColor = Color.AliceBlue
        'If Me.pnlAlert.BackColor = Color.AliceBlue Then
        '    Me.pnlAlert.BackColor = Color.Empty
        'End If
    End Sub



    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                'Dim searchManu As New frmSearchMenu
                frmSearchMenu._Menu = Me.txtSearch.Text
                Me.txtSearch.Text = ""
                frmSearchMenu.BringToFront()
                frmSearchMenu.ShowDialog()
                Me.txtSearch.Text = ""
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pnlAlert_MouseLeave(sender As Object, e As EventArgs) Handles pnlAlert.MouseLeave
        Me.pnlAlert.BackColor = Color.Transparent
    End Sub

    Private Sub lblNotificationCount_Click(sender As Object, e As EventArgs) Handles lblNotificationCount.Click
        Try
            Me.tmrAlerts.Enabled = False
            If Me.lstNotifications.Visible = False Then
                Me.lstNotifications.Visible = True
                Me.lstNotifications.BringToFront()
                Me.pnlDateRange.Visible = False
                Me.pnlDateRange.SendToBack()
            Else
                Me.lstNotifications.SendToBack()
                Me.lstNotifications.Visible = False
            End If
            ShowNotificationsList(LoginUserId)
            If bgwUpdateNotifications.IsBusy Then Exit Sub
            bgwUpdateNotifications.RunWorkerAsync()
            Do While bgwUpdateNotifications.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.tmrAlerts.Enabled = True
        End Try
    End Sub

    Private Sub frmDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Me.lstNotifications.Visible = True Then
                Me.lstNotifications.Visible = False
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub lstNotifications_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstNotifications.SelectedIndexChanged

    End Sub

    Private Sub bgwStockValue_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgwStockValue.RunWorkerCompleted
        Try
            If _dtStockValue.Rows.Count = 0 Then Exit Sub
            Dim dr() As DataRow
            dr = _dtStockValue.Select("Trans_Type='Stock Value'")
            If dr.Length > 0 Then
                Me.lblStockValue.Text = Math.Round(Val(dr(0).Item(1).ToString), 0)
            Else
                Me.lblStockValue.Text = 0
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub bgwStockValue_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bgwStockValue.DoWork
        Try
            GetStockValue()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lblStockValue_Click(sender As Object, e As EventArgs) Handles lblStockValue.Click
        Try
            Dim fromDate As String = dtpFromDate.Value.Year & "," & dtpFromDate.Value.Month & "," & dtpFromDate.Value.Day & ",0,0,0"
            Dim ToDate As String = dtpToDate.Value.Year & "," & dtpToDate.Value.Month & "," & dtpToDate.Value.Day & ",23,59,59"
            AddRptParam("@FromDate", Me.dtpFromDate.Value.Date.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.Date.ToString("yyyy-MM-dd 23:59:59"))
            ShowReport("DashBoardStockValue")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
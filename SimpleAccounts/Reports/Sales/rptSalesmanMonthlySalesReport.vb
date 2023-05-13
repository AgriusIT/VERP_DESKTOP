Imports SBModel
Imports SBDal
Public Class rptSalesmanMonthlySalesReport
    Implements IGeneral
    Enum EnumGrid
        Item
        Price
        Supply
        [Return]
        Sampling
        Unsold
        RetPer
        SaleValue
        Discount
        ActualSales
        GrossSales
        NetSales
    End Enum
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdRcords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdRcords.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdRcords.RecordNavigator = True
            Me.grdRcords.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdRcords.RootTable.Columns
                If col.Index <> EnumGrid.Item AndAlso col.Index <> EnumGrid.Price AndAlso col.Index <> EnumGrid.RetPer Then
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    col.FormatString = String.Empty
                    col.TotalFormatString = String.Empty
                End If
            Next
            Me.grdRcords.AutoSizeColumns()
            Me.grdRcords.RootTable.Columns(EnumGrid.Price).Visible = False
            Me.grdRcords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdRcords.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdRcords.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.grdRcords.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
            Me.grdRcords.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
            Me.grdRcords.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
            'For Each Col As Janus.Windows.GridEX.GridEXColumn In Me.grdRcords.RootTable.Columns
            '    Col.AutoSize()
            '    Col.Table.Columns(EnumGrid.Price).Visible = False
            'Col.Table.Columns(EnumGrid.Supply).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Col.Table.Columns(EnumGrid.Return).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Col.Table.Columns(EnumGrid.Sampling).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Col.Table.Columns(EnumGrid.Unsold).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Col.Table.Columns(EnumGrid.Discount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Col.Table.Columns(EnumGrid.ActualSales).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Col.Table.Columns(EnumGrid.GrossSales).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Col.Table.Columns(EnumGrid.NetSales).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Col.Table.Columns(5).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Col.Table.Columns(EnumGrid.RetPer).FormatString = "N"
            'Col.Table.Columns(EnumGrid.RetPer).TotalFormatString = "N"
            'Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim Str As String
            ''Start TFS2124
            Str = "SELECT COA_DETAIL_ID, DETAIL_TITLE FROM vwCOADetail WHERE 1=1"
            If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                Str += " AND (Account_Type = 'Customer')  "
            Else
                Str += " AND (Account_Type in('Customer','Vendor'))  "
            End If

            ''End TFS2124
            ' FillDropDown(Me.cmbSalesman, "SELECT customerID, CustomerName FROM tblcustomer ORDER BY CustomerName", True)
            FillDropDown(Me.cmbSalesman, Str, True)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub rptSalesmanMonthlySalesReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            Me.FillCombos()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnGenerateReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateReport.Click
        Try

            If Me.cmbSalesman.SelectedIndex = 0 Then
                ShowValidationMessage("Please select salesman")
                Me.cmbSalesman.Focus()
                Exit Sub
            End If

            'Dim strSQL As New System.Text.StringBuilder
            'strSQL.AppendLine(" select  articleDefTable.ArticleDescription, isnull(dispatch,0) as Supply, isnull(ret.ret,0) as [Return], ")
            'strSQL.AppendLine(" isnull(dispatch.sampling,0) as Sampling, isnull(damage.dmg,0) as Unsold, ")
            'strSQL.AppendLine(" (isnull(ret.ret,0) * 100)/ case when ( isnull(dispatch.dispatch,1) = 0 ) ")
            'strSQL.AppendLine(" then 1 else isnull(dispatch.dispatch,1) end  as [Return %]  ")
            'strSQL.AppendLine(" from articleDefTable INNER JOIN ArticleGroupDefTable ON ArticleDefTable.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId left outer join ")
            'strSQL.AppendLine(" (SELECT     SUM(SalesDetailTable.Qty * SalesDetailTable.Price) AS dispatch, articledefid  , ")
            'strSQL.AppendLine(" sum(isnull(SampleQty,0)) as Sampling ")
            'strSQL.AppendLine(" FROM SalesDetailTable INNER JOIN ")
            'strSQL.AppendLine(" SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId ")
            'strSQL.AppendLine(" WHERE (convert(varchar,SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            'strSQL.AppendLine(" and customerCode = " & Me.cmbSalesman.SelectedValue)
            'strSQL.AppendLine(" GROUP BY articledefid) as dispatch on articleDefTable.articleid = dispatch.articledefid left outer join ")
            'strSQL.AppendLine(" (SELECT SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.Price) AS Ret, articledefid ")
            'strSQL.AppendLine(" FROM SalesReturnMasterTable INNER JOIN ")
            'strSQL.AppendLine(" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId ")
            'strSQL.AppendLine(" WHERE (convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            'strSQL.AppendLine(" and customerCode = " & Me.cmbSalesman.SelectedValue)
            'strSQL.AppendLine(" GROUP BY articledefid) as Ret on articleDefTable.articleid= ret.articledefid left outer join ")
            'strSQL.AppendLine(" (SELECT SUM(SalesReturnDetailTable.Qty * SalesReturnDetailTable.Price) AS Dmg, articledefid ")
            'strSQL.AppendLine(" FROM SalesReturnMasterTable INNER JOIN ")
            'strSQL.AppendLine(" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = SalesReturnMasterTable.locationID  ")
            'strSQL.AppendLine(" WHERE convert(varchar , SalesReturnMasterTable.SalesReturnDate,102)  BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) ")
            'strSQL.AppendLine(" and customerCode = " & Me.cmbSalesman.SelectedValue & "  and tbldeflocation.location_type = 'Damage'")
            'strSQL.AppendLine(" GROUP BY articledefid) as Damage  on articleDefTable.articleid = Damage.articledefid ")
            'strSQL.AppendLine(" WHERE ArticleGroupDefTable.SalesItem=1 ")

            'Dim strSQL As New System.Text.StringBuilder
            'strSQL.AppendLine(" select  articleDefTable.ArticleDescription, articleDefTable.SalePrice as Price, IsNull(Dispatch.SalesQty,0) as SalesQty, IsNull(Ret.ReturnQty,0) as ReturnQty,")
            'strSQL.AppendLine(" isnull(dispatch.sampling,0) as Sampling, IsNull(Damage.DemageQty,0) as DemageQty, ")
            'strSQL.AppendLine(" Round((isnull(ret.ret,0) * 100)/ case when (isnull(dispatch.dispatch,1)=0) ")
            'strSQL.AppendLine(" then 1 else isnull(dispatch.dispatch,1) end ,0)  as [Return %], (ISNULL(dispatch.dispatch,0) - (ISNULL(Damage.dmg,0)+ISNULL(Ret.Ret,0))) as [Gross Sale],  Round(ISNULL(dispatch.discount,0) - (IsNull(Ret.ReturnMc,0) + IsNull(Damage.DemageMc,0)),0) as Discount ")
            'strSQL.AppendLine(" from articleDefTable INNER JOIN ArticleGroupDefTable ON ArticleDefTable.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId left outer join ")
            'strSQL.AppendLine(" (SELECT SUM(ISNULL(SalesDetailTable.Qty,0) * ISNULL(SalesDetailTable.CurrentPrice,0)) AS dispatch, SUM(ISNULL(SalesDetailTable.Qty,0)) as SalesQty, (Case When Sum(SalesDetailTable.Qty) <> 0 then Sum((ISNULL(SalesDetailTable.Qty,0)* (IsNull(SalesDetailTable.CurrentPrice,0)-IsNull(SalesDetailTable.Price,0)))) else 0 end) as Discount, articledefid  , ")
            'strSQL.AppendLine(" sum(isnull(SampleQty,0)) as Sampling ")
            'strSQL.AppendLine(" FROM SalesDetailTable INNER JOIN ")
            'strSQL.AppendLine(" SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId ")
            'strSQL.AppendLine(" WHERE (convert(varchar,SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            'strSQL.AppendLine(" and customerCode = " & Me.cmbSalesman.SelectedValue)
            'strSQL.AppendLine(" GROUP BY articledefid) as dispatch on articleDefTable.articleid = dispatch.articledefid left outer join ")
            'strSQL.AppendLine(" (SELECT SUM(ISNULL(SalesReturnDetailTable.Qty,0) * ISNULL(SalesReturnDetailTable.CurrentPrice,0)) AS Ret,  SUM(IsNull(SalesReturnDetailTable.Qty,0)) As ReturnQty, (Case When Sum(SalesReturnDetailTable.Qty) <> 0 then Sum((ISNULL(SalesReturnDetailTable.Qty,0)* (IsNull(SalesReturnDetailTable.CurrentPrice,0)-IsNull(SalesReturnDetailTable.Price,0)))) else 0 end) as ReturnMc, articledefid ")
            'strSQL.AppendLine(" FROM SalesReturnMasterTable INNER JOIN ")
            'strSQL.AppendLine(" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId INNER JOIN tblDefLocation ON SalesReturnDetailTable.LocationId = tblDefLocation.Location_Id ")
            'strSQL.AppendLine(" WHERE (convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            'strSQL.AppendLine(" and customerCode = " & Me.cmbSalesman.SelectedValue & " AND tblDefLocation.Location_Type='Damage' ")
            'strSQL.AppendLine(" GROUP BY articledefid) as Ret on articleDefTable.articleid= ret.articledefid left outer join ")
            'strSQL.AppendLine(" (SELECT SUM(ISNULL(SalesReturnDetailTable.Qty,0) * ISNULL(SalesReturnDetailTable.CurrentPrice,0)) AS Dmg, SUM(IsNull(SalesReturnDetailTable.Qty,0)) As DemageQty, (Case When Sum(SalesReturnDetailTable.Qty) <> 0 then Sum((ISNULL(SalesReturnDetailTable.Qty,0)* (IsNull(SalesReturnDetailTable.CurrentPrice,0)-IsNull(SalesReturnDetailTable.Price,0)))) else 0 end) as DemageMc, articledefid ")
            'strSQL.AppendLine(" FROM SalesReturnMasterTable INNER JOIN ")
            'strSQL.AppendLine(" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join ")
            'strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  ")
            'strSQL.AppendLine(" WHERE convert(varchar , SalesReturnMasterTable.SalesReturnDate,102)  BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) ")
            'strSQL.AppendLine(" and customerCode = " & Me.cmbSalesman.SelectedValue & "  and tbldeflocation.location_type <> 'Damage'")
            'strSQL.AppendLine(" GROUP BY articledefid) as Damage  on articleDefTable.articleid = Damage.articledefid ")
            'strSQL.AppendLine(" WHERE ArticleGroupDefTable.SalesItem=1 ")
            'strSQL.AppendLine(" ORDER BY articleDefTable.SortOrder, articleDefTable.ArticleCode ")

            'Dim dt As New DataTable
            'dt.Clear()
            'dt = GetDataTable(strSQL.ToString())
            'dt.AcceptChanges()
            'dt.Columns.Add("ActualSales", GetType(System.Double))
            'dt.Columns.Add("GrossSales", GetType(System.Double))
            'dt.Columns.Add("NetSales", GetType(System.Double))
            'dt.Columns(EnumGrid.ActualSales).Expression = "(SalesQty-(ReturnQty+DemageQty))"
            'dt.Columns(EnumGrid.GrossSales).Expression = "[Gross Sale]" '"((SalesQty-(ReturnQty+DemageQty))* Price)"
            'dt.Columns(EnumGrid.NetSales).Expression = "([Gross Sale]-Discount)"
            Dim dt As DataTable = Getall()
            Me.grdRcords.DataSource = dt
            'Me.grdRcords.RetrieveStructure()
            Me.ApplyGridSettings()
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function Getall() As DataTable
        Try
            Dim strSQL As New System.Text.StringBuilder
            strSQL.AppendLine(" select  articleDefTable.ArticleDescription, articleDefTable.SalePrice as Price, IsNull(Dispatch.SalesQty,0) as SalesQty, IsNull(Ret.ReturnQty,0) as ReturnQty,")
            strSQL.AppendLine(" isnull(dispatch.sampling,0) as Sampling, IsNull(Damage.DemageQty,0) as DemageQty, ")
            strSQL.AppendLine(" Round((isnull(ret.ret,0) * 100)/ case when (isnull(dispatch.dispatch,1)=0) ")
            strSQL.AppendLine(" then 1 else isnull(dispatch.dispatch,1) end ,0)  as [Return %], (ISNULL(dispatch.dispatch,0) - (ISNULL(Damage.dmg,0)+ISNULL(Ret.Ret,0))) as [Gross Sale],  Round(ISNULL(dispatch.discount,0) - (IsNull(Ret.ReturnMc,0) + IsNull(Damage.DemageMc,0)),0) as Discount ")
            strSQL.AppendLine(" from articleDefTable INNER JOIN ArticleGroupDefTable ON ArticleDefTable.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId left outer join ")
            strSQL.AppendLine(" (SELECT SUM(ISNULL(SalesDetailTable.Qty,0) * ISNULL(SalesDetailTable.CurrentPrice,0)) AS dispatch, SUM(ISNULL(SalesDetailTable.Qty,0)) as SalesQty, (Case When Sum(SalesDetailTable.Qty) <> 0 then Sum((ISNULL(SalesDetailTable.Qty,0)* (IsNull(SalesDetailTable.CurrentPrice,0)-IsNull(SalesDetailTable.Price,0)))) else 0 end) as Discount, articledefid  , ")
            strSQL.AppendLine(" sum(isnull(SampleQty,0)) as Sampling ")
            strSQL.AppendLine(" FROM SalesDetailTable INNER JOIN ")
            strSQL.AppendLine(" SalesMasterTable ON SalesDetailTable.SalesId = SalesMasterTable.SalesId ")
            strSQL.AppendLine(" WHERE (convert(varchar,SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            strSQL.AppendLine(" and customerCode = " & Me.cmbSalesman.SelectedValue)
            strSQL.AppendLine(" GROUP BY articledefid) as dispatch on articleDefTable.articleid = dispatch.articledefid left outer join ")
            strSQL.AppendLine(" (SELECT SUM(ISNULL(SalesReturnDetailTable.Qty,0) * ISNULL(SalesReturnDetailTable.CurrentPrice,0)) AS Ret,  SUM(IsNull(SalesReturnDetailTable.Qty,0)) As ReturnQty, (Case When Sum(SalesReturnDetailTable.Qty) <> 0 then Sum((ISNULL(SalesReturnDetailTable.Qty,0)* (IsNull(SalesReturnDetailTable.CurrentPrice,0)-IsNull(SalesReturnDetailTable.Price,0)))) else 0 end) as ReturnMc, articledefid ")
            strSQL.AppendLine(" FROM SalesReturnMasterTable INNER JOIN ")
            strSQL.AppendLine(" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId INNER JOIN tblDefLocation ON SalesReturnDetailTable.LocationId = tblDefLocation.Location_Id ")
            strSQL.AppendLine(" WHERE (convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) ")
            strSQL.AppendLine(" and customerCode = " & Me.cmbSalesman.SelectedValue & " AND tblDefLocation.Location_Type='Damage' ")
            strSQL.AppendLine(" GROUP BY articledefid) as Ret on articleDefTable.articleid= ret.articledefid left outer join ")
            strSQL.AppendLine(" (SELECT SUM(ISNULL(SalesReturnDetailTable.Qty,0) * ISNULL(SalesReturnDetailTable.CurrentPrice,0)) AS Dmg, SUM(IsNull(SalesReturnDetailTable.Qty,0)) As DemageQty, (Case When Sum(SalesReturnDetailTable.Qty) <> 0 then Sum((ISNULL(SalesReturnDetailTable.Qty,0)* (IsNull(SalesReturnDetailTable.CurrentPrice,0)-IsNull(SalesReturnDetailTable.Price,0)))) else 0 end) as DemageMc, articledefid ")
            strSQL.AppendLine(" FROM SalesReturnMasterTable INNER JOIN ")
            strSQL.AppendLine(" SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId inner join ")
            strSQL.AppendLine(" tbldeflocation on tbldeflocation.location_id = SalesReturnDetailTable.locationID  ")
            strSQL.AppendLine(" WHERE convert(varchar , SalesReturnMasterTable.SalesReturnDate,102)  BETWEEN Convert(Datetime, '" & Me.dtpFrom.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.Date.ToString("dd/MMM/yyyy") & "', 102) ")
            strSQL.AppendLine(" and customerCode = " & Me.cmbSalesman.SelectedValue & "  and tbldeflocation.location_type <> 'Damage'")
            strSQL.AppendLine(" GROUP BY articledefid) as Damage  on articleDefTable.articleid = Damage.articledefid ")
            strSQL.AppendLine(" WHERE ArticleGroupDefTable.SalesItem=1 ")
            strSQL.AppendLine(" AND (IsNull(Dispatch.SalesQty,0) <> 0 Or IsNull(Ret.ReturnQty,0) <> 0 Or IsNull(Damage.DemageQty,0) <> 0 )")
            strSQL.AppendLine(" ORDER BY articleDefTable.SortOrder, articleDefTable.ArticleCode ")

            Dim dt As New DataTable
            dt.Clear()
            dt = GetDataTable(strSQL.ToString())
            dt.AcceptChanges()
            dt.Columns.Add("ActualSales", GetType(System.Double))
            dt.Columns.Add("GrossSales", GetType(System.Double))
            dt.Columns.Add("NetSales", GetType(System.Double))
            dt.Columns(EnumGrid.ActualSales).Expression = "(SalesQty-(ReturnQty+DemageQty))"
            dt.Columns(EnumGrid.GrossSales).Expression = "[Gross Sale]" '"((SalesQty-(ReturnQty+DemageQty))* Price)"
            dt.Columns(EnumGrid.NetSales).Expression = "([Gross Sale]-Discount)"
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        Try
            Me.cmbSalesman.SelectedIndex = 0
            Me.grdRcords.DataSource = Nothing
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Dim id As Integer = 0
            id = Me.cmbSalesman.SelectedValue
            FillDropDown(Me.cmbSalesman, "SELECT COA_DETAIL_ID, DETAIL_TITLE FROM vwCOADetail WHERE ACCOUNT_TYPE='Customer'", True)
            Me.cmbSalesman.SelectedValue = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try

            Me.CtrlGrdBar1.txtGridTitle.Text = "Salesman Monthly Sales"
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdRcords.Name) Then
                Dim fs1 As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdRcords.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdRcords.LoadLayoutFile(fs1)
                fs1.Close()
                fs1.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "SaleMan Monthly Sales Report" & Chr(10) & "Sales Man " & Me.cmbSalesman.Text & "" & Chr(10) & "Date From: " & Me.dtpFrom.Value.ToString("dd-MM-yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd-Mm-yyyy") & " "
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim dt As DataTable = Getall()
            dt.Columns.Add("Customer", GetType(System.String))
            For Each row As DataRow In dt.Rows
                row.BeginEdit()
                row("Customer") = Me.cmbSalesman.Text
                row.EndEdit()
            Next
            AddRptParam("@FromDate", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptSalesmanMonthlyReport", , , , , , , dt)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
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
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.btnGenerateReport.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.Visible = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Me.btnGenerateReport.Enabled = False
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
            Else
                Me.Visible = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.btnGenerateReport.Enabled = False
                Me.btnPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Grid Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Grid Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Show" Then
                        Me.btnGenerateReport.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
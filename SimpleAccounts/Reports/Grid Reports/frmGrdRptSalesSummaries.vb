''11-Sep-2014 Task:2837 Imran Ali Bug Fixe In Sales Summaries Report
''23-Sep-2014 Task:2853 Imran Ali Group Total In Sales Summareis Report
Public Class frmGrdRptSalesSummaries
    Enum enmSalesSummary
        ID
        Description
        Count
    End Enum
    Enum enmCustomer
        PK_ID
        Customer
        Director
        Manager
        Region
        City
        Area
        Saleman
        DirectorId
        ManagerId
        EmployeeCode
        StateId
        CityId
        AreaId
        Count
    End Enum
    Private Sub frmGrdRptSalesSummaries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.rbtItemType.Checked = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillData()
        Try

            Dim intTotalMonth As Integer = Me.dtpDateTo.Value.Subtract(Me.dtpDateForm.Value).Days
            Dim m_date As New List(Of DateTime)
            If intTotalMonth >= 1 Then
                For i As Integer = 0 To intTotalMonth - 1
                    If i = 0 Then
                        m_date.Add(Me.dtpDateForm.Value.AddDays(i))
                    Else
                        m_date.Add(Me.dtpDateForm.Value.AddDays(i))
                    End If
                Next
            Else
                If Not intTotalMonth < 0 Then
                    m_date.Add(Me.dtpDateForm.Value)
                Else
                    Throw New Exception("Some of data is not provided.")
                End If
            End If

            Dim str As String = String.Empty
            If Me.rbtItemType.Checked = True Then
                str = "Select ArticleTypeId as PK_ID, ArticleTypeName as [Article Type] From ArticleTypeDefTable WHERE ArticleTypeId In (Select DISTINCT ArticleTypeId From ArticleDefTable WHERE ArticleId In(Select DISTINCT ArticleDefId From SalesDetailTable)) ORDER BY 2 ASC"
            ElseIf Me.rbtItem.Checked = True Then
                str = "Select ArticleId as PK_ID, ArticleDescription + ' ~ ' + ArticleCode as [Article Description]  From ArticleDefView WHERE ArticleId In(Select DISTINCT ArticleDefID From SalesDetailTable) ORDER BY 2 ASC"
            ElseIf Me.rbtSaleman.Checked = True Then
                str = "Select Employee_Id as PK_ID, Employee_Name + ' ~ ' + Employee_Code as [Employee] From tblDefEmployee WHERE Employee_Id In(Select IsNull(SaleMan,0) as SaleMan From tblCustomer) ORDER BY 2 ASC"
            ElseIf Me.rbtManager.Checked = True Then
                str = "Select Employee_Id as PK_ID, Employee_Name + ' ~ ' + Employee_Code as [Employee] From tblDefEmployee WHERE Employee_ID In(Select IsNull(Manager,0) as Manager From tblCustomer) ORDER BY 2 ASC"
            ElseIf Me.rbtCustomer.Checked = True Or Me.rbtArea.Checked = True Then
                'str = "SELECT DISTINCT dbo.vwCOADetail.coa_detail_id AS PK_ID, " _
                '    & " dbo.vwCOADetail.detail_title AS Customer," _
                '    & " Emp.Employee_Name AS Manager, " _
                '    & " dbo.tblDefEmployee.Employee_Name AS Saleman, " _
                '    & " dbo.tblListState.StateName AS Region,  " _
                '    & " dbo.tblListCity.CityName AS City,   " _
                '    & " dbo.tblListTerritory.TerritoryName AS Area, " _
                '    & " ISNULL(dbo.SalesMasterTable.EmployeeCode, 0) AS EmployeeCode, " _
                '    & " ISNULL(dbo.tblListCity.StateId, 0) AS StateId, " _
                '    & " ISNULL(dbo.tblListTerritory.CityId, 0) AS CityId, " _
                '    & " ISNULL(dbo.tblListTerritory.TerritoryId, 0) AS AreaId, " _
                '    & " ISNULL(tblDefEmployee.RefEmployeeId, 0) AS ManagerId  " _
                '    & " FROM dbo.SalesMasterTable " _
                '    & " LEFT OUTER JOIN  dbo.tblDefEmployee ON dbo.SalesMasterTable.EmployeeCode = dbo.tblDefEmployee.Employee_ID  " _
                '    & " LEFT OUTER JOIN  dbo.tblCustomer ON dbo.SalesMasterTable.CustomerCode = dbo.tblCustomer.AccountId " _
                '    & " RIGHT OUTER JOIN  dbo.tblListState LEFT OUTER JOIN  dbo.tblListCity ON dbo.tblListState.StateId = dbo.tblListCity.StateId " _
                '    & " LEFT OUTER JOIN  dbo.tblListTerritory ON dbo.tblListCity.CityId = dbo.tblListTerritory.CityId ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId " _
                '    & " RIGHT OUTER JOIN  dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " _
                '    & " LEFT OUTER JOIN tblDefEmployee Emp on Emp.Employee_Id  = tblDefEmployee.RefEmployeeId " _
                '    & " WHERE (dbo.vwCOADetail.account_type = 'Customer')   "
                str = "Select coa_detail_id as PK_ID, detail_title as Customer, EmpDct.Employee_Name as Director, EmpMgr.Employee_Name as Manager, StateName as Region, CityName as City, TerritoryName as Area, EmpSaleMan.Employee_Name as SaleMan, IsNull(Director,0) as DirectorId, IsNull(Manager,0) as ManagerId, IsNull(SaleMan,0) as Employee_Code, IsNull(COA.StateId,0) as StateId, IsNull(COA.CityId,0) as CityId, IsNull(TerritoryId,0) as TerritoryId From vwCOADetail COA LEFT OUTER JOIN tblDefEmployee EmpMgr on EmpMgr.Employee_Id =COA.Manager LEFT OUTER JOIN tblDefEmployee EmpDct On EmpDct.Employee_Id = COA.Director LEFT OUTER JOIN tblDefEmployee EmpSaleMan on EmpSaleMan.Employee_Id = COA.SaleMan WHERE COA.detail_title <> '' "
                ''11-Sep-2014 Task:2837 Imran Ali Bug Fixe In Sales Summaries Report
                If getConfigValueByType("Show Vendor On Sales").ToString = "True" Then
                    str += " AND COA.Account_Type IN('Customer','Vendor')"
                Else
                    str += " AND COA.Account_Type IN('Customer')"
                End If
                'End Task:2837
            End If

                Dim dt As New DataTable
                If str.Length > 0 Then
                    dt = GetDataTable(str)
                    For Each mdate As DateTime In m_date
                        If Not dt.Columns.Contains(mdate.ToString("MMM-yy")) Then
                            dt.Columns.Add(mdate.ToString("M-yyyy").ToString, GetType(System.String))
                            dt.Columns.Add(mdate.ToString("MMM-yy"), GetType(System.Double))
                        End If
                    Next
                    If Me.rbtCustomer.Checked = False Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            For c As Integer = enmSalesSummary.Count To dt.Columns.Count - 2 Step 2
                                dt.Rows(i).BeginEdit()
                                dt.Rows(i).Item(c + 1) = 0
                                dt.Rows(i).EndEdit()
                            Next
                        Next
                    Else
                        For i As Integer = 0 To dt.Rows.Count - 1
                            For c As Integer = enmCustomer.Count To dt.Columns.Count - 2 Step 2
                                dt.Rows(i).BeginEdit()
                                dt.Rows(i).Item(c + 1) = 0
                                dt.Rows(i).EndEdit()
                            Next
                        Next
                    End If
                    dt.AcceptChanges()

                    Dim strSQL As String = String.Empty

                    If Me.rbtItemType.Checked = True Then
                        strSQL = "SELECT dbo.ArticleDefView.ArticleTypeId, SUM(ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)) AS Amount, " _
                          & " CONVERT(varchar, MONTH(dbo.SalesMasterTable.SalesDate)) + '-' + CONVERT(varchar, YEAR(dbo.SalesMasterTable.SalesDate)) AS Month, SUM(((IsNull(TaxPercent,0)/100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)))) as TaxAmount  " _
                          & " FROM dbo.ArticleDefView INNER JOIN  dbo.SalesDetailTable ON dbo.ArticleDefView.ArticleId = dbo.SalesDetailTable.ArticleDefId INNER JOIN " _
                          & " dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId WHERE (Convert(varchar, SalesDate,102) BETWEEN Convert(datetime, '" & Me.dtpDateForm.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(datetime,'" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  GROUP BY dbo.ArticleDefView.ArticleTypeId, dbo.ArticleDefView.ArticleTypeName, CONVERT(varchar, MONTH(dbo.SalesMasterTable.SalesDate)) + '-' + CONVERT(varchar,YEAR(dbo.SalesMasterTable.SalesDate)) "
                    ElseIf Me.rbtItem.Checked = True Then
                        strSQL = " SELECT dbo.ArticleDefView.ArticleId, SUM(ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)) AS Amount, " _
                               & " CONVERT(varchar, MONTH(dbo.SalesMasterTable.SalesDate)) + '-' + CONVERT(varchar, YEAR(dbo.SalesMasterTable.SalesDate)) AS Month, SUM(((IsNull(TaxPercent,0)/100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)))) as TaxAmount   " _
                               & " FROM dbo.ArticleDefView INNER JOIN  dbo.SalesDetailTable ON dbo.ArticleDefView.ArticleId = dbo.SalesDetailTable.ArticleDefId INNER JOIN  " _
                               & " dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId WHERE (Convert(varchar, SalesDate,102) BETWEEN Convert(datetime, '" & Me.dtpDateForm.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(datetime,'" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "',102))  GROUP BY dbo.ArticleDefView.ArticleId, dbo.ArticleDefView.ArticleDescription, CONVERT(varchar, MONTH(dbo.SalesMasterTable.SalesDate)) + '-' + CONVERT(varchar,YEAR(dbo.SalesMasterTable.SalesDate)) "
                    ElseIf Me.rbtSaleman.Checked = True Then
                    strSQL = "SELECT IsNull(dbo.tblCustomer.SaleMan,0) as EmployeeCode,SUM(ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)) AS Amount, CONVERT(varchar, MONTH(dbo.SalesMasterTable.SalesDate)) " _
                           & " + '-' + CONVERT(varchar, YEAR(dbo.SalesMasterTable.SalesDate)) AS Month, SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100)  * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0))) AS TaxAmount " _
                           & " FROM dbo.SalesDetailTable INNER JOIN dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId INNER JOIN  dbo.tblCustomer ON dbo.SalesMasterTable.CustomerCode = dbo.tblCustomer.AccountId " _
                           & " WHERE (CONVERT(varchar, dbo.SalesMasterTable.SalesDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpDateForm.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(datetime, '" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY CONVERT(varchar, MONTH(dbo.SalesMasterTable.SalesDate)) + '-' + CONVERT(varchar, YEAR(dbo.SalesMasterTable.SalesDate)), IsNull(dbo.tblCustomer.SaleMan,0)"
                    ElseIf Me.rbtManager.Checked = True Then
                        strSQL = "SELECT IsNull(dbo.tblCustomer.Manager,0) as EmployeeCode, SUM(ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)) AS Amount, CONVERT(varchar, MONTH(dbo.SalesMasterTable.SalesDate)) " _
                          & " + '-' + CONVERT(varchar, YEAR(dbo.SalesMasterTable.SalesDate)) AS Month, SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100)  * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0))) AS TaxAmount " _
                          & " FROM dbo.SalesDetailTable INNER JOIN dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId INNER JOIN  dbo.tblCustomer ON dbo.SalesMasterTable.CustomerCode = dbo.tblCustomer.AccountId " _
                          & " WHERE (CONVERT(varchar, dbo.SalesMasterTable.SalesDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpDateForm.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(datetime, '" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY CONVERT(varchar, MONTH(dbo.SalesMasterTable.SalesDate)) + '-' + CONVERT(varchar, YEAR(dbo.SalesMasterTable.SalesDate)),IsNull(dbo.tblCustomer.Manager,0)"
                    ElseIf Me.rbtCustomer.Checked = True Or Me.rbtArea.Checked = True Then
                        'strSQL = "SELECT DISTINCT IsNull(dbo.SalesMasterTable.CustomerCode,0) as CustomerCode,SUM(ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0)) AS Amount, " _
                        '  & " CONVERT(varchar, MONTH(dbo.SalesMasterTable.SalesDate)) + '-' + CONVERT(varchar, YEAR(dbo.SalesMasterTable.SalesDate)) AS Month,  " _
                        '  & " SUM((ISNULL(dbo.SalesDetailTable.TaxPercent, 0) / 100) * (ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.Price, 0))) AS TaxAmount,  " _
                        '  & " IsNull(dbo.tblListState.StateId,0) as StateId, IsNull(dbo.tblListCity.CityId,0) as CityId, IsNull(dbo.tblListTerritory.TerritoryId,0) as TerritoryId, IsNull(dbo.SalesMasterTable.EmployeeCode,0) as SalemanId,   " _
                        '  & " IsNull(tblDefEmployee.RefEmployeeId,0) as ManagerId " _
                        '  & " FROM  dbo.tblCustomer RIGHT OUTER JOIN " _
                        '  & " dbo.SalesDetailTable INNER JOIN " _
                        '  & " dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId LEFT OUTER JOIN " _
                        '  & " dbo.tblDefEmployee ON dbo.SalesMasterTable.EmployeeCode = dbo.tblDefEmployee.Employee_ID INNER JOIN " _
                        '  & " dbo.vwCOADetail ON dbo.SalesMasterTable.CustomerCode = dbo.vwCOADetail.coa_detail_id ON  " _
                        '  & " dbo.tblCustomer.AccountId = dbo.SalesMasterTable.CustomerCode LEFT OUTER JOIN " _
                        '  & " dbo.tblListTerritory LEFT OUTER JOIN " _
                        '  & " dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId LEFT OUTER JOIN " _
                        '  & " dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId " _
                        '  & " WHERE (CONVERT(varchar, dbo.SalesMasterTable.SalesDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpDateForm.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(datetime, '" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102))    " _
                        '  & " GROUP BY CONVERT(varchar, MONTH(dbo.SalesMasterTable.SalesDate)) + '-' + CONVERT(varchar, YEAR(dbo.SalesMasterTable.SalesDate)),  IsNull(dbo.SalesMasterTable.EmployeeCode,0), IsNull(dbo.tblListState.StateId,0), IsNull(dbo.tblListCity.CityId,0),   IsNull(dbo.tblListTerritory.TerritoryId,0), IsNull(tblDefEmployee.RefEmployeeId,0) , IsNull(dbo.SalesMasterTable.CustomerCode,0) "

                        strSQL = "SELECT dbo.SalesMasterTable.CustomerCode,  SUM(dbo.SalesDetailTable.Qty * dbo.SalesDetailTable.Price) AS Amount,  " _
                         & " CONVERT(varchar, MONTH(dbo.SalesMasterTable.SalesDate)) + '-' + CONVERT(varchar, " _
                          & " YEAR(dbo.SalesMasterTable.SalesDate)) AS Month," _
                          & " SUM((IsNull(dbo.SalesDetailTable.TaxPercent,0) / 100) * (IsNull(dbo.SalesDetailTable.Qty,0) * IsNull(dbo.SalesDetailTable.Price,0))) AS SalesTax, dbo.vwCOADetail.Director,  " _
                          & " dbo.vwCOADetail.Manager, dbo.vwCOADetail.SaleMan, dbo.vwCOADetail.StateId, dbo.vwCOADetail.CityId, dbo.vwCOADetail.TerritoryId " _
                          & " FROM dbo.SalesMasterTable INNER JOIN " _
                          & " dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId INNER JOIN " _
                          & " dbo.vwCOADetail ON dbo.SalesMasterTable.CustomerCode = dbo.vwCOADetail.coa_detail_id " _
                          & " WHERE (CONVERT(varchar, dbo.SalesMasterTable.SalesDate, 102) BETWEEN CONVERT(datetime, '" & Me.dtpDateForm.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND CONVERT(datetime, '" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " _
                          & " GROUP BY dbo.SalesMasterTable.CustomerCode, CONVERT(varchar, MONTH(dbo.SalesMasterTable.SalesDate)) + '-' + CONVERT(varchar,  " _
                          & " YEAR(dbo.SalesMasterTable.SalesDate)), dbo.vwCOADetail.Director, dbo.vwCOADetail.Manager, dbo.vwCOADetail.SaleMan, dbo.vwCOADetail.StateId,  " _
                          & " dbo.vwCOADetail.CityId, dbo.vwCOADetail.TerritoryId "


                    End If

                    Dim dtData As New DataTable
                    If strSQL.Length > 0 Then
                        dtData = GetDataTable(strSQL)
                    Else
                        Throw New Exception("Some of data is not provided.")
                    End If
                    Dim dr() As DataRow
                    For Each r As DataRow In dt.Rows
                        'If rbtCustomer.Checked = False Then
                        dr = dtData.Select("" & dtData.Columns(0).ColumnName.ToString & "=" & Val(r(0).ToString) & "")
                        'Else
                        '    'dr = dtData.Select("" & dtData.Columns(0).ColumnName.ToString & "=" & Val(r(0).ToString) & " AND Saleman= " & Val(r.Item("EmployeeCode").ToString) & " AND StateId=" & Val(r.Item("StateId").ToString) & " AND CityId=" & Val(r.Item("CityId").ToString) & " AND TerritoryId=" & Val(r.Item("AreaId").ToString) & " AND Manager=" & Val(r.Item("ManagerId").ToString) & "")
                        'End If
                        If dr IsNot Nothing Then
                            If dr.Length > 0 Then
                                For Each drFound As DataRow In dr
                                    r(dt.Columns.IndexOf(drFound(2)) + 1) = (Val(drFound(1).ToString) + Val(drFound(3).ToString))
                                Next
                            End If
                        End If
                    Next
                    dt.AcceptChanges()

                    Dim strTotal As String = String.Empty
                ''11-Sep-2014 Task:2837 Imran Ali Bug Fixe In Sales Summaries Report
                If Me.rbtCustomer.Checked = False AndAlso Me.rbtArea.Checked = False Then
                    'End Task:2837
                    For c As Integer = enmSalesSummary.Count To dt.Columns.Count - 2 Step 2
                        If strTotal.Length > 0 Then
                            strTotal += "+[" & dt.Columns(c + 1).ColumnName.ToString & "]"
                        Else
                            strTotal = "[" & dt.Columns(c + 1).ColumnName.ToString & "]"
                        End If
                    Next
                Else
                    For c As Integer = enmCustomer.Count To dt.Columns.Count - 2 Step 2
                        If strTotal.Length > 0 Then
                            strTotal += "+[" & dt.Columns(c + 1).ColumnName.ToString & "]"
                        Else
                            strTotal = "[" & dt.Columns(c + 1).ColumnName.ToString & "]"
                        End If
                    Next
                End If


                    dt.Columns.Add("Total", GetType(System.Double))
                    dt.Columns.Add("Avg", GetType(System.Double))
                If Me.rbtCustomer.Checked = False AndAlso Me.rbtArea.Checked = False Then
                    If strTotal.Length > 0 Then
                        dt.Columns("Total").Expression = strTotal.ToString
                        dt.Columns("Avg").Expression = "[Total]/" & (((dt.Columns.Count - enmSalesSummary.Count) - 2) / 2) & ""
                    End If
                Else
                    If strTotal.Length > 0 Then
                        dt.Columns("Total").Expression = strTotal.ToString
                        dt.Columns("Avg").Expression = "[Total]/" & (((dt.Columns.Count - enmCustomer.Count) - 2) / 2) & ""
                    End If
                End If
                    dt.AcceptChanges()
                    Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()

                Me.grd.RootTable.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always 'Task:2853 Setting Group Total 

                    Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
                    Me.grd.RootTable.Columns(enmCustomer.PK_ID).Visible = False
                If Me.rbtCustomer.Checked = True Or Me.rbtArea.Checked = True Then
                    Me.grd.RootTable.Columns(enmCustomer.EmployeeCode).Visible = False
                    Me.grd.RootTable.Columns(enmCustomer.ManagerId).Visible = False
                    Me.grd.RootTable.Columns(enmCustomer.StateId).Visible = False
                    Me.grd.RootTable.Columns(enmCustomer.AreaId).Visible = False
                    Me.grd.RootTable.Columns(enmCustomer.CityId).Visible = False
                    Me.grd.RootTable.Columns(enmCustomer.ManagerId).Visible = False
                    Me.grd.RootTable.Columns(enmCustomer.DirectorId).Visible = False
                End If
                If Me.rbtCustomer.Checked = False Then
                    For c As Integer = enmSalesSummary.Count To Me.grd.RootTable.Columns.Count - 2 Step 2
                        If Me.grd.RootTable.Columns(c).DataMember <> "Total" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "Avg" Then
                            Me.grd.RootTable.Columns(c).Visible = False
                            Me.grd.RootTable.Columns(c + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                            Me.grd.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                            Me.grd.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                            Me.grd.RootTable.Columns(c + 1).FormatString = "N" & DecimalPointInValue
                            Me.grd.RootTable.Columns(c + 1).FormatString = "N" & DecimalPointInValue
                            Me.grd.RootTable.Columns(c + 1).TotalFormatString = "N" & DecimalPointInValue
                            Me.grd.RootTable.Columns(c + 1).TotalFormatString = "N" & DecimalPointInValue
                        End If
                    Next
                Else
                    For c As Integer = enmCustomer.Count To Me.grd.RootTable.Columns.Count - 2 Step 2
                        If Me.grd.RootTable.Columns(c).DataMember <> "Total" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "Avg" Then
                            Me.grd.RootTable.Columns(c).Visible = False
                            Me.grd.RootTable.Columns(c + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                            Me.grd.RootTable.Columns(c + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                            Me.grd.RootTable.Columns(c + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                            Me.grd.RootTable.Columns(c + 1).FormatString = "N" & DecimalPointInValue
                            Me.grd.RootTable.Columns(c + 1).FormatString = "N" & DecimalPointInValue
                            Me.grd.RootTable.Columns(c + 1).TotalFormatString = "N" & DecimalPointInValue
                            Me.grd.RootTable.Columns(c + 1).TotalFormatString = "N" & DecimalPointInValue
                        End If
                    Next
                End If

                    Me.grd.RootTable.Columns("Total").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns("Avg").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                    Me.grd.RootTable.Columns("Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns("Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns("Avg").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    Me.grd.RootTable.Columns("Avg").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                    Me.grd.RootTable.Columns("Avg").FormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns("Total").FormatString = "N" & DecimalPointInValue

                    Me.grd.RootTable.Columns("Avg").TotalFormatString = "N" & DecimalPointInValue
                    Me.grd.RootTable.Columns("Total").TotalFormatString = "N" & DecimalPointInValue

                    If Me.rbtArea.Checked = True Then
                    Me.grd.RootTable.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True
                        Dim grp() As Janus.Windows.GridEX.GridEXGroup = {New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Manager")), New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Region")), New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("City")), New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Area")), New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Saleman"))}
                        Me.grd.RootTable.Groups.AddRange(grp)
                    End If
                    CtrlGrdBar1_Load(Nothing, Nothing)
                    Me.grd.AutoSizeColumns()
                    Me.grd.RootTable.Columns("Avg").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
                    Me.grd.RootTable.Columns("Total").FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox

                    Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

                Else
                    Throw New Exception("Some of data is not provided.")
                End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            FillData()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
    '    Try
    '        Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Sales Summaries Month Wise " & vbCrLf & "Date From:" & Me.dtpDateForm.Value.ToString("yyyy-MMM-dd") & " Date To: " & Me.dtpDateTo.Value.ToString("yyyy-MMM-dd") & ""
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Sales Summaries Month Wise " & vbCrLf & "Date From:" & Me.dtpDateForm.Value.ToString("yyyy-MMM-dd") & " Date To: " & Me.dtpDateTo.Value.ToString("yyyy-MMM-dd") & ""
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
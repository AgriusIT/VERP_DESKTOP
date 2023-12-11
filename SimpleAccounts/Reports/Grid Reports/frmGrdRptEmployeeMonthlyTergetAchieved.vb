Public Class frmGrdRptEmployeeMonthlyTergetAchieved
    Dim dtpFromDate As New DateTimePicker
    Dim dtpToDate As New DateTimePicker

    Enum enmDetail
        Employee_ID
        stateid
        cityid
        territoryid
        Region
        City
        Area
        EmpCode
        Employee
        Count
    End Enum
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Employee Monthly Target Achieved" & vbCrLf & "Date From: " & Me.dtpFromDate.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpToDate.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillGrid()
        Try
            Me.dtpFromDate.Value = CDate(Val(Me.txtYear.Text) & "-" & Val(Me.cmbMonth.SelectedValue) & "-1")
            Me.dtpToDate.Value = CDate(Val(Me.txtYear.Text) & "-" & Val(Me.cmbMonth.SelectedValue) & "-" & Date.DaysInMonth(dtpFromDate.Value.Year, dtpFromDate.Value.Month) & "")




            Dim str As String = String.Empty
            'str = "Select COA.StateID, COA.CityID, COA.TerritoryID, COA.StateName as [Region], COA.CityName as City, COA.TerritoryName as Area, Emp.Employee_Name as Employee, COA.detail_Code as [Account Code], COA.detail_title as [Customer],COA.CustomerType as [Customer Type], COA.Account_Type as [Account Type] From vwCOADetail COA LEFT OUTER JOIN EmployeesView emp on emp.Employee_ID = COA.SaleMan WHERE COA.coa_detail_id in(Select DISTINCT CustomerCode From SalesMasterTable)"
            str = "Select emp.Employee_ID, coa.stateid, coa.cityid, coa.territoryid, stateName as Region, coa.CityName as City, coa.TerritoryName as Area, emp.Employee_Code as [Emp Code], Emp.Employee_Name as [Employee] From vwCOADetail COA INNER JOIN tblDefEmployee Emp on COA.SaleMan = emp.Employee_ID"
            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.AcceptChanges()


            Dim IDs As String = Me.lstArticleType.SelectedIDs
            Dim dtType As New DataTable
            dtType = GetDataTable("Select ArticleTypeID,ArticleTypeName From ArticleTypeDefTable " & IIf(IDs.Length > 0, " WHERE ArticleTypeID In(" & IDs & ") ", "") & " Order By 2 ASC")
            dtType.AcceptChanges()

            Dim intMonths As Integer = DateDiff(DateInterval.Month, Me.dtpFromDate.Value.Date, Me.dtpToDate.Value.Date)
            intMonths = IIf(intMonths = 0, 1, intMonths)

            For Each r As DataRow In dtType.Rows
                If Not dt.Columns.Contains(r("ArticleTypeName").ToString) Then
                    dt.Columns.Add(r("ArticleTypeID"), GetType(System.Int32), r("ArticleTypeID"))
                    dt.Columns.Add(r("ArticleTypeName"), GetType(System.String))
                    dt.Columns.Add(r("ArticleTypeID") & "^" & "Target_Weight", GetType(System.Double))
                    dt.Columns.Add(r("ArticleTypeID") & "^" & "Achieved_Target_Weight", GetType(System.Double))
                    dt.Columns.Add(r("ArticleTypeID") & "^" & "Diff_Target_Weight", GetType(System.Double))
                    dt.Columns.Add(r("ArticleTypeID") & "^" & "Target_Value", GetType(System.Double))
                    dt.Columns.Add(r("ArticleTypeID") & "^" & "Achieved_Target_Value", GetType(System.Double))
                    dt.Columns.Add(r("ArticleTypeID") & "^" & "Diff_Target_Value", GetType(System.Double))
                End If
            Next

            Dim strTotalTargetWeight As String = String.Empty
            Dim strTotalAchievedWeight As String = String.Empty
            Dim strTotalWeightDiff As String = String.Empty
            Dim strTotalTargetValue As String = String.Empty
            Dim strTotalAchievedValue As String = String.Empty
            Dim strTotalValueDiff As String = String.Empty

            For Each r As DataRow In dt.Rows
                For c As Integer = enmDetail.Count To dt.Columns.Count - 8 Step 8
                    r.BeginEdit()
                    r(c + 2) = 0
                    r(c + 3) = 0
                    'r(c + 4) = 0
                    r(c + 5) = 0
                    r(c + 6) = 0
                    'r(c + 7) = 0
                    r.EndEdit()
                Next
            Next

            For c As Integer = enmDetail.Count To dt.Columns.Count - 8 Step 8
                dt.Columns(c + 4).Expression = "[" & dt.Columns(c + 2).ColumnName & "]" & "-" & "[" & dt.Columns(c + 3).ColumnName & "]"
                dt.Columns(c + 7).Expression = "[" & dt.Columns(c + 5).ColumnName & "]" & "-" & "[" & dt.Columns(c + 6).ColumnName & "]"


                If strTotalTargetWeight.Length > 0 Then
                    strTotalTargetWeight += "+" & "[" & dt.Columns(c + 2).ColumnName & "]"
                Else
                    strTotalTargetWeight = "[" & dt.Columns(c + 2).ColumnName & "]"
                End If

                If strTotalAchievedWeight.Length > 0 Then
                    strTotalAchievedWeight += "+" & "[" & dt.Columns(c + 3).ColumnName & "]"
                Else
                    strTotalAchievedWeight = "[" & dt.Columns(c + 3).ColumnName & "]"
                End If

                If strTotalWeightDiff.Length > 0 Then
                    strTotalWeightDiff += "+" & "[" & dt.Columns(c + 4).ColumnName & "]"
                Else
                    strTotalWeightDiff = "[" & dt.Columns(c + 4).ColumnName & "]"
                End If

                If strTotalTargetValue.Length > 0 Then
                    strTotalTargetValue += "+" & "[" & dt.Columns(c + 5).ColumnName & "]"
                Else
                    strTotalTargetValue = "[" & dt.Columns(c + 5).ColumnName & "]"
                End If

                If strTotalAchievedValue.Length > 0 Then
                    strTotalAchievedValue += "+" & "[" & dt.Columns(c + 6).ColumnName & "]"
                Else
                    strTotalAchievedValue = "[" & dt.Columns(c + 6).ColumnName & "]"
                End If

                If strTotalValueDiff.Length > 0 Then
                    strTotalValueDiff += "+" & "[" & dt.Columns(c + 7).ColumnName & "]"
                Else
                    strTotalValueDiff = "[" & dt.Columns(c + 7).ColumnName & "]"
                End If

            Next




            Dim dtTargetData As New DataTable
            dtTargetData = GetDataTable("SELECT CityID, TerritoryID, EmployeeID, ArticleTypeID, SUM(ISNULL(Target_Weight, 0)) AS Target_Weight, SUM(ISNULL(Target_Value, 0)) AS Target_Value " _
                                      & " FROM dbo.SalesManTargetDetailTable  WHERE (Convert(varchar,StartDate,102) BETWEEN Convert(DateTime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) GROUP BY CityID, TerritoryID, EmployeeID, ArticleTypeID")
            dtTargetData.AcceptChanges()

            For Each r As DataRow In dt.Rows
                Dim dr() As DataRow = dtTargetData.Select("CityID=" & Val(r("CityID").ToString) & " AND TerritoryID=" & Val(r("TerritoryID")) & " AND EmployeeID=" & Val(r("Employee_ID").ToString) & "")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            r.BeginEdit()
                            r(dt.Columns.IndexOf(drFound(3)) + 2) = Val(drFound(4).ToString)
                            r(dt.Columns.IndexOf(drFound(3)) + 5) = Val(drFound(5).ToString)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next
            Dim dtData As New DataTable
            dtData = GetDataTable("Select COA.StateID, COA.CityID, COA.TerritoryID, SalesMasterTable.EmployeeCode, ArticleDefView.ArticleTypeId, SUM((IsNull(Qty,0) * IsNull(ItemWeight,0)+IsNull(SampleQty,0) * IsNull(ItemWeight,0))-IsNull(Sales_Return.Return_Weight,0)) as Sold_Weight, SUM((IsNull(Qty,0) * IsNull(Price,0))-IsNull(Return_Amount,0)) as Sold_Amount From vwCOADetail COA INNER JOIN SalesMasterTable on SalesMasterTable.CustomerCode = coa.coa_Detail_id " _
                                & " INNER JOIN SalesDetailTable on SalesMasterTable.SalesID = SalesDetailTable.SalesID " _
                                & " INNER JOIN ArticleDefView on ArticleDefView.ArticleID = SalesDetailTable.ArticleDefID " _
                                & " LEFT OUTER JOIN ( " _
                                & " Select COA.StateID, COA.CityID, COA.TerritoryID, SalesReturnMasterTable.EmployeeCode, ArticleDefView.ArticleTypeId, (IsNull(Qty,0) * IsNull(ItemWeight,0)+IsNull(SampleQty,0) * IsNull(ItemWeight,0)) as Return_Weight, (IsNull(Qty,0) * IsNull(Price,0)) as Return_Amount From vwCOADetail COA INNER JOIN SalesReturnMasterTable on SalesReturnMasterTable.CustomerCode = coa.coa_Detail_id " _
                                & " INNER JOIN SalesReturnDetailTable on SalesReturnMasterTable.SalesReturnID = SalesReturnDetailTable.SalesReturnID " _
                                & " INNER JOIN ArticleDefView on ArticleDefView.ArticleID = SalesReturnDetailTable.ArticleDefID " _
                                & " WHERE (Convert(Varchar,dbo.SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(Datetime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " _
                                & " ) Sales_Return on Sales_Return.StateID = COA.StateID " _
                                & " AND Sales_Return.CityID = COA.CityID " _
                                & " AND Sales_Return.TerritoryID = COA.TerritoryID " _
                                & " AND Sales_Return.EmployeeCode = SalesMasterTable.EmployeeCode AND Sales_Return.ArticleTypeID = ArticleDefView.ArticleTypeID " _
                                & " WHERE (Convert(Varchar,dbo.SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime,'" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102)) " _
                                & " Group By COA.StateID, COA.CityID, COA.TerritoryID, SalesMasterTable.EmployeeCode,ArticleDefView.ArticleTypeId")
            dtData.AcceptChanges()

            For Each r As DataRow In dt.Rows
                Dim dr() As DataRow = dtData.Select("CityID=" & Val(r("CityID").ToString) & " AND TerritoryID=" & Val(r("TerritoryID")) & " AND EmployeeCode=" & Val(r("Employee_ID").ToString) & "")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            r.BeginEdit()
                            r(dt.Columns.IndexOf(drFound(4)) + 3) = Val(drFound(5).ToString)
                            r(dt.Columns.IndexOf(drFound(4)) + 6) = Val(drFound(6).ToString)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next


            dt.Columns.Add("Total_Target_Weight", GetType(System.Double))
            dt.Columns.Add("Total_Achieved_Weight", GetType(System.Double))
            dt.Columns.Add("Diff_Weight", GetType(System.Double))
            dt.Columns.Add("Total_Target_Value", GetType(System.Double))
            dt.Columns.Add("Total_Achieved_Value", GetType(System.Double))
            dt.Columns.Add("Diff_Value", GetType(System.Double))
            dt.Columns.Add("Total_Target_Tons", GetType(System.Double))
            dt.Columns.Add("Total_Achieved_Tons", GetType(System.Double))
            dt.Columns.Add("Diff_Tons", GetType(System.Double))

            dt.Columns("Total_Target_Weight").Expression = strTotalTargetWeight.ToString()
            dt.Columns("Total_Achieved_Weight").Expression = strTotalAchievedWeight.ToString()
            dt.Columns("Diff_Weight").Expression = strTotalWeightDiff.ToString()
            dt.Columns("Total_Target_Value").Expression = strTotalTargetValue.ToString()
            dt.Columns("Total_Achieved_Value").Expression = strTotalAchievedValue.ToString()
            dt.Columns("Diff_Value").Expression = strTotalValueDiff.ToString()
            dt.Columns("Total_Target_Tons").Expression = "IsNull(Total_Target_Weight,0)/1000"
            dt.Columns("Total_Achieved_Tons").Expression = "IsNull(Total_Achieved_Weight,0)/1000"
            dt.Columns("Diff_Tons").Expression = "(IsNull(Total_Target_Tons,0)-IsNull(Total_Achieved_Tons,0))"


            dt.AcceptChanges()



            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()

            Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdSaved.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            Me.grdSaved.RootTable.ColumnSetRowCount = 1

            Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet

            ColumnSet = Me.grdSaved.RootTable.ColumnSets.Add
            ColumnSet.ColumnCount = 5
            ColumnSet.Caption = "Detail"
            ColumnSet.Add(Me.grdSaved.RootTable.Columns("Region"), 0, 0)
            ColumnSet.Add(Me.grdSaved.RootTable.Columns("City"), 0, 1)
            ColumnSet.Add(Me.grdSaved.RootTable.Columns("Area"), 0, 2)
            ColumnSet.Add(Me.grdSaved.RootTable.Columns("Emp Code"), 0, 3)
            ColumnSet.Add(Me.grdSaved.RootTable.Columns("Employee"), 0, 4)

            Me.grdSaved.RootTable.Columns("CityID").Visible = False
            Me.grdSaved.RootTable.Columns("StateID").Visible = False
            Me.grdSaved.RootTable.Columns("TerritoryID").Visible = False
            Dim intColumns As Integer = Me.grdSaved.RootTable.Columns.Count - 9
            For c As Integer = enmDetail.Count To intColumns - 8 Step 8
                Me.grdSaved.RootTable.Columns(c).Visible = False
                Me.grdSaved.RootTable.Columns(c + 2).Caption = "Target"
                Me.grdSaved.RootTable.Columns(c + 3).Caption = "Achieved"
                Me.grdSaved.RootTable.Columns(c + 4).Caption = "Diff"
                Me.grdSaved.RootTable.Columns(c + 5).Caption = "Target Value"
                Me.grdSaved.RootTable.Columns(c + 6).Caption = "Achieved"
                Me.grdSaved.RootTable.Columns(c + 7).Caption = "Diff"

                Me.grdSaved.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns(c + 3).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns(c + 4).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns(c + 5).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns(c + 6).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns(c + 7).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                Me.grdSaved.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(c + 3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(c + 4).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(c + 5).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(c + 6).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(c + 7).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grdSaved.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(c + 3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(c + 4).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(c + 5).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(c + 6).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns(c + 7).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grdSaved.RootTable.Columns(c + 2).FormatString = "N" & DecimalPointInQty
                Me.grdSaved.RootTable.Columns(c + 3).FormatString = "N" & DecimalPointInQty
                Me.grdSaved.RootTable.Columns(c + 4).FormatString = "N" & DecimalPointInQty
                Me.grdSaved.RootTable.Columns(c + 5).FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns(c + 6).FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns(c + 7).FormatString = "N" & DecimalPointInValue

                Me.grdSaved.RootTable.Columns(c + 2).TotalFormatString = "N" & DecimalPointInQty
                Me.grdSaved.RootTable.Columns(c + 3).TotalFormatString = "N" & DecimalPointInQty
                Me.grdSaved.RootTable.Columns(c + 4).TotalFormatString = "N" & DecimalPointInQty
                Me.grdSaved.RootTable.Columns(c + 5).TotalFormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns(c + 6).TotalFormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns(c + 7).TotalFormatString = "N" & DecimalPointInValue

                ColumnSet1 = Me.grdSaved.RootTable.ColumnSets.Add
                ColumnSet1.ColumnCount = 6
                ColumnSet1.Caption = Me.grdSaved.RootTable.Columns(c + 1).Caption
                ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                ColumnSet1.Add(Me.grdSaved.RootTable.Columns(c + 2), 0, 0)
                ColumnSet1.Add(Me.grdSaved.RootTable.Columns(c + 3), 0, 1)
                ColumnSet1.Add(Me.grdSaved.RootTable.Columns(c + 4), 0, 2)
                ColumnSet1.Add(Me.grdSaved.RootTable.Columns(c + 5), 0, 3)
                ColumnSet1.Add(Me.grdSaved.RootTable.Columns(c + 6), 0, 4)
                ColumnSet1.Add(Me.grdSaved.RootTable.Columns(c + 7), 0, 5)
            Next


            ColumnSet2 = Me.grdSaved.RootTable.ColumnSets.Add
            ColumnSet2.ColumnCount = 9
            ColumnSet2.Caption = "Totals"
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Total_Target_Weight"), 0, 0)
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Total_Achieved_Weight"), 0, 1)
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Diff_Weight"), 0, 2)
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Total_Target_Value"), 0, 3)
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Total_Achieved_Value"), 0, 4)
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Diff_Value"), 0, 5)
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Total_Target_Tons"), 0, 6)
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Total_Achieved_Tons"), 0, 7)
            ColumnSet2.Add(Me.grdSaved.RootTable.Columns("Diff_Tons"), 0, 8)


            Me.grdSaved.RootTable.Columns("Total_Target_Weight").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Total_Achieved_Weight").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Diff_Weight").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Total_Target_Value").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Total_Achieved_Value").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Diff_Value").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Total_Target_Tons").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Total_Achieved_Tons").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Diff_Tons").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            Me.grdSaved.RootTable.Columns("Total_Target_Weight").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Total_Achieved_Weight").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Diff_Weight").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Total_Target_Value").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Total_Achieved_Value").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Diff_Value").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Total_Target_Tons").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Total_Achieved_Tons").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Diff_Tons").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaved.RootTable.Columns("Total_Target_Weight").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Total_Achieved_Weight").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Diff_Weight").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Total_Target_Value").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Total_Achieved_Value").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Diff_Value").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Total_Target_Tons").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Total_Achieved_Tons").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Diff_Tons").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaved.RootTable.Columns("Total_Target_Weight").FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns("Total_Achieved_Weight").FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns("Diff_Weight").FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns("Total_Target_Value").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Total_Achieved_Value").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Diff_Value").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Total_Target_Tons").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Total_Achieved_Tons").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Diff_Tons").FormatString = "N" & DecimalPointInValue

            Me.grdSaved.RootTable.Columns("Total_Target_Weight").TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns("Total_Achieved_Weight").TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns("Diff_Weight").TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns("Total_Target_Value").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Total_Achieved_Value").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Diff_Value").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Total_Target_Tons").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Total_Achieved_Tons").TotalFormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("Diff_Tons").TotalFormatString = "N" & DecimalPointInValue

            Me.grdSaved.RootTable.Columns("Total_Target_Weight").Caption = "Total Target Weight"
            Me.grdSaved.RootTable.Columns("Total_Achieved_Weight").Caption = "Total Achieved Weight"
            Me.grdSaved.RootTable.Columns("Diff_Weight").Caption = "Diff Weight"
            Me.grdSaved.RootTable.Columns("Total_Target_Value").Caption = "Total Target Value"
            Me.grdSaved.RootTable.Columns("Total_Achieved_Value").Caption = "Achieved Value"
            Me.grdSaved.RootTable.Columns("Diff_Value").Caption = "Diff Value"
            Me.grdSaved.RootTable.Columns("Total_Target_Tons").Caption = "Total Target Tons"
            Me.grdSaved.RootTable.Columns("Total_Achieved_Tons").Caption = "Total Achieved Tons"
            Me.grdSaved.RootTable.Columns("Diff_Tons").Caption = "Diff Tons"


            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdSaved.AutoSizeColumns()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtYear_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtYear.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillCombo(Optional Condition As String = "")
        Try
            FillListBox(Me.lstArticleType.ListItem, "Select ArticleTypeID, ArticleTypeName From ArticleTypeDefTable Order By ArticleTypeName ASC")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmGrdRptEmployeeMonthlyTergetAchieved_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try

            FillCombo()
            Me.txtYear.Text = DateTime.Now.Year
            dtpFromDate.Value = DateTime.Now
            dtpToDate.Value = DateTime.Now
            Me.cmbMonth.ValueMember = "Month"
            Me.cmbMonth.DisplayMember = "Month_Name"
            Me.cmbMonth.DataSource = GetMonths()
            Me.cmbMonth.SelectedValue = DateTime.Now.Month


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            FillCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
End Class
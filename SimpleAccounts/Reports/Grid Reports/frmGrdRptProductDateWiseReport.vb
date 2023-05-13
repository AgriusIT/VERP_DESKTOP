Public Class frmGrdRptProductDateWiseReport
    Dim dtpDateFrom As New DateTimePicker
    Dim dtpDateTo As New DateTimePicker
    Enum enmCustomer
        mDate
        Count
    End Enum

    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try

            Dim strSQL As String = String.Empty
            If Condition = "Product" Then
                strSQL = String.Empty
                strSQL = "Select ArticleGenderId, ArticleGenderName From ArticleGenderDefTable ORDER BY 2 ASC"
                FillListBox(Me.uiProductList.ListItem, strSQL)
            ElseIf Condition = "Customer" Then
                strSQL = String.Empty
                strSQL = "Select coa_detail_id, detail_title,detail_code,main_sub_sub_Id From vwCOADetail WHERE 1=1 "
                ''Start TFS2124
                If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                    strSQL += " AND (dbo.vwCOADetail.account_type = 'Customer')  "
                Else
                    strSQL += " AND (dbo.vwCOADetail.account_type in('Customer','Vendor'))  "
                End If
                strSQL += " ORDER BY 2 ASC"
                ''End TFS2124
                FillDropDown(Me.cmbCustomer, strSQL)
            ElseIf Condition = "Month" Then
                strSQL = String.Empty
                Me.cmbPeriod.ValueMember = "Month"
                Me.cmbPeriod.DisplayMember = "Month_Name"
                Me.cmbPeriod.DataSource = GetMonths()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmGrdRptProductCustomerWiseReport_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.cmbPeriod.Text = "Current Month"
            FillCombos("Customer")
            FillCombos("Product")
            FillCombos("Month")
            Me.txtYear.Text = Now.Year
            Me.cmbPeriod.SelectedValue = Now.Month
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillData(Optional ByVal Condition As String = "")
        Try

            Me.dtpDateFrom.Value = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbPeriod.SelectedValue & "-1")
            Me.dtpDateTo.Value = MyToDate(Me.cmbPeriod.SelectedValue, Val(Me.txtYear.Text))
            Dim intTotalDays As Integer = 0I
            intTotalDays = Me.dtpDateTo.Value.Subtract(Me.dtpDateFrom.Value).Days
            Dim myDates As New List(Of DateTime)

       

            Dim strSQL As String = String.Empty

            'strSQL = "Select coa_detail_id, detail_title as [Customer], detail_code as [Code] From vwCOADetail ORDER BY 2 ASC"

            Dim objData As New DataTable
            objData.Columns.Add("Date", GetType(System.DateTime))
            Dim drDate As DataRow
            For i As Integer = 0 To intTotalDays
                drDate = objData.NewRow
                drDate(0) = Me.dtpDateFrom.Value.AddDays(i)
                objData.Rows.Add(drDate)
                objData.AcceptChanges()
                myDates.Add(Me.dtpDateFrom.Value.AddDays(i))
            Next

            'objData = GetDataTable(strSQL)
            'objData.AcceptChanges()

            strSQL = String.Empty
            strSQL = "Select ArticleGenderId, ArticleGenderName From ArticleGenderDefTable WHERE ArticleGenderId IN(" & IIf(uiProductList.SelectedIDs.Length > 0, uiProductList.SelectedIDs, 0) & ") ORDER BY 2 ASC"
            Dim objProdData As New DataTable
            objProdData = GetDataTable(strSQL)
            objProdData.AcceptChanges()

            For Each r As DataRow In objProdData.Rows
                If Not objData.Columns.Contains(r.Item(1).ToString) Then
                    objData.Columns.Add(r.Item(0).ToString, GetType(System.Int32), r.Item(0))
                    objData.Columns.Add(r.Item(1).ToString, GetType(System.String))
                    objData.Columns.Add("Dispatch" & "^" & r.Item(0).ToString, GetType(System.Double))
                    objData.Columns.Add("Return" & "^" & r.Item(0).ToString, GetType(System.Double))
                    objData.Columns.Add("Market" & "^" & r.Item(0).ToString, GetType(System.Double))
                End If
            Next
            objData.AcceptChanges()
            For Each r As DataRow In objData.Rows
                For c As Integer = enmCustomer.Count To objData.Columns.Count - 5 Step 5
                    r.BeginEdit()
                    r(c + 2) = 0
                    r(c + 3) = 0
                    r(c + 4) = 0
                    r.EndEdit()
                Next
            Next

            strSQL = String.Empty
            strSQL = "SELECT dbo.ArticleDefView.ArticleGenderId, Convert(datetime,Convert(varchar,dbo.SalesMasterTable.SalesDate,102),102) as SalesDate, SUM(ISNULL(dbo.SalesDetailTable.Qty, 0) * ISNULL(dbo.SalesDetailTable.CurrentPrice, 0)) " _
                     & "  AS Dispatch, SUM((ISNULL(dbo.SalesDetailTable.CurrentPrice, 0) - ISNULL(dbo.SalesDetailTable.Price, 0)) * ISNULL(dbo.SalesDetailTable.Qty, 0))  " _
                     & "  AS Disp_Market FROM dbo.SalesDetailTable INNER JOIN  dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId INNER JOIN " _
                     & " dbo.ArticleDefView ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE ArticleGenderId In(" & IIf(Me.uiProductList.SelectedIDs.Length > 0, Me.uiProductList.SelectedIDs, 0) & ") AND (Convert(Varchar,SalesDate, 102) BETWEEN Convert(DateTime,'" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)) AND SalesMasterTable.CustomerCode=" & IIf(Me.cmbCustomer.SelectedIndex > 0, Me.cmbCustomer.SelectedValue, 0) & " GROUP BY Convert(datetime,Convert(varchar,dbo.SalesMasterTable.SalesDate,102),102), dbo.ArticleDefView.ArticleGenderId "
            Dim dtDisp As New DataTable
            dtDisp = GetDataTable(strSQL)
            Dim dr() As DataRow
            For Each objRow As DataRow In objData.Rows
                objRow.BeginEdit()
                dr = dtDisp.Select("SalesDate='" & CDate(objRow.Item(0)).ToString("yyyy-M-d 00:00:00") & "'")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            objRow(objData.Columns.IndexOf(drFound(0)) + 2) = Val(drFound(2).ToString)
                            objRow(objData.Columns.IndexOf(drFound(0)) + 4) = Val(drFound(3).ToString)
                        Next
                    End If
                End If
                objRow.EndEdit()
            Next

            objData.AcceptChanges()

            strSQL = String.Empty
            strSQL = "SELECT dbo.ArticleDefView.ArticleGenderId, Convert(datetime,Convert(varchar,dbo.SalesReturnMasterTable.SalesReturnDate,102),102) as SalesReturnDate, SUM(ISNULL(dbo.SalesReturnDetailTable.Qty, 0) * ISNULL(dbo.SalesReturnDetailTable.CurrentPrice, 0)) " _
                     & "  AS Dispatch, SUM((ISNULL(dbo.SalesReturnDetailTable.CurrentPrice, 0) - ISNULL(dbo.SalesReturnDetailTable.Price, 0)) * ISNULL(dbo.SalesReturnDetailTable.Qty, 0))  " _
                     & "  AS Disp_Market FROM dbo.SalesReturnDetailTable INNER JOIN  dbo.SalesReturnMasterTable ON dbo.SalesReturnDetailTable.SalesReturnId = dbo.SalesReturnMasterTable.SalesReturnId INNER JOIN " _
                     & " dbo.ArticleDefView ON dbo.SalesReturnDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE ArticleGenderId In(" & IIf(Me.uiProductList.SelectedIDs.Length > 0, Me.uiProductList.SelectedIDs, 0) & ") AND (Convert(Varchar,SalesReturnDate, 102) BETWEEN Convert(DateTime,'" & Me.dtpDateFrom.Value.ToString("yyyy-M-d 00:00:00") & "',102) AND Convert(DateTime,'" & Me.dtpDateTo.Value.ToString("yyyy-M-d 23:59:59") & "',102)) AND SalesReturnMasterTable.CustomerCode=" & IIf(Me.cmbCustomer.SelectedIndex > 0, Me.cmbCustomer.SelectedValue, 0) & " GROUP BY Convert(datetime,Convert(varchar,dbo.SalesReturnMasterTable.SalesReturnDate,102),102), dbo.ArticleDefView.ArticleGenderId "
            Dim dtRet As New DataTable
            dtRet = GetDataTable(strSQL)
            Dim drRet() As DataRow
            For Each objRow As DataRow In objData.Rows
                objRow.BeginEdit()
                drRet = dtRet.Select("SalesReturnDate='" & CDate(objRow.Item(0)).ToString("yyyy-M-d 00:00:00") & "'")
                If drRet IsNot Nothing Then
                    If drRet.Length > 0 Then
                        For Each drFound As DataRow In drRet
                            objRow(objData.Columns.IndexOf(drFound(0)) + 3) = Val(drFound(2).ToString)
                            objRow(objData.Columns.IndexOf(drFound(0)) + 4) += -Val(drFound(3).ToString)
                        Next
                    End If
                End If
                objRow.EndEdit()
            Next
            objData.AcceptChanges()

            Me.grd.DataSource = objData
            Me.grd.RetrieveStructure()
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False

            Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
            Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
            Me.grd.RootTable.ColumnSetRowCount = 1
            Me.grd.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
            ColumnSet1 = Me.grd.RootTable.ColumnSets.Add
            ColumnSet1.ColumnCount = 1
            ColumnSet1.Caption = ""
            ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ColumnSet1.Add(Me.grd.RootTable.Columns(enmCustomer.mDate), 0, 0)

            For c As Integer = enmCustomer.Count To Me.grd.RootTable.Columns.Count - 5 Step 5
                Me.grd.RootTable.Columns(c).Visible = False

                Me.grd.RootTable.Columns(c + 2).Caption = "Dispatch"
                Me.grd.RootTable.Columns(c + 3).Caption = "Return"
                Me.grd.RootTable.Columns(c + 4).Caption = "Market"

                Me.grd.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(c + 3).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(c + 4).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum



                Me.grd.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 4).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grd.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c + 4).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grd.RootTable.Columns(c + 2).FormatString = "N"
                Me.grd.RootTable.Columns(c + 3).FormatString = "N"
                Me.grd.RootTable.Columns(c + 4).FormatString = "N"

                Me.grd.RootTable.Columns(c + 2).TotalFormatString = "N"
                Me.grd.RootTable.Columns(c + 3).TotalFormatString = "N"
                Me.grd.RootTable.Columns(c + 4).TotalFormatString = "N"

                ColumnSet = Me.grd.RootTable.ColumnSets.Add
                ColumnSet.ColumnCount = 3
                ColumnSet.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                ColumnSet.Caption = Me.grd.RootTable.Columns(c + 1).Caption
                ColumnSet.Add(Me.grd.RootTable.Columns(c + 2), 0, 0)
                ColumnSet.Add(Me.grd.RootTable.Columns(c + 3), 0, 1)
                ColumnSet.Add(Me.grd.RootTable.Columns(c + 4), 0, 2)

            Next

            Dim dtData As New DataTable
            dtData = CType(Me.grd.DataSource, DataTable)

            Dim strTotalDispatch As String = String.Empty
            Dim strTotalReturn As String = String.Empty
            Dim strTotalMarket As String = String.Empty

            For c As Integer = enmCustomer.Count To Me.grd.RootTable.Columns.Count - 5 Step 5
                strTotalDispatch += "+[" & Me.grd.RootTable.Columns(c + 2).DataMember & "]"
                strTotalReturn += "+[" & Me.grd.RootTable.Columns(c + 3).DataMember & "]"
                strTotalMarket += "+[" & Me.grd.RootTable.Columns(c + 4).DataMember & "]"
            Next

            dtData.Columns.Add("Total", GetType(System.Double))
            dtData.Columns.Add("Total_Dispatch", GetType(System.Double))
            dtData.Columns.Add("Total_Return", GetType(System.Double))
            dtData.Columns.Add("Total_Market", GetType(System.Double))
            dtData.Columns.Add("Gross_Total", GetType(System.Double))

            dtData.Columns("Total_Dispatch").Expression = strTotalDispatch.ToString
            dtData.Columns("Total_Return").Expression = strTotalReturn.ToString
            dtData.Columns("Total_Market").Expression = strTotalMarket.ToString
            dtData.Columns("Gross_Total").Expression = "([Total_Dispatch]-([Total_Return]+[Total_Market]))"

            Me.grd.RootTable.Columns.Add("Total", Janus.Windows.GridEX.ColumnType.Text)
            Me.grd.RootTable.Columns.Add("Total_Dispatch", Janus.Windows.GridEX.ColumnType.Text)
            Me.grd.RootTable.Columns.Add("Total_Return", Janus.Windows.GridEX.ColumnType.Text)
            Me.grd.RootTable.Columns.Add("Total_Market", Janus.Windows.GridEX.ColumnType.Text)
            Me.grd.RootTable.Columns.Add("Gross_Total", Janus.Windows.GridEX.ColumnType.Text)



            Me.grd.RootTable.Columns("Total_Dispatch").Caption = "Dispatch"
            Me.grd.RootTable.Columns("Total_Return").Caption = "Return"
            Me.grd.RootTable.Columns("Total_Market").Caption = "Market"
            Me.grd.RootTable.Columns("Gross_Total").Caption = "Gross"


            Me.grd.RootTable.Columns("Total_Dispatch").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total_Return").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total_Market").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Gross_Total").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("Total_Dispatch").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total_Return").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total_Market").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Gross_Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Total_Dispatch").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total_Return").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total_Market").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Gross_Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Total_Dispatch").FormatString = "N"
            Me.grd.RootTable.Columns("Total_Return").FormatString = "N"
            Me.grd.RootTable.Columns("Total_Market").FormatString = "N"
            Me.grd.RootTable.Columns("Gross_Total").FormatString = "N"

            Me.grd.RootTable.Columns("Total_Dispatch").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Total_Return").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Total_Market").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Gross_Total").TotalFormatString = "N"


            ColumnSet2 = Me.grd.RootTable.ColumnSets.Add
            ColumnSet2.ColumnCount = 4
            ColumnSet2.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            ColumnSet2.Caption = Me.grd.RootTable.Columns("Total").Caption
            ColumnSet2.Add(Me.grd.RootTable.Columns("Total_Dispatch"), 0, 0)
            ColumnSet2.Add(Me.grd.RootTable.Columns("Total_Return"), 0, 1)
            ColumnSet2.Add(Me.grd.RootTable.Columns("Total_Market"), 0, 2)
            ColumnSet2.Add(Me.grd.RootTable.Columns("Gross_Total"), 0, 3)
            Me.grd.RootTable.Columns("Date").FormatString = "dd/MMM/yyyy"
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(1).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            If txtYear.Text = String.Empty Or Me.txtYear.Text.Length < 4 Then
                ShowErrorMessage("Year is not valid.")
                Me.txtYear.Focus()
                Exit Sub
            End If
            If Me.cmbCustomer.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select Customer.")
                Me.cmbCustomer.Focus()
                Exit Sub
            End If
            FillData()
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & Me.cmbCustomer.Text & vbCrLf & "Product Summary" & vbCrLf & "Date From: " & Me.dtpDateFrom.Value.ToString("dd/MMM/yyyy") & " Date To: " & Me.dtpDateTo.Value.ToString("dd/MMM/yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
          
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            FillCombos("Customer")
            FillCombos("Product")
            FillCombos("Month")
            Me.txtYear.Text = Now.Year
            Me.cmbPeriod.SelectedValue = Now.Month
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function MyToDate(ByVal Month As Integer, ByVal Year As Integer) As DateTime
        Try
            Dim myDate As DateTime
            If Month = 2 Then
                If Date.IsLeapYear(Year) Then
                    myDate = CDate(Year & "-" & Month & "-29") 'Feb Last Date
                Else
                    myDate = CDate(Year & "-" & Month & "-28") 'Feb Last Date
                End If
            ElseIf Month = 1 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Jan Last Date
            ElseIf Month = 3 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Mar Last Date
            ElseIf Month = 4 Then
                myDate = CDate(Year & "-" & Month & "-30") 'April Last Date
            ElseIf Month = 5 Then
                myDate = CDate(Year & "-" & Month & "-31") 'May Last Date
            ElseIf Month = 6 Then
                myDate = CDate(Year & "-" & Month & "-30") 'June Last Date
            ElseIf Month = 7 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Jully Last Date
            ElseIf Month = 8 Then
                myDate = CDate(Year & "-" & Month & "-31") 'August Last Date
            ElseIf Month = 9 Then
                myDate = CDate(Year & "-" & Month & "-30") 'Sep Last Date
            ElseIf Month = 10 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Oct Last Date
            ElseIf Month = 11 Then
                myDate = CDate(Year & "-" & Month & "-30") 'Nov Last Date
            ElseIf Month = 12 Then
                myDate = CDate(Year & "-" & Month & "-31") 'Dec Last Date
            End If
            Return myDate
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
Public Class frmGrdRptSalesByGender
    Enum Customer
        Id
        Name
        State
        City
        Territory
        ExpiryDate
        Discount
        Other_Exp
        Fuel
        TransitInsurance
        Credit_Limit
        Type
        Email
        PhoneNo
    End Enum
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            FillGrid()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
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
    Private Sub frmGrdRptSalesByGender_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.tbProgressbar.Visible = False
            Me.cmbPeriod.Text = "Current Month"
            FillUltraDropDown(Me.cmbAccount, "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                  "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone " & _
                                                  "FROM         tblCustomer LEFT OUTER JOIN " & _
                                                  "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                  "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                  "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                  "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                  "WHERE (vwCOADetail.account_type='Customer') and  vwCOADetail.coa_detail_id is not  null")
            Me.cmbAccount.Rows(0).Activate()
            If cmbAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0
            id = Me.cmbAccount.Value
            Dim Str As String = "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                 "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone " & _
                                                 "FROM         tblCustomer LEFT OUTER JOIN " & _
                                                 "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                 "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                 "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                 "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                 "WHERE vwCOADetail.coa_detail_id is not  null"

            ''Start TFS2124
            If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                Str += " AND (vwCOADetail.account_type = 'Customer')  "
            Else
                Str += " AND (vwCOADetail.account_type in('Customer','Vendor'))  "
            End If

            ''End TFS2124
            FillUltraDropDown(Me.cmbAccount, Str)
            Me.cmbAccount.Rows(0).Activate()
            If cmbAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                Me.cmbAccount.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
            End If
            Me.cmbAccount.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillGrid()
        Me.tbProgressbar.Visible = True
        Try

            Dim dt As New DataTable
            dt.Columns.Add("Date", GetType(System.DateTime))
            Dim dr As DataRow
            Dim Days As Integer = Me.dtpTo.Value.Subtract(Me.dtpFrom.Value).Days
            If Days > 0 Then
                For iDate As Integer = 0 To Days - 1
                    dr = dt.NewRow
                    dr(0) = Me.dtpFrom.Value.AddDays(iDate)
                    dt.Rows.Add(dr)
                Next
            Else
                dr = dt.NewRow
                dr(0) = Me.dtpFrom.Value
                dt.Rows.Add(dr)
            End If
            dt.AcceptChanges()
            '--------------------------- Category Add -------------------
            Dim dtCategory As DataTable = GetDataTable("Select ArticleGenderId, ArticleGenderName From ArticleGenderDefTable")
            Dim i As Integer = 1
            If dtCategory IsNot Nothing Then
                If dtCategory.Rows.Count > 0 Then
                    For Each drCategory As DataRow In dtCategory.Rows
                        If Not dtCategory.Constraints(drCategory(1)) Then
                            dt.Columns.Add(drCategory(0), GetType(System.Int16), drCategory(0))
                            dt.Columns.Add(drCategory(1).ToString, GetType(System.Double))
                            dt.Columns.Add("Rep" & " " & i, GetType(System.Double))
                            dt.Columns.Add("Mkt" & " " & i, GetType(System.Double))
                            dt.Columns.Add("Sale" & " " & i, GetType(System.Double))
                            i += 1
                        End If
                    Next
                End If
            End If
            dt.AcceptChanges()
            For Each row As DataRow In dt.Rows
                For c As Integer = 1 To dt.Columns.Count - 5 Step 5
                    row.BeginEdit()
                    row(c + 1) = 0
                    row(c + 2) = 0
                    row(c + 3) = 0
                    row(c + 4) = 0
                    row.EndEdit()
                Next
            Next
            dt.AcceptChanges()
            Dim str As String = " Select SalesDate, ArticleGenderId, Sales, MC From (" _
           & " SELECT CONVERT(Datetime, CONVERT(varchar, dbo.SalesMasterTable.SalesDate, 102), 102) AS SalesDate, dbo.ArticleDefView.ArticleGenderId, " _
           & " SUM(dbo.SalesDetailTable.Qty * dbo.SalesDetailTable.CurrentPrice) AS Sales, SUM((dbo.SalesDetailTable.CurrentPrice - dbo.SalesDetailTable.Price)  " _
           & " * dbo.SalesDetailTable.Qty) AS MC " _
           & " FROM dbo.SalesMasterTable INNER JOIN " _
           & " dbo.SalesDetailTable ON dbo.SalesMasterTable.SalesId = dbo.SalesDetailTable.SalesId INNER JOIN " _
           & " dbo.ArticleDefView ON dbo.SalesDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId " & IIf(Me.cmbAccount.SelectedRow.Cells(0).Value > 0, " WHERE SalesMasterTable.CustomerCode=" & Me.cmbAccount.Value & "", "") & " " _
           & " GROUP BY dbo.ArticleDefView.ArticleGenderId, CONVERT(Datetime, CONVERT(varchar, dbo.SalesMasterTable.SalesDate, 102), 102))abc "

            Dim dtdata As DataTable = GetDataTable(str)
            Dim r() As DataRow
            For Each row As DataRow In dt.Rows
                r = dtdata.Select("SalesDate='" & row(0) & "'")
                If r IsNot Nothing Then
                    If r.Length > 0 Then
                        For Each drFound As DataRow In r
                            row.BeginEdit()
                            row(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                            row(dt.Columns.IndexOf(drFound(1)) + 3) = drFound(3)
                            row.EndEdit()
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()
            Dim dtDmg As DataTable = GetDataTable("SELECT CONVERT(DateTime, CONVERT(Varchar, dbo.SalesReturnMasterTable.SalesReturnDate, 102), 102) AS SalesReturnDate, dbo.ArticleDefView.ArticleGenderId,  " _
              & " SUM(dbo.SalesReturnDetailTable.Qty * dbo.SalesReturnDetailTable.CurrentPrice) AS Rep, SUM(((isnull(currentPrice,0)-Isnull(Price,0))*Qty)) as CM  " _
              & " FROM         dbo.SalesReturnMasterTable INNER JOIN  " _
              & " dbo.SalesReturnDetailTable ON dbo.SalesReturnMasterTable.SalesReturnId = dbo.SalesReturnDetailTable.SalesReturnId INNER JOIN " _
              & " dbo.ArticleDefView ON dbo.SalesReturnDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId INNER JOIN  " _
              & " dbo.tblDefLocation ON dbo.SalesReturnDetailTable.LocationId = dbo.tblDefLocation.location_id  " _
              & " WHERE     (dbo.tblDefLocation.location_type = 'Damage') " & IIf(Me.cmbAccount.SelectedRow.Cells(0).Value > 0, " AND SalesReturnMasterTable.CustomerCode=" & Me.cmbAccount.Value & " ", "") & " " _
              & " GROUP BY CONVERT(DateTime, CONVERT(Varchar, dbo.SalesReturnMasterTable.SalesReturnDate, 102), 102), dbo.ArticleDefView.ArticleGenderId")
            Dim drDmg() As DataRow
            For Each row As DataRow In dt.Rows
                drDmg = dtDmg.Select("SalesReturnDate='" & row(0) & "'")
                If drDmg IsNot Nothing Then
                    If drDmg.Length > 0 Then
                        For Each drFound As DataRow In drDmg
                            row.BeginEdit()
                            row(dt.Columns.IndexOf(drFound(1)) + 2) = drFound(2)
                            row(dt.Columns.IndexOf(drFound(1)) + 3) = row(dt.Columns.IndexOf(drFound(1)) + 3) - drFound(3)
                            row.EndEdit()
                        Next
                    End If
                End If
            Next
            dt.AcceptChanges()

            Dim TotalSales As String = String.Empty
            'For col As Integer = 1 To dt.Columns.Count - 5 Step 5
            '    For col1 As Integer = 1 To dt.Columns.Count - 5 Step 5
            '        If TotalSales.Length > 0 Then
            '            TotalSales = TotalSales & "+" & "[" & dt.Columns(col + 1).ColumnName & "]" & "-" & "[" & dt.Columns(col + 2).ColumnName & "]" & "-" & "[" & dt.Columns(col + 3).ColumnName & "]"
            '        Else
            '            TotalSales = "[" & dt.Columns(col + 1).ColumnName & "]" & "-" & "[" & dt.Columns(col + 2).ColumnName & "]" & "-" & "[" & dt.Columns(col + 3).ColumnName & "]"
            '        End If
            '        dt.Columns(col + 4).Expression = TotalSales
            '    Next
            '    TotalSales = String.Empty
            'Next
            For Each row As DataRow In dt.Rows
                For col As Integer = 1 To dt.Columns.Count - 5 Step 5
                    row.BeginEdit()
                    row(col + 4) = row(col + 1) - row(col + 2) - row(col + 3)
                    row.EndEdit()
                Next
            Next

            dt.Columns.Add("Total", GetType(System.Double))
            dt.Columns.Add("Total Prod", GetType(System.Double))
            dt.Columns.Add("Total Rep", GetType(System.Double))
            dt.Columns.Add("Total Mkt", GetType(System.Double))

            'Dim Total_Sales As String = String.Empty
            'For TCol As Integer = 1 To dt.Columns.Count - 5 Step 5
            '    If Total_Sales.Length > 0 Then
            '        Total_Sales = Total_Sales & "+" & "[" & dt.Columns(TCol + 4).ColumnName & "]"
            '    Else
            '        Total_Sales = "[" & dt.Columns(TCol + 4).ColumnName & "]"
            '    End If
            'Next
            'dt.Columns("Total").Expression = Total_Sales

            Dim Total_Prod As String = String.Empty
            For TCol As Integer = 1 To dt.Columns.Count - 5 Step 5
                If Total_Prod.Length > 0 Then
                    Total_Prod = Total_Prod & "+" & "[" & dt.Columns(TCol + 1).ColumnName & "]"
                Else
                    Total_Prod = "[" & dt.Columns(TCol + 1).ColumnName & "]"
                End If
            Next
            dt.Columns("Total Prod").Expression = Total_Prod


            Dim Total_Rep As String = String.Empty
            For TCol As Integer = 1 To dt.Columns.Count - 5 Step 5
                If Total_Rep.Length > 0 Then
                    Total_Rep = Total_Rep & "+" & "[" & dt.Columns(TCol + 2).ColumnName & "]"
                Else
                    Total_Rep = "[" & dt.Columns(TCol + 2).ColumnName & "]"
                End If
            Next
            dt.Columns("Total Rep").Expression = Total_Rep


            Dim Total_Mkt As String = String.Empty
            For TCol As Integer = 1 To dt.Columns.Count - 5 Step 5
                If Total_Mkt.Length > 0 Then
                    Total_Mkt = Total_Mkt & "+" & "[" & dt.Columns(TCol + 3).ColumnName & "]"
                Else
                    Total_Mkt = "[" & dt.Columns(TCol + 3).ColumnName & "]"
                End If
            Next
            dt.Columns("Total Mkt").Expression = Total_Mkt
            dt.Columns("Total").Expression = "[Total Prod]-[Total Rep]-[Total Mkt]"
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSetting()
        Catch ex As Exception
            Throw ex
        Finally
            Me.tbProgressbar.Visible = False
        End Try
    End Sub
    Public Sub ApplyGridSetting(Optional ByVal Condition As String = "")
        Try
            If Me.grd.RootTable Is Nothing Then Exit Sub
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

            For col As Integer = 1 To Me.grd.RootTable.Columns.Count - 5 Step 5
                Me.grd.RootTable.Columns(col).Visible = False
                Me.grd.RootTable.Columns(col + 1).AllowSort = False
                Me.grd.RootTable.Columns(col + 2).AllowSort = False
                Me.grd.RootTable.Columns(col + 3).AllowSort = False
                Me.grd.RootTable.Columns(col + 4).AllowSort = False

                Me.grd.RootTable.Columns(col + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(col + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(col + 3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(col + 4).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grd.RootTable.Columns(col + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(col + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(col + 3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(col + 4).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grd.RootTable.Columns(col + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(col + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(col + 3).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(col + 4).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                Me.grd.RootTable.Columns(col + 1).FormatString = "N"
                Me.grd.RootTable.Columns(col + 2).FormatString = "N"
                Me.grd.RootTable.Columns(col + 3).FormatString = "N"
                Me.grd.RootTable.Columns(col + 4).FormatString = "N"


                Me.grd.RootTable.Columns(col + 1).TotalFormatString = "N"
                Me.grd.RootTable.Columns(col + 2).TotalFormatString = "N"
                Me.grd.RootTable.Columns(col + 3).TotalFormatString = "N"
                Me.grd.RootTable.Columns(col + 4).TotalFormatString = "N"

                Me.grd.RootTable.Columns(col + 1).CellStyle.BackColor = Color.Ivory
            Next
            Me.grd.RootTable.Columns("Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Prod").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Rep").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Mkt").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Prod").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Rep").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Mkt").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            grd.RootTable.Columns("Total").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total Prod").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total Rep").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total Mkt").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("Total").FormatString = "N"
            Me.grd.RootTable.Columns("Total Prod").FormatString = "N"
            Me.grd.RootTable.Columns("Total Rep").FormatString = "N"
            Me.grd.RootTable.Columns("Total Mkt").FormatString = "N"

            Me.grd.RootTable.Columns("Total").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Total Prod").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Total Rep").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Total Mkt").TotalFormatString = "N"


            grd.RootTable.Columns("Total").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grd.RootTable.Columns("Total Prod").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grd.RootTable.Columns("Total Rep").EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grd.RootTable.Columns("Total Mkt").EditType = Janus.Windows.GridEX.EditType.TextBox


            grd.RootTable.Columns("Total").CellStyle.BackColor = Color.Azure
            Me.grd.RootTable.Columns("Total Prod").CellStyle.BackColor = Color.Snow
            Me.grd.RootTable.Columns("Total Rep").CellStyle.BackColor = Color.Snow
            Me.grd.RootTable.Columns("Total Mkt").CellStyle.BackColor = Color.Snow


            Me.grd.RootTable.Columns(0).AllowSort = False
            Me.grd.RootTable.Columns(0).FormatString = "dd/MMM/yyyy"
            Me.grd.AutoSizeColumns()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = "Item Wise Sales"
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs1 As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs1)
                fs1.Dispose()
                fs1.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Sales Detail By Category" & Chr(10) & " Dealer (" & IIf(Me.cmbAccount.Value > 0, Me.cmbAccount.Text, "All") & ")" & Chr(10) & "Date From: " & Me.dtpFrom.Value.ToString("dd-MM-yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd-Mm-yyyy") & " "
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
Public Class frmEmployeeWiseMonthlySale
    Dim _DateFrom As DateTime
    Dim _DateTo As DateTime
    Dim _Company As Integer
    Dim _Customer As Integer
    Dim _Employee As Integer
    Dim _Department As String
    Dim _Type As String
    Dim _Category As String
    Dim _SubCategory As String
    Dim _ItemFrom As Int16
    Dim _ItemTo As Integer
    Dim _InvoiceWise As Boolean
    Dim _ItemWise As Boolean
    Dim _MonthlySales As Boolean
    Dim _Dv As New DataView
    Enum enmCustomer
        Id
        Code
        AccountDescription
        Count
    End Enum
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

    Private Sub frmEmployeeWiseMonthlySale_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then

            NewToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
      
    End Sub
    Private Sub frmEmployeeWiseMonthlySale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Me.cmbyear.ValueMember = "Year"
            'Me.cmbyear.DisplayMember = "Year"
            'Me.cmbyear.DataSource = GetYears()

            'Me.cmbmonth.ValueMember = "Month"
            'Me.cmbmonth.DisplayMember = "Month_Name"
            'Me.cmbmonth.DataSource = GetMonths()

            'FillDropDown(cmbEmployee, "Select Employee_ID,Employee_Name from tblDefEmployee WHERE SalePerson=1 ORDER By 1 Asc")
            'Me.cmbyear.SelectedValue = Date.Now.Year
            'Me.cmbmonth.SelectedValue = Date.Now.Month
            Me.cmbPeriod.Text = "Current Month"
            FillCombo("Company")
            FillCombo("Customer")
            FillCombo("Employee")
            FillCombo("Department")
            FillCombo("Type")
            FillCombo("Category")
            FillCombo("SubCategory")
            FillCombo("ItemFrom")
            FillCombo("ItemTo")
            Me.tblProgressbar.Visible = False
            Me.tblProgressbar.Value = 0

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            ' Loop to get Months
            'Dim month As String = String.Empty
            'For i As Integer = 1 To Me.dtpTo.Value.Month + 1 'Me.cmbmonth.SelectedIndex + 1
            '    If month.Length > 0 Then
            '        month = month & "," & i
            '    Else
            '        month = i
            '    End If
            'Next
            'Dim j As Integer = 0
        
            Dim dateMonth As Integer = DateDiff(DateInterval.Month, Me.dtpFrom.Value, Me.dtpTo.Value)

            If Me.RadioButton1.Checked = True Then
                Dim str As String = String.Empty
                'grdEmployee.DataSource = Nothing
                str = "SELECT  DISTINCT coa_detail_id, Detail_Code as [Account Code], detail_title  as [Account Description] FROM dbo.vwCOADetail inner join SalesMasterTable on SalesMasterTable.CustomerCode =  vwCOADetail.coa_detail_id WHERE Detail_title <> '' " & IIf(Me.cmbEmployee.SelectedIndex > 0, " AND SalesMasterTable.EmployeeCode=" & Me.cmbEmployee.SelectedValue & "", "") & " AND (Convert(varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102))  " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND SalesMasterTable.LocationId=" & Me.cmbCompany.SelectedValue & "", "") & " " & IIf(Me.cmbAccount.Value > 0, " AND SalesMasterTable.CustomerCode=" & Me.cmbAccount.Value & "", "") & "  "
                Dim dt As DataTable = GetDataTable(str)
                If dt IsNot Nothing Then

                    Dim intMonth As Integer = Me.dtpTo.Value.Subtract(Me.dtpFrom.Value).Days
                    Dim AddDate As New List(Of DateTime)
                    If Me.dtpFrom.Value <> Me.dtpTo.Value Then
                        For i As Integer = 0 To intMonth - 1 'cmbmonth.SelectedValue  'Run Loop From 1st to Selected Value
                            AddDate.Add(dtpFrom.Value.AddDays(i))
                            'dt.Columns.Add(z, GetType(System.Int16), z) 'Add columns
                            'dt.Columns.Add(GetMonthName(z).ToString("MMM") & "(" & Me.dtpFrom.Value.Year & ")", GetType(System.Double))    'Go in the Functio of GetMonthName and Select Name and convert into String 
                        Next
                    Else
                        AddDate.Add(dtpFrom.Value)
                    End If
                    Dim strMonth As String = String.Empty
                    Dim myMonth As String = String.Empty
                    For Each dtDatetime As DateTime In AddDate
                        If strMonth <> dtDatetime.Month & "-" & dtDatetime.Year Then
                            myMonth = dtDatetime.Month.ToString & "" & dtDatetime.Year.ToString
                            dt.Columns.Add(myMonth, GetType(System.String), myMonth) 'Add columns
                            dt.Columns.Add(dtDatetime.ToString("MMM") & "-" & dtDatetime.Year, GetType(System.Double))    'Go in the Functio of GetMonthName and Select Name and convert into String 
                        End If
                        strMonth = dtDatetime.Month & "-" & dtDatetime.Year

                    Next
                    ' for Inserting Value 0 in each column 
                    For Each drow3 As DataRow In dt.Rows
                        For j As Integer = enmCustomer.Count To dt.Columns.Count - 2 Step 2
                            drow3.BeginEdit()
                            drow3(j + 1) = 0
                            drow3.EndEdit()
                        Next
                    Next
                End If
                'Get Total Sales of All Customers
                Dim str1 As String = "Select CustomerCode, Convert(int, Convert(varchar, Month) + '' + Convert(varchar, Year)) as strMonth,  Sales From ( " _
                    & " Select abc.Year,abc.Month,abc.CustomerCode,Sales-Expense.Expense AS Sales From " _
                    & " (SELECT    YEAR(dbo.SalesMasterTable.SalesDate) AS Year, MONTH(dbo.SalesMasterTable.SalesDate) AS Month, dbo.SalesMasterTable.CustomerCode, " _
                    & "           SUM((dbo.SalesDetailTable.Qty * dbo.SalesDetailTable.Price) + ((dbo.SalesDetailTable.Qty * dbo.SalesDetailTable.Price) * dbo.SalesDetailTable.TaxPercent / 100)) AS Sales " _
                    & " FROM         dbo.SalesDetailTable INNER JOIN " _
                    & "             dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId  WHERE (Convert(varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102))  " & IIf(Me.cmbEmployee.SelectedIndex > 0, " And SalesMasterTable.EmployeeCode=" & Me.cmbEmployee.SelectedValue & "", "") & "  " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND SalesMasterTable.LocationId=" & Me.cmbCompany.SelectedValue & "", "") & " " & IIf(Me.cmbAccount.Value > 0, " AND SalesMasterTable.CustomerCode=" & Me.cmbAccount.Value & "", "") & "  " _
                    & " GROUP BY MONTH(dbo.SalesMasterTable.SalesDate), dbo.SalesMasterTable.CustomerCode, YEAR(dbo.SalesMasterTable.SalesDate) " _
                    & " )abc " _
                    & " Left Outer Join(SELECT   YEAR(SalesDate) AS Year, MONTH(SalesDate) AS Month, CustomerCode, SUM(FuelExpense + OtherExpense + Adjustment) AS Expense " _
                    & "                FROM dbo.SalesMasterTable WHERE (Convert(varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102))  " & IIf(Me.cmbEmployee.SelectedIndex > 0, " And SalesMasterTable.EmployeeCode=" & Me.cmbEmployee.SelectedValue & "", "") & " " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND SalesMasterTable.LocationId=" & Me.cmbCompany.SelectedValue & "", "") & " " & IIf(Me.cmbAccount.Value > 0, " AND SalesMasterTable.CustomerCode=" & Me.cmbAccount.Value & "", "") & "  " _
                    & " GROUP BY MONTH(SalesDate), YEAR(SalesDate), CustomerCode)Expense on Expense.CustomerCode = abc.CustomerCode " _
                    & " ) xyz "
                ''where xyz.year BETWEEN " & Me.dtpFrom.Value.Year & " AND " & Me.dtpTo.Value.Year & " and xyz.Month in (" & month & ") "
                Dim dtdata As DataTable = GetDataTable(str1)
                Dim dr() As DataRow
                For Each Row As DataRow In dt.Rows
                    dr = dtdata.Select("CustomerCode = " & Row(0) & "")   'Select Customer Code's data
                    If dr IsNot Nothing Then
                        If dr.Length > 0 Then
                            For Each drFound As DataRow In dr
                                Row.BeginEdit()
                                Row(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)    'Insert Data
                                Row.EndEdit()
                            Next
                        End If
                    End If
                Next
                dt.Columns.Add("Total", GetType(System.Double)) ' Add New Column of Total
                Dim gettotal As String = String.Empty
                For s As Integer = enmCustomer.Count To dt.Columns.Count - 2 Step 2 'Start from 2 and jump next 2nd Column
                    If gettotal.Length > 0 Then
                        gettotal = gettotal & "+" & "[" & dt.Columns(s + 1).ColumnName & "]"
                    Else
                        gettotal = "[" & dt.Columns(s + 1).ColumnName & "]"
                    End If
                Next
                dt.Columns("Total").Expression = gettotal.ToString  ' Add all row's data
                dt.AcceptChanges()
                grdEmployee.DataSource = dt
                grdEmployee.RetrieveStructure()
                ApplygridSetting()
                CtrlGrdBar1_Load(Nothing, Nothing)
            Else
                Dim str As String = String.Empty
                'grdEmployee.DataSource = Nothing
                str = "Select ArticleDefId, [Account Code], [Account Description]  From (SELECT DISTINCT ArticleDefId, ArticleCode as [Account Code], ArticleDescription  as [Account Description], ArticleDefView.SortOrder FROM dbo.ArticleDefView inner join SalesDetailTable on SalesDetailTable.ArticleDefId =  ArticleDefView.ArticleId INNER JOIN SalesMasterTable On SalesMasterTable.SalesId = SalesDetailTable.SalesId WHERE ArticleDefView.SalesItem = '1' " & IIf(Me.cmbEmployee.SelectedIndex > 0, " AND SalesMasterTable.EmployeeCode=" & Me.cmbEmployee.SelectedValue & "", "") & " AND (Convert(varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(Datetime, '" & dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND SalesMasterTable.LocationId=" & Me.cmbCompany.SelectedValue & "", "") & " " & IIf(Me.cmbAccount.Value > 0, " AND SalesMasterTable.CustomerCode=" & Me.cmbAccount.Value & "", "") & ") abc INNER JOIN ArticleDefView on ArticleDefView.ArticleId = abc.ArticleDefId Order By ArticleDefView.SortOrder Asc"
                Dim dt As DataTable = GetDataTable(str)
                If dt IsNot Nothing Then

                    Dim intMonth As Integer = Me.dtpTo.Value.Subtract(Me.dtpFrom.Value).Days
                    Dim AddDate As New List(Of DateTime)
                    For i As Integer = 0 To intMonth - 1 'cmbmonth.SelectedValue  'Run Loop From 1st to Selected Value
                        AddDate.Add(dtpFrom.Value.AddDays(i))
                        'dt.Columns.Add(z, GetType(System.Int16), z) 'Add columns
                        'dt.Columns.Add(GetMonthName(z).ToString("MMM") & "(" & Me.dtpFrom.Value.Year & ")", GetType(System.Double))    'Go in the Functio of GetMonthName and Select Name and convert into String 
                    Next
                    Dim strMonth As String = String.Empty
                    Dim myMonth As String = String.Empty
                    For Each dtDatetime As DateTime In AddDate
                        If strMonth <> dtDatetime.Month & "-" & dtDatetime.Year Then
                            myMonth = dtDatetime.Month.ToString & "" & dtDatetime.Year.ToString
                            dt.Columns.Add(myMonth, GetType(System.String), myMonth) 'Add columns
                            dt.Columns.Add(dtDatetime.ToString("MMM") & "-" & dtDatetime.Year, GetType(System.Double))    'Go in the Functio of GetMonthName and Select Name and convert into String 
                        End If
                        strMonth = dtDatetime.Month & "-" & dtDatetime.Year

                    Next
                    ' for Inserting Value 0 in each column 
                    For Each drow3 As DataRow In dt.Rows
                        For j As Integer = enmCustomer.Count To dt.Columns.Count - 2 Step 2
                            drow3.BeginEdit()
                            drow3(j + 1) = 0
                            drow3.EndEdit()
                        Next
                    Next
                End If

                Dim str1 As String = "Select ArticleDefId, Convert(int, Convert(varchar, Month) + '' + Convert(varchar, Year)) as strMonth,  Sales From ( " _
                                  & " Select abc.Year,abc.Month, abc.ArticleDefId, Sales  From " _
                                  & " (SELECT    YEAR(dbo.SalesMasterTable.SalesDate) AS Year, MONTH(dbo.SalesMasterTable.SalesDate) AS Month, dbo.SalesDetailTable.ArticleDefId, " _
                                  & "           SUM((dbo.SalesDetailTable.Qty * dbo.SalesDetailTable.Price) + ((dbo.SalesDetailTable.Qty * dbo.SalesDetailTable.Price) * dbo.SalesDetailTable.TaxPercent / 100)) AS Sales " _
                                  & " FROM         dbo.SalesDetailTable INNER JOIN " _
                                  & "             dbo.SalesMasterTable ON dbo.SalesDetailTable.SalesId = dbo.SalesMasterTable.SalesId  WHERE (Convert(varchar, SalesMasterTable.SalesDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', 102))  " & IIf(Me.cmbEmployee.SelectedIndex > 0, " And SalesMasterTable.EmployeeCode=" & Me.cmbEmployee.SelectedValue & "", "") & " " & IIf(Me.cmbCompany.SelectedIndex > 0, " AND SalesMasterTable.LocationId=" & Me.cmbCompany.SelectedValue & "", "") & " " & IIf(Me.cmbAccount.Value > 0, " AND SalesMasterTable.CustomerCode=" & Me.cmbAccount.Value & "", "") & "   " _
                                  & " GROUP BY MONTH(dbo.SalesMasterTable.SalesDate), dbo.SalesDetailTable.ArticleDefId, YEAR(dbo.SalesMasterTable.SalesDate) " _
                                  & " )abc " _
                                  & " ) xyz INNER JOIN ArticleDefView On ArticleDefView.ArticleId = xyz.ArticleDefId "
                str1 += " WHERE ArticleDefview.ArticleDescription <> ''"
                If Me.GroupBox3.Enabled = True Then
                    str1 += " AND ArticleDefView.ArticleGroupId In (" & Me.cmbDepartment.SelectedIDs & ") "
                    str1 += " AND ArticleDefView.ArticleTypeId In (" & Me.cmbType.SelectedIDs & ") "
                    str1 += " AND ArticleDefView.ArticleLPOId In (" & Me.cmbCategory.SelectedIDs & ") "
                    str1 += " AND ArticleDefView.CompanyId In (" & Me.cmbSubCategory.SelectedIDs & ") "
                End If
                If Me.GroupBox4.Enabled = True Then
                    If Me.cmbItem.Value > 0 Then
                        str1 += " AND ArticleDefView.ArticleId >= " & Me.cmbItem.Value & ""
                    End If

                    If Me.cmbItemTo.Value > 0 Then
                        str1 += " AND ArticleDefView.ArticleId <= " & Me.cmbItemTo.Value & ""
                    End If
                End If
                ''where xyz.year BETWEEN " & Me.dtpFrom.Value.Year & " AND " & Me.dtpTo.Value.Year & " and xyz.Month in (" & month & ") "
                Dim dtdata As DataTable = GetDataTable(str1)
                Dim dr() As DataRow
                For Each Row As DataRow In dt.Rows
                    dr = dtdata.Select("ArticleDefId = " & Row(0) & "")   'Select Customer Code's data
                    If dr IsNot Nothing Then
                        If dr.Length > 0 Then
                            For Each drFound As DataRow In dr
                                Row.BeginEdit()
                                Row(dt.Columns.IndexOf(drFound(1)) + 1) = drFound(2)    'Insert Data
                                Row.EndEdit()
                            Next
                        End If
                    End If
                Next
                dt.Columns.Add("Total", GetType(System.Double)) ' Add New Column of Total
                Dim gettotal As String = String.Empty
                For s As Integer = enmCustomer.Count To dt.Columns.Count - 2 Step 2 'Start from 2 and jump next 2nd Column
                    If gettotal.Length > 0 Then
                        gettotal = gettotal & "+" & "[" & dt.Columns(s + 1).ColumnName & "]"
                    Else
                        gettotal = "[" & dt.Columns(s + 1).ColumnName & "]"
                    End If
                Next
                dt.Columns("Total").Expression = gettotal.ToString  ' Add all row's data
                dt.AcceptChanges()
                grdEmployee.DataSource = dt
                grdEmployee.RetrieveStructure()
                ApplygridSetting()
            End If
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplygridSetting()
        Try
            Me.grdEmployee.RootTable.Columns(0).Visible = False
            Me.grdEmployee.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdEmployee.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            ' For Hiding Columns
            For i As Integer = enmCustomer.Count To Me.grdEmployee.RootTable.Columns.Count - 2 Step 2
                Me.grdEmployee.RootTable.Columns(i).Visible = False
                'For Alligment
                Me.grdEmployee.RootTable.Columns(i + 1).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdEmployee.RootTable.Columns(i + 1).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdEmployee.RootTable.Columns(i + 1).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdEmployee.RootTable.Columns(i + 1).FormatString = "N"
                Me.grdEmployee.RootTable.Columns(i + 1).TotalFormatString = "N"
                Me.grdEmployee.RootTable.Columns(i + 1).AllowSort = False
            Next
            ' Fot Total Column Allignment
            Me.grdEmployee.RootTable.Columns("Total").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployee.RootTable.Columns("Total").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdEmployee.RootTable.Columns("Total").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdEmployee.RootTable.Columns("Total").FormatString = "N"
            Me.grdEmployee.RootTable.Columns("Total").TotalFormatString = "N"
            Me.grdEmployee.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        Try
            btnLoad_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        'R-974 Ehtisham ul Haq user friendly system modification on 16-1-14 

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Try
            'Dim Id As Integer = 0I
            'Id = Me.cmbmonth.SelectedValue
            'GetMonths()
            'Me.cmbmonth.SelectedValue = Id
            'Id = Me.cmbyear.SelectedValue
            'GetYears()
            'Me.cmbyear.SelectedValue = Id
            'Id = Me.cmbEmployee.SelectedValue
            'FillDropDown(cmbEmployee, "Select Employee_ID,Employee_Name from tblDefEmployee WHERE SalePerson=1 ORDER By 1 Asc")
            'Me.cmbEmployee.SelectedValue = Id
            Dim Id As Integer = 0I
            Id = Me.cmbAccount.Value
            FillCombo("Customer")
            Me.cmbAccount.Value = Id
            Id = Me.cmbEmployee.SelectedValue
            FillCombo("Employee")
            Me.cmbEmployee.SelectedValue = Id
            'Id = Me.cmbDepartment.SelectedValue
            FillCombo("Department")
            'Me.cmbDepartment.SelectedValue = Id
            'Id = Me.cmbType.SelectedValue
            FillCombo("Type")
            'Me.cmbType.SelectedValue = Id
            'Id = Me.cmbCategory.SelectedValue
            FillCombo("Category")
            'Me.cmbCategory.SelectedValue = Id
            'Id = Me.cmbSubCategory.SelectedValue
            FillCombo("SubCategory")
            'Me.cmbSubCategory.SelectedValue = Id
            Id = Me.cmbItem.Value
            FillCombo("ItemFrom")
            Me.cmbItem.Value = Id
            Id = Me.cmbItemTo.Value
            FillCombo("ItemTo")
            Me.cmbItemTo.Value = Id
            Id = Me.cmbCompany.SelectedValue
            FillCombo("Company")
            Me.cmbCompany.SelectedValue = Id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblprogress.visible = False
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployee.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdEmployee.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grdEmployee.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Employee Wise Monthly Sales " & vbCrLf & " Date From: " & Me.dtpFrom.Value & "  Date To: " & Me.dtpTo.Value & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "Customer" Then
                FillUltraDropDown(Me.cmbAccount, "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                   "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone " & _
                                                   "FROM         tblCustomer LEFT OUTER JOIN " & _
                                                   "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                   "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                   "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                   "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                   "WHERE  vwCOADetail.coa_detail_id is not  null  And " & IIf(getConfigValueByType("Show Vendor On Sales") = "True", "  (vwCOADetail.account_type In ('Customer','Vendor')) ", " (vwCOADetail.account_type='Customer') ") & "")
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
            ElseIf Condition = "Employee" Then
                FillDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name From tblDefEmployee WHERE SalePerson=1 And Active = 1") ''TASKTFS75 added and set active =1
            ElseIf Condition = "Department" Then
                FillListBox(Me.cmbDepartment.ListItem, "Select ArticleGroupId, ArticleGroupName From ArticleGroupDefTable")
            ElseIf Condition = "Type" Then
                FillListBox(Me.cmbType.ListItem, "Select ArticleTypeId, ArticleTypeName From ArticleTypeDefTable")
            ElseIf Condition = "Category" Then
                FillListBox(Me.cmbCategory.ListItem, "Select ArticleCompanyId, ArticleCompanyName From ArticleCompanyDefTable")
            ElseIf Condition = "SubCategory" Then
                FillListBox(Me.cmbSubCategory.ListItem, "Select ArticleLpoId, ArticleLpoName From ArticleLpoDefTable")
            ElseIf Condition = "ItemFrom" Then
                FillUltraDropDown(Me.cmbItem, "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock FROM ArticleDefView where Active=1 AND SalesItem=1")
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = True
            ElseIf Condition = "ItemTo" Then
                FillUltraDropDown(Me.cmbItemTo, "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock FROM ArticleDefView where Active=1 AND SalesItem=1")
                Me.cmbItemTo.Rows(0).Activate()
                Me.cmbItemTo.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItemTo.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
                Me.cmbItemTo.DisplayLayout.Bands(0).Columns("Price").Hidden = True
            ElseIf Condition = "Company" Then
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable")
            End If
        Catch ex As Exception
            Throw ex
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

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Try
            If Me.RadioButton1.Checked = True Then
                Me.GroupBox3.Enabled = False
                Me.GroupBox4.Enabled = False
                _InvoiceWise = True
                _ItemWise = False
            ElseIf Me.RadioButton2.Checked = True Then
                Me.GroupBox3.Enabled = True
                Me.GroupBox4.Enabled = True
                _InvoiceWise = True
                _ItemWise = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
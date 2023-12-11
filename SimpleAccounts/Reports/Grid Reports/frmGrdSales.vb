Public Class frmGrdSales
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
    Enum enmInvoiceWise
        SalesId
        SalesNo
        SalesDate
        Detail_Code
        Detail_Title
        FuelExpense
        OtherExpense
        Adjustment
        Qty
        SampleQty
        Discount
        Tax
        Amount
        Employee_Name
        User_Name
        coa_detail_id
        Employee_Code
        LocationId
    End Enum
    Enum enmItemWise
        SalesId
        SalesNo
        SalesDate
        Detail_Code
        Detail_Title
        ArticleCode
        ArticleDescription
        Size
        Color
        Qty
        SampleQty
        Price
        Discount
        Tax
        Amount
        Employee_Name
        User_Name
        coa_detail_id
        Employee_Code
        GroupId
        CompanyId
        LPOId
        TypeId
        ArticleId
        LocationId
    End Enum

    Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try
            Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
            Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

            If Condition = "InvoiceWise" Then
                Me.grd.RootTable.Columns(enmInvoiceWise.SalesId).Visible = False
                Me.grd.RootTable.Columns(enmInvoiceWise.coa_detail_id).Visible = False
                Me.grd.RootTable.Columns(enmInvoiceWise.Employee_Code).Visible = False
                Me.grd.RootTable.Columns(enmInvoiceWise.LocationId).Visible = False
                Me.grd.RootTable.Columns(enmInvoiceWise.SalesDate).FormatString = "dd/MMM/yyyy"
                Me.grd.RootTable.Columns(enmInvoiceWise.FuelExpense).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmInvoiceWise.OtherExpense).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmInvoiceWise.Adjustment).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmInvoiceWise.Qty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmInvoiceWise.SampleQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmInvoiceWise.Discount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmInvoiceWise.Tax).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmInvoiceWise.Amount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmInvoiceWise.FuelExpense).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.OtherExpense).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.Adjustment).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.SampleQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.Discount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.Tax).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.Amount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.FuelExpense).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.OtherExpense).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.Adjustment).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.Qty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.SampleQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.Discount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.Tax).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmInvoiceWise.Amount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grd.RootTable.Columns(enmInvoiceWise.FuelExpense).TotalFormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.OtherExpense).TotalFormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.Adjustment).TotalFormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.Qty).TotalFormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.SampleQty).TotalFormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.Discount).TotalFormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.Tax).TotalFormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.Amount).TotalFormatString = "N"


                Me.grd.RootTable.Columns(enmInvoiceWise.FuelExpense).FormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.OtherExpense).FormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.Adjustment).FormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.Qty).FormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.SampleQty).FormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.Discount).FormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.Tax).FormatString = "N"
                Me.grd.RootTable.Columns(enmInvoiceWise.Amount).FormatString = "N"

                Me.grd.AutoSizeColumns()
            ElseIf Condition = "ItemWise" Then
                Me.grd.RootTable.Columns(enmItemWise.SalesId).Visible = False
                Me.grd.RootTable.Columns(enmItemWise.coa_detail_id).Visible = False
                Me.grd.RootTable.Columns(enmItemWise.Employee_Code).Visible = False
                Me.grd.RootTable.Columns(enmItemWise.GroupId).Visible = False
                Me.grd.RootTable.Columns(enmItemWise.CompanyId).Visible = False
                Me.grd.RootTable.Columns(enmItemWise.LPOId).Visible = False
                Me.grd.RootTable.Columns(enmItemWise.TypeId).Visible = False
                Me.grd.RootTable.Columns(enmItemWise.ArticleId).Visible = False
                Me.grd.RootTable.Columns(enmItemWise.LocationId).Visible = False
                Me.grd.RootTable.Columns(enmItemWise.SalesDate).FormatString = "dd/MMM/yyyy"
                Me.grd.RootTable.Columns(enmItemWise.Qty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmItemWise.SampleQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmItemWise.Discount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmItemWise.Tax).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmItemWise.Amount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grd.RootTable.Columns(enmItemWise.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmItemWise.SampleQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmItemWise.Discount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmItemWise.Tax).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmItemWise.Amount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmItemWise.Qty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmItemWise.SampleQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmItemWise.Discount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmItemWise.Tax).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(enmItemWise.Amount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grd.RootTable.Columns(enmItemWise.Qty).TotalFormatString = "N"
                Me.grd.RootTable.Columns(enmItemWise.SampleQty).TotalFormatString = "N"
                Me.grd.RootTable.Columns(enmItemWise.Discount).TotalFormatString = "N"
                Me.grd.RootTable.Columns(enmItemWise.Tax).TotalFormatString = "N"
                Me.grd.RootTable.Columns(enmItemWise.Amount).TotalFormatString = "N"

                Me.grd.RootTable.Columns(enmItemWise.Qty).FormatString = "N"
                Me.grd.RootTable.Columns(enmItemWise.SampleQty).FormatString = "N"
                Me.grd.RootTable.Columns(enmItemWise.Discount).FormatString = "N"
                Me.grd.RootTable.Columns(enmItemWise.Tax).FormatString = "N"
                Me.grd.RootTable.Columns(enmItemWise.Amount).FormatString = "N"

                Me.grd.AutoSizeColumns()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Dim _DateFrom As DateTime
    Dim _DateTo As DateTime
    Dim _Company As String
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

    Private Sub frmGrdSales_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmGrdSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            FillCombos("Company")
            FillCombos("Customer")
            FillCombos("Employee")
            FillCombos("Department")
            FillCombos("Type")
            FillCombos("Category")
            FillCombos("SubCategory")
            FillCombos("ItemFrom")
            FillCombos("ItemTo")
            Me.tblProgressbar.Visible = False
            Me.tblProgressbar.Value = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            If Condition = "Customer" Then
                Dim Str As String = "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                   "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone " & _
                                                   "FROM         tblCustomer LEFT OUTER JOIN " & _
                                                   "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                   "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                   "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                   "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                   "WHERE  vwCOADetail.coa_detail_id is not  null"
                ''Start TFS2124
                If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                    Str += " AND (vwCOADetail.Account_Type = 'Customer')  "
                Else
                    Str += " AND (vwCOADetail.Account_Type in('Customer','Vendor'))  "
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
            ElseIf Condition = "Employee" Then
                FillDropDown(Me.cmbEmployee, "Select Employee_Id, Employee_Name From tblDefEmployee WHERE SalePerson=1 And Active=1") ''''TASKTFS75 added and set active = 1
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
                FillListBox(Me.uilstCompany.ListItem, "Select CompanyId, CompanyName From CompanyDefTable")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub cmbCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.cmbCategory.SelectedIndex = -1 Then Exit Sub
    '        FillCombos("SubCategory")
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim Id As Integer = 0I
            Id = Me.cmbAccount.Value
            FillCombos("Customer")
            Me.cmbAccount.Value = Id
            Id = Me.cmbEmployee.SelectedValue
            FillCombos("Employee")
            Me.cmbEmployee.SelectedValue = Id
            'Id = Me.cmbDepartment.SelectedValue
            FillCombos("Department")
            'Me.cmbDepartment.SelectedValue = Id
            'Id = Me.cmbType.SelectedValue
            FillCombos("Type")
            'Me.cmbType.SelectedValue = Id
            'Id = Me.cmbCategory.SelectedValue
            FillCombos("Category")
            'Me.cmbCategory.SelectedValue = Id
            'Id = Me.cmbSubCategory.SelectedValue
            FillCombos("SubCategory")
            'Me.cmbSubCategory.SelectedValue = Id
            Id = Me.cmbItem.Value
            FillCombos("ItemFrom")
            Me.cmbItem.Value = Id
            Id = Me.cmbItemTo.Value
            FillCombos("ItemTo")
            Me.cmbItemTo.Value = Id
            FillCombos("Company")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged, RadioButton2.CheckedChanged
        Try
            If Me.RadioButton1.Checked = True Then
                Me.GroupBox3.Enabled = False
                Me.GroupBox4.Enabled = False
                _InvoiceWise = True
                _ItemWise = False
                _MonthlySales = False
            ElseIf Me.RadioButton2.Checked = True Then
                Me.GroupBox3.Enabled = True
                Me.GroupBox4.Enabled = True
                _InvoiceWise = True
                _ItemWise = True
                _MonthlySales = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetAll() As DataView
        Try
            Dim str As String = String.Empty
            Dim strFilter As String = String.Empty
            If _InvoiceWise = True Then
                str = "SP_SalesInvoiceWise"
            Else
                str = "SP_SalesItemsWise"
            End If
            Dim dt As DataTable = GetDataTable(str)
            dt.TableName = "ProbableSales"
            _Dv.Table = dt

            strFilter = " SalesDate>='" & _DateFrom.ToString("yyyy-M-d 00:00:00") & "'"

            strFilter += " AND SalesDate <='" & _DateTo.ToString("yyyy-M-d 23:59:59") & "'"

            If _Customer > 0 Then
                strFilter += " AND coa_detail_id=" & _Customer & ""
            End If

            If _Employee > 0 Then
                strFilter += " AND EmployeeCode=" & _Employee & ""
            End If

            If _Company > 0 Then
                strFilter += " AND LocationId IN (" & _Company & ")"
            End If

            If _InvoiceWise = False Then
                If _Department.Length > 0 Then
                    strFilter += " AND ArticleGroupId In(" & _Department & ")"
                End If

                If _Type.Length > 0 Then
                    strFilter += " AND ArticleTypeId In(" & _Type & ")"
                End If

                If _Category.Length > 0 Then
                    strFilter += " AND ArticleCompanyId In(" & _Category & ")"
                End If

                If _SubCategory.Length > 0 Then
                    strFilter += " AND ArticleLPOId In(" & _SubCategory & ")"
                End If
                If _ItemFrom > 0 AndAlso _ItemTo > 0 Then
                    strFilter += " AND ArticleId >=" & _ItemFrom & " AND ArticleId <=" & _ItemTo & " "
                ElseIf _ItemFrom > 0 AndAlso _ItemTo = 0 Then
                    strFilter += " AND ArticleId =" & _ItemFrom & ""
                ElseIf _ItemFrom = 0 AndAlso _ItemTo > 0 Then
                    strFilter += " AND ArticleId =" & _ItemTo & ""
                End If
            End If
            _Dv.RowFilter = strFilter
            Return _Dv
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Private Sub frmGrdSales_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    '    Dim lbl As New Label
    '    Try


    '        lbl.Visible = True
    '        lbl.Dock = DockStyle.Fill
    '        lbl.Text = "Loading, Please wait..."
    '        Me.Controls.Add(lbl)
    '        lbl.BringToFront()

    '        _DateFrom = Me.dtpFrom.Value
    '        _DateTo = Me.dtpTo.Value
    '        _Customer = Me.cmbAccount.Value
    '        _Employee = Me.cmbEmployee.SelectedValue
    '        _InvoiceWise = IIf(Me.RadioButton1.Checked = True, True, False)
    '        _Department = Me.cmbDepartment.SelectedIDs
    '        _Type = Me.cmbType.SelectedIDs
    '        _Category = Me.cmbCategory.SelectedIDs
    '        _SubCategory = Me.cmbSubCategory.SelectedIDs
    '        _ItemFrom = Me.cmbItem.Value
    '        _ItemTo = Me.cmbItemTo.Value

    '        If BackgroundWorker1.IsBusy Then Exit Sub
    '        BackgroundWorker1.RunWorkerAsync()
    '        Do While BackgroundWorker1.IsBusy
    '            Application.DoEvents()
    '        Loop

    '        If _Dv IsNot Nothing Then
    '            Me.grd.DataSource = Nothing
    '            Me.grd.DataSource = _Dv
    '            Me.grd.RetrieveStructure()
    '            If Me.RadioButton1.Checked = True Then
    '                ApplyGridSettings("InvoiceWise")
    '            Else
    '                ApplyGridSettings("ItemWise")
    '            End If
    '            CtrlGrdBar1_Load(Nothing, Nothing)
    '        End If

    '        Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(1).TabPage.Tab
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    Finally
    '        lbl.Visible = False
    '    End Try
    'End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim lbl As New Label
        Try
            Me.tblProgressbar.Visible = True
            Me.tblProgressbar.Value = 0
            lbl.Visible = True
            lbl.Dock = DockStyle.Fill
            lbl.Text = "Loading..."
            Me.Controls.Add(lbl)
            lbl.BringToFront()
            _DateFrom = Me.dtpFrom.Value
            _DateTo = Me.dtpTo.Value
            _Customer = Me.cmbAccount.Value
            _Employee = Me.cmbEmployee.SelectedValue
            _InvoiceWise = IIf(Me.RadioButton1.Checked = True, True, False)
            _Department = Me.cmbDepartment.SelectedIDs
            _Type = Me.cmbType.SelectedIDs
            _Category = Me.cmbCategory.SelectedIDs
            _SubCategory = Me.cmbSubCategory.SelectedIDs
            _ItemFrom = Me.cmbItem.Value
            _ItemTo = Me.cmbItemTo.Value
            _Company = Me.uilstCompany.SelectedIDs
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop

            If _Dv IsNot Nothing Then
                Me.grd.DataSource = Nothing
                Me.grd.DataSource = _Dv
                Me.grd.RetrieveStructure()
                If Me.RadioButton1.Checked = True Then
                    ApplyGridSettings("InvoiceWise")
                Else
                    ApplyGridSettings("ItemWise")
                End If
                CtrlGrdBar1_Load(Nothing, Nothing)
            End If

            Me.UltraTabControl2.SelectedTab = Me.UltraTabControl2.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
            Me.tblProgressbar.Visible = False
            Me.tblProgressbar.Value = 0
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Probable Sales Report" & vbCrLf & "Date From:" & _DateFrom.ToString("dd-MMM-yyyy") & " Month: " & _DateTo.ToString("dd-MMM-yyyy") & " "

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

 
    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub
    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click

    End Sub
    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

    End Sub
    Private Sub dtpTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpTo.ValueChanged

    End Sub
    Private Sub dtpFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFrom.ValueChanged

    End Sub
End Class
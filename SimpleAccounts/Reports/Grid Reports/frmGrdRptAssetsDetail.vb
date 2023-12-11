Public Class frmGrdRptAssetsDetail
    Public _Category As String
    Public _Type As String
    Public _Condition As String
    Public _Status As String
    Public _Location As String
    Public _VendorId As Integer
    Public _EmployeeId As Integer
    Public _PriceFrom As Double
    Public _PriceTo As Double
    Public _PurchaseDateFromCheck As Boolean = False
    Public _PurchaseDateToCheck As Boolean = False
    Public _PurchaseDateFrom As DateTime
    Public _PurchaseDateTo As DateTime
    Dim dv As New DataView

    Private Sub frmGrdRptAssetsDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmGrdRptAssetsDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombobox("Category")
            FillCombobox("Type")
            FillCombobox("Condition")
            FillCombobox("Status")
            FillCombobox("Location")
            FillCombobox("Vendor")
            FillCombobox("Employee")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombobox(Optional ByVal Condition As String = "")
        Try
            If Condition = "Category" Then
                FillListBox(Me.cmbCategory.ListItem, "Select Asset_Category_Id, Asset_Category_Name From tblAssetCategory")
            ElseIf Condition = "Type" Then
                FillListBox(Me.cmbType.ListItem, "Select Asset_Type_Id, Asset_Type_Name From tblAssetType")
            ElseIf Condition = "Condition" Then
                FillListBox(Me.cmbCondition.ListItem, "Select Asset_Condition_Id, Asset_Condition_Name From tblAssetCondition")
            ElseIf Condition = "Status" Then
                FillListBox(Me.cmbStatus.ListItem, "Select Asset_Status_Id, Asset_Status_Name From tblAssetStatus")
            ElseIf Condition = "Location" Then
                FillListBox(Me.cmbLocation.ListItem, "Select Asset_Location_Id, Asset_Location_Name From tblAssetLocation")
            ElseIf Condition = "Vendor" Then
                FillDropDown(Me.ComboBox1, "Select coa_detail_id, detail_title From vwcoadetail where account_type='Vendor'")
            ElseIf Condition = "Employee" Then
                FillDropDown(Me.ComboBox2, "Select Employee_Id, Employee_Name From tblDefEmployee Where Active = 1") ''TASKTFS75 added and set active =1
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function FillData() As DataView
        Try
            Dim strFilter As String = String.Empty
            Dim str As String = "SP_AssetsDetail"
            Dim dt As DataTable = GetDataTable(str)
            dt.TableName = "Asset Detail"
            dv.Table = dt
            strFilter = "Asset_Name <> ''"
            dv.RowFilter = strFilter
            If _Category.Length > 0 Then
                strFilter += " AND Asset_Category_Id In(" & _Category & ")"
            End If
            If _Type.Length > 0 Then
                strFilter += " AND Asset_Type_Id In (" & _Type & ")"
            End If
            If _Condition.Length > 0 Then
                strFilter += " AND Asset_Type_Id In(" & _Condition & ")"
            End If
            If _Status.Length > 0 Then
                strFilter += " AND Asset_Status_Id In (" & _Status & ")"
            End If
            If _Location.Length > 0 Then
                strFilter += " AND Asset_Location_Id In (" & _Location & ")"
            End If
            If _VendorId > 0 Then
                strFilter += " AND VendorId=" & _VendorId & ""
            End If
            If _EmployeeId > 0 Then
                strFilter += " AND EmployeeId=" & _EmployeeId & ""
            End If
            If _PriceFrom > 0 Then
                strFilter += " AND PurchasePrice >= " & _PriceFrom & ""
            End If
            If _PriceTo > 0 Then
                strFilter += " AND PurchaseTo <= " & _PriceTo & ""
            End If
            If _PurchaseDateFromCheck = True Then
                strFilter += " AND PurchaseDate >='" & _PurchaseDateFrom.ToString("yyyy-M-d 00:00:00") & "'"
            End If
            If _PurchaseDateToCheck = True Then
                strFilter += " AND PurchaseDate <='" & _PurchaseDateTo.ToString("yyyy-M-d 23:59:59") & "'"
            End If
            dv.RowFilter = strFilter
            Return dv
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            FillData()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            _Category = Me.cmbCategory.SelectedIDs
            _Type = Me.cmbType.SelectedIDs
            _Condition = Me.cmbCondition.SelectedIDs
            _Status = Me.cmbStatus.SelectedIDs
            _Location = Me.cmbLocation.SelectedIDs
            _VendorId = Me.ComboBox1.SelectedValue
            _EmployeeId = Me.ComboBox2.SelectedValue
            _PriceFrom = Val(Me.TextBox1.Text)
            _PriceTo = Val(Me.TextBox2.Text)
            If Me.dtpFrom.Checked = True Then
                _PurchaseDateFromCheck = True
                _PurchaseDateFrom = Me.dtpFrom.Value
            End If
            If Me.dtpTo.Checked = True Then
                _PurchaseDateToCheck = True
                _PurchaseDateTo = Me.dtpTo.Value
            End If
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop

            If dv IsNot Nothing Then
                Me.grd.DataSource = dv
                Me.grd.RetrieveStructure()
                ApplyGridSetting()
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSetting()
        Try
            If Me.grd.RootTable Is Nothing Then Exit Sub
            Me.grd.RootTable.Columns("Asset_Category_Id").Visible = False
            Me.grd.RootTable.Columns("Asset_Type_Id").Visible = False
            Me.grd.RootTable.Columns("Asset_Condition_Id").Visible = False
            Me.grd.RootTable.Columns("Asset_Status_Id").Visible = False
            Me.grd.RootTable.Columns("Asset_Location_Id").Visible = False
            Me.grd.RootTable.Columns("Asset_Location").Visible = False
            Me.grd.RootTable.Columns("VendorId").Visible = False
            Me.grd.RootTable.Columns("EmployeeId").Visible = False
            Dim CategoryGroup As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Category"))
            Me.grd.RootTable.Groups.Add(CategoryGroup)
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub frmGrdRptAssetsDetail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
    '    Try
    '        _Category = Me.cmbCategory.SelectedIDs
    '        _Type = Me.cmbType.SelectedIDs
    '        _Condition = Me.cmbCondition.SelectedIDs
    '        _Status = Me.cmbStatus.SelectedIDs
    '        _Location = Me.cmbLocation.SelectedIDs
    '        _VendorId = Me.ComboBox1.SelectedValue
    '        _EmployeeId = Me.ComboBox2.SelectedValue
    '        _PriceFrom = Val(Me.TextBox1.Text)
    '        _PriceTo = Val(Me.TextBox2.Text)
    '        If Me.dtpFrom.Checked = True Then
    '            _PurchaseDateFromCheck = True
    '            _PurchaseDateFrom = Me.dtpFrom.Value
    '        End If
    '        If Me.dtpTo.Checked = True Then
    '            _PurchaseDateToCheck = True
    '            _PurchaseDateTo = Me.dtpTo.Value
    '        End If
    '        If BackgroundWorker1.IsBusy Then Exit Sub
    '        BackgroundWorker1.RunWorkerAsync()
    '        Do While BackgroundWorker1.IsBusy
    '            Application.DoEvents()
    '        Loop

    '        If dv IsNot Nothing Then
    '            Me.grd.DataSource = dv
    '            Me.grd.RetrieveStructure()
    '            ApplyGridSetting()
    '            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
    '        End If

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            FillCombobox("Category")
            FillCombobox("Type")
            FillCombobox("Condition")
            FillCombobox("Status")
            FillCombobox("Location")
            FillCombobox("Vendor")
            FillCombobox("Employee")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load

    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Invoice Wise Payable Report" & Chr(10) & "From Date: " & dtpFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpTo.Value.ToString("dd-MM-yyyy").ToString & ""
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
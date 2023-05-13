''07-Aug-2014 Task:2774 Imran Ali Revised Invoice Based Aging Report (Ravi)
Imports SBModel
Public Class frmGrdRptInvoiceAging

    Public flgCompanyRights As Boolean = False
    Dim IsOpenForm As Boolean = False
    Dim dtData As DataTable
    Dim dtCustomer As DataTable
    Dim dvCustomer As DataView
    Dim strAging As Integer = 1
    Dim str1stAging As Integer = 30
    Dim str1stAgingName As String = "1_30"
    Dim str2ndAging As Integer = 60
    Dim str2ndAgingName As String = "30_60"
    Dim str3rdAging As Integer = 90
    Dim str3rdAgingName As String = "60_90"
    Dim str4thAging As Integer = 180
    Dim str4thAgingName As String = "90_180"
    Dim str5thAging As Integer = 270
    Dim str5thAgingName As String = "180_270"
    Dim str6thAging As Integer = 360
    Dim str6thAgingName As String = "360+"

    Private Sub rbtVendor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtVendor.CheckedChanged
        Try
            If IsOpenForm = True Then
                FillCombos("SubSub")
                FillCombos("Vendor")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try

            Dim strVendorCustomer As String = getConfigValueByType("Show Customer On Purchase").ToString
            Dim strCustomerVendor As String = getConfigValueByType("Show Vendor On Sales").ToString
            Dim Str As String = ""
            If Condition = "Vendor" Then
                If strVendorCustomer = "True" Then
                    Str = "Select coa_detail_id, detail_title + '~' + detail_code as Customer_Name , account_type From vwCOADetail where account_type IN('Vendor','Customer') AND detail_title <> '' And main_sub_sub_id in (Select main_sub_sub_id From tblCOAMainSubSub Where account_type IN('Vendor','Customer')) "
                Else
                    Str = "Select coa_detail_id, detail_title + '~' + detail_code as Customer_Name , account_type From vwCOADetail where account_type IN('Vendor') AND detail_title <> '' And main_sub_sub_id in (Select main_sub_sub_id From tblCOAMainSubSub Where account_type IN('Vendor')) "
                End If
                Str += " ORDER BY 2 ASC "
                dtCustomer = GetDataTable(Str)
                dtCustomer.TableName = "Customer"
                dvCustomer = New DataView
                dvCustomer.Table = dtCustomer
                FillListBox(Me.lslAccountList.ListItem, Str)
                Me.lslAccountList.DeSelect()
            ElseIf Condition = "Customer" Then
                If strCustomerVendor = "True" Then
                    Str = "Select coa_detail_id, detail_title + '~' + detail_code as Customer_Name , account_type From vwCOADetail where account_type IN('Customer','Vendor') AND detail_title <> '' And main_sub_sub_id in (Select main_sub_sub_id From tblCOAMainSubSub Where account_type IN('Vendor','Customer')) "
                Else
                    Str = "Select coa_detail_id, detail_title + '~' + detail_code as Customer_Name , account_type From vwCOADetail where account_type='Customer' AND detail_title <> '' And main_sub_sub_id in (Select main_sub_sub_id From tblCOAMainSubSub Where account_type IN('Customer')) "
                End If
                Str += " ORDER BY 2 ASC "
                dtCustomer = GetDataTable(Str)
                dtCustomer.TableName = "Customer"
                dvCustomer = New DataView
                dvCustomer.Table = dtCustomer
                FillListBox(Me.lslAccountList.ListItem, Str)
                Me.lslAccountList.DeSelect()
            End If

            If Condition = "SubSub" Then
                If rbtCustomer.Checked = True Then
                    FillListBox(Me.lstSubSubAccount.ListItem, "Select main_sub_sub_id, sub_sub_title As [Sub Sub Title], account_type As [Account Type] From tblCOAMainSubSub Where account_type = 'Customer'")
                Else
                    FillListBox(Me.lstSubSubAccount.ListItem, "Select main_sub_sub_id, sub_sub_title As [Sub Sub Title], account_type As [Account Type] From tblCOAMainSubSub Where account_type = 'Vendor'")
                End If
            End If
            Me.lstSubSubAccount.DeSelect()
            Str = "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & " And CompanyId Is Not Null) " _
                & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable WHERE CompanyName <> '' " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")" _
                & "Else " _
                & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
            FillListBox(Me.lstCompany.ListItem, Str)
            Me.lstCompany.DeSelect()
            Str = " If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 "
            FillListBox(Me.lstCostCenter.ListItem, Str)
            Me.lstCostCenter.DeSelect()
            Str = "Select distinct CostCenterGroup, CostCenterGroup from tbldefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1"
            FillListBox(Me.lstCostCenterHead.ListItem, Str)
            Me.lstCostCenterHead.DeSelect()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnAddAging.Enabled = True
                Me.btnGetAll.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmGrdRptClosingStockByGRNnDC)
            Me.Visible = False
            Me.btnAddAging.Enabled = False
            Me.btnGetAll.Enabled = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
                ElseIf RightsDt.FormControlName = "Add Aging" Then
                    Me.btnAddAging.Enabled = True
                ElseIf RightsDt.FormControlName = "Get All" Then
                    Me.btnGetAll.Enabled = True
                ElseIf RightsDt.FormControlName = "Print" Then
                    CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf RightsDt.FormControlName = "Export" Then
                    CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf RightsDt.FormControlName = "Field Chooser" Then
                    CtrlGrdBar1.mGridChooseFielder.Enabled = True

                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub rbtCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtCustomer.CheckedChanged
        Try
            If IsOpenForm = True Then
                FillCombos("SubSub")
                FillCombos("Customer")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub




    Private Sub frmGrdRptInvoiceAging_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If
            GetSecurityRights()
            dtData = New DataTable
            dtData.TableName = "tblAgingTemplate"
            dtData.Columns.Add("Id", GetType(System.Int32))
            dtData.Columns("Id").AutoIncrement = True
            dtData.Columns("Id").AutoIncrementSeed = 1
            dtData.Columns("Id").AutoIncrementStep = 1
            dtData.Columns.Add("Format_Name", GetType(System.String))
            dtData.Columns.Add("Aging", GetType(System.Int32))
            dtData.Columns.Add("1stAging", GetType(System.Int32))
            dtData.Columns.Add("1stAgingName", GetType(System.String))
            dtData.Columns.Add("2ndAging", GetType(System.Int32))
            dtData.Columns.Add("2ndAgingName", GetType(System.String))
            dtData.Columns.Add("3rdAging", GetType(System.Int32))
            dtData.Columns.Add("3rdAgingName", GetType(System.String))
            dtData.Columns.Add("4thAging", GetType(System.Int32))
            dtData.Columns.Add("4thAgingName", GetType(System.String))
            dtData.Columns.Add("5thAging", GetType(System.Int32))
            dtData.Columns.Add("5thAgingName", GetType(System.String))
            dtData.Columns.Add("6thAging", GetType(System.Int32))
            dtData.Columns.Add("6thAgingName", GetType(System.String))

            If IO.File.Exists(Application.StartupPath & "\TemplateAgingBalanceNew.xml") Then
                dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
            Else
                Dim dr As DataRow
                dr = dtData.NewRow
                dr(1) = "Default"
                dr(2) = 1
                dr(3) = 30
                dr(4) = "1_30"
                dr(5) = 60
                dr(6) = "30_60"
                dr(7) = 90
                dr(8) = "60_90"
                dr(9) = 180
                dr(10) = "90_180"
                dr(11) = 270
                dr(12) = "180_270"
                dr(13) = 360
                dr(14) = "360+"
                dtData.Rows.Add(dr)
                dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
            End If
            Me.cmbAging.ValueMember = "Id"
            Me.cmbAging.DisplayMember = "Format_Name"
            Me.cmbAging.DataSource = dtData
            FillCombos("Vendor")
            FillCombos("SubSub")
            FillCombos()
            IsOpenForm = True


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim dtData As New DataTable
            If Me.lslAccountList.ListItem.SelectedItems Is Nothing Then Exit Sub
            If Me.rbtVendor.Checked = True Then
                dtData = GetDataTable("SP_PurchaseInvoiceAging '" & Now.ToString("yyyy-M-d 00:00:00") & "'," & strAging & "," & str1stAging & "," & str2ndAging & "," & str3rdAging & "," & str4thAging & "," & str5thAging & "," & str6thAging & "")
            Else
                ' dtData = GetDataTable("SP_InvoiceAging '" & Now.ToString("yyyy-M-d 00:00:00") & "'," & strAging & "," & str1stAging & "," & str2ndAging & "," & str3rdAging & "," & str4thAging & "," & str5thAging & "," & str6thAging & "")
                dtData = GetDataTable("SP_InvoiceAging '" & Now.ToString("yyyy-M-d 00:00:00") & "'," & strAging & "," & str1stAging & "," & str2ndAging & "," & str3rdAging & "," & str4thAging & "," & str5thAging & "," & str6thAging & "")
            End If
            'dtData.Columns.Add("AmountDue", GetType(System.Double))
            dtData.Columns("AmountDue").Expression = "[1_30]+[30_60]+[60_90]+[90_180]+[180_270]+[360+]"
            dtData.Columns("Not Due").Expression = "IIF([Credit Days] > [Default Days],[Amount],0)"
            Dim dv As New DataView
            dtData.TableName = "tblAging"
            dv.Table = dtData

            dv.RowFilter = "([Not Due] <> 0  Or [1_30] <> 0 Or [30_60] <> 0 Or [60_90] <> 0 Or [90_180] <> 0 Or [180_270] <> 0 Or [360+] <> 0 ) "
            dv.RowFilter = "(([Not Due] + [1_30] + [30_60] + [60_90] + [90_180] + [180_270] + [360+] )> 0 ) " ''TFS4683 : This Filter Invoices having more receiving than invoice amount 
            If Me.lslAccountList.SelectedIDs.Length > 0 Then
                dv.RowFilter += "AND CustomerCode IN(" & Me.lslAccountList.SelectedIDs & ")"
            End If
            If Me.lstCompany.SelectedIDs.Length > 0 Then
                dv.RowFilter += "AND LocationId IN(" & Me.lstCompany.SelectedIDs & ")"
            End If
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                dv.RowFilter += "AND CostCenterId IN(" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            Me.grd.DataSource = dv.ToTable
            Me.grd.RetrieveStructure()
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns(1).Visible = False
            Me.grd.RootTable.Columns("LocationId").Visible = False
            Me.grd.RootTable.Columns("CostCenterId").Visible = False
            Me.grd.RootTable.Columns("Current_Amount").Visible = False
            Me.grd.RootTable.Columns("SalesNo").Caption = "No"
            Me.grd.RootTable.Columns("SalesDate").Caption = "Date"
            Me.grd.RootTable.Columns("SalesDate").FormatString = "dd/MMM/yyyy"
            '  Me.grd.RootTable.Columns("Less_20Days").Caption = "Due"

            Me.grd.RootTable.Columns("TotalReceivedAmount").Caption = "Received Amount"

            Me.grd.RootTable.Columns("1_30").Caption = str1stAgingName '"20"
            Me.grd.RootTable.Columns("30_60").Caption = str2ndAgingName '"21-50"
            Me.grd.RootTable.Columns("60_90").Caption = str3rdAgingName '"Over 51 days"
            Me.grd.RootTable.Columns("90_180").Caption = str4thAgingName '"20"
            Me.grd.RootTable.Columns("180_270").Caption = str5thAgingName '"21-50"
            Me.grd.RootTable.Columns("360+").Caption = str6thAgingName

            Me.grd.RootTable.Columns("1_30").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("30_60").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("60_90").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("90_180").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("180_270").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("360+").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Current_Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Not Due").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Default Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Credit Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Current_Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Current_Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("1_30").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("30_60").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("1_30").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("30_60").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("60_90").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("90_180").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("180_270").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("360+").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("1_30").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("30_60").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("60_90").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("90_180").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("180_270").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("360+").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("TotalReceivedAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("TotalReceivedAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("TotalReceivedAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("AmountDue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("AmountDue").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("AmountDue").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Not Due").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Not Due").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Current_Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Current_Amount").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("1_30").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("30_60").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("60_90").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("90_180").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("180_270").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("360+").FormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("1_30").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("30_60").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("60_90").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("90_180").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("180_270").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("360+").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("Not Due").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Not Due").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns("TotalReceivedAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("TotalReceivedAmount").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns("AmountDue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("AmountDue").TotalFormatString = "N" & TotalAmountRounding

            Dim group As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Account Description"))
            Me.grd.RootTable.Groups.Add(group)
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
            Me.Cursor = Cursors.Default
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = "Daily Working Report"
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs1 As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs1)
                fs1.Close()
                fs1.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Invoice Aging Report" & Chr(10) & "UpTo: " & Now.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddAging_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAging.Click
        Try
            If frmAgingBalancesTemplateNew.ShowDialog = Windows.Forms.DialogResult.Yes Then
                dtData = New DataTable
                dtData.TableName = "tblAgingTemplate"
                dtData.Columns.Add("Id", GetType(System.Int32))
                dtData.Columns("Id").AutoIncrement = True
                dtData.Columns("Id").AutoIncrementSeed = 1
                dtData.Columns("Id").AutoIncrementStep = 1
                dtData.Columns.Add("Format_Name", GetType(System.String))
                dtData.Columns.Add("Aging", GetType(System.Int32))
                dtData.Columns.Add("1stAging", GetType(System.Int32))
                dtData.Columns.Add("1stAgingName", GetType(System.String))
                dtData.Columns.Add("2ndAging", GetType(System.Int32))
                dtData.Columns.Add("2ndAgingName", GetType(System.String))
                dtData.Columns.Add("3rdAging", GetType(System.Int32))
                dtData.Columns.Add("3rdAgingName", GetType(System.String))
                dtData.Columns.Add("4thAging", GetType(System.Int32))
                dtData.Columns.Add("4thAgingName", GetType(System.String))
                dtData.Columns.Add("5thAging", GetType(System.Int32))
                dtData.Columns.Add("5thAgingName", GetType(System.String))
                dtData.Columns.Add("6thAging", GetType(System.Int32))
                dtData.Columns.Add("6thAgingName", GetType(System.String))
                If IO.File.Exists(Application.StartupPath & "\TemplateAgingBalanceNew.xml") Then
                    dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                Else
                    Dim dr As DataRow
                    dr = dtData.NewRow
                    dr(1) = "Default"
                    dr(2) = 1
                    dr(3) = 30
                    dr(4) = "1_30"
                    dr(5) = 60
                    dr(6) = "30_60"
                    dr(7) = 90
                    dr(8) = "60_90"
                    dr(9) = 180
                    dr(10) = "90_180"
                    dr(11) = 270
                    dr(12) = "180_270"
                    dr(13) = 360
                    dr(14) = "360+"
                    dtData.Rows.Add(dr)
                    dtData.WriteXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                    dtData.ReadXml(Application.StartupPath & "\TemplateAgingBalanceNew.xml")
                End If
                Me.cmbAging.ValueMember = "Id"
                Me.cmbAging.DisplayMember = "Format_Name"
                Me.cmbAging.DataSource = dtData
                CtrlGrdBar1_Load(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbAging_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbAging.SelectedIndexChanged
        Try
            If IsOpenForm = False Then Exit Sub
            strAging = Val(CType(Me.cmbAging.SelectedItem, DataRowView).Item("Aging").ToString)
            str1stAging = Val(CType(Me.cmbAging.SelectedItem, DataRowView).Item("1stAging").ToString)
            str2ndAging = Val(CType(Me.cmbAging.SelectedItem, DataRowView).Item("2ndAging").ToString)
            str3rdAging = Val(CType(Me.cmbAging.SelectedItem, DataRowView).Item("3rdAging").ToString)

            str1stAgingName = CType(Me.cmbAging.SelectedItem, DataRowView).Item("1stAgingName").ToString
            str2ndAgingName = CType(Me.cmbAging.SelectedItem, DataRowView).Item("2ndAgingName").ToString
            str3rdAgingName = CType(Me.cmbAging.SelectedItem, DataRowView).Item("3rdAgingName").ToString

            'btnRefresh_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            FillCombos("Vendor")
            FillCombos("SubSub")
            FillCombos()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetAll()
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim dtData As New DataTable
            If Me.lslAccountList.ListItem.SelectedItems Is Nothing Then Exit Sub
            If Me.rbtVendor.Checked = True Then
                dtData = GetDataTable("SP_PurchaseInvoiceAging '" & Now.ToString("yyyy-M-d 00:00:00") & "'," & strAging & "," & str1stAging & "," & str2ndAging & "")
            Else
                dtData = GetDataTable("SP_InvoiceAging '" & Now.ToString("yyyy-M-d 00:00:00") & "'," & strAging & "," & str1stAging & "," & str2ndAging & "")
            End If
            'dtData.Columns.Add("AmountDue", GetType(System.Double))
            dtData.Columns("AmountDue").Expression = "[20Days]+[21_50Days]+[51Days]"
            dtData.Columns("Not Due").Expression = "IIF([Credit Days] > [Default Days],[Amount],0)"
            Dim dv As New DataView
            dtData.TableName = "tblAging"
            dv.Table = dtData

            'dv.RowFilter = "([Not Due] <> 0 Or [Less_20Days] <> 0 Or [20Days] <> 0 Or [21_50Days] <> 0 Or [51Days] <> 0) "
            If Me.lslAccountList.SelectedIDs.Length > 0 Then
                dv.RowFilter += " CustomerCode IN(" & Me.lslAccountList.SelectedIDs & ")"
            End If
            Me.grd.DataSource = dv.ToTable
            Me.grd.RetrieveStructure()
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns(1).Visible = False

            Me.grd.RootTable.Columns("SalesNo").Caption = "No"
            Me.grd.RootTable.Columns("SalesDate").Caption = "Date"
            Me.grd.RootTable.Columns("SalesDate").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("Less_20Days").Caption = "Due"

            Me.grd.RootTable.Columns("TotalReceivedAmount").Caption = "Received Amount"

            Me.grd.RootTable.Columns("20Days").Caption = str1stAgingName '"20"
            Me.grd.RootTable.Columns("21_50Days").Caption = str2ndAgingName '"21-50"
            Me.grd.RootTable.Columns("51Days").Caption = str3rdAgingName '"Over 51 days"

            Me.grd.RootTable.Columns("20Days").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("21_50Days").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("51Days").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Less_20Days").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Not Due").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Default Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Credit Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Less_20Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Less_20Days").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("20Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("21_50Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("20Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("21_50Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("51Days").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("20Days").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("21_50Days").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("51Days").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("TotalReceivedAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("TotalReceivedAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("TotalReceivedAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("AmountDue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("AmountDue").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("AmountDue").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Not Due").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Not Due").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Less_20Days").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Less_20Days").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("20Days").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("21_50Days").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("51Days").FormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("20Days").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("21_50Days").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("51Days").TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns("Not Due").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Not Due").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns("TotalReceivedAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("TotalReceivedAmount").TotalFormatString = "N" & TotalAmountRounding

            Me.grd.RootTable.Columns("AmountDue").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("AmountDue").TotalFormatString = "N" & TotalAmountRounding

            Dim group As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns("Account Description"))
            Me.grd.RootTable.Groups.Add(group)
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
            Me.Cursor = Cursors.Default
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnGetAll_Click(sender As Object, e As EventArgs) Handles btnGetAll.Click
        Try
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pnlHeader_DoubleClick(sender As Object, e As EventArgs) Handles pnlHeader.DoubleClick
        'frmSaveMsg.ShowDialog()
    End Sub


    Private Sub lstSubSubAccount_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstSubSubAccount.SelectedIndexChaned
        Try
            If IsOpenForm = True Then
                Dim strVendorCustomer As String = getConfigValueByType("Show Customer On Purchase").ToString
                Dim strCustomerVendor As String = getConfigValueByType("Show Vendor On Sales").ToString
                Dim Str = ""
                If rbtVendor.Checked = True Then
                    If strVendorCustomer = "True" Then
                        Str = "Select coa_detail_id, detail_title + '~' + detail_code as Customer_Name , account_type From vwCOADetail where account_type IN('Vendor','Customer') AND detail_title <> '' "
                    Else
                        Str = "Select coa_detail_id, detail_title + '~' + detail_code as Customer_Name , account_type From vwCOADetail where account_type IN('Vendor') AND detail_title <> '' "
                    End If
                ElseIf rbtCustomer.Checked = True Then
                    If strCustomerVendor = "True" Then
                        Str = "Select coa_detail_id, detail_title + '~' + detail_code as Customer_Name , account_type From vwCOADetail where account_type IN('Customer','Vendor') AND detail_title <> '' "
                    Else
                        Str = "Select coa_detail_id, detail_title + '~' + detail_code as Customer_Name , account_type From vwCOADetail where account_type='Customer' AND detail_title <> '' "
                    End If
                End If
                If Me.lstSubSubAccount.SelectedIDs.Length > 0 Then
                    Str += " And main_sub_sub_id in (" & Me.lstSubSubAccount.SelectedIDs & ") "
                End If
                Str += " ORDER BY 2 ASC "
                dtCustomer = GetDataTable(Str)
                dtCustomer.TableName = "Customer"
                dvCustomer.Table = dtCustomer
                FillListBox(Me.lslAccountList.ListItem, Str)
                Me.lslAccountList.DeSelect()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lstCostCenterHead_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstCostCenterHead.SelectedIndexChaned
        Try
            If IsOpenForm = True Then
                If Me.lstCostCenterHead.SelectedIDs.Length > 0 Then
                    FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 AND CostCenterGroup IN (" & Me.lstCostCenterHead.SelectedItems & ") Else SELECT  CostCenterID,Name FROM tblDefCostCenter WHERE Active=1 AND CostCenterGroup IN (" & Me.lstCostCenterHead.SelectedItems & ") ")
                    Me.lstCostCenter.DeSelect()
                Else
                    FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name FROM tblDefCostCenter WHERE Active=1 ")
                    Me.lstCostCenter.DeSelect()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSearchCustomer_TextChanged(sender As Object, e As EventArgs) Handles txtSearchCustomer.TextChanged
        Try
            If txtSearchCustomer.Text <> "" Then
                dvCustomer.RowFilter = "Customer_Name LIKE '%" & Me.txtSearchCustomer.Text & "%' "
            End If
            Me.lslAccountList.ListItem.DataSource = dvCustomer
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        'Rafay:task start : to print the account statement


        Try
            AddRptParam("@ToDate", Date.Now.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@Aging", Me.cmbAging.SelectedItem.Item("strAging").ToString)
            AddRptParam("@1stAging", Me.cmbAging.SelectedItem.Item("str1stAging").ToString)
            AddRptParam("@2ndAging", Me.cmbAging.SelectedItem.Item("str2ndAging").ToString)
            AddRptParam("@3rdAging", Me.cmbAging.SelectedItem.Item("str3rdAging").ToString)
            AddRptParam("@4thAging", Me.cmbAging.SelectedItem.Item("str4thAging").ToString)
            AddRptParam("@5thAging", Me.cmbAging.SelectedItem.Item("str5thAging").ToString)
            AddRptParam("@6thAging", Me.cmbAging.SelectedItem.Item("str6thAging").ToString)
            ' AddRptParam("@AccountId", Me.lslAccountList.Value)
            'AddRptParam("@Aging", Me.cmbAging.SelectedItem.Item("Aging").Value.ToString)
            'AddRptParam("@1stAging", Me.cmbAging.SelectedItem.Item("1stAgingName").Value.ToString)
            'AddRptParam("@2ndAging", Me.cmbAging.SelectedItem.Item("2ndAgingName").Value.ToString)
            'AddRptParam("@3rdAging", Me.cmbAging.SelectedItem.Item("3rdAgingName").Value.ToString)
            'AddRptParam("@4thAging", Me.cmbAging.SelectedItem.Item("4thAgingName").Value.ToString)
            'AddRptParam("@5thAging", Me.cmbAging.SelectedItem.Item("5thAgingName").Value.ToString)
            'AddRptParam("@6thAging", Me.cmbAging.SelectedItem.Item("6thAgingName").Value.ToString)
            ShowReport("rptInvoiceAging")

            'AddRptParam("@ToDate", Date.Now.ToString("yyyy-MM-dd 23:59:59"))
            'AddRptParam("@Aging", Me.cmbAging.SelectedItem.Item("Aging").ToString)
            'AddRptParam("@1stAging", Me.cmbAging.SelectedItem.Item("1stAgingName").ToString)
            'AddRptParam("@2ndAging", Me.cmbAging.SelectedItem.Item("2ndAgingName").ToString)
            'AddRptParam("@3rdAging", Me.cmbAging.SelectedItem.Item("3rdAgingName").ToString)
            'AddRptParam("@4thAging", Me.cmbAging.SelectedItem.Item("4thAgingName").ToString)
            'AddRptParam("@5thAging", Me.cmbAging.SelectedItem.Item("5thAgingName").ToString)
            'AddRptParam("@6thAging", Me.cmbAging.SelectedItem.Item("6thAgingName").ToString)


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
        'Rafay:Task End:
    End Sub

    '    Private Sub ToolStripSplitButton2_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripSplitButton2.ButtonClick
    '        Me.Cursor = Cursors.WaitCursor
    '        Try
    '            If Me.grd.RowCount = 0 Then Exit Sub

    '            AddRptParam("@ToDate", Date.Now.ToString("yyyy-MM-dd 23:59:59"))
    '            AddRptParam("@Aging", Me.grd.CurrentRow.Cells("Aging").Value)
    '            AddRptParam("@1stAging", Me.grd.CurrentRow.Cells("1stAging").Value)
    '            AddRptParam("@2ndAging", Me.grd.CurrentRow.Cells("2ndtAging").Value)
    '            AddRptParam("@3rdAging", Me.grd.CurrentRow.Cells("3rdAging").Value)
    '            AddRptParam("@4thAging", Me.grd.CurrentRow.Cells("4thAging").Value)
    '            AddRptParam("@5thAging", Me.grd.CurrentRow.Cells("5thAging").Value)
    '            AddRptParam("@6thAging", Me.grd.CurrentRow.Cells("6thAging").Value)

    '            If rbtCustomer.Checked = True Then
    '                ShowReport("rptInvoiceAging")
    '            Else
    '                ShowReport("rptInvoiceAging")
    '            End If

    '        Catch ex As Exception
    '            Throw ex
    '        Finally
    '            Me.Cursor = Cursors.Default
    '        End Try
    '    End Sub

    '    Private Sub lslAccountList_Load(sender As Object, e As EventArgs) Handles lslAccountList.Load

    '    End Sub
End Class
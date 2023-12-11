Public Class frmGrdPurchaseSummary

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

    Sub FillCombo(Optional ByVal Condition As String = "")
        Try

            If Condition = "Customer" Then
                Dim Str As String
                ''Start TFS2124
                Str = "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                       "tblListTerritory.TerritoryName as Territory , '' as ExpiryDate, 0 as [Discount] ,0 as [Other Expense], 0 as Fuel , 0 as Limit, dbo.vwCOADetail.account_type as Type, 0 as typeid, tblVendor.Email,tblVendor.Phone " & _
                                                       "FROM         tblVendor LEFT OUTER JOIN " & _
                                                       "tblListTerritory ON tblVendor.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                       "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                       "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                       "vwCOADetail ON tblVendor.AccountId = vwCOADetail.coa_detail_id " & _
                                                       "WHERE  vwCOADetail.coa_detail_id is not  null"
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    Str += " AND vwCOADetail.Account_Type in ('Vendor','Customer') "
                Else
                    Str += " AND  vwCOADetail.Account_Type='Vendor' "
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
            ElseIf Condition = "Company" Then
                FillDropDown(Me.ComboBox1, "Select CompanyId, CompanyName From CompanyDefTable")
            ElseIf Condition = "CostCentre" Then
                FillDropDown(Me.cmbCostCentre, "select * from tbldefCostCenter order by sortorder , name", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmGrdPurchaseSummary_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            ToolStripButton1_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmGrdSalesSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            FillCombo("Customer")
            FillCombo("Company")
            FillCombo("CostCentre")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFromDate.Value = Date.Today
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFromDate.Value = Date.Today.AddDays(-1)
                Me.dtpToDate.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))

                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpToDate.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try

            Dim strQry As String = "SELECT CASE WHEN V.Type Is Null Then 'Credit' ELSE 'Cash' END [Type], dbo.ReceivingMasterTable.ReceivingNo, dbo.ReceivingMasterTable.ReceivingDate, dbo.vwCOADetail.detail_code as Code, dbo.vwCOADetail.detail_title as Customer, dbo.ReceivingMasterTable.ReceivingQty as Qty, " _
                               & "  dbo.ReceivingMasterTable.ReceivingAmount as Amount FROM dbo.ReceivingMasterTable INNER JOIN dbo.vwCOADetail ON dbo.ReceivingMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id  LEFT OUTER JOIN (Select DISTINCT V.Voucher_Code , CASE WHEN V.Voucher_Code <> v.Voucher_No Then 'Cash' ELSE 'Credit' END Type  From tblVoucher V WHERE V.Voucher_No <> V.Voucher_Code) V on V.Voucher_Code = ReceivingMasterTable.ReceivingNo WHERE (ReceivingMasterTable.ReceivingNo like 'Pur%') AND (Convert(Varchar, dbo.ReceivingMasterTable.ReceivingDate, 102) BETWEEN Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102))"
            If Me.ComboBox1.SelectedIndex > 0 Then
                strQry += " AND ReceivingMasterTable.LocationId=" & Me.ComboBox1.SelectedValue & " "
            End If

            If Me.cmbAccount.ActiveRow.Cells(0).Value > 0 Then
                strQry += " AND ReceivingMasterTable.VendorId=" & Me.cmbAccount.Value & ""
            End If
            If Me.cmbCostCentre.SelectedIndex > 0 Then
                strQry += " AND ReceivingMasterTable.CostCenterId=" & Me.cmbCostCentre.SelectedValue & ""
            End If

            If Me.rbtCash.Checked = True Then
                strQry += " AND dbo.ReceivingMasterTable.ReceivingNo In (Select a.Voucher_Code From tblVoucher  a  inner join tblVoucherDetail b ON a.Voucher_Id= b.voucher_id INNER JOIN vwCOADetail vw on vw.coa_detail_id = b.coa_detail_id WHERE (Convert(Varchar, a.Voucher_Date, 102) BETWEEN Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND a.Voucher_Code <> a.Voucher_No " & IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, " AND b.coa_detail_id=" & Me.cmbAccount.Value & "", "") & " " & IIf(Me.ComboBox1.SelectedIndex > 0, " AND vw.CompanyId=" & Me.ComboBox1.SelectedValue & "", "") & ")"
            End If

            If Me.rbtCredit.Checked = True Then
                strQry += " AND dbo.ReceivingMasterTable.ReceivingNo not In (Select a.Voucher_Code From tblVoucher  a  inner join tblVoucherDetail b ON a.Voucher_Id= b.voucher_id INNER JOIN vwCOADetail vw on vw.coa_detail_id = b.coa_detail_id WHERE (Convert(Varchar, a.Voucher_Date, 102) BETWEEN Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102)) AND a.Voucher_Code <> a.Voucher_No " & IIf(Me.cmbAccount.ActiveRow.Cells(0).Value > 0, " AND b.coa_detail_id=" & Me.cmbAccount.Value & "", "") & " " & IIf(Me.ComboBox1.SelectedIndex > 0, " AND vw.CompanyId=" & Me.ComboBox1.SelectedValue & "", "") & ")"
            End If

            Dim dt As New DataTable
            dt = GetDataTable(strQry)

            Me.grd.DataSource = Nothing
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grd.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum


            Me.grd.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("ReceivingDate").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("ReceivingNo").Caption = "Inv No"
            Me.grd.RootTable.Columns("ReceivingDate").Caption = "Inv Date"

            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

            Me.grd.AutoSizeColumns()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try

            Dim id As Integer = 0D

            id = Me.cmbAccount.ActiveRow.Cells(0).Value
            FillCombo("Customer")
            Me.cmbAccount.ActiveRow.Cells(0).Value = id

            id = Me.ComboBox1.SelectedValue
            FillCombo("Company")
            Me.ComboBox1.SelectedValue = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Purchase Invoice Summary" & Chr(10) & "From Date: " & dtpFromDate.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpToDate.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
End Class
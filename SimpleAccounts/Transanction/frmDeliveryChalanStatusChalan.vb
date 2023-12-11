''06-Sep-2014 Task:2829 Imran Ali Selected Customer Delivery Chalan on Sales Invoice (Converters)
Public Class frmDeliveryChalanStatusChalan
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
    Dim flgCompanyRights As Boolean = False
    Public _DeliveryChalanId As Integer = 0I
    Public _CustomerId As Integer = 0I

    Private Sub frmDeliveryChalanStatusChalan_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13
        If e.KeyCode = Keys.S AndAlso e.Control Then
            btnSave_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub frmDeliveryChalanStatusChalan_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13 
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            Dim str As String = String.Empty
            If getConfigValueByType("Show Vendor On Sales") = "True" Then
                str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                    "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone " & _
                                    "FROM  tblCustomer LEFT OUTER JOIN " & _
                                    "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                    "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                    "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                    "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                    "WHERE (vwCOADetail.account_type in( 'Customer','Vendor' )) and  vwCOADetail.coa_detail_id is not  null "
                If flgCompanyRights = True Then
                    str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                End If
            Else
                str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                                   "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone " & _
                                                   "FROM         tblCustomer LEFT OUTER JOIN " & _
                                                   "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                                   "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                                   "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                                   "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                                   "WHERE (vwCOADetail.account_type='Customer') and  vwCOADetail.coa_detail_id is not  null "
                If flgCompanyRights = True Then
                    str += " AND vwCOADetail.CompanyId=" & MyCompanyId
                End If
            End If
            str += " AND vwCOADetail.Active in(0,1,NULL)"
            str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title "
            FillUltraDropDown(cmbVendor, str)
            If cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                Me.cmbVendor.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
            End If
            Me.cmbVendor.Rows(0).Activate()
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            'FillDropDown(Me.cmbDeliveryChalan, "Select DeliveryId, DeliveryNo From DeliveryChalanMasterTable WHERE Status='Open' OR Status is null AND CustomerCode=" & Me.cmbVendor.Value & "")
            ''06-Sep-2014 Task:2829 Imran Ali Selected Customer Delivery Chalan on Sales Invoice (Converters)
            FillDropDown(Me.cmbDeliveryChalan, "Select DeliveryChalanMasterTable.DeliveryId, DeliveryChalanMasterTable.DeliveryNo,DeliveryChalanMasterTable.BiltyNo,DeliveryChalanMasterTable.Remarks, IsNull(DeliveryChalanMasterTable.EmployeeCode,0) as EmployeeCode, IsNull(DeliveryChalanMasterTable.LocationId,0) as LocationId, IsNull(DeliveryChalanMasterTable.CostCenterId,0) as Project, SO.PONo, SO.PO_Date, Other_Company From DeliveryChalanMasterTable LEFT OUTER JOIN SalesOrderMasterTable SO On So.SalesOrderID = DeliveryChalanMasterTable.POId  WHERE (DeliveryChalanMasterTable.Status='Open' OR DeliveryChalanMasterTable.Status is null) AND CustomerCode=" & Me.cmbVendor.Value & " AND IsNull(Post,0)=1 Order BY DeliveryID DESC ")
            'End Task:2829
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub frmDeliveryChalanStatusChalan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            _DeliveryChalanId = Me.cmbDeliveryChalan.SelectedValue
            _CustomerId = Me.cmbVendor.Value
            frmSales.cmbCompany.SelectedValue = Val(CType(Me.cmbDeliveryChalan.SelectedItem, DataRowView).Row.Item("LocationId").ToString)
            'frmSales.RefreshControls()
            frmSales.DeliveryChalanId = _DeliveryChalanId
            'frmSales.cmbVendor.Value = _CustomerId
            'Task:2829 User will retrive data load button
            frmSales.txtRemarks.Text = CType(Me.cmbDeliveryChalan.SelectedItem, DataRowView).Row.Item("Remarks").ToString
            frmSales.uitxtBiltyNo.Text = CType(Me.cmbDeliveryChalan.SelectedItem, DataRowView).Row.Item("BiltyNo").ToString
            frmSales.cmbProject.SelectedValue = CType(Me.cmbDeliveryChalan.SelectedItem, DataRowView).Row.Item("Project").ToString
            'End Task:2829
            frmSales.cmbOtherCompany.Text = CType(Me.cmbDeliveryChalan.SelectedItem, DataRowView).Row.Item("Other_Company").ToString
            'frmSales.txtChalanNo.Text = CType(Me.cmbDeliveryChalan.SelectedItem, DataRowView).Row.Item("DeliveryNo").ToString
            'frmSales.DisplayDetail(-1)
            frmSales.DisplayDeliveryChalanDetail(_DeliveryChalanId)
            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbVendor_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendor.ValueChanged
        Try
            If Me.cmbVendor.ActiveRow Is Nothing Then Exit Sub
            FillDropDown(Me.cmbDeliveryChalan, "Select DeliveryChalanMasterTable.DeliveryId, DeliveryChalanMasterTable.DeliveryNo,DeliveryChalanMasterTable.BiltyNo,DeliveryChalanMasterTable.Remarks, IsNull(DeliveryChalanMasterTable.EmployeeCode,0) as EmployeeCode, IsNull(DeliveryChalanMasterTable.LocationId,0) as LocationId, IsNull(DeliveryChalanMasterTable.CostCenterId,0) as Project, SO.PONo, SO.PO_Date, Other_Company From DeliveryChalanMasterTable LEFT OUTER JOIN SalesOrderMasterTable SO On So.SalesOrderID = DeliveryChalanMasterTable.POId  WHERE (DeliveryChalanMasterTable.Status='Open' OR DeliveryChalanMasterTable.Status is null) AND CustomerCode=" & Me.cmbVendor.Value & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


End Class
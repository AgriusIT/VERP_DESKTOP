Imports System.Windows.Forms
Imports System.Data.OleDb
Imports SBModel
Partial Class SummaryofSalesInvoicesReturn
    Public flgCompanyRights As Boolean = False
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            GetCrystalReportRights()
            Me.CallShowReport()
        Catch Ex As Exception
            ShowErrorMessage(Ex.Message)
        End Try
    End Sub
    Sub CallShowReport(Optional ByVal Print As Boolean = False)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim fromDate As String = Me.DateTimePicker1.Value.Year & "," & Me.DateTimePicker1.Value.Month & "," & Me.DateTimePicker1.Value.Day & ",0,0,0"
            Dim ToDate As String = Me.DateTimePicker2.Value.Year & "," & Me.DateTimePicker2.Value.Month & "," & Me.DateTimePicker2.Value.Day & ",23,59,59"
            'If cmbCustomer.Visible = True Then
            '    If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
            '        ShowReport("SaleReturnSummary", "{SaleReturnSummary;1.SalesReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SaleReturnSummary;1.CustomerCode}=" & Me.cmbCustomer.Value & " " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SaleReturnSummary;1.CustomerTypeID}=" & Me.cmbCustomerType.SelectedValue & "", "") & "", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, Print)
            '    Else
            '        ShowReport("SaleReturnSummary", "{SaleReturnSummary;1.SalesReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")  " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SaleReturnSummary;1.CustomerTypeID}=" & Me.cmbCustomerType.SelectedValue & "", "") & "", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, Print)
            '    End If
            'Else
            '    ShowReport("SaleReturnSummary", "{SaleReturnSummary;1.SalesReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")  " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SaleReturnSummary;1.CustomerTypeID}=" & Me.cmbCustomerType.SelectedValue & "", "") & "", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, Print)
            'End If
            If cmbCustomer.Visible = True Then
                If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                    ShowReport("SaleReturnSummary", "{SaleReturnSummary;1.SalesReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ") AND {SaleReturnSummary;1.CustomerCode}=" & Me.cmbCustomer.Value & " " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SaleReturnSummary;1.CustomerTypeID}=" & Me.cmbCustomerType.SelectedValue & "", "") & IIf(Me.cmbState.SelectedIndex > 0, " AND {SaleReturnSummary;1.StateID} =" & Me.cmbState.SelectedValue & "", "") & IIf(Me.cmbRegion.SelectedIndex > 0, " AND {SaleReturnSummary;1.RegionID} =" & Me.cmbRegion.SelectedValue & "", "") & IIf(Me.cmbZone.SelectedIndex > 0, " AND {SaleReturnSummary;1.ZoneID} =" & Me.cmbZone.SelectedValue & "", "") & IIf(Me.cmbBelt.SelectedIndex > 0, " AND {SaleReturnSummary;1.BeltID} =" & Me.cmbBelt.SelectedValue & "", "") & IIf(Me.cmbCity.SelectedIndex > 0, " AND {SaleReturnSummary;1.CityID} =" & Me.cmbCity.SelectedValue & "", "") & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND {SaleReturnSummary;1.CostCenterID} =" & Me.cmbCostCenter.SelectedValue & "", "") & "", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, Print)
                Else
                    ShowReport("SaleReturnSummary", "{SaleReturnSummary;1.SalesReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")  " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SaleReturnSummary;1.CustomerTypeID}=" & Me.cmbCustomerType.SelectedValue & "", "") & IIf(Me.cmbState.SelectedIndex > 0, " AND {SaleReturnSummary;1.StateID} =" & Me.cmbState.SelectedValue & "", "") & IIf(Me.cmbRegion.SelectedIndex > 0, " AND {SaleReturnSummary;1.RegionID} =" & Me.cmbRegion.SelectedValue & "", "") & IIf(Me.cmbZone.SelectedIndex > 0, " AND {SaleReturnSummary;1.ZoneID} =" & Me.cmbZone.SelectedValue & "", "") & IIf(Me.cmbBelt.SelectedIndex > 0, " AND {SaleReturnSummary;1.BeltID} =" & Me.cmbBelt.SelectedValue & "", "") & IIf(Me.cmbCity.SelectedIndex > 0, " AND {SaleReturnSummary;1.CityID} =" & Me.cmbCity.SelectedValue & "", "") & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND {SaleReturnSummary;1.CostCenterID} =" & Me.cmbCostCenter.SelectedValue & "", "") & "", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, Print)
                End If
            Else
                ShowReport("SaleReturnSummary", "{SaleReturnSummary;1.SalesReturnDate} in DateTime (" & fromDate & ") to DateTime (" & ToDate & ")  " & IIf(Me.cmbCustomerType.SelectedIndex > 0, " AND {SaleReturnSummary;1.CustomerTypeID}=" & Me.cmbCustomerType.SelectedValue & "", "") & IIf(Me.cmbState.SelectedIndex > 0, " AND {SaleReturnSummary;1.StateID} =" & Me.cmbState.SelectedValue & "", "") & IIf(Me.cmbRegion.SelectedIndex > 0, " AND {SaleReturnSummary;1.RegionID} =" & Me.cmbRegion.SelectedValue & "", "") & IIf(Me.cmbZone.SelectedIndex > 0, " AND {SaleReturnSummary;1.ZoneID} =" & Me.cmbZone.SelectedValue & "", "") & IIf(Me.cmbBelt.SelectedIndex > 0, " AND {SaleReturnSummary;1.BeltID} =" & Me.cmbBelt.SelectedValue & "", "") & IIf(Me.cmbCity.SelectedIndex > 0, " AND {SaleReturnSummary;1.CityID} =" & Me.cmbCity.SelectedValue & "", "") & IIf(Me.cmbCostCenter.SelectedIndex > 0, " AND {SaleReturnSummary;1.CostCenterID} =" & Me.cmbCostCenter.SelectedValue & "", "") & "", Me.DateTimePicker1.Value, Me.DateTimePicker2.Value, Print)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        CallShowReport(True)
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.DateTimePicker1.Value = Date.Today
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-1)
            Me.DateTimePicker2.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.DateTimePicker1.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.DateTimePicker2.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.DateTimePicker1.Value = New Date(Date.Now.Year, 1, 1)
            Me.DateTimePicker2.Value = Date.Today
        End If
    End Sub
    Private Sub SummaryofSalesInvoicesReturn_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Dim Str As String
            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If
            Me.pnlCustomerType.Visible = True
            Me.pnlPeriod.Visible = True
            Me.pnlVendorCustomer.Visible = True
            Me.cmbPeriod.Text = "Current Month"
            Me.Text = "Summary Of Sales Return"
            FillDropDown(Me.cmbCustomerType, "Select Typeid, Name From TblDefCustomerType Where Active=1")
            FillDropDown(Me.cmbState, "Select StateId,StateName from tblListState Where Active=1")
            FillDropDown(Me.cmbRegion, "Select RegionId,RegionName from tblListRegion Where Active=1")
            FillDropDown(Me.cmbZone, "Select ZoneId,ZoneName from tblListZone Where Active=1")
            FillDropDown(Me.cmbBelt, "Select BeltId,BeltName from tblListBelt Where Active=1")
            FillDropDown(Me.cmbCity, "Select CityId,CityName from tblListCity Where Active=1")
            FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            ''Start TFS2124
            Str = "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], Sub_Sub_title as [Account Head] from vwCOADetail WHERE 1 = 1 " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " "
            If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                Str += " AND (Account_Type = 'Customer')  "
            Else
                Str += " AND (Account_Type in('Customer','Vendor'))  "
            End If
            Str += " ORDER BY 2 ASC"
            ''End TFS2124
            FillUltraDropDown(cmbCustomer, Str)
            Me.cmbCustomer.Rows(0).Activate()
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub cmbState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbState.SelectedIndexChanged
        Try
            FillDropDown(Me.cmbRegion, "Select RegionId,RegionName from tblListRegion Where Active=1 " & IIf(Me.cmbState.SelectedIndex > 0, "And StateId=" & Me.cmbState.SelectedValue & "", "") & "")
            FillDropDown(Me.cmbCity, "Select CityId,CityName from tblListCity Where Active=1" & IIf(Me.cmbState.SelectedIndex > 0, "And StateId=" & Me.cmbState.SelectedValue & "", "") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRegion.SelectedIndexChanged
        Try
            FillDropDown(Me.cmbZone, "Select ZoneId,ZoneName from tblListZone Where Active=1 " & IIf(Me.cmbRegion.SelectedIndex > 0, "And RegionId=" & Me.cmbRegion.SelectedValue & "", "") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbZone.SelectedIndexChanged
        Try
            FillDropDown(Me.cmbBelt, "Select BeltId,BeltName from tblListBelt Where Active=1" & IIf(Me.cmbZone.SelectedIndex > 0, "And ZoneId=" & Me.cmbZone.SelectedValue & "", "") & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
            Else
                Me.btnPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
End Class
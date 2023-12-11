'05-July-2017 TFS1054 : Ali Faisal : Add new form to save update and delete records through this form.
Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient
Public Class frmItemWiseProgressUpto
    Implements IGeneral
    ''' <summary>
    ''' Ali Faisal : set indexes of detail grid to use name of columns from enum instead of from query.
    ''' </summary>
    ''' <remarks>05-July-2017 TFS1054 : Ali Faisal</remarks>
    Enum grdenm
        VendorName
        CostCenter
        PONo
        PODate
        Item
        ContractNo
        ContractDate
        ContractValue
        AmountPaid
        Balance
        Progress
    End Enum

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If rdoCustomer.Checked = True Then
                Me.grd.RootTable.Columns("SODate").FormatString = str_DisplayDateFormat
            Else
                Me.grd.RootTable.Columns(grdenm.PODate).FormatString = str_DisplayDateFormat
            End If
            Me.grd.RootTable.Columns(grdenm.ContractDate).FormatString = str_DisplayDateFormat
            Me.grd.RootTable.Columns(grdenm.Progress).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.Progress).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.Progress).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(grdenm.ContractValue).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.ContractValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.ContractValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(grdenm.AmountPaid).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.AmountPaid).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.AmountPaid).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(grdenm.Balance).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.Balance).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.Balance).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Dim grdGroupBy As New Janus.Windows.GridEX.GridEXGroup(Me.grd.RootTable.Columns(grdenm.Item))
            grdGroupBy.GroupPrefix = String.Empty
            Me.grd.RootTable.Groups.Add(grdGroupBy)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    ''' <summary>
    ''' Ali Faisal : Add security rights for standard user to enable/disable buttons on right based. 
    ''' </summary>
    ''' <remarks>05-July-2017 TFS1054 : Ali Faisal</remarks>
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnShow.Enabled = True
                Me.btnNew.Enabled = True
                Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                Me.CtrlGrdBar2.mGridPrint.Enabled = True
                Me.CtrlGrdBar2.mGridExport.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnShow.Enabled = False
            Me.CtrlGrdBar2.mGridChooseFielder.Enabled = False
            Me.CtrlGrdBar2.mGridPrint.Enabled = False
            Me.CtrlGrdBar2.mGridExport.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar2.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar2.mGridExport.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Item" Then
                str = "SELECT ArticleId AS Id, ArticleCode AS Code, ArticleDescription AS Item, ArticleSizeName AS Size, ArticleColorName AS Combination, PackQty, ISNULL(SalePrice, 0) AS Price, ISNULL(PurchasePrice, 0) AS PurchasePrice, SortOrder, ArticleGroupName AS Dept, ArticleTypeName AS Type, ArticleGenderName AS Origin, ArticleLpoName AS Brand, ISNULL(Cost_Price, 0) AS [Cost Price], ISNULL(TradePrice, 0) AS [Trade Price] FROM ArticleDefView"
                str += " WHERE Active=1 "
                FillUltraDropDown(Me.cmbItem, str)
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    If Me.rbtnByCode.Checked = True Then
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                    Else
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                    End If
                End If
                UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ElseIf Condition = "CostCenter" Then
                str = "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active=1"
                FillDropDown(Me.cmbCostCenter, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : To show all saved records in history gidr.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>05-July-2017 TFS1054 : Ali Faisal</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable

            If rdoCustomer.Checked Then
                str = "SELECT vwCOADetail.detail_title AS CustomerName, tblDefCostCenter.Name AS CostCenter, SalesOrderMasterTable.SalesOrderNo AS SONo, SalesOrderMasterTable.SalesOrderDate AS SODate,  ArticleDefView.ArticleDescription AS Item, tblCustomerContractMaster.DocNo AS ContractNo, tblCustomerContractMaster.DocDate AS ContractDate, tblCustomerContractMaster.ContractValue, ISNULL(BillAmount.Amount, 0)  AS AmountPaid, ISNULL(BillAmount.Balance, 0) AS Balance, ISNULL(tblCustomerContractMaster.ContractValue, 0) - ISNULL(BillAmount.Amount, 0) AS RemainingValue "
                str += " FROM (SELECT tblVoucherDetail.coa_detail_id, SUM(tblVoucherDetail.debit_amount) AS Amount, SUM(tblVoucherDetail.debit_amount) - SUM(tblVoucherDetail.credit_amount) AS Balance FROM tblVoucherDetail LEFT OUTER JOIN tblDefCostCenter ON tblVoucherDetail.CostCenterID = tblDefCostCenter.CostCenterID "
                If Me.cmbCostCenter.SelectedValue > 0 Then
                    str += " WHERE tblDefCostCenter.CostCenterId = " & Me.cmbCostCenter.SelectedValue & " "
                End If
                str += " GROUP BY tblVoucherDetail.coa_detail_id) AS BillAmount RIGHT OUTER JOIN tblCustomerContractMaster LEFT OUTER JOIN SalesOrderMasterTable ON tblCustomerContractMaster.SOId = SalesOrderMasterTable.SalesOrderId LEFT OUTER JOIN tblDefCostCenter AS tblDefCostCenter ON SalesOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN vwCOADetail ON tblCustomerContractMaster.CustomerId = vwCOADetail.coa_detail_id ON BillAmount.coa_detail_id = tblCustomerContractMaster.CustomerId LEFT OUTER JOIN ArticleDefView ON tblCustomerContractMaster.ItemId = ArticleDefView.ArticleId "
                str += " WHERE tblCustomerContractMaster.DocDate Between '" & Me.dtpFromDate.Value.ToString("yyyy-M-dd 00:00:00") & "' And '" & Me.dtpToDate.Value.ToString("yyyy-M-dd 23:59:59") & "'"
                If Me.cmbItem.Value > 0 Then
                    str += " AND tblCustomerContractMaster.ItemId = " & Me.cmbItem.Value & ""
                End If
                If Me.cmbCostCenter.SelectedValue > 0 Then
                    str += " AND SalesOrderMasterTable.CostCenterId = " & Me.cmbCostCenter.SelectedValue & ""
                End If
                str += " GROUP BY tblCustomerContractMaster.DocNo, tblCustomerContractMaster.DocDate, tblCustomerContractMaster.ContractValue, ArticleDefView.ArticleDescription, SalesOrderMasterTable.SalesOrderNo, SalesOrderMasterTable.SalesOrderDate, tblDefCostCenter.Name, vwCOADetail.detail_title, BillAmount.Amount, BillAmount.Balance, tblCustomerContractMaster.ItemId"
            Else

                'str = "SELECT vwCOADetail.detail_title AS VendorName, tblDefCostCenter.Name AS CostCenter, PurchaseOrderMasterTable.PurchaseOrderNo AS PONo, PurchaseOrderMasterTable.PurchaseOrderDate AS PODate, ArticleDefView.ArticleDescription AS Item, tblVendorContractMaster.DocNo AS ContractNo, tblVendorContractMaster.DocDate AS ContractDate, tblVendorContractMaster.ContractValue, ISNULL(BillAmount.Amount, 0) AS AmountPaid, ISNULL(tblVendorContractMaster.ContractValue, 0) - ISNULL(BillAmount.Amount, 0) AS Balance, CONVERT(decimal(16, 4), SUM(tblProjectProgressApproval.BillAmount) / ISNULL(tblVendorContractMaster.ContractValue, 1) * 100) AS [ProgressUpto%] "
                'str += " FROM tblProjectProgressApproval INNER JOIN tblVendorContractMaster ON tblProjectProgressApproval.ContractId = tblVendorContractMaster.ContractId INNER JOIN ArticleDefView ON tblVendorContractMaster.ItemId = ArticleDefView.ArticleId INNER JOIN PurchaseOrderMasterTable ON tblVendorContractMaster.POId = PurchaseOrderMasterTable.PurchaseOrderId INNER JOIN tblTaskProgressMaster ON tblProjectProgressApproval.ProgressId = tblTaskProgressMaster.Id INNER JOIN vwCOADetail ON tblVendorContractMaster.VendorId = vwCOADetail.coa_detail_id LEFT OUTER JOIN (SELECT coa_detail_id, SUM(debit_amount) AS Amount FROM tblVoucherDetail "
                'If Me.cmbCostCenter.SelectedValue > 0 Then
                '    str += " WHERE CostCenterID = " & Me.cmbCostCenter.SelectedValue & " "
                'End If
                'str += " GROUP BY coa_detail_id) AS BillAmount ON BillAmount.coa_detail_id = tblVendorContractMaster.VendorId LEFT OUTER JOIN tblDefCostCenter ON PurchaseOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID "
                'str += " WHERE tblVendorContractMaster.ContractValue > 0 AND tblTaskProgressMaster.DocDate Between '" & Me.dtpFromDate.Value.ToString("yyyy-M-dd 00:00:00") & "' And '" & Me.dtpToDate.Value.ToString("yyyy-M-dd 23:59:59") & "'"
                'If Me.cmbItem.Value > 0 Then
                '    str += " AND tblTaskProgressMaster.ItemId = " & Me.cmbItem.Value & ""
                'End If
                'If Me.cmbCostCenter.SelectedValue > 0 Then
                '    str += " AND PurchaseOrderMasterTable.CostCenterId = " & Me.cmbCostCenter.SelectedValue & ""
                'End If
                'str += " GROUP BY tblVendorContractMaster.DocNo, tblVendorContractMaster.DocDate, tblVendorContractMaster.ContractValue, ArticleDefView.ArticleDescription, PurchaseOrderMasterTable.PurchaseOrderNo, PurchaseOrderMasterTable.PurchaseOrderDate, tblDefCostCenter.Name, vwCOADetail.detail_title, BillAmount.Amount"
                str = "SELECT vwCOADetail.detail_title AS VendorName, tblDefCostCenter.Name AS CostCenter, PurchaseOrderMasterTable.PurchaseOrderNo AS PONo, PurchaseOrderMasterTable.PurchaseOrderDate AS PODate, ArticleDefView.ArticleDescription AS Item, tblVendorContractMaster.DocNo AS ContractNo, tblVendorContractMaster.DocDate AS ContractDate, tblVendorContractMaster.ContractValue, ISNULL(BillAmount.Amount, 0) AS AmountPaid, ISNULL(BillAmount.Balance, 0) AS Balance, ISNULL(tblVendorContractMaster.ContractValue, 0) - ISNULL(BillAmount.Amount, 0) AS RemainingValue "
                str += " FROM tblVendorContractMaster LEFT OUTER JOIN ArticleDefView ON tblVendorContractMaster.ItemId = ArticleDefView.ArticleId LEFT OUTER JOIN PurchaseOrderMasterTable ON tblVendorContractMaster.POId = PurchaseOrderMasterTable.PurchaseOrderId LEFT OUTER JOIN vwCOADetail ON tblVendorContractMaster.VendorId = vwCOADetail.coa_detail_id LEFT OUTER JOIN (SELECT tblVoucherDetail.coa_detail_id, SUM(tblVoucherDetail.debit_amount) AS Amount, SUM(tblVoucherDetail.debit_amount) - SUM(tblVoucherDetail.credit_amount) AS Balance FROM tblVoucherDetail LEFT OUTER JOIN tblDefCostCenter ON tblVoucherDetail.CostCenterID = tblDefCostCenter.CostCenterID "
                If Me.cmbCostCenter.SelectedValue > 0 Then
                    str += " WHERE tblDefCostCenter.CostCenterId = " & Me.cmbCostCenter.SelectedValue & " "
                End If
                str += " GROUP BY tblVoucherDetail.coa_detail_id) AS BillAmount ON BillAmount.coa_detail_id = tblVendorContractMaster.VendorId LEFT OUTER JOIN tblDefCostCenter ON PurchaseOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID "
                str += " WHERE tblVendorContractMaster.DocDate Between '" & Me.dtpFromDate.Value.ToString("yyyy-M-dd 00:00:00") & "' And '" & Me.dtpToDate.Value.ToString("yyyy-M-dd 23:59:59") & "'"
                If Me.cmbItem.Value > 0 Then
                    str += " AND tblVendorContractMaster.ItemId = " & Me.cmbItem.Value & ""
                End If
                If Me.cmbCostCenter.SelectedValue > 0 Then
                    str += " AND PurchaseOrderMasterTable.CostCenterId = " & Me.cmbCostCenter.SelectedValue & ""
                End If
                str += " GROUP BY tblVendorContractMaster.DocNo, tblVendorContractMaster.DocDate, tblVendorContractMaster.ContractValue, ArticleDefView.ArticleDescription, PurchaseOrderMasterTable.PurchaseOrderNo, PurchaseOrderMasterTable.PurchaseOrderDate, tblDefCostCenter.Name, vwCOADetail.detail_title, BillAmount.Amount, BillAmount.Balance, tblVendorContractMaster.ItemId"
            End If
            dt = GetDataTable(Str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Reset controls to default values.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>05-July-2017 TFS1054 : Ali Faisal</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            FillCombos("Item")
            FillCombos("CostCenter")
            Me.cmbPeriod.Text = "Current Month"
            Me.cmbItem.Value = 0
            Me.cmbCostCenter.SelectedValue = 0
            Me.rdoVendor.Checked = True
            Me.rdoCustomer.Checked = False
            Me.grd.DataSource = (Nothing)
            CtrlGrdBar2_Load(Nothing, Nothing)
            ApplySecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmItemTaskProgress_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.btnNew_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Load form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1054 : Ali Faisal</remarks>
    Private Sub frmItemTaskProgress_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Reset controls on New button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1054 : Ali Faisal</remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            GetAllRecords()
            Me.UltraTabControl1.Tabs(1).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & " Contract Detail Report " & Chr(10) & "From Date " & Me.dtpFromDate.Value & " " & Chr(10) & "To Date " & Me.dtpToDate.Value & ""
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

    Private Sub rbtnByCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnByCode.CheckedChanged, rbtnByName.CheckedChanged, rdoVendor.CheckedChanged, rdoCustomer.CheckedChanged
        Try
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                If Me.rbtnByCode.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@CostCenterId", Me.cmbCostCenter.SelectedValue)
            AddRptParam("@ItemId", Me.cmbItem.Value)
            If (rdoCustomer.Checked) Then
                ShowReport("rptItemWiseContactDetialsCustomer")
            Else
                ShowReport("rptItemWiseContactDetialsVendors")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
'Ayesha Rehman : TFS2726 : Add report to show the GRN detail report
Imports System
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmInvoiceWiseProfitReport
    Implements IGeneral
    Dim _SearchDt As New DataTable
    Dim flgAvgRate As Boolean = False
    ''' <summary>
    ''' Ayesha Rehman : TFS2726 : Set indexing of Records grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        'SalesId
        'CostCenterId
        'CostCenter
        'SalesNo
        'SalesDate
        'CustomerId
        'Customer
        'SalesDetailId
        'ItemId
        'ItemCode
        'ItemName
        'Price
        'CostPrice
        'PurchasePrice
        'Profit

        CostCenter
        SalesNo
        SalesDate
        Customer
        ItemCode
        ItemName
        Price
        CostPrice
        PurchasePrice
        Profit
    End Enum
    ''' <summary>
    ''' Ayesha Rehman : TFS2726 : Apply grid settings to hide some columns and formating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            'Me.grdSaved.RootTable.Columns(grd.SalesId).Visible = False
            'Me.grdSaved.RootTable.Columns(grd.CostCenterId).Visible = False
            'Me.grdSaved.RootTable.Columns(grd.CustomerId).Visible = False
            'Me.grdSaved.RootTable.Columns(grd.SalesDetailId).Visible = False
            'Me.grdSaved.RootTable.Columns(grd.ItemId).Visible = False

            Me.grdSaved.RootTable.Columns(grd.SalesDate).FormatString = str_DisplayDateFormat

            Me.grdSaved.RootTable.Columns(grd.Price).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.Price).TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.Price).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.Price).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Price).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Near
            Me.grdSaved.RootTable.Columns(grd.Price).Caption = "Sales Price"

            Me.grdSaved.RootTable.Columns(grd.PurchasePrice).Caption = "Purchase Price"
            Me.grdSaved.RootTable.Columns(grd.PurchasePrice).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.PurchasePrice).TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.PurchasePrice).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.PurchasePrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.PurchasePrice).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Near
            Me.grdSaved.RootTable.Columns(grd.CostPrice).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.CostPrice).TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.CostPrice).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.CostPrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.CostPrice).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Near
            Me.grdSaved.RootTable.Columns(grd.Profit).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.Profit).TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.Profit).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.Profit).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Profit).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Near
            If flgAvgRate = True Then
                Me.grdSaved.RootTable.Columns(grd.PurchasePrice).Visible = False
            Else
                Me.grdSaved.RootTable.Columns(grd.CostPrice).Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : TFS2726 : Apply security to show specific controls to standard users
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                Me.btnPrintPreview.Enabled = True
                Me.btnShow.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnPrint.Enabled = False
            Me.btnPrintPreview.Enabled = False
            Me.btnShow.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print Preview" Then
                    Me.btnPrintPreview.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    If Me.btnPrint.Text = "&Print" Then btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    ''' <summary>
    ''' Ayesha Rehman : TFS2726 : Fill list bosex
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "HeadCostCenter" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
            ElseIf Condition = "CostCenter" Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1")
            ElseIf Condition = "Vendor" Then
                FillListBox(Me.lstVendor.ListItem, "SELECT coa_detail_id, detail_title FROM vwCOADetail WHERE Active = 1 " & IIf(getConfigValueByType("Show Vendor On Sales") = "True", " And account_type IN ('Vendor','Customer')", "AND account_type = 'Customer' ") & " ORDER BY coa_detail_id")
            ElseIf Condition = "Item" Then
                FillListBox(Me.lstItem.ListItem, "SELECT ArticleId, ArticleCode + ' ~ ' + ArticleDescription AS ArticleName FROM ArticleDefTable WHERE Active = 1 ORDER BY SortOrder")

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub
    ''' <summary>
    ''' Ayesha Rehman : TFS2726 : Get all records to get data in given duration
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""

            'str = "SELECT SalesMasterTable.SalesId, SalesDetailTable.LocationId, tblDefLocation.location_name AS LocationName, SalesMasterTable.CostCenterId, tblDefCostCenter.Name AS CostCenter, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, SalesMasterTable.CustomerCode AS CustomerId, vwCOADetail.detail_title AS Customer, SalesDetailTable.SaleDetailId, SalesDetailTable.ArticleDefId AS ItemId, ArticleDefView.ArticleCode AS ItemCode, ArticleDefView.ArticleDescription AS ItemName, SalesDetailTable.Price, SalesDetailTable.CostPrice , SalesDetailTable.PurchasePrice, " & IIf(flgAvgRate = True, "SalesDetailTable.Price - SalesDetailTable.CostPrice ", "SalesDetailTable.Price - SalesDetailTable.PurchasePrice") & " AS Profit " _
            '      & "FROM SalesMasterTable INNER JOIN SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId LEFT OUTER JOIN tblDefLocation ON SalesDetailTable.LocationId = tblDefLocation.location_id INNER JOIN vwCOADetail ON SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id INNER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefCostCenter ON SalesMasterTable.CostCenterId = tblDefCostCenter.CostCenterID " _
            '    & "WHERE SalesMasterTable.SalesDate BETWEEN '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "' AND '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "'"

            'str = "SELECT SalesMasterTable.SalesId, SalesMasterTable.CostCenterId, tblDefCostCenter.Name AS CostCenter, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, SalesMasterTable.CustomerCode AS CustomerId, vwCOADetail.detail_title AS Customer, SalesDetailTable.SaleDetailId, SalesDetailTable.ArticleDefId AS ItemId, ArticleDefView.ArticleCode AS ItemCode, ArticleDefView.ArticleDescription AS ItemName, SalesDetailTable.Price, SalesDetailTable.CostPrice , SalesDetailTable.PurchasePrice, " & IIf(flgAvgRate = True, "SalesDetailTable.Price - SalesDetailTable.CostPrice ", "SalesDetailTable.Price - SalesDetailTable.PurchasePrice") & " AS Profit " _
            '     & "FROM SalesMasterTable INNER JOIN SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId INNER JOIN vwCOADetail ON SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id INNER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefCostCenter ON SalesMasterTable.CostCenterId = tblDefCostCenter.CostCenterID " _
            '   & "WHERE SalesMasterTable.SalesDate BETWEEN '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "' AND '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "'"

            str = "SELECT  tblDefCostCenter.Name AS CostCenter, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate,vwCOADetail.detail_title AS Customer, ArticleDefView.ArticleCode AS ItemCode, ArticleDefView.ArticleDescription AS ItemName, SalesDetailTable.Price, SalesDetailTable.CostPrice , SalesDetailTable.PurchasePrice, " & IIf(flgAvgRate = True, "SalesDetailTable.Price - SalesDetailTable.CostPrice ", "SalesDetailTable.Price - SalesDetailTable.PurchasePrice") & " AS Profit " _
                 & "FROM SalesMasterTable INNER JOIN SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId INNER JOIN vwCOADetail ON SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id INNER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefCostCenter ON SalesMasterTable.CostCenterId = tblDefCostCenter.CostCenterID " _
               & "WHERE SalesMasterTable.SalesDate BETWEEN '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "' AND '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "'"
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                str += " AND SalesMasterTable.CostCenterId IN (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstVendor.SelectedIDs.Length > 0 Then
                str += " AND SalesMasterTable.CustomerCode IN (" & Me.lstVendor.SelectedIDs & ")"
            End If
            If Me.lstItem.SelectedIDs.Length > 0 Then
                str += " AND SalesDetailTable.ArticleDefId IN (" & Me.lstItem.SelectedIDs & ")"
            End If
            str += "Order by SalesMasterTable.SalesDate desc"
            Dim dt As DataTable = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    ''' <summary>
    ''' Ayesha Rehman : TFS2726 : Reset all controls to their default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.cmbPeriod.Text = "Current Month"
            Me.txtSearch.Text = ""
            FillCombos("HeadCostCenter")
            FillCombos("CostCenter")
            FillCombos("Vendor")
            FillCombos("Item")
            If Not getConfigValueByType("AvgRate").ToString = "Error" Then
                flgAvgRate = getConfigValueByType("AvgRate")
            Else
                flgAvgRate = False
            End If
            _SearchDt = CType(Me.lstItem.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.UltraTabControl1.Tabs(0).Selected = True
            CtrlGrdBar1_Load(Nothing, Nothing)
            ApplySecurity(Utility.EnumDataMode.New)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
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
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Invoice Wise Profit Report"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGRNDetailReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            btnNew_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman : TFS2726 : Show crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click, btnPrintPreview.Click
        Try
            GetCrystalReportRights()
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            'AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MMM-dd 00:00:00"))
            'AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MMM-dd 23:59:59"))
            'AddRptParam("@flgAvgRate", IIf(flgAvgRate = True, 1, 0))
            'AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            'AddRptParam("@VendorIds", Me.lstVendor.SelectedIDs)
            'AddRptParam("@ItemIds", Me.lstItem.SelectedIDs)
            'ShowReport("rptInvoiceWiseProfitReport")
            Dim Str As String = String.Empty
            Str = "SELECT  tblDefCostCenter.Name AS CostCenter, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate,vwCOADetail.detail_title AS Customer, ArticleDefView.ArticleCode AS ItemCode, ArticleDefView.ArticleDescription AS ItemName, SalesDetailTable.Price, SalesDetailTable.CostPrice , SalesDetailTable.PurchasePrice, " & IIf(flgAvgRate = True, "SalesDetailTable.Price - SalesDetailTable.CostPrice ", "SalesDetailTable.Price - SalesDetailTable.PurchasePrice") & " AS Profit " _
                  & "FROM SalesMasterTable INNER JOIN SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId INNER JOIN vwCOADetail ON SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id INNER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefCostCenter ON SalesMasterTable.CostCenterId = tblDefCostCenter.CostCenterID " _
                & "WHERE SalesMasterTable.SalesDate BETWEEN '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "' AND '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "'"
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                Str += " AND SalesMasterTable.CostCenterId IN (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstVendor.SelectedIDs.Length > 0 Then
                Str += " AND SalesMasterTable.CustomerCode IN (" & Me.lstVendor.SelectedIDs & ")"
            End If
            If Me.lstItem.SelectedIDs.Length > 0 Then
                Str += " AND SalesDetailTable.ArticleDefId IN (" & Me.lstItem.SelectedIDs & ")"
            End If
            Str += "Order by SalesMasterTable.SalesDate desc"
            Dim dt As DataTable = GetDataTable(Str)
            ShowReport("rptInvoiceWiseProfit", , , , , , , dt, , , , )


            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            Finally
                Me.Cursor = Cursors.Default
                Me.lblProgress.Visible = False
            End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            GetAllRecords()
            Me.UltraTabControl1.Tabs(1).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Search item list by name or code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "ArticleName Like '%" & Me.txtSearch.Text & "%'"
            Me.lstItem.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.btnPrint.Visible = False
            End If
            If Me.UltraTabControl1.Tabs(1).Selected = True Then
                Me.btnPrint.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstHeadCostCenter_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstHeadCostCenter.SelectedIndexChaned
        Try
            If Me.lstHeadCostCenter.SelectedIDs.Length > 0 Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1  AND CostCenterGroup IN (" & Me.lstHeadCostCenter.SelectedItems & ")")
            Else
                FillCombos("CostCenter")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class
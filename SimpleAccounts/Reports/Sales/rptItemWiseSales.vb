''28-04-2016 TAKS-414 Muhammad Ameen: Applied grouping for the Grid.
Public Class rptItemWiseSales
    Implements IGeneral
    Dim IsOpenForm As Boolean = False

    Enum EnumGrid
        SortOrder
        articleId
        CompanyName
        Code
        Name
        Unit
        GroupDepartment
        ProductType
        Category
        SubCategory
        Price
        Qty
        SchemeQty
        Amount
        NetBill
        DamageQty
        Danage_Percent
        UnitWeight
        TotalWeight
        CostPrice
        CostAmount
        CityName
        Territory
        Provice
        SaleMan
        Manager

    End Enum
    Enum EnumGridConsolidate
        SortOrder
        articleId
        CompanyName
        Code
        Name
        Unit
        GroupDepartment
        ProductType
        Category
        SubCategory
        Price
        Qty
        SchemeQty
        Discount
        Amount
        TotalAmount
        NetBill
        DamageQty
        Damage_Percent
        UnitWeight
        TotalWeight
        CostPrice
        CostAmount
        CityName
        Territory
        Provice
        SaleMan
        Manager
    End Enum

    Private Sub btnGenerateReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerateReport.Click
        Me.GetAllRecords()
    End Sub
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdRcords.RootTable.Columns(0).Visible = False
            Me.grdRcords.RootTable.Columns(1).Visible = False
            Me.grdRcords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdRcords.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            Me.grdRcords.RecordNavigator = True
            Me.grdRcords.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
            Me.grdRcords.RootTable.Columns("CompanyName").Caption = "Company"



            Me.grdRcords.RootTable.Columns("Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRcords.RootTable.Columns("Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdRcords.RootTable.Columns("Price").FormatString = "N" & DecimalPointInValue

            If Me.RbtDetail.Checked = True Then
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdRcords.RootTable.Columns
                    If col.Index = EnumGrid.Qty Or col.Index = EnumGrid.Amount Or col.Index = EnumGrid.SchemeQty Or col.Index = EnumGrid.NetBill Or col.Index = EnumGrid.DamageQty Or col.Index = EnumGrid.TotalWeight Or col.Index = EnumGrid.CostAmount Then
                        col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        If Not col.Index = EnumGridConsolidate.Qty Or col.Index = EnumGridConsolidate.SchemeQty Then
                            col.FormatString = "N" & DecimalPointInValue
                            col.TotalFormatString = "N" & DecimalPointInValue
                        Else
                            col.FormatString = "N" & DecimalPointInQty
                            col.TotalFormatString = "N" & DecimalPointInQty
                        End If
                    End If
                Next
                Me.grdRcords.RootTable.Columns("Damage %").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdRcords.RootTable.Columns("Damage %").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grdRcords.RootTable.Columns("Damage %").FormatString = "N" & DecimalPointInValue
            Else
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdRcords.RootTable.Columns
                    If col.Index = EnumGridConsolidate.Qty Or col.Index = EnumGridConsolidate.SchemeQty Or col.Index = EnumGridConsolidate.Discount Or col.Index = EnumGridConsolidate.Amount Or col.Index = EnumGridConsolidate.TotalAmount Or col.Index = EnumGridConsolidate.NetBill Or col.Index = EnumGridConsolidate.DamageQty Or col.Index = EnumGridConsolidate.TotalWeight Or col.Index = EnumGridConsolidate.CostAmount Then
                        col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        col.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        If Not col.Index = EnumGridConsolidate.Qty Or col.Index = EnumGridConsolidate.SchemeQty Then
                            col.FormatString = "N" & DecimalPointInValue
                            col.TotalFormatString = "N" & DecimalPointInValue
                        Else
                            col.FormatString = "N" & DecimalPointInQty
                            col.TotalFormatString = "N" & DecimalPointInQty
                        End If
                    End If
                Next
                Me.grdRcords.RootTable.Columns("Damage %").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdRcords.RootTable.Columns("Damage %").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                Me.grdRcords.RootTable.Columns("Damage %").FormatString = "N" & DecimalPointInValue
                Me.grdRcords.RootTable.Columns("CompanyName").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Empty
                Me.grdRcords.RootTable.Columns("Code").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Empty
                Me.grdRcords.RootTable.Columns("Name").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Empty
                Me.grdRcords.RootTable.Columns("Unit").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Empty

                Me.grdRcords.RootTable.Columns("CompanyName").TextAlignment = Janus.Windows.GridEX.TextAlignment.Empty
                Me.grdRcords.RootTable.Columns("Code").TextAlignment = Janus.Windows.GridEX.TextAlignment.Empty
                Me.grdRcords.RootTable.Columns("Name").TextAlignment = Janus.Windows.GridEX.TextAlignment.Empty
                Me.grdRcords.RootTable.Columns("Unit").TextAlignment = Janus.Windows.GridEX.TextAlignment.Empty

            End If
            Me.grdRcords.RootTable.Columns("SchemeQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdRcords.RootTable.Columns("SchemeQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRcords.RootTable.Columns("SchemeQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdRcords.RootTable.Columns("Unit Weight").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRcords.RootTable.Columns("Unit Weight").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grdRcords.RootTable.Columns("Total Weight").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRcords.RootTable.Columns("Total Weight").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRcords.RootTable.Columns("Total Weight").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdRcords.RootTable.Columns("Cost Price").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRcords.RootTable.Columns("Cost Price").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grdRcords.RootTable.Columns("Cost Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRcords.RootTable.Columns("Cost Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRcords.RootTable.Columns("Cost Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdRcords.RootTable.Columns("Unit Weight").FormatString = "N" & DecimalPointInQty
            Me.grdRcords.RootTable.Columns("Total Weight").FormatString = "N" & DecimalPointInQty
            Me.grdRcords.RootTable.Columns("Cost Price").FormatString = "N" & DecimalPointInValue
            Me.grdRcords.RootTable.Columns("Cost Amount").FormatString = "N" & DecimalPointInValue
            Me.grdRcords.RootTable.Columns("Total Weight").TotalFormatString = "N" & DecimalPointInQty
            Me.grdRcords.RootTable.Columns("Cost Amount").TotalFormatString = "N" & DecimalPointInValue

            'Dim grp As New Janus.Windows.GridEX.GridEXGroup(Me.grdRcords.RootTable.Columns("Price"))
            'Me.grdRcords.RootTable.Groups.Add(grp)
            'Me.grdRcords.ExpandGroups()
            Me.grdRcords.AutoSizeColumns()
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            FillDropDown(cmbCompany, "Select CompanyId, CompanyName from CompanyDefTable")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim IsSalesOrderAnalysis As Boolean
            If Not GetConfigValue("SalesOrderAnalysis").ToString = "Error" Then
                IsSalesOrderAnalysis = Convert.ToBoolean(GetConfigValue("SalesOrderAnalysis").ToString)
            End If


            Dim strSql As String = String.Empty
            If Me.RbtDetail.Checked = True AndAlso Me.rboLoose.Checked = True Then
                strSql = "rptItemWiseSales '" & Me.dtpFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "','" & Me.dtpTo.Value.Date.ToString("yyyy-M-d 23:59:59") & "'"
                'If Me.cmbCompany.SelectedIndex > 0 Then
                '    'strSql += ", LocationId = " & Me.cmbCompany.SelectedValue & " "
                strSql += "," & Me.cmbCompany.SelectedValue & ""
                strSql += "," & Me.cmbCostCenter.SelectedValue & ""
            ElseIf Me.RbtDetail.Checked = True AndAlso Me.rboPack.Checked = True Then
                strSql = "rptItemWiseSalesPack '" & Me.dtpFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "','" & Me.dtpTo.Value.Date.ToString("yyyy-M-d 23:59:59") & "'"
                'If Me.cmbCompany.SelectedIndex > 0 Then
                '    'strSql += ", LocationId = " & Me.cmbCompany.SelectedValue & " "
                strSql += "," & Me.cmbCompany.SelectedValue & ""
                strSql += "," & Me.cmbCostCenter.SelectedValue & ""
                ' End If
            Else
                strSql = "rptItemWiseSalesConsolidate '" & Me.dtpFrom.Value.Date.ToString("yyyy-M-d 00:00:00") & "','" & Me.dtpTo.Value.Date.ToString("yyyy-M-d 23:59:59") & "'"
                'If Me.cmbCompany.SelectedIndex > 0 Then
                strSql += "," & Me.cmbCompany.SelectedValue & ""
                strSql += "," & Me.cmbCostCenter.SelectedValue & ""
                'End If
            End If
            Me.grdRcords.DataSource = GetDataTable(strSql)
            Me.grdRcords.RetrieveStructure()
            Me.ApplyGridSettings()

            If IsSalesOrderAnalysis = True Then
                If Not Me.RbtConsolidate.Checked = True Then
                    Me.grdRcords.RootTable.Columns("Amount").Visible = False
                    Me.grdRcords.RootTable.Columns("NetBill").Visible = True
                Else
                    Me.grdRcords.RootTable.Columns("Discount").Visible = False
                    Me.grdRcords.RootTable.Columns("Total_Amount").Visible = False
                    Me.grdRcords.RootTable.Columns("Amount").Visible = False
                    Me.grdRcords.RootTable.Columns("NetBill").Visible = True
                End If
            Else
                Me.grdRcords.RootTable.Columns("Amount").Visible = True
                Me.grdRcords.RootTable.Columns("NetBill").Visible = False
            End If



        Catch ex As Exception
            msg_Error("Error occured while generating report: " & ex.Message)
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub rptItemWiseSales_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            'Me.dtpFrom.Value = Date.Now.AddMonths(-1)
            'Me.dtpTo.Value = Date.Now
            FillCombos("company")
            FillDropDown(Me.cmbCostCenter, "select * from tbldefCostCenter order by sortorder , name", True)
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub RbtDetail_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtDetail.CheckedChanged
        Try
            grpLoosePack.Visible = True
            If IsOpenForm = True Then GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub RbtConsolidate_CheckedChanged(sender As Object, e As EventArgs) Handles RbtConsolidate.CheckedChanged
        Try
            grpLoosePack.Visible = False
            If IsOpenForm = True Then GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = "Item Wise Sales"
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdRcords.Name) Then
                Dim fs1 As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdRcords.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdRcords.LoadLayoutFile(fs1)
                fs1.Dispose()
                fs1.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & Chr(10) & "Invoice Type:" & IIf(Me.cmbCompany.SelectedIndex > 0, "" & Me.cmbCompany.Text & "", "All") & Chr(10) & "Item Wise Sales Report" & Chr(10) & "Date From: " & Me.dtpFrom.Value.ToString("dd-MM-yyyy") & " Date To: " & Me.dtpTo.Value.ToString("dd-Mm-yyyy") & ""
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

    Private Sub ItemWiseSalesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemWiseSalesToolStripMenuItem.Click
        Try
            AddRptParam("@FromDate", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("@CompanyId", Me.cmbCompany.SelectedValue)
            'Ali Faisal : TFS1635 : Add Cost center parameter for report print
            AddRptParam("@CostCentreId", Me.cmbCostCenter.SelectedValue)
            'Ali Faisal : TFS1635 : End
            ShowReport("rptItemWiseSaleReport")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ItemWiseSalesConsolidateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemWiseSalesConsolidateToolStripMenuItem.Click
        Try
            AddRptParam("@FromDate", Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59"))
            AddRptParam("@CompanyId", Me.cmbCompany.SelectedValue)
            'Ali Faisal : TFS1635 : Add Cost center parameter for report print
            AddRptParam("@CostCenterId", Me.cmbCostCenter.SelectedValue)
            'Ali Faisal : TFS1635 : End
            ShowReport("rptItemWiseSalesConsolidate")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnChart_Click(sender As Object, e As EventArgs) Handles btnChart.Click
        Try
            ApplyStyleSheet(frmRptCustomizeCharts)
            frmRptCustomizeCharts.cmbChartType.SelectedIndex = frmRptCustomizeCharts.enmChartType.Column
            frmRptCustomizeCharts._TopRecords = 5
            frmRptCustomizeCharts._grd = Me.grdRcords
            frmRptCustomizeCharts._XValueMember = "Name"
            If Me.RbtDetail.Checked Then
                frmRptCustomizeCharts._YValueMember = "Amount"
            Else
                frmRptCustomizeCharts._YValueMember = "Price"
            End If
            frmRptCustomizeCharts.Text = "Item Wise Sales Report"
            frmRptCustomizeCharts.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
End Class
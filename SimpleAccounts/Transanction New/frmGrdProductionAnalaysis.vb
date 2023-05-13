'5-8-2014 TASK:2614 Imran Ali Delete Record Production Analysis
Imports SBDal
Imports SBModel
Imports SBUtility
Imports SBUtility.Utility
Imports Janus.Windows.GridEX

Public Class frmGrdProductionAnalaysis
    Implements IGeneral
    Private _IsNormalMode As Boolean = True
    Dim aid As Integer = 0I
    Dim pa As GrdProductionAlnalysisBE
    Dim productioList As List(Of GrdProductionAlnalysisBE)
    Dim flgShift As Boolean = False
    Dim FirstTimeSelectProject As Boolean = False

    Private Sub frmGrdProductionAnalaysis_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        If e.KeyCode = Keys.F4 Then
            btnSave_Click(Nothing, Nothing)

        End If
        If e.KeyCode = Keys.Escape Then
            btnNew_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.P AndAlso e.Control = True Then
            btnPrint_Click(Nothing, Nothing)
        End If

    End Sub

    Private Sub frmGrdProductionAnalaysis_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            FillCombos()
            GetAllRecords("LoadOnSaveMode")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub frmGrdProductionAnalaysis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            'For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
            '    If c.Index = 0 AndAlso c.Index = 3 AndAlso c.Index = 4 Then
            '        c.EditType = Janus.Windows.GridEX.EditType.NoEdit
            '    End If
            'Next


            Me.grd.RootTable.Columns(0).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(3).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(4).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("SalePrice").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Price").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Stock_Amount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns("Demand_Amount").EditType = Janus.Windows.GridEX.EditType.NoEdit

            Me.grd.RootTable.Columns("PackQty").Visible = False
            Me.grd.RootTable.Columns("ArticleId").Visible = False
            Me.grd.RootTable.Columns("AnalysisDate").Visible = False
            Me.grd.RootTable.Columns("ProjectId").Visible = False
            Me.grd.RootTable.Columns("SalePrice").Caption = "Price"
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                r.BeginEdit()
                r.Cells("ProjectId").Value = Me.cmbProject.SelectedValue
                r.Cells("AnalysisDate").Value = Me.dtpDemandDate.Value
                r.EndEdit()
            Next

            Me.grd.RootTable.Columns("Demand").AggregateFunction = AggregateFunction.Sum
            Me.grd.RootTable.Columns("CurrentStock").AggregateFunction = AggregateFunction.Sum
            Me.grd.RootTable.Columns("Production").AggregateFunction = AggregateFunction.Sum
            Me.grd.RootTable.Columns("Estimate").AggregateFunction = AggregateFunction.Sum
            Me.grd.RootTable.Columns("Batch").AggregateFunction = AggregateFunction.Sum
            Me.grd.RootTable.Columns("Stock_Amount").AggregateFunction = AggregateFunction.Sum
            Me.grd.RootTable.Columns("Demand_Amount").AggregateFunction = AggregateFunction.Sum


            Me.grd.RootTable.Columns("Demand").TextAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("CurrentStock").TextAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("Production").TextAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("Estimate").TextAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("Batch").TextAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("SalePrice").TextAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("Price").TextAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("Stock_Amount").TextAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("Demand_Amount").TextAlignment = TextAlignment.Far


            Me.grd.RootTable.Columns("Demand").HeaderAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("CurrentStock").HeaderAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("Production").HeaderAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("Estimate").HeaderAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("Batch").HeaderAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("SalePrice").HeaderAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("Price").HeaderAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("Stock_Amount").HeaderAlignment = TextAlignment.Far
            Me.grd.RootTable.Columns("Demand_Amount").HeaderAlignment = TextAlignment.Far

            Me.grd.RootTable.Columns("Batch").FormatString = "N"
            Me.grd.RootTable.Columns("Batch").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Stock_Amount").FormatString = "N"
            Me.grd.RootTable.Columns("Demand_Amount").FormatString = "N"
            Me.grd.RootTable.Columns("Stock_Amount").TotalFormatString = "N"
            Me.grd.RootTable.Columns("Demand_Amount").TotalFormatString = "N"

            Me.grd.RootTable.Columns("SalePrice").CellStyle.BackColor = Color.Snow
            Me.grd.RootTable.Columns("Price").CellStyle.BackColor = Color.Snow
            Me.grd.RootTable.Columns("Stock_Amount").CellStyle.BackColor = Color.Ivory
            Me.grd.RootTable.Columns("Demand_Amount").CellStyle.BackColor = Color.Ivory

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New GrdProductionAnalysisDAL().Delete(productioList) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            FillDropDown(Me.cmbProject, "Select CostCenterId, Name From tblDefCostCenter")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            'Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
            ''Dim dr As DataRow
            ''dr = dt.NewRow
            'For Each r As DataRow In dt.Rows
            '    pa.Id = aid
            '    pa.AnalysisDate = dt.Rows(0).Item("AnalysisDate")
            '    pa.ProjectId = dt.Rows(0).Item("ProjectId")
            '    pa.ArticleDefId = dt.Rows(0).Item("ArticleId")
            '    pa.PackQty = dt.Rows(0).Item("PackQty")
            '    pa.Demand = dt.Rows(0).Item("Demand")
            '    pa.CurrentStock = dt.Rows(0).Item("CurrentStock")
            '    pa.Production = dt.Rows(0).Item("Production")
            '    pa.Estimate = dt.Rows(0).Item("Estimate")
            '    pa.Batch = dt.Rows(0).Item("Batch")
            'Next

            productioList = New List(Of GrdProductionAlnalysisBE)
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                pa = New GrdProductionAlnalysisBE
                pa.Id = aid
                pa.AnalysisDate = row.Cells("AnalysisDate").Value
                pa.ProjectId = row.Cells("ProjectId").Value
                pa.ArticleDefId = row.Cells("ArticleId").Value
                pa.PackQty = row.Cells("PackQty").Value
                pa.Demand = row.Cells("Demand").Value
                pa.CurrentStock = row.Cells("CurrentStock").Value
                pa.Production = row.Cells("Production").Value
                pa.Estimate = row.Cells("Estimate").Value
                pa.Batch = row.Cells("Batch").Value
                productioList.Add(pa)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = String.Empty

            If Condition = "LoadOnSaveMode" Then
                str = "SELECT TOP 100 PERCENT Convert(DateTime, '') as AnalysisDate, 0 AS ProjectId, dbo.ArticleDefView.ArticleId, " _
                     & " dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, Isnull(ArticleDefView.PackQty,0) as PackQty,  " _
                     & " 0 AS Demand, ArticleDefView.SalePrice, Convert(float,0) as Demand_Amount, 0 AS CurrentStock, ArticleDefView.SalePrice as Price, Convert(float,0) as Stock_Amount,   " _
                     & " 0 AS Production, 0 AS Estimate, Convert(float, 0) AS Batch " _
                     & " FROM  dbo.ArticleDefView " _
                     & " WHERE (dbo.ArticleDefView.Active = 1) AND (dbo.ArticleDefView.SalesItem = 1)  " _
                     & " ORDER BY dbo.ArticleDefView.SortOrder "

                Dim dt As New DataTable
                dt = GetDataTable(str)

                str = String.Empty
                'str = "SELECT  b.ArticleDefId, SUM(isnull(b.Qty,0)) AS Qty FROM  dbo.SalesOrderMasterTable AS a INNER JOIN dbo.SalesOrderDetailTable AS b ON a.SalesOrderId = b.SalesOrderId WHERE (Convert(Varchar, SalesOrderDate, 102) = Convert(DateTime, '" & Me.dtpDemandDate.Value.ToString("yyyy-M-d 00:00:00") & "',102)) " & IIf(Me.cmbProject.SelectedIndex > 0, " AND  CostCenterId=" & Me.cmbProject.SelectedValue & "", "") & " GROUP BY b.ArticleDefId"
                str = "SELECT  b.ArticleDefId, SUM(isnull(b.Qty,0)) AS Qty FROM  dbo.SalesOrderMasterTable AS a INNER JOIN dbo.SalesOrderDetailTable AS b ON a.SalesOrderId = b.SalesOrderId WHERE (Convert(Varchar, SalesOrderDate, 102) = Convert(DateTime, '" & Me.dtpDemandDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))  GROUP BY b.ArticleDefId"
                Dim dtData As New DataTable
                dtData = GetDataTable(str)
                Dim dr() As DataRow
                If dt IsNot Nothing Then
                    For Each r As DataRow In dt.Rows
                        dr = dtData.Select("ArticleDefId=" & r.Item("ArticleId").ToString & "")
                        If dr.Length > 0 Then
                            For Each DrFound As DataRow In dr
                                r.BeginEdit()
                                r("Demand") = DrFound(1)
                                r.EndEdit()
                            Next
                        End If
                    Next
                End If

                str = String.Empty
                str = "Select vwArt.ArticleId as ArticleDefId, (ISNULL(Opening.Qty,0) " & IIf(flgShift = True, "+0)", " +ISNULL(NStock.Qty,0))") & " as OpeningQty From ArticleDefView vwArt LEFT OUTER JOIN (Select ArticleDefId, SUM(ISNULL(INQty,0)-ISNULL(OutQty,0)) As Qty From StockMasterTable INNER JOIN StockDetailTable On StockMasterTable.StockTransId = StockDetailTable.StockTransID LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = StockDetailTable.LocationId WHERE (Convert(Varchar, StockMasterTable.docDate, 102) < Convert(DateTime, '" & Me.dtpDemandDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND tblDefLocation.Location_Type <> 'Damage' Group By ArticleDefID) Opening On Opening.ArticleDefId = vwArt.ArticleId LEFT OUTER JOIN (Select ArticleDefID, SUM(Isnull(InQty,0)-isnull(OutQty,0)) as Qty From StockMasterTable  INNER JOIN StockDetailTable on StockDetailTable.StockTransID = StockMasterTAble.StockTransId  LEFT OUTER JOIN tblDefCostCenter on tblDefCostCenter.CostCenterId = StockMasterTable.Project LEFT OUTER JOIN tblDefLocation on tblDefLocation.Location_Id = StockDetailTable.LocationId  WHERE ISNULL(tbldefCostCenter.DayShift,0)=1 AND tblDefLocation.Location_Type <> 'Damage' AND (Convert(varchar, StockMasterTable.docDate, 102) = Convert(Datetime, '" & Me.dtpDemandDate.Value.ToString("yyyy-M-d 00:00:00") & "' ,102)) Group By ArticleDefID) NStock On NStock.ArticleDefId = vwArt.ArticleId WHERE vwArt.SalesItem=1 "
                Dim dtData1 As New DataTable
                dtData1 = GetDataTable(str)
                Dim dr1() As DataRow
                If dtData1 IsNot Nothing Then
                    For Each r As DataRow In dt.Rows
                        dr1 = dtData1.Select("ArticleDefId=" & r.Item("ArticleId").ToString & "")
                        If dr1.Length > 0 Then
                            For Each DrFound As DataRow In dr1
                                r.BeginEdit()
                                r("CurrentStock") = DrFound(1)
                                r.EndEdit()
                            Next
                        End If
                    Next
                End If
                dt.Columns("Demand_Amount").Expression = "Demand*SalePrice"
                dt.Columns("Stock_Amount").Expression = "CurrentStock*Price"
                dt.Columns("Production").Expression = "Demand-CurrentStock"
                dt.Columns("Batch").Expression = "IIF(PackQty=0,0,(Estimate/PackQty))"
                dt.AcceptChanges()
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                Me.grd.RootTable.Columns("ProjectId").EditType = Janus.Windows.GridEX.EditType.Combo
                Me.grd.RootTable.Columns("ProjectId").HasValueList = True
                Dim dtCostCenter As New DataTable
                dtCostCenter = GetDataTable("Select CostCenterId, Name From tblDefCostCenter")
                Me.grd.RootTable.Columns("ProjectId").ValueList.PopulateValueList(dtCostCenter.DefaultView, "CostCenterId", "Name")
                ApplyGridSettings()
            ElseIf Condition = "LoadOnUpdateMode" Then
                str = "SELECT TOP 100 PERCENT dbo.tblProductionAnalysis.AnalysisDate, ISNULL(dbo.tblProductionAnalysis.ProjectId, 0) AS ProjectId, dbo.ArticleDefView.ArticleId, " _
                 & " dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleDescription, ISNULL(dbo.tblProductionAnalysis.PackQty, 0) AS PackQty,  " _
                 & " ISNULL(dbo.tblProductionAnalysis.Demand, 0) AS Demand, ArticleDefView.SalePrice, Convert(float, 0) as Demand_Amount, ISNULL(dbo.tblProductionAnalysis.CurrentStock, 0) AS CurrentStock,  ArticleDefView.SalePrice as Price, Convert(float,0) as Stock_Amount, " _
                 & " ISNULL(dbo.tblProductionAnalysis.Production, 0) AS Production, ISNULL(dbo.tblProductionAnalysis.Estimate, 0) AS Estimate, ISNULL(dbo.tblProductionAnalysis.Batch,  " _
                 & " 0) AS Batch " _
                 & " FROM  dbo.ArticleDefView LEFT OUTER JOIN " _
                 & " dbo.tblProductionAnalysis ON dbo.ArticleDefView.ArticleId = dbo.tblProductionAnalysis.ArticleDefId " _
                 & " WHERE (dbo.ArticleDefView.Active = 1) AND (dbo.ArticleDefView.SalesItem = 1) AND (Convert(varchar, AnalysisDate, 102) = Convert(DateTime, '" & Me.dtpDemandDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND ProjectId=" & Me.cmbProject.SelectedValue & "" _
                 & " ORDER BY dbo.ArticleDefView.SortOrder "
                Dim dt As New DataTable
                dt = GetDataTable(str)
                dt.Columns("Demand_Amount").Expression = "Demand*SalePrice"
                dt.Columns("Stock_Amount").Expression = "CurrentStock*Price"
                dt.Columns("Production").Expression = "Demand-CurrentStock"
                dt.Columns("Batch").Expression = "IIF(PackQty=0,0,(Estimate/PackQty))"
                dt.AcceptChanges()
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                Me.grd.RootTable.Columns("ProjectId").EditType = Janus.Windows.GridEX.EditType.Combo
                Me.grd.RootTable.Columns("ProjectId").HasValueList = True
                Dim dtCostCenter As New DataTable
                dtCostCenter = GetDataTable("Select CostCenterId, Name From tblDefCostCenter")
                Me.grd.RootTable.Columns("ProjectId").ValueList.PopulateValueList(dtCostCenter.DefaultView, "CostCenterId", "Name")
                ApplyGridSettings()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.dtpDemandDate.Value = Now
            If Not FirstTimeSelectProject = True Then Me.cmbProject.SelectedIndex = 0
            Me.GetAllRecords("LoadOnSaveMode")
            Me.btnSave.Text = "&Save"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New GrdProductionAnalysisDAL().Save(productioList) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            If Me.cmbProject.SelectedIndex = -1 Then Exit Sub
            If CheckExistingRecord(Me.dtpDemandDate.Value, Me.cmbProject.SelectedValue) = True Then
                _IsNormalMode = False
                Me.btnSave.Text = "&Update"
            Else
                _IsNormalMode = True
                Me.btnSave.Text = "&Save"
            End If

            If _IsNormalMode = False Then
                GetAllRecords("LoadOnUpdateMode")
            Else
                GetAllRecords("LoadOnSaveMode")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Function CheckExistingRecord(ByVal AnalysisDate As DateTime, ByVal ProjectId As Integer) As Boolean
        Try

            Dim dt As New DataTable
            Dim str As String = String.Empty
            'Before
            'str = "SELECT Count(*) RecordCount From tblProductionAnalysis WHERE (Convert(Varchar, AnalysisDate, 102) = Convert(DateTime, '" & Me.dtpDemandDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102))"
            'After
            str = "SELECT AnalysisDate,ProjectId From tblProductionAnalysis WHERE (Convert(Varchar, AnalysisDate, 102) = Convert(DateTime, '" & Me.dtpDemandDate.Value.ToString("yyyy-M-d 00:00:00") & "', 102)) AND ProjectId = '" & ProjectId & "' "

            dt = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            FirstTimeSelectProject = False
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If Me.btnSave.Text = "&Save" Then

                    If Save() = True Then
                        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
                        'msg_Information("Information saved successfully")
                        FirstTimeSelectProject = True
                        ReSetControls()
                    End If
                Else
                    If Save() = True Then
                        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
                        'msg_Information("Information update succesfully ")
                        FirstTimeSelectProject = True
                        ReSetControls()
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@Date", Me.dtpDemandDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@Projectid", Me.cmbProject.SelectedValue)
            ShowReport("rptProductionAnalysis")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProject_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProject.SelectedIndexChanged
        Try
            If Me.cmbProject.SelectedIndex = -1 Then Exit Sub
            Dim str As String = "Select ISNULL(DayShift,0) as DayShift From tblDefCostCenter WHERE CostCenterId=" & Me.cmbProject.SelectedValue
            Dim dt As New DataTable
            dt = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item(0) = True Then
                        flgShift = True
                    Else
                        flgShift = False
                    End If
                Else
                    flgShift = False
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(str_ApplicationStartUpPath & "\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New System.IO.FileStream(str_ApplicationStartUpPath & "\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Production Level Report " & vbCrLf & "Analysis Date:" & Me.dtpDemandDate.Value.ToString("dd-MM-yyyy") & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''8-May-2014 TASK: Imran Ali Delete Record Production Analysis
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Delete() = True Then
                msg_Information(str_informDelete)
                FirstTimeSelectProject = True
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14
        ''8-May-2014 TASK:2614 Imran Ali Delete Record Production Analysis
        'If e.KeyCode = Keys.Delete Then
        '    btnDelete_Click(Nothing, Nothing)
        'End If

    End Sub
End Class
'Ali Faisal : TFS1362 : Add Form for Engine wise stock report in grid and also crystal report on 22-Aug-2017
Imports SBModel
Public Class frmGrdRptEngineWiseStock
    ''' <summary>
    ''' Ali Faisal : TFS1362 : Get all records to show the records of stock having engine number
    ''' </summary>
    ''' <remarks>Ali Faisal : TFS1362 : 22-Aug-2017</remarks>
    Public Sub GetAllRecords()
        Try
            Dim str As String = ""
            str = "SELECT StockDetailTable.LocationId, tblDefLocation.location_name Location, ArticleDefView.ArticleGroupName AS Department, ArticleDefView.ArticleTypeName AS Type, StockDetailTable.ArticleDefId, ArticleDefView.ArticleCode, " _
                 & "ArticleDefView.ArticleDescription, ArticleDefView.ArticleSizeName Size, ArticleDefView.ArticleColorName Color, ArticleDefView.SalePrice, ArticleDefView.ArticleUnitName Unit, StockDetailTable.Engine_No EngineNo, " _
                 & "StockDetailTable.Chassis_No ChassisNo, SUM(StockDetailTable.InQty) AS InQty, SUM(StockDetailTable.OutQty) AS OutQty, SUM(StockDetailTable.InQty) - SUM(StockDetailTable.OutQty) AS StockQty " _
                 & "FROM         StockDetailTable INNER JOIN " _
                 & "ArticleDefView ON StockDetailTable.ArticleDefId = ArticleDefView.ArticleId INNER JOIN " _
                 & "tblDefLocation ON StockDetailTable.LocationId = tblDefLocation.location_id " _
                 & "WHERE (StockDetailTable.Engine_No > 0) "
            If Me.cmbLocation.SelectedValue > 0 Then
                str += "AND LocationId = " & Me.cmbLocation.SelectedValue & " "
            End If
            If Me.cmbItem.Value > 0 Then
                str += "AND ArticleDefId = " & Me.cmbItem.Value & " "
            End If
            str += "GROUP BY StockDetailTable.Engine_No, StockDetailTable.Chassis_No, StockDetailTable.ArticleDefId, StockDetailTable.LocationId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription,  " _
            & " ArticleDefView.SalePrice, ArticleDefView.ArticleTypeName, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, ArticleDefView.ArticleGroupName, " _
            & " tblDefLocation.location_name HAVING (SUM(StockDetailTable.InQty) - SUM(StockDetailTable.OutQty) > 0)"
            Dim dt As New DataTable
            dt = GetDataTable(str)
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()

            Me.grd.RootTable.Columns("InQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("OutQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("StockQty").FormatString = "N" & DecimalPointInQty

            Me.grd.RootTable.Columns("InQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("OutQty").TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("StockQty").TotalFormatString = "N" & DecimalPointInQty


            Me.grd.RootTable.Columns("InQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OutQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("StockQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("InQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("OutQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("StockQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("InQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("OutQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("StockQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("LocationId").Visible = False
            Me.grd.RootTable.Columns("ArticleDefId").Visible = False

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1362 : 22-Aug-2017 : Apply Security Rights for Standard user
    ''' </summary>
    ''' <remarks>Ali Faisal : TFS1362 : 22-Aug-2017</remarks>
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnPrint.Enabled = True
                Me.btnShow.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnPrint.Enabled = False
                    Me.btnShow.Enabled = False
                    Exit Sub
                End If
            Else
                Me.btnPrint.Enabled = False
                Me.btnShow.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    End If
                    If RightsDt.FormControlName = "Show" Then
                        Me.btnShow.Enabled = True
                    End If
                    If RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                    ''TASK TFS1384 replaced Report Print and Report Export with Report Print and Report Export on 07-09-2017
                    If RightsDt.FormControlName = "Report Print" Then
                        IsCrystalReportPrint = True
                    End If
                    If RightsDt.FormControlName = "Report Export" Then
                        IsCrystalReportExport = True
                    End If
                    ''End TASK TFS1384
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1362 : 22-Aug-2017 : Grid Control to get saved Layouts
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Ali Faisal : TFS1362 : 22-Aug-2017</remarks>
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Engine wise Stock Report" & vbCrLf & ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1362 : 22-Aug-2017 : Close form on button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Ali Faisal : TFS1362 : 22-Aug-2017</remarks>
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1362 : 22-Aug-2017 : Reset all controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Ali Faisal : TFS1362 : 22-Aug-2017</remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            FillDropDown(Me.cmbLocation, "Select * From tblDefLocation Where Active = 1 Order By 1", True)
            Dim Str As String = ""
            Str = "SELECT ArticleDefView.ArticleId as Id, ArticleDefView.ArticleDescription Item, ArticleDefView.ArticleCode Code, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID],Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SubSubId,0) as AccountId,SalesAccountId,CGSAccountId, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand],IsNull(Cost_Price,0) as Cost_Price, IsNull(TradePrice,0) as [Trade Price] FROM  ArticleDefView "
            Str += " where Active=1 "
            If ItemSortOrder = True Then
                Str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByCode = True Then
                Str += " ORDER BY ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByName = True Then
                Str += " ORDER BY ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            Else
                Str += " ORDER BY ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            End If
                FillUltraDropDown(Me.cmbItem, Str)
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SalesAccountId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("CGSAccountId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost_Price").Hidden = True
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
                End If
            Me.cmbLocation.SelectedValue = 0
            Me.cmbItem.Value = 0
            Me.grd.DataSource = Nothing
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1362 : 22-Aug-2017 : Get all records on Show button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Ali Faisal : TFS1362 : 22-Aug-2017</remarks>
    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1362 : 22-Aug-2017 : Print Crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Ali Faisal : TFS1362 : 22-Aug-2017</remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@LocationId", Me.cmbLocation.SelectedValue)
            AddRptParam("@ArticleId", Me.cmbItem.Value)
            ShowReport("rptEngineWiseStock")
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub frmGrdRptEngineWiseStock_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            btnNew_Click(Nothing, Nothing)
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
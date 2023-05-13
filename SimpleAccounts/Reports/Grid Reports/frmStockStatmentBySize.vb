Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class frmStockStatmentBySize
    'Public startDate As DateTime = Me.dtpFromDate.Value.AddMonths(-1)
    'Public EndDate As DateTime = Me.dtpToDate.Value
    Enum enumDtData
        LPO
        Type
        ArticleId
        Code
        Name
        Color
        Size
        Price
        Opening
        Rec
        Prod
        Disp
        Pur_R
        Sale
        Cust_R
        AddStock
        LessStock
        Closing
        Sz1
        Sz2
        Sz3
        Sz4
        Sz5
        Sz6
        Sz7
        Sz8
        Value
        Year
        DispValue
        RecValue
        SampleQty
        SampleQtyValue
        AddStockValue
        LessStockValue
        'SalesValue
    End Enum
    Private Sub frmStockStatmentBySize_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            Me.cmbPeriod.Text = "Current Month"
            FillDropDown(Me.cmbLocation, "Select Location_Id, Location_Name From tblDefLocation", False)
            GetDataStockWithSize()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Function configureDT() As DataTable
        Dim Dt As New DataTable
        Dt.Columns.Add(enumDtData.LPO.ToString, GetType(System.String))
        Dt.Columns.Add(enumDtData.Type.ToString, GetType(System.String))
        Dt.Columns.Add(enumDtData.ArticleId.ToString, GetType(System.Int32))
        Dt.Columns.Add(enumDtData.Code.ToString, GetType(System.String))
        Dt.Columns.Add(enumDtData.Name.ToString, GetType(System.String))
        Dt.Columns.Add(enumDtData.Color.ToString, GetType(System.String))
        Dt.Columns.Add(enumDtData.Size.ToString, GetType(System.String))
        Dt.Columns.Add(enumDtData.Price.ToString, GetType(System.String))
        Dt.Columns.Add(enumDtData.Opening.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Rec.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Prod.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Disp.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Pur_R.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Sale.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Cust_R.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.AddStock.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.LessStock.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Closing.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Sz1.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Sz2.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Sz3.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Sz4.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Sz5.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Sz6.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Sz7.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Sz8.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Value.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.Year.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.DispValue.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.RecValue.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.SampleQty.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.SampleQtyValue.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.AddStockValue.ToString, GetType(System.Double))
        Dt.Columns.Add(enumDtData.LessStockValue.ToString, GetType(System.Double))
        'Dt.Columns.Add(enumDtData.SalesValue.ToString, GetType(System.Double))

        Return Dt
    End Function

    Private Sub GridEX1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Dim dt As DataTable = GetFormRights(EnumForms.frmStockStatmentBySize)
            Me.Visible = False
            CtrlGrdBar1.mGridPrint.Enabled = False
            CtrlGrdBar1.mGridExport.Enabled = False
            CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For Each RightsDt As GroupRights In Rights
                If RightsDt.FormControlName = "View" Then
                    Me.Visible = True
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
    Private Sub GridEX1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.SelectionChanged
        Try
            Me.Text = "Stock Statment [" & GridEX1.GetRow.Cells(enumDtData.Code).Text & "]"
            If Me.SplitContainer1.Panel2Collapsed = True Then Exit Sub
            If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                frmhistoryBySize.Close()
            End If
            If GridEX1.RowCount = 0 Then
                Exit Sub
            End If
            frmhistoryBySize.article_code = GridEX1.GetRow.Cells(enumDtData.ArticleId).Value 'GridEX1.GetRow.Cells(enumDtData.Code).Text
            frmhistoryBySize.ArticleCode = GridEX1.GetRow.Cells(enumDtData.Code).Value 'GridEX1.GetRow.Cells(enumDtData.Code).Text
            frmhistoryBySize.startDate = Me.dtpFromDate.Value
            frmhistoryBySize.EndDate = Me.dtpToDate.Value
            frmhistoryBySize.ArticleDescription = Me.GridEX1.GetRow.Cells(enumDtData.Name).Text
            'frmhistoryBySize.LocationId = Me.cmbLocation.SelectedValue
            '  frmhistory.Show()
            frmhistoryBySize.TopLevel = False
            frmhistoryBySize.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            frmhistoryBySize.Dock = DockStyle.Fill
            Me.SplitContainer1.Panel2.Controls.Add(frmhistoryBySize)
            frmhistoryBySize.Show()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.SplitContainer1.Panel2Collapsed = True Then
            Me.SplitContainer1.Panel2Collapsed = False
            GridEX1_SelectionChanged(GridEX1, Nothing)
        Else
            Me.SplitContainer1.Panel2Collapsed = True
            If Me.SplitContainer1.Panel2.Controls.Count > 0 Then
                frmhistoryBySize.Close()
            End If
        End If
    End Sub
    Private Sub btnHideZero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHideZero.Click
        Try
            If Me.btnHideZero.Text = "Show Zero" Then
                Me.btnHideZero.Text = "Hide Zero"
                Me.GridEX1.RemoveFilters()
            Else
                Me.btnHideZero.Text = "Show Zero"
                Dim c As New Janus.Windows.GridEX.GridEXFilterCondition(Me.GridEX1.RootTable.Columns(enumDtData.Closing), Janus.Windows.GridEX.ConditionOperator.NotEqual, 0)
                Me.GridEX1.RootTable.ApplyFilter(c)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGenerate.Click
        Try
            GetDataStockWithSize()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetDataStockWithSize()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dt As New DataTable
            Dim adp As New SqlClient.SqlDataAdapter
            Dim dtData As New DataTable()
            'adp = New SqlClient.SqlDataAdapter("SP_Rpt_StockStatement '" & Me.dtpFromDate.Value.Date.ToString("yyyy-MM-dd") & "', '" & Me.dtpToDate.Value.Date.ToString("yyyy-MM-dd") & "', " & Me.cmbLocation.SelectedValue, SQLHelper.CON_STR)
            adp = New SqlClient.SqlDataAdapter("SP_Rpt_StockStatement '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "', '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', " & IIf(Me.rdLoose.Checked = True, 0, 1) & "", SQLHelper.CON_STR)
            dt.Clear()
            adp.Fill(dt)
            dtData = configureDT()
            Dim colNo As Integer = 1
            Dim Size As String = String.Empty

            For Each row As DataRow In dt.Rows
                Dim allowAdd As Boolean = False

                If dtData.Rows.Count > 0 Then
                    If row.Item("ArticleCode").ToString = dtData.Rows(0).Item(enumDtData.Code).ToString AndAlso row.Item("Color").ToString = dtData.Rows(0).Item(enumDtData.Color).ToString Then
                        allowAdd = False
                    Else
                        allowAdd = True
                    End If
                Else
                    allowAdd = True
                End If

                Dim dr As DataRow

                If allowAdd Then

                    dr = dtData.NewRow

                    dr.Item(enumDtData.LPO) = row.Item("ArticleLPOName")
                    dr.Item(enumDtData.Type) = row.Item("ArticleGenderName")
                    dr.Item(enumDtData.ArticleId) = row.Item("ArticleId")
                    dr.Item(enumDtData.Code) = row.Item("ArticleCode")
                    dr.Item(enumDtData.Name) = row.Item("Item Name")
                    dr.Item(enumDtData.Color) = row.Item("Color")
                    Size = row.Item("Size")
                    dr.Item(enumDtData.Price) = row.Item("Price")
                    dr.Item(enumDtData.Opening) = row.Item("Opening")
                    dr.Item(enumDtData.Rec) = row.Item("Receiving")
                    dr.Item(enumDtData.Prod) = row.Item("Production")
                    dr.Item(enumDtData.Disp) = row.Item("Dispatch")
                    dr.Item(enumDtData.Pur_R) = row.Item("Pur Ret")
                    dr.Item(enumDtData.Sale) = row.Item("Sales")
                    dr.Item(enumDtData.Cust_R) = row.Item("Sales Ret")
                    dr.Item(enumDtData.AddStock) = row.Item("Adj Add")
                    dr.Item(enumDtData.LessStock) = row.Item("Adj Less")
                    dr.Item(enumDtData.Closing) = row.Item("ClosingQty")
                    dr.Item(enumDtData.Value) = row.Item("ClosingValue")
                    dr.Item(enumDtData.Sz1) = row.Item("ClosingQty")
                    dr.Item(enumDtData.Year) = row.Item("Year Sales")
                    dr.Item(enumDtData.DispValue) = row.Item("DispatchValue")
                    dr.Item(enumDtData.RecValue) = row.Item("ReceivingValue")
                    dr.Item(enumDtData.SampleQty) = row.Item("SampleQty")
                    dr.Item(enumDtData.SampleQtyValue) = row.Item("SampleQtyValue")
                    dr.Item(enumDtData.AddStockValue) = row.Item("Add Value")
                    dr.Item(enumDtData.LessStockValue) = row.Item("Less Value")
                    'dr.Item(enumDtData.SalesValue) = row.Item("SalesValue")
                    colNo = 1

                    dtData.Rows.InsertAt(dr, 0)
                Else
                    dr = dtData.Rows(0)
                    dr.Item(enumDtData.Opening) += row.Item("Opening")
                    dr.Item(enumDtData.Rec) += row.Item("Receiving")
                    dr.Item(enumDtData.Prod) += row.Item("Production")
                    dr.Item(enumDtData.Disp) += row.Item("Dispatch")
                    dr.Item(enumDtData.Pur_R) += row.Item("Pur Ret")
                    dr.Item(enumDtData.Sale) += row.Item("Sales")
                    dr.Item(enumDtData.Cust_R) += row.Item("Sales Ret")
                    dr.Item(enumDtData.AddStock) = row.Item("Adj Add")
                    dr.Item(enumDtData.LessStock) = row.Item("Adj Less")
                    dr.Item(enumDtData.Closing) += row.Item("ClosingQty")
                    dr.Item(enumDtData.Value) += row.Item("ClosingValue")
                    dr.Item(enumDtData.Year) += row.Item("Year Sales")
                    dr.Item(enumDtData.Sz1 + colNo) = row.Item("ClosingQty")
                    dr.Item(enumDtData.DispValue) += row.Item("DispatchValue")
                    dr.Item(enumDtData.RecValue) += row.Item("ReceivingValue")
                    dr.Item(enumDtData.SampleQty) = row.Item("SampleQty")
                    dr.Item(enumDtData.SampleQtyValue) = row.Item("SampleQtyValue")
                    dr.Item(enumDtData.AddStockValue) = row.Item("Add Value")
                    dr.Item(enumDtData.LessStockValue) = row.Item("Less Value")
                    'dr.Item(enumDtData.SalesValue) += row.Item("SalesValue")
                    If colNo < 7 Then colNo += 1
                End If
                dr.Item(enumDtData.Size) = Size & "-" & row.Item("Size")
            Next

            GridEX1.DataSource = dtData
            GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns("ArticleId").Visible = False
            GridEX1.AutoSizeColumns()

            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                If col.Index >= enumDtData.Price Then
                    col.FormatString = String.Empty
                    col.TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                    col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                End If
            Next

            GridEX1.TotalRowFormatStyle.BackColor = Color.Navy
            GridEX1.TotalRowFormatStyle.ForeColor = Color.White

            GridEX1.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.True
            GridEX1.GetTotalRow.Cells(0).Text = "Total:"
            GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always

            'Me.UiCtrlFilterGrid1.MyGrid = Me.GridEX1
            'Me.UiCtrlFilterGrid1.ConfigureControl()
            'Me.UiCtrlGridBar1.txtGridTitle.Text = "Stock Statment From: " & Me.dtpFromDate.Value & " To: " & Me.dtpToDate.Value

            CtrlGrdBar1_Load(Nothing, Nothing)

            'dim grp as Janus.Windows.GridEX.gro
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub GridBarUserControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
          
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UiCtrlGridBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
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
    End Sub

    Private Sub dtpToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpToDate.ValueChanged

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub
End Class
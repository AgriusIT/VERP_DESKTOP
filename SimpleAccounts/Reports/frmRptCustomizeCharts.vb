Imports System
Imports System.Data
Imports System.Xml

Public Class frmRptCustomizeCharts
    Dim IsOpenForm As Boolean = False
    Dim _dtColumns As New DataTable
    Dim series2 As New Windows.Forms.DataVisualization.Charting.Series
    Public Enum enmChartType
        Point
        FastPont
        Bubble
        Line
        Spline
        Stepline
        FastLine
        Bar
        StackedBar
        StackedBar100
        Column
        StackedColumn
        StackedColumn100
        Area
        SplineArea
        StackedArea
        StackedArea100
        Pie
        Doughnut
        Stock
        CandleStick
        Range
        SplineRange
        RangeBar
        RangeColumn
        Radar
        Polar
        ErrorBar
        BoxPlot
        Renko
        ThreeLineBreak
        Kagi
        PointAndFigure
        Funnel
        Pyramid
    End Enum
    Public Property _grd As Janus.Windows.GridEX.GridEX
    Public Property _XValueMember As String = String.Empty
    Public Property _YValueMember As String = String.Empty
    Public Property _YValueMember2 As String = String.Empty
    Public Property _TopRecords As Integer = 10
    Private Sub FillChart(Optional Condition As String = "")
        Try
            Dim dt As New DataTable
            dt = CType(_grd.DataSource, DataTable)
            dt.TableName = "Default"
            dt.AcceptChanges()
            Dim dtn As DataTable = dt.Clone
            Dim dv As New DataView
            dv.Table = dt
            dv.Sort = "" & _YValueMember & " DESC"
            Dim dtData As DataTable = dv.ToTable
            dtData.AcceptChanges()
            For i As Integer = 0 To _TopRecords - 1
                dtn.ImportRow(dv.ToTable.Rows(i))
            Next

            Dim dvFiltered As New DataView(dtn)
            dvFiltered.ToTable.AcceptChanges()

            Me.Chart1.Series(0).ChartType = GetSeriesType(Me.cmbChartType.SelectedIndex)
            Me.Chart1.Series(0).XValueMember = _XValueMember
            Me.Chart1.Series(0).YValueMembers = _YValueMember
            series2.Name = "Series2"
            series2.IsValueShownAsLabel = True
            If Not Me.Chart1.Series.Contains(series2) Then
                Me.Chart1.Series.Add(series2)
            Else
                If _YValueMember <> _YValueMember2 Then
                    Me.Chart1.Series(1).ChartType = GetSeriesType(Me.cmbChartType.SelectedIndex)
                    Me.Chart1.Series(1).XValueMember = _XValueMember
                    Me.Chart1.Series(1).YValueMembers = _YValueMember2
                Else
                    Chart1.Series.RemoveAt(1)
                End If
            End If


            Me.Chart1.DataSource = dvFiltered.ToTable
            Me.Chart1.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmRptCustomizeCharts_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim lbl As New Label
        lbl.Text = "Loading please wait...."
        lbl.Dock = DockStyle.Fill
        lbl.Visible = True
        Me.GroupBox1.Controls.Add(lbl)
        lbl.BringToFront()
        Application.DoEvents()
        Try
            FillCombo("Category")
            FillCombo("Value1")
            FillCombo("Value2")
            Me.cmbCategory.SelectedValue = _XValueMember
            Me.cmbValue1.SelectedValue = _YValueMember
            Me.cmbValue2.SelectedValue = _YValueMember
            _YValueMember2 = _YValueMember
            Me.nudTopRecord.Value = _TopRecords
            FillChart()
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            frmRptCustomizeCharts_Shown(Me, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChartType.SelectedIndexChanged
        Try
            If IsOpenForm = False Then Exit Sub
            Dim cmb As ComboBox = sender
            GetSeriesType(cmb.SelectedIndex)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetSeriesType(charindex As Integer) As DataVisualization.Charting.SeriesChartType
        Try
            Select Case charindex
                Case 0
                    Return DataVisualization.Charting.SeriesChartType.Point
                Case 1
                    Return DataVisualization.Charting.SeriesChartType.FastPoint
                Case 2
                    Return DataVisualization.Charting.SeriesChartType.Bubble
                Case 3
                    Return DataVisualization.Charting.SeriesChartType.Line
                Case 4
                    Return DataVisualization.Charting.SeriesChartType.Spline
                Case 5
                    Return DataVisualization.Charting.SeriesChartType.StepLine
                Case 6
                    Return DataVisualization.Charting.SeriesChartType.FastPoint
                Case 7
                    Return DataVisualization.Charting.SeriesChartType.Bar
                Case 8
                    Return DataVisualization.Charting.SeriesChartType.StackedBar
                Case 9
                    Return DataVisualization.Charting.SeriesChartType.StackedBar100
                Case 10
                    Return DataVisualization.Charting.SeriesChartType.Column
                Case 11
                    Return DataVisualization.Charting.SeriesChartType.StackedColumn
                Case 12
                    Return DataVisualization.Charting.SeriesChartType.StackedColumn100
                Case 13
                    Return DataVisualization.Charting.SeriesChartType.Area
                Case 14
                    Return DataVisualization.Charting.SeriesChartType.SplineArea
                Case 15
                    Return DataVisualization.Charting.SeriesChartType.StackedArea
                Case 16
                    Return DataVisualization.Charting.SeriesChartType.StackedArea100
                Case 17
                    Return DataVisualization.Charting.SeriesChartType.Pie
                Case 18
                    Return DataVisualization.Charting.SeriesChartType.Doughnut
                Case 19
                    Return DataVisualization.Charting.SeriesChartType.Stock
                Case 20
                    Return DataVisualization.Charting.SeriesChartType.Candlestick
                Case 21
                    Return DataVisualization.Charting.SeriesChartType.Range
                Case 22
                    Return DataVisualization.Charting.SeriesChartType.SplineRange
                Case 23
                    Return DataVisualization.Charting.SeriesChartType.RangeBar
                Case 24
                    Return DataVisualization.Charting.SeriesChartType.RangeColumn
                Case 25
                    Return DataVisualization.Charting.SeriesChartType.Radar
                Case 26
                    Return DataVisualization.Charting.SeriesChartType.Polar
                Case 27
                    Return DataVisualization.Charting.SeriesChartType.ErrorBar
                Case 28
                    Return DataVisualization.Charting.SeriesChartType.BoxPlot
                Case 29
                    Return DataVisualization.Charting.SeriesChartType.Renko
                Case 30
                    Return DataVisualization.Charting.SeriesChartType.ThreeLineBreak
                Case 31
                    Return DataVisualization.Charting.SeriesChartType.Kagi
                Case 32
                    Return DataVisualization.Charting.SeriesChartType.PointAndFigure
                Case 33
                    Return DataVisualization.Charting.SeriesChartType.Funnel
                Case 34
                    Return DataVisualization.Charting.SeriesChartType.Pyramid
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub FillColumns()
        Try
            _dtColumns = New DataTable
            _dtColumns.Columns.Add("DataMember", GetType(System.String))
            _dtColumns.Columns.Add("DisplayMember", GetType(System.String))
            If _grd IsNot Nothing Then
                If _grd.RootTable.Columns.Count > 1 Then
                    For Each col As Janus.Windows.GridEX.GridEXColumn In _grd.RootTable.Columns
                        Dim dr As DataRow = _dtColumns.NewRow
                        dr(0) = col.DataMember
                        dr(1) = col.Caption.ToString
                        _dtColumns.Rows.Add(dr)
                        _dtColumns.AcceptChanges()
                    Next
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Dim lbl As New Label
        lbl.Text = "Loading please wait...."
        lbl.Dock = DockStyle.Fill
        lbl.Visible = True
        Me.GroupBox1.Controls.Add(lbl)
        lbl.BringToFront()
        Application.DoEvents()
        Try
            _XValueMember = Me.cmbCategory.SelectedValue
            _YValueMember = Me.cmbValue1.SelectedValue
            _YValueMember2 = Me.cmbValue2.SelectedValue
            _TopRecords = Me.nudTopRecord.Value
            FillChart()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            lbl.Visible = False
        End Try
    End Sub
    Private Sub FillCombo(Optional Condition As String = "")
        Try

            FillColumns()
            If Condition = "Category" Then
                Me.cmbCategory.ValueMember = "DataMember"
                Me.cmbCategory.DisplayMember = "DisplayMember"
                Me.cmbCategory.DataSource = _dtColumns
            ElseIf Condition = "Value1" Then
                Me.cmbValue1.ValueMember = "DataMember"
                Me.cmbValue1.DisplayMember = "DisplayMember"
                Me.cmbValue1.DataSource = _dtColumns
            ElseIf Condition = "Value2" Then
                Me.cmbValue2.ValueMember = "DataMember"
                Me.cmbValue2.DisplayMember = "DisplayMember"
                Me.cmbValue2.DataSource = _dtColumns
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtTopRecord_KeyPress(sender As Object, e As KeyPressEventArgs)
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
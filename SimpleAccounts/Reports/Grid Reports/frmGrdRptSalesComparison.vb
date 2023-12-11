Public Class frmGrdRptSalesComparison
    Dim dtData As New DataTable

    Private Sub frmGrdRptSalesComparison_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                If e.KeyCode = Keys.Cancel Then
                    Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdRptSalesComparison_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.txtYearFrom.Text = Now.AddYears(-3).Year
            Me.txtYearTo.Text = Now.Year
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Public Sub FillGrid(Optional ByVal Condition As String = "")
        Try

            If dtData.Columns.Count > 0 Then
                dtData.Columns.Clear()
                dtData.Clear()
            End If

            ''Create Data Column
            dtData.Columns.Add("Year", GetType(System.Int32))
            dtData.Columns.Add("Jan", GetType(System.Double))
            dtData.Columns.Add("Feb", GetType(System.Double))
            dtData.Columns.Add("Mar", GetType(System.Double))
            dtData.Columns.Add("Apr", GetType(System.Double))
            dtData.Columns.Add("May", GetType(System.Double))
            dtData.Columns.Add("Jun", GetType(System.Double))
            dtData.Columns.Add("Jul", GetType(System.Double))
            dtData.Columns.Add("Aug", GetType(System.Double))
            dtData.Columns.Add("Sep", GetType(System.Double))
            dtData.Columns.Add("Oct", GetType(System.Double))
            dtData.Columns.Add("Nov", GetType(System.Double))
            dtData.Columns.Add("Dec", GetType(System.Double))
            'End Data Column

            'Set Count Years
            Dim intYearCount As Int32 = Val(Me.txtYearTo.Text) - Val(Me.txtYearFrom.Text)
            'End Count Years
            Dim dr As DataRow 'Create Data Row
            Dim intYearList As New List(Of Integer) 'Create Year Array
            Dim nIntYear As Integer = Val(Me.txtYearFrom.Text)
            If intYearCount > 0 Then 'Check Year Count
                For i As Integer = 0 To intYearCount 'Start Loop
                    'Add Year in intYearList
                    If i = 0 Then
                        intYearList.Add(nIntYear)
                    Else
                        nIntYear += 1
                        intYearList.Add(nIntYear)
                    End If
                Next
                For Each intYear As Integer In intYearList 'Start Loop Check 
                    dr = dtData.NewRow
                    dr(0) = intYear
                    dtData.Rows.Add(dr)
                Next
            Else
                dr = dtData.NewRow
                dr(0) = Val(Me.txtYearFrom.Text)
                dtData.Rows.Add(dr)
            End If
            dtData.AcceptChanges()



            Dim Query As String = String.Empty
            Query = "SP_SalesComparisonYearWise " & Val(Me.txtYearFrom.Text) & ", " & Val(Me.txtYearTo.Text) & ""
            Dim dt As New DataTable
            dt = GetDataTable(Query)
            Dim drData() As DataRow

            For Each r As DataRow In dtData.Rows
                drData = dt.Select("Year=" & r(0) & "")
                For Each drFound As DataRow In drData
                    r.BeginEdit()
                    r(dtData.Columns.IndexOf(GetMonthName(drFound(3)).ToString)) = drFound(0)
                    r.EndEdit()
                Next
            Next

            dtData.AcceptChanges()
            Me.grd.DataSource = dtData
            Me.grd.RetrieveStructure()
            Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                Me.grd.RootTable.Columns(c).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grd.RootTable.Columns(c).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                If c > 0 Then
                    Me.grd.RootTable.Columns(c).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                End If
            Next
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetMonthName(ByVal Month As Integer) As String
        Try
            Dim MonthName As String = String.Empty
            Select Case Month
                Case 1
                    MonthName = "Jan"
                Case 2
                    MonthName = "Feb"
                Case 3
                    MonthName = "Mar"
                Case 4
                    MonthName = "Apr"
                Case 5
                    MonthName = "May"
                Case 6
                    MonthName = "Jun"
                Case 7
                    MonthName = "Jul"
                Case 8
                    MonthName = "Aug"
                Case 9
                    MonthName = "Sep"
                Case 10
                    MonthName = "Oct"
                Case 11
                    MonthName = "Nov"
                Case 12
                    MonthName = "Dec"
            End Select
            Return MonthName
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try

            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = "Sales Comparison Year Wise"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class

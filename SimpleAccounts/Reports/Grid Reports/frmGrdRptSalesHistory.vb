Public Class frmGrdRptSalesHistory

    Private _LastYearMonthFrom As DateTime
    Private _LastYearMonthTo As DateTime
    Private _CurrentYearMonthFrom As DateTime
    Private _CurrentYearMonthTo As DateTime
    Private _DtData As New DataTable
    Private _DtTargetData As New DataTable
    Private _SelectMonth As String = String.Empty
    Private _SelectYear As Integer
    Dim strMonth As String = String.Empty
    Private Sub cmbMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMonth.SelectedIndexChanged
        Try
            _SelectYear = Me.txtYear.Text
            _SelectMonth = Me.cmbMonth.Text
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Customers Wise Growth Sales " & vbCrLf & "Year:" & Me.txtYear.Text & " Month: " & Me.cmbMonth.Text & " "
            Select Case Me.cmbMonth.Text
                Case "January"
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 1)
                    _LastYearMonthTo = New Date(_SelectYear - 1, 1, 31)
                    _CurrentYearMonthFrom = New Date(_SelectYear, 1, 1)
                    _CurrentYearMonthTo = New Date(_SelectYear, 1, 31)
                Case "February"
                    '_LastYearMonthFrom = New Date(_SelectYear - 1, 2, 1)
                    'If Date.IsLeapYear(_SelectYear - 1) = True Then
                    '    _LastYearMonthTo = New Date(_SelectYear - 1, 2, 29)
                    'Else
                    '    _LastYearMonthTo = New Date(_SelectYear - 1, 2, 28)
                    'End If
                    '_CurrentYearMonthFrom = New Date(_SelectYear, 2, 1)
                    'If Date.IsLeapYear(Date.Now.Year) = True Then
                    '    _CurrentYearMonthTo = New Date(_SelectYear, 2, 29)
                    'Else
                    '    _CurrentYearMonthTo = New Date(_SelectYear, 2, 28)
                    'End If
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 1)
                    If Date.IsLeapYear(_SelectYear - 1) = True Then
                        _LastYearMonthTo = New Date(_SelectYear - 1, 2, 29)
                    Else
                        _LastYearMonthTo = New Date(_SelectYear - 1, 2, 28)
                    End If
                    _CurrentYearMonthFrom = New Date(_SelectYear, 2, 1)
                    If Date.IsLeapYear(Me.txtYear.Text) = True Then
                        _CurrentYearMonthTo = New Date(_SelectYear, 2, 29)
                    Else
                        _CurrentYearMonthTo = New Date(_SelectYear, 2, 28)
                    End If
                Case "March"
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 1)
                    _LastYearMonthTo = New Date(_SelectYear - 1, 3, 31)
                    _CurrentYearMonthFrom = New Date(_SelectYear, 1, 1)
                    _CurrentYearMonthTo = New Date(_SelectYear, 3, 31)
                Case "April"
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 31)
                    _LastYearMonthTo = New Date(_SelectYear - 1, 4, 30)
                    _CurrentYearMonthFrom = New Date(_SelectYear, 1, 31)
                    _CurrentYearMonthTo = New Date(_SelectYear, 4, 30)
                Case "May"
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 31)
                    _LastYearMonthTo = New Date(_SelectYear - 1, 5, 31)
                    _CurrentYearMonthFrom = New Date(_SelectYear, 1, 31)
                    _CurrentYearMonthTo = New Date(_SelectYear, 5, 31)
                Case "June"
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 31)
                    _LastYearMonthTo = New Date(_SelectYear - 1, 6, 30)
                    _CurrentYearMonthFrom = New Date(_SelectYear, 1, 31)
                    _CurrentYearMonthTo = New Date(_SelectYear, 6, 30)
                Case "July"
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 31)
                    _LastYearMonthTo = New Date(_SelectYear - 1, 7, 31)
                    _CurrentYearMonthFrom = New Date(_SelectYear, 1, 31)
                    _CurrentYearMonthTo = New Date(_SelectYear, 7, 31)
                Case "August"
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 31)
                    _LastYearMonthTo = New Date(_SelectYear - 1, 8, 31)
                    _CurrentYearMonthFrom = New Date(_SelectYear, 1, 31)
                    _CurrentYearMonthTo = New Date(_SelectYear, 8, 31)
                Case "September"
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 31)
                    _LastYearMonthTo = New Date(_SelectYear - 1, 9, 30)
                    _CurrentYearMonthFrom = New Date(_SelectYear, 1, 31)
                    _CurrentYearMonthTo = New Date(_SelectYear, 9, 30)
                Case "October"
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 31)
                    _LastYearMonthTo = New Date(_SelectYear - 1, 10, 31)
                    _CurrentYearMonthFrom = New Date(_SelectYear, 1, 31)
                    _CurrentYearMonthTo = New Date(_SelectYear, 10, 31)
                Case "November"
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 31)
                    _LastYearMonthTo = New Date(_SelectYear - 1, 11, 30)
                    _CurrentYearMonthFrom = New Date(_SelectYear, 1, 31)
                    _CurrentYearMonthTo = New Date(_SelectYear, 11, 30)
                Case "December"
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 31)
                    _LastYearMonthTo = New Date(_SelectYear - 1, 12, 31)
                    _CurrentYearMonthFrom = New Date(_SelectYear, 1, 31)
                    _CurrentYearMonthTo = New Date(_SelectYear, 12, 31)
                Case "Year"
                    _LastYearMonthFrom = New Date(_SelectYear - 1, 1, 1)
                    _LastYearMonthTo = New Date(_SelectYear - 1, 12, 31)
                    _CurrentYearMonthFrom = New Date(_SelectYear, 12, 1)
                    _CurrentYearMonthTo = New Date(_SelectYear, 12, 31)
            End Select

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            _DtData = GetDataTable("SP_SalesHistoryByCustomer '" & _LastYearMonthFrom.ToString("yyyy-M-d 00:00:00") & "', '" & _LastYearMonthTo.ToString("yyyy-M-d 23:59:59") & "', '" & _CurrentYearMonthFrom.ToString("yyyy-M-d 00:00:00") & "', '" & _CurrentYearMonthTo.ToString("yyyy-M-d 23:59:59") & "'")
            If _DtTargetData IsNot Nothing Then
                Dim dr() As DataRow
                For Each row As DataRow In _DtData.Rows
                    dr = _DtTargetData.Select("CustomerId=" & row("coa_detail_id") & "")
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            row.BeginEdit()
                            row("Target") = drFound(1)
                            row.EndEdit()
                        Next
                    End If
                Next
            End If
            _DtData.Columns("Growth").Expression = "[CY Sales]-[LY Sales]"
            '---------------------------------------- Achived % Loop --------------- 
            For Each Ach As DataRow In _DtData.Rows
                Ach.BeginEdit()
                If Not Ach("Target") = 0 AndAlso Not Ach("CY Sales") = 0 Then
                    Ach("Achieved %") = (Ach("CY Sales") / Ach("Target"))
                End If
                Ach.EndEdit()
            Next
            '--------------------------------------- Growth % Loop --------------------
            For Each Grt As DataRow In _DtData.Rows
                Grt.BeginEdit()
                If Not Grt("Growth") = 0 AndAlso Not Grt("LY Sales") = 0 Then
                    Grt("Growth %") = (Grt("Growth") / Grt("LY Sales") * 100)
                End If
                Grt.EndEdit()
            Next
            '--------------------------------------------- Market Returns % Loop ----------------------
            For Each Mrk As DataRow In _DtData.Rows
                Mrk.BeginEdit()
                If Not Mrk("MarketReturns") = 0 AndAlso Not Mrk("CY Sales") = 0 Then
                    Mrk("MarketReturns %") = (Mrk("MarketReturns") / Mrk("CY Sales") * 100)
                End If
                Mrk.EndEdit()
            Next

            Dim AchievedPoints As Double = 0D
            Dim GrowthPoints As Double = 0D
            Dim MarketReturnsPoints As Double = 0D

            For Each point As DataRow In _DtData.Rows
                point.BeginEdit()

                If point("Achieved %") > 80 Then
                    AchievedPoints = 12
                ElseIf point("Achieved %") > 90 Then
                    AchievedPoints = 23
                ElseIf point("Achieved %") > 100 Then
                    AchievedPoints = 30
                Else
                    AchievedPoints = 0
                End If

                If point("Growth %") > 10 Then
                    GrowthPoints = 2
                ElseIf point("Growth %") > 15 Then
                    GrowthPoints = 3
                ElseIf point("Growth %") > 20 Then
                    GrowthPoints = 5
                Else
                    GrowthPoints = 0
                End If

                If point("MarketReturns %") > 2 Then
                    MarketReturnsPoints = -10
                ElseIf point("MarketReturns %") > 3 Then
                    MarketReturnsPoints = -30
                ElseIf point("MarketReturns %") > 5 Then
                    MarketReturnsPoints = -35
                Else
                    MarketReturnsPoints = 0
                End If
                point("Points") = AchievedPoints + GrowthPoints + MarketReturnsPoints
                point.EndEdit()
            Next
            _DtData.AcceptChanges()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptSalesHistory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmGrdRptSalesHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.txtYear.Text = Date.Now.Year
            'FillDropDown(Me.txtYear, "Select DISTINCT ISNULL(Target_Year,2012) as Target_Year, ISNULL(Target_Year,2012) as Target_Year From tblDefCustomerTarget", False)
            Me.cmbMonth.Text = GetMonthName(Date.Now.Month)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdRptSalesHistory_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            strMonth = String.Empty
            For i As Integer = 0 To Me.cmbMonth.SelectedIndex
                If strMonth.Length > 1 Then
                    strMonth = strMonth & "+" & Me.cmbMonth.Items(i)
                Else
                    strMonth = Me.cmbMonth.Items(i)
                End If
            Next


            If BackgroundWorker2.IsBusy Then Exit Sub
            BackgroundWorker2.RunWorkerAsync()
            Do While BackgroundWorker2.IsBusy
                Application.DoEvents()
            Loop 'get Target Data ................


            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop 'get Sales History Data .........

            FillGrid() ' Fill Grid .............

        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading data: " & ex.Message)
        End Try
    End Sub
    Sub FillGrid()
        Try

            Me.GridEX1.DataSource = Nothing
            If _DtData IsNot Nothing Then Me.GridEX1.DataSource = _DtData
            GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns(0).Visible = False
            'Me.GridEX1.RootTable.Columns("SMarketReturns").Visible = False
            ApplyGridSettings()
            Me.GridEX1.AutoSizeColumns()
            Me.CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetMonthName(ByVal MonthNo As Int16) As String
        Try
            Select Case MonthNo
                Case 1
                    _SelectMonth = "January"
                Case 2
                    _SelectMonth = "February"
                Case 3
                    _SelectMonth = "March"
                Case 4
                    _SelectMonth = "April"
                Case 5
                    _SelectMonth = "May"
                Case 6
                    _SelectMonth = "June"
                Case 7
                    _SelectMonth = "July"
                Case 8
                    _SelectMonth = "August"
                Case 9
                    _SelectMonth = "September"
                Case 10
                    _SelectMonth = "October"
                Case 11
                    _SelectMonth = "November"
                Case 12
                    _SelectMonth = "December"
            End Select

            If _SelectMonth.Length > 1 Then
                Return _SelectMonth
            Else
                Return Date.Now.ToString("MMM")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnGenerate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        Try

            If Not Me.txtYear.Text.Length = 4 AndAlso Not CInt(Me.txtYear.Text) Then
                ShowErrorMessage("Year is not valid")
                Me.txtYear.Focus()
                Exit Sub
            End If

            strMonth = String.Empty
            For i As Integer = 0 To Me.cmbMonth.SelectedIndex
                If strMonth.Length > 1 Then
                    strMonth = strMonth & "+" & Me.cmbMonth.Items(i)
                Else
                    strMonth = Me.cmbMonth.Items(i)
                End If
            Next

            If BackgroundWorker2.IsBusy Then Exit Sub
            BackgroundWorker2.RunWorkerAsync()
            Do While BackgroundWorker2.IsBusy
                Application.DoEvents()
            Loop 'get Target Data ................



            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop 'get Sales history Data ......................

            FillGrid() ' Fill Grid .............
        Catch ex As Exception
            ShowErrorMessage("Error occured while loading data:  " & ex.Message)
        End Try
    End Sub
    'Private Sub txtYear_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        cmbMonth_SelectedIndexChanged(Nothing, Nothing)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Sub GetTarget()
        Try
            _DtTargetData = GetDataTable("Select CustomerId, " & strMonth & "  as Target From tblDefCustomerTarget WHERE Target_Year='" & _SelectYear & "'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            GetTarget()
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading target data: " & ex.Message)
        End Try
    End Sub
    Private Sub ApplyGridSettings()
        Try
            Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
            For i As Int16 = Me.GridEX1.RootTable.Columns("LY Sales").Index To Me.GridEX1.RootTable.Columns.Count - 1
                Me.GridEX1.RootTable.Columns(i).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.GridEX1.RootTable.Columns(i).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.GridEX1.RootTable.Columns(i).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Next
            Me.GridEX1.RootTable.Columns("LY Sales").CellStyle.BackColor = Color.Ivory
            Me.GridEX1.RootTable.Columns("Target").CellStyle.BackColor = Color.Snow
            Me.GridEX1.RootTable.Columns("CY Sales").CellStyle.BackColor = Color.LightCyan
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try

            Me.btnGenerate_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage("Error occured while loading data:  " & ex.Message)
        End Try
    End Sub

    Private Sub txtYear_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtYear.TextChanged
       
    End Sub

    Private Sub txtYear_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtYear.Validated
        Try
            If Me.txtYear.Text = String.Empty Then Exit Sub
            If Not Me.txtYear.Text.Length = 4 AndAlso Not CInt(Me.txtYear.Text) Then Exit Sub
            cmbMonth_SelectedIndexChanged(Nothing, Nothing)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Dispose()
                fs.Close()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Customers Wise Growth Sales " & vbCrLf & "Year:" & Me.txtYear.Text & " Month: " & Me.cmbMonth.Text & " "

        Catch ex As Exception

        End Try
    End Sub
End Class
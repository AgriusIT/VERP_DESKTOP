Public Class frmRptMonthlyPurchaseSummary

    Dim FirstMonth_FromDate As DateTime
    Dim FirstMonth_ToDate As DateTime
    Dim SecondMonth_FromDate As DateTime
    Dim SecondMonth_ToDate As DateTime


    Private Sub frmRptMonthlyPurchaseSummary_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            FillUltraDropDown(Me.cmbAccount, "Select coa_detail_id, detail_title as [Account Title],detail_code as [Code], sub_sub_title as [Account Head] From vwCOAdetail WHERE account_type In('Vendor') AND detail_title <> ''")
            Me.cmbAccount.Rows(0).Activate()
            Me.cmbAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True

            Me.txtYear.Text = Date.Now.Year

            Me.cmbMonth.ValueMember = "Month"
            Me.cmbMonth.DisplayMember = "Month_Name"
            Me.cmbMonth.DataSource = GetMonths()

            Me.cmbCompareMonth.ValueMember = "Month"
            Me.cmbCompareMonth.DisplayMember = "Month_Name"
            Me.cmbCompareMonth.DataSource = GetMonths()


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try

            FirstMonth_FromDate = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.SelectedValue & "-" & "1")
            FirstMonth_ToDate = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbMonth.SelectedValue & "-" & GetDayOfEndMonth(Me.cmbMonth.SelectedValue))

            SecondMonth_FromDate = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbCompareMonth.SelectedValue & "-" & "1")
            SecondMonth_ToDate = CDate(Val(Me.txtYear.Text) & "-" & Me.cmbCompareMonth.SelectedValue & "-" & GetDayOfEndMonth(Me.cmbCompareMonth.SelectedValue))


            AddRptParam("@1st_Month_FromDate", FirstMonth_FromDate)
            AddRptParam("@1st_Month_ToDate", FirstMonth_ToDate)
            AddRptParam("@1st_MonthName", Me.cmbMonth.Text)
            AddRptParam("@2nd_Month_FromDate", SecondMonth_FromDate)
            AddRptParam("@2nd_Month_ToDate", SecondMonth_ToDate)
            AddRptParam("@2nd_MonthName", Me.cmbCompareMonth.Text)
            ShowReport("rptMonthlyPurchaseSummary")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function GetDayOfEndMonth(ByVal Month As Integer) As Integer
        Try
            Select Case Month
                Case 1
                    Return 31
                Case 2
                    If Date.IsLeapYear(Me.txtYear.Text) = False Then
                        Return 28
                    Else
                        Return 29
                    End If
                Case 3
                    Return 31
                Case 4
                    Return 30
                Case 5
                    Return 31
                Case 6
                    Return 30
                Case 7
                    Return 31
                Case 8
                    Return 31
                Case 9
                    Return 30
                Case 10
                    Return 31
                Case 11
                    Return 30
                Case 12
                    Return 31
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
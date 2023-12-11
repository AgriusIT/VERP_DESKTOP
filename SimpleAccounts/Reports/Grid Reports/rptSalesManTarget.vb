Public Class rptSalesManTarget

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim Str As String = "SELECT Employee_ID, Employee_Name FROM tblDefEmployee WHERE SalePerson <> 0 And (Active='1') order by Employee_Name"
        FillDropDown(cmbSalesMan, Str)
        Reset()

    End Sub
    Private Sub Reset()
        dtpMonth.Value = Format(Now, "MMM/yyyy/01")
        cmbSalesMan.SelectedIndex = 0
    End Sub
    Public Function GetLastDayOfMonth(ByVal pDate As Date)
        GetLastDayOfMonth = Microsoft.VisualBasic.Day(DateSerial(Year(pDate), Month(pDate) + 1, 0))
    End Function
    Private Function Criteria() As String
        Dim strCriteria As String
        Dim ToDate As Date
        ToDate = Format(dtpMonth.Value, "yyyy/MM/" & GetLastDayOfMonth(dtpMonth.Value))
        Dim DC As String = " {V_Rpt_SalesMan_Target.SalesDate} in DateTime (" & dtpMonth.Value.Year & ", " & dtpMonth.Value.Month & "," & dtpMonth.Value.Day & ",00,00,00 ) to DateTime ( " & dtpMonth.Value.Year & ", " & dtpMonth.Value.Month & "," & Date.DaysInMonth(dtpMonth.Value.Year, dtpMonth.Value.Month) & ", 23,59,59)"

        strCriteria = " 1 = 1"
        strCriteria = strCriteria & " And {V_Rpt_SalesMan_Target.Employee_ID} = " & Val(cmbSalesMan.SelectedValue)
        'strCriteria = " And {V_Rpt_SalesMan_Target.SalesDate} >= #" & dtpMonth.Value.Date & "# AND {V_Rpt_SalesMan_Target.SalesDate} <= #" & ToDate & "#"
        strCriteria = strCriteria & " And " & DC
        Return strCriteria

    End Function

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        ShowReport("SalesManTarget", Criteria)

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class

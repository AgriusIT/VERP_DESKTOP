Public Class IssuanceHistoryByProduction
    Public ArticleId As Integer
    Public CostCenterId As Integer
    Public startDate As DateTime
    Public endDate As DateTime

    Private Sub grd_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow

    End Sub
    Private Sub IssuanceHistoryByProduction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If ArticleId = 0 Then Exit Sub
            Dim dt As DataTable
            dt = GetDataTable("sp_storeissuancehistorybyproduction '" & ArticleId & "', '" & CostCenterId & "', '" & startDate.Date.ToString("dd/MMM/yyyy") & "', '" & endDate.Date.ToString("dd/MMM/yyyy") & "'")
            dt.AcceptChanges()
            Me.grd.DataSource = Nothing
            grd.DataSource = dt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = "Store Issuence History From " & startDate.Date.ToString("dd-MM-yyyy") & " To " & endDate.Date.ToString("dd-MM-yyyy") & " "
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
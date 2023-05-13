Public Class frmDSRStatementNew

    Private Sub frmDSRStatementNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.DateTimePicker1.Value = Date.Now
            FillDropDown(Me.cmbSalesman, "Select coa_detail_id, detail_title,detail_code,CityName, TerritoryName From vwCOADetail WHERE Account_Type='Customer'")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            AddRptParam("@DsrId", Me.cmbSalesman.SelectedValue)
            AddRptParam("@DsrDate", Me.DateTimePicker1.Value)
            AddRptParam("@detail_title", CType(Me.cmbSalesman.SelectedItem, DataRowView).Item("detail_title").ToString)
            AddRptParam("@detail_code", CType(Me.cmbSalesman.SelectedItem, DataRowView).Item("detail_code").ToString)
            AddRptParam("@CityName", CType(Me.cmbSalesman.SelectedItem, DataRowView).Item("CityName").ToString)
            AddRptParam("@TerritoryName", CType(Me.cmbSalesman.SelectedItem, DataRowView).Item("TerritoryName").ToString)
            ShowReport("rptRptDSRStatementNew")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
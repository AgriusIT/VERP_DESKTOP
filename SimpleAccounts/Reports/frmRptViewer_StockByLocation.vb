Public Class frmRptViewer_StockByLocation

    Private Sub frmRptViewer_StockByLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CrystalReportViewer1.ReportSource = My.Forms.rptStockByLocation.ObjCrystal
    End Sub
End Class
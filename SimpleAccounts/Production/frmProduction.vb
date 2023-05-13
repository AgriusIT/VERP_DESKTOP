Public Class frmProduction
    Dim ControlName As New Form
    Dim enm As EnumForms = EnumForms.Non

    
    Private Sub UiButton1_Click(sender As Object, e As EventArgs) Handles UiButton1.Click
        Try
            frmMain.LoadControl("frmCmfa")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton5_Click(sender As Object, e As EventArgs) Handles UiButton5.Click
        Try
            frmMain.LoadControl("frmGrdRptCMFASummary")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton9_Click(sender As Object, e As EventArgs) Handles UiButton9.Click
        Try
            frmMain.LoadControl("frmTicketTracking")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton17_Click(sender As Object, e As EventArgs) Handles UiButton17.Click
        Try
            frmMain.LoadControl("Stock Receiving")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub UiButton13_Click(sender As Object, e As EventArgs) Handles UiButton13.Click
        Try
            frmMain.LoadControl("Stock Dispatch")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton21_Click(sender As Object, e As EventArgs) Handles UiButton21.Click
        Try
            frmMain.LoadControl("frmCmfa")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton25_Click(sender As Object, e As EventArgs) Handles UiButton25.Click
        Try
            frmMain.LoadControl("frmGrdRptCMFASummary")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton29_Click(sender As Object, e As EventArgs) Handles UiButton29.Click
        Try
            frmMain.LoadControl("frmGrdRptCMFAOfSummaries")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton33_Click(sender As Object, e As EventArgs) Handles UiButton33.Click
        Try
            frmMain.LoadControl("frmRptCMFADetail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton37_Click(sender As Object, e As EventArgs) Handles UiButton37.Click
        Try
            frmMain.LoadControl("frmCMFAAll")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton2_Click(sender As Object, e As EventArgs) Handles UiButton2.Click
        Try
            frmMain.LoadControl("frmGrdRptCMFAllRecords")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton6_Click(sender As Object, e As EventArgs) Handles UiButton6.Click
        Try
            frmMain.LoadControl("defineCostSheet")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton10_Click(sender As Object, e As EventArgs) Handles UiButton10.Click
        Try
            frmMain.LoadControl("frmProductionPlanStatus")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton14_Click(sender As Object, e As EventArgs) Handles UiButton14.Click
        Try
            frmMain.LoadControl("frmGrdRptProductionLevel")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton18_Click(sender As Object, e As EventArgs) Handles UiButton18.Click
        Try
            frmMain.LoadControl("frmrptGrdProducedItems")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton22_Click(sender As Object, e As EventArgs) Handles UiButton22.Click
        Try
            frmMain.LoadControl("rptProductionSummary")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton26_Click(sender As Object, e As EventArgs) Handles UiButton26.Click
        Try
            frmMain.LoadControl("frmGrdRptProductionComparison")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton30_Click(sender As Object, e As EventArgs) Handles UiButton30.Click
        Try
            frmMain.LoadControl("StoreSummary")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton34_Click(sender As Object, e As EventArgs) Handles UiButton34.Click
        Try
            frmMain.LoadControl("StoreDetail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton38_Click(sender As Object, e As EventArgs) Handles UiButton38.Click
        Try
            frmMain.LoadControl("frmImport")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton3_Click(sender As Object, e As EventArgs) Handles UiButton3.Click
        Try
            frmMain.LoadControl("rptLCDetail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton7_Click(sender As Object, e As EventArgs) Handles UiButton7.Click
        Try
            frmMain.LoadControl("frmRptGrdStockInOutDetail")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton11_Click(sender As Object, e As EventArgs) Handles UiButton11.Click
        Try
            frmMain.LoadControl("ProjectWiseStockLedger")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton15_Click(sender As Object, e As EventArgs) Handles UiButton15.Click
        Try
            frmMain.LoadControl("rptProjectWiseLedger")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton19_Click(sender As Object, e As EventArgs) Handles UiButton19.Click
        Try
            frmMain.LoadControl("rptProduction")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton23_Click(sender As Object, e As EventArgs) Handles UiButton23.Click
        Try
            frmMain.LoadControl("frmRejectDispatchedStock")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UiButton27_Click(sender As Object, e As EventArgs) Handles UiButton27.Click
        Try
            frmMain.LoadControl("frmRptPlansStatus")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
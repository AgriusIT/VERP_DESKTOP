Public Class frmCMFAAll

    Private Sub frmCMFAAll_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.dtpFromDate.Value = Date.Now.AddDays(-(Date.Now.Day - 1))
            Me.dtpToDate.Value = Date.Now
            Me.rbtApprovedAll.Checked = True
            Me.rbtSalesAll.Checked = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            Dim strFilter As String = String.Empty
            strFilter = "{SP_CMFAAll;1.DocNo} <> ""''"""
            If Me.rbtApproved.Checked = True Then
                strFilter += " AND {SP_CMFAAll;1.Approved}=True"
            End If
            If Me.rbtUnApproved.Checked = True Then
                strFilter += " AND {SP_CMFAAll;1.Approved}=False"
            End If
            If Me.GroupBox2.Visible = True Then
                If Me.rbtWithSale.Checked = True Then
                    strFilter += " AND {SP_CMFAAll;1.SalesNo} <> ""''"""
                End If
                If Me.rbtWithoutSales.Checked = True Then
                    strFilter += " AND {SP_CMFAAll;1.SalesQty} = 0"
                End If
            End If
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
            ShowReport("rptCMFAAll", strFilter.ToString)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtUnApproved_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtUnApproved.CheckedChanged
        Try
            If Me.rbtUnApproved.Checked = True Then
                Me.GroupBox2.Enabled = False
            Else
                Me.GroupBox2.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
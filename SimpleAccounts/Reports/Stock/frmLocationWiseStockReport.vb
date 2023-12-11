''TASK TFS1199 Muhammad Ameen on 15-08-2017. This new report has been created to show location wise opening stock, received stock, issued stock, produced stock and closing stock with the criteria of location and date.
Public Class frmLocationWiseStockReport
    ''' <summary>
    ''' TASK TFS1199
    ''' </summary>
    ''' <remarks> This method contains query to fill location combo. </remarks>
    Private Sub FillCombo()
        Dim strng As String = ""
        Try
            strng = "If exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Name,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Name, IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order"
            FillDropDown(cmbLocation, strng)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmLocationWiseStockReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim ID As Integer = 0
        Try
            ID = Me.cmbLocation.SelectedValue
            FillCombo()
            Me.cmbLocation.SelectedValue = ID
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim dt As DataTable
        Try
            If Not Me.cmbLocation.SelectedValue > 0 Then
                msg_Error("Please select a location")
                Me.cmbLocation.Focus()
                Exit Sub
            End If
            'dt = GetDataTable("Exec sp_LocationWiseReceivingAndIssuing '" & dtpFrom.Value & "', '" & dtpTo.Value & "', " & Me.cmbLocation.SelectedValue & "")
            ''Commented Agaisnt TFS
            'dt = GetDataTable("Exec sp_LocationWiseReceivingAndIssuing '" & dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', '" & dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', " & Me.cmbLocation.SelectedValue & "")
            dt = GetDataTable("Exec sp_LocationWiseReceivingAndIssuing '" & dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "', '" & dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "', " & Me.cmbLocation.SelectedValue & "," & IIf(chkExcludeZeroStock.Checked = True, 1, 0) & "")
            dt.AcceptChanges()
            Me.GridEX1.DataSource = dt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
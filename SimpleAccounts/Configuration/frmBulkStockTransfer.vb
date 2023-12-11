'' Muhammad Amin TASK TFS3776 made new feature of Bulk Stock Transfer. Dated 04-07-18
Imports SBDal
Imports SBModel
Public Class frmBulkStockTransfer
    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("DN" + "-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "DispatchMasterTable", "DispatchNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("DN" & "-" & Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "DispatchMasterTable", "DispatchNo")
            Else
                Return GetNextDocNo("DN", 6, "DispatchMasterTable", "DispatchNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Function GetDocumentNo1() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("SRN" + "-" + Microsoft.VisualBasic.Right(Me.dtpDate.Value.Year, 2) + "-", "ReceivingMasterTable", "ReceivingNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("SRN" & "-" & Format(Me.dtpDate.Value, "yy") & Me.dtpDate.Value.Month.ToString("00"), 4, "ReceivingMasterTable", "ReceivingNo")
            Else
                Return GetNextDocNo("SRN", 6, "ReceivingMasterTable", "ReceivingNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmBulkStockTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            FillLocations()
            Me.txtDispatchNo.Text = GetDocumentNo()
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillLocations()
        Try
            Dim Str As String = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                  & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                  & " Else " _
                  & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
            FillDropDown(Me.cmbFromLocation, Str, True)
            FillDropDown(Me.cmbToLocation, Str, True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillLocations(ByVal FromLocationId As Integer)
        Try
            Dim Str As String = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                  & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Location_id <> " & FromLocationId & " order by sort_order " _
                  & " Else " _
                  & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation WHERE Location_id <> " & FromLocationId & " order by sort_order"
            FillDropDown(Me.cmbToLocation, Str, True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbFromLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFromLocation.SelectedIndexChanged
        Try
            If Me.cmbFromLocation.SelectedIndex > 0 Then
                FillLocations(Me.cmbFromLocation.SelectedValue)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Try
            Dim DispatchNo As String = String.Empty
            Dim ReceivingNo As String = String.Empty
            Dim IsAverageRate As Boolean = False
            If IsValid() Then
                If getConfigValueByType("AvgRate").ToString = "True" Then
                    IsAverageRate = True
                End If
                ReceivingNo = GetDocumentNo1()
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                If BulkStockTransferDAL.ProcessStockTransfer(Me.txtDispatchNo.Text, ReceivingNo, Me.dtpDate.Value, Me.cmbFromLocation.SelectedValue, Me.cmbToLocation.SelectedValue, IsAverageRate) Then
                    msg_Information("Record has been transferred successfully.")
                    Me.lblProgress.Visible = False
                    Me.txtDispatchNo.Text = GetDocumentNo()
                    'Me.Close()
                Else
                    msg_Information("No record has been transferred.")
                    Me.lblProgress.Visible = False
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Function IsValid() As Boolean
        Try
            If Not Me.txtDispatchNo.Text.Length > 0 Then
                ShowErrorMessage("Dispatch No is required.")
                Me.txtDispatchNo.Focus()
                Return False
            End If
            If Me.cmbFromLocation.SelectedIndex < 1 Then
                ShowErrorMessage("From location is required.")
                Me.cmbFromLocation.Focus()
                Return False
            End If
            If Me.cmbToLocation.SelectedIndex < 1 Then
                ShowErrorMessage("To location is required.")
                Me.cmbToLocation.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Id As Integer = 0
        Try
            FillLocations()
            Me.txtDispatchNo.Text = GetDocumentNo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
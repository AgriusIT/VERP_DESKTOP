Imports System.Data.SqlClient
Imports SBDal

Public Class DeliveryInformation

    Private Sub rdoBuilty_CheckedChanged(sender As Object, e As EventArgs) Handles rdoBuilty.CheckedChanged, rdoHumayun.CheckedChanged
        Try
            If rdoBuilty.Checked = True Then
                pnlDriverInfo.Visible = True
                pnlTransporter.Visible = True
            Else
                pnlTransporter.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rdoLater_CheckedChanged(sender As Object, e As EventArgs) Handles rdoLater.CheckedChanged
        Try
            If rdoLater.Checked = True Then
                pnlTransporter.Visible = False
                pnlDriverInfo.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            
            If rdoSelf.Checked = True Then
                trans = conn.BeginTransaction
                    str = "Insert Into tblDeliveryInfo(salesMasterId,DriverName,DriverContact,VehicleNumber,TimeIn,Timeout, TransporterName, BulityNo, Source) Values(" & frmPOSEntry.SalesId & ", N'" & txtDriverName.Text & "', N'" & txtDriverContactNo.Text & "', N'" & txtVehicleNo.Text & "','" & dtpArrivalTime.Value & "','" & dtpDepartureTime.Value & "',NULL,NULL,'" & Me.rdoSelf.Text & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    trans.Commit()
                ElseIf rdoBuilty.Checked = True Then
                    trans = conn.BeginTransaction
                    str = "Insert Into tblDeliveryInfo(salesMasterId,DriverName,DriverContact,VehicleNumber,TimeIn,Timeout, TransporterName, BulityNo, Source) Values(" & frmPOSEntry.SalesId & ", N'" & txtDriverName.Text & "', N'" & txtDriverContactNo.Text & "', N'" & txtVehicleNo.Text & "'," & dtpArrivalTime.Value & "," & dtpDepartureTime.Value & ",N'" & cmbTransporterName.SelectedValue & ",'" & txtBiltyNo.Text & "','" & Me.rdoBuilty.Text & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    trans.Commit()
                ElseIf rdoHumayun.Checked = True Then
                    trans = conn.BeginTransaction
                    str = "Insert Into tblDeliveryInfo(salesMasterId,DriverName,DriverContact,VehicleNumber,TimeIn,Timeout, TransporterName, BulityNo, Source) Values(" & frmPOSEntry.SalesId & ", N'" & txtDriverName.Text & "', N'" & txtDriverContactNo.Text & "', N'" & txtVehicleNo.Text & "'," & dtpArrivalTime.Value & "," & dtpDepartureTime.Value & ",NULL,NULL,'" & Me.rdoHumayun.Text & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    trans.Commit()
            ElseIf rdoLater.Checked = True Then
                Me.Close()
            End If
            ResetControls()
            Me.Close()
        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ResetControls()
        Try
            Me.txtBiltyNo.Text = ""
            Me.txtDriverName.Text = ""
            Me.txtDriverContactNo.Text = ""
            Me.txtVehicleNo.Text = ""
            Me.cmbTransporterName.SelectedIndex = 0
            Me.dtpArrivalTime.Value = Now
            Me.dtpDepartureTime.Value = Now
            rdoBuilty.Checked = True
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub FillCombos()
        Try
            Dim str As String
            str = "select * from tbldeftransporter where active=1 order by sortorder,2"
            FillDropDown(Me.cmbTransporterName, str)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub DeliveryInformation_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            FillCombos()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
Imports SBModel
Public Class frmTransferredStockReport
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
            Else
                Me.btnPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub FillCombo()
        Try
            FillDropDown(Me.cmbDepartment, "Select ArticleGroupId, ArticleGroupName From ArticleGroupDefTable Where Active=1")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmTransferredStockReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            FillCombo()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetReport()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetReport()
        Try
            Try
                AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00"))
                AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59"))
                AddRptParam("@OutQty", Val(Me.txtQty.Text))
                If Not Me.cmbDepartment.SelectedValue Is Nothing Then
                    AddRptParam("@Department", Me.cmbDepartment.SelectedValue)
                Else
                    AddRptParam("@Department", 0)
                End If
                ShowReport("rptStockMovementStatus")
            Catch ex As Exception
                Throw ex
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
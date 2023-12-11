Imports SBModel
Imports SBDal
Imports System.Text.RegularExpressions
Imports System.Data.OleDb
Public Class frmRejectDispatchedStock
    Dim DalObject As New RejectDispatchedStockDAL
    Private Sub FillCombo(ByVal strCondition As String)
        Dim str As String
        If strCondition = "Location" Then

            str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                       & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                       & " Else " _
                       & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"


            FillUltraDropDown(cmbToLocation, str)
            Me.cmbToLocation.Rows(0).Activate()
        ElseIf strCondition = "DispatchNo" Then
            'str = "Select DispatchID, DispatchNo, VendorID from DispatchMasterTable where DispatchID not in(select PurchaseOrderId from ReceivingMasterTable where (ReceivingNo like 'SR%' Or ReceivingNo like 'SRN%') ) and (DispatchNo like 'GP%' Or DispatchNo like 'DN%')"
            str = "Select DispatchID, DispatchNo, VendorID from DispatchMasterTable "
            cmbDispatchNo.DataSource = Nothing
            FillUltraDropDown(cmbDispatchNo, str)
            Me.cmbDispatchNo.Rows(0).Activate()
        End If
    End Sub

    Private Sub frmRejectDispatchedStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombo("DispatchNo")
            FillCombo("Location")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetAll()
        Try
            Dim DispatchId As Integer = 0
            Dim ToLocation As Integer = 0
            If cmbDispatchNo.Value > 0 Then
                DispatchId = cmbDispatchNo.Value
            End If
            If Me.cmbToLocation.Value > 0 Then
                ToLocation = Me.cmbToLocation.Value
            End If
            Me.GridEX1.DataSource = DalObject.GetAll(Me.dtpFromDate.Value, Me.dtpToDate.Value, DispatchId, ToLocation)
            Me.GridEX1.RootTable.Columns("RejectedQty").EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.GridEX1.RetrieveStructure()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim Id As Integer = 0
            Id = Me.cmbDispatchNo.Value
            FillCombo("DispatchNo")
            Me.cmbDispatchNo.Value = Id
            Id = Me.cmbToLocation.Value
            FillCombo("Location")
            Me.cmbToLocation.Value = Id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GridEX1_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnButtonClick
        Try
            If e.Column.Key = "Reject" Then
                DalObject.RejectDispatchedQty(Me.GridEX1.GetRow.Cells("DispatchId").Value, Me.GridEX1.GetRow.Cells("DispatchDetailId").Value, Me.GridEX1.GetRow.Cells("RemainingQty").Value)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
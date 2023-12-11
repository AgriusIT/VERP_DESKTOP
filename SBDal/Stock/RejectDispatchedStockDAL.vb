Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net
Public Class RejectDispatchedStockDAL
    '    SELECT DispatchMasterTable.LocationId, DispatchMasterTable.DispatchNo, DispatchMasterTable.DispatchDate, DispatchDetailTable.Qty,  
    'tblDefLocation.location_name
    'FROM DispatchMasterTable  INNER JOIN
    '                         DispatchDetailTable  ON DispatchMasterTable.DispatchId = DispatchDetailTable.DispatchId LEFT JOIN
    '                        (Select * FROM ReceivingMasterTable INNER JOIN
    '                        ReceivingDetailTable  ON ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId) LEFT JOIN
    '                         tblDefLocation ON DispatchDetailTable.LocationId = tblDefLocation.location_id ON ReceivingMasterTable.PurchaseOrderID = DispatchMasterTable.DispatchId

    Public Function GetAll(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal DispatchId As Integer, ByVal ToLocation As Integer) As DataTable
        Dim dt As DataTable
        Dim strAll As String = ""
        Try
            strAll = " SELECT DispatchMasterTable.LocationId, tblDefLocation.location_name As LocationName, DispatchMasterTable.DispatchNo, DispatchMasterTable.DispatchDate, " _
                      & "  ArticleDefTable.ArticleDescription, IsNull(DispatchDetailTable.Qty, 0) As DispatchedQty, IsNull(Received.Qty, 0) As ReceivedQty, IsNull(DispatchDetailTable.RejectedQty, 0) As RejectedQty, (IsNull(DispatchDetailTable.Qty, 0)- (IsNull(DispatchDetailTable.RejectedQty, 0) + IsNull(Received.Qty, 0))) As RemainingQty, " _
                      & "  Case When (IsNull(DispatchDetailTable.Qty, 0)-(IsNull(DispatchDetailTable.RejectedQty, 0) + IsNull(Received.Qty, 0))) > 0 Then 'Open' Else 'Close' End As Status, DispatchMasterTable.DispatchId, DispatchDetailTable.DispatchDetailId " _
                      & " FROM DispatchMasterTable  INNER JOIN " & _
                                     " DispatchDetailTable  ON DispatchMasterTable.DispatchId = DispatchDetailTable.DispatchId Inner Join " & _
                                     " (Select ReceivingMasterTable.PurchaseOrderId, ReceivingDetailTable.Qty, ReceivingDetailTable.ArticleDefId FROM ReceivingMasterTable INNER JOIN " & _
                                     " ReceivingDetailTable  ON ReceivingMasterTable.ReceivingId = ReceivingDetailTable.ReceivingId Where ReceivingNo like 'SR%' Or ReceivingNo like 'SRN%') As Received ON  DispatchMasterTable.DispatchId = Received.PurchaseOrderId And DispatchDetailTable.ArticleDefId = Received.ArticleDefId LEFT JOIN " & _
                                     " tblDefLocation ON DispatchDetailTable.LocationId = tblDefLocation.location_id " & _
                                     " Inner Join ArticleDefTable ON DispatchDetailTable.ArticleDefId = ArticleDefTable.ArticleId Where Convert(varchar, DispatchMasterTable.DispatchDate, 102) Between Convert(datetime, '" & FromDate.ToString("yyyy-M-d 00:00:00") & "',102) And Convert(datetime, '" & ToDate.ToString("yyyy-M-d 23:59:59") & "',102) "

            If DispatchId > 0 Then
                strAll += " And DispatchDetailTable.DispatchId =" & DispatchId & ""
            End If
            If ToLocation > 0 Then
                strAll += " And DispatchDetailTable.LocationId =" & ToLocation & ""
            End If

            'select PurchaseOrderId from ReceivingMasterTable where (ReceivingNo like 'SR%' Or ReceivingNo like 'SRN%') ) and (DispatchNo like 'GP%' Or DispatchNo like 'DN%'

            dt = UtilityDAL.GetDataTable(strAll)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RejectDispatchedQty(ByVal DispatchId As Integer, ByVal DispatchDetailId As Integer, ByVal RejectedQty As Double) As Boolean
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim str As String = " Update DispatchDetailTable Set RejectedQty = IsNull(RejectedQty, 0) + " & RejectedQty & " Where DispatchDetailId =" & DispatchDetailId & " And DispatchId =" & DispatchId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Str)
            trans.Commit()
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

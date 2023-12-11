Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class StockDispatchDAL
    Public Function Add(ByVal StockDispatch As StockDispatchBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "INSERT INTO DispatchMasterTable(LocationId, DispatchNo, DispatchDate, VendorId, PurchaseOrderID, PartyInvoiceNo, PartySlipNo, DispatchQty, DispatchAmount, CashPaid, Remarks, UserName, RefProductionNo) " _
            & " VALUES(" & StockDispatch.LocationId & ", N'" & StockDispatch.DispatchNo & "', N'" & StockDispatch.DispatchDate & "',  " & StockDispatch.VendorId & ", " & StockDispatch.PurchaseOrderID & ", N'" & StockDispatch.PartyInvoiceNo & "', N'" & StockDispatch.PartySlipNo & "', " & StockDispatch.DispatchQty & ", " & StockDispatch.DispatchAmount & ", " & StockDispatch.CashPaid & ", N'" & StockDispatch.Remarks & "', N'" & StockDispatch.UserName & "', N'" & StockDispatch.RefDocument & "') Select @@Identity"
            StockDispatch.DispatchId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            AddDetail(StockDispatch.DispatchId, trans, StockDispatch)
            Call New StockDAL().Add(StockDispatch.StockMaster)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddDetail(ByVal DispatchId As Integer, ByVal trans As SqlTransaction, ByVal StockDispatch As StockDispatchBE) As Boolean
        Try
            Dim str As String = String.Empty
            Dim StockDispatchDt As List(Of StockDispatchDetailBE) = StockDispatch.StockDispatchDetail
            For Each StockDispatchDetail As StockDispatchDetailBE In StockDispatchDt
                str = "INSERT INTO DispatchDetailTable(DispatchId, LocationId, ArticleDefId, ArticleSize, Sz1, Sz7, Qty, Price, CurrentPrice, BatchNo, BatchId) " _
                    & " VALUES (" & DispatchId & ", " & StockDispatchDetail.LocationId & ", " & StockDispatchDetail.ArticleDefId & ", N'" & StockDispatchDetail.ArticleSize & "', " & StockDispatchDetail.Sz1 & ", " & StockDispatchDetail.Sz7 & ", " & StockDispatchDetail.Qty & ", " & StockDispatchDetail.Price & ", " & StockDispatchDetail.CurrentPrice & ", N'" & StockDispatchDetail.BatchNo & "', " & StockDispatchDetail.BatchID & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Update(ByVal StockDispatch As StockDispatchBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Select * From DispatchMasterTable WHERE DispatchNo=N'" & StockDispatch.DispatchNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    str = "UPDATE DispatchMasterTable SET LocationId=" & StockDispatch.LocationId & ", " _
                        & " DispatchNo=N'" & StockDispatch.DispatchNo & "', " _
                        & " DispatchDate=N'" & StockDispatch.DispatchDate & "', " _
                        & " VendorId= " & StockDispatch.VendorId & ", " _
                        & " PurchaseOrderID=" & StockDispatch.PurchaseOrderID & ", " _
                        & " PartyInvoiceNo=N'" & StockDispatch.PartyInvoiceNo & "',  " _
                        & " PartySlipNo=N'" & StockDispatch.PartySlipNo & "', " _
                        & " DispatchQty=" & StockDispatch.DispatchQty & ", " _
                        & " DispatchAmount=" & StockDispatch.DispatchAmount & ",  " _
                        & " CashPaid=" & StockDispatch.CashPaid & ",  " _
                        & " Remarks=N'" & StockDispatch.Remarks & "', " _
                        & " UserName= N'" & StockDispatch.UserName & "', " _
                        & " RefProductionNo=N'" & StockDispatch.RefDocument & "' WHERE DispatchId=" & dt.Rows(0).Item(0)
                    str = "Delete From DispatchDetailTable WHERE DispatchId=" & dt.Rows(0).Item(0)
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    AddDetail(dt.Rows(0).Item(0), trans, StockDispatch)
                Else
                    str = "INSERT INTO DispatchMasterTable(LocationId, DispatchNo, DispatchDate, VendorId, PurchaseOrderID, PartyInvoiceNo, PartySlipNo, DispatchQty, DispatchAmount, CashPaid, Remarks, UserName, RefProductionNo) " _
                    & " VALUES(" & StockDispatch.LocationId & ", N'" & StockDispatch.DispatchNo & "', N'" & StockDispatch.DispatchDate & "',  " & StockDispatch.VendorId & ", " & StockDispatch.PurchaseOrderID & ", N'" & StockDispatch.PartyInvoiceNo & "', N'" & StockDispatch.PartySlipNo & "', " & StockDispatch.DispatchQty & ", " & StockDispatch.DispatchAmount & ", " & StockDispatch.CashPaid & ", N'" & StockDispatch.Remarks & "', N'" & StockDispatch.UserName & "', N'" & StockDispatch.RefDocument & "') Select @@Identity"
                    StockDispatch.DispatchId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                    AddDetail(StockDispatch.DispatchId, trans, StockDispatch)
                End If
                str = String.Empty
                Call New StockDAL().Update(StockDispatch.StockMaster)
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal StockDispatch As StockDispatchBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            'Dim VoucherId As Integer = Convert.ToInt32(UtilityDAL.GetVoucherIdByVoucherNo("" & StockDispatch.DispatchNo & ""))
            Dim str As String = String.Empty

            str = "Select * From DispatchMasterTable WHERE DispatchNo=N'" & StockDispatch.DispatchNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    str = "Delete From DispatchDetailTable WHERE DispatchId=" & dt.Rows(0).Item(0) & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                    str = String.Empty
                    str = "Delete From DispatchMasterTable WHERE DispatchId=" & dt.Rows(0).Item(0) & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            End If
            ''Delete Voucher From tblVoucherDetail
            'str = String.Empty
            'str = "Delete From tblVoucherDetail WHERE Voucher_ID=" & VoucherId
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ''Delete Voucher From tblVoucherMaster
            'str = String.Empty
            'str = "Delete From tblVoucher WHERE Voucher_ID=" & VoucherId
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Call New StockDAL().Delete(StockDispatch.StockMaster)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
End Class

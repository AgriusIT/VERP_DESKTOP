Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class StockReceivingDAL
    Public Function Add(ByVal StockReceiving As StockReceivingMaster) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            StockReceiving.ReceivingNo = GetDocumentNo(StockReceiving.ReceivingDate, trans)

            Dim str As String = String.Empty
            str = "INSERT INTO ReceivingMasterTable(LocationId, ReceivingNo, ReceivingDate, VendorId, PurchaseOrderID, PartyInvoiceNo, PartySlipNo, ReceivingQty, ReceivingAmount, CashPaid, Remarks, UserName, DcNo, Post, DcDate, Driver_Name,Vehicle_No, vendor_invoice_no,CostCenterId) " _
            & " Values(" & StockReceiving.LocationId & ", N'" & StockReceiving.ReceivingNo & "', N'" & StockReceiving.ReceivingDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & StockReceiving.VendorId & ", " & StockReceiving.PurchaseOrderID & ", N'" & StockReceiving.PartyInvoiceNo & "', N'" & StockReceiving.PartySlipNo & "', " & StockReceiving.ReceivingQty & ", " & StockReceiving.ReceivingAmount & ", " & StockReceiving.CashPaid & ", N'" & StockReceiving.Remarks & "', N'" & StockReceiving.UserName & "', N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(StockReceiving.Post = True, 1, 0) & ", " & IIf(StockReceiving.DcDate <> Nothing, "N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & StockReceiving.Driver_Name & "', N'" & StockReceiving.Vehicle_No & "', N'" & StockReceiving.vendor_invoice_no & "', " & StockReceiving.CostCenterId & " ) Select @@Identity"
            StockReceiving.ReceivingId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            AddDetail(StockReceiving, trans, StockReceiving.ReceivingId)

            ''If UtilityDAL.GetConfigValue("StockTransferFromDispatch").ToString = "True" Then
            'If Not StockReceiving.StockMaster Is Nothing Then
            '    Call New StockDAL().Add(StockReceiving.StockMaster)
            'End If
            ''End If
            trans.Commit()
            'If UtilityDAL.GetConfigValue("StockTransferFromDispatch").ToString = "True" Then
            If Not StockReceiving.StockMaster Is Nothing Then
                Call New StockDAL().Add(StockReceiving.StockMaster)
            End If
            'End If

            Return True
            trans.Rollback()
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Add(ByVal StockReceiving As StockReceivingMaster, ByVal trans As OleDb.OleDbTransaction) As Boolean
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try


            StockReceiving.ReceivingNo = GetDocumentNo(StockReceiving.ReceivingDate, trans)

            Dim str As String = String.Empty
            str = "INSERT INTO ReceivingMasterTable(LocationId, ReceivingNo, ReceivingDate, VendorId, PurchaseOrderID, PartyInvoiceNo, PartySlipNo, ReceivingQty, ReceivingAmount, CashPaid, Remarks, UserName, DcNo, Post, DcDate, Driver_Name,Vehicle_No, vendor_invoice_no,CostCenterId) " _
            & " Values(" & StockReceiving.LocationId & ", N'" & StockReceiving.ReceivingNo & "', N'" & StockReceiving.ReceivingDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & StockReceiving.VendorId & ", " & StockReceiving.PurchaseOrderID & ", N'" & StockReceiving.PartyInvoiceNo & "', N'" & StockReceiving.PartySlipNo & "', " & StockReceiving.ReceivingQty & ", " & StockReceiving.ReceivingAmount & ", " & StockReceiving.CashPaid & ", N'" & StockReceiving.Remarks.Replace("'", "''") & "', N'" & StockReceiving.UserName & "', N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(StockReceiving.Post = True, 1, 0) & ", " & IIf(StockReceiving.DcDate <> Nothing, "N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & StockReceiving.Driver_Name & "', N'" & StockReceiving.Vehicle_No & "', N'" & StockReceiving.vendor_invoice_no & "', " & StockReceiving.CostCenterId & " ) Select @@Identity"
            StockReceiving.ReceivingId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            AddDetail(StockReceiving, trans, StockReceiving.ReceivingId)

            ''If UtilityDAL.GetConfigValue("StockTransferFromDispatch").ToString = "True" Then
            'If Not StockReceiving.StockMaster Is Nothing Then
            '    Call New StockDAL().Add(StockReceiving.StockMaster)
            'End If
            ''End If
            'trans.Commit()
            'If UtilityDAL.GetConfigValue("StockTransferFromDispatch").ToString = "True" Then
            If Not StockReceiving.StockMaster Is Nothing Then
                StockReceiving.StockMaster.DocNo = StockReceiving.ReceivingNo
                Call New StockDAL().Add(StockReceiving.StockMaster, trans)
            End If
            'End If

            Return True
            'trans.Rollback()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function AddDetail(ByVal StockReceiving As StockReceivingMaster, ByVal trans As SqlTransaction, ByVal Receiving_Id As Integer) As Boolean
        Try

            Dim str As String = String.Empty

            Dim StockReceivingDetail As List(Of StockReceivingDetail) = StockReceiving.StockReceivingDetail
            For Each StockReceivingList As StockReceivingDetail In StockReceivingDetail
                str = "INSERT INTO ReceivingDetailTable(ReceivingId, LocationId, ArticleDefId, ArticleSize, Sz1, Sz2,Sz3,Sz4,Sz5,Sz6,Sz7,Qty, Price,CurrentPrice,BatchNo,BatchId, ReceivedQty, RejectedQty, TaxPercent) " _
                & " Values(" & Receiving_Id & ", " & StockReceivingList.LocationId & ", " & StockReceivingList.ArticleDefId & ", N'" & StockReceivingList.ArticleSize & "', " & StockReceivingList.Sz1 & "," & StockReceivingList.Sz2 & "," & StockReceivingList.Sz3 & "," & StockReceivingList.Sz4 & "," & StockReceivingList.Sz5 & "," & StockReceivingList.Sz6 & "," & StockReceivingList.Sz7 & "," & StockReceivingList.Qty & "," & StockReceivingList.Price & "," & StockReceivingList.CurrentPrice & ",N'" & StockReceivingList.BatchNo & "', " & StockReceivingList.BatchID & ", " & StockReceivingList.ReceivedQty & ", " & StockReceivingList.RejectedQty & ", " & StockReceivingList.TaxPercent & " )"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next


            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function AddDetail(ByVal StockReceiving As StockReceivingMaster, ByVal trans As OleDb.OleDbTransaction, ByVal Receiving_Id As Integer) As Boolean
        Try

            Dim str As String = String.Empty

            Dim StockReceivingDetail As List(Of StockReceivingDetail) = StockReceiving.StockReceivingDetail
            For Each StockReceivingList As StockReceivingDetail In StockReceivingDetail
                str = "INSERT INTO ReceivingDetailTable(ReceivingId, LocationId, ArticleDefId, ArticleSize, Sz1, Sz2,Sz3,Sz4,Sz5,Sz6,Sz7,Qty, Price,CurrentPrice,BatchNo,BatchId, ReceivedQty, RejectedQty, TaxPercent,FromLocationId, ExpiryDate, Origin) " _
                & " Values(" & Receiving_Id & ", " & StockReceivingList.LocationId & ", " & StockReceivingList.ArticleDefId & ", N'" & StockReceivingList.ArticleSize & "', " & StockReceivingList.Sz1 & "," & StockReceivingList.Sz2 & "," & StockReceivingList.Sz3 & "," & StockReceivingList.Sz4 & "," & StockReceivingList.Sz5 & "," & StockReceivingList.Sz6 & "," & StockReceivingList.Sz7 & "," & StockReceivingList.Qty & "," & StockReceivingList.Price & "," & StockReceivingList.CurrentPrice & ",N'" & StockReceivingList.BatchNo & "', " & StockReceivingList.BatchID & ", " & StockReceivingList.ReceivedQty & ", " & StockReceivingList.RejectedQty & ", " & StockReceivingList.TaxPercent & " ," & StockReceivingList.FromLocationId & ", " & IIf(StockReceivingList.ExpiryDate.ToString <> "", "N'" & StockReceivingList.ExpiryDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "Null") & ",N'" & StockReceivingList.Origin & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next


            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function Update(ByVal StockReceiving As StockReceivingMaster) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = String.Empty
            str = "Select * From ReceivingMasterTable WHERE ReceivingNo=N'" & StockReceiving.ReceivingNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str, trans)

            If dt.Rows.Count > 0 Then
                str = String.Empty
                str = "Delete From ReceivingDetailTable WHERE ReceivingId=" & dt.Rows(0).Item("ReceivingId").ToString & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                str = String.Empty
                'Before against Request no. 934
                'str = " UPDATE ReceivingMasterTable SET " _
                '& " LocationId=" & StockReceiving.LocationId & ", " _
                '& " ReceivingNo=N'" & StockReceiving.ReceivingNo & "', " _
                '& " ReceivingDate=N'" & StockReceiving.ReceivingDate.ToString("yyyy-M-d h:mm:ss tt") & "',  " _
                '& " VendorId=" & StockReceiving.VendorId & " , " _
                '& " PurchaseOrderID=" & StockReceiving.PurchaseOrderID & ", " _
                '& " PartyInvoiceNo=N'" & StockReceiving.PartyInvoiceNo & "',  " _
                '& " PartySlipNo=N'" & StockReceiving.PartySlipNo & "',  " _
                '& " ReceivingQty=" & StockReceiving.ReceivingQty & ",  " _
                '& " ReceivingAmount=" & StockReceiving.ReceivingAmount & ", " _
                '& " CashPaid=" & StockReceiving.CashPaid & ", " _
                '& " Remarks=N'" & StockReceiving.Remarks & "',  " _
                '& " UserName=N'" & StockReceiving.UserName & "',  " _
                '& " IGPNo=N'" & StockReceiving.IGPNo.ToString & "', " _
                '& " DcNo=N'" & StockReceiving.DcNo.ToString & "',  " _
                '& " Post=" & IIf(StockReceiving.Post = True, 1, 0) & ",  " _
                '& " DcDate=" & IIf(StockReceiving.DcDate <> "#12:00:00 AM#", "N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ",  " _
                '& " Driver_Name=N'" & StockReceiving.Driver_Name & "',  " _
                '& " Vehicle_No=N'" & StockReceiving.Vehicle_No & "', " _
                '& " vendor_invoice_no=N'" & StockReceiving.vendor_invoice_no & "', " _
                '& " CostCenterId=" & StockReceiving.CostCenterId & " " _
                '& " WHERE (ReceivingId=" & dt.Rows(0).Item("ReceivingId").ToString & ")"
                'ReqId-934 Resolve Comma Error
                str = " UPDATE ReceivingMasterTable SET " _
             & " LocationId=" & StockReceiving.LocationId & ", " _
             & " ReceivingNo=N'" & StockReceiving.ReceivingNo & "', " _
             & " ReceivingDate=N'" & StockReceiving.ReceivingDate.ToString("yyyy-M-d h:mm:ss tt") & "',  " _
             & " VendorId=" & StockReceiving.VendorId & " , " _
             & " PurchaseOrderID=" & StockReceiving.PurchaseOrderID & ", " _
             & " PartyInvoiceNo=N'" & StockReceiving.PartyInvoiceNo & "',  " _
             & " PartySlipNo=N'" & StockReceiving.PartySlipNo.Replace("'", "''") & "',  " _
             & " ReceivingQty=" & StockReceiving.ReceivingQty & ",  " _
             & " ReceivingAmount=" & StockReceiving.ReceivingAmount & ", " _
             & " CashPaid=" & StockReceiving.CashPaid & ", " _
             & " Remarks=N'" & StockReceiving.Remarks.Replace("'", "''") & "',  " _
             & " UserName=N'" & StockReceiving.UserName & "',  " _
             & " IGPNo=N'" & StockReceiving.IGPNo.ToString.Replace("'", "''") & "', " _
             & " DcNo=N'" & StockReceiving.DcNo.ToString.Replace("'", "''") & "',  " _
             & " Post=" & IIf(StockReceiving.Post = True, 1, 0) & ",  " _
             & " DcDate=" & IIf(StockReceiving.DcDate <> "#12:00:00 AM#", "N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ",  " _
             & " Driver_Name=N'" & StockReceiving.Driver_Name.Replace("'", "''") & "',  " _
             & " Vehicle_No=N'" & StockReceiving.Vehicle_No.Replace("'", "''") & "', " _
             & " vendor_invoice_no=N'" & StockReceiving.vendor_invoice_no.Replace("'", "''") & "', " _
             & " CostCenterId=" & StockReceiving.CostCenterId & " " _
             & " WHERE (ReceivingId=" & dt.Rows(0).Item("ReceivingId").ToString & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Call AddDetail(StockReceiving, trans, Convert.ToInt32(dt.Rows(0).Item("ReceivingId").ToString))
            Else
                str = String.Empty
                'Before against request no. 934
                'str = "INSERT INTO ReceivingMasterTable(LocationId, ReceivingNo, ReceivingDate, VendorId, PurchaseOrderID, PartyInvoiceNo, PartySlipNo, ReceivingQty, ReceivingAmount, CashPaid, Remarks, UserName, IGPNo, DcNo, Post, DcDate, Driver_Name,Vehicle_No, vendor_invoice_no,CostCenterId) " _
                '& " Values(" & StockReceiving.LocationId & ", N'" & StockReceiving.ReceivingNo & "', N'" & StockReceiving.ReceivingDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & StockReceiving.VendorId & ", " & StockReceiving.PurchaseOrderID & ", N'" & StockReceiving.PartyInvoiceNo & "', N'" & StockReceiving.PartySlipNo & "', " & StockReceiving.ReceivingQty & ", " & StockReceiving.ReceivingAmount & ", " & StockReceiving.CashPaid & ", N'" & StockReceiving.Remarks & "', N'" & StockReceiving.UserName & "', N'" & StockReceiving.IGPNo & "', N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(StockReceiving.Post = True, 1, 0) & ", " & IIf(StockReceiving.DcDate <> Nothing, "N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & StockReceiving.Driver_Name & "', N'" & StockReceiving.Vehicle_No & "', N'" & StockReceiving.vendor_invoice_no & "', " & StockReceiving.CostCenterId & " ) Select @@Identity"
                'ReqId-934 Resolve Comma Error
                StockReceiving.ReceivingNo = GetDocumentNo(StockReceiving.ReceivingDate, trans)
                str = "INSERT INTO ReceivingMasterTable(LocationId, ReceivingNo, ReceivingDate, VendorId, PurchaseOrderID, PartyInvoiceNo, PartySlipNo, ReceivingQty, ReceivingAmount, CashPaid, Remarks, UserName, IGPNo, DcNo, Post, DcDate, Driver_Name,Vehicle_No, vendor_invoice_no,CostCenterId) " _
                & " Values(" & StockReceiving.LocationId & ", N'" & StockReceiving.ReceivingNo & "', N'" & StockReceiving.ReceivingDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & StockReceiving.VendorId & ", " & StockReceiving.PurchaseOrderID & ", N'" & StockReceiving.PartyInvoiceNo.Replace("'", "''") & "', N'" & StockReceiving.PartySlipNo.Replace("'", "''") & "', " & StockReceiving.ReceivingQty & ", " & StockReceiving.ReceivingAmount & ", " & StockReceiving.CashPaid & ", N'" & StockReceiving.Remarks.Replace("'", "''") & "', N'" & StockReceiving.UserName.Replace("'", "''") & "', N'" & StockReceiving.IGPNo.Replace("'", "''") & "', N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(StockReceiving.Post = True, 1, 0) & ", " & IIf(StockReceiving.DcDate <> Nothing, "N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & StockReceiving.Driver_Name.Replace("'", "''") & "', N'" & StockReceiving.Vehicle_No.Replace("'", "''") & "', N'" & StockReceiving.vendor_invoice_no.Replace("'", "''") & "', " & StockReceiving.CostCenterId & " ) Select @@Identity"

                StockReceiving.ReceivingId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                Call AddDetail(StockReceiving, trans, Convert.ToInt32(StockReceiving.ReceivingId))
            End If
            'If UtilityDAL.GetConfigValue("StockTransferFromDispatch").ToString = "True" Then
            'If Not StockReceiving.StockMaster Is Nothing Then
            '    Call New StockDAL().Update(StockReceiving.StockMaster)
            '    'End If
            'End If
            trans.Commit()
            If Not StockReceiving.StockMaster Is Nothing Then
                Call New StockDAL().Update(StockReceiving.StockMaster)
                'End If
            End If
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal StockReceiving As StockReceivingMaster, ByVal trans As OleDb.OleDbTransaction) As Boolean
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = String.Empty
            'str = "Select * From ReceivingMasterTable WHERE ReceivingNo=N'" & StockReceiving.ReceivingNo & "'"
            'Dim dt As DataTable = UtilityDAL.GetDataTable(str, trans)
            Dim intReceivingId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, "Select ReceivingId From ReceivingMasterTable WHERE ReceivingNo='" & StockReceiving.ReceivingNo & "'")

            If intReceivingId > 0 Then
                str = String.Empty
                str = "Delete From ReceivingDetailTable WHERE ReceivingId in(Select ReceivingId From ReceivingMasterTable WHERE ReceivingNo='" & StockReceiving.ReceivingNo & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                str = String.Empty
                'Before against Request no. 934
                'str = " UPDATE ReceivingMasterTable SET " _
                '& " LocationId=" & StockReceiving.LocationId & ", " _
                '& " ReceivingNo=N'" & StockReceiving.ReceivingNo & "', " _
                '& " ReceivingDate=N'" & StockReceiving.ReceivingDate.ToString("yyyy-M-d h:mm:ss tt") & "',  " _
                '& " VendorId=" & StockReceiving.VendorId & " , " _
                '& " PurchaseOrderID=" & StockReceiving.PurchaseOrderID & ", " _
                '& " PartyInvoiceNo=N'" & StockReceiving.PartyInvoiceNo & "',  " _
                '& " PartySlipNo=N'" & StockReceiving.PartySlipNo & "',  " _
                '& " ReceivingQty=" & StockReceiving.ReceivingQty & ",  " _
                '& " ReceivingAmount=" & StockReceiving.ReceivingAmount & ", " _
                '& " CashPaid=" & StockReceiving.CashPaid & ", " _
                '& " Remarks=N'" & StockReceiving.Remarks & "',  " _
                '& " UserName=N'" & StockReceiving.UserName & "',  " _
                '& " IGPNo=N'" & StockReceiving.IGPNo.ToString & "', " _
                '& " DcNo=N'" & StockReceiving.DcNo.ToString & "',  " _
                '& " Post=" & IIf(StockReceiving.Post = True, 1, 0) & ",  " _
                '& " DcDate=" & IIf(StockReceiving.DcDate <> "#12:00:00 AM#", "N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ",  " _
                '& " Driver_Name=N'" & StockReceiving.Driver_Name & "',  " _
                '& " Vehicle_No=N'" & StockReceiving.Vehicle_No & "', " _
                '& " vendor_invoice_no=N'" & StockReceiving.vendor_invoice_no & "', " _
                '& " CostCenterId=" & StockReceiving.CostCenterId & " " _
                '& " WHERE (ReceivingId=" & dt.Rows(0).Item("ReceivingId").ToString & ")"
                'ReqId-934 Resolve Comma Error
                str = " UPDATE ReceivingMasterTable SET " _
             & " LocationId=" & StockReceiving.LocationId & ", " _
             & " ReceivingNo=N'" & StockReceiving.ReceivingNo & "', " _
             & " ReceivingDate=N'" & StockReceiving.ReceivingDate.ToString("yyyy-M-d h:mm:ss tt") & "',  " _
             & " VendorId=" & StockReceiving.VendorId & " , " _
             & " PurchaseOrderID=" & StockReceiving.PurchaseOrderID & ", " _
             & " PartyInvoiceNo=N'" & StockReceiving.PartyInvoiceNo & "',  " _
             & " PartySlipNo=N'" & StockReceiving.PartySlipNo.Replace("'", "''") & "',  " _
             & " ReceivingQty=" & StockReceiving.ReceivingQty & ",  " _
             & " ReceivingAmount=" & StockReceiving.ReceivingAmount & ", " _
             & " CashPaid=" & StockReceiving.CashPaid & ", " _
             & " Remarks=N'" & StockReceiving.Remarks.Replace("'", "''") & "',  " _
             & " UserName=N'" & StockReceiving.UserName & "',  " _
             & " IGPNo=N'" & StockReceiving.IGPNo.ToString.Replace("'", "''") & "', " _
             & " DcNo=N'" & StockReceiving.DcNo.ToString.Replace("'", "''") & "',  " _
             & " Post=" & IIf(StockReceiving.Post = True, 1, 0) & ",  " _
             & " DcDate=" & IIf(StockReceiving.DcDate <> "#12:00:00 AM#", "N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ",  " _
             & " Driver_Name=N'" & StockReceiving.Driver_Name.Replace("'", "''") & "',  " _
             & " Vehicle_No=N'" & StockReceiving.Vehicle_No.Replace("'", "''") & "', " _
             & " vendor_invoice_no=N'" & StockReceiving.vendor_invoice_no.Replace("'", "''") & "', " _
             & " CostCenterId=" & StockReceiving.CostCenterId & " " _
             & " WHERE (ReceivingId=" & intReceivingId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Call AddDetail(StockReceiving, trans, Convert.ToInt32(intReceivingId))
            Else
                str = String.Empty
                'Before against request no. 934
                'str = "INSERT INTO ReceivingMasterTable(LocationId, ReceivingNo, ReceivingDate, VendorId, PurchaseOrderID, PartyInvoiceNo, PartySlipNo, ReceivingQty, ReceivingAmount, CashPaid, Remarks, UserName, IGPNo, DcNo, Post, DcDate, Driver_Name,Vehicle_No, vendor_invoice_no,CostCenterId) " _
                '& " Values(" & StockReceiving.LocationId & ", N'" & StockReceiving.ReceivingNo & "', N'" & StockReceiving.ReceivingDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & StockReceiving.VendorId & ", " & StockReceiving.PurchaseOrderID & ", N'" & StockReceiving.PartyInvoiceNo & "', N'" & StockReceiving.PartySlipNo & "', " & StockReceiving.ReceivingQty & ", " & StockReceiving.ReceivingAmount & ", " & StockReceiving.CashPaid & ", N'" & StockReceiving.Remarks & "', N'" & StockReceiving.UserName & "', N'" & StockReceiving.IGPNo & "', N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(StockReceiving.Post = True, 1, 0) & ", " & IIf(StockReceiving.DcDate <> Nothing, "N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & StockReceiving.Driver_Name & "', N'" & StockReceiving.Vehicle_No & "', N'" & StockReceiving.vendor_invoice_no & "', " & StockReceiving.CostCenterId & " ) Select @@Identity"
                'ReqId-934 Resolve Comma Error
                StockReceiving.ReceivingNo = GetDocumentNo(StockReceiving.ReceivingDate, trans)
                str = "INSERT INTO ReceivingMasterTable(LocationId, ReceivingNo, ReceivingDate, VendorId, PurchaseOrderID, PartyInvoiceNo, PartySlipNo, ReceivingQty, ReceivingAmount, CashPaid, Remarks, UserName, IGPNo, DcNo, Post, DcDate, Driver_Name,Vehicle_No, vendor_invoice_no,CostCenterId) " _
                & " Values(" & StockReceiving.LocationId & ", N'" & StockReceiving.ReceivingNo & "', N'" & StockReceiving.ReceivingDate.ToString("yyyy-M-d h:mm:ss tt") & "'," & StockReceiving.VendorId & ", " & StockReceiving.PurchaseOrderID & ", N'" & StockReceiving.PartyInvoiceNo.Replace("'", "''") & "', N'" & StockReceiving.PartySlipNo.Replace("'", "''") & "', " & StockReceiving.ReceivingQty & ", " & StockReceiving.ReceivingAmount & ", " & StockReceiving.CashPaid & ", N'" & StockReceiving.Remarks.Replace("'", "''") & "', N'" & StockReceiving.UserName.Replace("'", "''") & "', N'" & StockReceiving.IGPNo.Replace("'", "''") & "', N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(StockReceiving.Post = True, 1, 0) & ", " & IIf(StockReceiving.DcDate <> Nothing, "N'" & StockReceiving.DcDate.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & StockReceiving.Driver_Name.Replace("'", "''") & "', N'" & StockReceiving.Vehicle_No.Replace("'", "''") & "', N'" & StockReceiving.vendor_invoice_no.Replace("'", "''") & "', " & StockReceiving.CostCenterId & " ) Select @@Identity"

                StockReceiving.ReceivingId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                Call AddDetail(StockReceiving, trans, Convert.ToInt32(StockReceiving.ReceivingId))
            End If
            'If UtilityDAL.GetConfigValue("StockTransferFromDispatch").ToString = "True" Then
            'If Not StockReceiving.StockMaster Is Nothing Then
            '    Call New StockDAL().Update(StockReceiving.StockMaster)
            '    'End If
            'End If
            'trans.Commit()
            If Not StockReceiving.StockMaster Is Nothing Then
                Call New StockDAL().Update(StockReceiving.StockMaster, trans)
                'End If
            End If
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal StockReceiving As StockReceivingMaster) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim str As String = String.Empty
            str = "Delete From ReceivingDetailTable WHERE ReceivingId=" & StockReceiving.ReceivingId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From ReceivingMasterTable WHERE ReceivingId=" & StockReceiving.ReceivingId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal StockReceiving As StockReceivingMaster, ByVal trans As OleDb.OleDbTransaction) As Boolean
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim str As String = String.Empty
            str = "Delete From ReceivingDetailTable WHERE ReceivingId=" & StockReceiving.ReceivingId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From ReceivingMasterTable WHERE ReceivingId=" & StockReceiving.ReceivingId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function

    Public Function DeleteDispatch(ByVal ReceivingId As Integer, ByVal trans As OleDb.OleDbTransaction) As Boolean
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Dim dt As New DataTable
        Try
            Dim str As String = String.Empty
            str = "Delete From ReceivingDetailTable WHERE ReceivingId=" & ReceivingId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            str = String.Empty
            str = "Delete From ReceivingMasterTable WHERE ReceivingId=" & ReceivingId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Function GetDocumentNo(dtpPODate As DateTime, trans As SqlTransaction) As String
        Try
            'If Me.txtPONo.Text = "" Then
            If UtilityDAL.GetConfigValue("VoucherNo", trans).ToString = "Yearly" Then
                Return UtilityDAL.GetSerialNo("SRN" + "-" + Microsoft.VisualBasic.Right(dtpPODate.Year, 2) + "-", "ReceivingMasterTable", "ReceivingNo", trans)
            ElseIf UtilityDAL.GetConfigValue("VoucherNo", trans).ToString = "Monthly" Then
                Return UtilityDAL.GetNextDocNo("SRN" & "-" & Format(dtpPODate, "yy") & dtpPODate.Month.ToString("00"), 4, "ReceivingMasterTable", "ReceivingNo", trans)
            Else
                Return UtilityDAL.GetNextDocNo("SRN", 6, "ReceivingMasterTable", "ReceivingNo", trans)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetDocumentNo(dtpPODate As DateTime, trans As OleDb.OleDbTransaction) As String
        Try
            'If Me.txtPONo.Text = "" Then
            If UtilityDAL.GetConfigValueOleDB("VoucherNo", trans).ToString = "Yearly" Then
                Return UtilityDAL.GetSerialNoOleDB("SRN" + "-" + Microsoft.VisualBasic.Right(dtpPODate.Year, 2) + "-", "ReceivingMasterTable", "ReceivingNo", trans)
            ElseIf UtilityDAL.GetConfigValueOleDB("VoucherNo", trans).ToString = "Monthly" Then
                Return UtilityDAL.GetNextDocNoOleDB("SRN" & "-" & Format(dtpPODate, "yy") & dtpPODate.Month.ToString("00"), 4, "ReceivingMasterTable", "ReceivingNo", trans)
            Else
                Return UtilityDAL.GetNextDocNoOleDB("SRN", 6, "ReceivingMasterTable", "ReceivingNo", trans)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

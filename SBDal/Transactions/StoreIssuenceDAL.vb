Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class StoreIssuenceDAL
    Public Function Add(ByVal StoreIssuence As StoreIssuenceMaster) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try


            StoreIssuence.DispatchNo = GetDocumentNo(StoreIssuence.DispatchDate, trans)

            Dim PurchaseAccount As Integer = Convert.ToInt32(UtilityDAL.GetConfigValue("PurchaseDebitAccount", trans).ToString.Replace("Error", "0"))
            Dim CostOfGoodsAccount As Integer = 0I 'Convert.ToInt32(UtilityDAL.GetConfigValue("StoreIssuenceAccount").ToString)
            Dim GLAccountArticleDepartment As Boolean = Convert.ToBoolean(UtilityDAL.GetConfigValue("GLAccountArticleDepartment", trans).Replace("Error", "False"))



            If StoreIssuence.CGSAccountId > 0 Then
                CostOfGoodsAccount = StoreIssuence.CGSAccountId
            Else
                CostOfGoodsAccount = Convert.ToInt32(UtilityDAL.GetConfigValue("StoreIssuenceAccount", trans).ToString.Replace("Error", "0"))
            End If


            Dim str As String = String.Empty
            'str = "INSERT INTO DispatchMasterTable(LocationId, DispatchNo, DispatchDate, VendorId, PurchaseOrderID, PartyInvoiceNo, PartySlipNo, DispatchQty, DispatchAmount, CashPaid, Remarks, UserName, RefDocument) " _
            '& " Values(" & StoreIssuence.LocationId & ", N'" & StoreIssuence.DispatchNo & "', N'" & StoreIssuence.DispatchDate & "'," & StoreIssuence.VendorId & ", " & StoreIssuence.PurchaseOrderID & ", N'" & StoreIssuence.PartyInvoiceNo & "', N'" & StoreIssuence.PartySlipNo & "', " & StoreIssuence.DispatchQty & ", " & StoreIssuence.DispatchAmount & ", " & StoreIssuence.CashPaid & ", N'" & StoreIssuence.Remarks & "', N'" & StoreIssuence.UserName & "', N'" & StoreIssuence.RefDocument & "') Select @@Identity"
            'ReqId-934 Resolve Comma Error
            str = "INSERT INTO DispatchMasterTable(LocationId, DispatchNo, DispatchDate, VendorId, PurchaseOrderID, PartyInvoiceNo, PartySlipNo, DispatchQty, DispatchAmount, CashPaid, Remarks, UserName, RefDocument,PlanId) " _
           & " Values(" & StoreIssuence.LocationId & ", N'" & StoreIssuence.DispatchNo & "', N'" & StoreIssuence.DispatchDate & "'," & StoreIssuence.VendorId & ", " & StoreIssuence.PurchaseOrderID & ", N'" & StoreIssuence.PartyInvoiceNo.Replace("'", "''") & "', N'" & StoreIssuence.PartySlipNo.Replace("'", "''") & "', " & StoreIssuence.DispatchQty & ", " & StoreIssuence.DispatchAmount & ", " & StoreIssuence.CashPaid & ", N'" & StoreIssuence.Remarks.Replace("'", "''") & "', N'" & StoreIssuence.UserName & "', N'" & StoreIssuence.RefDocument.Replace("'", "''") & "'," & StoreIssuence.PlanId & ") Select @@Identity"

            StoreIssuence.DispatchId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            AddDetail(StoreIssuence, trans, StoreIssuence.DispatchId)

            'Voucher information here...
            str = String.Empty
            'str = "INSERT INTO tblVoucher(location_id, voucher_code, voucher_type_id, voucher_no, voucher_date, Post, source, Reference, UserName) " _
            '& "Values(" & StoreIssuence.LocationId & ", N'" & StoreIssuence.DispatchNo & "', 1, N'" & StoreIssuence.DispatchNo & "', N'" & StoreIssuence.DispatchDate & "', " & IIf(StoreIssuence.Post = True, 1, 0) & ", 'frmProductionStore', N'" & StoreIssuence.Remarks & "', N'" & StoreIssuence.UserName & "') Select @@Identity"
            'ReqId-934 Resolve Comma Error
            str = "INSERT INTO tblVoucher(location_id, voucher_code, voucher_type_id, voucher_no, voucher_date, Post, source, Reference, UserName) " _
           & "Values(" & StoreIssuence.LocationId & ", N'" & StoreIssuence.DispatchNo & "', 1, N'" & StoreIssuence.DispatchNo & "', N'" & StoreIssuence.DispatchDate & "', " & IIf(StoreIssuence.Post = True, 1, 0) & ", 'frmProductionStore', N'" & StoreIssuence.Remarks.Replace("'", "''") & "', N'" & StoreIssuence.UserName & "') Select @@Identity"
            StoreIssuence.DispatchId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            For Each StoreIss As StoreIssuenceDetail In StoreIssuence.StoreIssuenceDetail
                If GLAccountArticleDepartment = True Then
                    PurchaseAccount = StoreIss.PurchaseAccountId  'Val(dtGrd.Rows(i).Item("PurchaseAccountId").ToString)
                End If
                'Purchase Debit Account Here ...
                str = String.Empty
                str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount, CostCenterID) " _
                & "Values(" & StoreIssuence.DispatchId & ", " & StoreIssuence.LocationId & ", " & CostOfGoodsAccount & ", 'Ref:" & StoreIssuence.DispatchNo & "', " & Val(StoreIss.Qty) * Val(StoreIss.Price) & ", 0, " & StoreIssuence.PurchaseOrderID & " )"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Store Credit Account Here ...
                str = String.Empty
                str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount, CostCenterID) " _
                & "Values(" & StoreIssuence.DispatchId & ", " & StoreIssuence.LocationId & ", " & PurchaseAccount & ", 'Ref:" & StoreIssuence.DispatchNo & "',0, " & Val(StoreIss.Qty) * Val(StoreIss.Price) & ", " & StoreIssuence.PurchaseOrderID & " )"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next


            If ValidateIssuanceVoucher(StoreIssuence.DispatchNo, trans) = False Then
                Throw New Exception("Record doesn't manage properly, Please check account mapping for store issuence voucher.")
            End If

            Call New StockDAL().Add(StoreIssuence.StockMaster, trans)

            trans.Commit()
            'If UtilityDAL.GetConfigValue("ProductionWithoutStoreIssuence").ToString = "True" Then
            'Call New StockDAL().Add(StoreIssuence.StockMaster)
            'End If
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddDetail(ByVal StoreIssuence As StoreIssuenceMaster, ByVal trans As SqlTransaction, ByVal Dispatch_Id As Integer) As Boolean
        Try

            Dim str As String = String.Empty
            Dim StoreIssuenceDetail As List(Of StoreIssuenceDetail) = StoreIssuence.StoreIssuenceDetail
            For Each StoreIssuenceList As StoreIssuenceDetail In StoreIssuenceDetail
                'str = "INSERT INTO DispatchDetailTable(DispatchId, LocationId, ArticleDefId, ArticleSize, Sz1, Sz2,Sz3,Sz4,Sz5,Sz6,Sz7,Qty, Price,CurrentPrice,BatchNo,BatchId, Pack_Desc) " _
                '& " Values(" & Dispatch_Id & ", " & StoreIssuenceList.LocationId & ", " & StoreIssuenceList.ArticleDefId & ", N'" & StoreIssuenceList.ArticleSize & "', " & StoreIssuenceList.Sz1 & "," & StoreIssuenceList.Sz2 & "," & StoreIssuenceList.Sz3 & "," & StoreIssuenceList.Sz4 & "," & StoreIssuenceList.Sz5 & "," & StoreIssuenceList.Sz6 & "," & StoreIssuenceList.Sz7 & "," & StoreIssuenceList.Qty & "," & StoreIssuenceList.Price & "," & StoreIssuenceList.CurrentPrice & ",N'" & StoreIssuenceList.BatchNo & "', " & StoreIssuenceList.BatchID & ", N'" & StoreIssuenceList.Pack_Desc.Replace("'", "''") & "')"
                str = "INSERT INTO DispatchDetailTable(DispatchId, LocationId, ArticleDefId, ArticleSize, Sz1, Sz2,Sz3,Sz4,Sz5,Sz6,Sz7,Qty, Price,CurrentPrice,BatchNo,BatchId, Pack_Desc,ArticleDefMasterId) " _
                & " Values(" & Dispatch_Id & ", " & StoreIssuenceList.LocationId & ", " & StoreIssuenceList.ArticleDefId & ", N'" & StoreIssuenceList.ArticleSize & "', " & StoreIssuenceList.Sz1 & "," & StoreIssuenceList.Sz2 & "," & StoreIssuenceList.Sz3 & "," & StoreIssuenceList.Sz4 & "," & StoreIssuenceList.Sz5 & "," & StoreIssuenceList.Sz6 & "," & StoreIssuenceList.Sz7 & "," & StoreIssuenceList.Qty & "," & StoreIssuenceList.Price & "," & StoreIssuenceList.CurrentPrice & ",N'" & StoreIssuenceList.BatchNo.Replace("'", "''") & "', " & StoreIssuenceList.BatchID & ", N'" & StoreIssuenceList.Pack_Desc.Replace("'", "''") & "'," & StoreIssuenceList.ArticleDefMasterId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)



                str = String.Empty
                str = "Update PlanCostSheetDetailTable Set IssuedQty=IsNull(IssuedQty,0)+" & StoreIssuenceList.Sz1 & " WHERE PlanId=" & StoreIssuence.PlanId & " AND ArticleMasterID=" & StoreIssuenceList.ArticleDefMasterId & " AND ArticleDefId=" & StoreIssuenceList.ArticleDefId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)



            Next

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function Update(ByVal StoreIssuence As StoreIssuenceMaster) As Boolean
        Dim VoucherId As Integer = Convert.ToInt32(UtilityDAL.GetVoucherIdByVoucherNo("" & StoreIssuence.DispatchNo & ""))
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim PurchaseAccount As Integer = Convert.ToInt32(UtilityDAL.GetConfigValue("PurchaseDebitAccount", trans).ToString.Replace("Error", "0"))
            Dim CostOfGoodsAccount As Integer = 0I 'Convert.ToInt32(UtilityDAL.GetConfigValue("StoreIssuenceAccount").ToString)
            Dim GLAccountArticleDepartment As Boolean = Convert.ToBoolean(UtilityDAL.GetConfigValue("GLAccountArticleDepartment", trans).Replace("Error", "False"))

            If StoreIssuence.CGSAccountId > 0 Then
                CostOfGoodsAccount = StoreIssuence.CGSAccountId
            Else
                CostOfGoodsAccount = Convert.ToInt32(UtilityDAL.GetConfigValue("StoreIssuenceAccount", trans).ToString.Replace("Error", "0"))
            End If


            Dim str As String = String.Empty
            str = String.Empty
            str = "Select * From DispatchMasterTable WHERE DispatchNo=N'" & StoreIssuence.DispatchNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)


            If dt.Rows.Count > 0 Then


                str = String.Empty
                str = "Update PlanCostSheetDetailTable Set IssuedQty=(IsNull(IssuedQty,0)-IsNull(DispatchDetailTable.Sz1,0)) From PlanCostSheetDetailTable, DispatchDetailTable,DispatchMasterTable  WHERE  PlanCostSheetDetailTable.PlanId = DispatchMasterTable.PlanId AND DispatchMasterTable.DispatchId = DispatchDetailTable.DispatchId AND DispatchDetailTable.ArticleDefId = PlanCostSheetDetailTable.ArticleDefId AND PlanCostSheetDetailTable.ArticleMasterID = DispatchDetailTable.ArticleDefMasterId AND PlanCostSheetDetailTable.PlanId=" & StoreIssuence.PlanId & " AND DispatchMasterTable.PlanId=" & StoreIssuence.PlanId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = String.Empty
                str = "Delete From DispatchDetailTable WHERE DispatchId=" & dt.Rows(0).Item("DispatchId").ToString & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                str = String.Empty
                'str = " UPDATE DispatchMasterTable SET " _
                '& " LocationId=" & StoreIssuence.LocationId & ", " _
                '& " DispatchNo=N'" & StoreIssuence.DispatchNo & "', " _
                '& " DispatchDate=N'" & StoreIssuence.DispatchDate & "',  " _
                '& " VendorId=" & StoreIssuence.VendorId & " , " _
                '& " PurchaseOrderID=" & StoreIssuence.PurchaseOrderID & ", " _
                '& " PartyInvoiceNo=N'" & StoreIssuence.PartyInvoiceNo & "',  " _
                '& " PartySlipNo=N'" & StoreIssuence.PartySlipNo & "',  " _
                '& " DispatchQty=" & StoreIssuence.DispatchQty & ",  " _
                '& " DispatchAmount=" & StoreIssuence.DispatchAmount & ", " _
                '& " CashPaid=" & StoreIssuence.CashPaid & ", " _
                '& " Remarks=N'" & StoreIssuence.Remarks & "',  " _
                '& " UserName=N'" & StoreIssuence.UserName & "',  " _
                '& " RefDocument=N'" & StoreIssuence.RefDocument & "' " _
                '& " WHERE (DispatchId=" & dt.Rows(0).Item("DispatchId").ToString & ")"
                'ReqId-934 Resolve Comma Error
                str = " UPDATE DispatchMasterTable SET " _
               & " LocationId=" & StoreIssuence.LocationId & ", " _
               & " DispatchNo=N'" & StoreIssuence.DispatchNo & "', " _
               & " DispatchDate=N'" & StoreIssuence.DispatchDate & "',  " _
               & " VendorId=" & StoreIssuence.VendorId & " , " _
               & " PurchaseOrderID=" & StoreIssuence.PurchaseOrderID & ", " _
               & " PartyInvoiceNo=N'" & StoreIssuence.PartyInvoiceNo & "',  " _
               & " PartySlipNo=N'" & StoreIssuence.PartySlipNo & "',  " _
               & " DispatchQty=" & StoreIssuence.DispatchQty & ",  " _
               & " DispatchAmount=" & StoreIssuence.DispatchAmount & ", " _
               & " CashPaid=" & StoreIssuence.CashPaid & ", " _
               & " Remarks=N'" & StoreIssuence.Remarks.Replace("'", "''") & "',  " _
               & " UserName=N'" & StoreIssuence.UserName & "',  " _
               & " RefDocument=N'" & StoreIssuence.RefDocument.Replace("'", "''") & "',PlanId=" & StoreIssuence.PlanId & " " _
               & " WHERE (DispatchId=" & dt.Rows(0).Item("DispatchId").ToString & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Call AddDetail(StoreIssuence, trans, Convert.ToInt32(dt.Rows(0).Item("DispatchId").ToString))
            Else
                str = String.Empty
                'str = "INSERT INTO DispatchMasterTable(LocationId, DispatchNo, DispatchDate, VendorId, PurchaseOrderID, PartyInvoiceNo, PartySlipNo, DispatchQty, DispatchAmount, CashPaid, Remarks, UserName, RefDocument) " _
                '& " Values(" & StoreIssuence.LocationId & ", N'" & StoreIssuence.DispatchNo & "', N'" & StoreIssuence.DispatchDate & "'," & StoreIssuence.VendorId & ", " & StoreIssuence.PurchaseOrderID & ", N'" & StoreIssuence.PartyInvoiceNo & "', N'" & StoreIssuence.PartySlipNo & "', " & StoreIssuence.DispatchQty & ", " & StoreIssuence.DispatchAmount & ", " & StoreIssuence.CashPaid & ", N'" & StoreIssuence.Remarks & "', N'" & StoreIssuence.UserName & "', N'" & StoreIssuence.RefDocument & "') Select @@Identity"
                'ReqId-934 Resolve Comma Error
                StoreIssuence.DispatchNo = GetDocumentNo(StoreIssuence.DispatchDate, trans)
                str = "INSERT INTO DispatchMasterTable(LocationId, DispatchNo, DispatchDate, VendorId, PurchaseOrderID, PartyInvoiceNo, PartySlipNo, DispatchQty, DispatchAmount, CashPaid, Remarks, UserName, RefDocument) " _
               & " Values(" & StoreIssuence.LocationId & ", N'" & StoreIssuence.DispatchNo & "', N'" & StoreIssuence.DispatchDate & "'," & StoreIssuence.VendorId & ", " & StoreIssuence.PurchaseOrderID & ", N'" & StoreIssuence.PartyInvoiceNo.Replace("'", "''") & "', N'" & StoreIssuence.PartySlipNo.Replace("'", "''") & "', " & StoreIssuence.DispatchQty & ", " & StoreIssuence.DispatchAmount & ", " & StoreIssuence.CashPaid & ", N'" & StoreIssuence.Remarks.Replace("'", "''") & "', N'" & StoreIssuence.UserName & "', N'" & StoreIssuence.RefDocument.Replace("'", "''") & "') Select @@Identity"

                StoreIssuence.DispatchId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                Call AddDetail(StoreIssuence, trans, Convert.ToInt32(StoreIssuence.DispatchId))
            End If

            str = String.Empty
            'str = "UPDATE tblVoucher SET " _
            '& " location_id =" & StoreIssuence.LocationId & ", " _
            '& " voucher_code=N'" & StoreIssuence.DispatchNo & "', " _
            '& " voucher_type_id=1, " _
            '& " voucher_no= N'" & StoreIssuence.DispatchNo & "', " _
            '& " voucher_date=N'" & StoreIssuence.DispatchDate & "', " _
            '& " Post=" & IIf(StoreIssuence.Post = True, 1, 0) & ", " _
            '& " source='frmProductionStore', " _
            '& " Reference=N'" & StoreIssuence.Remarks & "',  " _
            '& " UserName=N'" & StoreIssuence.UserName & "' WHERE Voucher_ID=" & VoucherId & ""
            str = "UPDATE tblVoucher SET " _
           & " location_id =" & StoreIssuence.LocationId & ", " _
           & " voucher_code=N'" & StoreIssuence.DispatchNo & "', " _
           & " voucher_type_id=1, " _
           & " voucher_no= N'" & StoreIssuence.DispatchNo & "', " _
           & " voucher_date=N'" & StoreIssuence.DispatchDate & "', " _
           & " Post=" & IIf(StoreIssuence.Post = True, 1, 0) & ", " _
           & " source='frmProductionStore', " _
           & " Reference=N'" & StoreIssuence.Remarks.Replace("'", "''") & "',  " _
           & " UserName=N'" & StoreIssuence.UserName & "' WHERE Voucher_ID=" & VoucherId & ""
            SQLHelper.ExecuteScaler(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From tblVoucherDetail WHERE Voucher_ID=" & VoucherId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            For Each StoreIss As StoreIssuenceDetail In StoreIssuence.StoreIssuenceDetail
                If GLAccountArticleDepartment = True Then
                    PurchaseAccount = StoreIss.PurchaseAccountId  'Val(dtGrd.Rows(i).Item("PurchaseAccountId").ToString)
                End If
                'Purchase Debit Account Here ...
                str = String.Empty
                str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount, CostCenterID) " _
                & " Values(" & VoucherId & ", " & StoreIssuence.LocationId & ", " & CostOfGoodsAccount & ", 'Ref:" & StoreIssuence.DispatchNo & "', " & Val(StoreIss.Qty) * Val(StoreIss.Price) & ", 0, " & StoreIssuence.PurchaseOrderID & " )"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                'Store Credit Account Here ...
                str = String.Empty
                str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount, CostCenterID) " _
                & " Values(" & VoucherId & ", " & StoreIssuence.LocationId & ", " & PurchaseAccount & ", 'Ref:" & StoreIssuence.DispatchNo & "',0, " & Val(StoreIss.Qty) * Val(StoreIss.Price) & ", " & StoreIssuence.PurchaseOrderID & " )"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next



            If ValidateIssuanceVoucher(StoreIssuence.DispatchNo, trans) = False Then
                Throw New Exception("Record doesn't manage properly, Please check account mapping for store issuence voucher.")
            End If

            Call New StockDAL().Update(StoreIssuence.StockMaster, trans)

            trans.Commit()
            'If UtilityDAL.GetConfigValue("ProductionWithoutStoreIssuence").ToString = "True" Then
            'Call New StockDAL().Update(StoreIssuence.StockMaster)
            'End If
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal StoreIssuence As StoreIssuenceMaster) As Boolean
        Dim VoucherId As Integer = Convert.ToInt32(UtilityDAL.GetVoucherIdByVoucherNo("" & StoreIssuence.DispatchNo & ""))
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try


            Dim str As String = String.Empty

            str = "Select * From DispatchMasterTable WHERE DispatchNo=N'" & StoreIssuence.DispatchNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str, trans)
            dt.AcceptChanges()
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    str = "Delete From DispatchDetailTable WHERE DispatchId=" & dt.Rows(0).Item(0).ToString & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                    str = String.Empty
                    str = "Delete From DispatchMasterTable WHERE DispatchId=" & dt.Rows(0).Item(0).ToString & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            End If
            'Delete Voucher From tblVoucherDetail

            str = String.Empty
            str = "Update PlanCostSheetDetailTable Set IssuedQty=(IsNull(IssuedQty,0)-IsNull(DispatchDetailTable.Sz1,0)) From PlanCostSheetDetailTable, DispatchDetailTable,DispatchMasterTable  WHERE  PlanCostSheetDetailTable.PlanId = DispatchMasterTable.PlanId AND DispatchMasterTable.PlanId = DispatchDetailTable.PlanId AND DispatchDetailTable.ArticleDefId = PlanCostSheetDetailTable.ArticleDefId AND PlanCostSheetDetailTable.ArticleDefMasterID = DispatchDetailTable.ArticleDefMasterId AND PlanCostSheetDetailTable.PlanId=" & StoreIssuence.PlanId & " AND DispatchMasterTable.PlanId=" & StoreIssuence.PlanId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From tblVoucherDetail WHERE Voucher_ID=" & VoucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Delete Voucher From tblVoucherMaster
            str = String.Empty
            str = "Delete From tblVoucher WHERE Voucher_ID=" & VoucherId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            Call New StockDAL().Delete(StoreIssuence.StockMaster, trans)

            trans.Commit()
            'Call New StockDAL().Delete(StoreIssuence.StockMaster)
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Protected Function ValidateIssuanceVoucher(IssuenceNo As String, trans As SqlTransaction) As Boolean
        Dim cmd As New SqlCommand
        cmd.Connection = trans.Connection
        cmd.Transaction = trans
        cmd.CommandTimeout = 300
        cmd.CommandType = CommandType.Text
        Try
            cmd.CommandText = ""
            cmd.CommandText = "Select IsNull(SUM(IsNull(debit_amount,0)-IsNull(credit_amount,0)),0) as RemainingBalance From tblVoucher INNER JOIN tblVoucherDetail on tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id WHERE Voucher_No='" & IssuenceNo & "' AND tblVoucherDetail.coa_detail_Id <> 0"
            Dim value As Long = cmd.ExecuteScalar()

            If value > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Function GetDocumentNo(dtpPODate As DateTime, trans As SqlTransaction) As String
        Try
            'If Me.txtPONo.Text = "" Then
            If UtilityDAL.GetConfigValue("VoucherNo", trans).ToString = "Yearly" Then
                Return UtilityDAL.GetSerialNo("I" + "-" + Microsoft.VisualBasic.Right(dtpPODate.Year, 2) + "-", "DispatchMasterTable", "DispatchNo", trans)
            ElseIf UtilityDAL.GetConfigValue("VoucherNo", trans).ToString = "Monthly" Then
                Return UtilityDAL.GetNextDocNo("I" & "-" & Format(dtpPODate, "yy") & dtpPODate.Month.ToString("00"), 4, "DispatchMasterTable", "DispatchNo", trans)
            Else
                Return UtilityDAL.GetNextDocNo("I", 6, "DispatchMasterTable", "DispatchNo", trans)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

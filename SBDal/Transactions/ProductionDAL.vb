'' Task No 2555  Amendments to get Unit of Measurement  to get the vaue in cmbUOM
'' 8-May-2014 TASK:2613 Imran Ali Production Entry Problem
''16-June-2014 TASK:2690 Imran Ali Add Department and Employee Fields On Production Entry
''21-Jul-2014 TASK2753 Imran Ali Employee Field On Production Store
''30-06-2016 TASK-461 Muhammad Ameen: Update purchase rate from production
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient

Public Class ProductionDAL
    Public Shared GetRecordId As Integer = 0
    Public Shared GetRecordNo As String = String.Empty
    Public Shared CostOfSaleAmount As Double = 0D
    Public Function Add(ByVal ProductionStore As ProductionMaster, Optional ByVal CallFromProductionOrder As Boolean = False) As Boolean   'Task 3240 Add new argument CallFromProductionOrder that check whether this function call from production order page
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            'Dim PurchaseAccount As Integer = Convert.ToInt32(UtilityDAL.GetConfigValue("PurchaseDebitAccount").ToString)
            'Dim StoreAccount As Integer = 0I 'Convert.ToInt32(UtilityDAL.GetConfigValue("StoreCreditAccount").ToString)

            'If ProductionStore.CGSAccountId > 0 Then
            '    StoreAccount = ProductionStore.CGSAccountId
            'Else
            '    StoreAccount = Convert.ToInt32(UtilityDAL.GetConfigValue("StoreCreditAccount").ToString.Replace("Error", "0"))
            'End If
            'ProductionStore.CGSAccountId = StoreAccount
            Dim str As String = String.Empty
            ProductionStore.Production_No = GetDocumentNo(ProductionStore.Production_Date, trans)
            'Production Master Infomation Save 
            'Before against request no. 934
            'str = "Insert Into ProductionMasterTable(Production_date, Production_no,Production_store, CustomerCode,  Order_No,Project, TotalQty, TotalAmount, Remarks,UserName,FDate, IGPNo, Post, IssuedStore, RefDocument, RefDispatchNo, PlanId)  " _
            '& " Values(N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.Production_No & "', " & ProductionStore.Production_Store & ", " & ProductionStore.CustomerCode & ", " & ProductionStore.Order_No & ", " & ProductionStore.Project & ", " & ProductionStore.TotalQty & ", " & ProductionStore.TotalAmount & ",  N'" & ProductionStore.Remarks & "', N'" & ProductionStore.UserName & "', N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.IGPNo & "', " & IIf(ProductionStore.Post = True, 1, 0) & ", N'" & ProductionStore.IssuedStore & "', N'" & ProductionStore.RefDocument & "',N'" & ProductionStore.RefDispatchNo & "', " & ProductionStore.PlanId & ") Select @@Identity"
            'ReqId-934 Resolve comma error
            'If ProductionStore.RefDispatchNo = Nothing Then ProductionStore.RefDispatchNo = String.Empty
            'Before against task:2690
            'str = "Insert Into ProductionMasterTable(Production_date, Production_no,Production_store, CustomerCode,  Order_No,Project, TotalQty, TotalAmount, Remarks,UserName,FDate, IGPNo, Post, IssuedStore, RefDocument, RefDispatchNo, PlanId)  " _
            '& " Values(N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.Production_No.Replace("'", "''") & "', " & ProductionStore.Production_Store & ", " & ProductionStore.CustomerCode & ", " & ProductionStore.Order_No & ", " & ProductionStore.Project & ", " & ProductionStore.TotalQty & ", " & ProductionStore.TotalAmount & ",  N'" & ProductionStore.Remarks.Replace("'", "''") & "', N'" & ProductionStore.UserName & "', N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.IGPNo.Replace("'", "''") & "', " & IIf(ProductionStore.Post = True, 1, 0) & ", N'" & ProductionStore.IssuedStore & "', N'" & ProductionStore.RefDocument.Replace("'", "''") & "'," & IIf(ProductionStore.RefDispatchNo = Nothing, "NULL", "N'" & ProductionStore.RefDispatchNo.Replace("'", "''") & "'") & ", " & ProductionStore.PlanId & ") Select @@Identity"
            'TAsk:2690 Added Field DepartmentId
            'str = "Insert Into ProductionMasterTable(Production_date, Production_no,Production_store, CustomerCode,  Order_No,Project, TotalQty, TotalAmount, Remarks,UserName,FDate, IGPNo, Post, IssuedStore, RefDocument, RefDispatchNo, PlanId, DepartmentId)  " _
            '           & " Values(N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.Production_No.Replace("'", "''") & "', " & ProductionStore.Production_Store & ", " & ProductionStore.CustomerCode & ", " & ProductionStore.Order_No & ", " & ProductionStore.Project & ", " & ProductionStore.TotalQty & ", " & ProductionStore.TotalAmount & ",  N'" & ProductionStore.Remarks.Replace("'", "''") & "', N'" & ProductionStore.UserName & "', N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.IGPNo.Replace("'", "''") & "', " & IIf(ProductionStore.Post = True, 1, 0) & ", N'" & ProductionStore.IssuedStore & "', N'" & ProductionStore.RefDocument.Replace("'", "''") & "'," & IIf(ProductionStore.RefDispatchNo = Nothing, "NULL", "N'" & ProductionStore.RefDispatchNo.Replace("'", "''") & "'") & ", " & ProductionStore.PlanId & ", " & ProductionStore.DepartmentId & ") Select @@Identity"
            'End Task:2690
            ''21-Jul-2014 TASK2753 Imran Ali Employee Field On Production Store
            str = "Insert Into ProductionMasterTable(Production_date, Production_no,Production_store, CustomerCode,  Order_No,Project, TotalQty, TotalAmount, Remarks,UserName,FDate, IGPNo, Post, IssuedStore, RefDocument, RefDispatchNo, PlanId, DepartmentId,EmployeeID, CGSAccountId, PlanTicketId)  " _
                      & " Values(N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.Production_No.Replace("'", "''") & "', " & ProductionStore.Production_Store & ", " & ProductionStore.CustomerCode & ", " & ProductionStore.Order_No & ", " & ProductionStore.Project & ", " & ProductionStore.TotalQty & ", " & ProductionStore.TotalAmount & ",  N'" & ProductionStore.Remarks.Replace("'", "''") & "', N'" & ProductionStore.UserName & "', N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.IGPNo.Replace("'", "''") & "', " & IIf(ProductionStore.Post = True, 1, 0) & ", N'" & ProductionStore.IssuedStore & "', N'" & ProductionStore.RefDocument.Replace("'", "''") & "'," & IIf(ProductionStore.RefDispatchNo = Nothing, "NULL", "N'" & ProductionStore.RefDispatchNo.Replace("'", "''") & "'") & ", " & ProductionStore.PlanId & ", " & ProductionStore.DepartmentId & "," & ProductionStore.EmployeeID & "," & ProductionStore.CGSAccountId & ", " & ProductionStore.PlanTicketId & ") Select @@Identity"
            'End Task:2753
            ProductionStore.ProductionId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            GetRecordId = ProductionStore.ProductionId
            GetRecordNo = ProductionStore.Production_No
            'Call Function Production Detail Infomation
            'Task 3420 Saad Afaal CallFromProductionOrder set value true then call AddDetail function with true value
            If CallFromProductionOrder = True Then
                AddDetail(ProductionStore.ProductionId, trans, ProductionStore, True)
            Else
                AddDetail(ProductionStore.ProductionId, trans, ProductionStore)
            End If

            If CallFromProductionOrder = False Then

                If Not UtilityDAL.GetConfigValue("ProductionVoucher", trans).ToString = "Error" Then
                    If UtilityDAL.GetConfigValue("ProductionVoucher", trans).ToString = "True" Then
                        ''--------------------------------------------------- Production Voucher On/Off ---------------------
                        ''Voucher information here...
                        ''ProductionStore.TotalAmount = CostOfSaleAmount
                        'str = String.Empty
                        ''Before against ReqId-934
                        ''str = "INSERT INTO tblVoucher(location_id, voucher_code, voucher_type_id, voucher_no, voucher_date, Post, source, Reference, UserName) " _
                        ''& "Values(" & ProductionStore.Production_Store & ", N'" & ProductionStore.Production_No & "', 1, N'" & ProductionStore.Production_No & "', N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(ProductionStore.Post = True, 1, 0) & ", 'frmProductionStore', N'" & ProductionStore.Remarks & "', N'" & ProductionStore.UserName & "') Select @@Identity"
                        ''ReqId-934 Resolve comma error
                        'str = "INSERT INTO tblVoucher(location_id, voucher_code, voucher_type_id, voucher_no, voucher_date, Post, source, Reference, UserName) " _
                        '& "Values(" & ProductionStore.Production_Store & ", N'" & ProductionStore.Production_No & "', 1, N'" & ProductionStore.Production_No & "', N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(ProductionStore.Post = True, 1, 0) & ", 'frmProductionStore', N'" & ProductionStore.Remarks.Replace("'", "''") & "', N'" & ProductionStore.UserName & "') Select @@Identity"

                        'ProductionStore.ProductionId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                        ''Purchase Debit Account Here ...
                        'str = String.Empty
                        'str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount, CostCenterID) " _
                        '& "Values(" & ProductionStore.ProductionId & ", " & ProductionStore.Production_Store & ", " & PurchaseAccount & ", 'Ref:" & ProductionStore.Production_No & "', " & ProductionStore.TotalAmount & ", 0, " & ProductionStore.Project & " )"
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                        ''Store Credit Account Here ...
                        'str = String.Empty
                        'str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount, CostCenterID) " _
                        '& "Values(" & ProductionStore.ProductionId & ", " & ProductionStore.Production_Store & ", " & StoreAccount & ", 'Ref:" & ProductionStore.Production_No & "',0, " & ProductionStore.TotalAmount & ", " & ProductionStore.Project & " )"
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                        If ProductionStore.Post = True Then
                            Call New VouchersDAL().Add(ProductionStore.VoucherHead, trans)
                        End If

                    End If
                End If

                str = ""
                str = "Update PlanMasterTable Set ProductionStatus=Case When IsNull(a.RemainingQty,0) > 0 then 'Open' Else 'Close' End From PlanMasterTable, (Select PlanId, IsNull(SUM(IsNull(Sz1,0)-IsNull(ProducedQty,0)),0) as RemainingQty From PlanDetailTable WHERE PlanId=" & ProductionStore.PlanId & " Group by PlanId) a WHERE a.PlanId = PlanMasterTable.PlanId AND PlanMasterTable.PlanId=" & ProductionStore.PlanId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            End If


           

            'Call Stock DAL Object 
            Call New StockDAL().Add(ProductionStore.StockMaster, trans)

            If CallFromProductionOrder = False Then

                If UtilityDAL.GetConfigValue("StoreIssuenceWithProduction", trans).ToString = "True" Then
                    'Store Issuance
                    Call AddStoreIssuance(ProductionStore, trans)
                End If

            End If

            trans.Commit()
            ''Call Stock DAL Object 
            'Call New StockDAL().Add(ProductionStore.StockMaster)
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
            CostOfSaleAmount = 0D
        End Try
    End Function
    Public Function Add(ByVal ProductionStore As ProductionMaster, ByVal trans As SqlTransaction) As Integer
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            'Dim PurchaseAccount As Integer = Convert.ToInt32(UtilityDAL.GetConfigValue("PurchaseDebitAccount").ToString)
            'Dim StoreAccount As Integer = 0I 'Convert.ToInt32(UtilityDAL.GetConfigValue("StoreCreditAccount").ToString)

            'If ProductionStore.CGSAccountId > 0 Then
            '    StoreAccount = ProductionStore.CGSAccountId
            'Else
            '    StoreAccount = Convert.ToInt32(UtilityDAL.GetConfigValue("StoreCreditAccount").ToString.Replace("Error", "0"))
            'End If
            'ProductionStore.CGSAccountId = StoreAccount
            Dim str As String = String.Empty
            ProductionStore.Production_No = GetDocumentNo(ProductionStore.Production_Date, trans)
            'Production Master Infomation Save 
            'Before against request no. 934
            'str = "Insert Into ProductionMasterTable(Production_date, Production_no,Production_store, CustomerCode,  Order_No,Project, TotalQty, TotalAmount, Remarks,UserName,FDate, IGPNo, Post, IssuedStore, RefDocument, RefDispatchNo, PlanId)  " _
            '& " Values(N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.Production_No & "', " & ProductionStore.Production_Store & ", " & ProductionStore.CustomerCode & ", " & ProductionStore.Order_No & ", " & ProductionStore.Project & ", " & ProductionStore.TotalQty & ", " & ProductionStore.TotalAmount & ",  N'" & ProductionStore.Remarks & "', N'" & ProductionStore.UserName & "', N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.IGPNo & "', " & IIf(ProductionStore.Post = True, 1, 0) & ", N'" & ProductionStore.IssuedStore & "', N'" & ProductionStore.RefDocument & "',N'" & ProductionStore.RefDispatchNo & "', " & ProductionStore.PlanId & ") Select @@Identity"
            'ReqId-934 Resolve comma error
            'If ProductionStore.RefDispatchNo = Nothing Then ProductionStore.RefDispatchNo = String.Empty
            'Before against task:2690
            'str = "Insert Into ProductionMasterTable(Production_date, Production_no,Production_store, CustomerCode,  Order_No,Project, TotalQty, TotalAmount, Remarks,UserName,FDate, IGPNo, Post, IssuedStore, RefDocument, RefDispatchNo, PlanId)  " _
            '& " Values(N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.Production_No.Replace("'", "''") & "', " & ProductionStore.Production_Store & ", " & ProductionStore.CustomerCode & ", " & ProductionStore.Order_No & ", " & ProductionStore.Project & ", " & ProductionStore.TotalQty & ", " & ProductionStore.TotalAmount & ",  N'" & ProductionStore.Remarks.Replace("'", "''") & "', N'" & ProductionStore.UserName & "', N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.IGPNo.Replace("'", "''") & "', " & IIf(ProductionStore.Post = True, 1, 0) & ", N'" & ProductionStore.IssuedStore & "', N'" & ProductionStore.RefDocument.Replace("'", "''") & "'," & IIf(ProductionStore.RefDispatchNo = Nothing, "NULL", "N'" & ProductionStore.RefDispatchNo.Replace("'", "''") & "'") & ", " & ProductionStore.PlanId & ") Select @@Identity"
            'TAsk:2690 Added Field DepartmentId
            'str = "Insert Into ProductionMasterTable(Production_date, Production_no,Production_store, CustomerCode,  Order_No,Project, TotalQty, TotalAmount, Remarks,UserName,FDate, IGPNo, Post, IssuedStore, RefDocument, RefDispatchNo, PlanId, DepartmentId)  " _
            '           & " Values(N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.Production_No.Replace("'", "''") & "', " & ProductionStore.Production_Store & ", " & ProductionStore.CustomerCode & ", " & ProductionStore.Order_No & ", " & ProductionStore.Project & ", " & ProductionStore.TotalQty & ", " & ProductionStore.TotalAmount & ",  N'" & ProductionStore.Remarks.Replace("'", "''") & "', N'" & ProductionStore.UserName & "', N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.IGPNo.Replace("'", "''") & "', " & IIf(ProductionStore.Post = True, 1, 0) & ", N'" & ProductionStore.IssuedStore & "', N'" & ProductionStore.RefDocument.Replace("'", "''") & "'," & IIf(ProductionStore.RefDispatchNo = Nothing, "NULL", "N'" & ProductionStore.RefDispatchNo.Replace("'", "''") & "'") & ", " & ProductionStore.PlanId & ", " & ProductionStore.DepartmentId & ") Select @@Identity"
            'End Task:2690
            ''21-Jul-2014 TASK2753 Imran Ali Employee Field On Production Store
            str = "Insert Into ProductionMasterTable(Production_date, Production_no,Production_store, CustomerCode,  Order_No,Project, TotalQty, TotalAmount, Remarks,UserName,FDate, IGPNo, Post, IssuedStore, RefDocument, RefDispatchNo, PlanId, DepartmentId,EmployeeID, CGSAccountId, PlanTicketId)  " _
                      & " Values(N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.Production_No.Replace("'", "''") & "', " & ProductionStore.Production_Store & ", " & ProductionStore.CustomerCode & ", " & ProductionStore.Order_No & ", " & ProductionStore.Project & ", " & ProductionStore.TotalQty & ", " & ProductionStore.TotalAmount & ",  N'" & ProductionStore.Remarks.Replace("'", "''") & "', N'" & ProductionStore.UserName & "', N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & ProductionStore.IGPNo.Replace("'", "''") & "', " & IIf(ProductionStore.Post = True, 1, 0) & ", N'" & ProductionStore.IssuedStore & "', N'" & ProductionStore.RefDocument.Replace("'", "''") & "'," & IIf(ProductionStore.RefDispatchNo = Nothing, "NULL", "N'" & ProductionStore.RefDispatchNo.Replace("'", "''") & "'") & ", " & ProductionStore.PlanId & ", " & ProductionStore.DepartmentId & "," & ProductionStore.EmployeeID & "," & ProductionStore.CGSAccountId & ", " & ProductionStore.PlanTicketId & ") Select @@Identity"
            'End Task:2753
            ProductionStore.ProductionId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            GetRecordId = ProductionStore.ProductionId
            GetRecordNo = ProductionStore.Production_No
            'Call Function Production Detail Infomation  
            AddDetail(ProductionStore.ProductionId, trans, ProductionStore)

            If Not UtilityDAL.GetConfigValue("ProductionVoucher", trans).ToString = "Error" Then
                If UtilityDAL.GetConfigValue("ProductionVoucher", trans).ToString = "True" Then
                    ''--------------------------------------------------- Production Voucher On/Off ---------------------
                    ''Voucher information here...
                    ''ProductionStore.TotalAmount = CostOfSaleAmount
                    'str = String.Empty
                    ''Before against ReqId-934
                    ''str = "INSERT INTO tblVoucher(location_id, voucher_code, voucher_type_id, voucher_no, voucher_date, Post, source, Reference, UserName) " _
                    ''& "Values(" & ProductionStore.Production_Store & ", N'" & ProductionStore.Production_No & "', 1, N'" & ProductionStore.Production_No & "', N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(ProductionStore.Post = True, 1, 0) & ", 'frmProductionStore', N'" & ProductionStore.Remarks & "', N'" & ProductionStore.UserName & "') Select @@Identity"
                    ''ReqId-934 Resolve comma error
                    'str = "INSERT INTO tblVoucher(location_id, voucher_code, voucher_type_id, voucher_no, voucher_date, Post, source, Reference, UserName) " _
                    '& "Values(" & ProductionStore.Production_Store & ", N'" & ProductionStore.Production_No & "', 1, N'" & ProductionStore.Production_No & "', N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(ProductionStore.Post = True, 1, 0) & ", 'frmProductionStore', N'" & ProductionStore.Remarks.Replace("'", "''") & "', N'" & ProductionStore.UserName & "') Select @@Identity"

                    'ProductionStore.ProductionId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
                    ''Purchase Debit Account Here ...
                    'str = String.Empty
                    'str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount, CostCenterID) " _
                    '& "Values(" & ProductionStore.ProductionId & ", " & ProductionStore.Production_Store & ", " & PurchaseAccount & ", 'Ref:" & ProductionStore.Production_No & "', " & ProductionStore.TotalAmount & ", 0, " & ProductionStore.Project & " )"
                    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    ''Store Credit Account Here ...
                    'str = String.Empty
                    'str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount, CostCenterID) " _
                    '& "Values(" & ProductionStore.ProductionId & ", " & ProductionStore.Production_Store & ", " & StoreAccount & ", 'Ref:" & ProductionStore.Production_No & "',0, " & ProductionStore.TotalAmount & ", " & ProductionStore.Project & " )"
                    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    If ProductionStore.Post = True Then
                        Call New VouchersDAL().Add(ProductionStore.VoucherHead, trans)
                    End If

                End If
            End If


            str = ""
            str = "Update PlanMasterTable Set ProductionStatus=Case When IsNull(a.RemainingQty,0) > 0 then 'Open' Else 'Close' End From PlanMasterTable, (Select PlanId, IsNull(SUM(IsNull(Sz1,0)-IsNull(ProducedQty,0)),0) as RemainingQty From PlanDetailTable WHERE PlanId=" & ProductionStore.PlanId & " Group by PlanId) a WHERE a.PlanId = PlanMasterTable.PlanId AND PlanMasterTable.PlanId=" & ProductionStore.PlanId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Call Stock DAL Object 
            If ProductionStore.StockMaster IsNot Nothing Then
                Call New StockDAL().Add(ProductionStore.StockMaster, trans)
            End If

            If UtilityDAL.GetConfigValue("StoreIssuenceWithProduction", trans).ToString = "True" Then
                'Store Issuance
                Call AddStoreIssuance(ProductionStore, trans)
            End If


            'trans.Commit()
            ''Call Stock DAL Object 
            'Call New StockDAL().Add(ProductionStore.StockMaster)
            Return ProductionStore.ProductionId
        Catch ex As Exception
            'trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
            'CostOfSaleAmount = 0D
        End Try
    End Function
    Public Function Update(ByVal ProductionStore As ProductionMaster, Optional ByVal CallFromProductionOrder As Boolean = False) As Boolean  'Task 3240 Add new argument CallFromProductionOrder that check whether this function call from production order page
        Dim VoucherId As Integer = Convert.ToInt32(UtilityDAL.GetVoucherIdByVoucherNo("" & ProductionStore.Production_No & ""))
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            'Dim PurchaseAccount As Integer = Convert.ToInt32(UtilityDAL.GetConfigValue("PurchaseDebitAccount").ToString)
            'Dim StoreAccount As Integer = 0I 'Convert.ToInt32(UtilityDAL.GetConfigValue("StoreCreditAccount").ToString)

            'If ProductionStore.CGSAccountId > 0 Then
            '    StoreAccount = ProductionStore.CGSAccountId
            'Else
            '    StoreAccount = Convert.ToInt32(UtilityDAL.GetConfigValue("StoreCreditAccount", trans).ToString.Replace("Error", "0"))
            'End If
            'ProductionStore.CGSAccountId = StoreAccount

            Dim str As String = String.Empty
            If ProductionStore.RefDispatchNo = Nothing Then ProductionStore.RefDispatchNo = String.Empty
            'Production Master Infomation Update 
            'str = "Update ProductionMasterTable Set Production_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', Production_no=N'" & ProductionStore.Production_No & "', Production_store=" & ProductionStore.Production_Store & ", CustomerCode=" & ProductionStore.CustomerCode & ", Order_No=" & ProductionStore.Order_No & ", Project=" & ProductionStore.Project & ", TotalQty=" & ProductionStore.TotalQty & ", TotalAmount=" & ProductionStore.TotalAmount & ",    Remarks=N'" & ProductionStore.Remarks & "', UserName=N'" & ProductionStore.UserName & "', FDate=N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', IGPNo=N'" & ProductionStore.IGPNo & "', Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", IssuedStore=N'" & ProductionStore.IssuedStore & "', RefDocument=N'" & ProductionStore.RefDocument & "', RefDispatchNo=N'" & ProductionStore.RefDispatchNo & "', PlanId=" & ProductionStore.PlanId & " WHERE Production_Id=" & ProductionStore.ProductionId & " "
            'str = "Update ProductionMasterTable Set Production_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', Production_no=N'" & ProductionStore.Production_No & "', Production_store=" & ProductionStore.Production_Store & ", CustomerCode=" & ProductionStore.CustomerCode & ", Order_No=" & ProductionStore.Order_No & ", Project=" & ProductionStore.Project & ", TotalQty=" & ProductionStore.TotalQty & ", TotalAmount=" & ProductionStore.TotalAmount & ",    Remarks=N'" & ProductionStore.Remarks.Replace("'", "''") & "', UserName=N'" & ProductionStore.UserName & "', FDate=N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', IGPNo=N'" & ProductionStore.IGPNo.Replace("'", "''") & "', Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", IssuedStore=N'" & ProductionStore.IssuedStore & "', RefDocument=N'" & ProductionStore.RefDocument.Replace("'", "''") & "', RefDispatchNo=N'" & ProductionStore.RefDispatchNo.Replace("'", "''") & "', PlanId=" & ProductionStore.PlanId & " WHERE Production_Id=" & ProductionStore.ProductionId & " "
            ''21-Jul-2014 TASK2753 Imran Ali Employee Field On Production Store

            If CallFromProductionOrder = True Then
                str = "Update ProductionMasterTable Set Production_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "' , Production_store=" & ProductionStore.Production_Store & ", CustomerCode=" & ProductionStore.CustomerCode & ", Order_No=" & ProductionStore.Order_No & ", Project=" & ProductionStore.Project & ", TotalQty=" & ProductionStore.TotalQty & ", TotalAmount=" & ProductionStore.TotalAmount & ",    Remarks=N'" & ProductionStore.Remarks.Replace("'", "''") & "', UpdateUserName=N'" & ProductionStore.UserName & "', FDate=N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', IGPNo=N'" & ProductionStore.IGPNo.Replace("'", "''") & "', Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", IssuedStore=N'" & ProductionStore.IssuedStore & "', RefDocument=N'" & ProductionStore.RefDocument.Replace("'", "''") & "', RefDispatchNo=N'" & ProductionStore.RefDispatchNo.Replace("'", "''") & "', PlanId=" & ProductionStore.PlanId & ",EmployeeID=" & ProductionStore.EmployeeID & ", CGSAccountId=" & ProductionStore.CGSAccountId & ", PlanTicketId = " & ProductionStore.PlanTicketId & " WHERE Production_Id=" & ProductionStore.ProductionId & " "
            Else
                str = "Update ProductionMasterTable Set Production_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', Production_no=N'" & ProductionStore.Production_No & "', Production_store=" & ProductionStore.Production_Store & ", CustomerCode=" & ProductionStore.CustomerCode & ", Order_No=" & ProductionStore.Order_No & ", Project=" & ProductionStore.Project & ", TotalQty=" & ProductionStore.TotalQty & ", TotalAmount=" & ProductionStore.TotalAmount & ",    Remarks=N'" & ProductionStore.Remarks.Replace("'", "''") & "', UpdateUserName=N'" & ProductionStore.UserName & "', FDate=N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', IGPNo=N'" & ProductionStore.IGPNo.Replace("'", "''") & "', Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", IssuedStore=N'" & ProductionStore.IssuedStore & "', RefDocument=N'" & ProductionStore.RefDocument.Replace("'", "''") & "', RefDispatchNo=N'" & ProductionStore.RefDispatchNo.Replace("'", "''") & "', PlanId=" & ProductionStore.PlanId & ",EmployeeID=" & ProductionStore.EmployeeID & ", CGSAccountId=" & ProductionStore.CGSAccountId & ", PlanTicketId = " & ProductionStore.PlanTicketId & " WHERE Production_Id=" & ProductionStore.ProductionId & " "
            End If

            'End Task:2753
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            GetRecordId = ProductionStore.ProductionId
            GetRecordNo = ProductionStore.Production_No
            'Delete Previouse Data from Production Detail Delete

            If CallFromProductionOrder = False Then
                str = String.Empty
                str = "Update PlanDetailTable Set ProducedQty=IsNull(ProducedQty,0)-IsNull(ProductionDetailTable.Sz1,0),  ProducedTotalQty=IsNull(ProducedTotalQty,0)-IsNull(ProductionDetailTable.Qty,0) From PlanDetailTable, ProductionDetailTable WHERE ProductionDetailTable.PlanDetailId = PlanDetailTable.PlanDetailId AND ProductionDetailTable.ArticleDefId = PlanDetailTable.ArticleDefId And PlanDetailTable.PlanId=" & ProductionStore.PlanId & "" ''TASK-408 added new column ProducedTotalQty to be updated too. Dated 14-06-2016
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            End If

            str = String.Empty
            str = "Delete From ProductionDetailTable WHERE Production_Id=" & ProductionStore.ProductionId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Voucher Data from tblVoucher Detail....
            'str = String.Empty
            'str = "Delete From tblVoucherDetail WHERE Voucher_ID=" & VoucherId & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Call Function Production Detail Infomation  

            If CallFromProductionOrder = True Then
                AddDetail(ProductionStore.ProductionId, trans, ProductionStore, True)
            Else
                AddDetail(ProductionStore.ProductionId, trans, ProductionStore)
            End If

            If CallFromProductionOrder = False Then

                'Voucher information here...
                If Not UtilityDAL.GetConfigValue("ProductionVoucher", trans).ToString = "Error" Then
                    If UtilityDAL.GetConfigValue("ProductionVoucher", trans).ToString = "True" Then
                        '--------------------------------------------------- Production Voucher On/Off ---------------------
                        ' str = String.Empty
                        ' 'ProductionStore.TotalAmount = CostOfSaleAmount
                        ' 'str = "UPDATE tblVoucher SET " _
                        ' '& " location_id =" & ProductionStore.Production_Store & ", " _
                        ' '& " voucher_code=N'" & ProductionStore.Production_No & "', " _
                        ' '& " voucher_type_id=1, " _
                        ' '& " voucher_no= N'" & ProductionStore.Production_No & "', " _
                        ' '& " voucher_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                        ' '& " Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", " _
                        ' '& " source='frmProductionStore', " _
                        ' '& " Reference=N'" & ProductionStore.Remarks & "',  " _
                        ' '& " UserName=N'" & ProductionStore.UserName & "' WHERE Voucher_ID=" & VoucherId & ""
                        ' 'Before against task:2690
                        ' 'str = "UPDATE tblVoucher SET " _
                        ' '& " location_id =" & ProductionStore.Production_Store & ", " _
                        ' '& " voucher_code=N'" & ProductionStore.Production_No & "', " _
                        ' '& " voucher_type_id=1, " _
                        ' '& " voucher_no= N'" & ProductionStore.Production_No & "', " _
                        ' '& " voucher_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                        ' '& " Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", " _
                        ' '& " source='frmProductionStore', " _
                        ' '& " Reference=N'" & ProductionStore.Remarks.Replace("'", "''") & "',  " _
                        ' '& " UserName=N'" & ProductionStore.UserName & "' WHERE Voucher_ID=" & VoucherId & ""
                        ' 'Task:2690 Added Field DepartmentId
                        ' str = "UPDATE tblVoucher SET " _
                        '& " location_id =" & ProductionStore.Production_Store & ", " _
                        '& " voucher_code=N'" & ProductionStore.Production_No & "', " _
                        '& " voucher_type_id=1, " _
                        '& " voucher_no= N'" & ProductionStore.Production_No & "', " _
                        '& " voucher_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                        '& " Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", " _
                        '& " source='frmProductionStore', " _
                        '& " Reference=N'" & ProductionStore.Remarks.Replace("'", "''") & "',  " _
                        '& " UserName=N'" & ProductionStore.UserName & "', DepartmentId=" & ProductionStore.DepartmentId & " WHERE Voucher_ID=" & VoucherId & ""
                        ' SQLHelper.ExecuteScaler(trans, CommandType.Text, str)


                        ' str = String.Empty
                        ' str = "Delete From tblVoucherDetail WHERE Voucher_Id=" & VoucherId & ""
                        ' SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                        ' ' Purchase Debit Account Here ...
                        ' str = String.Empty
                        ' str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount, CostCenterID) " _
                        ' & "Values(" & VoucherId & ", " & ProductionStore.Production_Store & ", " & PurchaseAccount & ", 'Ref:" & ProductionStore.Production_No & "', " & ProductionStore.TotalAmount & ", 0, " & ProductionStore.Project & " )"
                        ' SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                        ' 'Store Credit Account Here ...
                        ' str = String.Empty
                        ' str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount, CostCenterID) " _
                        ' & "Values(" & VoucherId & ", " & ProductionStore.Production_Store & ", " & StoreAccount & ", 'Ref:" & ProductionStore.Production_No & "',0, " & ProductionStore.TotalAmount & ", " & ProductionStore.Project & " )"
                        ' SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                        If ProductionStore.Post = True Then
                            Call New VouchersDAL().Update(ProductionStore.VoucherHead, trans)
                        Else

                            str = String.Empty
                            str = "Delete From tblVoucherDetail WHERE Voucher_Id In(Select Voucher_ID From tblVoucher WHERE Voucher_No=N'" & ProductionStore.Production_No & "')"
                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                            str = String.Empty
                            str = "Delete From tblVoucher WHERE Voucher_No=N'" & ProductionStore.Production_No & "'"
                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                        End If

                    End If
                End If

                str = ""
                str = "Update PlanMasterTable Set ProductionStatus=Case When IsNull(a.RemainingQty,0) > 0 then 'Open' Else 'Close' End From PlanMasterTable, (Select PlanId, IsNull(SUM(IsNull(Sz1,0)-IsNull(ProducedQty,0)),0) as RemainingQty From PlanDetailTable WHERE PlanId=" & ProductionStore.PlanId & " Group by PlanId) a WHERE a.PlanId = PlanMasterTable.PlanId AND PlanMasterTable.PlanId=" & ProductionStore.PlanId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            End If

            'Call Stock DAL Object
            Call New StockDAL().Update(ProductionStore.StockMaster, trans)

            If CallFromProductionOrder = False Then
                If UtilityDAL.GetConfigValue("StoreIssuenceWithProduction", trans).ToString = "True" Then
                    'Store Issuance
                    Call AddStoreIssuance(ProductionStore, trans)
                End If
            End If

            trans.Commit()
            ''Call Stock DAL Object
            'Call New StockDAL().Update(ProductionStore.StockMaster)
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
            CostOfSaleAmount = 0D
        End Try
    End Function
    Public Function Update(ByVal ProductionStore As ProductionMaster, ByVal trans As SqlTransaction) As Integer
        'Dim VoucherId As Integer = Convert.ToInt32(UtilityDAL.GetVoucherIdByVoucherNo("" & ProductionStore.Production_No & ""))
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            'Dim PurchaseAccount As Integer = Convert.ToInt32(UtilityDAL.GetConfigValue("PurchaseDebitAccount").ToString)
            'Dim StoreAccount As Integer = 0I 'Convert.ToInt32(UtilityDAL.GetConfigValue("StoreCreditAccount").ToString)

            'If ProductionStore.CGSAccountId > 0 Then
            '    StoreAccount = ProductionStore.CGSAccountId
            'Else
            '    StoreAccount = Convert.ToInt32(UtilityDAL.GetConfigValue("StoreCreditAccount", trans).ToString.Replace("Error", "0"))
            'End If
            'ProductionStore.CGSAccountId = StoreAccount

            Dim str As String = String.Empty
            If ProductionStore.RefDispatchNo = Nothing Then ProductionStore.RefDispatchNo = String.Empty
            'Production Master Infomation Update 
            'str = "Update ProductionMasterTable Set Production_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', Production_no=N'" & ProductionStore.Production_No & "', Production_store=" & ProductionStore.Production_Store & ", CustomerCode=" & ProductionStore.CustomerCode & ", Order_No=" & ProductionStore.Order_No & ", Project=" & ProductionStore.Project & ", TotalQty=" & ProductionStore.TotalQty & ", TotalAmount=" & ProductionStore.TotalAmount & ",    Remarks=N'" & ProductionStore.Remarks & "', UserName=N'" & ProductionStore.UserName & "', FDate=N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', IGPNo=N'" & ProductionStore.IGPNo & "', Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", IssuedStore=N'" & ProductionStore.IssuedStore & "', RefDocument=N'" & ProductionStore.RefDocument & "', RefDispatchNo=N'" & ProductionStore.RefDispatchNo & "', PlanId=" & ProductionStore.PlanId & " WHERE Production_Id=" & ProductionStore.ProductionId & " "
            'str = "Update ProductionMasterTable Set Production_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', Production_no=N'" & ProductionStore.Production_No & "', Production_store=" & ProductionStore.Production_Store & ", CustomerCode=" & ProductionStore.CustomerCode & ", Order_No=" & ProductionStore.Order_No & ", Project=" & ProductionStore.Project & ", TotalQty=" & ProductionStore.TotalQty & ", TotalAmount=" & ProductionStore.TotalAmount & ",    Remarks=N'" & ProductionStore.Remarks.Replace("'", "''") & "', UserName=N'" & ProductionStore.UserName & "', FDate=N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', IGPNo=N'" & ProductionStore.IGPNo.Replace("'", "''") & "', Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", IssuedStore=N'" & ProductionStore.IssuedStore & "', RefDocument=N'" & ProductionStore.RefDocument.Replace("'", "''") & "', RefDispatchNo=N'" & ProductionStore.RefDispatchNo.Replace("'", "''") & "', PlanId=" & ProductionStore.PlanId & " WHERE Production_Id=" & ProductionStore.ProductionId & " "
            ''21-Jul-2014 TASK2753 Imran Ali Employee Field On Production Store
            str = "Update ProductionMasterTable Set Production_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', Production_no=N'" & ProductionStore.Production_No & "', Production_store=" & ProductionStore.Production_Store & ", CustomerCode=" & ProductionStore.CustomerCode & ", Order_No=" & ProductionStore.Order_No & ", Project=" & ProductionStore.Project & ", TotalQty=" & ProductionStore.TotalQty & ", TotalAmount=" & ProductionStore.TotalAmount & ",    Remarks=N'" & ProductionStore.Remarks.Replace("'", "''") & "', UpdateUserName=N'" & ProductionStore.UserName & "', FDate=N'" & ProductionStore.FDate.ToString("yyyy-M-d h:mm:ss tt") & "', IGPNo=N'" & ProductionStore.IGPNo.Replace("'", "''") & "', Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", IssuedStore=N'" & ProductionStore.IssuedStore & "', RefDocument=N'" & ProductionStore.RefDocument.Replace("'", "''") & "', RefDispatchNo=N'" & ProductionStore.RefDispatchNo.Replace("'", "''") & "', PlanId=" & ProductionStore.PlanId & ",EmployeeID=" & ProductionStore.EmployeeID & ", CGSAccountId=" & ProductionStore.CGSAccountId & ", PlanTicketId = " & ProductionStore.PlanTicketId & " WHERE Production_Id=" & ProductionStore.ProductionId & " "
            'End Task:2753
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            GetRecordId = ProductionStore.ProductionId
            GetRecordNo = ProductionStore.Production_No
            'Delete Previouse Data from Production Detail Delete

            str = String.Empty
            str = "Update PlanDetailTable Set ProducedQty=IsNull(ProducedQty,0)-IsNull(ProductionDetailTable.Sz1,0),  ProducedTotalQty=IsNull(ProducedTotalQty,0)-IsNull(ProductionDetailTable.Qty,0) From PlanDetailTable, ProductionDetailTable WHERE ProductionDetailTable.PlanDetailId = PlanDetailTable.PlanDetailId AND ProductionDetailTable.ArticleDefId = PlanDetailTable.ArticleDefId And PlanDetailTable.PlanId=" & ProductionStore.PlanId & "" ''TASK-408 added new column ProducedTotalQty to be updated too. Dated 14-06-2016
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From ProductionDetailTable WHERE Production_Id=" & ProductionStore.ProductionId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Voucher Data from tblVoucher Detail....
            'str = String.Empty
            'str = "Delete From tblVoucherDetail WHERE Voucher_ID=" & VoucherId & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Call Function Production Detail Infomation  

            AddDetail(ProductionStore.ProductionId, trans, ProductionStore)
            'Voucher information here...
            If Not UtilityDAL.GetConfigValue("ProductionVoucher", trans).ToString = "Error" Then
                If UtilityDAL.GetConfigValue("ProductionVoucher", trans).ToString = "True" Then
                    '--------------------------------------------------- Production Voucher On/Off ---------------------
                    ' str = String.Empty
                    ' 'ProductionStore.TotalAmount = CostOfSaleAmount
                    ' 'str = "UPDATE tblVoucher SET " _
                    ' '& " location_id =" & ProductionStore.Production_Store & ", " _
                    ' '& " voucher_code=N'" & ProductionStore.Production_No & "', " _
                    ' '& " voucher_type_id=1, " _
                    ' '& " voucher_no= N'" & ProductionStore.Production_No & "', " _
                    ' '& " voucher_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                    ' '& " Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", " _
                    ' '& " source='frmProductionStore', " _
                    ' '& " Reference=N'" & ProductionStore.Remarks & "',  " _
                    ' '& " UserName=N'" & ProductionStore.UserName & "' WHERE Voucher_ID=" & VoucherId & ""
                    ' 'Before against task:2690
                    ' 'str = "UPDATE tblVoucher SET " _
                    ' '& " location_id =" & ProductionStore.Production_Store & ", " _
                    ' '& " voucher_code=N'" & ProductionStore.Production_No & "', " _
                    ' '& " voucher_type_id=1, " _
                    ' '& " voucher_no= N'" & ProductionStore.Production_No & "', " _
                    ' '& " voucher_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                    ' '& " Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", " _
                    ' '& " source='frmProductionStore', " _
                    ' '& " Reference=N'" & ProductionStore.Remarks.Replace("'", "''") & "',  " _
                    ' '& " UserName=N'" & ProductionStore.UserName & "' WHERE Voucher_ID=" & VoucherId & ""
                    ' 'Task:2690 Added Field DepartmentId
                    ' str = "UPDATE tblVoucher SET " _
                    '& " location_id =" & ProductionStore.Production_Store & ", " _
                    '& " voucher_code=N'" & ProductionStore.Production_No & "', " _
                    '& " voucher_type_id=1, " _
                    '& " voucher_no= N'" & ProductionStore.Production_No & "', " _
                    '& " voucher_date=N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                    '& " Post=" & IIf(ProductionStore.Post = True, 1, 0) & ", " _
                    '& " source='frmProductionStore', " _
                    '& " Reference=N'" & ProductionStore.Remarks.Replace("'", "''") & "',  " _
                    '& " UserName=N'" & ProductionStore.UserName & "', DepartmentId=" & ProductionStore.DepartmentId & " WHERE Voucher_ID=" & VoucherId & ""
                    ' SQLHelper.ExecuteScaler(trans, CommandType.Text, str)


                    ' str = String.Empty
                    ' str = "Delete From tblVoucherDetail WHERE Voucher_Id=" & VoucherId & ""
                    ' SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    ' ' Purchase Debit Account Here ...
                    ' str = String.Empty
                    ' str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount, CostCenterID) " _
                    ' & "Values(" & VoucherId & ", " & ProductionStore.Production_Store & ", " & PurchaseAccount & ", 'Ref:" & ProductionStore.Production_No & "', " & ProductionStore.TotalAmount & ", 0, " & ProductionStore.Project & " )"
                    ' SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    ' 'Store Credit Account Here ...
                    ' str = String.Empty
                    ' str = "INSERT INTO tblVoucherDetail(Voucher_Id, location_id, coa_detail_id, comments, debit_amount, credit_amount, CostCenterID) " _
                    ' & "Values(" & VoucherId & ", " & ProductionStore.Production_Store & ", " & StoreAccount & ", 'Ref:" & ProductionStore.Production_No & "',0, " & ProductionStore.TotalAmount & ", " & ProductionStore.Project & " )"
                    ' SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    If ProductionStore.Post = True Then
                        Call New VouchersDAL().Update(ProductionStore.VoucherHead, trans)
                    Else

                        str = String.Empty
                        str = "Delete From tblVoucherDetail WHERE Voucher_Id In(Select Voucher_ID From tblVoucher WHERE Voucher_No=N'" & ProductionStore.Production_No & "')"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                        str = String.Empty
                        str = "Delete From tblVoucher WHERE Voucher_No=N'" & ProductionStore.Production_No & "'"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                    End If

                End If
            End If

            str = ""
            str = "Update PlanMasterTable Set ProductionStatus=Case When IsNull(a.RemainingQty,0) > 0 then 'Open' Else 'Close' End From PlanMasterTable, (Select PlanId, IsNull(SUM(IsNull(Sz1,0)-IsNull(ProducedQty,0)),0) as RemainingQty From PlanDetailTable WHERE PlanId=" & ProductionStore.PlanId & " Group by PlanId) a WHERE a.PlanId = PlanMasterTable.PlanId AND PlanMasterTable.PlanId=" & ProductionStore.PlanId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Call Stock DAL Object
            If ProductionStore.StockMaster IsNot Nothing Then
                Call New StockDAL().Update(ProductionStore.StockMaster, trans)
            End If
            If UtilityDAL.GetConfigValue("StoreIssuenceWithProduction", trans).ToString = "True" Then
                'Store Issuance
                Call AddStoreIssuance(ProductionStore, trans)
            End If
            'trans.Commit()
            ''Call Stock DAL Object
            'Call New StockDAL().Update(ProductionStore.StockMaster)
            Return ProductionStore.ProductionId

        Catch ex As Exception
            'trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
            CostOfSaleAmount = 0D
        End Try
    End Function

    Public Function Delete(ByVal ProductionStore As ProductionMaster) As Boolean
        'Dim VoucherId As Integer = Convert.ToInt32(UtilityDAL.GetVoucherIdByVoucherNo("" & ProductionStore.Production_No & ""))
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Open Then
            Con.Close()
        End If
        Con.Open()


        Dim trans As SqlTransaction = Con.BeginTransaction
        Try


            Dim str As String = String.Empty

            str = String.Empty
            str = "Update PlanDetailTable Set ProducedQty=IsNull(ProducedQty,0)-IsNull(ProductionDetailTable.Sz1,0) From PlanDetailTable, ProductionDetailTable WHERE ProductionDetailTable.PlanDetailId = PlanDetailTable.PlanDetailId AND ProductionDetailTable.ArticleDefId = PlanDetailTable.ArticleDefId And PlanDetailTable.PlanId=" & ProductionStore.PlanId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From ProductionDetailTable  WHERE Production_Id=" & ProductionStore.ProductionId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From ProductionMasterTable WHERE Production_Id=" & ProductionStore.ProductionId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Delete Voucher From tblVoucherDetail
            str = String.Empty
            str = "Delete From tblVoucherDetail WHERE Voucher_ID in(select voucher_id from tblVoucher where voucher_no='" & ProductionStore.Production_No.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Delete Voucher From tblVoucherMaster
            str = String.Empty
            str = "Delete From tblVoucher WHERE Voucher_No='" & ProductionStore.Production_No.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From tblVoucherDetail where voucher_id in(select voucher_id from tblVoucher where voucher_no in(select dispatchno from DispatchMasterTable where RefDocument='" & ProductionStore.Production_No.Replace("'", "''") & "'))"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From tblVoucher where voucher_no in(select dispatchno from DispatchMasterTable where RefDocument='" & ProductionStore.Production_No.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From StockDetailTable WHERE StockTransId in(Select StockTransId From StockMasterTable where DocNo in(Select DispatchNo From DispatchMasterTable WHERE RefDocument='" & ProductionStore.Production_No & "'))"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From StockMasterTable WHERE DocNo in(Select DispatchNo From DispatchMasterTable WHERE RefDocument='" & ProductionStore.Production_No & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From DispatchDetailTable where dispatchid in(select dispatchid from dispatchmastertable where refDocument='" & ProductionStore.Production_No.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ''Start 25-04-2017
            str = String.Empty
            str = "Delete From StockDetailTable WHERE StockTransId in(Select StockTransId From StockMasterTable where DocNo ='" & ProductionStore.Production_No & "') "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = String.Empty
            str = "Delete From StockMasterTable WHERE DocNo ='" & ProductionStore.Production_No & "' "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ''End 25-04-2017


            str = String.Empty
            str = "Delete From DispatchMasterTable where RefDocument='" & ProductionStore.Production_No.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = ""
            str = "Update PlanMasterTable Set ProductionStatus=Case When IsNull(a.RemainingQty,0) > 0 then 'Open' Else 'Close' End From PlanMasterTable, (Select PlanId, IsNull(SUM(IsNull(Sz1,0)-IsNull(ProducedQty,0)),0) as RemainingQty From PlanDetailTable WHERE PlanId=" & ProductionStore.PlanId & " Group by PlanId) a WHERE a.PlanId = PlanMasterTable.PlanId AND PlanMasterTable.PlanId=" & ProductionStore.PlanId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            'Call Stock DAL For Delete 
            Call New StockDAL().Delete(ProductionStore.StockMaster, trans)

            trans.Commit()

            ''Call Stock DAL For Delete 
            'Call New StockDAL().Delete(ProductionStore.StockMaster)
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddDetail(ByVal MasterId As Integer, ByVal trans As SqlTransaction, ByVal ProductionStore As ProductionMaster, Optional ByVal CallFromProductionOrder As Boolean = False) As Boolean  'Task 3240 Add new argument CallFromProductionOrder that check whether this function call from production order page
        Try
            Dim str As String = String.Empty
            CostOfSaleAmount = 0D
            Dim ProductionList As List(Of ProductionDetail) = ProductionStore.ProductionDetail
            For Each Production As ProductionDetail In ProductionList
                'Before against request no. 934
                'str = "Insert Into ProductionDetailTable(Production_ID, Location_ID, ArticledefID, ArticleSize, Sz1,Sz2,Sz3,Sz4,Sz5,Sz6,Sz7,Qty,CurrentRate, Comments, Pack_Desc) " _
                '& " Values(" & MasterId & ", " & Production.Location_ID & ", " & Production.ArticledefID & ", N'" & Production.ArticleSize & "', " & Production.Sz1 & ", " & Production.Sz2 & ", " & Production.Sz3 & ", " & Production.Sz4 & ", " & Production.Sz5 & ", " & Production.Sz6 & ", " & Production.Sz7 & ", " & Production.Qty & ", " & Production.CurrentRate & ", N'" & Production.Comments & "', N'" & Production.Pack_Desc.Replace("'", "''") & "') "
                'ReqId-934 Resolve comma error
                ''Task No 2555 Update The Query Of Insert For Specific Table 
                '                str = "Insert Into ProductionDetailTable(Production_ID, Location_ID, ArticledefID, ArticleSize, Sz1,Sz2,Sz3,Sz4,Sz5,Sz6,Sz7,Qty,CurrentRate, Comments, Pack_Desc) " _
                '& " Values(" & MasterId & ", " & Production.Location_ID & ", " & Production.ArticledefID & ", N'" & Production.ArticleSize & "', " & Production.Sz1 & ", " & Production.Sz2 & ", " & Production.Sz3 & ", " & Production.Sz4 & ", " & Production.Sz5 & ", " & Production.Sz6 & ", " & Production.Sz7 & ", " & Production.Qty & ", " & Production.CurrentRate & ", N'" & Production.Comments.Replace("'", "''") & "', N'" & Production.Pack_Desc.Replace("'", "''") & "') "
                'Task:2555 Added Column UOM
                'Before against task:2690
                ' str = "Insert Into ProductionDetailTable(Production_ID, Location_ID, ArticledefID, ArticleSize, Sz1,Sz2,Sz3,Sz4,Sz5,Sz6,Sz7,Qty,CurrentRate, Comments, Pack_Desc,UOM) " _
                '& " Values(" & MasterId & ", " & Production.Location_ID & ", " & Production.ArticledefID & ", N'" & Production.ArticleSize & "', " & Production.Sz1 & ", " & Production.Sz2 & ", " & Production.Sz3 & ", " & Production.Sz4 & ", " & Production.Sz5 & ", " & Production.Sz6 & ", " & Production.Sz7 & ", " & Production.Qty & ", " & Production.CurrentRate & ", N'" & Production.Comments.Replace("'", "''") & "', N'" & Production.Pack_Desc.Replace("'", "''") & "', N'" & Production.UOM.Replace("'", "''") & "') "
                'End Task:2555
                'Task:2690 Added Field EmployeeId
                'Marked Against Task 20150506 Ali Ansari
                ' str = "Insert Into ProductionDetailTable(Production_ID, Location_ID, ArticledefID, ArticleSize, Sz1,Sz2,Sz3,Sz4,Sz5,Sz6,Sz7,Qty,CurrentRate, Comments, Pack_Desc,UOM) " _
                '    & " Values(" & MasterId & ", " & Production.Location_ID & ", " & Production.ArticledefID & ", N'" & Production.ArticleSize & "', " & Production.Sz1 & ", " & Production.Sz2 & ", " & Production.Sz3 & ", " & Production.Sz4 & ", " & Production.Sz5 & ", " & Production.Sz6 & ", " & Production.Sz7 & ", " & Production.Qty & ", " & Production.CurrentRate & ", N'" & Production.Comments.Replace("'", "''") & "', N'" & Production.Pack_Desc.Replace("'", "''") & "', N'" & Production.UOM.Replace("'", "''") & "') "
                'End Task:2690
                'Marked Against Task 20150506 Ali Ansari

                'Altered Against Task 20150506 Ali Ansari
                ''TASK TFS1496 addition column of PackPrice
                ''TASK TFS1616 addition column of BatchNo,RetailPrice,ExpiryDate Ayesha Rehman

                Dim expiryDate As String = "NULL"
                If Not Production.ExpiryDate = Nothing Then
                    expiryDate = "'" & Production.ExpiryDate.ToString("yyyy-M-d h:mm:ss tt") & "'"
                End If
                'Altered Against Task 1772 Ayesha Rehman
                str = "Insert Into ProductionDetailTable (Production_ID, Location_ID, ArticledefID, ArticleSize, Sz1,Sz2,Sz3,Sz4,Sz5,Sz6,Sz7,Qty,CurrentRate, Comments, Pack_Desc,UOM,EngineNo,ChasisNo,PlanDetailId, PackPrice, BatchNo, RetailPrice, Expirydate, Dim1, Dim2 ) " _
                    & " Values (" & MasterId & ", " & Production.Location_ID & ", " & Production.ArticledefID & ", N'" & Production.ArticleSize & "', " & Production.Sz1 & ", " & Production.Sz2 & ", " & Production.Sz3 & ", " & Production.Sz4 & ", " & Production.Sz5 & ", " & Production.Sz6 & ", " & Production.Sz7 & ", " & Production.Qty & ", " & Production.CurrentRate & ", N'" & Production.Comments.Replace("'", "''") & "', N'" & Production.Pack_Desc.Replace("'", "''") & "', N'" & Production.UOM.Replace("'", "''") & "','" & Production.EngineNo.Replace("'", "''") & "','" & Production.ChasisNo.Replace("'", "''") & "', " & Production.PlanDetailId & ", " & Production.PackPrice & ", N'" & Production.BatchNo.Replace("'", "''") & "' , " & Production.RetailPrice & ",  " & expiryDate & ", " & Production.Dim1 & " , " & Production.Dim2 & ") Select @@Identity"
                'Altered Against Task 20150506 Ali Ansari

                Dim intProductionId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)


                'Task 3420 check CallFromProductionOrder is false then run below code

                If CallFromProductionOrder = False Then

                    If UtilityDAL.GetConfigValue("ApplyCostSheetRateOnProduction", trans).ToString = "True" Then
                        GetProductionPrice(Production.ArticledefID, trans) 'Get Update Purchase & Cost Price .
                    End If

                    str = String.Empty
                    str = "Update PlanDetailTable Set ProducedQty=IsNull(ProducedQty,0)+" & Val(Production.Sz1) & ",  ProducedTotalQty=IsNull(ProducedTotalQty,0)+" & Val(Production.Qty) & " From PlanDetailTable, ProductionDetailTable WHERE ProductionDetailTable.PlanDetailId = PlanDetailTable.PlanDetailId AND ProductionDetailTable.ArticleDefId = PlanDetailTable.ArticleDefId And PlanDetailTable.PlanDetailId=" & Production.PlanDetailId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                    str = String.Empty
                    str = "Select Isnull(tblCostSheet.ArticleID,0) as ID, Isnull(Qty,0) as Qty, Isnull(Art.PurchasePrice,0) as Price From tblCostSheet INNER JOIN ArticleDefTable Art On Art.ArticleId = tblCostSheet.ArticleId WHERE tblCostSheet.MasterArticleId  In (Select DISTINCT MasterId From ArticleDefView WHERE ArticleId=" & Production.ArticledefID & ")"
                    Dim dt As New DataTable
                    dt = UtilityDAL.GetDataTable(str, trans)
                    If dt IsNot Nothing Then
                        If dt.Rows.Count > 0 Then
                            For Each row As DataRow In dt.Rows
                                CostOfSaleAmount += ((row("Qty") * Val(Production.Qty)) * row("Price"))
                            Next
                        End If
                    End If
                    ''Start TASK-461 on 30-06-2016
                    Dim dtprice As New DataTable
                    Dim costPrice As Decimal = 0D
                    Dim purchasePrice As Decimal = 0D
                    dtprice = GetCostPriceForProduction(Production.ArticledefID, trans)
                    dtprice.AcceptChanges()
                    If dtprice.Rows.Count > 0 Then
                        costPrice = Val(dtprice.Rows(0).Item("CostPrice").ToString)
                        purchasePrice = Val(dtprice.Rows(0).Item("PurchasePrice").ToString)
                    End If

                    str = String.Empty
                    str = "UPDATE ArticleDefTableMaster Set PurchasePrice=" & Val(Production.CurrentRate) & ", Cost_Price=" & costPrice & " WHERE ArticleId in (Select MasterId From ArticleDefTable WHERE ArticleId=" & Production.ArticledefID & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                    'Apply Current Rate 
                    str = String.Empty
                    str = "UPDATE ArticleDefTable Set PurchasePrice=" & Val(Production.CurrentRate) & ", Cost_Price=" & costPrice & " WHERE ArticleId=" & Production.ArticledefID & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                    'Ayesha Rehman :TFS1154 : Adding RetailPrice againt every Item
                    str = String.Empty
                    str = "UPDATE ArticleDefTable Set PrintedRetailPrice=" & Val(Production.RetailPrice) & " WHERE ArticleId=" & Production.ArticledefID & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                    str = String.Empty
                    str = " Select a.ArticleDefId, b.SalesItem From IncrementReductionTable a INNER JOIN ArticleDefView b ON b.ArticleId = a.ArticleDefId WHERE (Convert(varchar, a.IncrementReductionDate, 102) = Convert(Datetime, N'" & ProductionStore.Production_Date.ToString("yyyy-M-d 00:00:00") & "', 102))  AND a.ArticleDefId=" & Production.ArticledefID & ""
                    Dim dtRate As New DataTable
                    dtRate = UtilityDAL.GetDataTable(str, trans)
                    dt.AcceptChanges()

                    str = String.Empty
                    str = "Select ISNULL(SalesItem,0) as SalesItem From ArticleDefView WHERE Active=1 AND ArticleId=" & Production.ArticledefID & " "
                    Dim dtSalesItem As New DataTable
                    dtSalesItem = UtilityDAL.GetDataTable(str, trans)
                    dtSalesItem.AcceptChanges()

                    If dtSalesItem.Rows.Count > 0 Then
                        If Not dtRate Is Nothing Then
                            If dtRate.Rows.Count > 0 Then
                                str = String.Empty
                                str = "Update IncrementReductionTable Set PurchaseNewPrice=" & Val(Production.CurrentRate) & ", SaleNewPrice=" & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(Production.CurrentRate)) & ", Cost_Price_New=" & costPrice & "  WHERE ArticleDefId=" & Production.ArticledefID & " AND (Convert(varchar, IncrementReductionDate, 102) = Convert(Datetime, N'" & ProductionStore.Production_Date.ToString("yyyy-M-d 00:00:00") & "', 102)) "
                                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                            Else
                                str = String.Empty
                                str = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice,Cost_Price_Old, Cost_Price_New) " _
                                & " Values(N'" & ProductionStore.Production_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & Production.ArticledefID & ",  " & Val(0) & ", " & Val(0) & ", " & Production.ArticledefID & ", " & Val(0) & ", " & IIf(dtSalesItem.Rows(0).Item(0) = True, 0, Val(Production.CurrentRate)) & ",0," & costPrice & ")"
                                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                            End If
                        End If
                    End If


                    ''End TASK-461

                End If

            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetRecordById(ByVal MasterId As Integer, Optional ByVal CompanyId As Integer = Nothing, Optional ByVal Condition As String = "") As DataTable
        Try
            Dim str As String = String.Empty

            'Before against task:2555  
            'If Not Condition = "All" Then
            '    str = "SELECT  ProductionDetailTable.Location_ID, ProductionDetailTable.ArticledefID, tblDefLocation.Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ProductionDetailTable.ArticleSize, ProductionDetailTable.Sz7 as PackQty, ISNULL(ProductionDetailTable.Sz1,0) as Qty, ProductionDetailTable.CurrentRate, 0 as Total, ProductionDetailTable.Comments, Isnull(ProductionDetailTable.Pack_Desc, ProductionDetailTable.articleSize) as Pack_Desc, Isnull(ArticleDefView.SubSubId,0) as PurchaseAccountId " _
            '          & " FROM ProductionDetailTable INNER JOIN ArticleDefView ON ProductionDetailTable.ArticledefID = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = ProductionDetailTable.Location_ID WHERE ProductionDetailTable.Production_Id=" & MasterId & ""
            'Else
            '    str = "Select Isnull(Detail.Location_Id,0) as Location_Id, vwArt.ArticleId as ArticleDefId, Detail.Location_Name, vwArt.ArticleCode, vwArt.ArticleDescription, vwArt.ArticleColorName, vwArt.ArticleSizeName, Isnull(Detail.ArticleSize,'Loose') as ArticleSize, Isnull(Detail.PackQty,0) as PackQty, Isnull(Detail.Qty,0) as Qty, Round(IsNull(vwArt.PurchasePrice,0),2) as CurrentRate, 0 as Total, Detail.Comments, Isnull(Detail.Pack_Desc,'Loose') as Pack_Desc, Isnull(vwArt.SubSubId,0) as PurchaseAccountId From ArticleDefView vwArt " _
            '          & " LEFT OUTER JOIN (SELECT  ProductionDetailTable.Location_ID, ProductionDetailTable.ArticledefID, tblDefLocation.Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ProductionDetailTable.ArticleSize, ProductionDetailTable.Sz7 as PackQty, ISNULL(ProductionDetailTable.Sz1,0) as Qty, ProductionDetailTable.CurrentRate, 0 as Total, ProductionDetailTable.Comments, Isnull(ProductionDetailTable.Pack_Desc, ProductionDetailTable.articleSize) as Pack_Desc " _
            '          & " FROM ProductionDetailTable INNER JOIN ArticleDefView ON ProductionDetailTable.ArticledefID = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = ProductionDetailTable.Location_ID WHERE ProductionDetailTable.Production_Id=" & MasterId & ") Detail On Detail.ArticleDefId = vwArt.ArticleId WHERE vwArt.SalesItem=1 AND vwArt.Active=1 ORDER BY SortOrder ASC"
            'End If
            If Condition = String.Empty Then
                'Task:2555 Added Field UOM, BatchNo.
                'Before against task:2690
                'str = "SELECT  ProductionDetailTable.Location_ID, ProductionDetailTable.ArticledefID, tblDefLocation.Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ProductionDetailTable.ArticleSize, ProductionDetailTable.Sz7 as PackQty, ISNULL(ProductionDetailTable.Sz1,0) as Qty, ProductionDetailTable.CurrentRate, 0 as Total, ProductionDetailTable.Comments, '' as BatchNo, ProductionDetailTable.UOM, Isnull(ProductionDetailTable.Pack_Desc, ProductionDetailTable.articleSize) as Pack_Desc, Isnull(ArticleDefView.SubSubId,0) as PurchaseAccountId " _
                '      & " FROM ProductionDetailTable INNER JOIN ArticleDefView ON ProductionDetailTable.ArticledefID = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = ProductionDetailTable.Location_ID WHERE ProductionDetailTable.Production_Id=" & MasterId & ""
                'End Task:2555
                'Task:2690 Added Field EmployeeId
                'str = "SELECT  ProductionDetailTable.Location_ID, ProductionDetailTable.ArticledefID, tblDefLocation.Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ProductionDetailTable.ArticleSize, ProductionDetailTable.Sz7 as PackQty, ISNULL(ProductionDetailTable.Sz1,0) as Qty, ProductionDetailTable.CurrentRate, 0 as Total, ProductionDetailTable.Comments, '' as BatchNo, ProductionDetailTable.UOM, Isnull(ProductionDetailTable.Pack_Desc, ProductionDetailTable.articleSize) as Pack_Desc, Isnull(ArticleDefView.SubSubId,0) as PurchaseAccountId, IsNull(ProductionDetailTable.EmployeeId,0) as EmployeeId " _
                '      & " FROM ProductionDetailTable INNER JOIN ArticleDefView ON ProductionDetailTable.ArticledefID = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = ProductionDetailTable.Location_ID WHERE ProductionDetailTable.Production_Id=" & MasterId & ""
                'End Task:2690
                'Marked Against Task# 20150506 Ali Ansari
                'str = "SELECT  ProductionDetailTable.Location_ID, ProductionDetailTable.ArticledefID, tblDefLocation.Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ProductionDetailTable.ArticleSize, ProductionDetailTable.Sz7 as PackQty, ISNULL(ProductionDetailTable.Sz1,0) as Qty, ProductionDetailTable.CurrentRate, 0 as Total, ProductionDetailTable.Comments, '' as BatchNo, ProductionDetailTable.UOM, Isnull(ProductionDetailTable.Pack_Desc, ProductionDetailTable.articleSize) as Pack_Desc, Isnull(ArticleDefView.SubSubId,0) as PurchaseAccountId, ArticleDefView.CGSAccountId, IsNull(ProductionDetailTable.EmployeeId,0) as EmployeeId, Convert(DateTime,Null) as ExpiryDate " _
                '                    & " FROM ProductionDetailTable INNER JOIN ArticleDefView ON ProductionDetailTable.ArticledefID = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = ProductionDetailTable.Location_ID WHERE ProductionDetailTable.Production_Id=" & MasterId & ""
                'Marked Against Task# 20150506 Ali Ansari

                'Altered Against Task# 20150506 Ali Ansari
                ''TASK-408 included TotalQty column on 14-06-2016

                ''TASK TFS1496 addition column of PackPrice
                ''TASK TFS1616 addition column of RetailPrice on 20-10-2017
                'IsNull(ProductionDetailTable.ExpiryDate, 'ABC') AS ExpiryDate , 
                ''TASK TFS1772 addition column of Dim1, Dim2 on 05-12-2017
                str = "SELECT  ProductionDetailTable.Location_ID, ProductionDetailTable.ArticledefID, tblDefLocation.Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName As UnitName, ProductionDetailTable.ArticleSize, ProductionDetailTable.Sz7 as PackQty, Convert(float,ISNULL(ProductionDetailTable.Sz1,0)) as Qty, Convert(float,ISNULL(ProductionDetailTable.Dim1,0)) as Dim1, Convert(float,ISNULL(ProductionDetailTable.Dim2,0)) as Dim2, Convert(float,ProductionDetailTable.CurrentRate) as CurrentRate, Convert(float,ProductionDetailTable.PackPrice) as PackPrice, Convert(float,IsNull((ProductionDetailTable.Sz1)*(ProductionDetailTable.CurrentRate),0)) as Total, ProductionDetailTable.Comments, ProductionDetailTable.BatchNo, ProductionDetailTable.UOM, Isnull(ProductionDetailTable.Pack_Desc, ProductionDetailTable.articleSize) as Pack_Desc,IsNull(ProductionDetailTable.RetailPrice,0) RetailPrice, Isnull(ArticleDefView.SubSubId,0) as PurchaseAccountId, ArticleDefView.CGSAccountId, IsNull(ProductionDetailTable.EmployeeId,0)  as EmployeeId,  " & _
                    "ISNULL(CONVERT(DateTime, ProductionDetailTable.ExpiryDate, 102), CONVERT(nvarchar(30), ProductionDetailTable.ExpiryDate, 102)) as ExpiryDate, " & _
                    "EngineNo,ChasisNo,IsNull(ProductionDetailTable.PlanDetailId,0) as PlanDetailId, ArticleDefView.MasterId, ISNULL(ProductionDetailTable.Qty,0) as TotalQty " _
                                   & " FROM ProductionDetailTable INNER JOIN ArticleDefView ON ProductionDetailTable.ArticledefID = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = ProductionDetailTable.Location_ID WHERE ProductionDetailTable.Production_Id=" & MasterId & ""
                'Altered Against Task# 20150506 Ali Ansari
            ElseIf Condition = "Plan" Then
                ''TASK-408 included TotalQty column on 14-06-2016
                ''TASK TFS1616 addition column of RetailPrice on 20-10-2017
                ''TASK TFS1772 addition column of Dim1, Dim2 on 05-12-2017
                str = "SELECT  PlanDetailTable.LocationID as Location_ID, PlanDetailTable.ArticledefID, tblDefLocation.Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName As UnitName, PlanDetailTable.ArticleSize, PlanDetailTable.Sz7 as PackQty, (ISNULL(PlanDetailTable.Sz1,0)-ISNULL(PlanDetailTable.ProducedQty,0)) as Qty, 0 as Dim1, 0 as Dim2,  Convert(float,PlanDetailTable.Price) as CurrentRate, 0 As PackPrice, 0 as Total, '' as Comments, '' as BatchNo, '' as UOM, Isnull(PlanDetailTable.Pack_Desc, PlanDetailTable.articleSize) as Pack_Desc,0 as RetailPrice, Isnull(ArticleDefView.SubSubId,0) as PurchaseAccountId, ArticleDefView.CGSAccountId, 0 as EmployeeId, '' as ExpiryDate,'' as EngineNo,'' as ChasisNo, IsNull(PlanDetailTable.PlanDetailId,0) as PlanDetailId, ArticleDefView.MasterId, (IsNull(PlanDetailTable.Qty, 0)-IsNull(PlanDetailTable.ProducedTotalQty, 0)) As TotalQty" _
                                   & " FROM PlanDetailTable INNER JOIN ArticleDefView ON PlanDetailTable.ArticledefID = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = PlanDetailTable.LocationId WHERE PlanDetailTable.PlanId=" & MasterId & " And (ISNULL(PlanDetailTable.Sz1,0)-ISNULL(PlanDetailTable.ProducedQty,0)) > 0"
                ''TASK TFS1616 addition column of RetailPrice on 20-10-2017
                ''TASK TFS1772 addition column of Dim1, Dim2 on 05-12-2017
            ElseIf Condition = "Ticket" Then
                str = "SELECT  1 as Location_ID, vwArt.ArticleId as ArticledefID, '' As Location_Name, vwArt.ArticleCode, vwArt.ArticleDescription, vwArt.ArticleColorName, vwArt.ArticleSizeName, vwArt.ArticleUnitName As UnitName, '' As ArticleSize, 0 as PackQty, IsNull(Detail.TicketQuantity, 0) as Qty,0 as Dim1, 0 as Dim2, Convert(float,0) As CurrentRate, Convert(float,0) As PackPrice, Convert(float,0) as Total, '' As Comments, '' as BatchNo, '' As UOM, '' As Pack_Desc, 0 As RetialPrice, 0 as PurchaseAccountId, vwArt.CGSAccountId, 0 as EmployeeId, '' as ExpiryDate, '' As EngineNo, '' As ChasisNo, Detail.PlanDetailId, vwArt.MasterId, IsNull(Detail.TicketQuantity, 0) As TotalQty From ArticleDefView vwArt " _
              & " INNER JOIN PlanTickets Detail On Detail.ArticleId = vwArt.ArticleId WHERE Detail.PlanTicketsId = " & MasterId & "  ORDER BY SortOrder ASC"

            Else
                'Task:2555 Added Field UOM, BatchNo.
                ''TASK:2613 Added UOM Field
                'Before against task:2690
                'str = "Select Isnull(Detail.Location_Id,0) as Location_Id, vwArt.ArticleId as ArticleDefId, Detail.Location_Name, vwArt.ArticleCode, vwArt.ArticleDescription, vwArt.ArticleColorName, vwArt.ArticleSizeName, Isnull(Detail.ArticleSize,'Loose') as ArticleSize, Isnull(Detail.PackQty,0) as PackQty, Isnull(Detail.Qty,0) as Qty, Round(IsNull(vwArt.PurchasePrice,0),2) as CurrentRate, 0 as Total, Detail.Comments, '' as BatchNo, Detail.UOM, Isnull(Detail.Pack_Desc,'Loose') as Pack_Desc, Isnull(vwArt.SubSubId,0) as PurchaseAccountId From ArticleDefView vwArt " _
                '      & " LEFT OUTER JOIN (SELECT  ProductionDetailTable.Location_ID, ProductionDetailTable.ArticledefID, tblDefLocation.Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ProductionDetailTable.ArticleSize, ProductionDetailTable.Sz7 as PackQty, ISNULL(ProductionDetailTable.Sz1,0) as Qty, ProductionDetailTable.CurrentRate, 0 as Total, ProductionDetailTable.Comments, Isnull(ProductionDetailTable.Pack_Desc, ProductionDetailTable.articleSize) as Pack_Desc, ProductionDetailTable.UOM " _
                '      & " FROM ProductionDetailTable INNER JOIN ArticleDefView ON ProductionDetailTable.ArticledefID = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = ProductionDetailTable.Location_ID WHERE ProductionDetailTable.Production_Id=" & MasterId & ") Detail On Detail.ArticleDefId = vwArt.ArticleId WHERE vwArt.SalesItem=1 AND vwArt.Active=1 ORDER BY SortOrder ASC"
                'End Task:2613
                'End Task:2555
                'Task:2690 Added Field EmployeeId
                'str = "Select Isnull(Detail.Location_Id,0) as Location_Id, vwArt.ArticleId as ArticleDefId, Detail.Location_Name, vwArt.ArticleCode, vwArt.ArticleDescription, vwArt.ArticleColorName, vwArt.ArticleSizeName, Isnull(Detail.ArticleSize,'Loose') as ArticleSize, Isnull(Detail.PackQty,0) as PackQty, Isnull(Detail.Qty,0) as Qty, Round(IsNull(vwArt.PurchasePrice,0),2) as CurrentRate, 0 as Total, Detail.Comments, '' as BatchNo, Detail.UOM, Isnull(Detail.Pack_Desc,'Loose') as Pack_Desc, Isnull(vwArt.SubSubId,0) as PurchaseAccountId, IsNull(Detail.EmployeeId,0) as EmployeeId From ArticleDefView vwArt " _
                '     & " LEFT OUTER JOIN (SELECT  ProductionDetailTable.Location_ID, ProductionDetailTable.ArticledefID, tblDefLocation.Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ProductionDetailTable.ArticleSize, ProductionDetailTable.Sz7 as PackQty, ISNULL(ProductionDetailTable.Sz1,0) as Qty, ProductionDetailTable.CurrentRate, 0 as Total, ProductionDetailTable.Comments, Isnull(ProductionDetailTable.Pack_Desc, ProductionDetailTable.articleSize) as Pack_Desc, ProductionDetailTable.UOM, IsNull(ProductionDetailTable.EmployeeId,0) as EmployeeId " _
                '     & " FROM ProductionDetailTable INNER JOIN ArticleDefView ON ProductionDetailTable.ArticledefID = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = ProductionDetailTable.Location_ID WHERE ProductionDetailTable.Production_Id=" & MasterId & ") Detail On Detail.ArticleDefId = vwArt.ArticleId WHERE vwArt.SalesItem=1 AND vwArt.Active=1 ORDER BY SortOrder ASC"
                'End Task:2690

                'str = "Select Isnull(Detail.Location_Id,0) as Location_Id, vwArt.ArticleId as ArticleDefId, Detail.Location_Name, vwArt.ArticleCode, vwArt.ArticleDescription, vwArt.ArticleColorName, vwArt.ArticleSizeName, Isnull(Detail.ArticleSize,'Loose') as ArticleSize, Isnull(Detail.PackQty,0) as PackQty, Isnull(Detail.Qty,0) as Qty, Round(IsNull(vwArt.PurchasePrice,0),2) as CurrentRate, 0 as Total, Detail.Comments, '' as BatchNo, Detail.UOM, Isnull(Detail.Pack_Desc,'Loose') as Pack_Desc, Isnull(vwArt.SubSubId,0) as PurchaseAccountId, vwArt.CGSAccountId, IsNull(Detail.EmployeeId,0) as EmployeeId ,EngineNo, ChasisNo, IsNull(Detail.PlanDetailId,0) as PlanDetailId  From ArticleDefView vwArt " _
                ''TASK-408 added TotalQty column on 14-06-2016
                ''TASK TFS1496 addition column of PackPrice
                ''TASK TFS1616 addition column of RetailPrice on 20-10-2017
                ''TASK TFS1772 addition column of Dim1, Dim2 on 05-12-2017
                str = "SELECT  IsNull(Detail.Location_ID,0) as Location_ID, vwArt.ArticleId as ArticledefID, Detail.Location_Name, vwArt.ArticleCode, vwArt.ArticleDescription, vwArt.ArticleColorName, vwArt.ArticleSizeName, vwArt.ArticleUnitName As UnitName, IsNull(Detail.ArticleSize,'Loose'), IsNull(Detail.PackQty,0) as PackQty, ISNULL(Detail.Qty,0) as Qty, ISNULL(Detail.Dim1,0) as Dim1, ISNULL(Detail.Dim2,0) as Dim2, Convert(float,Detail.CurrentRate) as CurrentRate, 0 as Total, Detail.Comments, '' as BatchNo, Detail.UOM, Isnull(Detail.Pack_Desc, 'Loose') as Pack_Desc,Detail.RetailPrice, Isnull(vwArt.SubSubId,0) as PurchaseAccountId, vwArt.CGSAccountId, IsNull(Detail.EmployeeId,0)  as EmployeeId, Convert(DateTime,Null) as ExpiryDate,EngineNo,ChasisNo,IsNull(Detail.PlanDetailId,0) as PlanDetailId, vwArt.MasterId, IsNull(Detail.TotalQty, 0) As TotalQty From ArticleDefView vwArt " _
               & " LEFT OUTER JOIN (SELECT  ProductionDetailTable.Location_ID, ProductionDetailTable.ArticledefID, tblDefLocation.Location_Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ProductionDetailTable.ArticleSize, ProductionDetailTable.Sz7 as PackQty, ISNULL(ProductionDetailTable.Sz1,0) as Qty, ISNULL(ProductionDetailTable.Dim1,0) as Dim1, ISNULL(ProductionDetailTable.Dim2,0) as Dim2, ProductionDetailTable.CurrentRate, IsNull(ProductionDetailTable.PackPrice, 0) As PackPrice, 0 as Total, ProductionDetailTable.Comments, Isnull(ProductionDetailTable.Pack_Desc, ProductionDetailTable.articleSize) as Pack_Desc, ProductionDetailTable.UOM, IsNull(ProductionDetailTable.EmployeeId,0) as EmployeeId, IsNull(ProductionDetailTable.ExpiryDate, '') as ExpiryDate, EngineNo, ChasisNo,ProductionDetailTable.PlanDetailId, ISNULL(ProductionDetailTable.Qty,0) as TotalQty " _
               & " FROM ProductionDetailTable INNER JOIN ArticleDefView ON ProductionDetailTable.ArticledefID = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefLocation On tblDefLocation.Location_Id = ProductionDetailTable.Location_ID WHERE ProductionDetailTable.Production_Id=" & MasterId & ") Detail On Detail.ArticleDefId = vwArt.ArticleId WHERE vwArt.SalesItem=1 AND vwArt.Active=1 ORDER BY SortOrder ASC"

            End If
            'If CompanyId > 0 Then
            '    str += " AND ProductionDetailTable.Location_Id=" & CompanyId
            'End If
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetVoucherIdByVoucherNo(ByVal VoucherNo As String) As Integer
        Try
            Dim str As String = String.Empty
            str = "Select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & VoucherNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If dt Is Nothing Then Return False
            If dt.Rows.Count > 0 Then
                Return Convert.ToInt32(dt.Rows(0).Item(0).ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllRecord(Optional ByVal Condition As String = "", Optional ByVal dateFrom As DateTime = Nothing, Optional ByVal dateTo As DateTime = Nothing, Optional ByVal docNo As String = "", Optional ByVal IGPNo As String = "", Optional ByVal LargerAmount As Double = 0, Optional ByVal LessAmount As Double = 0, Optional ByVal CostCenter As Integer = 0, Optional ByVal Remarks As String = "", Optional ByVal LocationId As Integer = 0, Optional ByVal ChassisNo As String = "", Optional ByVal EngineNo As String = "") As DataTable
        Try
            Dim ClosingDate As DateTime = Convert.ToDateTime(UtilityDAL.GetConfigValue("EndOfDate").ToString)
            Dim PreviouseRecordShow As Boolean = Convert.ToBoolean(UtilityDAL.GetConfigValue("PreviouseRecordShow").ToString)
            Dim str As String = String.Empty
            'Before against task:2690
            'str = "   SELECT DISTINCT " & IIf(Condition = "All", "", "Top 50") & " ProductionMasterTable.Production_ID, ProductionMasterTable.Production_store, ProductionMasterTable.Project, " _
            '      & "    ProductionMasterTable.Production_date, ProductionMasterTable.Production_no, tblDefLocation.location_name,  " _
            '      & "    ProductionMasterTable.CustomerCode, ProductionMasterTable.Order_No, tblDefCostCenter.Name AS Project, ProductionMasterTable.IGPNo,  ProductionMasterTable.Remarks, CASE WHEN IsNull(ProductionMasterTable.Post,0)=0 then 'UnPosted' else 'Posted' end as Post, ISNULL(ProductionMasterTable.IssuedStore,0) as IssuedStore, ProductionMasterTable.RefDocument,ProductionMasterTable.RefDispatchNo, isnull(ProductionMasterTable.PlanId,0) as PlanId, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status]  " _
            '      & "    FROM ProductionMasterTable LEFT OUTER JOIN " _
            '      & "    tblDefCostCenter ON ProductionMasterTable.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
            '      & "    tblDefLocation ON ProductionMasterTable.Production_store = tblDefLocation.location_id INNER JOIN ProductionDetailTable ON ProductionDetailTable.Production_Id = ProductionMasterTable.Production_Id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = ProductionMasterTable.Production_No  WHERE ProductionMasterTable.Production_no IS NOT NULL " _
            '      & " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, ProductionMasterTable.Production_date,102) > Convert(Datetime, N'" & ClosingDate & "',  102))") & ""
            'Task:2690 added field DepartmentId
            'str = "   SELECT DISTINCT " & IIf(Condition = "All", "", "Top 50") & " ProductionMasterTable.Production_ID, ProductionMasterTable.Production_store, ProductionMasterTable.Project, " _
            '    & "    ProductionMasterTable.Production_date, ProductionMasterTable.Production_no, tblDefLocation.location_name,  " _
            '    & "    ProductionMasterTable.CustomerCode, ProductionMasterTable.Order_No, tblDefCostCenter.Name AS Project, ProductionMasterTable.IGPNo,  ProductionMasterTable.Remarks, CASE WHEN IsNull(ProductionMasterTable.Post,0)=0 then 'UnPosted' else 'Posted' end as Post, ISNULL(ProductionMasterTable.IssuedStore,0) as IssuedStore, ProductionMasterTable.RefDocument,ProductionMasterTable.RefDispatchNo, isnull(ProductionMasterTable.PlanId,0) as PlanId, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(ProductionMasterTable.DepartmentId,0) as DepartmentId  " _
            '    & "    FROM ProductionMasterTable LEFT OUTER JOIN " _
            '    & "    tblDefCostCenter ON ProductionMasterTable.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
            '    & "    tblDefLocation ON ProductionMasterTable.Production_store = tblDefLocation.location_id INNER JOIN ProductionDetailTable ON ProductionDetailTable.Production_Id = ProductionMasterTable.Production_Id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = ProductionMasterTable.Production_No  WHERE ProductionMasterTable.Production_no IS NOT NULL " _
            '    & " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, ProductionMasterTable.Production_date,102) > Convert(Datetime, N'" & ClosingDate & "',  102))") & ""
            'End Task:2690
            'str = "   SELECT DISTINCT " & IIf(Condition = "All", "", "Top 50") & " ProductionMasterTable.Production_ID, ProductionMasterTable.Production_store, ProductionMasterTable.Project, " _
            '  & "    ProductionMasterTable.Production_date, ProductionMasterTable.Production_no, tblDefLocation.location_name,   " _
            '  & "    ProductionMasterTable.CustomerCode, ProductionMasterTable.Order_No, tblDefCostCenter.Name AS Project, ProductionMasterTable.IGPNo,  ProductionMasterTable.Remarks, CASE WHEN IsNull(ProductionMasterTable.Post,0)=0 then 'UnPosted' else 'Posted' end as Post, ISNULL(ProductionMasterTable.IssuedStore,0) as IssuedStore, ProductionMasterTable.RefDocument,ProductionMasterTable.RefDispatchNo, isnull(ProductionMasterTable.PlanId,0) as PlanId, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(ProductionMasterTable.DepartmentId,0) as DepartmentId, ISNULL(ProductionMasterTable.EmployeeID,0) as EmployeeID, isnull(ProductionMasterTable.CGSAccountId,0) as CGSAccountId,ProductionMasterTable.UpdateUserName as [Modified By]  " _
            '  & "    FROM ProductionMasterTable LEFT OUTER JOIN " _
            '  & "    tblDefCostCenter ON ProductionMasterTable.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
            '  & "    tblDefLocation ON ProductionMasterTable.Production_store = tblDefLocation.location_id INNER JOIN ProductionDetailTable ON ProductionDetailTable.Production_Id = ProductionMasterTable.Production_Id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = ProductionMasterTable.Production_No  WHERE ProductionMasterTable.Production_no IS NOT NULL " _

            str = "   SELECT DISTINCT " & IIf(Condition = "All", "", "Top 50") & " ProductionMasterTable.Production_ID, ProductionMasterTable.Production_store, ProductionMasterTable.Project, " _
         & "    ProductionMasterTable.Production_date, ProductionMasterTable.Production_no, tblDefLocation.location_name,   " _
         & "    ProductionMasterTable.CustomerCode, ProductionMasterTable.Order_No, tblDefCostCenter.Name AS Project, ProductionMasterTable.IGPNo,  ProductionMasterTable.Remarks, CASE WHEN IsNull(ProductionMasterTable.Post,0)=0 then 'UnPosted' else 'Posted' end as Post, ISNULL(ProductionMasterTable.IssuedStore,0) as IssuedStore, ProductionMasterTable.RefDocument,ProductionMasterTable.RefDispatchNo, isnull(ProductionMasterTable.PlanId,0) as PlanId,[Plan].PlanNo, CASE WHEN ISNULL(PrintLog.Cont,0)=0 THEN 'Print Pending' ELSE 'Printed' end as [Print Status], IsNull(ProductionMasterTable.DepartmentId,0) as DepartmentId, ISNULL(ProductionMasterTable.EmployeeID,0) as EmployeeID, isnull(ProductionMasterTable.CGSAccountId,0) as CGSAccountId, ProductionMasterTable.UpdateUserName as [Modified By], ProductionMasterTable.PlanTicketId " _
         & "    FROM ProductionMasterTable LEFT OUTER JOIN PlanMasterTable [Plan] on [Plan].PlanId = ProductionMasterTable.PlanId LEFT OUTER JOIN " _
         & "    tblDefCostCenter ON ProductionMasterTable.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
         & "    tblDefLocation ON ProductionMasterTable.Production_store = tblDefLocation.location_id INNER JOIN ProductionDetailTable ON ProductionDetailTable.Production_Id = ProductionMasterTable.Production_Id LEFT OUTER JOIN(Select Count(Id) as Cont, DocumentNo From tblPrint_Log Group By DocumentNo) PrintLog On PrintLog.DocumentNo = ProductionMasterTable.Production_No  WHERE ProductionMasterTable.Production_no IS NOT NULL " _
         & " " & IIf(PreviouseRecordShow = True, "", " AND (Convert(varchar, ProductionMasterTable.Production_date,102) > Convert(Datetime, N'" & ClosingDate & "',  102))") & ""
            If dateFrom <> Nothing Then
                str += " AND ProductionMasterTable.Production_date >= Convert(Datetime, N'" & dateFrom.ToString("yyyy-M-d 00:00:00") & "', 102) "
            End If
            If dateTo <> Nothing Then
                str += " AND ProductionMasterTable.Production_date <= Convert(Datetime, N'" & dateTo.ToString("yyyy-M-d 00:00:00") & "', 102) "
            End If
            If docNo <> String.Empty Then
                str += " AND ProductionMasterTable.Production_no LIKE '%" & docNo & "%'"
            End If
            If IGPNo <> String.Empty Then
                str += " AND  ProductionMasterTable.IGPNo LIKE '%" & IGPNo & "%'"
            End If
            If LargerAmount > 0 Then
                str += " AND ProductionMasterTable.TotalAmount >= " & LargerAmount & ""
            End If
            If LessAmount > 0 Then
                str += " AND ProductionMasterTable.TotalAmount >= " & LessAmount & ""
            End If
            If CostCenter > 0 Then
                str += " AND ProductionMasterTable.Project=" & CostCenter
            End If
            If Remarks <> String.Empty Then
                str += " AND  ProductionMasterTable.Remarks LIKE '%" & Remarks & "%'"
            End If
            If LocationId > 0 Then
                str += " AND ProductionDetailTable.Location_Id=" & LocationId
            End If
            If EngineNo.Length > 0 Then
                str += " AND ProductionMasterTable.Production_ID in(Select Production_ID From ProductionDetailTable WHERE  EngineNo LIKE '%" & EngineNo & "%')"
            End If
            If ChassisNo.Length > 0 Then
                str += " AND ProductionMasterTable.Production_ID in(Select Production_ID From ProductionDetailTable WHERE  ChasisNo LIKE '%" & ChassisNo & "%')"
            End If
            str += " ORDER BY ProductionMasterTable.Production_no Desc "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetDocumentNo(dtpPODate As DateTime, trans As SqlTransaction) As String
        Try
            'If Me.txtPONo.Text = "" Then
            If UtilityDAL.GetConfigValue("VoucherNo", trans).ToString = "Yearly" Then
                Return UtilityDAL.GetSerialNo("PRD" + "-" + Microsoft.VisualBasic.Right(dtpPODate.Year, 2) + "-", "ProductionMasterTable", "Production_No", trans)
            ElseIf UtilityDAL.GetConfigValue("VoucherNo", trans).ToString = "Monthly" Then
                Return UtilityDAL.GetNextDocNo("PRD" & "-" & Format(dtpPODate, "yy") & dtpPODate.Month.ToString("00"), 4, "ProductionMasterTable", "Production_No", trans)
            Else
                Return UtilityDAL.GetNextDocNo("PRD", 6, "ProductionMasterTable", "Production_No", trans)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub GetProductionPrice(ArticleId As Integer, trans As SqlTransaction)
        Dim cmd As New SqlCommand
        Try
            Dim dtprice As New DataTable
            dtprice = GetCostPriceForProduction(Val(ArticleId), trans)
            dtprice.AcceptChanges()
            Dim dblCostPrice As Decimal = 0D
            Dim dblPurchasePrice As Decimal = 0D
            Dim dblPrice As Decimal = 0D
            If dtprice.Rows.Count > 0 Then
                dblCostPrice = Val(dtprice.Rows(0).Item("CostPrice").ToString)
                dblPurchasePrice = Val(dtprice.Rows(0).Item("PurchasePrice").ToString)
            End If
            If UtilityDAL.GetConfigValue("AvgRate", trans).ToString = "True" Then
                dblPrice = dblCostPrice
            Else
                dblPrice = dblPurchasePrice
            End If
            If dblPrice = 0 Then
                dblPrice = 0 'Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            End If

            cmd.Connection = trans.Connection
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 300

            cmd.CommandText = ""
            cmd.CommandText = "Select * From  IncrementReductionTable WHERE ID In(Select Max(ID) From IncrementReductionTable WHERE ArticleDefID=" & ArticleId & ") AND ArticleDefID=" & ArticleId & ""
            Dim da As New SqlDataAdapter
            Dim dtdata As New DataTable
            da.SelectCommand = cmd
            da.Fill(dtdata)

            dtdata.AcceptChanges()
            Dim dblOldPurchasePrice As Decimal = 0D
            Dim dblOldSalePrice As Decimal = 0D
            Dim dblOldCostPrice As Decimal = 0D

            If dtdata.Rows.Count > 0 Then

                dblOldPurchasePrice = Val(dtdata.Rows(0).Item("PurchaseNewPrice").ToString)
                dblOldSalePrice = Val(dtdata.Rows(0).Item("SaleNewPrice").ToString)
                dblOldCostPrice = Val(dtdata.Rows(0).Item("Cost_Price_New").ToString)

            End If

            cmd.CommandText = ""
            cmd.CommandText = "Select GetDate() as ServerDate"
            Dim daServerDate As New SqlDataAdapter
            Dim dtServerDate As New DataTable
            daServerDate.SelectCommand = cmd
            daServerDate.Fill(dtServerDate)
            dtServerDate.AcceptChanges()


            cmd.CommandText = ""
            cmd.CommandText = "Select MasterID From ArticleDefTable WHERE ArticleID=" & ArticleId & ""
            Dim intMasterID As Integer = cmd.ExecuteScalar()

            cmd.CommandText = ""
            cmd.CommandText = "Update ArticleDefTableMaster Set PurchasePrice=" & dblPurchasePrice & ", Cost_Price=" & dblCostPrice & " WHERE ArticleID=" & intMasterID & ""
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "Update ArticleDefTable Set PurchasePrice=" & dblPurchasePrice & ", Cost_Price=" & dblCostPrice & " WHERE MasterID=" & intMasterID & " AND ArticleID=" & ArticleId & ""
            cmd.ExecuteNonQuery()

            cmd.CommandText = ""
            cmd.CommandText = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice,SaleNewPrice,Cost_Price_Old, Cost_Price_New) " _
                            & " Values( Convert(DateTime, '" & CDate(dtServerDate.Rows(0).Item(0)).ToString("yyyy-M-d hh:mm:ss tt") & "',102) , " & ArticleId & ",  " & Val(0) & ", " & Val(dblOldPurchasePrice) & ", " & dblPurchasePrice & ", " & Val(dblOldSalePrice) & ", " & dblPurchasePrice & "," & Val(dblOldCostPrice) & "," & dblCostPrice & ")"
            cmd.ExecuteNonQuery()


        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub
    Public Shared Function GetCostPriceForProduction(ArticleId As Integer, Optional trans As SqlTransaction = Nothing) As DataTable
        Try

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SP_CostPriceForProduction " & ArticleId & "", trans)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    Public Sub AddStoreIssuance(Production As ProductionMaster, trans As SqlTransaction)
        Try

            Dim strSQL As String = String.Empty
            Dim strDocumentNo As String = String.Empty
            Dim intDispatchId As Integer
            Dim intStockTransID As Integer
            Dim intVoucher_Id As Integer

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select DispatchNo From DispatchMasterTable WHERE RefDocument=N'" & Production.Production_No & "'", trans)
            If dt IsNot Nothing Then
                dt.AcceptChanges()
                If dt.Rows.Count > 0 Then
                    strDocumentNo = dt.Rows(0).Item(0).ToString()
                    intDispatchId = SQLHelper.ExecuteScaler(trans, CommandType.Text, "Select DispatchId From DispatchMastertable WHERE Dispatchno='" & strDocumentNo.Replace("'", "''") & "'")
                    intStockTransID = SQLHelper.ExecuteScaler(trans, CommandType.Text, "Select StockTransId From StockMasterTable WHERE DocNo='" & strDocumentNo.Replace("'", "''") & "'")
                    intVoucher_Id = SQLHelper.ExecuteScaler(trans, CommandType.Text, "Select voucher_id From tblVoucher WHERE Voucher_No='" & strDocumentNo.Replace("'", "''") & "'")
                End If
            End If

            If strDocumentNo.Length = 0 Then
                Production.RefDocument = GetDocumentNo1(Production.Production_Date, trans)

                strSQL = "INSERT INTO DispatchMasterTable(DispatchNo,DispatchDate,VendorId,Remarks,UserName,Issued,RefDocument,StoreIssuanceAccountId) VALUES('" & Production.RefDocument.Replace("'", "''") & "',Convert(DateTime,'" & Production.Production_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102),0,N'" & Production.Remarks.Replace("'", "''") & "',N'" & Production.UserName.Replace("'", "''") & "',1,N'" & Production.Production_No.Replace("'", "''") & "'," & Production.CGSAccountId & ")Select @@Identity"
                intDispatchId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

                Dim dtDoc As New DataTable
                dtDoc = UtilityDAL.GetDataTable("Select DispatchNo From DispatchMasterTable WHERE DispatchId=" & intDispatchId & "", trans)
                If dtDoc IsNot Nothing Then
                    dtDoc.AcceptChanges()
                    If dtDoc.Rows.Count > 0 Then
                        strDocumentNo = dtDoc.Rows(0).Item(0).ToString()
                    End If
                End If

                strSQL = String.Empty
                strSQL = "INSERT INTO StockMasterTable(DocNo,DocDate,Doctype,Remarks) VALUES(N'" & strDocumentNo.Replace("'", "''") & "', Convert(datetime,'" & Production.Production_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102),6,N'" & Production.Remarks.Replace("'", "''") & "')Select @@Identity"
                intStockTransID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

                strSQL = String.Empty
                strSQL = "INSERT INTO tblVoucher(location_id,voucher_code,finiancial_year_id,voucher_type_id,voucher_no,voucher_date,post,source,UserName)" _
                    & " VALUES(1,N'" & strDocumentNo.Replace("'", "''") & "', 1,1,N'" & strDocumentNo.Replace("'", "''") & "',Convert(datetime,'" & Production.Production_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102),1,'frmStoreIssuence',N'" & Production.UserName.Replace("'", "''") & "')Select @@Identity"
                intVoucher_Id = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            Else

                strSQL = String.Empty
                strSQL = "Delete From StockDetailTable WHERE StockTransID in(Select StockTransID From StockMasterTable WHERE DocNo='" & strDocumentNo.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                strSQL = String.Empty
                strSQL = "Delete From tblVoucherDetail WHERE voucher_id in(Select voucher_id From tblVoucher WHERE voucher_no='" & strDocumentNo.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                strSQL = String.Empty
                strSQL = "Delete From DispatchDetailTable WHERE DispatchId in(Select DispatchId From DispatchMasterTable WHERE DispatchNo='" & strDocumentNo.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


                strSQL = String.Empty
                strSQL = "Update DispatchMastertable Set DispatchDate=Convert(datetime,'" & Production.Production_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Remarks='" & Production.Remarks.Replace("'", "''") & "', StoreIssuanceAccountId=" & Production.CGSAccountId & " WHERE DispatchId=" & intDispatchId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                strSQL = String.Empty
                strSQL = "Update StockMasterTable Set DocDate=Convert(datetime,'" & Production.Production_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Remarks='" & Production.Remarks.Replace("'", "''") & "' WHERE StockTransId=" & intStockTransID & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                strSQL = String.Empty
                strSQL = "Update tblVoucher Set Voucher_Date=Convert(datetime,'" & Production.Production_Date.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Remarks='" & Production.Remarks.Replace("'", "''") & "' WHERE voucher_id=" & intVoucher_Id & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            End If

            For Each objMod As ProductionDetail In Production.ProductionDetail
                strSQL = String.Empty
                strSQL = "INSERT INTO DispatchDetailTable(DispatchId, LocationId, ArticleDefId,ArticleSize,Sz1,Sz2,Sz3,Sz4,Sz5,Sz6,Sz7,Qty,Price,CurrentPrice,Comments,Pack_Desc,ArticleDefMasterID) " _
                    & " Select " & intDispatchId & "," & Production.IssuedStore & ",tblCostSheet.ArticleId, N'Loose',(Qty*" & objMod.Qty & "),0,0,0,0,0,1,(Qty*" & objMod.Qty & "),PurchasePrice,PurchasePrice,Null,N'Loose',MasterID From tblCostSheet INNER JOIN ArticleDefview on ArticleDefview.ArticleID = tblCostSheet.ArticleId WHERE tblCostSheet.MasterArticleId In(Select MasterID From ArticleDefTable WHERE ArticleId=" & objMod.ArticledefID & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next



            strSQL = ""
            strSQL = "Select Count(*) as Id From DispatchDetailTable WHERE DispatchID=" & intDispatchId & ""
            Dim intCountDispatch As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)


            If intCountDispatch > 0 Then



                strSQL = String.Empty
                strSQL = "INSERT INTO StockDetailTable(StockTransID,LocationId,ArticleDefId,InQty,OutQty,Rate, InAmount,OutAmount) " _
                    & " Select " & intStockTransID & ",DispatchDetailTable.LocationId,DispatchDetailTable.ArticleDefId,0, DispatchDetailTable.Qty,DispatchDetailTable.Price,0, (DispatchDetailTable.Qty*DispatchDetailTable.Price) as OutAmount From DispatchDetailTable INNER JOIN DispatchMasterTable On DispatchMastertable.DispatchId = DispatchDetailTable.DispatchId WHERE DispatchMastertable.DispatchId=" & intDispatchId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                strSQL = String.Empty
                'debit voucher
                strSQL = "INSERT INTO tblVoucherDetail(location_id,voucher_id, coa_detail_id,comments,debit_amount,credit_amount) " _
                    & " Select 1," & intVoucher_Id & "," & IIf(Production.CGSAccountId = 0, "Case When ArticleDefView.CGSAccountId=0 then null else ArticleDefView.CGSAccountId end", "" & Production.CGSAccountId & "") & ", (ArticleDefView.ArticleDescription + ' ' + Convert(varchar,Qty) + ' x ' + Convert(varchar,Price)), (IsNull(Qty,0)*isNull(Price,0)), 0 From DispatchDetailTable INNER JOIN ArticleDefView On ArticleDefView.ArticleId = DispatchDetailTable.ArticleDefID WHERE DispatchDetailTable.DispatchId=" & intDispatchId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                strSQL = String.Empty
                strSQL = "INSERT INTO tblVoucherDetail(location_id,voucher_id, coa_detail_id,comments,debit_amount,credit_amount) " _
                 & " Select 1," & intVoucher_Id & ",ArticleDefView.SubSubId, (ArticleDefView.ArticleDescription + ' ' + Convert(varchar,Qty) + ' x ' + Convert(varchar,Price)), 0, (IsNull(Qty,0)*isNull(Price,0)) From DispatchDetailTable INNER JOIN ArticleDefView On ArticleDefView.ArticleId = DispatchDetailTable.ArticleDefID WHERE DispatchDetailTable.DispatchId=" & intDispatchId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                strSQL = String.Empty
                strSQL = "Update ProductionMasterTable SET RefDocument='" & strDocumentNo.Replace("'", "''") & "' WHERE Production_ID=" & Production.ProductionId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            Else


                strSQL = ""
                strSQL = "Delete From StockDetailTable WHERE StockTransID in(Select StockTransID From StockMasterTable WHERE DocNo='" & strDocumentNo.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


                strSQL = ""
                strSQL = "Delete From StockMasterTable where DocNo='" & strDocumentNo.Replace("'", "''") & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)



                strSQL = ""
                strSQL = "Delete From tblVoucherDetail WHERE voucher_id in(Select voucher_id From tblVoucher WHERE voucher_no='" & strDocumentNo.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                strSQL = ""
                strSQL = "Delete From tblVoucher where Voucher_No='" & strDocumentNo.Replace("'", "''") & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


                strSQL = ""
                strSQL = "Delete From DispatchDetailTable WHERE DispatchId in(Select DispatchId From DispatchMasterTable WHERE DispatchNo='" & strDocumentNo.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


                strSQL = ""
                strSQL = "Delete From DispatchMasterTable where DispatchNo='" & strDocumentNo.Replace("'", "''") & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)



            End If

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Sub
    Function GetDocumentNo1(dtpPODate As DateTime, trans As SqlTransaction) As String
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

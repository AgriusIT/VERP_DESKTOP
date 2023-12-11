'04-Aug-2015 Task#04082015 Ahmad Sharif Delete voucher from both tblVoucher and tblVoucherDetail table when financial impact check off
''TASK TFS1427 Added UserName to be saved for voucher on 13-09-2017
Imports System
Imports System.Net
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports SBUtility.Utility
Imports Microsoft.VisualBasic
Public Class LCDAL

    Dim intVoucherID As Integer = 0I
    Dim flag As Boolean = True

    Public Function Add(ByVal LC As LCBE) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            'strSQL = "INSERT INTO LCMasterTable(LCNo,LCDate,LCAccountId,LCExpenseAccountId,ImportName,PerformaNo,PerformaDate,PaymentMode,LCBeforeDate,PortOfLoading,PortOfDischarge,SupplierAccountId,IndenterAccountId,PartialShipment,Transhipment,Insurrance,ExchangeRate,BankPaidAmount,ShipingCharges,PortCharges,AssessedValue,DDForCMCC,DDForETO,TotalQty,TotalAmount,TotalDuty,SalesTax,AdditionalSalesTax,AdvanceIncomeTax,UserName,EntryDate,CostCenterId, ExciseDutyPercent,ExciseDuty,LCDocID,OtherCharges,ExpenseByLC, Financial_Impact) " _
            '& " VALUES(N'" & LC.LCNo.Replace("'", "''") & "', Convert(DateTime,N'" & LC.LCDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & LC.LCAccountId & ", " & LC.LCExpenseAccountId & ", N'" & LC.ImportName.Replace("'", "''") & "', N'" & LC.PerformaNo.Replace("'", "''") & "', Convert(DateTime, N'" & LC.PerformaDate.ToString("yyyy-M-d hh:mm:ss tt") & "', 102), N'" & LC.PaymentMode.Replace("'", "''") & "',Convert(DateTime,N'" & LC.LCBoreDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), N'" & LC.PortOfLoading.Replace("'", "''") & "', N'" & LC.PortOfDischarge.Replace("'", "''") & "'," & LC.SupplierAccountId & "," & LC.IndenterAccountId & ", " & IIf(LC.PartialShipment = True, 1, 0) & ", " & IIf(LC.Transhipment = True, 1, 0) & ", " & LC.Insurrance & ", " & LC.ExchangeRate & "," & LC.BankPaidAmount & "," & LC.ShipingCharges & "," & LC.PortCharges & ", " & LC.AssessedValue & ", " & LC.DDForCMCC & ", " & LC.DDForETO & "," & LC.TotalQty & "," & LC.TotalAmount & "," & LC.TotalDuty & "," & LC.SalesTax & "," & LC.AdditionalSalesTax & "," & LC.AdvanceIncomeTax & ",N'" & LC.UserName.Replace("'", "''") & "', Convert(DateTime,N'" & LC.EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & LC.CostCenterId & ", " & LC.ExciseDutyPercent & "," & LC.ExciseDuty & "," & LC.LCDocId & "," & LC.OthersCharges & "," & LC.ExpenseByLC & ", " & IIf(LC.FinancialImpact = True, 1, 0) & ")Select @@Identity"
            '' TASK TFS1261 Added new column ReceivingNoteId.
            ''TASK TFS1350 added new field of Status on 22-08-2017
            strSQL = "INSERT INTO LCMasterTable(LCNo,LCDate,LCAccountId,LCExpenseAccountId, ImportName,PerformaNo,PerformaDate,PaymentMode,LCBeforeDate,PortOfLoading,PortOfDischarge,SupplierAccountId,IndenterAccountId,PartialShipment,Transhipment,Insurrance,ExchangeRate,BankPaidAmount,ShipingCharges,PortCharges,AssessedValue,DDForCMCC,DDForETO,TotalQty,TotalAmount,TotalDuty,SalesTax,AdditionalSalesTax,AdvanceIncomeTax,UserName,EntryDate, CostCenterId, ExciseDutyPercent,ExciseDuty,LCDocID,OtherCharges,ExpenseByLC, Financial_Impact,ShippingRemarks,PortRemarks,CMCCRemarks,ETORemarks,AdjCMCCAmount,AdjETOAmount , CurrencyID,CurrencyRate, PurchaseOrderId, ReceivingNoteId, Status) " _
           & " VALUES(N'" & LC.LCNo.Replace("'", "''") & "', Convert(DateTime,N'" & LC.LCDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & LC.LCAccountId & ", " & LC.LCExpenseAccountId & ", N'" & LC.ImportName.Replace("'", "''") & "', N'" & LC.PerformaNo.Replace("'", "''") & "', Convert(DateTime, N'" & LC.PerformaDate.ToString("yyyy-M-d hh:mm:ss tt") & "', 102), N'" & LC.PaymentMode.Replace("'", "''") & "',Convert(DateTime,N'" & LC.LCBoreDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), N'" & LC.PortOfLoading.Replace("'", "''") & "', N'" & LC.PortOfDischarge.Replace("'", "''") & "'," & LC.SupplierAccountId & "," & LC.IndenterAccountId & ", " & IIf(LC.PartialShipment = True, 1, 0) & ", " & IIf(LC.Transhipment = True, 1, 0) & ", " & LC.Insurrance & ", " & LC.ExchangeRate & "," & LC.BankPaidAmount & "," & LC.ShipingCharges & "," & LC.PortCharges & ", " & LC.AssessedValue & ", " & LC.DDForCMCC & ", " & LC.DDForETO & "," & LC.TotalQty & "," & LC.TotalAmount & "," & LC.TotalDuty & "," & LC.SalesTax & "," & LC.AdditionalSalesTax & "," & LC.AdvanceIncomeTax & ",N'" & LC.UserName.Replace("'", "''") & "', Convert(DateTime,N'" & LC.EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & LC.CostCenterId & ", " & LC.ExciseDutyPercent & "," & LC.ExciseDuty & "," & LC.LCDocId & "," & LC.OthersCharges & "," & LC.ExpenseByLC & ", " & IIf(LC.FinancialImpact = True, 1, 0) & ", N'" & LC.ShippingRemarks.Replace("'", "''") & "', N'" & LC.PortRemarks.Replace("'", "''") & "', N'" & LC.CMCCRemarks.Replace("'", "''") & "', N'" & LC.ETORemarks.Replace("'", "''") & "'," & Val(LC.AdjCMCCAmount) & "," & Val(LC.AdjETOAmount) & "," & Val(LC.CurrenyObj.CurrencyId) & "," & Val(LC.CurrenyObj.CurrencyRate) & ", " & Val(LC.PurchaseOrderId) & ", " & Val(LC.ReceivingNoteId) & ", '" & LC.Status.Replace("'", "''") & "' ) Select @@Identity"
            LC.LCID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)


            If LC.FinancialImpact = True Then
                'Voucher 
                strSQL = String.Empty
                'Task#04082015 add post column in insert query 
                ''TASK TFS1427 Added UserName to be saved for voucher on 13-09-2017
                strSQL = "INSERT INTO tblVoucher(Voucher_Code, Voucher_No,Voucher_Date,Location_Id,Voucher_Type_ID,Source,post, UserName, Reference) " _
                & " VALUES(N'" & LC.LCNo.Replace("'", "''") & "',N'" & LC.LCNo.Replace("'", "''") & "', Convert(DateTime,N'" & LC.VoucherDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),1,6,'frmImport'," & IIf(LC.FinancialImpact = True, 1, 0) & ", N'" & LC.UserName.Replace("'", "''") & "', N'" & LC.VoucherRemarks.Replace("'", "''") & "')Select @@Identity"
                intVoucherID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                LC.Voucher_Id = intVoucherID
            End If

            AddDetail(LC, trans, False)
            UpdatePurchaseOrderStatus(LC.PurchaseOrderId, trans)
            'AddQtyForWhole(LC.PurchaseOrderId, trans)
            'strSQL = String.Empty
            'LC.LCOtherDetail.LCId = LC.LCID
            'strSQL = "INSERT INTO LCOtherDetailTable(LCId,RefNo,OpeningDate,ExpiryDate,Amendment,OpeningBankAcId,AdvisingBank,SpecialInstruction,Remarks,OpenedBy,Vessel,BLNo,BLDate,ETDDate,ETADate,BankDocumentDate,BankPaymentDate,ClearingAgent,TransporterId) " _
            '& " VALUES(" & LC.LCOtherDetail.LCId & ",N'" & LC.LCOtherDetail.RefNo.Replace("'", "''") & "',Convert(DateTime,N'" & LC.LCOtherDetail.OpeningDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), Convert(DateTime,N'" & LC.LCOtherDetail.ExpiryDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), N'" & LC.LCOtherDetail.Amendment.Replace("'", "''") & "', " & LC.LCOtherDetail.OpeningBankAcId & ", N'" & LC.LCOtherDetail.AdvisingBank.Replace("'", "''") & "', " _
            '& " N'" & LC.LCOtherDetail.SepcialInstruction.Replace("'", "''") & "',N'" & LC.LCOtherDetail.Remarks.Replace("'", "''") & "'," & LC.LCOtherDetail.OpenedBy & ", N'" & LC.LCOtherDetail.Vessel.Replace("'", "''") & "',N'" & LC.LCOtherDetail.BLNo.Replace("'", "''") & "', Convert(DateTime,N'" & LC.LCOtherDetail.BLDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102) , " _
            '& " Convert(DateTime, N'" & LC.LCOtherDetail.ETDDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), Convert(DateTime,N'" & LC.LCOtherDetail.ETADate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(DateTime,N'" & LC.LCOtherDetail.BankDocumentDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(DateTime,N'" & LC.LCOtherDetail.BankPaymentDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & LC.LCOtherDetail.ClearingAgent.Replace("'", "''") & "'," & LC.LCOtherDetail.TransporterId & ")"
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            If LC.FinancialImpact = True Then
                strSQL = String.Empty
                strSQL = "INSERT INTO StockMasterTable(DocNo,DocDate,Remarks) VALUES(N'" & LC.LCNo.Replace("'", "''") & "',Convert(DateTime, N'" & LC.LCDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & LC.ImportName.Replace("'", "''") & "')Select @@Identity"
                Dim intDocId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                AddStockDetail(intDocId, LC, trans)
            End If

            LC.ActivityLog.ActivityName = "Save"
            LC.ActivityLog.ApplicationName = "Inventory"
            LC.ActivityLog.RecordType = "Import"
            LC.ActivityLog.RefNo = LC.LCNo
            Call UtilityDAL.BuildActivityLog(LC.ActivityLog, trans)
            intVoucherID = 0I

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()

        End Try
    End Function
    ' Commented below function against TASK TFS1609
    Public Function AddDetail(ByVal LC As LCBE, ByVal trans As SqlTransaction, Optional ByVal IsUpdateMode As Boolean = False) As Boolean
        Try

            Dim Totallist As Integer
            Dim NewVoucherId As Integer = 0
            If Not LC.ExpensesIDs Is Nothing Then
                Totallist = LC.ExpensesIDs.Count
            End If
            'Dim id As KeyValuePair(Of String, Integer)
            'id = LC.ExpensesIDs


            Dim strSQL As String = String.Empty
            If LC.LCDetail IsNot Nothing Then

                For Each LCDt As LCDetailBE In LC.LCDetail
                    ''Below if condition is commented on 19-02-2018
                    'If Val(LCDt.LCDetailId) = Val(0) Then
                    strSQL = String.Empty
                    Dim dblPurchasePrice As Double = 0D
                    Dim dblPurchasePrice1 As Double = 0D
                    Dim dblAddCost As Double = 0D
                    Dim creditExpenseAmount As Double = 0D
                    Dim countList As Integer = 1
                    Dim LCDetailId As Integer = 0
                    If Not LC.ExpensesIDs Is Nothing Then
                        countList = LC.ExpensesIDs.Count
                    End If
                    creditExpenseAmount = LCDt.Weighted_Cost / countList

                    LCDt.LCId = LC.LCID

                    ''
                    If LCDt.LCDetailId > 0 Then
                        ''Edit For TFS1956
                        strSQL = "Update LCDetailTable Set LCId = " & LC.LCID & ", LocationId = " & LCDt.LocationId & ", ArticleDefId = " & LCDt.ArticleDefId & ", ArticleSize = N'" & LCDt.ArticleSize.Replace("'", "''") & "', PackDesc = N'" & LCDt.PackDesc.Replace("'", "''") & "', Sz1 = " & LCDt.Sz1 & ", Sz7 = " & LCDt.Sz7 & ", Qty = " & LCDt.Qty & ", Price = " & LCDt.Price & ", TotalAmount = " & LCDt.TotalAmount & ", Insurrance =" & LCDt.Insurrance & ", AddCostPercent = " & LCDt.AddCostPercent & ", AssessedValue = " & LCDt.AssessedValue & ", DutyPercent= " & LCDt.DutyPercent & ", Duty = " & LCDt.Duty & ", AddCustomDutyPercent= " & LCDt.AddCustomDutyPercent & ", AddCustomDuty = " & LCDt.AddCustomDuty & ", RegulatoryDutyPercent= " & LCDt.RegulatoryDutyPercent & ", RegulatoryDuty = " & LCDt.RegulatoryDuty & ", SaleTaxPercent = " & LCDt.SaleTaxPercent & ", SaleTax = " & LCDt.SaleTax & ", AddSaleTaxPercent = " & LCDt.AddSaleTaxPercent & ", AddSaleTax = " & LCDt.AddSaleTax & ", AdvIncomeTaxPercent=" & LCDt.AdvIncomeTaxPercent & ", AdvIncomeTax=" & LCDt.AdvIncomeTax & ", Comments = N'" & LCDt.Comments.Replace("'", "''") & "', PurchaseAccountId = " & LCDt.PurchaseAccountId & ", Exch_Rate = " & LCDt.Exch_Rate & ", Import_Value = " & LCDt.Import_Value & ", ExciseDutyPercent = " & LCDt.ExciseDutyPercent & ", ExciseDuty = " & LCDt.ExciseDuty & ", Other_Charges= " & LCDt.Other_Charges & ", Net_Amount = " & LCDt.Net_Amount & ", Weight = " & LCDt.Weight & ", Per_Kg_Cost= " & LCDt.Per_Kg_Cost & ", Weighted_Cost = " & LCDt.Weighted_Cost & ", ImportAmount = " & LCDt.ImportAmount & ", PurchaseOrderId = " & Val(LCDt.PurchaseOrderId) & ", PurchaseOrderDetailId = " & Val(LCDt.PurchaseOrderDetailId) & " , ReceivingNoteId = " & Val(LCDt.ReceivingNoteId) & ", BatchNo='" & LCDt.BatchNo & "',ExpiryDate = Convert(DateTime,N'" & LCDt.ExpiryDate.ToString("yyyy-M-d hh:mm:ss tt") & "') WHERE LCDetailId = " & LCDt.LCDetailId & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                        LCDetailId = LCDt.LCDetailId
                    Else
                        strSQL = "INSERT INTO LCDetailTable(LCId,LocationId,ArticleDefId,ArticleSize,PackDesc,Sz1,Sz7,Qty,Price,TotalAmount,Insurrance,AddCostPercent,AssessedValue,DutyPercent,Duty,SaleTaxPercent,SaleTax,AddSaleTaxPercent,AddSaleTax,AdvIncomeTaxPercent,AdvIncomeTax,Comments, PurchaseAccountId,Exch_Rate,Import_Value, ExciseDutyPercent,ExciseDuty,Other_Charges,Net_Amount, Weight, Per_Kg_Cost, Weighted_Cost,ImportAmount, PurchaseOrderId, PurchaseOrderDetailId, ReceivingNoteId, AddCustomDutyPercent, AddCustomDuty, RegulatoryDutyPercent, RegulatoryDuty,BatchNo,ExpiryDate) VALUES(" & LC.LCID & "," & LCDt.LocationId & ", " & LCDt.ArticleDefId & ",N'" & LCDt.ArticleSize.Replace("'", "''") & "',N'" & LCDt.PackDesc.Replace("'", "''") & "'," & LCDt.Sz1 & "," & LCDt.Sz7 & "," & LCDt.Qty & "," & LCDt.Price & "," & LCDt.TotalAmount & "," & LCDt.Insurrance & "," & LCDt.AddCostPercent & "," & LCDt.AssessedValue & "," & LCDt.DutyPercent & "," & LCDt.Duty & ", " & LCDt.SaleTaxPercent & "," & LCDt.SaleTax & "," & LCDt.AddSaleTaxPercent & "," & LCDt.AddSaleTax & "," & LCDt.AdvIncomeTaxPercent & "," & LCDt.AdvIncomeTax & ",N'" & LCDt.Comments.Replace("'", "''") & "'," & LCDt.PurchaseAccountId & ", " & LCDt.Exch_Rate & "," & LCDt.Import_Value & "," & LCDt.ExciseDutyPercent & ", " & LCDt.ExciseDuty & "," & LCDt.Other_Charges & "," & LCDt.Net_Amount & "," & LCDt.Weight & "," & LCDt.Per_Kg_Cost & "," & LCDt.Weighted_Cost & ", " & LCDt.ImportAmount & ", " & Val(LCDt.PurchaseOrderId) & ", " & Val(LCDt.PurchaseOrderDetailId) & ", " & Val(LCDt.ReceivingNoteId) & "," & LCDt.AddCustomDutyPercent & "," & LCDt.AddCustomDuty & "," & LCDt.RegulatoryDutyPercent & "," & LCDt.RegulatoryDuty & ",'" & LCDt.BatchNo & "', Convert(DateTime,N'" & LCDt.ExpiryDate.ToString("yyyy-M-d hh:mm:ss tt") & "')) Select @@Identity "
                        LCDt.LCDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                    End If

                    ''

                    ''Below lines are commented on 19-02-2018
                    ''Edit for TFS1956
                    'strSQL = "INSERT INTO LCDetailTable(LCId,LocationId,ArticleDefId,ArticleSize,PackDesc,Sz1,Sz7,Qty,Price,TotalAmount,Insurrance,AddCostPercent,AssessedValue,DutyPercent,Duty,SaleTaxPercent,SaleTax,AddSaleTaxPercent,AddSaleTax,AdvIncomeTaxPercent,AdvIncomeTax,Comments, PurchaseAccountId,Exch_Rate,Import_Value, ExciseDutyPercent,ExciseDuty,Other_Charges,Net_Amount, Weight, Per_Kg_Cost, Weighted_Cost,ImportAmount, PurchaseOrderId, PurchaseOrderDetailId, ReceivingNoteId, AddCustomDutyPercent, AddCustomDuty, RegulatoryDutyPercent, RegulatoryDuty) VALUES(" & LC.LCID & "," & LCDt.LocationId & ", " & LCDt.ArticleDefId & ",N'" & LCDt.ArticleSize.Replace("'", "''") & "',N'" & LCDt.PackDesc.Replace("'", "''") & "'," & LCDt.Sz1 & "," & LCDt.Sz7 & "," & LCDt.Qty & "," & LCDt.Price & "," & LCDt.TotalAmount & "," & LCDt.Insurrance & "," & LCDt.AddCostPercent & "," & LCDt.AssessedValue & "," & LCDt.DutyPercent & "," & LCDt.Duty & ", " & LCDt.SaleTaxPercent & "," & LCDt.SaleTax & "," & LCDt.AddSaleTaxPercent & "," & LCDt.AddSaleTax & "," & LCDt.AdvIncomeTaxPercent & "," & LCDt.AdvIncomeTax & ",N'" & LCDt.Comments.Replace("'", "''") & "'," & LCDt.PurchaseAccountId & ", " & LCDt.Exch_Rate & "," & LCDt.Import_Value & "," & LCDt.ExciseDutyPercent & ", " & LCDt.ExciseDuty & "," & LCDt.Other_Charges & "," & LCDt.Net_Amount & "," & LCDt.Weight & "," & LCDt.Per_Kg_Cost & "," & LCDt.Weighted_Cost & ", " & LCDt.ImportAmount & ", " & Val(LCDt.PurchaseOrderId) & ", " & Val(LCDt.PurchaseOrderDetailId) & ", " & Val(LCDt.ReceivingNoteId) & "," & LCDt.AddCustomDutyPercent & "," & LCDt.AddCustomDuty & "," & LCDt.RegulatoryDutyPercent & "," & LCDt.RegulatoryDuty & ") SELECT @@IDENTITY"
                    ''SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    'LCDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                    'LCDt.LCDetailId = LCDetailId
                    AddPOQty(LCDt.PurchaseOrderId, LCDt.PurchaseOrderDetailId, LCDt.Sz1, LCDt.Qty, trans)
                    If LC.FinancialImpact = True Then

                        If LCDt.Qty <> 0 Then
                            dblPurchasePrice = ((LCDt.Other_Charges) / LCDt.Qty)
                        Else
                            dblPurchasePrice = 0D
                        End If

                        LCDt.LedgerComments = String.Empty
                        LCDt.LedgerComments = LCDt.ArticleDescription & "" & "(Qty:" & LCDt.Qty & ", Price:" & dblPurchasePrice & ")"
                        'Purchase Voucher'
                        strSQL = String.Empty
                        strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid , CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol, LCDetailId) VALUES(1," & LC.Voucher_Id & "," & LCDt.PurchaseAccountId & ", " & (Val(dblPurchasePrice) * Val(LCDt.Qty)) & ",0,N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "' ," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & "," & (Val(dblPurchasePrice) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & " ,0 , '" & LC.CurrenyObj.CurrencySymbol & "', " & LCDt.LCDetailId & ") "
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                        ''Purchase To LC Account'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & (Val(dblPurchasePrice) * Val(LCDt.Qty)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        Dim ConfigVal As String = UtilityDAL.GetConfigValue("ImportWieghtWiseCalculation")
                        If ConfigVal = "True" Then
                            strSQL = String.Empty
                            strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol, LCDetailId) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & (Val(LCDt.TotalAmount)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & (Val(LCDt.TotalAmount)) / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "', " & LCDt.LCDetailId & ") "
                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                            For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                                strSQL = String.Empty
                                strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol, LCDetailId ) VALUES(1," & LC.Voucher_Id & ", " & id.Key & " ,0,  " & creditExpenseAmount & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & creditExpenseAmount / LC.CurrenyObj.CurrencyRate & ", '" & LC.CurrenyObj.CurrencySymbol & "', " & LCDt.LCDetailId & ") "
                                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                            Next
                        Else
                            'strSQL = String.Empty
                            'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & ", " & LC.ExpensesIDs.Item[count] & " ,0, " & id.Value & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & ") "
                            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                            ' If (flag) Then
                            For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                                strSQL = String.Empty
                                strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol, LCDetailId) VALUES(1," & LC.Voucher_Id & ", " & id.Key & " ,0,  " & (Val(dblPurchasePrice) * Val(LCDt.Qty)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & (Val(dblPurchasePrice) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "', " & LCDt.LCDetailId & ") "
                                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                            Next
                        End If


                        'flag = False
                        'End If

                        'dblAddCost = ((LCDt.AddCostPercent / 100) * (LCDt.Insurrance + LCDt.TotalAmount))
                        ''Add Cost Duty'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LCDt.AdditionalCostAccountId & ", " & dblAddCost & ",0,'Ref:Additional Cost'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        ''Cost Duty To LC Account'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & dblAddCost & ",'Ref:Additional Cost'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        ''Custom Duty'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LCDt.CustomDutyAccountId & ", " & LCDt.Duty & ",0,'Ref:Custom Duty'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        ''Custom Duty To LC Account'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & LCDt.Duty & ",'Ref:Custom Duty'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        ''Sales Tax'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LCDt.SalesTaxAccountId & ", " & LCDt.SaleTax & ",0,'Ref:Sales Tax'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        ''Sales Tax To LC Account'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & LCDt.SaleTax & ",'Ref:Sales Tax'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        ''Additional Sales Tax'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LCDt.AdditionalSalesTaxAccountId & ", " & LCDt.AddSaleTax & ",0,'Ref:Additional Sales Tax'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        ''Additional Sales Tax To LC Account'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & LCDt.AddSaleTax & ",'Ref:Additional Sales Tax'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        ''Advance Income Tax'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LCDt.AdvanceIncomeTaxAccountId & ", " & LCDt.AdvIncomeTax & ",0,'Ref:Advance Income Tax'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        ''Advance Income To LC Account'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & LCDt.AdvIncomeTax & ",'Ref:Advance Income Tax'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        ''Excise Tax'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LCDt.ExciseDutyAccountId & ", " & LCDt.ExciseDuty & ",0,'Ref:Excise Duty'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        ''Excise To LC Account'
                        'strSQL = String.Empty
                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & LCDt.ExciseDuty & ",'Ref:Excise Duty'," & LC.CostCenterId & ") "
                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)




                        ''Commented Against TFS4234
                        'Dim dt As New DataTable
                        'Dim dblCostPrice As Double = 0D
                        'Dim dblCurrentStock As Double = 0D
                        'dt = UtilityDAL.GetDataTable("Select * From IncrementReductionTable WHERE ID In (Select Max(Id) From IncrementReductionTable WHERE ArticleDefId=" & LCDt.ArticleDefId & " Group By ArticleDefID)", trans)
                        'dt.AcceptChanges()
                        'If dt.HasErrors = False Then
                        '    If dt.Rows.Count > 0 Then
                        '        If dblPurchasePrice <> Val(dt.Rows(0).Item("PurchaseNewPrice").ToString) Then

                        '            Dim dtStock As New DataTable
                        '            dtStock = UtilityDAL.GetDataTable("Select ArticleDefId,  IsNull((SUM(IsNull(InQty,0)-IsNull(OutQty,0)) + " & Val(LCDt.Qty) & "),0) As CurrStock, IsNull((SUM(IsNull(InAmount,0)-IsNull(OutAmount,0)) + " & (Val(LCDt.Qty) * Val(dblPurchasePrice)) & "),0) as StockAmount From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefId=" & LCDt.ArticleDefId & " AND DocNo <> N'" & LC.LCNo.Replace("'", "''") & "' Group By ArticleDefId ", trans)
                        '            dtStock.AcceptChanges()


                        '            If dtStock.HasErrors = False Then
                        '                If dtStock.Rows.Count > 0 Then
                        '                    If Val(dtStock.Rows(0).Item("CurrStock").ToString) <> 0 Then
                        '                        dblCostPrice = Math.Round((Val(dtStock.Rows(0).Item("StockAmount").ToString) / Val(dtStock.Rows(0).Item("CurrStock").ToString)), 4)
                        '                        dblCurrentStock = Val(dtStock.Rows(0).Item("CurrStock").ToString)
                        '                    Else
                        '                        dblCostPrice = dblPurchasePrice
                        '                    End If
                        '                Else
                        '                    dblCostPrice = dblPurchasePrice
                        '                End If
                        '            End If

                        '            strSQL = String.Empty
                        '            strSQL = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty,PurchaseOldPrice,PurchaseNewPrice,SaleOldPrice,SaleNewPrice,Cost_Price_Old,Cost_Price_New) VALUES( " _
                        '            & " Convert(DateTime,N'" & LC.LCDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & LCDt.ArticleDefId & ", " & dblCurrentStock & "," & Val(dt.Rows(0).Item("PurchaseNewPrice").ToString) & ", " & dblPurchasePrice & "," & Val(dt.Rows(0).Item("SaleOldPrice").ToString) & "," & Val(dt.Rows(0).Item("SaleNewPrice").ToString) & ", " & Val(dt.Rows(0).Item("Cost_Price_New").ToString) & "," & dblCostPrice & ")"
                        '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        '            strSQL = String.Empty
                        '            strSQL = "Update ArticleDefTableMaster Set PurchasePrice=" & dblPurchasePrice & ", Cost_Price=" & GetCostPrice(LCDt, trans) & " WHERE ArticleId In(Select MasterID From ArticleDefTable WHERE ArticleID=" & LCDt.ArticleDefId & ")"
                        '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        '            strSQL = String.Empty
                        '            strSQL = "Update ArticleDefTable Set PurchasePrice=" & dblPurchasePrice & ", Cost_Price=" & GetCostPrice(LCDt, trans) & " WHERE ArticleId=" & LCDt.ArticleDefId & ""
                        '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        '        End If
                        '    End If
                        'End If
                        ''Start TFS4234
                        strSQL = String.Empty
                        strSQL = "Update ArticleDefTableMaster Set PurchasePrice=" & dblPurchasePrice & ", Cost_Price=" & GetCostPrice(LCDt, trans) & " WHERE ArticleId In(Select MasterID From ArticleDefTable WHERE ArticleID=" & LCDt.ArticleDefId & ")"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        strSQL = String.Empty
                        strSQL = "Update ArticleDefTable Set PurchasePrice=" & dblPurchasePrice & ", Cost_Price=" & GetCostPrice(LCDt, trans) & " WHERE ArticleId=" & LCDt.ArticleDefId & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                        ''End TFS4234
                        dblPurchasePrice = 0D
                        dblAddCost = 0
                    End If
                    'End If
                Next

                'If LC.FinancialImpact = True Then
                '    ''Shipping Charges'
                '    'strSQL = String.Empty
                '    'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.SupplierAccountId & ", " & LC.ShipingCharges & ",0,'Ref:shipping charges'," & LC.CostCenterId & ") "
                '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                '    ''Shipping Charges To LC Account'
                '    'strSQL = String.Empty
                '    'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & LC.ShipingCharges & ",'Ref:shipping charges'," & LC.CostCenterId & ") "
                '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


                '    ''Port Charges'
                '    'strSQL = String.Empty
                '    'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCExpenseAccountId & ", " & LC.PortCharges & ",0,'Ref:port charges'," & LC.CostCenterId & ") "
                '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                '    ''Port Charges To LC Account'
                '    'strSQL = String.Empty
                '    'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & LC.PortCharges & ",'Ref:port charges'," & LC.CostCenterId & ") "
                '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


                '    ''Others Charges'
                '    'strSQL = String.Empty
                '    'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.IndenterAccountId & ", " & LC.ShipingCharges & ",0,'Ref:other charges'," & LC.CostCenterId & ") "
                '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                '    ''Others Charges To LC Account'
                '    'strSQL = String.Empty
                '    'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & LC.ShipingCharges & ",'Ref:other charges'," & LC.CostCenterId & ") "
                '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                'End If
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' TASK TFS1609
    ''' </summary>
    ''' <param name="LC"></param>
    ''' <param name="trans"></param>
    ''' <param name="IsUpdateMode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateDetail(ByVal LC As LCBE, ByVal trans As SqlTransaction, Optional ByVal IsUpdateMode As Boolean = False) As Boolean
        Try
            Dim Totallist As Integer
            Dim NewVoucherId As Integer = 0
            If Not LC.ExpensesIDs Is Nothing Then
                Totallist = LC.ExpensesIDs.Count
            End If
            'Dim id As KeyValuePair(Of String, Integer)
            'id = LC.ExpensesIDs


            Dim strSQL As String = String.Empty
            If LC.LCDetail IsNot Nothing Then

                ''TASK TFS1609
                If LC.IsNewVoucher = True AndAlso LC.FinancialImpact = True Then
                    strSQL = "INSERT INTO tblVoucher(Voucher_Code, Voucher_No,Voucher_Date,Location_Id,Voucher_Type_ID,Source,post, UserName, Reference) " _
                                    & " VALUES(N'" & LC.LCNo.Replace("'", "''") & "',N'" & LC.NewVoucherNo.Replace("'", "''") & "', Convert(DateTime,N'" & LC.VoucherDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),1,6,'frmImport'," & IIf(LC.FinancialImpact = True, 1, 0) & ", N'" & LC.UserName.Replace("'", "''") & "' , N'" & LC.VoucherRemarks.Replace("'", "''") & "')Select @@Identity"
                    NewVoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                End If
                ''END TASK TFS1609
                For Each LCDt As LCDetailBE In LC.LCDetail
                    strSQL = String.Empty
                    Dim dblPurchasePrice As Double = 0D
                    Dim dblPurchasePrice1 As Double = 0D
                    Dim dblAddCost As Double = 0D
                    Dim creditExpenseAmount As Double = 0D
                    Dim countList As Integer = 1
                    Dim LCDetailId As Integer = 0
                    If Not LC.ExpensesIDs Is Nothing Then
                        countList = LC.ExpensesIDs.Count
                    End If
                    creditExpenseAmount = LCDt.Weighted_Cost / countList

                    LCDt.LCId = LC.LCID
                    If LCDt.LCDetailId > 0 Then
                        ''Edit For TFS1956
                        strSQL = "Update LCDetailTable Set LCId = " & LC.LCID & ", LocationId = " & LCDt.LocationId & ", ArticleDefId = " & LCDt.ArticleDefId & ", ArticleSize = N'" & LCDt.ArticleSize.Replace("'", "''") & "', PackDesc = N'" & LCDt.PackDesc.Replace("'", "''") & "', Sz1 = " & LCDt.Sz1 & ", Sz7 = " & LCDt.Sz7 & ", Qty = " & LCDt.Qty & ", Price = " & LCDt.Price & ", TotalAmount = " & LCDt.TotalAmount & ", Insurrance =" & LCDt.Insurrance & ", AddCostPercent = " & LCDt.AddCostPercent & ", AssessedValue = " & LCDt.AssessedValue & ", DutyPercent= " & LCDt.DutyPercent & ", Duty = " & LCDt.Duty & ", AddCustomDutyPercent= " & LCDt.AddCustomDutyPercent & ", AddCustomDuty = " & LCDt.AddCustomDuty & ", RegulatoryDutyPercent= " & LCDt.RegulatoryDutyPercent & ", RegulatoryDuty = " & LCDt.RegulatoryDuty & ", SaleTaxPercent = " & LCDt.SaleTaxPercent & ", SaleTax = " & LCDt.SaleTax & ", AddSaleTaxPercent = " & LCDt.AddSaleTaxPercent & ", AddSaleTax = " & LCDt.AddSaleTax & ", AdvIncomeTaxPercent=" & LCDt.AdvIncomeTaxPercent & ", AdvIncomeTax=" & LCDt.AdvIncomeTax & ", Comments = N'" & LCDt.Comments.Replace("'", "''") & "', PurchaseAccountId = " & LCDt.PurchaseAccountId & ", Exch_Rate = " & LCDt.Exch_Rate & ", Import_Value = " & LCDt.Import_Value & ", ExciseDutyPercent = " & LCDt.ExciseDutyPercent & ", ExciseDuty = " & LCDt.ExciseDuty & ", Other_Charges= " & LCDt.Other_Charges & ", Net_Amount = " & LCDt.Net_Amount & ", Weight = " & LCDt.Weight & ", Per_Kg_Cost= " & LCDt.Per_Kg_Cost & ", Weighted_Cost = " & LCDt.Weighted_Cost & ", ImportAmount = " & LCDt.ImportAmount & ", PurchaseOrderId = " & Val(LCDt.PurchaseOrderId) & ", PurchaseOrderDetailId = " & Val(LCDt.PurchaseOrderDetailId) & " , ReceivingNoteId = " & Val(LCDt.ReceivingNoteId) & " , BatchNo='" & LCDt.BatchNo & "',ExpiryDate = Convert(DateTime,N'" & LCDt.ExpiryDate.ToString("yyyy-M-d hh:mm:ss tt") & "') WHERE LCDetailId = " & LCDt.LCDetailId & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                        LCDetailId = LCDt.LCDetailId
                    Else
                        strSQL = "INSERT INTO LCDetailTable(LCId,LocationId,ArticleDefId,ArticleSize,PackDesc,Sz1,Sz7,Qty,Price,TotalAmount,Insurrance,AddCostPercent,AssessedValue,DutyPercent,Duty,SaleTaxPercent,SaleTax,AddSaleTaxPercent,AddSaleTax,AdvIncomeTaxPercent,AdvIncomeTax,Comments, PurchaseAccountId,Exch_Rate,Import_Value, ExciseDutyPercent,ExciseDuty,Other_Charges,Net_Amount, Weight, Per_Kg_Cost, Weighted_Cost,ImportAmount, PurchaseOrderId, PurchaseOrderDetailId, ReceivingNoteId, AddCustomDutyPercent, AddCustomDuty, RegulatoryDutyPercent, RegulatoryDuty,BatchNo,ExpiryDate) VALUES(" & LC.LCID & "," & LCDt.LocationId & ", " & LCDt.ArticleDefId & ",N'" & LCDt.ArticleSize.Replace("'", "''") & "',N'" & LCDt.PackDesc.Replace("'", "''") & "'," & LCDt.Sz1 & "," & LCDt.Sz7 & "," & LCDt.Qty & "," & LCDt.Price & "," & LCDt.TotalAmount & "," & LCDt.Insurrance & "," & LCDt.AddCostPercent & "," & LCDt.AssessedValue & "," & LCDt.DutyPercent & "," & LCDt.Duty & ", " & LCDt.SaleTaxPercent & "," & LCDt.SaleTax & "," & LCDt.AddSaleTaxPercent & "," & LCDt.AddSaleTax & "," & LCDt.AdvIncomeTaxPercent & "," & LCDt.AdvIncomeTax & ",N'" & LCDt.Comments.Replace("'", "''") & "'," & LCDt.PurchaseAccountId & ", " & LCDt.Exch_Rate & "," & LCDt.Import_Value & "," & LCDt.ExciseDutyPercent & ", " & LCDt.ExciseDuty & "," & LCDt.Other_Charges & "," & LCDt.Net_Amount & "," & LCDt.Weight & "," & LCDt.Per_Kg_Cost & "," & LCDt.Weighted_Cost & ", " & LCDt.ImportAmount & ", " & Val(LCDt.PurchaseOrderId) & ", " & Val(LCDt.PurchaseOrderDetailId) & ", " & Val(LCDt.ReceivingNoteId) & "," & LCDt.AddCustomDutyPercent & "," & LCDt.AddCustomDuty & "," & LCDt.RegulatoryDutyPercent & "," & LCDt.RegulatoryDuty & ",'" & LCDt.BatchNo & "', Convert(DateTime,N'" & LCDt.ExpiryDate.ToString("yyyy-M-d hh:mm:ss tt") & ") Select @@Identity "
                        LCDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                    End If
                    AddPOQty(LCDt.PurchaseOrderId, LCDt.PurchaseOrderDetailId, LCDt.Sz1, LCDt.Qty, trans)

                    If LC.FinancialImpact = True Then
                        ''TASK TFS1609
                        If LCDt.Other_Charges <> LCDt.Check_Other_Charges AndAlso IsUpdateMode = True AndAlso LCDt.Check_Other_Charges > 0 AndAlso LC.IsNewVoucher = True Then

                            Dim OtherChargesDifference As Double = (LCDt.Other_Charges - LCDt.Check_Other_Charges)
                            If OtherChargesDifference > 0 Then
                                If LCDt.Qty <> 0 Then
                                    dblPurchasePrice1 = (OtherChargesDifference / LCDt.Qty)
                                Else
                                    dblPurchasePrice1 = 0D
                                End If
                                'NewVoucherId = intVoucherID
                                LCDt.LedgerComments = String.Empty
                                LCDt.LedgerComments = LCDt.ArticleDescription & "" & "(Qty:" & LCDt.Qty & ", Price:" & dblPurchasePrice1 & ")"
                                'Purchase Voucher'
                                strSQL = String.Empty
                                strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid , CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol, LCDetailId ) VALUES(1," & NewVoucherId & "," & LCDt.PurchaseAccountId & ", " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ",0,N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "' ," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & "," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & " ,0 , '" & LC.CurrenyObj.CurrencySymbol & "', " & LCDetailId & " ) "
                                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                                    strSQL = String.Empty
                                    strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol, LCDetailId ) VALUES(1," & NewVoucherId & ", " & id.Key & " ,0,  " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "', " & LCDetailId & ") "
                                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                Next
                                If LCDt.Qty <> 0 Then
                                    dblPurchasePrice = ((LCDt.Other_Charges - OtherChargesDifference) / LCDt.Qty)
                                Else
                                    dblPurchasePrice = 0D
                                End If
                            End If
                            ''When OtherCharges is counted in minus then voucher entries will be done oppositely.
                            If OtherChargesDifference < 0 Then
                                If LCDt.Qty <> 0 Then
                                    dblPurchasePrice1 = Math.Abs(OtherChargesDifference / LCDt.Qty)
                                Else
                                    dblPurchasePrice1 = 0D
                                End If
                                LCDt.LedgerComments = String.Empty
                                LCDt.LedgerComments = LCDt.ArticleDescription & "" & "(Qty:" & LCDt.Qty & ", Price:" & dblPurchasePrice1 & ")"
                                'Purchase Voucher'
                                strSQL = String.Empty
                                strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid , CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol, LCDetailId) VALUES(1," & NewVoucherId & "," & LCDt.PurchaseAccountId & ", 0," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "' ," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0 ," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ", '" & LC.CurrenyObj.CurrencySymbol & "', " & LCDetailId & " ) "
                                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                                For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                                    strSQL = String.Empty
                                    strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol, LCDetailId) VALUES(1," & NewVoucherId & ", " & id.Key & ", " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ", 0, N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & "," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ", 0,'" & LC.CurrenyObj.CurrencySymbol & "', " & LCDetailId & ") "
                                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                Next

                            End If
                        End If
                        If LC.IsNewVoucher = True AndAlso LCDt.Check_Other_Charges = 0 Then
                            If LCDt.Qty <> 0 Then
                                dblPurchasePrice1 = (LCDt.Other_Charges / LCDt.Qty)
                            Else
                                dblPurchasePrice1 = 0D
                            End If
                            LCDt.LedgerComments = String.Empty
                            LCDt.LedgerComments = LCDt.ArticleDescription & "" & "(Qty:" & LCDt.Qty & ", Price:" & dblPurchasePrice1 & ")"
                            strSQL = String.Empty
                            strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid , CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol, LCDetailId ) VALUES(1," & NewVoucherId & "," & LCDt.PurchaseAccountId & ", " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ",0,N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "' ," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & "," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & " ,0 , '" & LC.CurrenyObj.CurrencySymbol & "', " & LCDetailId & " ) "
                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                            For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                                strSQL = String.Empty
                                strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol, LCDetailId ) VALUES(1," & NewVoucherId & ", " & id.Key & " ,0,  " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "', " & LCDetailId & ") "
                                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                            Next


                            'Else

                            '    If LCDt.Qty <> 0 Then
                            '        dblPurchasePrice = ((LCDt.Check_Other_Charges) / LCDt.Qty)
                            '    Else
                            '        dblPurchasePrice = 0D
                            '    End If

                            '    LCDt.LedgerComments = String.Empty
                            '    LCDt.LedgerComments = LCDt.ArticleDescription & "" & "(Qty:" & LCDt.Qty & ", Price:" & dblPurchasePrice & ")"
                            '    'Purchase Voucher'
                            '    strSQL = String.Empty
                            '    strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid , CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & "," & LCDt.PurchaseAccountId & ", " & (Val(dblPurchasePrice) * Val(LCDt.Qty)) & ",0,N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "' ," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & "," & (Val(dblPurchasePrice) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & " ,0 , '" & LC.CurrenyObj.CurrencySymbol & "' ) "
                            '    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                            '    Dim ConfigVal As String = UtilityDAL.GetConfigValue("ImportWieghtWiseCalculation")
                            '    If ConfigVal = "True" Then
                            '        strSQL = String.Empty
                            '        strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & (Val(LCDt.TotalAmount)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & (Val(LCDt.TotalAmount)) / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "') "
                            '        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                            '        For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                            '            strSQL = String.Empty
                            '            strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & ", " & id.Key & " ,0,  " & creditExpenseAmount & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & creditExpenseAmount / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "') "
                            '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                            '        Next
                            '    Else
                            '        For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                            '            strSQL = String.Empty
                            '            strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & ", " & id.Key & " ,0,  " & (Val(dblPurchasePrice) * Val(LCDt.Qty)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & (Val(dblPurchasePrice) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "') "
                            '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                            '        Next
                            '    End If
                        End If


                        '''' Below section is without new voucher update
                        If LCDt.Other_Charges <> LCDt.Check_Other_Charges AndAlso IsUpdateMode = False AndAlso LCDt.Check_Other_Charges > 0 AndAlso LC.IsNewVoucher = False Then


                            dblPurchasePrice1 = 0
                            Dim OtherChargesDifference As Double = (LCDt.Other_Charges - LCDt.Check_Other_Charges)
                            If OtherChargesDifference > 0 Then
                                If LCDt.Qty <> 0 Then
                                    dblPurchasePrice1 = (OtherChargesDifference / LCDt.Qty)
                                Else
                                    dblPurchasePrice1 = 0D
                                End If

                                'If LCDt.Qty <> 0 Then
                                '    dblPurchasePrice = ((LCDt.Check_Other_Charges) / LCDt.Qty)
                                'Else
                                '    dblPurchasePrice = 0D
                                'End If

                                LCDt.LedgerComments = String.Empty
                                LCDt.LedgerComments = LCDt.ArticleDescription & "" & "(Qty:" & LCDt.Qty & ", Price:" & dblPurchasePrice1 & ")"
                                'Purchase Voucher'
                                strSQL = String.Empty
                                'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid , CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & "," & LCDt.PurchaseAccountId & ", " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ",0,N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "' ," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & "," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & " ,0 , '" & LC.CurrenyObj.CurrencySymbol & "' ) "
                                strSQL = "Update tblVoucherDetail Set location_id =1, debit_amount = debit_amount + " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ", credit_amount =0, comments = N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "', costcenterid = " & LC.CostCenterId & ", CurrencyId = " & LC.CurrenyObj.CurrencyId & ", BaseCurrencyId = " & LC.CurrenyObj.BaseCurrencyId & ", BaseCurrencyRate = " & LC.CurrenyObj.BaseCurrencyRates & ", CurrencyRate = " & LC.CurrenyObj.CurrencyRate & ", Currency_debit_amount = Currency_debit_amount + " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ", Currency_Credit_amount =0, Currency_Symbol ='" & LC.CurrenyObj.CurrencySymbol & "' Where Voucher_Id = " & LC.Voucher_Id & " And coa_detail_id = " & LCDt.PurchaseAccountId & " And LCDetailId = " & LCDetailId & ""
                                'VALUES(1," & LC.Voucher_Id & "," & LCDt.PurchaseAccountId & ", " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ",0,N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "' ," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & "," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & " ,0 , '" & LC.CurrenyObj.CurrencySymbol & "' ) "
                                ''Voucher_Id, coa_detail_id,
                                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                Dim ConfigVal As String = UtilityDAL.GetConfigValue("ImportWieghtWiseCalculation")
                                If ConfigVal = "True" Then
                                    strSQL = String.Empty
                                    strSQL = " Delete From tblVoucherDetail Where Voucher_Id = " & LC.Voucher_Id & " And coa_detail_id = " & LC.LCAccountId & ""
                                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                    strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & (Val(LCDt.TotalAmount)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & (Val(LCDt.TotalAmount)) / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "') "
                                    'strSQL = "Update tblVoucherDetail Set location_id =1, debit_amount = 0, credit_amount = credit_amount + " & (Val(LCDt.TotalAmount)) & ", comments = N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "', costcenterid = " & LC.CostCenterId & ", CurrencyId = " & LC.CurrenyObj.CurrencyId & ", BaseCurrencyId = " & LC.CurrenyObj.BaseCurrencyId & ", BaseCurrencyRate = " & LC.CurrenyObj.BaseCurrencyRates & ", CurrencyRate = " & LC.CurrenyObj.CurrencyRate & ", Currency_debit_amount = 0, Currency_Credit_amount = Currency_Credit_amount + " & (Val(LCDt.TotalAmount)) / LC.CurrenyObj.CurrencyRate & ", Currency_Symbol ='" & LC.CurrenyObj.CurrencySymbol & "' Where Voucher_Id = " & LC.Voucher_Id & " And coa_detail_id = " & LC.LCAccountId & ""
                                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                                    For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                                        strSQL = String.Empty
                                        strSQL = " Delete From tblVoucherDetail Where Voucher_Id = " & LC.Voucher_Id & " And coa_detail_id = " & id.Key & ""
                                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                        strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & ", " & id.Key & " ,0,  " & creditExpenseAmount & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & creditExpenseAmount / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "') "

                                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                    Next
                                Else
                                    For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                                        strSQL = String.Empty
                                        'strSQL = " Delete From tblVoucherDetail Where Voucher_Id = " & LC.Voucher_Id & " And coa_detail_id = " & id.Key & ""
                                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & ", " & id.Key & " ,0,  " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "') "
                                        strSQL = "Update tblVoucherDetail Set location_id =1, debit_amount = 0, credit_amount = credit_amount + " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ", comments = N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "', costcenterid = " & LC.CostCenterId & ", CurrencyId = " & LC.CurrenyObj.CurrencyId & ", BaseCurrencyId = " & LC.CurrenyObj.BaseCurrencyId & ", BaseCurrencyRate = " & LC.CurrenyObj.BaseCurrencyRates & ", CurrencyRate = " & LC.CurrenyObj.CurrencyRate & ",  Currency_Credit_amount =  Currency_Credit_amount + " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ", Currency_debit_amount =0, Currency_Symbol ='" & LC.CurrenyObj.CurrencySymbol & "' Where Voucher_Id = " & LC.Voucher_Id & " And coa_detail_id = " & id.Key & " And LCDetailId = " & LCDetailId & " "

                                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                    Next
                                End If

                            End If

                            If OtherChargesDifference < 0 Then
                                dblPurchasePrice1 = 0
                                'If LCDt.Qty <> 0 Then
                                '    dblPurchasePrice1 = (OtherChargesDifference / LCDt.Qty)
                                'Else
                                '    dblPurchasePrice1 = 0D
                                'End If
                                If LCDt.Qty <> 0 Then
                                    dblPurchasePrice1 = Math.Abs(OtherChargesDifference / LCDt.Qty)
                                Else
                                    dblPurchasePrice1 = 0D
                                End If
                                'If LCDt.Qty <> 0 Then
                                '    dblPurchasePrice = ((LCDt.Check_Other_Charges) / LCDt.Qty)
                                'Else
                                '    dblPurchasePrice = 0D
                                'End If

                                LCDt.LedgerComments = String.Empty
                                LCDt.LedgerComments = LCDt.ArticleDescription & "" & "(Qty:" & LCDt.Qty & ", Price:" & dblPurchasePrice1 & ")"
                                'Purchase Voucher'
                                strSQL = String.Empty
                                'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid , CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & "," & LCDt.PurchaseAccountId & ", 0," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "' ," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0 ," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ", '" & LC.CurrenyObj.CurrencySymbol & "' ) "
                                strSQL = "Update tblVoucherDetail Set location_id =1, debit_amount = debit_amount - " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ", credit_amount =0, comments = N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "', costcenterid = " & LC.CostCenterId & ", CurrencyId = " & LC.CurrenyObj.CurrencyId & ", BaseCurrencyId = " & LC.CurrenyObj.BaseCurrencyId & ", BaseCurrencyRate = " & LC.CurrenyObj.BaseCurrencyRates & ", CurrencyRate = " & LC.CurrenyObj.CurrencyRate & ", Currency_debit_amount = Currency_debit_amount - " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ", Currency_Credit_amount =0, Currency_Symbol ='" & LC.CurrenyObj.CurrencySymbol & "' Where Voucher_Id = " & LC.Voucher_Id & " And coa_detail_id = " & LCDt.PurchaseAccountId & " And LCDetailId = " & LCDetailId & ""
                                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                Dim ConfigVal As String = UtilityDAL.GetConfigValue("ImportWieghtWiseCalculation")
                                If ConfigVal = "True" Then
                                    strSQL = String.Empty
                                    strSQL = " Delete From tblVoucherDetail Where Voucher_Id = " & LC.Voucher_Id & " And coa_detail_id = " & LC.LCAccountId & ""
                                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                    strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & (Val(LCDt.TotalAmount)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & (Val(LCDt.TotalAmount)) / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "') "
                                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                                    For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                                        strSQL = String.Empty
                                        strSQL = "Delete From tblVoucherDetail Where Voucher_Id = " & LC.Voucher_Id & " And coa_detail_id = " & id.Key & ""
                                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                        strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & ", " & id.Key & " ,0,  " & creditExpenseAmount & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & creditExpenseAmount / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "') "
                                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                    Next
                                Else
                                    For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                                        strSQL = String.Empty
                                        'strSQL = " Delete From tblVoucherDetail Where Voucher_Id = " & LC.Voucher_Id & " And coa_detail_id = " & id.Key & ""
                                        'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                        'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & ", " & id.Key & " ," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ",  0, N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ", " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ", 0,'" & LC.CurrenyObj.CurrencySymbol & "') "
                                        strSQL = "Update tblVoucherDetail Set location_id =1, debit_amount = 0, credit_amount = credit_amount - " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ", comments = N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "', costcenterid = " & LC.CostCenterId & ", CurrencyId = " & LC.CurrenyObj.CurrencyId & ", BaseCurrencyId = " & LC.CurrenyObj.BaseCurrencyId & ", BaseCurrencyRate = " & LC.CurrenyObj.BaseCurrencyRates & ", CurrencyRate = " & LC.CurrenyObj.CurrencyRate & ", Currency_debit_amount = 0, Currency_Credit_amount = Currency_Credit_amount - " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ", Currency_Symbol ='" & LC.CurrenyObj.CurrencySymbol & "' Where Voucher_Id = " & LC.Voucher_Id & " And coa_detail_id = " & id.Key & " And LCDetailId = " & LCDetailId & " "

                                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                    Next
                                End If

                            End If
                        End If


                        '''
                        If LC.IsNewVoucher = False AndAlso LCDt.Check_Other_Charges = 0 Then
                            dblPurchasePrice1 = 0
                            If LCDt.Qty <> 0 Then
                                dblPurchasePrice1 = (LCDt.Other_Charges / LCDt.Qty)
                            Else
                                dblPurchasePrice1 = 0D
                            End If
                            'If LCDt.Qty <> 0 Then
                            '    dblPurchasePrice = ((LCDt.Check_Other_Charges) / LCDt.Qty)
                            'Else
                            '    dblPurchasePrice = 0D
                            'End If

                            LCDt.LedgerComments = String.Empty
                            LCDt.LedgerComments = LCDt.ArticleDescription & "" & "(Qty:" & LCDt.Qty & ", Price:" & dblPurchasePrice1 & ")"
                            'Purchase Voucher'
                            strSQL = String.Empty
                            strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid , CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol, LCDetailId ) VALUES(1," & LC.Voucher_Id & "," & LCDt.PurchaseAccountId & ", " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ", 0,N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "' ," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & "," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & " ,0 , '" & LC.CurrenyObj.CurrencySymbol & "', " & LCDetailId & " ) "
                            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                            Dim ConfigVal As String = UtilityDAL.GetConfigValue("ImportWieghtWiseCalculation")
                            If ConfigVal = "True" Then
                                strSQL = String.Empty
                                strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & (Val(LCDt.TotalAmount)) & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & (Val(LCDt.TotalAmount)) / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "') "
                                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                                For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                                    strSQL = String.Empty
                                    strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol ) VALUES(1," & LC.Voucher_Id & ", " & id.Key & " ,0,  " & creditExpenseAmount & ",N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & creditExpenseAmount / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "') "
                                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                Next
                            Else
                                For Each id As KeyValuePair(Of String, Integer) In LC.ExpensesIDs
                                    strSQL = String.Empty
                                    strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid,CurrencyId ,BaseCurrencyId ,BaseCurrencyRate,CurrencyRate,Currency_debit_amount, Currency_Credit_amount,Currency_Symbol, LCDetailId) VALUES(1," & LC.Voucher_Id & ", " & id.Key & " ,0,  " & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) & ", N'" & LCDt.LedgerComments.Replace("'", "''") & ", " & LCDt.Comments.Replace("'", "''") & "'," & LC.CostCenterId & " ," & LC.CurrenyObj.CurrencyId & "," & LC.CurrenyObj.BaseCurrencyId & "," & LC.CurrenyObj.BaseCurrencyRates & "," & LC.CurrenyObj.CurrencyRate & ",0," & (Val(dblPurchasePrice1) * Val(LCDt.Qty)) / LC.CurrenyObj.CurrencyRate & ",'" & LC.CurrenyObj.CurrencySymbol & "', " & LCDetailId & " ) "
                                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                                Next
                            End If

                        End If



                        '''' End section without new voucher

                        ''Commented Against TFS4234
                        'Dim dt As New DataTable
                        'Dim dblCostPrice As Double = 0D
                        'Dim dblCurrentStock As Double = 0D
                        'dt = UtilityDAL.GetDataTable("Select * From IncrementReductionTable WHERE ID In (Select Max(Id) From IncrementReductionTable WHERE ArticleDefId=" & LCDt.ArticleDefId & " Group By ArticleDefID)", trans)
                        'dt.AcceptChanges()
                        'If dt.HasErrors = False Then
                        '    If dt.Rows.Count > 0 Then
                        '        If dblPurchasePrice <> Val(dt.Rows(0).Item("PurchaseNewPrice").ToString) Then

                        '            Dim dtStock As New DataTable
                        '            dtStock = UtilityDAL.GetDataTable("Select ArticleDefId,  IsNull((SUM(IsNull(InQty,0)-IsNull(OutQty,0)) + " & Val(LCDt.Qty) & "),0) As CurrStock, IsNull((SUM(IsNull(InAmount,0)-IsNull(OutAmount,0)) + " & (Val(LCDt.Qty) * Val(dblPurchasePrice)) & "),0) as StockAmount From StockDetailTable INNER JOIN StockMasterTable On StockMasterTable.StockTransId = StockDetailTable.StockTransId WHERE ArticleDefId=" & LCDt.ArticleDefId & " AND DocNo <> N'" & LC.LCNo.Replace("'", "''") & "' Group By ArticleDefId ", trans)
                        '            dtStock.AcceptChanges()


                        '            If dtStock.HasErrors = False Then
                        '                If dtStock.Rows.Count > 0 Then
                        '                    If Val(dtStock.Rows(0).Item("CurrStock").ToString) <> 0 Then
                        '                        dblCostPrice = Math.Round((Val(dtStock.Rows(0).Item("StockAmount").ToString) / Val(dtStock.Rows(0).Item("CurrStock").ToString)), 4)
                        '                        dblCurrentStock = Val(dtStock.Rows(0).Item("CurrStock").ToString)
                        '                    Else
                        '                        dblCostPrice = dblPurchasePrice
                        '                    End If
                        '                Else
                        '                    dblCostPrice = dblPurchasePrice
                        '                End If
                        '            End If

                        '            strSQL = String.Empty
                        '            strSQL = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty,PurchaseOldPrice,PurchaseNewPrice,SaleOldPrice,SaleNewPrice,Cost_Price_Old,Cost_Price_New) VALUES( " _
                        '            & " Convert(DateTime,N'" & LC.LCDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), " & LCDt.ArticleDefId & ", " & dblCurrentStock & "," & Val(dt.Rows(0).Item("PurchaseNewPrice").ToString) & ", " & dblPurchasePrice & "," & Val(dt.Rows(0).Item("SaleOldPrice").ToString) & "," & Val(dt.Rows(0).Item("SaleNewPrice").ToString) & ", " & Val(dt.Rows(0).Item("Cost_Price_New").ToString) & "," & dblCostPrice & ")"
                        '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        '            strSQL = String.Empty
                        '            strSQL = "Update ArticleDefTableMaster Set PurchasePrice=" & dblPurchasePrice & ", Cost_Price=" & GetCostPrice(LCDt, trans) & " WHERE ArticleId In(Select MasterID From ArticleDefTable WHERE ArticleID=" & LCDt.ArticleDefId & ")"
                        '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        '            strSQL = String.Empty
                        '            strSQL = "Update ArticleDefTable Set PurchasePrice=" & dblPurchasePrice & ", Cost_Price=" & GetCostPrice(LCDt, trans) & " WHERE ArticleId=" & LCDt.ArticleDefId & ""
                        '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        '        End If
                        '    End If
                        'End If
                        ''Start TFS4234
                        strSQL = String.Empty
                        strSQL = "Update ArticleDefTableMaster Set PurchasePrice=" & dblPurchasePrice & ", Cost_Price=" & GetCostPrice(LCDt, trans) & " WHERE ArticleId In(Select MasterID From ArticleDefTable WHERE ArticleID=" & LCDt.ArticleDefId & ")"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                        strSQL = String.Empty
                        strSQL = "Update ArticleDefTable Set PurchasePrice=" & dblPurchasePrice & ", Cost_Price=" & GetCostPrice(LCDt, trans) & " WHERE ArticleId=" & LCDt.ArticleDefId & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                        ''End TFS4234
                        dblPurchasePrice = 0D
                        dblAddCost = 0
                    End If
                Next

                'If LC.FinancialImpact = True Then
                '    ''Shipping Charges'
                '    'strSQL = String.Empty
                '    'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.SupplierAccountId & ", " & LC.ShipingCharges & ",0,'Ref:shipping charges'," & LC.CostCenterId & ") "
                '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                '    ''Shipping Charges To LC Account'
                '    'strSQL = String.Empty
                '    'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & LC.ShipingCharges & ",'Ref:shipping charges'," & LC.CostCenterId & ") "
                '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


                '    ''Port Charges'
                '    'strSQL = String.Empty
                '    'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCExpenseAccountId & ", " & LC.PortCharges & ",0,'Ref:port charges'," & LC.CostCenterId & ") "
                '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                '    ''Port Charges To LC Account'
                '    'strSQL = String.Empty
                '    'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & LC.PortCharges & ",'Ref:port charges'," & LC.CostCenterId & ") "
                '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


                '    ''Others Charges'
                '    'strSQL = String.Empty
                '    'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.IndenterAccountId & ", " & LC.ShipingCharges & ",0,'Ref:other charges'," & LC.CostCenterId & ") "
                '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                '    ''Others Charges To LC Account'
                '    'strSQL = String.Empty
                '    'strSQL = "INSERT INTO tblVoucherDetail(location_id,Voucher_Id, coa_detail_id, debit_amount,credit_amount, comments, costcenterid) VALUES(1," & LC.Voucher_Id & "," & LC.LCAccountId & ",0, " & LC.ShipingCharges & ",'Ref:other charges'," & LC.CostCenterId & ") "
                '    'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                'End If
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    Public Function AddStockDetail(ByVal DocId As Integer, ByVal LC As LCBE, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            If LC.LCDetail IsNot Nothing Then
                For Each LCDt As LCDetailBE In LC.LCDetail
                    strSQL = String.Empty
                    Dim dblPrice As Double = 0D
                    'dblPrice = ((LCDt.Net_Amount + LCDt.ExciseDuty + LCDt.Other_Charges) / LCDt.Qty)
                    dblPrice = ((LCDt.Other_Charges) / LCDt.Qty)  '((LCDt.Net_Amount + LCDt.ExciseDuty + LCDt.Other_Charges) / LCDt.Qty)
                    'strSQL = "INSERT INTO StockDetailTable(StockTransId,LocationId,ArticleDefId,InQty,OutQty,Rate,InAmount,OutAmount,Remarks) VALUES(" & DocId & "," & LCDt.LocationId & "," & LCDt.ArticleDefId & "," & IIf(LCDt.ArticleSize = "Loose", LCDt.Qty, (LCDt.Sz1 * LCDt.Sz7)) & ",0," & dblPrice & "," & (IIf(LCDt.ArticleSize = "Loose", LCDt.Qty, (LCDt.Sz1 * LCDt.Sz7)) * Val(LCDt.Price)) & ",0,N'" & LCDt.Comments.Replace("'", "''") & "')"
                    ''Start TFS4163 : Added BatchNo,Expiry Date 
                    ''Start TFS4234 : Added CostPrice
                    strSQL = "INSERT INTO StockDetailTable(StockTransId,LocationId,ArticleDefId,InQty,OutQty,Rate,InAmount,OutAmount, Remarks, Pack_Qty, In_PackQty, Out_PackQty,BatchNo , ExpiryDate,Cost_Price) VALUES(" & DocId & "," & LCDt.LocationId & "," & LCDt.ArticleDefId & "," & LCDt.Qty & ",0," & dblPrice & "," & ((LCDt.Qty) * Val(LCDt.Price)) & ",0,N'" & LCDt.Comments.Replace("'", "''") & "', " & LCDt.Sz7 & ", " & LCDt.Sz1 & ", 0 ,'" & LCDt.BatchNo & "', Convert(DateTime,N'" & LCDt.ExpiryDate.ToString("yyyy-M-d hh:mm:ss tt") & "') ," & GetCostPrice(LCDt, trans) & " )" ''TASK-408
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                  
                Next
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function

    Public Function Update(ByVal LC As LCBE, Optional ByVal VoucherEditMode As Boolean = False) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            '' TASK TFS1261 Added new column ReceivingNoteId for GRN.
            ''TASK TFS1350 added new field of Status on 22-08-2017
            strSQL = "UPDATE LCMasterTable SET LCNo=N'" & LC.LCNo.Replace("'", "''") & "',LCDate=Convert(DateTime,N'" & LC.LCDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),LCAccountId=" & LC.LCAccountId & ",LCExpenseAccountId=" & LC.LCExpenseAccountId & ",ImportName=N'" & LC.ImportName.Replace("'", "''") & "',PerformaNo=N'" & LC.PerformaNo.Replace("'", "''") & "',PerformaDate=Convert(DateTime, N'" & LC.PerformaDate.ToString("yyyy-M-d hh:mm:ss tt") & "', 102),PaymentMode=N'" & LC.PaymentMode.Replace("'", "''") & "',LCBeforeDate=Convert(DateTime, N'" & LC.LCBoreDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), PortOfLoading=N'" & LC.PortOfLoading.Replace("'", "''") & "',PortOfDischarge=N'" & LC.PortOfDischarge.Replace("'", "''") & "',SupplierAccountId=" & LC.SupplierAccountId & ",IndenterAccountId=" & LC.IndenterAccountId & ",PartialShipment=" & IIf(LC.PartialShipment = True, 1, 0) & ",Transhipment=" & IIf(LC.Transhipment = True, 1, 0) & ",Insurrance=" & LC.Insurrance & ",ExchangeRate=" & LC.ExchangeRate & ",BankPaidAmount=" & LC.BankPaidAmount & ",ShipingCharges=" & LC.ShipingCharges & ",PortCharges=" & LC.PortCharges & ",AssessedValue=" & LC.AssessedValue & ",DDForCMCC=" & LC.DDForCMCC & ",DDForETO=" & LC.DDForETO & ",TotalQty=" & LC.TotalQty & ",TotalAmount=" & LC.TotalAmount & ",TotalDuty=" & LC.TotalDuty & ",SalesTax=" & LC.SalesTax & ",AdditionalSalesTax=" & LC.AdditionalSalesTax & ",AdvanceIncomeTax=" & LC.AdvanceIncomeTax & ",UserName=N'" & LC.UserName.Replace("'", "''") & "',EntryDate=Convert(DateTime,N'" & LC.EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102), CostCenterId=" & LC.CostCenterId & ",ExciseDutyPercent=" & LC.ExciseDutyPercent & ",ExciseDuty=" & LC.ExciseDuty & ",LCDocID=" & LC.LCDocId & ",OtherCharges=" & LC.OthersCharges & ",ExpenseByLC=" & LC.ExpenseByLC & ", Financial_Impact=" & IIf(LC.FinancialImpact = True, 1, 0) & ", ShippingRemarks=N'" & LC.ShippingRemarks.Replace("'", "''") & "', PortRemarks=N'" & LC.PortRemarks.Replace("'", "''") & "', CMCCRemarks=N'" & LC.CMCCRemarks.Replace("'", "''") & "', ETORemarks=N'" & LC.ETORemarks.Replace("'", "''") & "',AdjCMCCAmount=" & Val(LC.AdjCMCCAmount) & ",AdjETOAmount=" & Val(LC.AdjETOAmount) & ",CurrencyID=" & Val(LC.CurrenyObj.CurrencyId) & ",CurrencyRate=" & Val(LC.CurrenyObj.CurrencyRate) & ", PurchaseOrderId = " & Val(LC.PurchaseOrderId) & ",  ReceivingNoteId= " & Val(LC.ReceivingNoteId) & ", Status ='" & LC.Status.Replace("'", "''") & "' WHERE LCID=" & LC.LCID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            'strSQL = String.Empty
            'LC.LCOtherDetail.LCId = LC.LCID
            'strSQL = "UPDATE LCOtherDetailTable SET RefNo=N'" & LC.LCOtherDetail.RefNo.Replace("'", "''") & "',OpeningDate=Convert(DateTime,N'" & LC.LCOtherDetail.OpeningDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),ExpiryDate=Convert(DateTime,N'" & LC.LCOtherDetail.ExpiryDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Amendment=N'" & LC.LCOtherDetail.Amendment.Replace("'", "''") & "',OpeningBankAcId=" & LC.LCOtherDetail.OpeningBankAcId & ",AdvisingBank=N'" & LC.LCOtherDetail.AdvisingBank.Replace("'", "''") & "',SpecialInstruction=N'" & LC.LCOtherDetail.SepcialInstruction.Replace("'", "''") & "',Remarks=N'" & LC.LCOtherDetail.Remarks.Replace("'", "''") & "',OpenedBy=" & LC.LCOtherDetail.OpenedBy & ",Vessel=N'" & LC.LCOtherDetail.Vessel.Replace("'", "''") & "',BLNo=N'" & LC.LCOtherDetail.BLNo.Replace("'", "''") & "',BLDate=Convert(DateTime,N'" & LC.LCOtherDetail.BLDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),ETDDate=Convert(DateTime, N'" & LC.LCOtherDetail.ETDDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),ETADate=Convert(DateTime,N'" & LC.LCOtherDetail.ETADate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),BankDocumentDate=Convert(DateTime,N'" & LC.LCOtherDetail.BankDocumentDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),BankPaymentDate=Convert(DateTime,N'" & LC.LCOtherDetail.BankPaymentDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),ClearingAgent=N'" & LC.LCOtherDetail.ClearingAgent.Replace("'", "''") & "',TransporterId=" & LC.LCOtherDetail.TransporterId & " WHERE LCId=" & LC.LCID & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            SubtractQtyForDelete(LC.LCID, trans)
            ''Below three lines are commented against TASK TFS1609
            'strSQL = String.Empty
            'strSQL = "Delete From LCDetailTable WHERE LCID=" & LC.LCID & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''END TASK TFS1609
            strSQL = String.Empty
            Dim intVoucherId As Integer = 0I

            ''Below line is commented on 23-10-2017
            'strSQL = "Select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & LC.LCNo.Replace("'", "''") & "'"

            strSQL = "Select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & LC.CurrentVoucherNo.Replace("'", "''") & "'"


            'Dim dtVoucher As New DataTable
            'dtVoucher = UtilityDAL.GetDataTable(strSQL, trans)
            'dtVoucher.AcceptChanges()
            'If dtVoucher IsNot Nothing Then
            '    If dtVoucher.Rows.Count > 0 Then
            '        intVoucherId = Val(dtVoucher.Rows(0).Item(0).ToString)
            '    End If
            'End If
            intVoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            LC.Voucher_Id = intVoucherId

            ''TASK TFS1609 on 18-10-2017
            If LC.FinancialImpact = True Then

            End If


            ''END TASK TFS1609

            ''Below lines are commented on 23-10-2017
            'strSQL = String.Empty
            'strSQL = "DELETE FROM tblVoucherDetail WHERE Voucher_Id=" & LC.Voucher_Id & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            If LC.FinancialImpact = True Then

                If LC.IsNewVoucher = False Then
                    If intVoucherId = 0 Then

                        strSQL = String.Empty
                        'Task#04082015 add post column in query
                        ''TASK TFS1427 Added UserName to be saved for voucher on 13-09-2017
                        strSQL = "INSERT INTO tblVoucher(Voucher_Code, Voucher_No,Voucher_Date,Location_Id,Voucher_Type_ID,Source,post, UserName, Reference) " _
                        & " VALUES(N'" & LC.LCNo.Replace("'", "''") & "',N'" & LC.CurrentVoucherNo.Replace("'", "''") & "', Convert(DateTime,N'" & LC.VoucherDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),1,6,'frmImport', " & IIf(LC.FinancialImpact = True, 1, 0) & ", N'" & LC.UserName & "', N'" & LC.VoucherRemarks.Replace("'", "''") & "')Select @@Identity"
                        intVoucherId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                        LC.Voucher_Id = intVoucherId
                    Else
                        strSQL = String.Empty
                        'Task#04082015 add post column in query
                        strSQL = "UPDATE tblVoucher SET Voucher_Code=N'" & LC.LCNo.Replace("'", "''") & "', Voucher_No=N'" & LC.CurrentVoucherNo.Replace("'", "''") & "',Voucher_Date=Convert(DateTime,N'" & LC.VoucherDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),post=" & IIf(LC.FinancialImpact = True, 1, 0) & ",Location_Id=1, Reference = N'" & LC.VoucherRemarks.Replace("'", "''") & "' WHERE Voucher_Id=" & LC.Voucher_Id & ""
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    End If
                End If
            Else

                'Task#04082015 Delete voucher from both tblVoucher and tblVoucherDetail table when financial impact check off
                strSQL = String.Empty
                'strSQL = "Delete from tblVoucher where voucher_id=" & LC.Voucher_Id

                strSQL = String.Empty
                strSQL = "Delete from tblVoucherDetail where voucher_id IN (Select voucher_id FROM tblVoucher WHERE voucher_code=N'" & LC.LCNo.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


                strSQL = "Delete from tblVoucher WHERE voucher_code=N'" & LC.LCNo.Replace("'", "''") & "' "

                Dim voucherID As Integer = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))

                'strSQL = String.Empty
                'strSQL = "Delete from tblVoucherDetail where voucher_id IN (Select voucher_id FROM tblVoucher WHERE voucher_code=N'" & LC.LCNo.Replace("'", "''") & "')"
                'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                'End Task#04082015

            End If
            ''Above lines are commented on 23-10-2017
            'AddDetail(LC, trans, LC.IsNewVoucher)
            If LC.FirstVoucher = True AndAlso LC.FinancialImpact = True Then
                AddDetail(LC, trans, LC.IsNewVoucher)
            Else
                UpdateDetail(LC, trans, LC.IsNewVoucher)
            End If


            UpdatePurchaseOrderStatus(LC.PurchaseOrderId, trans)
            'AddQtyForWhole(LC.PurchaseOrderId, trans)
            strSQL = String.Empty
            Dim intDocId As Integer = 0I
            strSQL = "Select StockTransID From StockMasterTable WHERE DocNo=N'" & LC.LCNo.Replace("'", "''") & "'"
            'Dim dtStockTrans As New DataTable
            'dtStockTrans = UtilityDAL.GetDataTable(strSQL, trans)
            'dtStockTrans.AcceptChanges()
            'If dtStockTrans IsNot Nothing Then
            '    If dtStockTrans.Rows.Count > 0 Then
            '        intDocId = Val(dtStockTrans.Rows(0).Item(0).ToString)
            '    End If
            'End If
            intDocId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From StockDetailTable WHERE StockTransId=" & intDocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            If LC.FinancialImpact = True Then
                If intDocId > 0 Then
                    strSQL = String.Empty
                    strSQL = "UPDATE StockMasterTable SET DocNo=N'" & LC.LCNo.Replace("'", "''") & "',DocDate=Convert(DateTime, N'" & LC.LCDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Remarks=N'" & LC.ImportName.Replace("'", "''") & "' WHERE StockTransID=" & intDocId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                Else
                    strSQL = String.Empty
                    strSQL = "INSERT INTO StockMasterTable(DocNo,DocDate,Remarks) VALUES(N'" & LC.LCNo.Replace("'", "''") & "',Convert(DateTime, N'" & LC.LCDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),N'" & LC.ImportName.Replace("'", "''") & "')Select @@Identity"
                    intDocId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                End If
                AddStockDetail(intDocId, LC, trans)
            End If

            LC.ActivityLog.ActivityName = "Update"
            LC.ActivityLog.ApplicationName = "Inventory"
            LC.ActivityLog.RecordType = "Import"
            LC.ActivityLog.RefNo = LC.LCNo
            Call UtilityDAL.BuildActivityLog(LC.ActivityLog, trans)


            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Delete(ByVal LC As LCBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            SubtractQtyForDelete(LC.LCID, trans)
            strSQL = String.Empty
            strSQL = "Delete From LCDetailTable WHERE LCID=" & LC.LCID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            UpdatePurchaseOrderStatus(LC.PurchaseOrderId, trans)
            'strSQL = String.Empty
            'strSQL = "Delete From LCOtherDetailTable WHERE LCID=" & LC.LCID & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            'strSQL = String.Empty
            'strSQL = "Delete From tblVoucherDetail WHERE Voucher_ID in(Select Voucher_Id From tblVoucher WHERE Voucher_No='" & LC.LCNo.Replace("'", "''") & "')"
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

           
            ''Delete voucher
            strSQL = String.Empty
            strSQL = "Delete from tblVoucherDetail where voucher_id IN (Select voucher_id FROM tblVoucher WHERE voucher_code=N'" & LC.LCNo.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            strSQL = "Delete from tblVoucher WHERE voucher_code=N'" & LC.LCNo.Replace("'", "''") & "' "

            Dim voucherID As Integer = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))
            ''END DELETE voucher



            strSQL = String.Empty
            strSQL = "Delete From StockDetailTable WHERE StockTransId In(Select StockTransId From StockMasterTable WHERE DocNo=N'" & LC.LCNo.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From tblVoucher WHERE Voucher_No='" & LC.LCNo.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From LCMasterTable  WHERE LCID=" & LC.LCID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From StockMasterTable WHERE DocNo=N'" & LC.LCNo.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            LC.ActivityLog.ActivityName = "Delete"
            LC.ActivityLog.ApplicationName = "Inventory"
            LC.ActivityLog.RecordType = "Import"
            LC.ActivityLog.RefNo = LC.LCNo
            Call UtilityDAL.BuildActivityLog(LC.ActivityLog, trans)


            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    ''Ayesha Rehman : 18-04-2018
    Public Function DeleteDetail(ByVal LCID As Integer, ByVal LCDetailId As Integer, Optional ByVal LCNO As String = "") As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim voucherId As Integer
            'Dim ItemID As Integer
            'Dim ItemName As String
            'Dim strItem As String
            'strItem = "SELECT ArticleDefId from LCDetailTable WHERE LCID=" & LCID & " And  LCDetailId =" & LCDetailId & ""
            'Dim dtItem As DataTable = UtilityDAL.GetDataTable(strItem)
            'If dtItem.Rows.Count > 0 Then
            '    ItemID = dtItem.Rows(0).Item("ArticleDefId")
            'End If
            'Dim strItemName As String
            'strItemName = "select ArticleDescription from ArticleDefView where ArticleId = " & ItemID & ""
            'Dim dtItemName As DataTable = UtilityDAL.GetDataTable(strItemName)
            'If dtItemName.Rows.Count > 0 Then
            '    ItemName = dtItemName.Rows(0).Item("ArticleDescription")
            'End If
            Dim strVoucher As String
            strVoucher = "SELECT voucher_id from tblVoucher where voucher_no = '" & LCNO & "'"
            Dim dtVoucher As DataTable = UtilityDAL.GetDataTable(strVoucher)
            If dtVoucher.Rows.Count > 0 Then
                voucherId = dtVoucher.Rows(0).Item("voucher_id")
            End If
            Dim strSQL1 As String = String.Empty
            strSQL1 = "Delete From tblVoucherDetail WHERE voucher_id=" & voucherId & " And  LCDetailId =" & LCDetailId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL1)
            Dim strSQL As String = String.Empty
            strSQL = "Delete From LCDetailTable WHERE LCID=" & LCID & " And  LCDetailId =" & LCDetailId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'Dim strStock As String
            'strStock = "SELECT StockTransId from StockMasterTable where DocNo = '" & LCNO & "'"
            'Dim dtStock As DataTable = UtilityDAL.GetDataTable(strStock)
            'If dtStock.Rows.Count > 0 Then
            '    StockTransId = dtStock.Rows(0).Item("StockTransId")
            'End If
            'strSQL = "Delete From StockDetailTable WHERE StockTransId=" & StockTransId & " And  LCDetailId =" & LCDetailId & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    ''End TFS

    Public Function DisplayRecord(Optional ByVal Condition As String = "") As DataTable
        Try

            Dim strSQL As String = String.Empty
            'strSQL = "SELECT  " & IIf(Condition.Length > 1, "", " TOP 50 ") & " LC_M.LCID, LC_M.LCNo, LC_M.LCDate, LC_M.LCAccountId, dbo.vwCOADetail.detail_title AS [LC Account], LC_M.LCExpenseAccountId, " _
            '        & "  COA1.detail_title AS [Expense Ac], LC_M.ImportName, LC_M.PerformaNo, LC_M.PerformaDate, LC_M.PaymentMode,LC_M.LCBeforeDate, LC_M.PortOfLoading, LC_M.PortOfDischarge,  " _
            '        & "  LC_M.SupplierAccountId, COA2.detail_title AS [Supplier Ac], LC_M.IndenterAccountId, COA3.detail_title AS [Indenter Ac], LC_M.PartialShipment, LC_M.Transhipment,    " _
            '        & "  LC_M.LatestDateOfShipment, LC_M.LSBDate, LC_M.Insurrance, LC_M.ExchangeRate, LC_M.BankPaidAmount, LC_M.ShipingCharges, LC_M.PortCharges,  " _
            '        & "  LC_M.AssessedValue, LC_M.DDForCMCC, LC_M.DDForETO, LC_M.TotalQty, LC_M.TotalAmount, LC_M.TotalDuty AS Duty, LC_M.SalesTax,  " _
            '        & "  LC_M.AdditionalSalesTax AS [Add Sales Tax], LC_M.AdvanceIncomeTax AS [Adv Income Tax], LC_M.UserName " _
            '        & "  FROM dbo.LCMasterTable AS LC_M LEFT OUTER JOIN " _
            '        & "  dbo.vwCOADetail AS COA3 ON LC_M.IndenterAccountId = COA3.coa_detail_id LEFT OUTER JOIN " _
            '        & "  dbo.vwCOADetail AS COA2 ON LC_M.SupplierAccountId = COA2.coa_detail_id LEFT OUTER JOIN " _
            '        & "  dbo.vwCOADetail AS COA1 ON LC_M.LCExpenseAccountId = COA1.coa_detail_id LEFT OUTER JOIN " _
            '        & "  dbo.vwCOADetail ON LC_M.LCAccountId = dbo.vwCOADetail.coa_detail_id " _
            '        & "  ORDER BY LC_M.LCID DESC "
            'strSQL = "SELECT " & IIf(Condition.Length > 1, "", " TOP 50 ") & " PERCENT LC_M.LCID, LC_M.LCNo, LC_M.LCDate, LC_M.LCAccountId, dbo.vwCOADetail.detail_title AS [LC Account], LC_M.ImportName, LC_M.Transhipment, " _
            '          & " ISNULL(LC_M.Insurrance, 0) AS Insurrance, ISNULL(LC_M.ExchangeRate, 0) AS ExchangeRate, ISNULL(LC_M.OtherCharges, 0) AS OtherCharges,  " _
            '          & " ISNULL(LC_M.ExpenseByLC, 0) AS ExpenseByLC, ISNULL(LC_M.AssessedValue, 0) AS AssessedValue, ISNULL(LC_M.DDForCMCC, 0) AS DDForCMCC,  " _
            '          & " ISNULL(LC_M.DDForETO, 0) AS DDForETO, ISNULL(LC_M.TotalQty, 0) AS TotalQty, ISNULL(LC_M.TotalAmount, 0) AS TotalAmount, ISNULL(LC_M.TotalDuty, 0) AS Duty,  " _
            '          & " ISNULL(LC_M.SalesTax, 0) AS SalesTax, LC_M.AdditionalSalesTax AS [Add Sales Tax], LC_M.AdvanceIncomeTax AS [Adv Income Tax],  " _
            '          & " ISNULL(LC_M.ExciseDutyPercent, 0) AS [Excise Duty%], ISNULL(LC_M.ExciseDuty, 0) AS ExciseDuty, ISNULL(LC_M.CostCenterId, 0) AS CostCenterId,  " _
            '          & " CS.Name AS [Cost Center], ISNULL(LC_M.LCDocId, 0) AS LCDocId, ISNULL(Doc_Att.[No Of Attachment], 0) AS [No Of Attachment], ISNULL(LC_M.Financial_Impact, 0)  " _
            '          & " AS Financial_Impact, LC_M.UserName " _
            '          & " FROM dbo.LCMasterTable AS LC_M LEFT OUTER JOIN   dbo.vwCOADetail ON LC_M.LCAccountId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
            '          & " dbo.tblDefCostCenter AS CS ON LC_M.CostCenterId = CS.CostCenterID LEFT OUTER JOIN (SELECT     COUNT(*) AS [No Of Attachment], DocId " _
            '          & " FROM dbo.DocumentAttachment WHERE (Source = 'frmImport')  GROUP BY DocId, Source) AS Doc_Att ON Doc_Att.DocId = LC_M.LCID ORDER BY LC_M.LCID DESC "
            '' TASK TFS1261 Added new column ReceivingNoteId for GRN.
            ''TASK TFS1350 added new field of Status on 22-08-2017
            strSQL = "SELECT " & IIf(Condition.Length > 1, "", " TOP 50 ") & " LC_M.LCID, LC_M.LCNo, LC_M.LCDate, LC_M.LCAccountId, dbo.vwCOADetail.detail_title AS [LC Account], CS.Name AS [Cost Center], LC_M.ImportName, LC_M.Transhipment, " _
          & " ISNULL(LC_M.Insurrance, 0) AS Insurrance, ISNULL(LC_M.ExchangeRate, 0) AS ExchangeRate, ISNULL(LC_M.OtherCharges, 0) AS OtherCharges,  " _
          & " ISNULL(LC_M.ExpenseByLC, 0) AS ExpenseByLC, ISNULL(LC_M.AssessedValue, 0) AS AssessedValue, ISNULL(LC_M.DDForCMCC, 0) AS DDForCMCC, IsNull(LC_M.AdjCMCCAmount,0) as [Adj CMCC], " _
          & " ISNULL(LC_M.DDForETO, 0) AS DDForETO, IsNull(LC_M.AdjETOAmount,0) as [Adj ETO], ISNULL(LC_M.TotalQty, 0) AS TotalQty, ISNULL(LC_M.TotalAmount, 0) AS TotalAmount, ISNULL(LC_M.TotalDuty, 0) AS Duty,  " _
          & " ISNULL(LC_M.SalesTax, 0) AS SalesTax, LC_M.AdditionalSalesTax AS [Add Sales Tax], LC_M.AdvanceIncomeTax AS [Adv Income Tax],  " _
          & " ISNULL(LC_M.ExciseDutyPercent, 0) AS [Excise Duty%], ISNULL(LC_M.ExciseDuty, 0) AS ExciseDuty, ISNULL(LC_M.CostCenterId, 0) AS CostCenterId,  " _
          & " ISNULL(LC_M.LCDocId, 0) AS LCDocId, ISNULL(Doc_Att.[No Of Attachment], 0) AS [No Of Attachment], ISNULL(LC_M.Financial_Impact, 0)  " _
          & " AS Financial_Impact, LC_M.UserName, LC_M.ShippingRemarks, LC_M.PortRemarks,LC_M.CMCCRemarks, LC_M.ETORemarks, IsNull(LC_M.PurchaseOrderId, 0) As PurchaseOrderId , IsNull(LC_M.ReceivingNoteId, 0) As ReceivingNoteId, LC_M.Status " _
          & " FROM dbo.LCMasterTable AS LC_M LEFT OUTER JOIN   dbo.vwCOADetail ON LC_M.LCAccountId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
          & " dbo.tblDefCostCenter AS CS ON LC_M.CostCenterId = CS.CostCenterID LEFT OUTER JOIN (SELECT     COUNT(*) AS [No Of Attachment], DocId " _
          & " FROM dbo.DocumentAttachment WHERE (Source = 'frmImport')  GROUP BY DocId, Source) AS Doc_Att ON Doc_Att.DocId = LC_M.LCID ORDER BY LC_M.LCID DESC "
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.TableName = "Main"
            dt.AcceptChanges()
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSingle(ByVal LCId As Integer) As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT LC_M.LCID, LC_M.LCNo, LC_M.LCDate, LC_M.LCAccountId, dbo.vwCOADetail.detail_title AS [LC Account], CS.Name AS [Cost Center], LC_M.ImportName, LC_M.Transhipment, " _
          & " ISNULL(LC_M.Insurrance, 0) AS Insurrance, ISNULL(LC_M.ExchangeRate, 0) AS ExchangeRate, ISNULL(LC_M.OtherCharges, 0) AS OtherCharges,  " _
          & " ISNULL(LC_M.ExpenseByLC, 0) AS ExpenseByLC, ISNULL(LC_M.AssessedValue, 0) AS AssessedValue, ISNULL(LC_M.DDForCMCC, 0) AS DDForCMCC, IsNull(LC_M.AdjCMCCAmount,0) as [Adj CMCC], " _
          & " ISNULL(LC_M.DDForETO, 0) AS DDForETO, IsNull(LC_M.AdjETOAmount,0) as [Adj ETO], ISNULL(LC_M.TotalQty, 0) AS TotalQty, ISNULL(LC_M.TotalAmount, 0) AS TotalAmount, ISNULL(LC_M.TotalDuty, 0) AS Duty,  " _
          & " ISNULL(LC_M.SalesTax, 0) AS SalesTax, LC_M.AdditionalSalesTax AS [Add Sales Tax], LC_M.AdvanceIncomeTax AS [Adv Income Tax],  " _
          & " ISNULL(LC_M.ExciseDutyPercent, 0) AS [Excise Duty%], ISNULL(LC_M.ExciseDuty, 0) AS ExciseDuty, ISNULL(LC_M.CostCenterId, 0) AS CostCenterId,  " _
          & " ISNULL(LC_M.LCDocId, 0) AS LCDocId, ISNULL(Doc_Att.[No Of Attachment], 0) AS [No Of Attachment], ISNULL(LC_M.Financial_Impact, 0)  " _
          & " AS Financial_Impact, LC_M.UserName, LC_M.ShippingRemarks, LC_M.PortRemarks,LC_M.CMCCRemarks,LC_M.ETORemarks, IsNull(LC_M.PurchaseOrderId, 0) As PurchaseOrderId, IsNull(LC_M.ReceivingNoteId, 0) As ReceivingNoteId, LC_M.Status " _
          & " FROM dbo.LCMasterTable AS LC_M LEFT OUTER JOIN   dbo.vwCOADetail ON LC_M.LCAccountId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
          & " dbo.tblDefCostCenter AS CS ON LC_M.CostCenterId = CS.CostCenterID LEFT OUTER JOIN (SELECT     COUNT(*) AS [No Of Attachment], DocId " _
          & " FROM dbo.DocumentAttachment WHERE (Source = 'frmImport') GROUP BY DocId, Source) AS Doc_Att ON Doc_Att.DocId = LC_M.LCID Where LC_M.LCID =" & LCId & " ORDER BY LC_M.LCID DESC "
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.TableName = "Main"
            dt.AcceptChanges()
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DisplayDetail(ByVal ReceiveId As Integer) As DataTable
        Try
            Dim strSQL As String = String.Empty
            ''Edit for TFS1956
            ''Edit Query ,Get BatchNo,ExpiryDate
            strSQL = "SELECT LC_D.LocationId, LC_D.ArticleDefId, ART.ArticleCode as [Code], ART.ArticleDescription as [Item],ART.HS_Code, ART.ArticleGenderName as [Origin], ART.ArticleColorName as [Color],ART.ArticleSizeName as [Size], LC_D.ArticleSize as [Unit], " _
                     & " LC_D.PackDesc, LC_D.Sz1 AS Qty, LC_D.Sz7 AS [Pack Qty], IsNull(LC_D.Exch_Rate,0) as Exch_Rate, IsNull(LC_D.Import_Value,0) as Import_Value, IsNull(LC_D.ImportAmount,0) as ImportAmount,LC_D.Price, LC_D.TotalAmount,isnull( LC_D.Weight,0) as Weight ,isnull( LC_D.Per_Kg_Cost,0) as Per_Kg_Cost ,isnull( LC_D.Weighted_Cost,0) as Weighted_Cost, LC_D.Insurrance, LC_D.AddCostPercent as [Add.Cost%], LC_D.AssessedValue as [Assd.Value],  " _
                     & " LC_D.DutyPercent as [Duty%], LC_D.Duty,LC_D.AddCustomDutyPercent as [AddCustomDuty%], LC_D.AddCustomDuty,LC_D.RegulatoryDutyPercent as [RegulatoryDuty%], LC_D.RegulatoryDuty , LC_D.SaleTaxPercent as [S. Tax%], LC_D.SaleTax, LC_D.AddSaleTaxPercent as [Add.S.Tax%], LC_D.AddSaleTax as [Add.S.Tax], LC_D.AdvIncomeTaxPercent as [Adv.I.Tax%], LC_D.AdvIncomeTax as [Adv.I.Tax], Convert(money,0) as [Net Amount], IsNull(LC_D.ExciseDutyPercent,0) as ExciseDutyPercent, IsNull(LC_D.ExciseDuty,0) as ExciseDuty, ISNULL(LC_D.BatchNo, 'xxxx') AS BatchNo,ISNULL(LC_D.ExpiryDate, getDate()) as ExpiryDate, IsNull(LC_D.Other_Charges,0) as Other_Charges, LC_D.Comments, ISNULL(LC_D.PurchaseAccountId,0) as PurchaseAccountId, ISNULL(LC_D.Qty, 0) As TotalQty, IsNull(LC_D.PurchaseOrderId, 0) As PurchaseOrderId, IsNull(LC_D.PurchaseOrderDetailId, 0) As PurchaseOrderDetailId, IsNull(LC_D.LCDetailId, 0) As LCDetailId, IsNull(LC_D.Other_Charges,0) as CheckOtherCharges, IsNull(LC_D.ReceivingNoteId,0) AS ReceivingNoteId " _
                     & " FROM dbo.LCDetailTable AS LC_D INNER JOIN dbo.ArticleDefView AS ART ON LC_D.ArticleDefId = ART.ArticleId " _
                     & " WHERE (LC_D.LCID=" & ReceiveId & ") " _
                     & " ORDER BY LC_D.LCDetailId "
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.TableName = "Detail"
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function DisplayPODetail(ByVal ReceiveId As Integer) As DataTable
        Try
            'PurchaseOrderDetailId	int	Unchecked
            'PurchaseOrderId	int	Unchecked
            'LocationId	int	Checked
            'ArticleDefId	int	Checked
            'ArticleSize	nvarchar(10)	Checked
            'Sz1	float	Checked
            'Sz2	float	Checked
            'Sz3	float	Checked
            'Sz4	float	Checked
            'Sz5	float	Checked
            'Sz6	float	Checked
            'Sz7	float	Checked
            'Qty	float	Checked
            'Price	float	Checked
            'CurrentPrice	float	Checked
            'DeliveredQty	float	Checked
            'PackPrice	float	Checked
            'Pack_Desc	nvarchar(300)	Checked
            'TaxPercent	float	Checked
            'Comments	nvarchar(1000)	Checked
            'DemandID	int	Checked
            'Item_Wise_Disc	int	Checked
            'Pack_40Kg_Weight	float	Checked
            'AdTax_Percent	float	Checked
            'AdTax_Amount	decimal(18, 4)	Checked
            'PurchaseDemandDetailId	int	Checked
            'ReceivedTotalQty	float	Checked
            '            Unchecked()

            Dim strSQL As String = String.Empty
            ''Edit for TFS1956
            strSQL = "SELECT LC_D.LocationId, LC_D.ArticleDefId, ART.ArticleCode as [Code], ART.ArticleDescription as [Item],ART.HS_Code, ART.ArticleGenderName as [Origin], ART.ArticleColorName as [Color],ART.ArticleSizeName as [Size], LC_D.ArticleSize as [Unit], " _
                     & " LC_D.Pack_Desc As PackDesc, (IsNull(LC_D.Sz1, 0)-IsNull(LC_D.DeliveredQty, 0)) AS Qty, LC_D.Sz7 AS [Pack Qty], 0 as Exch_Rate, 0 as Import_Value, 0 as ImportAmount, LC_D.Price, 0 As TotalAmount, 0 as Weight, 0 as Per_Kg_Cost, 0 as Weighted_Cost, 0 As Insurrance, 0 as [Add.Cost%], 0 as [Assd.Value],  " _
                     & " Convert(float, 0) as [Duty%], Convert(float, 0) As Duty, Convert(float, 0) as [AddCustomDuty%], Convert(float, 0) As AddCustomDuty, Convert(float, 0) as [RegulatoryDuty%], Convert(float, 0) As RegulatoryDuty,  LC_D.TaxPercent as [S. Tax%], Convert(float, 0) As SaleTax, LC_D.AdTax_Percent As [Add.S.Tax%], LC_D.AdTax_Amount as [Add.S.Tax], Convert(float, 0) as [Adv.I.Tax%], Convert(float, 0) as [Adv.I.Tax], Convert(money,0) as [Net Amount], Convert(float, 0) as ExciseDutyPercent, Convert(float, 0) as ExciseDuty,'xxxx' AS BatchNo,getDate() as ExpiryDate, Convert(float, 0) as Other_Charges, LC_D.Comments, 0 as PurchaseAccountId, (ISNULL(LC_D.Qty, 0)-IsNull(LC_D.ReceivedTotalQty, 0)) As TotalQty, LC_D.PurchaseOrderId, LC_D.PurchaseOrderDetailId, 0 As LCDetailId, 0 As CheckOtherCharges,  0 As ReceivingNoteId, LC_D.CurrencyId " _
                     & " FROM dbo.PurchaseOrderDetailTable AS LC_D LEFT OUTER JOIN dbo.ArticleDefView AS ART ON LC_D.ArticleDefId = ART.ArticleId " _
                     & " WHERE (LC_D.PurchaseOrderId=" & ReceiveId & ") And IsNull(LC_D.Sz1, 0) > IsNull(LC_D.DeliveredQty, 0) " _
                     & " ORDER BY LC_D.PurchaseOrderDetailId "
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.TableName = "PODetail"
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdatePurchaseOrderStatus(ByVal PurchaseOrderId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update PurchaseOrderMasterTable Set Status = Case When IsNull(CurrentQty.Qty, 0) > 0 Then 'Open' Else 'Close' End FROM PurchaseOrderMasterTable INNER JOIN (Select PODetail.PurchaseOrderId, Sum(IsNull(PODetail.Qty, 0)-IsNull(LCDetail.Qty,0)) As Qty From PurchaseOrderDetailTable As PODetail LEFT OUTER JOIN LCDetailTable As LCDetail ON PODetail.PurchaseOrderId = LCDetail.PurchaseOrderId  WHERE PODetail.PurchaseOrderId = " & PurchaseOrderId & " Group By PODetail.PurchaseOrderId) As CurrentQty ON PurchaseOrderMasterTable.PurchaseOrderId = CurrentQty.PurchaseOrderId Where PurchaseOrderMasterTable.PurchaseOrderId = " & PurchaseOrderId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddPOQty(ByVal PurchaseOrderId As Integer, ByVal PurchaseOrderDetailId As Integer, ByVal Sz1 As Double, ByVal Qty As Double, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update PurchaseOrderDetailTable Set DeliveredQty = IsNull(DeliveredQty, 0)+" & Sz1 & ", ReceivedTotalQty = IsNull(ReceivedTotalQty, 0)+" & Qty & " Where PurchaseOrderId =" & PurchaseOrderId & " And PurchaseOrderDetailId =" & PurchaseOrderDetailId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function SubtractPOQty(ByVal PurchaseOrderId As Integer, ByVal PurchaseOrderDetailId As Integer, ByVal Sz1 As Double, ByVal Qty As Double, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update PurchaseOrderDetailTable Set DeliveredQty = IsNull(DeliveredQty, 0)-" & Sz1 & ", ReceivedTotalQty = IsNull(ReceivedTotalQty, 0)-" & Qty & " Where PurchaseOrderId =" & PurchaseOrderId & " And PurchaseOrderDetailId =" & PurchaseOrderDetailId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function SubtractQtyForDelete(ByVal LCId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update PurchaseOrderDetailTable Set DeliveredQty = IsNull(DeliveredQty, 0)-IsNull(LCDetail.Sz1, 0), ReceivedTotalQty = IsNull(ReceivedTotalQty, 0)-IsNull(LCDetail.Qty, 0) FROM PurchaseOrderDetailTable INNER JOIN (Select PurchaseOrderId, PurchaseOrderDetailId, LCId, Sz1, Qty From LCDetailTable) As LCDetail ON PurchaseOrderDetailTable.PurchaseOrderDetailId = LCDetail.PurchaseOrderDetailId and PurchaseOrderDetailTable.PurchaseOrderId = LCDetail.PurchaseOrderId Where LCDetail.LCId =" & LCId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddQtyForWhole(ByVal PurchaseOrderId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update PurchaseOrderDetailTable Set DeliveredQty = IsNull(DeliveredQty, 0)+IsNull(LCDetail.Sz1, 0), ReceivedTotalQty = IsNull(ReceivedTotalQty, 0)+IsNull(LCDetail.Qty, 0) FROM PurchaseOrderDetailTable INNER JOIN (Select PurchaseOrderId, PurchaseOrderDetailId, Sz1, Qty From LCDetailTable) As LCDetail ON PurchaseOrderDetailTable.PurchaseOrderDetailId = LCDetail.PurchaseOrderDetailId and PurchaseOrderDetailTable.PurchaseOrderId = LCDetail.PurchaseOrderId Where PurchaseOrderDetailTable.PurchaseOrderId =" & PurchaseOrderId & " And IsNull(PurchaseOrderDetailTable.Sz1, 0) > IsNull(LCDetail.Sz1, 0)"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Enum enmGrdDetail
        LocationId
        ArticleDefId
        Code
        Item
        HS_Code
        Origin
        Color
        Size
        Unit
        PackDesc
        Qty
        PackQty
        Price
        TotalAmount
        Insurrance
        AddCostPercent
        AssessedValue
        DutyPercent
        Duty
        AddCustomDutyPercent   ''TFS1956
        AddCustomDuty          ''TFS1956
        RegulatoryDutyPercent  ''TFS1956
        RegulatoryDuty         ''TFS1956
        SaleTaxPercent
        SaleTax
        AddSaleTaxPercent
        AddSaleTax
        AdvIncomeTaxPercent
        AdvIncomeTax
        Net_Amount
        ExciseDutyPercent
        ExciseDuty
        Comments
        PurchaseAccountId
        TotalQty
        PurchaseOrderId
        PurchaseOrderDetailId
    End Enum


    Public Function GetNextVoucherNo(ByVal DocumentNo As String) As String
        Dim Voucher_No As String = ""
        Try
            Dim strSQL As String = String.Empty

            strSQL = " select  isnull(max(convert(integer,substring (tblVoucher.voucher_no, " & DocumentNo.Length + 2 & " ,2))),0)+1 from tblVoucher where tblVoucher.voucher_no like '" & DocumentNo & "%'"

            'strSQL = "SELECT voucher_no, (case when voucher_no like '%-%' then right(voucher_no, charindex('-', reverse(voucher_no)) - 1) else voucher_no end) As VoucherNo from tblVoucher Where voucher_code ='" & DocumentNo & "'"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Voucher_No = DocumentNo & "-" & dt.Rows(0).Item(0).ToString
                'Voucher_No = dt.Rows(0).Item(0).ToString

            End If
            'If dt.Rows.Count > 1 Then
            '    Voucher_No = dt.Select("Convert(int, VoucherNo) = Max(Convert(int, VoucherNo))").GetValue(0).ToString()
            '    Dim subStr As String = Voucher_No.Substring(Voucher_No.LastIndexOf("-"), 2)
            '    Dim value As Integer = Val(subStr) + 1
            '    DocumentNo = DocumentNo & value.ToString()
            'Else
            '    Voucher_No = dt.Rows(0).Item("voucher_no")
            '    Voucher_No = Voucher_No & "-1"

            'End If
            Return Voucher_No
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetCurrentVoucherNo(ByVal DocumentNo As String) As String
        Dim Voucher_No As String = ""
        Try
            Dim strSQL As String = String.Empty
            strSQL = " select  isnull(max(convert(integer,substring (tblVoucher.voucher_no, " & DocumentNo.Length + 2 & " ,2))),0) from tblVoucher where tblVoucher.voucher_no like '" & DocumentNo & "%'"

            'strSQL = "SELECT voucher_no, (case when voucher_no like '%-%' then right(voucher_no, charindex('-', reverse(voucher_no)) - 1) else voucher_no end) As VoucherNo from tblVoucher Where voucher_code ='" & DocumentNo & "'"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) > 0 Then
                    Voucher_No = DocumentNo & "-" & dt.Rows(0).Item(0).ToString

                Else
                    Voucher_No = DocumentNo
                End If
            End If
            Return Voucher_No
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetVoucherReference(ByVal DocumentNo As String) As String
        Dim Voucher_No As String = ""
        Try
            Dim strSQL As String = String.Empty
            strSQL = " SELECT Reference from tblVoucher where tblVoucher.voucher_no = '" & DocumentNo & "'"
            'strSQL = "SELECT voucher_no, (case when voucher_no like '%-%' then right(voucher_no, charindex('-', reverse(voucher_no)) - 1) else voucher_no end) As VoucherNo from tblVoucher Where voucher_code ='" & DocumentNo & "'"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Voucher_No = dt.Rows(0).Item(0).ToString
            End If
            Return Voucher_No
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Start TFS4234 : Ayesha Rehman : 16-08-2018 : This Function is made to calculate the Cost Price 
    Public Function GetCostPrice(ByVal LCDt As LCDetailBE, ByVal trans As SqlTransaction) As Double
        Try

       
        Dim strData As String = "Select ArticleDefID, CASE WHEN IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0) > 0 THEN IsNull(SUM(IsNull(InQty,0)-IsNull(OutQty,0)),0) +" & Val(LCDt.Qty) & " ELSE " & Val(LCDt.Qty) & " END AS BalanceQty, CASE WHEN SUM((IsNull(Rate,0)*IsNull(InQty,0))-(IsNull(Rate,0)*IsNull(OutQty,0))) > 0 THEN SUM((IsNull(Rate,0)*IsNull(InQty,0))-(IsNull(Rate,0)*IsNull(OutQty,0))) + " & (Val(LCDt.Other_Charges) / Val(LCDt.Qty)) * Val(LCDt.Qty) & " ELSE " & (Val(LCDt.Other_Charges) / Val(LCDt.Qty)) * Val(LCDt.Qty) & " END AS BalanceAmount, IsNull(SUM(IsNull(InQty, 0) - IsNull(OutQty, 0)), 0)+" & Val(LCDt.Qty) & " AS CheckStock From StockDetailTable WHERE ArticleDefID=" & Val(LCDt.ArticleDefId) & " AND IsNull(Rate,0) <> 0 Group By ArticleDefId "
        Dim dblCostPrice As Double = 0D
        Dim dtLastestPriceData As New DataTable
        dtLastestPriceData = UtilityDAL.GetDataTable(strData, trans)
        dtLastestPriceData.AcceptChanges()

        If dtLastestPriceData.Rows.Count > 0 Then
            If Val(dtLastestPriceData.Rows(0).Item(1).ToString) > 0 Then
                dblCostPrice = Val(Val(dtLastestPriceData.Rows(0).Item(2).ToString) / Val(dtLastestPriceData.Rows(0).Item(1).ToString))
                If dblCostPrice = 0 Then
                    dblCostPrice = Val(LCDt.Other_Charges) / Val(LCDt.Qty)
                End If
            Else
                dblCostPrice = Val(LCDt.Other_Charges) / Val(LCDt.Qty)
            End If
        Else
            dblCostPrice = Val(LCDt.Other_Charges) / Val(LCDt.Qty)
            End If
            Return dblCostPrice
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try    
    End Function
    ''End TFS4234
End Class

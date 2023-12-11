'Task No 2638 Dal for Rikshaw Claim Record 
''11-Jun-2015 Task# 2-11-06-2015 Ahmad Sharif: Modify Query , add Customer Name  

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Net
Imports Microsoft.VisualBasic
Public Class ClaimDAL
    Enum enmWarrantyClaim
        DocId
        DocNo
        DocDate
        ProjectId
        LocationId
        Post
        Remarks
        TotalQty
        TotalAmount
        Adjustment
        UserId
        EntryDate
    End Enum
    Enum enmWarrantyClaimDetail
        DocDetailId
        DocId
        LocationId
        ArticleDefId
        ArticleSize
        Sz1
        Sz2
        Sz3
        Sz4
        Sz5
        Sz6
        Sz7
        Qty
        Price
        Current_Price
        Tax_Percent
        Comments
        WarrantyAble
        Engine_No
        Chassis_No
        AccountId
        PackDesc
    End Enum

    Public Function SaveRecord(ByVal WarrantyClaim As ClaimBE) As Boolean


        Dim objCon As New SqlConnection(SQLHelper.CON_STR)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As SqlTransaction = objCon.BeginTransaction
        Try

            Dim Prefix As String = String.Empty
            Prefix = WarrantyClaim.DocNo.Substring(0, WarrantyClaim.DocNo.Length - 5)
            WarrantyClaim.DocNo = GetSerialNo(Prefix, objTrans)
            '-------------------------- Warranty Claim Document ----------------------
            Dim strSQL As String = String.Empty
            'strSQL = "INSERT INTO WarrantyClaimMasterTable(DocNo,DocDate,CustomerCode,ProjectId,LocationId,Post,Remarks,TotalQty,TotalAmount,Adjustment,UserId,EntryDate) " _
            '& " VALUES(N'" & WarrantyClaim.DocNo.Replace("'", "''") & "', Convert(DateTime, N'" & WarrantyClaim.DocDate & "',102)," & WarrantyClaim.CustomerCode & ", " & WarrantyClaim.ProjectId & ", " & WarrantyClaim.LocationId & ", " & IIf(WarrantyClaim.Post = True, 1, 0) & ", N'" & WarrantyClaim.Remarks.Replace("'", "''") & "', " & WarrantyClaim.TotalQty & "," & WarrantyClaim.TotalAmount & "," & WarrantyClaim.Adjustment & ", " & WarrantyClaim.UserId & ", Convert(DateTime, N'" & WarrantyClaim.EntryDate & "',102)) Select @@Identity"
            'WarrantyClaim.DocId = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)

            strSQL = "INSERT INTO WarrantyClaimMasterTable(DocNo,DocDate,CustomerCode,ProjectId,LocationId,Post,Remarks,TotalQty,TotalAmount,Adjustment,UserId,EntryDate,DeliveryID) " _
                   & " VALUES(N'" & WarrantyClaim.DocNo.Replace("'", "''") & "', Convert(DateTime, N'" & WarrantyClaim.DocDate & "',102)," & WarrantyClaim.CustomerCode & ", " & WarrantyClaim.ProjectId & ", " & WarrantyClaim.LocationId & ", " & IIf(WarrantyClaim.Post = True, 1, 0) & ", N'" & WarrantyClaim.Remarks.Replace("'", "''") & "', " & WarrantyClaim.TotalQty & "," & WarrantyClaim.TotalAmount & "," & WarrantyClaim.Adjustment & ", " & WarrantyClaim.UserId & ", Convert(DateTime, N'" & WarrantyClaim.EntryDate & "',102)," & WarrantyClaim.DeliveryID & ") Select @@Identity"
            WarrantyClaim.DocId = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)

            '--------------------------- Stock Document ------------------------------
            strSQL = String.Empty
            'strSQL = "INSERT INTO StockMasterTable(DocNo,DocDate,Project,DocType,Remarks,Project,Account_Id) VALUES(N'" & WarrantyClaim.DocNo.Replace("'", "''") & "', Convert(DateTime, N'" & WarrantyClaim.DocDate & "',102), " & WarrantyClaim.ProjectId & ", 0, N'" & WarrantyClaim.Remarks.Replace("'", "''") & "'," & WarrantyClaim.ProjectId & "," & WarrantyClaim.CustomerCode & ") Select @@Identity"
            strSQL = "INSERT INTO StockMasterTable(DocNo,DocDate,DocType,Remarks,Project,Account_Id) VALUES(N'" & WarrantyClaim.DocNo.Replace("'", "''") & "', Convert(DateTime, N'" & WarrantyClaim.DocDate & "',102), 8, N'" & WarrantyClaim.Remarks.Replace("'", "''") & "'," & WarrantyClaim.ProjectId & "," & WarrantyClaim.CustomerCode & ") Select @@Identity"
            WarrantyClaim.StockTransId = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)

            '-------------------------- Voucher Document -----------------------------
            strSQL = String.Empty
            strSQL = "INSERT INTO tblVoucher(Voucher_No, Voucher_Code, Voucher_Type_Id, Voucher_Date,Location_Id, finiancial_year_id, voucher_month, Post, Source) " _
            & " VALUES(N'" & WarrantyClaim.DocNo.Replace("'", "''") & "',N'" & WarrantyClaim.DocNo.Replace("'", "''") & "',1,Convert(DateTime,N'" & WarrantyClaim.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "', 102)," & WarrantyClaim.LocationId & ",1," & WarrantyClaim.DocDate.Month & ", " & IIf(WarrantyClaim.Post = True, 1, 0) & ", 'frmClaim' ) Select @@Identity"
            WarrantyClaim.VoucherId = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)

            '------------------------ Detail Record ----------------------------------
            SaveDetailRecord(WarrantyClaim, objTrans)

            objTrans.Commit()
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Function UpdateRecord(ByVal WarrantyClaim As ClaimBE) As Boolean


        Dim objCon As New SqlConnection(SQLHelper.CON_STR)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As SqlTransaction = objCon.BeginTransaction
        Try

            '-------------------------- Warranty Claim Document ----------------------
            Dim strSQL As String = String.Empty
            'strSQL = "UPDATE WarrantyClaimMasterTable  SET  DocNo=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "', DocDate=Convert(DateTime, N'" & WarrantyClaim.DocDate & "',102), CustomerCode=" & WarrantyClaim.CustomerCode & ", ProjectId=" & WarrantyClaim.ProjectId & ", LocationId=" & WarrantyClaim.LocationId & ", Post=" & IIf(WarrantyClaim.Post = True, 1, 0) & ", Remarks=N'" & WarrantyClaim.Remarks.Replace("'", "''") & "', TotalQty=" & WarrantyClaim.Adjustment & ", TotalAmount=" & WarrantyClaim.TotalAmount & ", Adjustment=" & WarrantyClaim.Adjustment & ", UserId=" & WarrantyClaim.UserId & ", EntryDate=Convert(DateTime, N'" & WarrantyClaim.EntryDate & "',102) WHERE DocId=" & WarrantyClaim.DocId & ""
            'SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = "UPDATE WarrantyClaimMasterTable  SET  DocNo=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "', DocDate=Convert(DateTime, N'" & WarrantyClaim.DocDate & "',102), CustomerCode=" & WarrantyClaim.CustomerCode & ", ProjectId=" & WarrantyClaim.ProjectId & ", LocationId=" & WarrantyClaim.LocationId & ", Post=" & IIf(WarrantyClaim.Post = True, 1, 0) & ", Remarks=N'" & WarrantyClaim.Remarks.Replace("'", "''") & "', TotalQty=" & WarrantyClaim.Adjustment & ", TotalAmount=" & WarrantyClaim.TotalAmount & ", Adjustment=" & WarrantyClaim.Adjustment & ", UserId=" & WarrantyClaim.UserId & ", EntryDate=Convert(DateTime, N'" & WarrantyClaim.EntryDate & "',102), DeliveryID=" & WarrantyClaim.DeliveryID & " WHERE DocId=" & WarrantyClaim.DocId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From WarrantyClaimDetailTable WHERE DocId=" & WarrantyClaim.DocId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            '--------------------------- Stock Document ------------------------------
            strSQL = String.Empty
            strSQL = "UPDATE StockMasterTable SET DocNo=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "',DocDate=Convert(DateTime, N'" & WarrantyClaim.DocDate & "',102),DocType=8,Remarks=N'" & WarrantyClaim.Remarks.Replace("'", "''") & "',Project=" & WarrantyClaim.ProjectId & ", Account_Id=" & WarrantyClaim.CustomerCode & " WHERE DocNo=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From StockDetailTable WHERE StockTransId IN(Select StockTransId From StockMasterTable WHERE DocNo=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            '-------------------------- Voucher Document -----------------------------
            strSQL = String.Empty
            strSQL = "UPDATE tblVoucher  SET Voucher_No=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "', Voucher_Code=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "', Voucher_Type_Id=1, Voucher_Date=Convert(DateTime, N'" & WarrantyClaim.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Post=" & IIf(WarrantyClaim.Post = True, 1, 0) & " WHERE Voucher_Id IN (Select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From tblVoucherDetail WHERE Voucher_Id IN(Select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            '------------------------ Get Ids ----------------------------------------


            strSQL = String.Empty
            strSQL = "Select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "'"
            Dim objVid As Object = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)
            WarrantyClaim.VoucherId = objVid

            strSQL = "Select StockTransId From StockMasterTable WHERE DocNo=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "'"
            Dim objStockTranId As Object = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)
            WarrantyClaim.StockTransId = objStockTranId

            '------------------------ Detail Record ----------------------------------

            SaveDetailRecord(WarrantyClaim, objTrans)

            objTrans.Commit()
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Function DeleteRecord(ByVal WarrantyClaim As ClaimBE) As Boolean


        Dim objCon As New SqlConnection(SQLHelper.CON_STR)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As SqlTransaction = objCon.BeginTransaction
        Try

            '-------------------------- Warranty Claim Document ----------------------
            Dim strSQL As String = String.Empty

            strSQL = String.Empty
            strSQL = "Delete From WarrantyClaimDetailTable WHERE DocId=" & WarrantyClaim.DocId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From WarrantyClaimMasterTable WHERE DocId=" & WarrantyClaim.DocId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            '--------------------------- Stock Document ------------------------------

            strSQL = String.Empty
            strSQL = "Delete From StockDetailTable WHERE StockTransId IN(Select StockTransId From StockMasterTable WHERE DocNo=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From StockMasterTable WHERE DocNo=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            '-------------------------- Voucher Document -----------------------------

            strSQL = String.Empty
            strSQL = "Delete From tblVoucherDetail WHERE Voucher_Id IN(Select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From tblVoucher WHERE Voucher_No=N'" & WarrantyClaim.DocNo.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            objTrans.Commit()
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Function SaveDetailRecord(ByVal WarrantyClaim As ClaimBE, ByVal objTrans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            If WarrantyClaim.WarrantyClaimDetail.Count > 0 Then
                For Each WarrantyClaimDt As WarrantyClaimDetailBE In WarrantyClaim.WarrantyClaimDetail

                    '----------------------- Warranty Claim Detail Record -----------------------------
                    strSQL = String.Empty
                    strSQL = "INSERT INTO WarrantyClaimDetailTable(DocId,LocationId,ArticleDefId,ArticleSize,Sz1,Sz2,Sz3,Sz4,Sz5,Sz6,Sz7,Qty,Price,Current_Price,Tax_Percent,Comments,WarrantyAble, Engine_No, Chassis_No, AccountId, PackDesc) " _
                    & " VALUES(" & WarrantyClaim.DocId & ", " & WarrantyClaimDt.LocationId & ", " & WarrantyClaimDt.ArticleDefId & ", N'" & WarrantyClaimDt.ArticleSize.Replace("'", "''") & "', " & WarrantyClaimDt.Sz1 & ", " & WarrantyClaimDt.Sz2 & ", " & WarrantyClaimDt.Sz3 & ", " & WarrantyClaimDt.Sz4 & ", " & WarrantyClaimDt.Sz5 & ", " & WarrantyClaimDt.Sz6 & ", " & WarrantyClaimDt.Sz7 & ", " & WarrantyClaimDt.Qty & ", " & WarrantyClaimDt.Price & ", " & WarrantyClaimDt.Current_Price & ", " & WarrantyClaimDt.Tax_Percent & ", N'" & WarrantyClaimDt.Comments.Replace("'", "''") & "'," & IIf(WarrantyClaimDt.WarrantyAble = True, 1, 0) & ", N'" & WarrantyClaimDt.Engine_No.Replace("'", "''") & "',N'" & WarrantyClaimDt.Chassis_No.Replace("'", "''") & "'," & WarrantyClaimDt.PurchaseAccountId & ", N'" & WarrantyClaimDt.PackDesc.Replace("'", "''") & "')"
                    SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

                    '----------------------- Stock Detail Record ----------------------------------------
                    strSQL = String.Empty
                    'strSQL = "INSERT INTO StockDetailTable(StockTransId, LocationId, ArticleDefId,OutQty,InQty,Rate,OutAmount,InAmount,Remarks,Engine_No,Chassis_No) " _
                    '& " VALUES(" & WarrantyClaim.StockTransId & ", " & WarrantyClaimDt.LocationId & ", " & WarrantyClaimDt.ArticleDefId & ", " & WarrantyClaimDt.Qty & ",0," & WarrantyClaimDt.Price & ", " & (Val(WarrantyClaimDt.Qty) * Val(WarrantyClaimDt.Price)) & ",0,N'" & WarrantyClaimDt.Comments.Replace("'", "''") & "', N'" & WarrantyClaimDt.Engine_No.Replace("'", "''") & "', N'" & WarrantyClaimDt.Chassis_No.Replace("'", "''") & "')"
                    strSQL = "INSERT INTO StockDetailTable(StockTransId, LocationId, ArticleDefId,OutQty,InQty,Rate,OutAmount,InAmount,Remarks,Engine_No,Chassis_No,Cost_Price,In_PackQty,Out_PackQty,Pack_Qty) " _
                    & " VALUES(" & WarrantyClaim.StockTransId & ", " & WarrantyClaimDt.LocationId & ", " & WarrantyClaimDt.ArticleDefId & ", " & WarrantyClaimDt.Qty & ",0," & WarrantyClaimDt.Price & ", " & (Val(WarrantyClaimDt.Qty) * Val(WarrantyClaimDt.Price)) & ",0,N'" & WarrantyClaimDt.Comments.Replace("'", "''") & "', N'" & WarrantyClaimDt.Engine_No.Replace("'", "''") & "', N'" & WarrantyClaimDt.Chassis_No.Replace("'", "''") & "','','',N'" & WarrantyClaimDt.Sz1 & "',N'" & WarrantyClaimDt.Sz7 & "')"
                    SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

                    '--------------------------- Voucher Detail Record --------------------------



                    If WarrantyClaimDt.WarrantyAble = True Then

                        'Purchase Voucher
                        strSQL = String.Empty
                        Dim dblTotalPurchaseAmount As Double = 0D
                        dblTotalPurchaseAmount = (Val(WarrantyClaimDt.Qty) * Val(WarrantyClaimDt.Price))
                        strSQL = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id, coa_detail_id, Comments, debit_amount, credit_amount,ArticleDefId) " _
                        & " VALUES(" & WarrantyClaim.VoucherId & ", " & WarrantyClaimDt.LocationId & ", " & WarrantyClaimDt.PurchaseAccountId & ", 'Stock Voucher [" & WarrantyClaim.DocNo & "] " & WarrantyClaimDt.ArticleDescription & " (" & WarrantyClaimDt.Qty & " X " & WarrantyClaimDt.Price & ")', 0, " & dblTotalPurchaseAmount & ", " & WarrantyClaimDt.ArticleDefId & ")"
                        SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)


                        'Other Loss Voucher
                        strSQL = String.Empty
                        Dim dblTotalCGSAmount As Double = 0D
                        dblTotalCGSAmount = (Val(WarrantyClaimDt.Qty) * Val(WarrantyClaimDt.Price))
                        strSQL = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id, coa_detail_id, Comments, debit_amount, credit_amount,ArticleDefId) " _
                        & " VALUES(" & WarrantyClaim.VoucherId & ", " & WarrantyClaimDt.LocationId & ", " & WarrantyClaim.OtherLossAccountId & ", 'Cost Of Goods Voucher [" & WarrantyClaim.DocNo & "] " & WarrantyClaimDt.ArticleDescription & " (" & WarrantyClaimDt.Qty & " X " & WarrantyClaimDt.Price & ")', " & dblTotalCGSAmount & ",0, " & WarrantyClaimDt.ArticleDefId & ")"
                        SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

                    End If

                    If WarrantyClaimDt.WarrantyAble = False Then

                        'Purchase Voucher
                        strSQL = String.Empty
                        Dim dblTotalPurchaseAmount As Double = 0D
                        dblTotalPurchaseAmount = (Val(WarrantyClaimDt.Qty) * Val(WarrantyClaimDt.Price))
                        strSQL = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id, coa_detail_id, Comments, debit_amount, credit_amount,ArticleDefId) " _
                        & " VALUES(" & WarrantyClaim.VoucherId & ", " & WarrantyClaimDt.LocationId & ", " & WarrantyClaimDt.PurchaseAccountId & ", 'Stock Voucher [" & WarrantyClaim.DocNo & "] " & WarrantyClaimDt.ArticleDescription & " (" & WarrantyClaimDt.Qty & " X " & WarrantyClaimDt.Price & ")', 0, " & dblTotalPurchaseAmount & ", " & WarrantyClaimDt.ArticleDefId & ")"
                        SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

                        ''Other Loss Voucher
                        'strSQL = String.Empty
                        'Dim dblTotalOthersLossAmount As Double = 0D
                        'dblTotalOthersLossAmount = (Val(WarrantyClaimDt.Qty) * Val(WarrantyClaimDt.Price))
                        'strSQL = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id, coa_detail_id, Comments, debit_amount, credit_amount,ArticleDefId) " _
                        '& " VALUES(" & WarrantyClaim.VoucherId & ", " & WarrantyClaimDt.LocationId & ", " & WarrantyClaim.OtherLossAccountId & ", 'Other Loss Voucher [" & WarrantyClaim.DocNo & "] " & WarrantyClaimDt.ArticleDescription & " (" & WarrantyClaimDt.Qty & " X " & WarrantyClaimDt.Price & ")', " & dblTotalOthersLossAmount & ",0, " & WarrantyClaimDt.ArticleDefId & ")"
                        'SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

                        'Customer Voucher
                        strSQL = String.Empty
                        Dim dblTotalCustomerAmount As Double = 0D
                        dblTotalCustomerAmount = (Val(WarrantyClaimDt.Qty) * Val(WarrantyClaimDt.Price))
                        strSQL = "INSERT INTO tblVoucherDetail(Voucher_Id, Location_Id, coa_detail_id, Comments, debit_amount, credit_amount,ArticleDefId) " _
                        & " VALUES(" & WarrantyClaim.VoucherId & ", " & WarrantyClaimDt.LocationId & ", " & WarrantyClaim.CustomerCode & ", 'Other Loss Voucher [" & WarrantyClaim.DocNo & "] " & WarrantyClaimDt.ArticleDescription & " (" & WarrantyClaimDt.Qty & " X " & WarrantyClaimDt.Price & ")', " & dblTotalCustomerAmount & ", 0, " & WarrantyClaimDt.ArticleDefId & ")"
                        SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
                    End If

                Next
            End If
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function

    Public Function GetAllRecord(ByVal CompanyId As Integer, Optional ByVal Condition As String = "") As DataTable
        Try
            Dim objDt As New DataTable
            'objDt = UtilityDAL.GetDataTable("SELECT  " & IIf(Condition = "All", "", "TOP 50") & " dbo.WarrantyClaimMasterTable.DocId, dbo.WarrantyClaimMasterTable.DocNo, dbo.WarrantyClaimMasterTable.DocDate, dbo.WarrantyClaimMasterTable.CustomerCode, dbo.WarrantyClaimMasterTable.ProjectId,  " _
            '   & " dbo.CompanyDefTable.CompanyName as Company, dbo.WarrantyClaimMasterTable.LocationId, dbo.tblDefCostCenter.Name, dbo.WarrantyClaimMasterTable.Post, " _
            '   & " dbo.WarrantyClaimMasterTable.Remarks, dbo.WarrantyClaimMasterTable.Adjustment  " _
            '   & " FROM dbo.WarrantyClaimMasterTable LEFT OUTER JOIN " _
            '   & " dbo.CompanyDefTable ON dbo.WarrantyClaimMasterTable.LocationId = dbo.CompanyDefTable.CompanyId LEFT OUTER JOIN " _
            '   & " dbo.tblDefCostCenter ON dbo.WarrantyClaimMasterTable.ProjectId = dbo.tblDefCostCenter.CostCenterID " _
            '   & " WHERE dbo.WarrantyClaimMasterTable.LocationId=" & CompanyId & " " _
            '   & " ORDER BY dbo.WarrantyClaimMasterTable.DocId DESC ")
            ''Task# 2-11-06-2015 Modify Query , add Customer Name  
            'objDt = UtilityDAL.GetDataTable("SELECT  " & IIf(Condition = "All", "", "TOP 50") & " dbo.WarrantyClaimMasterTable.DocId, dbo.WarrantyClaimMasterTable.DocNo, dbo.WarrantyClaimMasterTable.DocDate, dbo.WarrantyClaimMasterTable.CustomerCode, dbo.WarrantyClaimMasterTable.ProjectId,dbo.CompanyDefTable.CompanyName as Company,  " _
            '& " dbo.vwcoaDetail.detail_title as Customer, dbo.WarrantyClaimMasterTable.LocationId, dbo.tblDefCostCenter.Name as CostCenter, dbo.WarrantyClaimMasterTable.Post, " _
            '& " dbo.WarrantyClaimMasterTable.Remarks, dbo.WarrantyClaimMasterTable.Adjustment  " _
            '& " FROM dbo.WarrantyClaimMasterTable LEFT OUTER JOIN " _
            '& " dbo.CompanyDefTable ON dbo.WarrantyClaimMasterTable.LocationId = dbo.CompanyDefTable.CompanyId LEFT OUTER JOIN " _
            '& " dbo.vwcoaDetail ON dbo.WarrantyClaimMasterTable.CustomerCode = dbo.vwcoaDetail.coa_Detail_ID LEFT OUTER JOIN " _
            '& " dbo.tblDefCostCenter ON dbo.WarrantyClaimMasterTable.ProjectId = dbo.tblDefCostCenter.CostCenterID " _
            '& " WHERE dbo.WarrantyClaimMasterTable.DocNo <> '' " & IIf(CompanyId = 0, "", " AND dbo.WarrantyClaimMasterTable.LocationId=" & CompanyId & "") & " " _
            '& " ORDER BY dbo.WarrantyClaimMasterTable.DocId DESC ")
            ''End Task# 2-11-06-2015


            objDt = UtilityDAL.GetDataTable("SELECT  " & IIf(Condition = "All", "", "TOP 50") & " dbo.WarrantyClaimMasterTable.DocId, dbo.WarrantyClaimMasterTable.DocNo, dbo.WarrantyClaimMasterTable.DocDate, dbo.WarrantyClaimMasterTable.CustomerCode, dbo.WarrantyClaimMasterTable.ProjectId,dbo.CompanyDefTable.CompanyName as Company,  " _
      & " dbo.vwcoaDetail.detail_title as Customer, dbo.WarrantyClaimMasterTable.LocationId, dbo.tblDefCostCenter.Name as CostCenter, dbo.WarrantyClaimMasterTable.Post, " _
      & " dbo.WarrantyClaimMasterTable.Remarks, dbo.WarrantyClaimMasterTable.Adjustment,IsNull(WarrantyClaimMasterTable.DeliveryID,0) as DeliveryID, DeliveryChalanMasterTable.DeliveryNo, DeliveryChalanMasterTable.DeliveryDate  " _
      & " FROM dbo.WarrantyClaimMasterTable LEFT OUTER JOIN DeliveryChalanMasterTable on DeliveryChalanMasterTable.DeliveryID = WarrantyClaimMasterTable.DeliveryID LEFT OUTER JOIN " _
      & " dbo.CompanyDefTable ON dbo.WarrantyClaimMasterTable.LocationId = dbo.CompanyDefTable.CompanyId LEFT OUTER JOIN " _
      & " dbo.vwcoaDetail ON dbo.WarrantyClaimMasterTable.CustomerCode = dbo.vwcoaDetail.coa_Detail_ID LEFT OUTER JOIN " _
      & " dbo.tblDefCostCenter ON dbo.WarrantyClaimMasterTable.ProjectId = dbo.tblDefCostCenter.CostCenterID  " _
      & " WHERE dbo.WarrantyClaimMasterTable.DocNo <> '' " & IIf(CompanyId = 0, "", " AND dbo.WarrantyClaimMasterTable.LocationId=" & CompanyId & "") & " " _
      & " ORDER BY dbo.WarrantyClaimMasterTable.DocId DESC ")

            Return objDt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDetailRecord(ByVal DocId As Integer) As DataTable
        Try
            Dim objDt As New DataTable
            objDt = UtilityDAL.GetDataTable("SELECT dbo.WarrantyClaimDetailTable.LocationId, dbo.WarrantyClaimDetailTable.ArticleDefId, dbo.ArticleDefView.ArticleDescription as Item,dbo.ArticleDefView.ArticleCode as Code, dbo.ArticleDefView.ArticleSizeName as Size,  " _
                     & " dbo.ArticleDefView.ArticleColorName as Color, dbo.WarrantyClaimDetailTable.ArticleSize as Unit, IsNull(dbo.WarrantyClaimDetailTable.Sz7,0) as PackQty,IsNull(dbo.WarrantyClaimDetailTable.Sz1,0) as Qty, " _
                     & " IsNull(dbo.WarrantyClaimDetailTable.Price,0) as Price, Convert(float,0) as Total, Isnull(dbo.WarrantyClaimDetailTable.Current_Price,0) Current_Price, Isnull(dbo.WarrantyClaimDetailTable.Tax_Percent,0) as Tax_Percent,  " _
                     & " dbo.WarrantyClaimDetailTable.Comments,dbo.WarrantyClaimDetailTable.Engine_No,  " _
                     & " dbo.WarrantyClaimDetailTable.Chassis_No,dbo.WarrantyClaimDetailTable.WarrantyAble,Isnull(dbo.WarrantyClaimDetailTable.AccountId,0) as AccountId,dbo.WarrantyClaimDetailTable.PackDesc " _
                     & " FROM dbo.WarrantyClaimDetailTable INNER JOIN " _
                     & " dbo.ArticleDefView ON dbo.WarrantyClaimDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId " _
                     & " WHERE(dbo.WarrantyClaimDetailTable.DocId = " & DocId & ")")
            Return objDt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSerialNo(ByVal Prefix As String, Optional ByVal objTrans As SqlTransaction = Nothing) As String
        Try
            Dim SerialNo As String = String.Empty
            Dim Serial As Integer = 0I

            Dim objDt As New DataTable
            objDt = UtilityDAL.GetDataTable("SELECT IsNull(Max(Right(DocNo,5)),0)+1 as SerialNo From WarrantyClaimMasterTable WHERE LEFT(DocNo," & Prefix.Length & ")=N'" & Prefix & "'", objTrans)
            If objDt IsNot Nothing Then
                If objDt.Rows.Count > 0 Then
                    Serial = Val(objDt.Rows(0).Item(0).ToString)
                Else
                    Serial = 1
                End If
            Else
                Serial = 1
            End If
            SerialNo = Prefix & Right("00000" + CStr(Serial), 5)
            Return SerialNo
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

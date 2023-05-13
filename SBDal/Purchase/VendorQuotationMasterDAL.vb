Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net
Public Class VendorQuotationMasterDAL
    Dim NotificationDAL As New NotificationTemplatesDAL
    Dim ExpenseDAL As New InwardExpenseDetailDAL
    Public VendorQuotationDetailId As Integer = 0
    'VendorQuotationId	int	Unchecked
    'VendorQuotationNo	nvarchar(100)	Checked
    'VendorQuotationDate	datetime	Checked
    'VendorQuotationExpiryDate	datetime	Checked
    'VendorId	int	Checked
    'PurchaseInquiryId	int	Checked
    'ReferenceNo	nvarchar(100)	Checked
    'Remarks	nvarchar(500)	Checked

    Public Function Add(ByVal objMod As VendorQuotationMaster, ByVal Source As String, ByVal objPath As String, ByVal arrFile As List(Of String)) As Integer
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO VendorQuotationMaster(VendorQuotationNo, VendorQuotationDate, VendorQuotationExpiryDate, VendorId, PurchaseInquiryId, ReferenceNo, Remarks, UserName, Amount, NetTotal, Discount) " _
            & " VALUES('" & objMod.VendorQuotationNo.Replace("'", "''") & "', '" & objMod.VendorQuotationDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', '" & objMod.VendorQuotationExpiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', " & objMod.VendorId & ", " & objMod.PurchaseInquiryId & ", '" & objMod.ReferenceNo.Replace("'", "''") & "', '" & objMod.Remarks.Replace("'", "''") & "', '" & objMod.UserName.Replace("'", "''") & "'," & objMod.Amount & "," & objMod.NetTotal & "," & objMod.Discount & ") Select @@Identity"
            Dim VendorQuotationId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            SaveDocument(VendorQuotationId, Source, trans, objPath, arrFile, objMod.VendorQuotationDate, objMod.PurchaseInquiryId)
            objMod.VendorQuotationId = VendorQuotationId
            Dim DetailDAL As New VendorQuotationDetailDAL()
            DetailDAL.AddSingle(objMod, trans)
            trans.Commit()
            Dim ValueTable As DataTable = GetSingle(VendorQuotationId)
            NotificationDAL.SaveAndSendNotification("Vendor Quotation", "VendorQuotationMaster", VendorQuotationId, ValueTable, "Purchase > Vendor Quotation")
            Return VendorQuotationId
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddUpdateDetail(ByVal objMod As VendorQuotationMaster, ByVal VendorQuotationNo As String) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO VendorQuotationMaster(VendorQuotationNo, VendorQuotationDate, VendorQuotationExpiryDate, VendorId, PurchaseInquiryId, ReferenceNo, Remarks, UserName, Amount, NetTotal, Discount) " _
            & " VALUES('" & objMod.VendorQuotationNo.Replace("'", "''") & "', '" & objMod.VendorQuotationDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', '" & objMod.VendorQuotationExpiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', " & objMod.VendorId & ", " & objMod.PurchaseInquiryId & ", '" & objMod.ReferenceNo.Replace("'", "''") & "', '" & objMod.Remarks.Replace("'", "''") & "' , '" & objMod.UserName.Replace("'", "''") & "'," & objMod.Amount & "," & objMod.NetTotal & "," & objMod.Discount & ") Select @@Identity"
            Dim VendorQuotationId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objMod.VendorQuotationId = VendorQuotationId
            Dim DetailDAL As New VendorQuotationDetailDAL()
            DetailDAL.Add(objMod, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal objMod As VendorQuotationMaster) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Update VendorQuotationMaster SET VendorQuotationNo= '" & objMod.VendorQuotationNo.Replace("'", "''") & "', VendorQuotationDate='" & objMod.VendorQuotationDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', VendorQuotationExpiryDate='" & objMod.VendorQuotationExpiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', VendorId=" & objMod.VendorId & ", PurchaseInquiryId = " & objMod.PurchaseInquiryId & ", ReferenceNo='" & objMod.ReferenceNo.Replace("'", "''") & "', Remarks='" & objMod.Remarks.Replace("'", "''") & "' , UserName='" & objMod.UserName.Replace("'", "''") & "', Amount = " & objMod.Amount & ", NetTotal = " & objMod.NetTotal & ", Discount = " & objMod.Discount & " WHERE VendorQuotationId=" & objMod.VendorQuotationId & " Select @@Identity"
            Dim VendorQuotationId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Dim DetailDAL As New VendorQuotationDetailDAL()
            'DetailDAL.DeleteSIIWise(objMod.DetailList, objMod.VendorQuotationId, trans)
            DetailDAL.Add(objMod, trans)
            'DetailDAL.AddUpdate(objMod, trans)
            'VendorsDAL.DeleteSIIWise(objMod.PurchaseInquiryId, trans)
            'VendorsDAL.Add(objMod, trans)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()
        End Try

    End Function
    Public Function Update1(ByVal objMod As VendorQuotationMaster, ByVal Source As String, ByVal objPath As String, ByVal arrFile As List(Of String)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Update VendorQuotationMaster SET VendorQuotationNo= '" & objMod.VendorQuotationNo.Replace("'", "''") & "', VendorQuotationDate='" & objMod.VendorQuotationDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', VendorQuotationExpiryDate='" & objMod.VendorQuotationExpiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', VendorId=" & objMod.VendorId & ", PurchaseInquiryId = " & objMod.PurchaseInquiryId & ", ReferenceNo='" & objMod.ReferenceNo.Replace("'", "''") & "', Remarks='" & objMod.Remarks.Replace("'", "''") & "' , UserName='" & objMod.UserName.Replace("'", "''") & "', Amount = " & objMod.Amount & ", NetTotal = " & objMod.NetTotal & ", Discount = " & objMod.Discount & " WHERE VendorQuotationId=" & objMod.VendorQuotationId & " Select @@Identity"
            Dim VendorQuotationId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            SaveDocument(objMod.VendorQuotationId, Source, trans, objPath, arrFile, objMod.VendorQuotationDate, objMod.PurchaseInquiryId)
            Dim DetailDAL As New VendorQuotationDetailDAL()
            'DetailDAL.DeleteSIIWise(objMod.DetailList, objMod.VendorQuotationId, trans)
            DetailDAL.AddSingle(objMod, trans)
            'DetailDAL.AddUpdate(objMod, trans)
            'VendorsDAL.DeleteSIIWise(objMod.PurchaseInquiryId, trans)
            'VendorsDAL.Add(objMod, trans)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()
        End Try

    End Function


    Public Function Delete(ByVal obj As VendorQuotationMaster) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try

            Dim strSQL As String = String.Empty
            strSQL = "Delete From VendorQuotationMaster WHERE VendorQuotationId=" & obj.VendorQuotationId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Dim DetailDAL As New VendorQuotationDetailDAL()
            'Dim VendorsDAL As New PurchaseInquiryVendorsDAL()
            'For Each objMod As PurchaseInquiryDetail In obj.DetailList
            '    If objMod.SalesInquiryDetailId > 0 Then
            '        DetailDAL.UpdateSalesInquiryStatusAgainstSubtraction(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans)
            '    End If
            'Next
            DetailDAL.DeleteSIIWise(obj.DetailList, obj.VendorQuotationId, trans)
            'VendorsDAL.DeleteSIIWise(obj.PurchaseInquiryId, trans)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()
        End Try

    End Function
    Public Function GetAllRecords(Optional ByVal Source As String = "", Optional ByVal UserName As String = "") As DataTable
        Try
            'VendorQuotationId	int	Unchecked
            'VendorQuotationNo	nvarchar(100)	Checked
            'VendorQuotationDate	datetime	Checked
            'VendorQuotationExpiryDate	datetime	Checked
            'VendorId	int	Checked
            'PurchaseInquiryId	int	Checked
            'ReferenceNo	nvarchar(100)	Checked
            'Remarks	nvarchar(500)	Checked
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT VendorQuotation.VendorQuotationId, VendorQuotation.VendorQuotationNo, VendorQuotation.VendorQuotationDate, VendorQuotation.VendorQuotationExpiryDate, IsNull(COA.coa_detail_id,0) as VendorId, COA.detail_code As Code, COA.detail_title As Vendor, VendorQuotation.PurchaseInquiryId, PurchaseInquiryMaster.PurchaseInquiryNo, VendorQuotation.ReferenceNo, VendorQuotation.Remarks,  VendorQuotation.UserName, VendorQuotation.Amount , VendorQuotation.Discount , VendorQuotation.NetTotal , Doc_Att.NoOfAttachment As [No Of Attachments] FROM dbo.VendorQuotationMaster AS VendorQuotation LEFT OUTER JOIN  dbo.vwCOADetail AS COA ON VendorQuotation.VendorId = COA.coa_detail_id LEFT JOIN PurchaseInquiryMaster ON VendorQuotation.PurchaseInquiryId = PurchaseInquiryMaster.PurchaseInquiryId LEFT OUTER JOIN (Select Count(*) as NoOfAttachment, DocId From DocumentAttachment WHERE (source =N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = VendorQuotation.VendorQuotationId " & IIf(UserName = "", "", " Where VendorQuotation.UserName = '" & UserName & "'") & " Order by VendorQuotation.VendorQuotationDate DESC")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTop50(Optional ByVal Source As String = "", Optional ByVal UserName As String = "") As DataTable
        Try
            'VendorQuotationId	int	Unchecked
            'VendorQuotationNo	nvarchar(100)	Checked
            'VendorQuotationDate	datetime	Checked
            'VendorQuotationExpiryDate	datetime	Checked
            'VendorId	int	Checked
            'PurchaseInquiryId	int	Checked
            'ReferenceNo	nvarchar(100)	Checked
            'Remarks	nvarchar(500)	Checked
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT Distinct Top 50 VendorQuotation.VendorQuotationId, VendorQuotation.VendorQuotationNo, VendorQuotation.VendorQuotationDate, VendorQuotation.VendorQuotationExpiryDate, IsNull(COA.coa_detail_id,0) as VendorId, COA.detail_code As Code, COA.detail_title As Vendor, VendorQuotation.PurchaseInquiryId, PurchaseInquiryMaster.PurchaseInquiryNo, VendorQuotation.ReferenceNo, VendorQuotation.Remarks,  VendorQuotation.UserName, VendorQuotation.Amount , VendorQuotation.Discount , VendorQuotation.NetTotal , Doc_Att.NoOfAttachment As [No Of Attachments] FROM dbo.VendorQuotationMaster AS VendorQuotation LEFT OUTER JOIN  dbo.vwCOADetail AS COA ON VendorQuotation.VendorId = COA.coa_detail_id LEFT JOIN PurchaseInquiryMaster ON VendorQuotation.PurchaseInquiryId = PurchaseInquiryMaster.PurchaseInquiryId LEFT OUTER JOIN (Select Count(*) as NoOfAttachment, DocId From DocumentAttachment WHERE (source =N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = VendorQuotation.VendorQuotationId " & IIf(UserName = "", "", " Where VendorQuotation.UserName = '" & UserName & "'") & " Order by VendorQuotation.VendorQuotationDate DESC")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAll(Optional ByVal Source As String = "", Optional ByVal UserName As String = "") As DataTable
        Try
            'VendorQuotationId	int	Unchecked
            'VendorQuotationNo	nvarchar(100)	Checked
            'VendorQuotationDate	datetime	Checked
            'VendorQuotationExpiryDate	datetime	Checked
            'VendorId	int	Checked
            'PurchaseInquiryId	int	Checked
            'ReferenceNo	nvarchar(100)	Checked
            'Remarks	nvarchar(500)	Checked
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT Distinct VendorQuotation.VendorQuotationId, VendorQuotation.VendorQuotationNo, VendorQuotation.VendorQuotationDate, VendorQuotation.VendorQuotationExpiryDate, IsNull(COA.coa_detail_id,0) as VendorId, COA.detail_code As Code, COA.detail_title As Vendor, VendorQuotation.PurchaseInquiryId, PurchaseInquiryMaster.PurchaseInquiryNo, VendorQuotation.ReferenceNo, VendorQuotation.Remarks,  VendorQuotation.UserName, VendorQuotation.Amount , VendorQuotation.Discount , VendorQuotation.NetTotal , Doc_Att.NoOfAttachment As [No Of Attachments] FROM dbo.VendorQuotationMaster AS VendorQuotation LEFT OUTER JOIN  dbo.vwCOADetail AS COA ON VendorQuotation.VendorId = COA.coa_detail_id LEFT JOIN PurchaseInquiryMaster ON VendorQuotation.PurchaseInquiryId = PurchaseInquiryMaster.PurchaseInquiryId LEFT OUTER JOIN (Select Count(*) as NoOfAttachment, DocId From DocumentAttachment WHERE (source =N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = VendorQuotation.VendorQuotationId " & IIf(UserName = "", "", " Where VendorQuotation.UserName = '" & UserName & "'") & " Order by VendorQuotation.VendorQuotationDate DESC")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSingle(ByVal VendorQuotationId As Integer) As DataTable
        Try
            'VendorQuotationId	int	Unchecked
            'VendorQuotationNo	nvarchar(100)	Checked
            'VendorQuotationDate	datetime	Checked
            'VendorQuotationExpiryDate	datetime	Checked
            'VendorId	int	Checked
            'PurchaseInquiryId	int	Checked
            'ReferenceNo	nvarchar(100)	Checked
            'Remarks	nvarchar(500)	Checked
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT VendorQuotation.VendorQuotationId, VendorQuotation.VendorQuotationNo, VendorQuotation.VendorQuotationDate, VendorQuotation.VendorQuotationExpiryDate, IsNull(COA.coa_detail_id,0) as VendorId, COA.detail_code As Code, COA.detail_title As Vendor, VendorQuotation.PurchaseInquiryId, PurchaseInquiryMaster.PurchaseInquiryNo, VendorQuotation.ReferenceNo, VendorQuotation.Remarks,  VendorQuotation.UserName , VendorQuotation.Amount , VendorQuotation.Discount , VendorQuotation.NetTotal  FROM dbo.VendorQuotationMaster AS VendorQuotation LEFT OUTER JOIN  dbo.vwCOADetail AS COA ON VendorQuotation.VendorId = COA.coa_detail_id LEFT JOIN PurchaseInquiryMaster ON VendorQuotation.PurchaseInquiryId = PurchaseInquiryMaster.PurchaseInquiryId Where VendorQuotation.VendorQuotationId = " & VendorQuotationId & "")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Validate(ByVal VendorQuotationNo As String) As Integer
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT VendorQuotation.VendorQuotationId FROM dbo.VendorQuotationMaster AS VendorQuotation Where VendorQuotation.VendorQuotationNo ='" & VendorQuotationNo & "'")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows(0).Item(0).ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsDetailExists(ByVal VendorQuotationNo As String) As Boolean
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT VendorQuotation.VendorQuotationId FROM dbo.VendorQuotationMaster AS VendorQuotation Where VendorQuotation.VendorQuotationNo ='" & VendorQuotationNo & "' And VendorQuotation.VendorQuotationId In (Select VendorQuotationId FROM VendorQuotationDetail)")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return True ''Val(dt.Rows(0).Item(0).ToString)
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, ByVal objTrans As SqlTransaction, ByVal objPath As String, ByVal arrFile As List(Of String), ByVal docDate As DateTime, ByVal PurchaseInquiryId As Integer) As Boolean
        'Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'If Con.State = ConnectionState.Closed Then Con.Open()
        Try

            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()


            Dim objdt As New DataTable
            objdt = UtilityDAL.GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)

            Dim strSQL As String = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
            'cmd.ExecuteNonQuery()

            'Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString

            If arrFile.Count > 0 Then
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        Dim New_Files As String = intId & "_" & DocId & "_VQ" & PurchaseInquiryId & "_" & docDate.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If


            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
                        SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
                    Next
                End If
            End If
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function

    Public Function ForSingle(ByVal objMod As VendorQuotationMaster, ByVal Source As String, ByVal objPath As String, ByVal arrFile As List(Of String), ByVal ExpenseDetail As List(Of InwardExpenseDetailBE)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "If Exists(Select VendorQuotationId From VendorQuotationMaster Where VendorQuotationNo='" & objMod.VendorQuotationNo & "') Update VendorQuotationMaster SET VendorQuotationNo= '" & objMod.VendorQuotationNo.Replace("'", "''") & "', VendorQuotationDate='" & objMod.VendorQuotationDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', VendorQuotationExpiryDate='" & objMod.VendorQuotationExpiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', VendorId=" & objMod.VendorId & ", PurchaseInquiryId = " & objMod.PurchaseInquiryId & ", ReferenceNo='" & objMod.ReferenceNo.Replace("'", "''") & "', Remarks='" & objMod.Remarks.Replace("'", "''") & "' , UserName='" & objMod.UserName.Replace("'", "''") & "', Amount = " & objMod.Amount & ", NetTotal = " & objMod.NetTotal & ", Discount = " & objMod.Discount & " WHERE VendorQuotationId=" & objMod.VendorQuotationId & " " _
                & "  Else INSERT INTO VendorQuotationMaster(VendorQuotationNo, VendorQuotationDate, VendorQuotationExpiryDate, VendorId, PurchaseInquiryId, ReferenceNo, Remarks, UserName, Amount, NetTotal, Discount) " _
                & "  VALUES('" & objMod.VendorQuotationNo.Replace("'", "''") & "', '" & objMod.VendorQuotationDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', '" & objMod.VendorQuotationExpiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', " & objMod.VendorId & ", " & objMod.PurchaseInquiryId & ", '" & objMod.ReferenceNo.Replace("'", "''") & "', '" & objMod.Remarks.Replace("'", "''") & "', '" & objMod.UserName.Replace("'", "''") & "'," & objMod.Amount & "," & objMod.NetTotal & "," & objMod.Discount & ") Select @@Identity"
            Dim VendorQuotationId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            If VendorQuotationId > 0 Then
                objMod.VendorQuotationId = VendorQuotationId
            End If
            ''Start TFS3109
            If ExpenseDAL.ValidateIfExist(objMod.VendorQuotationId) Then
                ExpenseDAL.Delete(objMod.VendorQuotationId)
                ExpenseDAL.Add(ExpenseDetail, objMod.VendorQuotationId)
            Else
                ExpenseDAL.Add(ExpenseDetail, objMod.VendorQuotationId)
            End If
            ''End TFS3109
            SaveDocument(objMod.VendorQuotationId, Source, trans, objPath, arrFile, objMod.VendorQuotationDate, objMod.PurchaseInquiryId)
            Dim DetailDAL As New VendorQuotationDetailDAL()
            'DetailDAL.DeleteSIIWise(objMod.DetailList, objMod.VendorQuotationId, trans)
            VendorQuotationDetailId = DetailDAL.AddSingle(objMod, trans)
            'DetailDAL.AddUpdate(objMod, trans)
            'VendorsDAL.DeleteSIIWise(objMod.PurchaseInquiryId, trans)
            'VendorsDAL.Add(objMod, trans)
            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()
        End Try

    End Function

End Class

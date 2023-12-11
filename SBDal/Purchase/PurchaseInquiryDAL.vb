Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net
Imports System.Drawing
Imports System.IO

Public Class PurchaseInquiryDAL
    Dim NotificationDAL As New NotificationTemplatesDAL
    'PurchaseInquiryId	int	Unchecked
    'PurchaseInquiryNo	nvarchar(100)	Checked
    'PurchaseInquiryDate	datetime	Checked
    'DueDate	datetime	Checked
    'IndentNo	nvarchar(50)	Checked
    'IndentingDepartment	nvarchar(50)	Checked
    'OldInquiryNo	nvarchar(100)	Checked
    'OldInquiryDate	datetime	Checked
    'Remarks	nvarchar(500)	Checked
    '		Unchecked
    ''TFS2375 : Added Optional Variable PurchaseInqId by Reference to get the PurchaseInquiryId
    Public Function Add(ByVal objMod As PurchaseInquiryMaster, ByVal Source As String, ByVal objPath As String, _
                        ByVal arrFile As List(Of String), ByVal GroupId As Integer, ByVal UserId As Integer, _
                        Optional ByRef PurchaseInqId As Integer = 0) As Boolean

        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO PurchaseInquiryMaster(PurchaseInquiryNo, PurchaseInquiryDate, DueDate, IndentNo, IndentingDepartment, OldInquiryNo, OldInquiryDate, Remarks, UserName, SalesInquiryId, PurchaseDemandId , Posted , Posted_UserName , PostedDate) " _
            & " VALUES('" & objMod.PurchaseInquiryNo.Replace("'", "''") & "', '" & objMod.PurchaseInquiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', '" & objMod.DueDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', '" & objMod.IndentNo.Replace("'", "''") & "', '" & objMod.IndentingDepartment.Replace("'", "''") & "', '" & objMod.OldInquiryNo.Replace("'", "''") & "', " & IIf(objMod.OldInquiryDate = DateTime.MinValue, "NULL", "Convert(DateTime, '" & objMod.OldInquiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', 102)") & ", '" & objMod.Remarks.Replace("'", "''") & "' , '" & objMod.UserName.Replace("'", "''") & "', " & objMod.SalesInquiryId & ", " & objMod.SalesInquiryId & "," & IIf(objMod.Posted = True, 1, 0) & ",'" & objMod.Posted_UserName.Replace("'", "''") & "','" & objMod.PostedDate.ToString("yyyy-M-dd hh:mm:ss tt") & "') Select @@Identity"
            Dim PurchaseInquiryId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objMod.PurchaseInquiryId = PurchaseInquiryId
            Dim DetailDAL As New PurchaseInquiryDetailDAL()
            Dim VendorsDAL As New PurchaseInquiryVendorsDAL()
            'DetailDAL.DeleteSIIWise(PurchaseInquiryId)
            DetailDAL.Add(objMod, trans, GroupId, UserId)
            VendorsDAL.Add(objMod, trans)
            If arrFile.Count > 0 Then
                SaveDocument(PurchaseInquiryId, Source, trans, objPath, arrFile, objMod.PurchaseInquiryDate, objMod.PurchaseInquiryId)
            End If
            trans.Commit()
            Dim ValueTable As DataTable = GetSingle(PurchaseInquiryId)
            NotificationDAL.SaveAndSendNotification("Purchase Inquiry", "PurchaseInquiryMaster", PurchaseInquiryId, ValueTable, "Purchase > Purchase Inquiry")

            ''If PurchaseInqId <> 0 Then
            'Store the newly generated id in the reference parameter
            PurchaseInqId = objMod.PurchaseInquiryId
            'End If

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal objMod As PurchaseInquiryMaster, ByVal Source As String, ByVal objPath As String, ByVal arrFile As List(Of String), ByVal GroupId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Update PurchaseInquiryMaster SET PurchaseInquiryNo= '" & objMod.PurchaseInquiryNo.Replace("'", "''") & "', PurchaseInquiryDate='" & objMod.PurchaseInquiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', DueDate='" & objMod.DueDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', IndentNo='" & objMod.IndentNo.Replace("'", "''") & "', IndentingDepartment='" & objMod.IndentingDepartment.Replace("'", "''") & "', OldInquiryNo='" & objMod.OldInquiryNo.Replace("'", "''") & "', OldInquiryDate=" & IIf(objMod.OldInquiryDate = DateTime.MinValue, "NULL", "Convert(DateTime, '" & objMod.OldInquiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', 102)") & ", Remarks='" & objMod.Remarks.Replace("'", "''") & "', UserName = '" & objMod.UserName.Replace("'", "''") & "', SalesInquiryId = " & objMod.SalesInquiryId & ", PurchaseDemandId = " & objMod.SalesInquiryId & ",Posted= " & IIf(objMod.Posted = True, 1, 0) & ", Posted_UserName= '" & objMod.Posted_UserName.Replace("'", "''") & "', PostedDate ='" & objMod.PostedDate.ToString("yyyy-M-dd hh:mm:ss tt") & "' WHERE PurchaseInquiryId= " & objMod.PurchaseInquiryId & " Select @@Identity"
            Dim PurchaseInquiryId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Dim DetailDAL As New PurchaseInquiryDetailDAL()
            Dim VendorsDAL As New PurchaseInquiryVendorsDAL()
            DetailDAL.DeleteSIIWise(objMod.PurchaseInquiryId, trans)
            DetailDAL.AddUpdate(objMod, trans, GroupId)
            VendorsDAL.DeleteSIIWise(objMod.PurchaseInquiryId, trans)
            VendorsDAL.Add(objMod, trans)
            SaveDocument(objMod.PurchaseInquiryId, Source, trans, objPath, arrFile, objMod.PurchaseInquiryDate, objMod.PurchaseInquiryId)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()
        End Try

    End Function

    Public Function Delete(ByVal obj As PurchaseInquiryMaster, ByVal GroupId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try

            Dim strSQL As String = String.Empty
            strSQL = "Delete From PurchaseInquiryMaster WHERE PurchaseInquiryId=" & obj.PurchaseInquiryId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Dim DetailDAL As New PurchaseInquiryDetailDAL()
            Dim VendorsDAL As New PurchaseInquiryVendorsDAL()
            For Each objMod As PurchaseInquiryDetail In obj.DetailList
                If objMod.SalesInquiryDetailId > 0 Then
                    DetailDAL.UpdateSalesInquiryStatusAgainstSubtraction(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans)
                    DetailDAL.UpdateAssigningSubtractionStatus(objMod.SalesInquiryId, objMod.SalesInquiryDetailId, objMod.Qty, trans, GroupId)
                    DetailDAL.UpdateSecurityStatus(objMod.SalesInquiryDetailId, GroupId, objMod.Qty, trans)
                End If
            Next
            DetailDAL.DeleteSIIWise(obj.PurchaseInquiryId, trans)
            VendorsDAL.DeleteSIIWise(obj.PurchaseInquiryId, trans)
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
            'PurchaseInquiryNo, PurchaseInquiryDate, DueDate, IndentNo, IndentingDepartment, OldInquiryNo, OldInquiryDate, Remarks
            Dim dt As New DataTable
            'dt = UtilityDAL.GetDataTable("SELECT PurchaseInquiryId, PurchaseInquiryNo, PurchaseInquiryDate, DueDate, IndentNo, IndentingDepartment, OldInquiryNo, OldInquiryDate, Remarks, UserName, Doc_Att.NoOfAttachment As [No Of Attachments] FROM dbo.PurchaseInquiryMaster LEFT OUTER JOIN (Select Count(*) as NoOfAttachment, DocId From DocumentAttachment WHERE (source =N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = PurchaseInquiryMaster.PurchaseInquiryId " & IIf(UserName = "", "", "Where UserName='" & UserName & "'") & " Order By PurchaseInquiryDate DESC")
            dt = UtilityDAL.GetDataTable("SELECT dbo.PurchaseInquiryMaster.PurchaseInquiryId, dbo.PurchaseInquiryMaster.PurchaseInquiryNo, SalesInquiryMaster.SalesInquiryNo, SalesInquiryMaster.CustomerInquiryNo, dbo.PurchaseInquiryMaster.PurchaseInquiryDate, dbo.PurchaseInquiryMaster.DueDate, dbo.PurchaseInquiryMaster.IndentNo, dbo.PurchaseInquiryMaster.IndentingDepartment, dbo.PurchaseInquiryMaster.OldInquiryNo, dbo.PurchaseInquiryMaster.OldInquiryDate, dbo.PurchaseInquiryMaster.Remarks, dbo.PurchaseInquiryMaster.UserName, Doc_Att.NoOfAttachment As [No Of Attachments] , dbo.PurchaseInquiryMaster.Posted , dbo.PurchaseInquiryMaster.Posted_UserName , dbo.PurchaseInquiryMaster.PostedDate, CASE WHEN ISNULL(EmailLog.EmailCount, 0) > 0 THEN 'Sent' ELSE 'Not sent' END AS [Email Status], ISNULL(EmailLog.EmailCount, 0) AS [Email Sent] FROM dbo.PurchaseInquiryMaster LEFT OUTER JOIN (Select Count(*) as NoOfAttachment, DocId From DocumentAttachment WHERE (source =N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = PurchaseInquiryMaster.PurchaseInquiryId Left Outer Join(Select Distinct PurchaseInquiryId, SalesInquiryId From PurchaseInquiryDetail) As Detail ON  PurchaseInquiryMaster.PurchaseInquiryId = Detail.PurchaseInquiryId Left Outer Join SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId LEFT OUTER JOIN (SELECT Count(DocumentNo) AS EmailCount, DocumentNo FROM tblEmailLog GROUP BY DocumentNo) AS EmailLog ON PurchaseInquiryMaster.PurchaseInquiryNo = EmailLog.DocumentNo " & IIf(UserName = "", "", "Where PurchaseInquiryMaster.UserName='" & UserName & "'") & " Order By PurchaseInquiryDate DESC")
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTop50(Optional ByVal Source As String = "", Optional ByVal UserName As String = "") As DataTable
        Try
            'PurchaseInquiryNo, PurchaseInquiryDate, DueDate, IndentNo, IndentingDepartment, OldInquiryNo, OldInquiryDate, Remarks
            Dim dt As New DataTable
            'dt = UtilityDAL.GetDataTable("SELECT Distinct Top 50 PurchaseInquiryId, PurchaseInquiryNo, PurchaseInquiryDate, DueDate, IndentNo, IndentingDepartment, OldInquiryNo, OldInquiryDate, Remarks, UserName, Doc_Att.NoOfAttachment As [No Of Attachments] FROM dbo.PurchaseInquiryMaster LEFT OUTER JOIN (Select Count(*) as NoOfAttachment, DocId From DocumentAttachment WHERE (source =N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = PurchaseInquiryMaster.PurchaseInquiryId " & IIf(UserName = "", "", "Where UserName='" & UserName & "'") & " Order By PurchaseInquiryDate DESC")
            dt = UtilityDAL.GetDataTable("SELECT Top 50 dbo.PurchaseInquiryMaster.PurchaseInquiryId, dbo.PurchaseInquiryMaster.PurchaseInquiryNo, SalesInquiryMaster.SalesInquiryNo, SalesInquiryMaster.CustomerInquiryNo, dbo.PurchaseInquiryMaster.PurchaseInquiryDate, dbo.PurchaseInquiryMaster.DueDate, dbo.PurchaseInquiryMaster.IndentNo, dbo.PurchaseInquiryMaster.IndentingDepartment, dbo.PurchaseInquiryMaster.OldInquiryNo, dbo.PurchaseInquiryMaster.OldInquiryDate, dbo.PurchaseInquiryMaster.Remarks, dbo.PurchaseInquiryMaster.UserName, Doc_Att.NoOfAttachment As [No Of Attachments] , dbo.PurchaseInquiryMaster.Posted , dbo.PurchaseInquiryMaster.Posted_UserName , dbo.PurchaseInquiryMaster.PostedDate FROM dbo.PurchaseInquiryMaster LEFT OUTER JOIN (Select Count(*) as NoOfAttachment, DocId From DocumentAttachment WHERE (source =N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = PurchaseInquiryMaster.PurchaseInquiryId Left Outer Join(Select Distinct PurchaseInquiryId, SalesInquiryId From PurchaseInquiryDetail) As Detail ON  PurchaseInquiryMaster.PurchaseInquiryId = Detail.PurchaseInquiryId Left Outer Join SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId " & IIf(UserName = "", "", "Where PurchaseInquiryMaster.UserName='" & UserName & "'") & " Order By PurchaseInquiryDate DESC")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAll(Optional ByVal Source As String = "", Optional ByVal UserName As String = "") As DataTable
        Try
            'PurchaseInquiryNo, PurchaseInquiryDate, DueDate, IndentNo, IndentingDepartment, OldInquiryNo, OldInquiryDate, Remarks
            Dim dt As New DataTable
            'dt = UtilityDAL.GetDataTable("SELECT PurchaseInquiryId, PurchaseInquiryNo, PurchaseInquiryDate, DueDate, IndentNo, IndentingDepartment, OldInquiryNo, OldInquiryDate, Remarks, UserName, Doc_Att.NoOfAttachment As [No Of Attachments] FROM dbo.PurchaseInquiryMaster LEFT OUTER JOIN (Select Count(*) as NoOfAttachment, DocId From DocumentAttachment WHERE (source =N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = PurchaseInquiryMaster.PurchaseInquiryId " & IIf(UserName = "", "", "Where UserName='" & UserName & "'") & " Order By PurchaseInquiryDate DESC")
            dt = UtilityDAL.GetDataTable("SELECT dbo.PurchaseInquiryMaster.PurchaseInquiryId, dbo.PurchaseInquiryMaster.PurchaseInquiryNo, SalesInquiryMaster.SalesInquiryNo, SalesInquiryMaster.CustomerInquiryNo, dbo.PurchaseInquiryMaster.PurchaseInquiryDate, dbo.PurchaseInquiryMaster.DueDate, dbo.PurchaseInquiryMaster.IndentNo, dbo.PurchaseInquiryMaster.IndentingDepartment, dbo.PurchaseInquiryMaster.OldInquiryNo, dbo.PurchaseInquiryMaster.OldInquiryDate, dbo.PurchaseInquiryMaster.Remarks, dbo.PurchaseInquiryMaster.UserName, Doc_Att.NoOfAttachment As [No Of Attachments], dbo.PurchaseInquiryMaster.Posted , dbo.PurchaseInquiryMaster.Posted_UserName , dbo.PurchaseInquiryMaster.PostedDate FROM dbo.PurchaseInquiryMaster LEFT OUTER JOIN (Select Count(*) as NoOfAttachment, DocId From DocumentAttachment WHERE (source =N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = PurchaseInquiryMaster.PurchaseInquiryId Left Outer Join(Select Distinct PurchaseInquiryId, SalesInquiryId From PurchaseInquiryDetail) As Detail ON  PurchaseInquiryMaster.PurchaseInquiryId = Detail.PurchaseInquiryId Left Outer Join SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId " & IIf(UserName = "", "", "Where PurchaseInquiryMaster.UserName='" & UserName & "'") & " Order By PurchaseInquiryDate DESC")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSingle(ByVal PurchaseInquiryId As Integer) As DataTable
        Try
            'PurchaseInquiryNo, PurchaseInquiryDate, DueDate, IndentNo, IndentingDepartment, OldInquiryNo, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT PurchaseInquiryId, PurchaseInquiryNo, PurchaseInquiryDate, DueDate, IndentNo, IndentingDepartment, OldInquiryNo, OldInquiryDate, Remarks, UserName , Posted , Posted_UserName , PostedDate FROM dbo.PurchaseInquiryMaster Where PurchaseInquiryMaster.PurchaseInquiryId=" & PurchaseInquiryId & "")
            dt.AcceptChanges()

            Return dt
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
                        Dim New_Files As String = intId & "_" & DocId & "_RFQ" & PurchaseInquiryId & "_" & docDate.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
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

    Public Function GetAttachments(ByVal Source As String, ByVal PurchaseInquiryID As Integer) As DataTable
        Try
            Dim dt As New DataTable
            Dim converter As New ImageConverter
            dt = UtilityDAL.GetDataTable("Select Id, DocId,FileName,Path, Convert(Image,'')  as Attachment_Image,1 as Pic  From DocumentAttachment WHERE Source='" & Source.Replace("'", "''") & "' AND DocId=" & PurchaseInquiryID & " and right(filename,3) in ('BMP','DIB','RLE','JPG','JPEG','JPE','JFIF','GIF','TIF','TIFF','PNG') union all Select Id, DocId,FileName,Path,  Convert(Image,'') ,0  From DocumentAttachment WHERE Source='" & Source.Replace("'", "''") & "' AND DocId=" & PurchaseInquiryID & " and right(filename,3) not in ('BMP','DIB','RLE','JPG','JPEG','JPE','JFIF','GIF','TIF','TIFF','PNG')")
            dt.AcceptChanges()
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        If r.Item("PIC").ToString = "1" Then
                            LoadPicture(r, "Attachment_Image", r.Item("Path").ToString & "\" & r.Item("FileName").ToString)
                        Else
                            '               r("Attachment_Image") = imgBytes
                        End If
                        r.EndEdit()
                    Next
                End If
            End If
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub LoadPicture(ByVal ObjDataRow As DataRow, ByVal strImageField As String, ByVal FilePath As String)
        Try
            If IO.File.Exists(FilePath) Then
                If FilePath.Length > 0 Then

                    Dim objImage As Image = Image.FromFile(FilePath)
                    objImage = SizeImage(objImage, 400, 600)
                    Dim ms As New MemoryStream
                    objImage.Save(ms, Imaging.ImageFormat.Png)
                    Dim oBytes() As Byte = ms.ToArray
                    objImage.Dispose()
                    ms.Close()
                    'Dim fs As IO.FileStream = New IO.FileStream(FilePath, IO.FileMode.Open, IO.FileAccess.Read)
                    'Dim OImage As Byte() = New Byte(fs.Length) {}
                    'fs.Read(OImage, 0, fs.Length)
                    ObjDataRow(strImageField) = oBytes
                    'fs.Flush()
                    'fs.Dispose()
                    'fs.Close()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Function SizeImage(ByVal img As Image, ByVal width As Integer, ByVal height As Integer) As Image
        Dim newBit As New Bitmap(width, height) 'new blank bitmap
        Dim g As Graphics = Graphics.FromImage(newBit)
        'change interpolation for reduction quality
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
        g.DrawImage(img, 0, 0, width, height)
        Return newBit
    End Function

    Public Function AddCCBCCEmails(ByVal List As List(Of CCBCCEmails)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            For Each obj As CCBCCEmails In List
                Dim strSQL As String = String.Empty
                strSQL = "If Not Exists(Select EmailAddressId From CCBCCEmailsTable Where EmailAddress ='" & obj.EmailAddress.Replace("'", "''") & "') INSERT INTO CCBCCEmailsTable(EmailAddress) " _
                & " VALUES(N'" & obj.EmailAddress & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetCCBCCEmails() As DataTable
        Try
            'PurchaseInquiryNo, PurchaseInquiryDate, DueDate, IndentNo, IndentingDepartment, OldInquiryNo, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT EmailAddress FROM CCBCCEmailsTable")
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Public Function GetNextCIN() As String
    '    Try
    '        Dim CustomerInquiryNo As String = ""
    '        'PurchaseInquiryNo, PurchaseInquiryDate, DueDate, IndentNo, IndentingDepartment, OldInquiryNo, OldInquiryDate, Remarks
    '        Dim dt As New DataTable
    '        dt = UtilityDAL.GetDataTable("SELECT ISNULL(Max(Convert(Integer, CustomerInquiryNo)), 0) + 1 FROM PurchaseInquiryMaster")
    '        dt.AcceptChanges()
    '        If dt.Rows.Count > 0 Then
    '            CustomerInquiryNo = dt.Rows(0).Item(0).ToString
    '        Else
    '            CustomerInquiryNo = ""
    '        End If
    '        Return CustomerInquiryNo
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

End Class

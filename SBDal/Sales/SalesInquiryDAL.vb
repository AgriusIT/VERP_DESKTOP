Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net
Public Class SalesInquiryDAL
    Dim NotificationDAL As New NotificationTemplatesDAL

    Public Function Add(ByVal objMod As SalesInquiryMaster, ByVal Source As String, ByVal objPath As String, ByVal arrFile As List(Of String), Optional ByRef SalesInqId As Integer = 0) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO SalesInquiryMaster(SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks, UserName , Post , Posted_UserName , PostedDate) " _
            & " VALUES('" & objMod.SalesInquiryNo.Replace("'", "''") & "', '" & objMod.SalesInquiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', " & objMod.CustomerId & ", " & objMod.LocationId & ", " & objMod.ContactPersonId & ", '" & objMod.CustomerInquiryNo.Replace("'", "''") & "', '" & objMod.IndentNo.Replace("'", "''") & "', '" & objMod.IndentDepartment.Replace("'", "''") & "', '" & objMod.CustomerInquiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "','" & objMod.OldInquiryNo.Replace("'", "''") & "','" & objMod.DueDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', " & IIf(objMod.OldInquiryDate = DateTime.MinValue, "NULL", "Convert(DateTime, '" & objMod.OldInquiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', 102)") & ", '" & objMod.Remarks.Replace("'", "''") & "' , '" & objMod.UserName.Replace("'", "''") & "'," & IIf(objMod.Posted = True, 1, 0) & ",'" & objMod.Posted_UserName.Replace("'", "''") & "','" & objMod.PostedDate.ToString("yyyy-M-dd hh:mm:ss tt") & "') Select @@Identity"
            Dim SalesInquiryId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objMod.SalesInquiryId = SalesInquiryId
            SalesInqId = objMod.SalesInquiryId
            Dim DetailDAL As New SalesInquiryDetailDAL()
            'DetailDAL.DeleteSIIWise(SalesInquiryId)

            '' Adding notifications
            'Dim Notification As New AgriusNotifications

            'Notification.NotificationTitle = objMod.TaskName
            'Notification.NotificationDescription = objMod.TaskRemarks
            'Notification.SourceApplication = "CRM > Task"
            'Notification.ApplicationReference = objMod.TaskId

            'Dim objDList As New List(Of NotificationDetail)
            'objDList.Add(New NotificationDetail(New SecurityUser(objMod.UserId)))
            'Notification.NotificationDetils = objDList

            'Dim Dal As New NotificationDAL
            'Dim list As New List(Of AgriusNotifications)
            'list.Add(Notification)

            'Dal.AddNotification(list)
            '' End 
            DetailDAL.Add(objMod, trans)
            SaveDocument(SalesInquiryId, Source, trans, objPath, arrFile, objMod.SalesInquiryDate, objMod.CustomerId)
            'NotificationDAL.SaveAndSendNotification("Sales Inquiry", "SalesInquiryMaster", SalesInquiryId, GetSingle(SalesInquiryId), "Sales > Sales Inquiry")




            ' *** New Segment *** By Shahid Rasool *** 10-Jun-2017 ***
            '// Adding notification

            '// Creating new object of Notification configuration dal
            '// Dal will be used to get users list for the notification 
            Dim NDal As New NotificationConfigurationDAL

            '// Creating new object of Agrius Notification class
            Dim Notification As New AgriusNotifications

            '// Reference document number
            Notification.ApplicationReference = SalesInquiryId

            '// Date of notification
            Notification.NotificationDate = Now

            '// Preparing notification title string
            Notification.NotificationTitle = "New Sales [" & objMod.SalesInquiryNo & "]  is added with " & objMod.DetailList.Count & " items."

            '// Preparing notification description string
            Notification.NotificationDescription = "New Sales [" & objMod.SalesInquiryNo & "] is added by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

            '// Setting source application as refrence in the notification
            Notification.SourceApplication = "Sales"



            '// Starting to get users list to add child

            '// Creating notification detail object list
            Dim List As New List(Of NotificationDetail)

            '// Getting users list
            List = NDal.GetNotificationUsers("Sales Created")

            '// Adding users list in the Notification object of current inquiry
            'Commented because of Notifications Malfunctioning By Ali Faisal: September 24, 2018
            'objMod.Notification.NotificationDetils.AddRange(List)

            '// Getting and adding user groups list in the Notification object of current inquiry
            'objMod.Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("Sales Created"))

            '// Not getting role list because no role is associated at the moment
            '// We will need this in future and we can use it later
            '// We can consult to Update function of this class


            '// ***This segment will be used to save notification in database table

            '// Creating new list of objects of Agrius Notification
            Dim NList As New List(Of AgriusNotifications)

            '// Copying notification object from current sales inquiry to newly defined instance
            '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
            NList.Add(objMod.Notification)

            '// Creating object of Notification DAL
            Dim GNotification As New NotificationDAL

            '// Saving notification to database
            'GNotification.AddNotification(NList, trans)

            '// End Adding Notification

            '// End Adding Notification
            ' *** End Segment ***

            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal objMod As SalesInquiryMaster, ByVal Source As String, ByVal objPath As String, ByVal arrFile As List(Of String)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Update SalesInquiryMaster SET SalesInquiryNo= '" & objMod.SalesInquiryNo.Replace("'", "''") & "', SalesInquiryDate='" & objMod.SalesInquiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', CustomerId=" & objMod.CustomerId & ", LocationId=" & objMod.LocationId & ", ContactPersonId=" & objMod.ContactPersonId & ", CustomerInquiryNo='" & objMod.CustomerInquiryNo.Replace("'", "''") & "', IndentNo='" & objMod.IndentNo.Replace("'", "''") & "', IndentDepartment='" & objMod.IndentDepartment.Replace("'", "''") & "', CustomerInquiryDate='" & objMod.CustomerInquiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', OldInquiryNo='" & objMod.OldInquiryNo.Replace("'", "''") & "', DueDate='" & objMod.DueDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', OldInquiryDate=" & IIf(objMod.OldInquiryDate = DateTime.MinValue, "NULL", "Convert(DateTime, '" & objMod.OldInquiryDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', 102)") & ", Remarks='" & objMod.Remarks.Replace("'", "''") & "', UserName = '" & objMod.UserName.Replace("'", "''") & "',Post= " & IIf(objMod.Posted = True, 1, 0) & ", Posted_UserName= '" & objMod.Posted_UserName.Replace("'", "''") & "', PostedDate ='" & objMod.PostedDate.ToString("yyyy-M-dd hh:mm:ss tt") & "' WHERE SalesInquiryId=" & objMod.SalesInquiryId & " Select @@Identity"
            Dim SalesInquiryId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'objMod.SalesInquiryId = SalesInquiryId

            Dim DetailDAL As New SalesInquiryDetailDAL()

            '// 15-May-2017
            '// Commented by Rai Shahid to remove delete insert.
            'DetailDAL.DeleteSIIWise(objMod.SalesInquiryId, trans)
            'DetailDAL.Add(objMod, trans)

            '// Updating each row, used pervious function
            'TODO: Function need to be verified
            DetailDAL.Update(objMod.DetailList, trans)

            SaveDocument(objMod.SalesInquiryId, Source, trans, objPath, arrFile, objMod.SalesInquiryDate, objMod.CustomerId)

            ' *** New Segment *** By Shahid Rasool *** 10-Jun-2017 ***
            '// Adding notification

            '// Creating new object of Agrius Notification class
            objMod.Notification = New AgriusNotifications

            '// Reference document number
            objMod.Notification.ApplicationReference = SalesInquiryId

            '// Date of notification
            objMod.Notification.NotificationDate = Now

            '// Preparing notification title string
            objMod.Notification.NotificationTitle = "Sales Inquiry [" & objMod.SalesInquiryNo & "] is updated"

            '// Preparing notification description string
            objMod.Notification.NotificationDescription = "Sales Inquiry [" & objMod.SalesInquiryNo & "] is updated by user " & LoginUser.LoginUserName & " on " & Date.Now.ToString("dd-MMM-yyy hh:mm:ss")

            '// Setting source application as refrence in the notification
            objMod.Notification.SourceApplication = "Sales Inquiry"


            '// Starting to get users list to add child

            '// Creating new object of Notification configuration dal
            '// Dal will be used to get users list for the notification 
            Dim NDal As New NotificationConfigurationDAL

            '// Creating notification detail object list
            Dim List As New List(Of NotificationDetail)

            '// Getting users list
            List = NDal.GetNotificationUsers("Sales Inquiry Changed")

            '// Adding users list in the Notification object of current inquiry
            objMod.Notification.NotificationDetils.AddRange(List)

            '// Getting and adding user groups list in the Notification object of current inquiry
            objMod.Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("Sales Inquiry Changed"))

            '// Getting notification roles list configured for current notification
            Dim objRole As Object = NDal.GetNotificationRoles("Sales Inquiry Changed")

            '// Using loop to go through each role
            For Each obj As Object In objRole

                '// Using case statment to differentiate each user role
                Select Case obj.RoleTitle

                    Case "Assigned To"

                    Case "Previously Assigned To"

                End Select


            Next

            '// This segment will be used to save notification in database table

            '// Creating new list of objects of Agrius Notification
            Dim NList As New List(Of AgriusNotifications)

            '// Copying notification object from current sales inquiry to newly defined instance
            '// Reason to copy here is that while saving record we need list of Notification object but we have only one object of Agrius Notification
            NList.Add(objMod.Notification)

            '// Creating object of Notification DAL
            Dim GNotification As New NotificationDAL

            '// Saving notification to database
            GNotification.AddNotification(NList, trans)

            '// End Adding Notification
            ' *** End Segment ***

            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try

    End Function

    Public Function Delete(ByVal SalesInquiryId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try

            Dim strSQL As String = String.Empty

            Dim DetailDAL As New SalesInquiryDetailDAL()
            DetailDAL.DeleteSIIWise(SalesInquiryId, trans)

            strSQL = "Delete From SalesInquiryMaster WHERE SalesInquiryId=" & SalesInquiryId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
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
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT SalesInquiry.SalesInquiryId, SalesInquiry.SalesInquiryNo, SalesInquiry.SalesInquiryDate, IsNull(COA.coa_detail_id,0) as CustomerId, COA.detail_code As Code, COA.detail_title As Customer, SalesInquiry.LocationId, SalesInquiry.ContactPersonId, SalesInquiry.CustomerInquiryNo, SalesInquiry.IndentNo, SalesInquiry.IndentDepartment, SalesInquiry.CustomerInquiryDate, SalesInquiry.OldInquiryNo, SalesInquiry.DueDate, SalesInquiry.OldInquiryDate, SalesInquiry.Remarks, SalesInquiry.UserName, Doc_Att.NoOfAttachment As [No Of Attachments] , SalesInquiry.Post , SalesInquiry.Posted_UserName , SalesInquiry.PostedDate FROM dbo.SalesInquiryMaster AS SalesInquiry LEFT OUTER JOIN  dbo.vwCOADetail AS COA ON SalesInquiry.CustomerId = COA.coa_detail_id LEFT OUTER JOIN (Select Count(*) as NoOfAttachment, DocId From DocumentAttachment WHERE (source =N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = SalesInquiry.SalesInquiryId " & IIf(UserName = "", "", " Where SalesInquiry.UserName ='" & UserName & "'") & " Order by SalesInquiry.SalesInquiryDate DESC")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTop50(Optional ByVal Source As String = "", Optional ByVal UserName As String = "") As DataTable
        Try
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT Distinct Top 50 SalesInquiry.SalesInquiryId, SalesInquiry.SalesInquiryNo, SalesInquiry.SalesInquiryDate, IsNull(COA.coa_detail_id,0) as CustomerId, COA.detail_code As Code, COA.detail_title As Customer, SalesInquiry.LocationId, SalesInquiry.ContactPersonId, SalesInquiry.CustomerInquiryNo, SalesInquiry.IndentNo, SalesInquiry.IndentDepartment, SalesInquiry.CustomerInquiryDate, SalesInquiry.OldInquiryNo, SalesInquiry.DueDate, SalesInquiry.OldInquiryDate, SalesInquiry.Remarks, SalesInquiry.UserName, Doc_Att.NoOfAttachment As [No Of Attachments] , SalesInquiry.Post , SalesInquiry.Posted_UserName , SalesInquiry.PostedDate FROM dbo.SalesInquiryMaster AS SalesInquiry LEFT OUTER JOIN  dbo.vwCOADetail AS COA ON SalesInquiry.CustomerId = COA.coa_detail_id LEFT OUTER JOIN (Select Count(*) as NoOfAttachment, DocId From DocumentAttachment WHERE (source =N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = SalesInquiry.SalesInquiryId " & IIf(UserName = "", "", " Where SalesInquiry.UserName ='" & UserName & "'") & " Order by SalesInquiry.SalesInquiryDate DESC")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAll(Optional ByVal Source As String = "", Optional ByVal UserName As String = "") As DataTable
        Try
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT SalesInquiry.SalesInquiryId, SalesInquiry.SalesInquiryNo, SalesInquiry.SalesInquiryDate, IsNull(COA.coa_detail_id,0) as CustomerId, COA.detail_code As Code, COA.detail_title As Customer, SalesInquiry.LocationId, SalesInquiry.ContactPersonId, SalesInquiry.CustomerInquiryNo, SalesInquiry.IndentNo, SalesInquiry.IndentDepartment, SalesInquiry.CustomerInquiryDate, SalesInquiry.OldInquiryNo, SalesInquiry.DueDate, SalesInquiry.OldInquiryDate, SalesInquiry.Remarks, SalesInquiry.UserName, Doc_Att.NoOfAttachment As [No Of Attachments] , SalesInquiry.Post , SalesInquiry.Posted_UserName , SalesInquiry.PostedDate FROM dbo.SalesInquiryMaster AS SalesInquiry LEFT OUTER JOIN  dbo.vwCOADetail AS COA ON SalesInquiry.CustomerId = COA.coa_detail_id LEFT OUTER JOIN (Select Count(*) as NoOfAttachment, DocId From DocumentAttachment WHERE (source =N'" & Source & "') Group By DocId, Source) Doc_Att on Doc_Att.DocId = SalesInquiry.SalesInquiryId " & IIf(UserName = "", "", " Where SalesInquiry.UserName ='" & UserName & "'") & " Order by SalesInquiry.SalesInquiryDate DESC")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ValidateCustomerInquiryNo(ByVal CustomerInquiryNo As String, ByVal CustomerId As Integer) As Boolean
        Try
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT CustomerInquiryNo FROM dbo.SalesInquiryMaster Where REPLACE( REPLACE( REPLACE( REPLACE( REPLACE( REPLACE( REPLACE(CustomerInquiryNo, '!', '' ), '#', '' ), '$', '' ), '&', '' ), '-', '' ), '@', '' ), ' ', '' ) ='" & CustomerInquiryNo & "' And CustomerId =" & CustomerId & "")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0).ToString.Length > 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
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
                        Dim New_Files As String = intId & "_" & DocId & "_SINQ" & PurchaseInquiryId & "_" & docDate.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
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
    Public Function GetSalesInquiryStatus(ByVal CustomerId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, Optional ByVal Status As String = "") As DataTable
        Try
            Dim strSQL As String = " SELECT SalesInquiry.SalesInquiryId, SalesInquiry.SalesInquiryNo, SalesInquiry.SalesInquiryDate, " _
                                  & " SalesInquiry.CustomerId, COA.detail_code As Code, COA.detail_title As Customer, SalesInquiry.CustomerInquiryNo,  SalesInquiry.IndentNo, " _
                                  & " SalesInquiry.IndentDepartment, SalesInquiry.CustomerInquiryDate,  SalesInquiry.OldInquiryNo, InquiryDetail.Qty, InquiryDetail.PurchasedQty, InquiryDetail.Remaining, QuotationDetail.QuotationQty, SalesQuotation.SalesQuotationQty, " _
                                  & " SalesInquiry.DueDate,  SalesInquiry.OldInquiryDate, SalesInquiry.Remarks , SalesInquiry.Post , SalesInquiry.Posted_UserName , SalesInquiry.PostedDate " _
                                  & " FROM SalesInquiryMaster As SalesInquiry LEFT OUTER JOIN (Select  Detail.SalesInquiryId, Sum(IsNull(Detail.Qty, 0)) As Qty, Sum(IsNull(Detail.PurchasedQty, 0)) As PurchasedQty, Sum(IsNull(Detail.Qty, 0)-IsNull(Detail.PurchasedQty, 0)) As Remaining From SalesInquiryDetail As Detail Inner Join SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId Group By Detail.SalesInquiryId) As InquiryDetail ON SalesInquiry.SalesInquiryId = InquiryDetail.SalesInquiryId " _
                                  & " Left Outer Join (Select IsNull(PurchaseInquiryDetail.SalesInquiryId, 0) As SalesInquiryId, IsNull(PurchaseInquiryDetail.PurchaseInquiryId, 0) As PurchaseInquiryId, Sum(IsNull(VendorQuotationDetail.Qty, 0)) As QuotationQty From VendorQuotationDetail Inner Join VendorQuotationMaster ON VendorQuotationDetail.VendorQuotationId = VendorQuotationMaster.VendorQuotationId Inner Join PurchaseInquiryDetail ON VendorQuotationMaster.PurchaseInquiryId = PurchaseInquiryDetail.PurchaseInquiryId And VendorQuotationDetail.PurchaseInquiryDetailId = PurchaseInquiryDetail.PurchaseInquiryDetailId GROUP BY PurchaseInquiryDetail.SalesInquiryId, PurchaseInquiryDetail.PurchaseInquiryId) As QuotationDetail ON SalesInquiry.SalesInquiryId = QuotationDetail.SalesInquiryId " _
                                  & " Left Outer Join(Select PurchaseInquiryId, Sum(IsNull(Qty, 0)) As SalesQuotationQty From QuotationDetailTable Group by PurchaseInquiryId) As SalesQuotation ON QuotationDetail.PurchaseInquiryId = SalesQuotation.PurchaseInquiryId " _
                                  & " Inner Join vwCOADetail As COA ON SalesInquiry.CustomerId = COA.coa_detail_Id Where SalesInquiry.SalesInquiryId > 0 "
            Dim dt As New DataTable
            If CustomerId > 0 Then
                strSQL += " And SalesInquiry.CustomerId = " & CustomerId & ""
            End If
            If Not FromDate = DateTime.MinValue Then
                strSQL += " And SalesInquiry.SalesInquiryDate >= Convert(DateTime, '" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102)"
            End If
            If Not ToDate = DateTime.MinValue Then
                strSQL += " And SalesInquiry.SalesInquiryDate <= Convert(DateTime, '" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)"
            End If
            If Status = "Close" Then
                strSQL += " And InquiryDetail.PurchasedQty >= InquiryDetail.Qty "
            End If
            If Status = "Open" Then
                strSQL += " And InquiryDetail.Qty > InquiryDetail.PurchasedQty"
            End If
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetStatusDetail(ByVal SalesInquiryId As Integer) As DataTable
        Try
            Dim strSQL As String = " SELECT Detail.SalesInquiryDetailId,  Detail.SalesInquiryId, " _
                                   & " Detail.SerialNo,  Detail.RequirementDescription,  Detail.ArticleId, " _
                                   & " Article.ArticleCode As Code, Article.ArticleDescription,  Detail.UnitId, " _
                                   & " Unit.ArticleUnitName As Unit,  Detail.ItemTypeId, Type.ArticleTypeName As Type, " _
                                   & " Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, " _
                                   & " SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, " _
                                   & " Origin.ArticleGenderName As Origin, IsNull(Detail.Qty, 0) As Qty, " _
                                   & " ISNULL(Detail.PurchasedQty, 0) As PurchasedQty, (ISNULL(Detail.Qty, 0)-ISNULL(Detail.PurchasedQty, 0)) As Remaining, " _
                                   & " IsNull(VendorQuotationDetail.Qty, 0) As VendorQuotationQty, Detail.Comments " _
                                   & " FROM SalesInquiryDetail As Detail Inner Join SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId LEFT OUTER JOIN " _
                                   & " PurchaseInquiryDetail ON Detail.SalesInquiryDetailId = PurchaseInquiryDetail.SalesInquiryDetailId  LEFT OUTER JOIN " _
                                   & " VendorQuotationDetail ON PurchaseInquiryDetail.PurchaseInquiryDetailId = VendorQuotationDetail.PurchaseInquiryDetailId LEFT OUTER JOIN  " _
                                   & " ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT OUTER JOIN " _
                                   & " ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT OUTER JOIN " _
                                   & " ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT OUTER JOIN " _
                                   & " ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT OUTER JOIN " _
                                   & " ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT OUTER JOIN " _
                                   & " ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where Detail.SalesInquiryId = " & SalesInquiryId & ""
            Dim dt As New DataTable
            'Dim strSQL As String = "SELECT Detail.SalesInquiryDetailId, Detail.SalesInquiryId, Detail.SerialNo, Detail.RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull( Detail.Qty, 0)-IsNull( Detail.PurchasedQty, 0)) As Qty,  Detail.Comments FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId " & IIf(IsAdmin = True, "", "INNER JOIN SalesInquiryRights ON Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId") & "  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where IsNull(Detail.Qty, 0)>IsNull( Detail.PurchasedQty, 0) " & IIf(IsAdmin = True, "", " And SalesInquiryRights.GroupId = " & GroupId & " And SalesInquiryRights.Rights=1") & ""
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSingle(ByVal SalesInquiryId As Integer) As DataTable
        Try
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT SalesInquiry.SalesInquiryId, SalesInquiry.SalesInquiryNo, SalesInquiry.SalesInquiryDate, IsNull(COA.coa_detail_id,0) as CustomerId, COA.detail_code As Code, COA.detail_title As Customer, SalesInquiry.LocationId, SalesInquiry.ContactPersonId, SalesInquiry.CustomerInquiryNo, SalesInquiry.IndentNo, SalesInquiry.IndentDepartment, SalesInquiry.CustomerInquiryDate, SalesInquiry.OldInquiryNo, SalesInquiry.DueDate, SalesInquiry.OldInquiryDate, SalesInquiry.Remarks, SalesInquiry.UserName , SalesInquiry.Post , SalesInquiry.Posted_UserName , SalesInquiry.PostedDate FROM dbo.SalesInquiryMaster AS SalesInquiry LEFT OUTER JOIN  dbo.vwCOADetail AS COA ON SalesInquiry.CustomerId = COA.coa_detail_id Where SalesInquiry.SalesInquiryId = " & SalesInquiryId & " Order by SalesInquiry.SalesInquiryDate DESC")
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

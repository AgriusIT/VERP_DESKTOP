Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net
Public Class SalesInquiryRightsDAL

    Dim NTemplatesDAL As New NotificationTemplatesDAL
    Public Function Add1(ByVal objList As List(Of SalesInquiryRights)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim SalesInquiryRightsId As Integer = 0I
        Dim InquiryRightsIdsList As New List(Of Integer)
        Try
            For Each objMod As SalesInquiryRights In objList
                Dim strSQL As String = String.Empty
                strSQL = " If Not exists(Select SalesInquiryRightsId From SalesInquiryRights Where SalesInquiryRightsId =" & objMod.SalesInquiryRightsId & " And GroupId = " & objMod.GroupId & ") INSERT INTO SalesInquiryRights(GroupId, SalesInquiryDetailId, SalesInquiryId, Rights, UserName, Status, Qty, PurchasedQty, UserId) " _
                & " VALUES(" & objMod.GroupId & ", " & objMod.SalesInquiryDetailId & ", " & objMod.SalesInquiryId & ", " & IIf(objMod.Rights = True, 1, 0) & ", '" & objMod.UserName.Replace("'", "''") & "', '" & objMod.Status.Replace("'", "''") & "', " & objMod.Qty & ", " & objMod.PurchasedQty & " , " & objMod.UserId & ") Else Update SalesInquiryRights Set GroupId =" & objMod.GroupId & ", SalesInquiryDetailId = " & objMod.SalesInquiryDetailId & ", SalesInquiryId =" & objMod.SalesInquiryId & ", Rights = " & IIf(objMod.Rights = True, 1, 0) & ", UserName= '" & objMod.UserName.Replace("'", "''") & "', Status= '" & objMod.Status.Replace("'", "''") & "', Qty= " & objMod.Qty & ", PurchasedQty= " & objMod.PurchasedQty & ", UserId= " & objMod.UserId & "   Where SalesInquiryRightsId =" & objMod.SalesInquiryRightsId & " Select @@IDENTITY"
                SalesInquiryRightsId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                If SalesInquiryRightsId > 0 Then
                    InquiryRightsIdsList.Add(SalesInquiryRightsId)
                End If
            Next
            trans.Commit()
            For Each InquiryRightsId As Integer In InquiryRightsIdsList
                Dim ValueTable As DataTable = GetOne(InquiryRightsId)
                NTemplatesDAL.SaveAndSendNotification("Assigning Of Inquiries", "SalesInquiryRights", SalesInquiryRightsId, ValueTable, "Sales > Sales Inquiry")
            Next
            Return True
        Catch ex As Exception
            trans.Rollback()

            Throw ex
        Finally
            Con.Close()
        End Try

    End Function
    Public Function Add(ByVal objMod As SalesInquiryRights) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim SalesInquiryRightsId As Integer = 0I
        Dim InquiryRightsIdsList As New List(Of Integer)
        Try
            Dim strSQL As String = String.Empty
            strSQL = " If Not exists(Select SalesInquiryRightsId From SalesInquiryRights Where SalesInquiryRightsId =" & objMod.SalesInquiryRightsId & " And GroupId = " & objMod.GroupId & ") INSERT INTO SalesInquiryRights(GroupId, SalesInquiryDetailId, SalesInquiryId, Rights, UserName, Status, Qty, PurchasedQty, UserId, VendorId, RequirementDescription) " _
            & " VALUES(" & objMod.GroupId & ", " & objMod.SalesInquiryDetailId & ", " & objMod.SalesInquiryId & ", " & IIf(objMod.Rights = True, 1, 0) & ", '" & objMod.UserName.Replace("'", "''") & "', '" & objMod.Status.Replace("'", "''") & "', " & objMod.Qty & ", " & objMod.PurchasedQty & " , " & objMod.UserId & ", " & objMod.VendorId & ", '" & objMod.RequirementDescription.Replace("'", "''") & "') Else Update SalesInquiryRights Set GroupId =" & objMod.GroupId & ", SalesInquiryDetailId = " & objMod.SalesInquiryDetailId & ", SalesInquiryId =" & objMod.SalesInquiryId & ", Rights = " & IIf(objMod.Rights = True, 1, 0) & ", UserName= '" & objMod.UserName.Replace("'", "''") & "', Status= '" & objMod.Status.Replace("'", "''") & "', Qty= " & objMod.Qty & ", PurchasedQty= " & objMod.PurchasedQty & ", UserId= " & objMod.UserId & ", VendorId= " & objMod.VendorId & ", RequirementDescription = '" & objMod.RequirementDescription.Replace("'", "''") & "'   Where SalesInquiryRightsId =" & objMod.SalesInquiryRightsId & " Select @@IDENTITY"
            SalesInquiryRightsId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            '// Fetching Group list according event role [Assigned]
            Dim GroupEventRolesAssigned As New List(Of NotificationDetail)

            If objMod.Groups.Count > 0 Then

                Dim strNewGroups As String = String.Empty
                For Each g As SalesInquiryRightsGroups In objMod.Groups
                    strNewGroups = strNewGroups & IIf(strNewGroups.Length > 0, ",", "") & g.GroupId.ToString
                Next

                Dim dtGroups As DataTable = UtilityDAL.GetDataTable("select * from tblUserGroup where GroupId in (" & strNewGroups.ToString & ") and GroupId not in (select groupid from SalesInquiryRightsGroups where GroupId in (" & strNewGroups.ToString & "))", trans)


                If dtGroups.Rows.Count > 0 Then
                    For Each dr As DataRow In dtGroups.Rows
                        Dim NDetail As New NotificationDetail
                        NDetail.GroupId = Val(dr.Item(0).ToString)
                        GroupEventRolesAssigned.Add(NDetail)
                    Next
                End If

            End If

            '// Fetching users list according event role [Assigned]
            Dim UserEventRolesAssigned As New List(Of NotificationDetail)


            If objMod.Users.Count > 0 Then

                Dim strNewUsers As String = String.Empty
                For Each g As SalesInquiryRightsUsers In objMod.Users
                    strNewUsers = strNewUsers & IIf(strNewUsers.Length > 0, ",", "") & g.UserId.ToString
                Next

                Dim dtUsers As DataTable = UtilityDAL.GetDataTable("select user_id from tblUser where user_id in (" & strNewUsers.ToString & ") and user_id not in (select UserId from SalesInquiryRightsUsers where UserId in (" & strNewUsers.ToString & ") and SalesInquiryRightsId= " & objMod.SalesInquiryRightsId & ")", trans)

                If dtUsers.Rows.Count > 0 Then
                    For Each dr As DataRow In dtUsers.Rows
                        Dim NDetail As New NotificationDetail
                        NDetail.NotificationUser = New SecurityUser(Val(dr.Item(0).ToString))
                        UserEventRolesAssigned.Add(NDetail)
                    Next
                End If

            End If

            '// Fetching users list according event role [Previously Assigned] that are removed now
            Dim GroupEventRolesAssignedPreviously As New List(Of NotificationDetail)
            Dim UserEventRolesAssignedPreviously As New List(Of NotificationDetail)

            If SalesInquiryRightsId > 0 Then
                objMod.SalesInquiryRightsId = SalesInquiryRightsId
                AddGroups(objMod, trans)
                AddUsers(objMod, trans)
            Else
                DeleteUsers(objMod.SalesInquiryRightsId, trans)
                DeleteGroups(objMod.SalesInquiryRightsId, trans)
                AddGroups(objMod, trans)
                AddUsers(objMod, trans)
            End If
            'Next


            ' *** New Segment *** By Shahid Rasool *** 11-Jun-2017 ***
            '// Adding notification

            '// Creating new object of Notification configuration dal
            '// Dal will be used to get users list for the notification 
            Dim NDal As New NotificationConfigurationDAL

            '// Reference document number
            objMod.Notification.ApplicationReference = objMod.SalesInquiryId

            '// Date of notification
            objMod.Notification.NotificationDate = Now

            '// Setting source application as refrence in the notification
            objMod.Notification.SourceApplication = "Sales Inquiry Assignment"



            '// Starting to get users list to add child

            '// Creating notification detail object list
            Dim List As New List(Of NotificationDetail)

            '// Getting users list
            List = NDal.GetNotificationUsers("Sales Inquiry Assigned")

            '// Adding users list in the Notification object of current inquiry
            objMod.Notification.NotificationDetils.AddRange(List)

            '// Getting and adding user groups list in the Notification object of current inquiry
            objMod.Notification.NotificationDetils.AddRange(NDal.GetNotificationGroups("Sales Inquiry Assigned"))

            '// Not getting role list because no role is associated at the moment
            '// We will need this in future and we can use it later
            '// We can consult to Update function of this class
            objMod.Notification.NotificationDetils.AddRange(UserEventRolesAssigned)
            objMod.Notification.NotificationDetils.AddRange(GroupEventRolesAssigned)

            '// ***This segment will be used to save notification in database table

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

            '// End Adding Notification
            ' *** End Segment ***   

            trans.Commit()
            'For Each InquiryRightsId As Integer In InquiryRightsIdsList
            Dim ValueTable As DataTable = GetOne(SalesInquiryRightsId)
            SaveAndSendNotification("Assigning Of Inquiries", "SalesInquiryRights", SalesInquiryRightsId, ValueTable, "Sales > Sales Inquiry", objMod)
            'Next
            Return True
        Catch ex As Exception
            If Not trans Is Nothing AndAlso Not trans.Connection Is Nothing Then
                trans.Rollback()
            End If

            Throw ex

        Finally

            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then

                Con.Close()

            End If

        End Try
    End Function
    Public Function AddSingle(ByVal objMod As SalesInquiryRights) As Integer
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim SalesInquiryRightsId As Integer = 0I
        Dim InquiryRightsIdsList As New List(Of Integer)
        Try
            'For Each objMod As SalesInquiryRights In objList
            Dim strSQL As String = String.Empty
            strSQL = " If Not exists(Select SalesInquiryRightsId From SalesInquiryRights Where SalesInquiryRightsId =" & objMod.SalesInquiryRightsId & " And GroupId = " & objMod.GroupId & ") INSERT INTO SalesInquiryRights(GroupId, SalesInquiryDetailId, SalesInquiryId, Rights, UserName, Status, Qty, PurchasedQty, UserId, VendorId, IsPurchaseInquiry, RequirementDescription) " _
            & " VALUES(" & objMod.GroupId & ", " & objMod.SalesInquiryDetailId & ", " & objMod.SalesInquiryId & ", " & IIf(objMod.Rights = True, 1, 0) & ", '" & objMod.UserName.Replace("'", "''") & "', '" & objMod.Status.Replace("'", "''") & "', " & objMod.Qty & ", " & objMod.PurchasedQty & " , " & objMod.UserId & ", " & objMod.VendorId & ", " & IIf(objMod.IsPurchaseInquiry = True, 1, 0) & ", '" & objMod.RequirementDescription.Replace("'", "''") & "') Else Update SalesInquiryRights Set GroupId =" & objMod.GroupId & ", SalesInquiryDetailId = " & objMod.SalesInquiryDetailId & ", SalesInquiryId =" & objMod.SalesInquiryId & ", Rights = " & IIf(objMod.Rights = True, 1, 0) & ", UserName= '" & objMod.UserName.Replace("'", "''") & "', Status= '" & objMod.Status.Replace("'", "''") & "', Qty= " & objMod.Qty & ", PurchasedQty= " & objMod.PurchasedQty & ", UserId= " & objMod.UserId & ", VendorId= " & objMod.VendorId & ", IsPurchaseInquiry = " & IIf(objMod.IsPurchaseInquiry = True, 1, 0) & ", RequirementDescription = '" & objMod.RequirementDescription.Replace("'", "''") & "'   Where SalesInquiryRightsId =" & objMod.SalesInquiryRightsId & " Select @@IDENTITY"
            SalesInquiryRightsId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            If SalesInquiryRightsId > 0 Then
                objMod.SalesInquiryRightsId = SalesInquiryRightsId
                AddGroups(objMod, trans)
                AddUsers(objMod, trans)
            Else
                DeleteGroups(objMod.SalesInquiryRightsId, trans)
                DeleteUsers(objMod.SalesInquiryRightsId, trans)
                AddGroups(objMod, trans)
                AddUsers(objMod, trans)
            End If
            'Next
            trans.Commit()
            'For Each InquiryRightsId As Integer In InquiryRightsIdsList
            Dim ValueTable As DataTable = GetOne(SalesInquiryRightsId)
            SaveAndSendNotification("Assigning Of Inquiries", "SalesInquiryRights", SalesInquiryRightsId, ValueTable, "Sales > Sales Inquiry", objMod)
            'Next
            Return SalesInquiryRightsId
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Delete(ByVal SalesInquiryRightsId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From SalesInquiryRights WHERE SalesInquiryRightsId=" & SalesInquiryRightsId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            DeleteGroups(SalesInquiryRightsId, trans)
            DeleteUsers(SalesInquiryRightsId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function DeleteGroups(ByVal SalesInquiryRightsId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From SalesInquiryRightsGroups WHERE SalesInquiryRightsId=" & SalesInquiryRightsId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'trans.Commit()
            Return True
        Catch ex As Exception
            ' trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function DeleteUsers(ByVal SalesInquiryRightsId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From SalesInquiryRightsUsers WHERE SalesInquiryRightsId=" & SalesInquiryRightsId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'trans.Commit()
            Return True
        Catch ex As Exception
            ' trans.Rollback()
            Throw ex
        Finally
            'Con.Close()
        End Try
    End Function
    Public Function AddGroups(ByVal Obj As SalesInquiryRights, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            For Each group As SalesInquiryRightsGroups In Obj.Groups
                Dim strSQL As String = String.Empty
                strSQL = "Insert Into SalesInquiryRightsGroups(SalesInquiryRightsId, GroupId) Values(" & Obj.SalesInquiryRightsId & ", " & group.GroupId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            Return True
        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function
    Public Function AddUsers(ByVal Obj As SalesInquiryRights, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            For Each user As SalesInquiryRightsUsers In Obj.Users
                Dim strSQL As String = String.Empty
                strSQL = "Insert Into SalesInquiryRightsUsers(SalesInquiryRightsId, UserId) Values(" & Obj.SalesInquiryRightsId & ", " & user.UserId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            'trans.Commit()
            Return True
        Catch ex As Exception
            'trans.Rollback()
            Throw ex
        Finally
            ' Con.Close()
        End Try
    End Function
    Public Function UpdateRequirementDescription(ByVal SalesInquiryDetailId As Integer, ByVal RequirementDescription As String) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE SalesInquiryDetail Set InternalDescription = N'" & RequirementDescription.Replace("'", "''") & "', InternalDescriptionChanged=1 WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & ""
            'strSQL = "UPDATE SalesInquiryRights Set RequirementDescription = N'" & RequirementDescription.Replace("'", "''") & "' WHERE SalesInquiryDetailId=" & SalesInquiryDetailId & ""
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
    Public Function GetAllRecords() As DataTable
        Try
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT SalesInquiry.SalesInquiryId, SalesInquiry.SalesInquiryNo, SalesInquiry.SalesInquiryDate, IsNull(COA.coa_detail_id,0) as CustomerId, COA.detail_code As Code, COA.detail_title As Customer, SalesInquiry.LocationId, SalesInquiry.ContactPersonId, SalesInquiry.CustomerInquiryNo, SalesInquiry.IndentNo, SalesInquiry.IndentDepartment, SalesInquiry.CustomerInquiryDate, SalesInquiry.OldInquiryNo, SalesInquiry.DueDate, SalesInquiry.OldInquiryDate, SalesInquiry.Remarks FROM dbo.SalesInquiryMaster AS SalesInquiry LEFT OUTER JOIN  dbo.vwCOADetail AS COA ON SalesInquiry.CustomerId = COA.coa_detail_id Order by SalesInquiry.SalesInquiryDate DESC")
            dt.AcceptChanges()

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetOne(ByVal SalesInquiryRightsId As Integer) As DataTable
        Try
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT SalesInquiry.SalesInquiryId, SalesInquiry.SalesInquiryNo, SalesInquiry.SalesInquiryDate, IsNull(COA.coa_detail_id,0) as CustomerId, COA.detail_code As Code, COA.detail_title As Customer, SalesInquiry.LocationId, SalesInquiry.ContactPersonId, SalesInquiry.CustomerInquiryNo, SalesInquiry.IndentNo, SalesInquiry.IndentDepartment, SalesInquiry.CustomerInquiryDate, SalesInquiry.OldInquiryNo, SalesInquiry.DueDate, SalesInquiry.OldInquiryDate, SalesInquiry.Remarks, SalesInquiryRights.SalesInquiryRightsId, SalesInquiryRights.SalesInquiryDetailId, SalesInquiryRights.Qty, IsNull(SalesInquiryRights.PurchasedQty, 0) PurchasedQty, SalesInquiryRights.Status, SalesInquiryRights.UserName, SalesInquiryRights.GroupId FROM dbo.SalesInquiryMaster AS SalesInquiry INNER JOIN SalesInquiryRights ON SalesInquiry.SalesInquiryId = SalesInquiryRights.SalesInquiryId LEFT OUTER JOIN  dbo.vwCOADetail AS COA ON SalesInquiry.CustomerId = COA.coa_detail_id Where SalesInquiryRights.SalesInquiryRightsId =" & SalesInquiryRightsId & "")
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
    Public Function GetAgainstSearch(ByVal CustomerId As Integer, ByVal SalesInquiryId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Try
            Dim dt As New DataTable
            'Dim strSQL As String = "SELECT 0 As SalesInquiryRightsId, Convert(Bit, 0) As Rights, Detail.SalesInquiryDetailId, SalesInquiryMaster.CustomerInquiryNo, SalesInquiryMaster.SalesInquiryNo, Detail.SalesInquiryId, Detail.SerialNo, Detail.RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull( Detail.Qty, 0)-IsNull( Detail.PurchasedQty, 0)) As Qty, Convert(float, 0) As PurchasedQty,  Detail.Comments,  0 As GroupId, '' As Status, 0 As UserId, 0 As VendorId, 0 As IsPurchaseInquiry FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId   LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where IsNull(Detail.Qty, 0)>IsNull( Detail.PurchasedQty, 0) And Detail.SalesInquiryDetailId Not In (Select SalesInquiryDetailId From SalesInquiryRights Where Rights = 1)"

            '// Rai Shahid  15-May-2017
            '// Commented to replace customer description column with internal description
            'Dim strSQL As String = "SELECT 0 As SalesInquiryRightsId, Convert(Bit, 0) As Rights, Detail.SalesInquiryDetailId, SalesInquiryMaster.CustomerInquiryNo, SalesInquiryMaster.SalesInquiryNo, vwCOADetail.detail_title, Detail.SalesInquiryId, Detail.SerialNo, Detail.RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull( Detail.Qty, 0)-IsNull( Detail.PurchasedQty, 0)) As Qty, Convert(float, 0) As PurchasedQty,  Detail.Comments,  0 As GroupId, '' As Status, 0 As UserId, 0 As VendorId, 0 As IsPurchaseInquiry FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId  INNER JOIN vwCOADetail ON SalesInquiryMaster.CustomerId = vwCOADetail.coa_detail_id  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where IsNull(Detail.Qty, 0)>IsNull( Detail.PurchasedQty, 0) And Detail.SalesInquiryDetailId Not In (Select SalesInquiryDetailId From SalesInquiryRights Where Rights = 1)"

            '// Replaced customer description column with internal description
            Dim strSQL As String = "SELECT 0 As SalesInquiryRightsId, Convert(Bit, 0) As Rights, Detail.SalesInquiryDetailId, SalesInquiryMaster.CustomerInquiryNo, SalesInquiryMaster.SalesInquiryNo, vwCOADetail.detail_title, Detail.SalesInquiryId, Detail.SerialNo, Detail.InternalDescription as RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull( Detail.Qty, 0)-IsNull( Detail.PurchasedQty, 0)) As Qty, Convert(float, 0) As PurchasedQty,  Detail.Comments,  0 As GroupId, '' As Status, 0 As UserId, 0 As VendorId, 0 As IsPurchaseInquiry FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId  INNER JOIN vwCOADetail ON SalesInquiryMaster.CustomerId = vwCOADetail.coa_detail_id  LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where IsNull(Detail.Qty, 0)>IsNull( Detail.PurchasedQty, 0) And Detail.SalesInquiryDetailId Not In (Select SalesInquiryDetailId From SalesInquiryRights Where Rights = 1)"
            If CustomerId > 0 Then
                strSQL += " And SalesInquiryMaster.CustomerId = " & CustomerId & ""
            End If
            If SalesInquiryId > 0 Then
                strSQL += " And SalesInquiryMaster.SalesInquiryId = " & SalesInquiryId & ""
            End If
            If Not FromDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate >= Convert(DateTime, '" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102)"
            End If
            If Not ToDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate <= Convert(DateTime, '" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)"
            End If
            strSQL += " order by SalesInquiryNo Desc, SerialNo asc "

            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAgainstGroups(ByVal Groups As String, ByVal CustomerId As Integer, ByVal SalesInquiryId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Try
            Dim dt As New DataTable
            'Dim strSQL As String = "SELECT SalesInquiryRights.Rights, Detail.SalesInquiryDetailId, Detail.SalesInquiryId, Detail.SerialNo, Detail.RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull(Detail.Qty, 0)-IsNull(Detail.PurchasedQty, 0)) As Qty,  Detail.Comments, SalesInquiryRights.SalesInquiryRightsId, SalesInquiryRights.GroupId, SalesInquiryRights.Status FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId Left Outer Join SalesInquiryRights ON Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where IsNull(Detail.Qty, 0)>IsNull(Detail.PurchasedQty, 0) And SalesInquiryRights.GroupId In (" & Groups & ") And SalesInquiryRights.Rights = 1"
            'Dim strSQL As String = "SELECT SalesInquiryRights.Rights, Detail.SalesInquiryDetailId, Detail.SalesInquiryId, SalesInquiryMaster.CustomerInquiryNo, SalesInquiryMaster.SalesInquiryNo, Detail.SerialNo, Detail.RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull(SalesInquiryRights.Qty, 0)-IsNull(SalesInquiryRights.PurchasedQty, 0)) As Qty, IsNull(SalesInquiryRights.PurchasedQty, 0) As PurchasedQty,  Detail.Comments, SalesInquiryRights.SalesInquiryRightsId, SalesInquiryRights.GroupId, SalesInquiryRights.Status, IsNull(SalesInquiryRights.UserId, 0) As UserId, IsNull(SalesInquiryRights.VendorId, 0) As VendorId, IsNull(SalesInquiryRights.IsPurchaseInquiry, 0) As IsPurchaseInquiry FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId Left Outer Join SalesInquiryRights ON Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where IsNull(SalesInquiryRights.Qty, 0)>IsNull(SalesInquiryRights.PurchasedQty, 0) And SalesInquiryRights.GroupId In (" & Groups & ") And SalesInquiryRights.Rights = 1"
            'Dim strSQL As String = "SELECT Distinct SalesInquiryRights.SalesInquiryRightsId, SalesInquiryRights.Rights, Detail.SalesInquiryDetailId, Detail.SalesInquiryId, SalesInquiryMaster.CustomerInquiryNo, SalesInquiryMaster.SalesInquiryNo, Detail.SerialNo, SalesInquiryRights.RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull(SalesInquiryRights.Qty, 0)-IsNull(SalesInquiryRights.PurchasedQty, 0)) As Qty, IsNull(SalesInquiryRights.PurchasedQty, 0) As PurchasedQty,  Detail.Comments, SalesInquiryRights.GroupId, SalesInquiryRights.Status, SalesInquiryRights.UserId, SalesInquiryRights.VendorId, IsNull(SalesInquiryRights.IsPurchaseInquiry, 0) As IsPurchaseInquiry FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId Left Outer Join SalesInquiryRights ON Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Left Outer Join SalesInquiryRightsGroups ON SalesInquiryRights.SalesInquiryRightsId = SalesInquiryRightsGroups.SalesInquiryRightsId Where IsNull(SalesInquiryRights.Qty, 0)>IsNull(SalesInquiryRights.PurchasedQty, 0) And SalesInquiryRightsGroups.GroupId In (" & Groups & ") And SalesInquiryRights.Rights = 1"
            'Ali Faisal : TFS1308 : Selected the QTY from SalesInquiryDetail table instead of SalesInquiryRights table on 16-Aug-2017
            'Dim strSQL As String = "SELECT DISTINCT	SalesInquiryRights.SalesInquiryRightsId, SalesInquiryRights.Rights, Detail.SalesInquiryDetailId, Detail.SalesInquiryId, SalesInquiryMaster.CustomerInquiryNo, SalesInquiryMaster.SalesInquiryNo, vwCOADetail.detail_title,  Detail.SerialNo, Detail.InternalDescription as RequirementDescription, Detail.ArticleId, Article.ArticleCode AS Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName AS Unit, Detail.ItemTypeId,  Type.ArticleTypeName AS Type, Detail.CategoryId, Category.ArticleCompanyName AS Category, Detail.SubCategoryId, SubCategory.ArticleLpoName AS SubCategory, Detail.OriginId,  Origin.ArticleGenderName AS Origin, ISNULL(SalesInquiryRights.Qty, 0) - ISNULL(SalesInquiryRights.PurchasedQty, 0) AS Qty, ISNULL(SalesInquiryRights.PurchasedQty, 0) AS PurchasedQty,   Detail.Comments, SalesInquiryRights.GroupId, SalesInquiryRights.Status, SalesInquiryRights.UserId, SalesInquiryRights.VendorId, ISNULL(SalesInquiryRights.IsPurchaseInquiry, 0)  AS IsPurchaseInquiry, tblUser.FULLNAME  FROM         vwCOADetail INNER JOIN SalesInquiryDetail AS Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId LEFT OUTER JOIN SalesInquiryRights LEFT OUTER JOIN SalesInquiryRightsUsers LEFT OUTER JOIN tblUser ON SalesInquiryRightsUsers.UserId = tblUser.User_ID ON SalesInquiryRights.SalesInquiryRightsId = SalesInquiryRightsUsers.SalesInquiryRightsId ON  Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId LEFT OUTER JOIN ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable AS Unit ON Detail.UnitId = Unit.ArticleUnitId LEFT OUTER JOIN ArticleTypeDefTable AS Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT OUTER JOIN ArticleCompanyDefTable AS Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT OUTER JOIN ArticleLpoDefTable AS SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT OUTER JOIN ArticleGenderDefTable AS Origin ON Detail.OriginId = Origin.ArticleGenderId LEFT OUTER JOIN SalesInquiryRightsGroups ON SalesInquiryRights.SalesInquiryRightsId = SalesInquiryRightsGroups.SalesInquiryRightsId   Where 1=1 "
            'Dim strSQL As String = "SELECT DISTINCT	SalesInquiryRights.SalesInquiryRightsId, SalesInquiryRights.Rights, Detail.SalesInquiryDetailId, Detail.SalesInquiryId, SalesInquiryMaster.CustomerInquiryNo, SalesInquiryMaster.SalesInquiryNo, vwCOADetail.detail_title,  Detail.SerialNo, Detail.InternalDescription as RequirementDescription, Detail.ArticleId, Article.ArticleCode AS Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName AS Unit, Detail.ItemTypeId,  Type.ArticleTypeName AS Type, Detail.CategoryId, Category.ArticleCompanyName AS Category, Detail.SubCategoryId, SubCategory.ArticleLpoName AS SubCategory, Detail.OriginId,  Origin.ArticleGenderName AS Origin, IsNull(Detail.Qty,0) AS Qty, ISNULL(SalesInquiryRights.PurchasedQty, 0) AS PurchasedQty, Detail.Comments, SalesInquiryRights.GroupId, SalesInquiryRights.Status, SalesInquiryRights.UserId, SalesInquiryRights.VendorId, ISNULL(SalesInquiryRights.IsPurchaseInquiry, 0)  AS IsPurchaseInquiry, tblUser.FULLNAME  FROM vwCOADetail INNER JOIN SalesInquiryDetail AS Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId LEFT OUTER JOIN SalesInquiryRights LEFT OUTER JOIN SalesInquiryRightsUsers LEFT OUTER JOIN tblUser ON SalesInquiryRightsUsers.UserId = tblUser.User_ID ON SalesInquiryRights.SalesInquiryRightsId = SalesInquiryRightsUsers.SalesInquiryRightsId ON  Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId LEFT OUTER JOIN ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable AS Unit ON Detail.UnitId = Unit.ArticleUnitId LEFT OUTER JOIN ArticleTypeDefTable AS Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT OUTER JOIN ArticleCompanyDefTable AS Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT OUTER JOIN ArticleLpoDefTable AS SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT OUTER JOIN ArticleGenderDefTable AS Origin ON Detail.OriginId = Origin.ArticleGenderId LEFT OUTER JOIN SalesInquiryRightsGroups ON SalesInquiryRights.SalesInquiryRightsId = SalesInquiryRightsGroups.SalesInquiryRightsId   Where 1=1 "
            Dim strSQL As String = "SELECT SalesInquiryRights.SalesInquiryRightsId, SalesInquiryRights.Rights, Detail.SalesInquiryDetailId, Detail.SalesInquiryId, SalesInquiryMaster.CustomerInquiryNo, SalesInquiryMaster.SalesInquiryNo, vwCOADetail.detail_title,  Detail.SerialNo, Detail.InternalDescription as RequirementDescription, Detail.ArticleId, Article.ArticleCode AS Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName AS Unit, Detail.ItemTypeId,  Type.ArticleTypeName AS Type, Detail.CategoryId, Category.ArticleCompanyName AS Category, Detail.SubCategoryId, SubCategory.ArticleLpoName AS SubCategory, Detail.OriginId,  Origin.ArticleGenderName AS Origin, IsNull(Detail.Qty,0) AS Qty, ISNULL(SalesInquiryRights.PurchasedQty, 0) AS PurchasedQty, Detail.Comments, SalesInquiryRights.GroupId, SalesInquiryRights.Status, SalesInquiryRights.UserId, SalesInquiryRights.VendorId, ISNULL(SalesInquiryRights.IsPurchaseInquiry, 0)  AS IsPurchaseInquiry, tblUser.FULLNAME  FROM vwCOADetail INNER JOIN SalesInquiryDetail AS Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId LEFT OUTER JOIN SalesInquiryRights LEFT OUTER JOIN SalesInquiryRightsUsers LEFT OUTER JOIN tblUser ON SalesInquiryRightsUsers.UserId = tblUser.User_ID ON SalesInquiryRights.SalesInquiryRightsId = SalesInquiryRightsUsers.SalesInquiryRightsId ON  Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId LEFT OUTER JOIN ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT OUTER JOIN ArticleUnitDefTable AS Unit ON Detail.UnitId = Unit.ArticleUnitId LEFT OUTER JOIN ArticleTypeDefTable AS Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT OUTER JOIN ArticleCompanyDefTable AS Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT OUTER JOIN ArticleLpoDefTable AS SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT OUTER JOIN ArticleGenderDefTable AS Origin ON Detail.OriginId = Origin.ArticleGenderId LEFT OUTER JOIN SalesInquiryRightsGroups ON SalesInquiryRights.SalesInquiryRightsId = SalesInquiryRightsGroups.SalesInquiryRightsId Where 1=1  "      'AND SalesInquiryRights.Status IS NULL
            'Ali Faisal : TFS1308 : End

            'If Groups.ToString.Trim.Length > 0 Then

            '    strSQL += " And SalesInquiryRightsGroups.GroupId In (" & Groups & ") And SalesInquiryRights.Rights = 1 "

            'End If

            If CustomerId > 0 Then
                strSQL += " And SalesInquiryMaster.CustomerId = " & CustomerId & ""
            End If
            If SalesInquiryId > 0 Then
                strSQL += " And SalesInquiryMaster.SalesInquiryId = " & SalesInquiryId & ""
            End If
            If Not FromDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate >= Convert(DateTime, '" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102)"
            End If
            If Not ToDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate <= Convert(DateTime, '" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)"
            End If
            strSQL += " order by SalesInquiryNo Desc, CAST(Detail.SerialNo AS Numeric(10,0)) asc "
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt

        Catch ex As Exception
            Throw ex

        End Try


    End Function

    Public Function GetAssigned(ByVal CustomerId As Integer, ByVal SalesInquiryId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Try
            Dim dt As New DataTable
            Dim strSQL As String = "SELECT DISTINCT VendorQuotationDetail.SerialNo, SalesInquiryMaster.CustomerInquiryNo, SalesInquiryMaster.SalesInquiryId, SalesInquiryMaster.SalesInquiryNo, vwCOADetail.detail_title, PurchaseInquiryMaster.PurchaseInquiryId, PurchaseInquiryMaster.PurchaseInquiryNo, VendorQuotationMaster.VendorQuotationId, VendorQuotationMaster.VendorQuotationNo, vwCOADetail_1.detail_title AS VendorName, VendorQuotationDetail.VendorQuotationDetailId, VendorQuotationDetail.ArticleId, Article.ArticleCode AS Code, Article.ArticleDescription, CASE WHEN VendorQuotationDetail.RequirementDescription = '' THEN SalesInquiryDetail.InternalDescription ELSE VendorQuotationDetail.RequirementDescription END AS RequirementDescription, VendorQuotationDetail.UnitId, Unit.ArticleUnitName AS Unit, VendorQuotationDetail.ItemTypeId AS TypeId, Type.ArticleTypeName AS Type, VendorQuotationDetail.CategoryId, Category.ArticleCompanyName AS Category, VendorQuotationDetail.SubCategoryId, SubCategory.ArticleLpoName AS SubCategory, VendorQuotationDetail.OriginId AS OrigionId, Origin.ArticleGenderName AS Origion, VendorQuotationDetail.Qty FROM vwCOADetail INNER JOIN SalesInquiryMaster AS SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId INNER JOIN PurchaseInquiryMaster ON SalesInquiryMaster.SalesInquiryId = PurchaseInquiryMaster.SalesInquiryId INNER JOIN VendorQuotationMaster ON PurchaseInquiryMaster.PurchaseInquiryId = VendorQuotationMaster.PurchaseInquiryId INNER JOIN VendorQuotationDetail ON VendorQuotationMaster.VendorQuotationId = VendorQuotationDetail.VendorQuotationId INNER JOIN vwCOADetail AS vwCOADetail_1 ON VendorQuotationMaster.VendorId = vwCOADetail_1.coa_detail_id INNER JOIN SalesInquiryDetail ON SalesInquiryMaster.SalesInquiryId = SalesInquiryDetail.SalesInquiryId LEFT OUTER JOIN ArticleLpoDefTable AS SubCategory ON VendorQuotationDetail.SubCategoryId = SubCategory.ArticleLpoId LEFT OUTER JOIN ArticleGenderDefTable AS Origin ON VendorQuotationDetail.OriginId = Origin.ArticleGenderId LEFT OUTER JOIN ArticleCompanyDefTable AS Category ON VendorQuotationDetail.CategoryId = Category.ArticleCompanyId LEFT OUTER JOIN ArticleUnitDefTable AS Unit ON VendorQuotationDetail.UnitId = Unit.ArticleUnitId LEFT OUTER JOIN ArticleDefTable AS Article ON VendorQuotationDetail.ArticleId = Article.ArticleId LEFT OUTER JOIN ArticleTypeDefTable AS Type ON VendorQuotationDetail.ItemTypeId = Type.ArticleTypeId WHERE VendorQuotationDetail.StatusId = 1"
            If CustomerId > 0 Then
                strSQL += " And SalesInquiryMaster.CustomerId = " & CustomerId & ""
            End If
            If SalesInquiryId > 0 Then
                strSQL += " And SalesInquiryMaster.SalesInquiryId = " & SalesInquiryId & ""
            End If
            If Not FromDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate >= Convert(DateTime, '" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102)"
            End If
            If Not ToDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate <= Convert(DateTime, '" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)"
            End If
            strSQL += " order by SalesInquiryNo Desc, VendorQuotationDetail.SerialNo asc "
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetPending(ByVal CustomerId As Integer, ByVal SalesInquiryId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Try
            Dim dt As New DataTable
            Dim strSQL As String = "SELECT DISTINCT VendorQuotationDetail.SerialNo, SalesInquiryMaster.CustomerInquiryNo, SalesInquiryMaster.SalesInquiryId, SalesInquiryMaster.SalesInquiryNo, vwCOADetail.detail_title, PurchaseInquiryMaster.PurchaseInquiryId, PurchaseInquiryMaster.PurchaseInquiryNo, VendorQuotationMaster.VendorQuotationId, VendorQuotationMaster.VendorQuotationNo, vwCOADetail_1.detail_title AS VendorName, VendorQuotationDetail.VendorQuotationDetailId, VendorQuotationDetail.ArticleId, Article.ArticleCode AS Code, Article.ArticleDescription, VendorQuotationDetail.RequirementDescription, VendorQuotationDetail.UnitId, Unit.ArticleUnitName AS Unit, VendorQuotationDetail.ItemTypeId AS TypeId, Type.ArticleTypeName AS Type, VendorQuotationDetail.CategoryId, Category.ArticleCompanyName AS Category, VendorQuotationDetail.SubCategoryId, SubCategory.ArticleLpoName AS SubCategory, VendorQuotationDetail.OriginId AS OrigionId, Origin.ArticleGenderName AS Origion, VendorQuotationDetail.Qty FROM vwCOADetail INNER JOIN SalesInquiryMaster AS SalesInquiryMaster ON vwCOADetail.coa_detail_id = SalesInquiryMaster.CustomerId INNER JOIN PurchaseInquiryMaster ON SalesInquiryMaster.SalesInquiryId = PurchaseInquiryMaster.SalesInquiryId INNER JOIN VendorQuotationMaster ON PurchaseInquiryMaster.PurchaseInquiryId = VendorQuotationMaster.PurchaseInquiryId INNER JOIN VendorQuotationDetail ON VendorQuotationMaster.VendorQuotationId = VendorQuotationDetail.VendorQuotationId INNER JOIN vwCOADetail AS vwCOADetail_1 ON VendorQuotationMaster.VendorId = vwCOADetail_1.coa_detail_id LEFT OUTER JOIN ArticleLpoDefTable AS SubCategory ON VendorQuotationDetail.SubCategoryId = SubCategory.ArticleLpoId LEFT OUTER JOIN ArticleGenderDefTable AS Origin ON VendorQuotationDetail.OriginId = Origin.ArticleGenderId LEFT OUTER JOIN ArticleCompanyDefTable AS Category ON VendorQuotationDetail.CategoryId = Category.ArticleCompanyId LEFT OUTER JOIN ArticleUnitDefTable AS Unit ON VendorQuotationDetail.UnitId = Unit.ArticleUnitId LEFT OUTER JOIN ArticleDefTable AS Article ON VendorQuotationDetail.ArticleId = Article.ArticleId LEFT OUTER JOIN ArticleTypeDefTable AS Type ON VendorQuotationDetail.ItemTypeId = Type.ArticleTypeId WHERE VendorQuotationDetail.StatusId = 2"
            If CustomerId > 0 Then
                strSQL += " And SalesInquiryMaster.CustomerId = " & CustomerId & ""
            End If
            If SalesInquiryId > 0 Then
                strSQL += " And SalesInquiryMaster.SalesInquiryId = " & SalesInquiryId & ""
            End If
            If Not FromDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate >= Convert(DateTime, '" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102)"
            End If
            If Not ToDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate <= Convert(DateTime, '" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)"
            End If
            strSQL += " order by SalesInquiryNo Desc, VendorQuotationDetail.SerialNo asc "
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetApproved(ByVal CustomerId As Integer, ByVal SalesInquiryId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
        Try
            Dim dt As New DataTable
            Dim strSQL As String = "SELECT VendorQuotationDetail.SerialNo, SalesInquiryMaster.CustomerInquiryNo, SalesInquiryMaster.SalesInquiryId, SalesInquiryMaster.SalesInquiryNo, PurchaseInquiryMaster.PurchaseInquiryNo,  VendorQuotationMaster.VendorQuotationNo, InquiryComparisonStatement.RequirementDescription, InquiryComparisonStatement.NetCostValue, InquiryComparisonStatement.CurrencySymbol, InquiryComparisonStatement.Margin, InquiryComparisonStatement.Qty, InquiryComparisonStatement.Comments, VendorQuotationDetail.QuotedTerms, VendorQuotationDetail.ValidityOfQuotation, VendorQuotationDetail.DeliveryPeriod, VendorQuotationDetail.ApproxGrossWeight, VendorQuotationDetail.GenuineOrReplacement, VendorQuotationDetail.LiteratureOrDatasheet, VendorQuotationDetail.NewOrRefurbish FROM SalesInquiryMaster INNER JOIN PurchaseInquiryMaster ON SalesInquiryMaster.SalesInquiryId = PurchaseInquiryMaster.SalesInquiryId INNER JOIN VendorQuotationMaster ON PurchaseInquiryMaster.PurchaseInquiryId = VendorQuotationMaster.PurchaseInquiryId INNER JOIN VendorQuotationDetail ON VendorQuotationMaster.VendorQuotationId = VendorQuotationDetail.VendorQuotationId INNER JOIN InquiryComparisonStatement ON VendorQuotationDetail.VendorQuotationDetailId = InquiryComparisonStatement.VendorQuotationDetailId WHERE VendorQuotationDetail.StatusId = 3"
            If CustomerId > 0 Then
                strSQL += " And SalesInquiryMaster.CustomerId = " & CustomerId & ""
            End If
            If SalesInquiryId > 0 Then
                strSQL += " And SalesInquiryMaster.SalesInquiryId = " & SalesInquiryId & ""
            End If
            If Not FromDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate >= Convert(DateTime, '" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102)"
            End If
            If Not ToDate = DateTime.MinValue Then
                strSQL += " And SalesInquiryMaster.SalesInquiryDate <= Convert(DateTime, '" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)"
            End If
            strSQL += " order by SalesInquiryMaster.SalesInquiryNo Desc, VendorQuotationDetail.SerialNo asc "
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAgainstGroup(ByVal GroupId As Integer) As DataTable
        Try
            Dim dt As New DataTable
            Dim strSQL As String = "SELECT SalesInquiryRights.SalesInquiryRightsId, SalesInquiryRights.Rights, Detail.SalesInquiryDetailId, Detail.SalesInquiryId, Detail.SerialNo, SalesInquiryRights.RequirementDescription, Detail.ArticleId, Article.ArticleCode As Code, Article.ArticleDescription, Detail.UnitId, Unit.ArticleUnitName As Unit, Detail.ItemTypeId, Type.ArticleTypeName As Type,  Detail.CategoryId, Category.ArticleCompanyName As Category,  Detail.SubCategoryId, SubCategory.ArticleLpoName As SubCategory,  Detail.OriginId, Origin.ArticleGenderName As Origin, (IsNull(Detail.Qty, 0)-IsNull(Detail.PurchasedQty, 0)) As Qty,  Detail.Comments, SalesInquiryRights.GroupId FROM SalesInquiryDetail As Detail INNER JOIN SalesInquiryMaster ON Detail.SalesInquiryId = SalesInquiryMaster.SalesInquiryId Left Outer Join SalesInquiryRights ON Detail.SalesInquiryDetailId = SalesInquiryRights.SalesInquiryDetailId LEFT OUTER JOIN  ArticleDefTable AS Article ON Detail.ArticleId = Article.ArticleId LEFT JOIN ArticleUnitDefTable As Unit On Detail.UnitId = Unit.ArticleUnitId LEFT JOIN ArticleTypeDefTable As Type ON Detail.ItemTypeId = Type.ArticleTypeId LEFT JOIN ArticleCompanyDefTable As Category ON Detail.CategoryId = Category.ArticleCompanyId LEFT JOIN ArticleLpoDefTable As SubCategory ON Detail.SubCategoryId = SubCategory.ArticleLpoId LEFT JOIN ArticleGenderDefTable As Origin ON Detail.OriginId = Origin.ArticleGenderId Where IsNull(Detail.Qty, 0)>IsNull(Detail.PurchasedQty, 0) And SalesInquiryRights.GroupId =" & GroupId & " And SalesInquiryRights.Rights=1"

            'If CustomerId > 0 Then
            '    strSQL += " And SalesInquiryMaster.CustomerId = " & CustomerId & ""
            'End If
            'If SalesInquiryId > 0 Then
            '    strSQL += " And SalesInquiryMaster.SalesInquiryId = " & SalesInquiryId & ""
            'End If
            'If Not FromDate = DateTime.MinValue Then
            '    strSQL += " And SalesInquiryMaster.SalesInquiryDate >= Convert(DateTime, '" & FromDate.ToString("yyyy-M-dd 00:00:00") & "', 102)"
            'End If
            'If Not ToDate = DateTime.MinValue Then
            '    strSQL += " And SalesInquiryMaster.SalesInquiryDate <= Convert(DateTime, '" & ToDate.ToString("yyyy-M-dd 23:59:59") & "', 102)"
            'End If
            dt = UtilityDAL.GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsAssignedAlready(ByVal GroupId As Integer, ByVal SalesInquiryDetailId As Integer, Optional ByVal IsRightTrue As Boolean = True) As Boolean
        Try
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT SalesInquiryRightsId FROM dbo.SalesInquiryRights Where GroupId =" & GroupId & " And SalesInquiryDetailId = " & SalesInquiryDetailId & " And Rights ='" & IsRightTrue & "'")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsPurchaseInquiry(ByVal SalesInquiryDetailId As Integer) As Boolean
        Try
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT IsPurchaseInquiry FROM dbo.SalesInquiryRights Where SalesInquiryDetailId = " & SalesInquiryDetailId & "")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0).ToString = "True" Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetVendorEmail(ByVal VendorId As Integer) As String
        Try
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT Email FROM dbo.tblVendor Where VendorID = " & VendorId & "")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item(0).ToString()
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetGroups(ByVal SalesInquiryRightsId As Integer)
        Try
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT GroupId FROM dbo.SalesInquiryRightsGroups Where SalesInquiryRightsId =" & SalesInquiryRightsId & " ")
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUsers(ByVal SalesInquiryRightsId As Integer)
        Try
            'SalesInquiryNo, SalesInquiryDate, CustomerId, LocationId, ContactPersonId, CustomerInquiryNo, IndentNo, IndentDepartment, CustomerInquiryDate, OldInquiryNo, DueDate, OldInquiryDate, Remarks
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT UserId FROM dbo.SalesInquiryRightsUsers Where SalesInquiryRightsId =" & SalesInquiryRightsId & " ")
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SaveAndSendNotification(ByVal Subject As String, ByVal TableName As String, ByVal ApplicationReference As Integer, ByVal ValueTable As DataTable, ByVal SourceApplication As String, ByVal obj As SalesInquiryRights) As Boolean
        Dim UserList As New List(Of Integer)
        Dim Template As String = ""
        Try
            If ValueTable.Rows.Count > 0 Then
                Dim dtTemplate As DataTable = NTemplatesDAL.GetTemplate(Subject, TableName)
                If dtTemplate.Rows.Count > 0 Then
                    'For Each group As SalesInquiryRightsGroups In Obj.Groups
                    '    Dim dtUsers As DataTable = NTemplatesDAL.GetGroupUsers(Val(group.GroupId.ToString))
                    '    If dtUsers.Rows.Count > 0 Then
                    '        For Each dr1 As DataRow In dtUsers.Rows
                    '            UserList.Add(Val(dr1.Item("User_ID").ToString))
                    '        Next
                    '    End If
                    'Next
                    For Each user As SalesInquiryRightsUsers In Obj.Users
                        'For Each dr As DataRow In dtTemplateUsers.Rows
                        UserList.Add(user.UserId)
                        'Next
                    Next
                    'Dim dtTemplateGroups As DataTable = GetGroups(Val(dtTemplate.Rows(0).Item("NotificationTemplatesId").ToString))
                    'If dtTemplateGroups.Rows.Count > 0 Then
                    '    For Each dr As DataRow In dtTemplateGroups.Rows
                    '        Dim dtUsers As DataTable = GetGroupUsers(Val(dr.Item("GroupId").ToString))
                    '        If dtUsers.Rows.Count > 0 Then
                    '            For Each dr1 As DataRow In dtUsers.Rows
                    '                UserList.Add(Val(dr1.Item("User_ID").ToString))
                    '            Next
                    '        End If
                    '    Next
                    'End If
                    'Dim dtTemplateUsers As DataTable = GetUsers(Val(dtTemplate.Rows(0).Item("NotificationTemplatesId").ToString))
                    'If dtTemplateUsers.Rows.Count > 0 Then
                    '    For Each dr As DataRow In dtTemplateUsers.Rows
                    '        UserList.Add(dr.Item("UserId").ToString)
                    '    Next
                    'End If

                    ' Adding notifications
                    If UserList.Count > 0 Then
                        Template = dtTemplate.Rows(0).Item("Template").ToString
                        'Dim dtColumns As DataTable = GetColumns(TableName)
                        If ValueTable.Rows.Count > 0 Then
                            For Each column As DataColumn In ValueTable.Columns
                                If Template.Contains("@" & column.ColumnName) Then
                                    Template = Template.Replace("@" & column.ColumnName, ValueTable.Rows(0).Item(column.ColumnName).ToString)
                                End If
                            Next
                        End If
                        Dim Notification As New AgriusNotifications
                        Notification.NotificationTitle = dtTemplate.Rows(0).Item("Subject").ToString
                        Notification.NotificationDescription = Template
                        Notification.SourceApplication = SourceApplication
                        Notification.ApplicationReference = ApplicationReference
                        Dim objDList As New List(Of NotificationDetail)
                        For Each user As Integer In UserList
                            objDList.Add(New NotificationDetail(New SecurityUser(user)))
                        Next
                        Notification.NotificationDetils = objDList
                        Dim Dal As New NotificationDAL
                        Dim list As New List(Of AgriusNotifications)
                        list.Add(Notification)
                        Dal.AddNotification(list)
                    End If
                    ' End 
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateVendorQuotationDetail(ByVal VendorQuotationDetailId As Integer, ByVal StatusId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE VendorQuotationDetail SET StatusId = " & StatusId & " WHERE VendorQuotationDetailId = " & VendorQuotationDetailId & ""
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

    Public Shared Function GetInquiryUsers(ByVal SalesInquiryId As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            Query = " SELECT DISTINCT UserId, UserName FROM (SELECT DISTINCT User_ID AS UserId, User_Name AS UserName FROM tblUser WHERE User_ID IN (SELECT DISTINCT SalesInquiryRightsUsers.UserId FROM SalesInquiryRightsUsers INNER JOIN SalesInquiryRights ON SalesInquiryRightsUsers.SalesInquiryRightsId = SalesInquiryRights.SalesInquiryRightsId  WHERE SalesInquiryRights.SalesInquiryId = " & SalesInquiryId & " ) " _
                   & " UNION ALL SELECT DISTINCT User_ID AS UserId, User_Name AS UserName FROM tblUser WHERE GroupId IN (SELECT DISTINCT SalesInquiryRightsGroups.GroupId FROM SalesInquiryRightsGroups INNER JOIN SalesInquiryRights ON SalesInquiryRightsGroups.SalesInquiryRightsId = SalesInquiryRights.SalesInquiryRightsId WHERE SalesInquiryRights.SalesInquiryId = " & SalesInquiryId & " )) AS Users "
            Dim dtUsers As DataTable = UtilityDAL.GetDataTable(Query)
            For Each dr As DataRow In dtUsers.Rows
                dr.BeginEdit()
                dr.Item("UserName") = UtilityDAL.Decrypt(dr.Item("UserName"))
                dr.EndEdit()
            Next
            dtUsers.AcceptChanges()
            Return dtUsers
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetInquiryUsers2(ByVal SalesInquiryRightsId As Integer) As DataTable
        Dim Query As String = String.Empty
        Try
            Query = " SELECT DISTINCT UserId, UserName FROM (SELECT DISTINCT User_ID AS UserId, User_Name AS UserName FROM tblUser WHERE User_ID IN (SELECT DISTINCT SalesInquiryRightsUsers.UserId FROM SalesInquiryRightsUsers INNER JOIN SalesInquiryRights ON SalesInquiryRightsUsers.SalesInquiryRightsId = SalesInquiryRights.SalesInquiryRightsId  WHERE SalesInquiryRights.SalesInquiryRightsId = " & SalesInquiryRightsId & " ) " _
                   & " UNION ALL SELECT DISTINCT User_ID AS UserId, User_Name AS UserName FROM tblUser WHERE GroupId IN (SELECT DISTINCT SalesInquiryRightsGroups.GroupId FROM SalesInquiryRightsGroups INNER JOIN SalesInquiryRights ON SalesInquiryRightsGroups.SalesInquiryRightsId = SalesInquiryRights.SalesInquiryRightsId WHERE SalesInquiryRights.SalesInquiryRightsId = " & SalesInquiryRightsId & " )) AS Users "
            Dim dtUsers As DataTable = UtilityDAL.GetDataTable(Query)
            For Each dr As DataRow In dtUsers.Rows
                dr.BeginEdit()
                dr.Item("UserName") = UtilityDAL.Decrypt(dr.Item("UserName"))
                dr.EndEdit()
            Next
            dtUsers.AcceptChanges()
            Return dtUsers
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetUsersEmail(ByVal GroupId As Integer) As DataTable
        Try
            Dim QUERY As String = "SELECT User_ID, Email FROM tblUser WHERE GroupId = " & GroupId & ""
            Dim DT As DataTable = UtilityDAL.GetDataTable(QUERY)
            Return DT
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

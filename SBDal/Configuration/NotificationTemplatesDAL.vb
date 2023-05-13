Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Net
Imports System.Drawing
Imports System.IO
Public Class NotificationTemplatesDAL
    Public Function Add(ByVal template As NotificationTemplates) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        '        NotificationTemplatesId	int	Unchecked
        'Subject	nvarchar(100)	Checked
        'Template	nvarchar(1500)	Checked
        'TemplateDate	datetime	Checked
        'TableName	nvarchar(100)	Checked
        '        Unchecked()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "If Not Exists(Select NotificationTemplatesId From NotificationTemplates Where NotificationTemplatesId =" & template.NotificationTemplatesId & " And Subject = '" & template.Subject.Replace("'", "''") & "') INSERT INTO NotificationTemplates(Subject, Template, TemplateDate, TableName) " _
            & " VALUES(N'" & template.Subject.Replace("'", "''") & "', N'" & template.Template.Replace("'", "''") & "',  '" & template.TemplateDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', N'" & template.TableName.Replace("'", "''") & "') " _
            & " Else Update NotificationTemplates Set Subject = N'" & template.Subject.Replace("'", "''") & "', Template =N'" & template.Template.Replace("'", "''") & "', TemplateDate ='" & template.TemplateDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', TableName = N'" & template.TableName.Replace("'", "''") & "' Where NotificationTemplatesId =" & template.NotificationTemplatesId & " SELECT @@IDENTITY"
            Dim NotificationTemplatesId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            If NotificationTemplatesId > 0 Then
                template.NotificationTemplatesId = NotificationTemplatesId
                AddGroup(template, trans)
                AddUser(template, trans)
            Else
                DeleteGroups(template.NotificationTemplatesId, trans)
                DeleteUsers(template.NotificationTemplatesId, trans)
                AddGroup(template, trans)
                AddUser(template, trans)
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
    Public Function AddGroup(ByVal template As NotificationTemplates, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            For Each group As NotificationGroups In template.NGList

                Dim strSQL As String = String.Empty
                strSQL = " INSERT INTO NotificationGroups(NotificationTemplatesId, GroupId) " _
                & " VALUES(" & template.NotificationTemplatesId & ", " & group.GroupId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddUser(ByVal template As NotificationTemplates, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            For Each user As NotificationUsers In template.NUList

                Dim strSQL As String = String.Empty
                strSQL = " INSERT INTO NotificationUsers(NotificationTemplatesId, UserId) " _
                & " VALUES(" & template.NotificationTemplatesId & ", " & user.UserId & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Next
            'trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal NotificationTemplatesId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = " Delete FROM NotificationTemplates Where NotificationTemplatesId = " & NotificationTemplatesId & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            DeleteGroups(NotificationTemplatesId, trans)
            DeleteUsers(NotificationTemplatesId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function DeleteGroups(ByVal NotificationTemplatesId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = " Delete FROM NotificationGroups Where NotificationTemplatesId = " & NotificationTemplatesId & " "
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
    Public Function DeleteUsers(ByVal NotificationTemplatesId As Integer, ByVal trans As SqlTransaction) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        'Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = " Delete FROM NotificationUsers Where NotificationTemplatesId = " & NotificationTemplatesId & " "
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
    Public Function SaveAndSendNotification(ByVal Subject As String, ByVal TableName As String, ByVal ApplicationReference As Integer, ByVal ValueTable As DataTable, ByVal SourceApplication As String) As Boolean
        Dim UserList As New List(Of Integer)
        Dim Template As String = ""
        Try
            If ValueTable.Rows.Count > 0 Then
                Dim dtTemplate As DataTable = GetTemplate(Subject, TableName)
                If dtTemplate.Rows.Count > 0 Then
                    Dim dtTemplateGroups As DataTable = GetGroups(Val(dtTemplate.Rows(0).Item("NotificationTemplatesId").ToString))
                    If dtTemplateGroups.Rows.Count > 0 Then
                        For Each dr As DataRow In dtTemplateGroups.Rows
                            Dim dtUsers As DataTable = GetGroupUsers(Val(dr.Item("GroupId").ToString))
                            If dtUsers.Rows.Count > 0 Then
                                For Each dr1 As DataRow In dtUsers.Rows
                                    UserList.Add(Val(dr1.Item("User_ID").ToString))
                                Next
                            End If
                        Next
                    End If
                    Dim dtTemplateUsers As DataTable = GetUsers(Val(dtTemplate.Rows(0).Item("NotificationTemplatesId").ToString))
                    If dtTemplateUsers.Rows.Count > 0 Then
                        For Each dr As DataRow In dtTemplateUsers.Rows
                            UserList.Add(dr.Item("UserId").ToString)
                        Next
                    End If

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
    Public Function GetAll() As DataTable
        Dim Str As String = ""
        Dim dt As DataTable
        Try
            Str = "Select * From NotificationTemplates"
            dt = UtilityDAL.GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTemplate(ByVal Subject As String, ByVal TableName As String) As DataTable
        Dim Str As String = ""
        Dim dt As DataTable
        Try
            Str = "Select * From NotificationTemplates Where Subject = N'" & Subject.Replace("'", "''") & "' And TableName = N'" & TableName.Replace("'", "''") & "'"
            dt = UtilityDAL.GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetGroups(ByVal NotificationTemplatesId As Integer) As DataTable
        Dim Str As String = ""
        Dim dt As DataTable
        Try
            Str = "Select GroupId From NotificationGroups Where NotificationTemplatesId = " & NotificationTemplatesId & ""
            dt = UtilityDAL.GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetUsers(ByVal NotificationTemplatesId As Integer) As DataTable
        Dim Str As String = ""
        Dim dt As DataTable
        Try
            Str = "Select UserId From NotificationUsers Where NotificationTemplatesId = " & NotificationTemplatesId & ""
            dt = UtilityDAL.GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetGroupUsers(ByVal GroupId As Integer) As DataTable
        Dim Str As String = ""
        Dim dt As DataTable
        Try
            Str = "Select User_ID From tblUser Where GroupId = " & GroupId & ""
            dt = UtilityDAL.GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetColumns(ByVal TableName As String) As DataTable
        Dim Str As String = ""
        Dim dt As DataTable
        Try
            Str = "Select '@' + COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" & TableName & "'"
            dt = UtilityDAL.GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTables() As DataTable
        Dim Str As String = ""
        Dim dt As DataTable
        Try
            Str = "Select TABLE_NAME, CASE WHEN TABLE_NAME ='SalesInquiryMaster' Then 'Sales Inquiry' WHEN TABLE_NAME ='SalesInquiryRights' Then 'Assigning Of Inquiries' WHEN TABLE_NAME ='PurchaseInquiryMaster' Then 'Purchase Inquiry' WHEN TABLE_NAME ='VendorQuotationMaster' Then 'Vendor Quotation' WHEN TABLE_NAME ='InquiryComparisonStatement' Then 'Inquiry Comparison Statement' WHEN TABLE_NAME ='QuotationMasterTable' Then 'Quotation' WHEN TABLE_NAME ='SalesOrderMasterTable' Then 'Sales Order' WHEN TABLE_NAME ='SalesMasterTable' Then 'Sales' WHEN TABLE_NAME ='SalesReturnMasterTable' Then 'Sales Return' WHEN TABLE_NAME ='PurchaseOrderMasterTable' Then 'Purchase Order' WHEN TABLE_NAME ='ReceivingMasterTable' Then 'Purchase' WHEN TABLE_NAME ='ReceivingNoteMasterTable' Then 'Goods Receiving Note' WHEN TABLE_NAME ='PurchaseReturnMasterTable' Then 'Purchase Return' WHEN TABLE_NAME ='DeliveryChalanMasterTable' Then 'Delivery Chalan' WHEN TABLE_NAME ='tblVoucher' Then 'Voucher' WHEN TABLE_NAME ='PurchaseDemandMasterTable' Then 'Purchase Demand' Else TABLE_NAME END AS [Table Name] FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME IN('SalesInquiryMaster', 'SalesInquiryRights', 'PurchaseInquiryMaster', 'VendorQuotationMaster', 'InquiryComparisonStatement', 'QuotationMasterTable', 'SalesOrderMasterTable', 'SalesMasterTable', 'SalesReturnMasterTable','PurchaseOrderMasterTable', 'ReceivingMasterTable', 'ReceivingNoteMasterTable', 'PurchaseReturnMasterTable', 'DeliveryChalanMasterTable', 'tblVoucher', 'PurchaseDemandMasterTable')"
            dt = UtilityDAL.GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SubjectExists(ByVal Subject As String) As Boolean
        Dim Str As String = ""
        Dim dt As DataTable
        Try
            Str = "Select Subject From NotificationTemplates Where Subject = '" & Subject.Replace("'", "''") & "'"
            dt = UtilityDAL.GetDataTable(Str)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

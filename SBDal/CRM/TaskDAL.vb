'2015-06-22 Task#2015060024 Add Time in and Time Out Ali Ansari 
'2015-08-03 Task#20150801 Making Employee Wise Selection according to rights 
Imports SBModel
Imports SBUtility.Utility
Imports SBDal

Imports SBUtility
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Security.Cryptography


Public Class TaskDAL
    Dim dateTime As DateTime
    Public Function Add(ByVal objMod As Task) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Dim str As String = String.Empty
        Try


            objMod.TaskNo = GetNextDocNo(objMod.Prefix, trans)
            'Marked Against Task#2015060024 Add Time in and Time Out Ali Ansari 
            '            str = "Insert Into TblDefTasks Values(N'" & objMod.TaskDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.TaskName.Trim.Replace("'", "''") & "', N'" & objMod.TaskRemarks.Trim.Replace("'", "''") & "', " & objMod.TaskProject & ", " & objMod.TaskType & ", " & objMod.TaskStatus & ", " & objMod.TaskUser & ", " & objMod.Active & ", " & objMod.CustomerId & "," & IIf(objMod.ClosingDate = Date.MinValue, "NULL", "'" & objMod.ClosingDate.ToString("yyyy-M-d hh:mm:ss tt") & "'") & ", '" & objMod.TaskNo.Replace("'", "''") & "')"
            'Marked Against Task#2015060024 Add Time in and Time Out Ali Ansari 
            'Marked Against Task#20150801 Add UserID Ali Ansari
            'str = "Insert Into TblDefTasks Values(N'" & objMod.TaskDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.TaskName.Trim.Replace("'", "''") & "', N'" & objMod.TaskRemarks.Trim.Replace("'", "''") & "', " & objMod.TaskProject & ", " & objMod.TaskType & ", " & objMod.TaskStatus & ", " & objMod.TaskUser & ", " & objMod.Active & ", " & objMod.CustomerId & "," & IIf(objMod.ClosingDate = Date.MinValue, "NULL", "'" & objMod.ClosingDate.ToString("yyyy-M-d hh:mm:ss tt") & "'") & ", '" & objMod.TaskNo.Replace("'", "''") & "'," & IIf(objMod.TimeIn = Nothing, "NULL", "convert(datetime,'" & objMod.TimeIn & "',102)") & "," & IIf(objMod.TimeOut = Nothing, "Null", "convert(datetime,'" & objMod.TimeOut & "',102)") & ")"
            'Marked Against Task#20150801 Add UserID Ali Ansari
            'Altered Against Task#20150801 Add UserID Ali Ansari

            '            TaskId	int	Unchecked
            'TaskDate	datetime	Checked
            'Name	varchar(50)	Checked
            'Remarks	varchar(200)	Checked
            'Project	int	Checked
            'Type	int	Checked
            'TaskStatus	int	Checked
            'TaskUser	int	Checked
            'Active	bit	Checked
            'CustomerId	int	Checked
            'ClosingDate	datetime	Checked
            'Task_No	nvarchar(20)	Checked
            'TimeIn	datetime	Checked
            'TimeOut	datetime	Checked
            'UserId	int	Checked
            'AssignedTo	int	Checked
            'Completed	bit	Checked
            'FormName	nvarchar(50)	Checked
            'Ref_No	nvarchar(50)	Checked
            'CreatedBy	nvarchar(100)	Checked
            'CreatedByID	int	Checked
            'LastUpdatedBy	nvarchar(100)	Checked
            'ContactPersonID	int	Checked
            '            Unchecked()
            str = "Insert Into TblDefTasks Values(N'" & objMod.TaskDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objMod.TaskName.Trim.Replace("'", "''") & "',  N'" & objMod.TaskRemarks.Trim.Replace("'", "''") & "', " _
                & "" & objMod.TaskProject & ", " & objMod.TaskType & ", " & objMod.TaskStatus & ", " & objMod.TaskUser & ", " & objMod.Active & ", " & objMod.CustomerId & ", " _
                & "" & IIf(objMod.ClosingDate = Date.MinValue, "NULL", "'" & objMod.ClosingDate.ToString("yyyy-M-d hh:mm:ss tt") & "'") & ", '" & objMod.TaskNo.Replace("'", "''") & "', " _
                & "" & IIf(objMod.TimeIn = Nothing, "NULL", "convert(datetime,'" & objMod.TimeIn & "',102)") & "," & IIf(objMod.TimeOut = Nothing, "Null", "convert(datetime,'" & objMod.TimeOut & "',102)") & ", " _
                & "" & objMod.UserId & "," & objMod.AssignedTo & ", '" & objMod.Completed & "', N'" & objMod.FormName.Trim.Replace("'", "''") & "', N'" & objMod.Ref_No.Trim.Replace("'", "''") & "', " _
                & " N'" & objMod.CreatedBy.Trim.Replace("'", "''") & "', " & objMod.CreatedByID & ", N'" & objMod.CreatedBy.Trim.Replace("'", "''") & "', " & objMod.ContactPersonID & ", " & IIf(objMod.CustomEndDate = Date.MinValue, "NULL", "'" & objMod.CustomEndDate.ToString("yyyy-M-d hh:mm:ss tt") & "'") & " , NULL, NULL, NULL)" ' CreatedByID Private _ As String
            'Altered Against Task#20150801 Add UserID Ali Ansari
            objMod.TaskId = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))


            '// Adding notifications
            Dim Notification As New AgriusNotifications

            Notification.NotificationTitle = objMod.TaskName
            Notification.NotificationDescription = objMod.TaskRemarks
            Notification.SourceApplication = "CRM > Task"
            Notification.ApplicationReference = objMod.TaskId

            Dim objDList As New List(Of NotificationDetail)
            objDList.Add(New NotificationDetail(New SecurityUser(objMod.UserId)))
            Notification.NotificationDetils = objDList
           
            Dim Dal As New NotificationDAL
            Dim list As New List(Of AgriusNotifications)
            list.Add(Notification)

            Dal.AddNotification(list)


            objMod.ActivityLog.ActivityName = "Save"
            objMod.ActivityLog.RecordType = "CRM"
            objMod.ActivityLog.RefNo = objMod.TaskNo
            UtilityDAL.BuildActivityLog(objMod.ActivityLog, trans)
            trans.Commit()
            Return True
        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal ObjMod As Task) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Dim str As String = String.Empty
        Try
            'Marked Against Task#2015060024 Add Time in and Time Out Ali Ansari 
            ' str = "Update tblDefTasks set TaskDate=N'" & ObjMod.TaskDate.ToString("yyyy-M-d h:mm:ss tt") & "',  Name=N'" & ObjMod.TaskName.Trim.Replace("'", "''") & "', Remarks=N'" & ObjMod.TaskRemarks.Trim.Replace("'", "''") & "', Project=" & ObjMod.TaskProject & ", Type=" & ObjMod.TaskType & ", TaskUser=" & ObjMod.TaskUser & ",  TaskStatus=" & ObjMod.TaskStatus & ", Active=" & ObjMod.Active & ", CustomerId=" & ObjMod.CustomerId & ",ClosingDate=" & IIf(ObjMod.ClosingDate = Date.MinValue, "NULL", "'" & ObjMod.ClosingDate.ToString("yyyy-M-d hh:mm:ss tt") & "'") & ", Task_No='" & ObjMod.TaskNo.Replace("'", "''") & "' WHERE  TaskID=" & ObjMod.TaskId & " "
            'Marked Against Task#2015060024 Add Time in and Time Out Ali Ansari 
            'Marked Against Task#20150801 Add User Id Ali Ansari
            'Altered Against Task#2015060024 Add Time in and Time Out Ali Ansari 
            'str = "Update tblDefTasks set TaskDate=N'" & ObjMod.TaskDate.ToString("yyyy-M-d h:mm:ss tt") & "',  Name=N'" & ObjMod.TaskName.Trim.Replace("'", "''") & "', Remarks=N'" & ObjMod.TaskRemarks.Trim.Replace("'", "''") & "', Project=" & ObjMod.TaskProject & ", Type=" & ObjMod.TaskType & ", TaskUser=" & ObjMod.TaskUser & ",  TaskStatus=" & ObjMod.TaskStatus & ", Active=" & ObjMod.Active & ", CustomerId=" & ObjMod.CustomerId & ",ClosingDate=" & IIf(ObjMod.ClosingDate = Date.MinValue, "NULL", "'" & ObjMod.ClosingDate.ToString("yyyy-M-d hh:mm:ss tt") & "'") & ", Task_No='" & ObjMod.TaskNo.Replace("'", "''") & "', " _
            '& "timein = " & IIf(ObjMod.TimeIn = Nothing, "NULL", "convert(datetime,'" & ObjMod.TimeIn & "',102)") & " ,TimeOut=" & IIf(ObjMod.TimeOut = Nothing, "NULL", "'" & ObjMod.TimeOut.ToString("yyyy-M-d hh:mm:ss tt") & "'") & " WHERE  TaskID=" & ObjMod.TaskId & " "
            ''Altered Against Task#2015060024 Add Time in and Time Out Ali Ansari 
            'Marked Against Task#20150801 Add User Id Ali Ansari
            'Altered Against Task#20150801 Add User Id Ali Ansari
            str = "Update tblDefTasks set TaskDate=N'" & ObjMod.TaskDate.ToString("yyyy-M-d h:mm:ss tt") & "',  Name=N'" & ObjMod.TaskName.Trim.Replace("'", "''") & "', Remarks=N'" & ObjMod.TaskRemarks.Trim.Replace("'", "''") & "', Project=" & ObjMod.TaskProject & ", Type=" & ObjMod.TaskType & ", TaskUser=" & ObjMod.TaskUser & ",  TaskStatus=" & ObjMod.TaskStatus & ", Active=" & ObjMod.Active & ", CustomerId=" & ObjMod.CustomerId & ",ClosingDate=" & IIf(ObjMod.ClosingDate = Date.MinValue, "NULL", "'" & ObjMod.ClosingDate.ToString("yyyy-M-d hh:mm:ss tt") & "'") & ", Task_No='" & ObjMod.TaskNo.Replace("'", "''") & "', " _
            & "timein = " & IIf(ObjMod.TimeIn = Date.MinValue, "NULL", "convert(datetime,'" & ObjMod.TimeIn & "',102)") & " ,TimeOut=" & IIf(ObjMod.TimeOut = Date.MinValue, "NULL", "'" & ObjMod.TimeOut.ToString("yyyy-M-d hh:mm:ss tt") & "'") & " , userid = " & ObjMod.UserId & ", AssignedTo = " & ObjMod.AssignedTo & ", Completed = " & ObjMod.Completed & ", Ref_No = N'" & ObjMod.Ref_No.Trim.Replace("'", "''") & "', FormName=N'" & ObjMod.FormName.Trim.Replace("'", "''") & "', LastUpdatedBy = N'" & ObjMod.LastUpdatedBy.Trim.Replace("'", "''") & "', ContactPersonID =" & ObjMod.ContactPersonID & ", CustomEndDate=" & IIf(ObjMod.CustomEndDate = Date.MinValue, "NULL", "'" & ObjMod.CustomEndDate.ToString("yyyy-M-d hh:mm:ss tt") & "'") & " WHERE  TaskID=" & ObjMod.TaskId & " "
            'Altered Against Task#20150801 Add User Id Ali Ansari
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ObjMod.ActivityLog.ActivityName = "Update"
            ObjMod.ActivityLog.RecordType = "CRM"
            ObjMod.ActivityLog.RefNo = ObjMod.TaskNo
            UtilityDAL.BuildActivityLog(ObjMod.ActivityLog, trans)
            trans.Commit()
            Return True
        Catch ex As SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal ObjMod As Task) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Dim str As String = String.Empty

        Try
            str = "Delete From tblTaskActivityUpdate WHERE TaskId =" & ObjMod.TaskId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            str = "Delete From tblDefTasks WHERE TaskID=" & ObjMod.TaskId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            ObjMod.ActivityLog.ActivityName = "Delete"
            ObjMod.ActivityLog.RecordType = "CRM"
            ObjMod.ActivityLog.RefNo = ObjMod.TaskId
            'ObjMod.ActivityLog.FormName = 

            UtilityDAL.BuildActivityLog(ObjMod.ActivityLog, trans)

            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllRecords(ByVal cond As String) As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim Dt As New DataTable
        Dim Da As SqlDataAdapter
        Try
            Dim str As String = String.Empty
            'Marked Against Task#2015060024 Add Time in and Time Out Ali Ansari 
            ' str = "SELECT     TblDefTasks.TaskId, TblDefTasks.TaskDate, TblDefTasks.Task_No, TblDefTasks.Name as [Task Name], TblDefTasks.Remarks, TblDefTasks.Project, tblDefCostCenter.Name AS [Cost Center],TblDefTasks.TaskUser, tblDefEmployee.Employee_Name as [Employee Name], tblDefTasks.CustomerId, COA.Detail_Title as Customer,  " _
            '& "                TblDefTasks.Type, tbldefTypes.Name AS [Task Type], TblDefTasks.TaskStatus, tbldefStatus.Name AS Status, tblDefTasks.ClosingDate, " _
            '& "            TblDefTasks.Active " _
            '& "           FROM   TblDefTasks LEFT OUTER JOIN" _
            '& "           tbldefTypes ON TblDefTasks.Type = tbldefTypes.TypeId" _
            '& "           LEFT OUTER JOIN " _
            '& "           tblDefEmployee ON TblDefTasks.TaskUser = tblDefEmployee.Employee_Id LEFT OUTER JOIN " _
            '& "           tbldefStatus ON TblDefTasks.TaskStatus = tbldefStatus.StatusId LEFT OUTER JOIN " _
            '& "           tblDefCostCenter ON TblDefTasks.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN vwCOADetail COA On COA.coa_detail_id = tblDefTasks.CustomerId "
            'Marked Against Task#2015060024 Add Time in and Time Out Ali Ansari 
            'Marked Against Task#20150801 Add user id Ali Ansari
            'Altered Against Task#2015060024 Add Time in and Time Out Ali Ansari 
            ' str = "SELECT     TblDefTasks.TaskId, TblDefTasks.TaskDate, TblDefTasks.Task_No, TblDefTasks.Name as [Task Name], TblDefTasks.Remarks,tbldeftasks.TimeIn,tbldeftasks.TimeOut, TblDefTasks.Project, tblDefCostCenter.Name AS [Cost Center],TblDefTasks.TaskUser, tblDefEmployee.Employee_Name as [Employee Name], tblDefTasks.CustomerId, COA.Detail_Title as Customer,  " _
            '& "                TblDefTasks.Type, tbldefTypes.Name AS [Task Type], TblDefTasks.TaskStatus, tbldefStatus.Name AS Status, tblDefTasks.ClosingDate, " _
            '& "            TblDefTasks.Active " _
            '& "           FROM   TblDefTasks LEFT OUTER JOIN" _
            '& "           tbldefTypes ON TblDefTasks.Type = tbldefTypes.TypeId" _
            '& "           LEFT OUTER JOIN " _
            '& "           tblDefEmployee ON TblDefTasks.TaskUser = tblDefEmployee.Employee_Id LEFT OUTER JOIN " _
            '& "           tbldefStatus ON TblDefTasks.TaskStatus = tbldefStatus.StatusId LEFT OUTER JOIN " _
            '& "           tblDefCostCenter ON TblDefTasks.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN vwCOADetail COA On COA.coa_detail_id = tblDefTasks.CustomerId "
            'Altered Against Task#2015060024 Add Time in and Time Out Ali Ansari 
            'Marked Against Task#20150801 Add user id Ali Ansari
            'Altered Against Task#20150801 Making Employee Wise Selection 
            str = "SELECT     TblDefTasks.TaskId, TblDefTasks.TaskDate, TblDefTasks.Task_No, TblDefTasks.Name as [Task Name], TblDefTasks.Remarks,tbldeftasks.TimeIn,tbldeftasks.TimeOut, TblDefTasks.Project, tblDefCostCenter.Name AS [Cost Center],TblDefTasks.TaskUser, tblDefEmployee.Employee_Name as [Employee Name], tblDefTasks.CustomerId, COA.Detail_Title as Customer,  " _
                    & " TblDefTasks.Type, tbldefTypes.Name AS [Task Type], TblDefTasks.TaskStatus, tbldefStatus.Name AS Status, tblDefTasks.ClosingDate, " _
                    & " TblDefTasks.Active,tbldeftasks.UserID, TblDefTasks.AssignedTo, tblUser.User_Name As AssignedToName, TblDefTasks.Completed,  " _
                    & " Ref_No, FormName, CreatedBy, ISNULL(CreatedByID, 0) As CreatedByID, LastUpdatedBy, ISNULL(ContactPersonID, 0) As ContactPersonID, COA.EndDate as CustomEndDate " _
                    & " FROM   TblDefTasks LEFT OUTER JOIN" _
                    & " tbldefTypes ON TblDefTasks.Type = tbldefTypes.TypeId" _
                    & " LEFT OUTER JOIN " _
                    & " tblDefEmployee ON TblDefTasks.TaskUser = tblDefEmployee.Employee_Id LEFT OUTER JOIN " _
                    & " tbldefStatus ON TblDefTasks.TaskStatus = tbldefStatus.StatusId LEFT OUTER JOIN " _
                    & " tblDefCostCenter ON TblDefTasks.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN vwCOADetail COA On COA.coa_detail_id = tblDefTasks.CustomerId LEFT OUTER JOIN " _
                    & " tblUser On TblDefTasks.AssignedTo = tblUser.User_ID "

            str = str + cond
            str = str + " order by TblDefTasks.taskid desc"
            'Altered Against Task#20150801 Making Employee Wise Selection 
            Da = New SqlDataAdapter(str, Con)
            Da.Fill(Dt)
            For Each DR As DataRow In Dt.Rows
                DR.BeginEdit()
                If Not DR("AssignedToName") Is DBNull.Value Then
                    DR("AssignedToName") = Decrypt(DR("AssignedToName"))
                End If

                DR.EndEdit()
            Next
            Dt.AcceptChanges()
            Return Dt
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Private Function Decrypt(ByVal strText As String) As String
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim sDecrKey As String = "simpleaccounts"
        Dim inputByteArray(strText.Length) As Byte
        Try
            Dim byKey() As Byte = System.Text.Encoding.UTF8.GetBytes(Microsoft.VisualBasic.Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider
            inputByteArray = Convert.FromBase64String(strText)
            Dim ms As New IO.MemoryStream
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Dim strDecrypt = encoding.GetString(ms.ToArray())
            ms.Close()
            cs.Close()
            des.Clear()
            Return strDecrypt
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
    Public Function GetTypeWise(Optional ByVal ref_no As String = "") As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim Dt As New DataTable
        Dim Da As SqlDataAdapter
        Try
            Dim str As String = String.Empty

            str = "SELECT     TblDefTasks.TaskId, TblDefTasks.TaskDate, TblDefTasks.Task_No, TblDefTasks.Name as [Task Name], TblDefTasks.Remarks,tbldeftasks.TimeIn,tbldeftasks.TimeOut, TblDefTasks.Project, tblDefCostCenter.Name AS [Cost Center],TblDefTasks.TaskUser, tblDefEmployee.Employee_Name as [Employee Name], tblDefTasks.CustomerId, COA.Detail_Title as Customer,  " _
                    & " TblDefTasks.Type, tbldefTypes.Name AS [Task Type], TblDefTasks.TaskStatus, tbldefStatus.Name AS Status, tblDefTasks.ClosingDate, " _
                    & " TblDefTasks.Active,tbldeftasks.UserID, TblDefTasks.AssignedTo, tblUser.User_Name As AssignedToName, TblDefTasks.Completed,  " _
                    & " Ref_No, FormName, CreatedBy, ISNULL(CreatedByID, 0) As CreatedByID, LastUpdatedBy, ISNULL(ContactPersonID, 0) As ContactPersonID, tblDefTasks.CustomEndDate " _
                    & " FROM   TblDefTasks LEFT OUTER JOIN" _
                    & " tbldefTypes ON TblDefTasks.Type = tbldefTypes.TypeId" _
                    & " LEFT OUTER JOIN " _
                    & " tblDefEmployee ON TblDefTasks.TaskUser = tblDefEmployee.Employee_Id LEFT OUTER JOIN " _
                    & " tbldefStatus ON TblDefTasks.TaskStatus = tbldefStatus.StatusId LEFT OUTER JOIN " _
                    & " tblDefCostCenter ON TblDefTasks.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN vwCOADetail COA On COA.coa_detail_id = tblDefTasks.CustomerId LEFT OUTER JOIN " _
                    & " tblUser On TblDefTasks.AssignedTo = tblUser.User_ID " _
                    & " Where TblDefTasks.Type in (select TypeId from tbldefTypes) " & IIf(ref_no = String.Empty, "", "And Ref_No='" & ref_no & "' ") & " And tblDefTasks.ClosingDate Is Null"


            str = str + " order by TblDefTasks.Type desc"

            Da = New SqlDataAdapter(str, Con)
            Da.Fill(Dt)
            For Each DR As DataRow In Dt.Rows
                DR.BeginEdit()
                If Not DR("AssignedToName") Is DBNull.Value Then
                    DR("AssignedToName") = Decrypt(DR("AssignedToName"))
                End If

                DR.EndEdit()
            Next
            Dt.AcceptChanges()
            Return Dt
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAssignedToWise(Optional ByVal ref_no As String = "") As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim Dt As New DataTable
        Dim Da As SqlDataAdapter
        Try
            Dim str As String = String.Empty

            str = "SELECT     TblDefTasks.TaskId, TblDefTasks.TaskDate, TblDefTasks.Task_No, TblDefTasks.Name as [Task Name], TblDefTasks.Remarks,tbldeftasks.TimeIn,tbldeftasks.TimeOut, TblDefTasks.Project, tblDefCostCenter.Name AS [Cost Center],TblDefTasks.TaskUser, tblDefEmployee.Employee_Name as [Employee Name], tblDefTasks.CustomerId, COA.Detail_Title as Customer,  " _
                    & " TblDefTasks.Type, tbldefTypes.Name AS [Task Type], TblDefTasks.TaskStatus, tbldefStatus.Name AS Status, tblDefTasks.ClosingDate, " _
                    & " TblDefTasks.Active,tbldeftasks.UserID, TblDefTasks.AssignedTo, tblUser.User_Name As AssignedToName, TblDefTasks.Completed,  " _
                    & " Ref_No, FormName, CreatedBy, ISNULL(CreatedByID, 0) As CreatedByID, LastUpdatedBy, ISNULL(ContactPersonID, 0) As ContactPersonID, tblDefTasks.CustomEndDate " _
                    & " FROM   TblDefTasks LEFT OUTER JOIN" _
                    & " tbldefTypes ON TblDefTasks.Type = tbldefTypes.TypeId" _
                    & " LEFT OUTER JOIN " _
                    & " tblDefEmployee ON TblDefTasks.TaskUser = tblDefEmployee.Employee_Id LEFT OUTER JOIN " _
                    & " tbldefStatus ON TblDefTasks.TaskStatus = tbldefStatus.StatusId LEFT OUTER JOIN " _
                    & " tblDefCostCenter ON TblDefTasks.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN vwCOADetail COA On COA.coa_detail_id = tblDefTasks.CustomerId LEFT OUTER JOIN " _
                    & " tblUser On TblDefTasks.AssignedTo = tblUser.User_ID " _
                    & " Where TblDefTasks.AssignedTo In (Select User_ID From tblUser) " & IIf(ref_no = String.Empty, "", "And Ref_No='" & ref_no & "' ") & " And tblDefTasks.ClosingDate Is Null"


            str = str + " order by TblDefTasks.taskid desc"

            Da = New SqlDataAdapter(str, Con)
            Da.Fill(Dt)
            For Each DR As DataRow In Dt.Rows
                DR.BeginEdit()
                If Not DR("AssignedToName") Is DBNull.Value Then
                    DR("AssignedToName") = Decrypt(DR("AssignedToName"))
                End If

                DR.EndEdit()
            Next
            Dt.AcceptChanges()
            Return Dt
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetMyWork(ByVal loginUser As Integer, Optional ByVal ref_no As String = "") As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim Dt As New DataTable
        Dim Da As SqlDataAdapter
        Try
            Dim str As String = String.Empty
            dateTime = UtilityDAL.GetCurrentServerDate()
            str = "SELECT     TblDefTasks.TaskId, Case When Convert(varchar,TblDefTasks.TaskDate, 102) = Convert(datetime,'" & dateTime.ToString("yyyy-M-d 00:00:00") & "',102) Then TblDefTasks.TaskDate When  Convert(varchar,TblDefTasks.TaskDate, 102) < Convert(datetime,'" & dateTime.ToString("yyyy-M-d 00:00:00") & "',102) Then TblDefTasks.TaskDate When  Convert(varchar,TblDefTasks.TaskDate, 102) > Convert(datetime,'" & dateTime.ToString("yyyy-M-d 00:00:00") & "',102) Then TblDefTasks.TaskDate End as TaskDate, Case When  Convert(varchar,TblDefTasks.TaskDate, 102) = Convert(datetime,'" & dateTime.ToString("yyyy-M-d 00:00:00") & "',102) Then 'Today' When  Convert(varchar,TblDefTasks.TaskDate, 102) < Convert(datetime,'" & dateTime.ToString("yyyy-M-d 00:00:00") & "',102) Then 'Previous' When  Convert(varchar,TblDefTasks.TaskDate, 102) > Convert(datetime,'" & dateTime.ToString("yyyy-M-d 00:00:00") & "',102) Then 'Upcoming' End as [Work Period], TblDefTasks.Task_No, TblDefTasks.Name as [Task Name], TblDefTasks.Remarks,tbldeftasks.TimeIn,tbldeftasks.TimeOut, TblDefTasks.Project, tblDefCostCenter.Name AS [Cost Center],TblDefTasks.TaskUser, tblDefEmployee.Employee_Name as [Employee Name], tblDefTasks.CustomerId, COA.Detail_Title as Customer,  " _
                    & " TblDefTasks.Type, tbldefTypes.Name AS [Task Type], TblDefTasks.TaskStatus, tbldefStatus.Name AS Status, tblDefTasks.ClosingDate, " _
                    & " TblDefTasks.Active,tbldeftasks.UserID, TblDefTasks.AssignedTo, tblUser.User_Name As AssignedToName, TblDefTasks.Completed,  " _
                    & " Ref_No, FormName, CreatedBy, ISNULL(CreatedByID, 0) As CreatedByID, LastUpdatedBy, ISNULL(ContactPersonID, 0) As ContactPersonID, tblDefTasks.CustomEndDate " _
                    & " FROM   TblDefTasks LEFT OUTER JOIN" _
                    & " tbldefTypes ON TblDefTasks.Type = tbldefTypes.TypeId" _
                    & " LEFT OUTER JOIN " _
                    & " tblDefEmployee ON TblDefTasks.TaskUser = tblDefEmployee.Employee_Id LEFT OUTER JOIN " _
                    & " tbldefStatus ON TblDefTasks.TaskStatus = tbldefStatus.StatusId LEFT OUTER JOIN " _
                    & " tblDefCostCenter ON TblDefTasks.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN vwCOADetail COA On COA.coa_detail_id = tblDefTasks.CustomerId LEFT OUTER JOIN " _
                    & " tblUser On TblDefTasks.AssignedTo = tblUser.User_ID " _
                    & " Where TblDefTasks.AssignedTo = " & loginUser & " " & IIf(ref_no = String.Empty, "", "And Ref_No='" & ref_no & "' ") & " And tblDefTasks.ClosingDate Is Null"


            'str = str + " order by TblDefTasks.TaskDate desc"

            Da = New SqlDataAdapter(str, Con)
            Da.Fill(Dt)
            For Each DR As DataRow In Dt.Rows
                DR.BeginEdit()
                If Not DR("AssignedToName") Is DBNull.Value Then
                    DR("AssignedToName") = Decrypt(DR("AssignedToName"))
                End If

                DR.EndEdit()
            Next
            Dt.AcceptChanges()
            Return Dt
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetStatusWise(Optional ByVal ref_no As String = "") As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim Dt As New DataTable
        Dim Da As SqlDataAdapter
        Try
            Dim str As String = String.Empty

            str = "SELECT TblDefTasks.TaskId, TblDefTasks.TaskDate, TblDefTasks.Task_No, TblDefTasks.Name as [Task Name], TblDefTasks.Remarks,tbldeftasks.TimeIn,tbldeftasks.TimeOut, TblDefTasks.Project, tblDefCostCenter.Name AS [Cost Center],TblDefTasks.TaskUser, tblDefEmployee.Employee_Name as [Employee Name], tblDefTasks.CustomerId, COA.Detail_Title as Customer,  " _
                    & " TblDefTasks.Type, tbldefTypes.Name AS [Task Type], TblDefTasks.TaskStatus, tbldefStatus.Name AS Status, tblDefTasks.ClosingDate, " _
                    & " TblDefTasks.Active,tbldeftasks.UserID, TblDefTasks.AssignedTo, tblUser.User_Name As AssignedToName, TblDefTasks.Completed,  " _
                    & " Ref_No, FormName, CreatedBy, ISNULL(CreatedByID, 0) As CreatedByID, LastUpdatedBy, ISNULL(ContactPersonID, 0) As ContactPersonID, tblDefTasks.CustomEndDate" _
                    & " FROM   TblDefTasks LEFT OUTER JOIN" _
                    & " tbldefTypes ON TblDefTasks.Type = tbldefTypes.TypeId" _
                    & " LEFT OUTER JOIN " _
                    & " tblDefEmployee ON TblDefTasks.TaskUser = tblDefEmployee.Employee_Id LEFT OUTER JOIN " _
                    & " tbldefStatus ON TblDefTasks.TaskStatus = tbldefStatus.StatusId LEFT OUTER JOIN " _
                    & " tblDefCostCenter ON TblDefTasks.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN vwCOADetail COA On COA.coa_detail_id = tblDefTasks.CustomerId LEFT OUTER JOIN " _
                    & " tblUser On TblDefTasks.AssignedTo = tblUser.User_ID " _
                    & " Where TblDefTasks.TaskStatus In (Select StatusId From tbldefStatus ) " & IIf(ref_no = String.Empty, "", "And Ref_No='" & ref_no & "' ") & " "


            str = str + " order by TblDefTasks.TaskStatus desc"

            Da = New SqlDataAdapter(str, Con)
            Da.Fill(Dt)
            For Each DR As DataRow In Dt.Rows
                DR.BeginEdit()
                If Not DR("AssignedToName") Is DBNull.Value Then
                    DR("AssignedToName") = Decrypt(DR("AssignedToName"))
                End If

                DR.EndEdit()
            Next
            Dt.AcceptChanges()
            Return Dt
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetCustomerWise(Optional ByVal ref_no As String = "") As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim Dt As New DataTable
        Dim Da As SqlDataAdapter
        Try
            Dim str As String = String.Empty

            str = "SELECT     TblDefTasks.TaskId, TblDefTasks.TaskDate, TblDefTasks.Task_No, TblDefTasks.Name as [Task Name], TblDefTasks.Remarks,tbldeftasks.TimeIn,tbldeftasks.TimeOut, TblDefTasks.Project, tblDefCostCenter.Name AS [Cost Center],TblDefTasks.TaskUser, tblDefEmployee.Employee_Name as [Employee Name], tblDefTasks.CustomerId, COA.Detail_Title as Customer,  " _
                    & " TblDefTasks.Type, tbldefTypes.Name AS [Task Type], TblDefTasks.TaskStatus, tbldefStatus.Name AS Status, tblDefTasks.ClosingDate, " _
                    & " TblDefTasks.Active,tbldeftasks.UserID, TblDefTasks.AssignedTo, tblUser.User_Name As AssignedToName, TblDefTasks.Completed,  " _
                    & " Ref_No, FormName, CreatedBy, ISNULL(CreatedByID, 0) As CreatedByID, LastUpdatedBy, ISNULL(ContactPersonID, 0) As ContactPersonID, tblDefTasks.CustomEndDate " _
                    & " FROM   TblDefTasks LEFT OUTER JOIN" _
                    & " tbldefTypes ON TblDefTasks.Type = tbldefTypes.TypeId" _
                    & " LEFT OUTER JOIN " _
                    & " tblDefEmployee ON TblDefTasks.TaskUser = tblDefEmployee.Employee_Id LEFT OUTER JOIN " _
                    & " tbldefStatus ON TblDefTasks.TaskStatus = tbldefStatus.StatusId LEFT OUTER JOIN " _
                    & " tblDefCostCenter ON TblDefTasks.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN vwCOADetail COA On COA.coa_detail_id = tblDefTasks.CustomerId LEFT OUTER JOIN " _
                    & " tblUser On TblDefTasks.AssignedTo = tblUser.User_ID " _
                    & " Where tblDefTasks.CustomerId in (select coa_detail_id From vwCOADetail) " & IIf(ref_no = String.Empty, "", "And Ref_No='" & ref_no & "' ") & " " ''=" & customerID & ""


            str = str + " order by tblDefTasks.CustomerId desc"

            Da = New SqlDataAdapter(str, Con)
            Da.Fill(Dt)
            For Each DR As DataRow In Dt.Rows
                DR.BeginEdit()
                If Not DR("AssignedToName") Is DBNull.Value Then
                    DR("AssignedToName") = Decrypt(DR("AssignedToName"))
                End If

                DR.EndEdit()
            Next
            Dt.AcceptChanges()
            Return Dt
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetProjectWise(Optional ByVal ref_no As String = "") As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim Dt As New DataTable
        Dim Da As SqlDataAdapter
        Try
            Dim str As String = String.Empty

            str = "SELECT     TblDefTasks.TaskId, TblDefTasks.TaskDate, TblDefTasks.Task_No, TblDefTasks.Name as [Task Name], TblDefTasks.Remarks,tbldeftasks.TimeIn,tbldeftasks.TimeOut, TblDefTasks.Project, tblDefCostCenter.Name AS [Cost Center],TblDefTasks.TaskUser, tblDefEmployee.Employee_Name as [Employee Name], tblDefTasks.CustomerId, COA.Detail_Title as Customer,  " _
                    & " TblDefTasks.Type, tbldefTypes.Name AS [Task Type], TblDefTasks.TaskStatus, tbldefStatus.Name AS Status, tblDefTasks.ClosingDate, " _
                    & " TblDefTasks.Active,tbldeftasks.UserID, TblDefTasks.AssignedTo, tblUser.User_Name As AssignedToName, TblDefTasks.Completed,  " _
                    & " Ref_No, FormName, CreatedBy, ISNULL(CreatedByID, 0) As CreatedByID, LastUpdatedBy, ISNULL(ContactPersonID, 0) As ContactPersonID, tblDefTasks.CustomEndDate" _
                    & " FROM   TblDefTasks LEFT OUTER JOIN" _
                    & " tbldefTypes ON TblDefTasks.Type = tbldefTypes.TypeId" _
                    & " LEFT OUTER JOIN " _
                    & " tblDefEmployee ON TblDefTasks.TaskUser = tblDefEmployee.Employee_Id LEFT OUTER JOIN " _
                    & " tbldefStatus ON TblDefTasks.TaskStatus = tbldefStatus.StatusId LEFT OUTER JOIN " _
                    & " tblDefCostCenter ON TblDefTasks.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN vwCOADetail COA On COA.coa_detail_id = tblDefTasks.CustomerId LEFT OUTER JOIN " _
                    & " tblUser On TblDefTasks.AssignedTo = tblUser.User_ID " _
                    & " Where TblDefTasks.Project IN (Select CostCenterID From tblDefCostCenter) " & IIf(ref_no = String.Empty, "", "And Ref_No='" & ref_no & "' ") & " And tblDefTasks.ClosingDate Is Null"


            str = str + " order by TblDefTasks.Project desc"

            Da = New SqlDataAdapter(str, Con)
            Da.Fill(Dt)
            For Each DR As DataRow In Dt.Rows
                DR.BeginEdit()
                If Not DR("AssignedToName") Is DBNull.Value Then
                    DR("AssignedToName") = Decrypt(DR("AssignedToName"))
                End If

                DR.EndEdit()
            Next
            Dt.AcceptChanges()
            Return Dt
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    ''' <summary>
    ''' TASK TFS1790
    ''' </summary>
    ''' <param name="LoginUserId"></param>
    ''' <param name="IsAdminGroup"></param>
    ''' <param name="ref_no"></param>
    ''' <returns></returns>
    ''' <remarks>Below function is defined to get with date grouping and user wise</remarks>
    Public Function GetDateWise(ByVal LoginUserId As Integer, ByVal IsAdminGroup As Boolean, Optional ByVal ref_no As String = "") As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim Dt As New DataTable
        Dim Da As SqlDataAdapter
        Try
            Dim str As String = String.Empty

            str = "SELECT TblDefTasks.TaskId, TblDefTasks.TaskDate, TblDefTasks.Task_No, TblDefTasks.Name as [Task Name], TblDefTasks.Remarks,tbldeftasks.TimeIn,tbldeftasks.TimeOut, TblDefTasks.Project, tblDefCostCenter.Name AS [Cost Center],TblDefTasks.TaskUser, tblDefEmployee.Employee_Name as [Employee Name], tblDefTasks.CustomerId, COA.Detail_Title as Customer,  " _
                    & " TblDefTasks.Type, tbldefTypes.Name AS [Task Type], TblDefTasks.TaskStatus, tbldefStatus.Name AS Status, tblDefTasks.ClosingDate, " _
                    & " TblDefTasks.Active,tbldeftasks.UserID, TblDefTasks.AssignedTo, tblUser.User_Name As AssignedToName, TblDefTasks.Completed,  " _
                    & " Ref_No, FormName, CreatedBy, ISNULL(CreatedByID, 0) As CreatedByID, LastUpdatedBy, ISNULL(ContactPersonID, 0) As ContactPersonID, tblDefTasks.CustomEndDate" _
                    & " FROM   TblDefTasks LEFT OUTER JOIN" _
                    & " tbldefTypes ON TblDefTasks.Type = tbldefTypes.TypeId" _
                    & " LEFT OUTER JOIN " _
                    & " tblDefEmployee ON TblDefTasks.TaskUser = tblDefEmployee.Employee_Id LEFT OUTER JOIN " _
                    & " tbldefStatus ON TblDefTasks.TaskStatus = tbldefStatus.StatusId LEFT OUTER JOIN " _
                    & " tblDefCostCenter ON TblDefTasks.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN vwCOADetail COA On COA.coa_detail_id = tblDefTasks.CustomerId LEFT OUTER JOIN " _
                    & " tblUser On TblDefTasks.AssignedTo = tblUser.User_ID " _
                    & " Where TblDefTasks.TaskDate IS NOT NULL " & IIf(IsAdminGroup = False, " AND TblDefTasks.AssignedTo = " & LoginUserId & "", "") & " " & IIf(ref_no = String.Empty, "", "And Ref_No='" & ref_no & "' ") & " And tblDefTasks.ClosingDate Is Null"


            str = str + " order by TblDefTasks.Project desc"

            Da = New SqlDataAdapter(str, Con)
            Da.Fill(Dt)
            For Each DR As DataRow In Dt.Rows
                DR.BeginEdit()
                If Not DR("AssignedToName") Is DBNull.Value Then
                    DR("AssignedToName") = Decrypt(DR("AssignedToName"))
                End If

                DR.EndEdit()
            Next
            Dt.AcceptChanges()
            Return Dt
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function


    Public Shared Function GetNextDocNo(ByVal Prefix As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        Try

            Dim dt As New DataTable
            Dim Serial As Integer = 1I
            Dim SerialNo As String = String.Empty
            dt = UtilityDAL.GetDataTable("Select IsNull(Max(IsNull(Right(Task_No,5),0)),0)+1 Serial From tblDefTasks WHERE LEFT(Task_No," & Prefix.Length & ")='" & Prefix.Replace("'", "''") & "'", trans)
            dt.AcceptChanges()

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Serial = Val(dt.Rows(0).Item(0).ToString)
                End If
            End If

            SerialNo = Prefix.ToString & CStr(Microsoft.VisualBasic.Right("00000" & CStr(Serial), 5))
            Return SerialNo
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Sub UpdateAssignedTo(ByVal ActivityLog As ActivityLog, ByVal taskID As Integer, ByVal assignedTo As Integer)
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Dim ActivityLog As New ActivityLog
        Dim trans As SqlTransaction
        Try
            If Not Con.State = 1 Then Con.Open()
            trans = Con.BeginTransaction
            Dim str As String = String.Empty

            str = "Update  tblDefTasks Set AssignedTo= '" & assignedTo & "' Where TaskID='" & taskID & "' "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ActivityLog.ActivityName = "Update"
            ActivityLog.RecordType = "CRM"
            'ActivityLog.RefNo = taskID
            UtilityDAL.BuildActivityLog(ActivityLog, trans)

            trans.Commit()
        Catch ex As Exception
            Throw ex
            trans.Rollback()
        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try

    End Sub
    Public Sub UpdateTypeCell(ByVal ActivityLog As ActivityLog, ByVal taskID As Integer, ByVal type As Integer)
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Dim ActivityLog As New ActivityLog
        Dim trans As SqlTransaction
        Try
            If Not Con.State = 1 Then Con.Open()
            trans = Con.BeginTransaction
            Dim str As String = String.Empty

            str = "Update tblDefTasks Set Type='" & type & "' Where TaskID='" & taskID & "' "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ActivityLog.ActivityName = "Update"
            ActivityLog.RecordType = "CRM"
            'ActivityLog.RefNo = taskID
            UtilityDAL.BuildActivityLog(ActivityLog, trans)

            trans.Commit()
        Catch ex As Exception
            Throw ex
            trans.Rollback()
        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try

    End Sub
    Public Sub UpdateStatusCell(ByVal ActivityLog As ActivityLog, ByVal taskID As Integer, ByVal status As Integer)
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Dim ActivityLog As New ActivityLog
        Dim trans As SqlTransaction
        Try
            If Not Con.State = 1 Then Con.Open()
            trans = Con.BeginTransaction
            Dim str As String = String.Empty

            str = "Update  tblDefTasks Set TaskStatus='" & status & "' Where TaskID='" & taskID & "' "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ActivityLog.ActivityName = "Update"
            ActivityLog.RecordType = "CRM"
            'ActivityLog.RefNo = taskID
            UtilityDAL.BuildActivityLog(ActivityLog, trans)

            trans.Commit()
        Catch ex As Exception
            Throw ex
            trans.Rollback()
        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try

    End Sub
    Public Sub UpdateTitleCell(ByVal ActivityLog As ActivityLog, ByVal taskID As Integer, ByVal title As String)
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Dim ActivityLog As New ActivityLog
        Dim trans As SqlTransaction
        Try
            If Not Con.State = 1 Then Con.Open()
            trans = Con.BeginTransaction
            Dim str As String = String.Empty

            str = "Update  tblDefTasks Set Name='" & title.Trim.Replace("''", "''") & "' Where TaskID='" & taskID & "' "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            ActivityLog.ActivityName = "Update"
            ActivityLog.RecordType = "CRM"
            'ActivityLog.RefNo = taskID
            UtilityDAL.BuildActivityLog(ActivityLog, trans)

            trans.Commit()
        Catch ex As Exception
            Throw ex
            trans.Rollback()
        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try

    End Sub
    Public Function CountTasksAgainstDocument(ByVal ref_No As String) As Integer
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Dim ActivityLog As New ActivityLog
        Dim dt As New DataTable
        Try
            If Not Con.State = 1 Then Con.Open()

            Dim str As String = String.Empty

            str = "Select Count(*) FROM TblDefTasks Where Ref_No ='" & ref_No.Trim.Replace("''", "''") & "' "
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            dt = UtilityDAL.GetDataTable(str)
            Return dt.Columns(0).DefaultValue

        Catch ex As Exception
            Throw ex

        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try

    End Function
    Public Function GetReferenceTasks(ByVal ref_No As String) As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Not Con.State = 1 Then Con.Open()
        Dim Dt As New DataTable
        Dim Da As SqlDataAdapter
        Try
            Dim str As String = String.Empty

            str = "SELECT     TblDefTasks.TaskId, TblDefTasks.TaskDate, TblDefTasks.Task_No, TblDefTasks.Name as [Task Name], TblDefTasks.Remarks,tbldeftasks.TimeIn,tbldeftasks.TimeOut, TblDefTasks.Project, tblDefCostCenter.Name AS [Cost Center],TblDefTasks.TaskUser, tblDefEmployee.Employee_Name as [Employee Name], tblDefTasks.CustomerId, COA.Detail_Title as Customer,  " _
                    & " TblDefTasks.Type, tbldefTypes.Name AS [Task Type], TblDefTasks.TaskStatus, tbldefStatus.Name AS Status, tblDefTasks.ClosingDate, " _
                    & " TblDefTasks.Active,tbldeftasks.UserID, TblDefTasks.AssignedTo, tblUser.User_Name As AssignedToName, TblDefTasks.Completed,  " _
                    & " Ref_No, FormName, CreatedBy, ISNULL(CreatedByID, 0) As CreatedByID, LastUpdatedBy, ISNULL(ContactPersonID, 0) As ContactPersonID, tblDefTasks.CustomEndDate" _
                    & " FROM   TblDefTasks LEFT OUTER JOIN" _
                    & " tbldefTypes ON TblDefTasks.Type = tbldefTypes.TypeId" _
                    & " LEFT OUTER JOIN " _
                    & " tblDefEmployee ON TblDefTasks.TaskUser = tblDefEmployee.Employee_Id LEFT OUTER JOIN " _
                    & " tbldefStatus ON TblDefTasks.TaskStatus = tbldefStatus.StatusId LEFT OUTER JOIN " _
                    & " tblDefCostCenter ON TblDefTasks.Project = tblDefCostCenter.CostCenterID LEFT OUTER JOIN vwCOADetail COA On COA.coa_detail_id = tblDefTasks.CustomerId LEFT OUTER JOIN " _
                    & " tblUser On TblDefTasks.AssignedTo = tblUser.User_ID " _
                    & " Where Ref_No = '" & ref_No & "' "


            str = str + " order by TblDefTasks.taskid desc"
            'Altered Against Task#20150801 Making Employee Wise Selection 
            Da = New SqlDataAdapter(str, Con)
            Da.Fill(Dt)
            For Each DR As DataRow In Dt.Rows
                DR.BeginEdit()
                If Not DR("AssignedToName") Is DBNull.Value Then
                    DR("AssignedToName") = Decrypt(DR("AssignedToName"))
                End If

                DR.EndEdit()
            Next
            Dt.AcceptChanges()
            Return Dt
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetLog(ByVal TaskNo As String, Optional ByVal FormName As String = "") As DataTable
        Dim dt As New DataTable
        Dim str As String = String.Empty
        Try

            str = " Select LogID, LogActivityName, LogUserID, LogRecordType, LogRecordRefNo, " _
                & " LogDateTime, LogComments,  LogSystem, LogFormName From tblActivityLog Where  LogRecordRefNo= N'" & TaskNo.Trim.Replace("'", "''") & "' And LogFormName =N'" & FormName.Trim.Replace("'", "''") & "' "
            dt = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    '    ID	int	Unchecked
    'Description	nvarchar(500)	Checked
    'Type	nvarchar(50)	Checked
    'Date	datetime	Checked
    'Time	datetime	Checked
    'TaskId	int	Checked
    '		Unchecked
    Public Function GetTaskActivity(ByVal TaskID As Integer) As DataTable
        Dim dt As New DataTable
        Dim str As String = String.Empty
        Try
            'ID()
            'TypeID()
            'Dated()
            'Description()
            'Time()
            'TaskID()

            'str = " Select tblTaskActivityUpdate.ID, tblTaskActivityUpdate.Description, tblTaskActivityType.Description As TypeDescription , tblTaskActivityUpdate.TypeID, tblTaskActivityUpdate.Dated, tblTaskActivityUpdate.Time, " _
            '    & " ISNULL(tblTaskActivityUpdate.TaskId, 0) As TaskId From tblTaskActivityUpdate Left Outer Join tblTaskActivityType on tblTaskActivityUpdate.TypeID = tblTaskActivityType.ID Where TaskId= " & TaskID & " "
            str = " Select ID, Dated, TypeID, Time, Description, " _
               & " ISNULL(TaskId, 0) As TaskId From tblTaskActivityUpdate Where TaskId = " & TaskID & ""
            dt = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function GetTaskActivities() As DataTable
        Dim dt As New DataTable
        Dim str As String = String.Empty
        Try
            'ID()
            'TypeID()
            'Dated()
            'Description()
            'Time()
            'TaskID()

            'str = " Select tblTaskActivityUpdate.ID, tblTaskActivityUpdate.Description, tblTaskActivityType.Description As TypeDescription , tblTaskActivityUpdate.TypeID, tblTaskActivityUpdate.Dated, tblTaskActivityUpdate.Time, " _
            '    & " ISNULL(tblTaskActivityUpdate.TaskId, 0) As TaskId From tblTaskActivityUpdate Left Outer Join tblTaskActivityType on tblTaskActivityUpdate.TypeID = tblTaskActivityType.ID Where TaskId= " & TaskID & " "
            str = " Select ID, TypeID, Dated, Time, Description, " _
               & " ISNULL(TaskId, 0) As TaskId From tblTaskActivityUpdate "
            dt = UtilityDAL.GetDataTable(str)
            dt.AcceptChanges()
            Return dt

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Sub AddTaskActivity(ByVal model As TaskActivity)
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        'Dim ActivityLog As New ActivityLog
        Dim trans As SqlTransaction
        Try
            '            ID	int	Unchecked
            'Description	nvarchar(500)	Checked
            'Type	nvarchar(50)	Checked
            'Date	datetime	Checked
            'Time	datetime	Checked
            'TaskId	int	Checked
            '            Unchecked()
            If Not Con.State = 1 Then Con.Open()
            trans = Con.BeginTransaction
            Dim str As String = String.Empty

            'str = "Delete from tblTaskActivityUpdate where TaskId = " & model.TaskID & ""
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)




            str = "Insert into tblTaskActivityUpdate(Description, TypeID, Dated, Time, TaskId) Values( N'" & model.Description.Trim.Replace("'", "''") & "', " & model.Type & "," & IIf(model.Dated = Date.MinValue, "NULL", "'" & model.Dated.ToString("yyyy-M-d hh:mm:ss tt") & "'") & ",  N'" & model.Timespent.Trim.Replace("'", "''") & "', " & model.TaskID & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'ActivityLog.ActivityName = "TaskActivitySave"
            'ActivityLog.RecordType = "CRM"
            'ActivityLog.RefNo = model.TaskID
            'UtilityDAL.BuildActivityLog(ActivityLog, trans)

            trans.Commit()
        Catch ex As Exception
            Throw ex
            trans.Rollback()
        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try

    End Sub
    Public Sub DeleteTaskActivity(ByVal activityID As Integer)
        Dim Con As New SqlConnection(SQLHelper.CON_STR)

        Dim trans As SqlTransaction
        Try
            '            ID	int	Unchecked
            'Description	nvarchar(500)	Checked
            'Type	nvarchar(50)	Checked
            'Date	datetime	Checked
            'Time	datetime	Checked
            'TaskId	int	Checked
            '            Unchecked()
            If Not Con.State = 1 Then Con.Open()
            trans = Con.BeginTransaction
            Dim str As String = String.Empty

            str = "Delete from tblTaskActivityUpdate where ID = " & activityID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)






            trans.Commit()
        Catch ex As Exception
            Throw ex
            trans.Rollback()
        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try

    End Sub
    Public Sub AddActivityType(ByVal model As ActivityType)
        Dim Con As New SqlConnection(SQLHelper.CON_STR)

        Dim trans As SqlTransaction
        Try
            '           
            If Not Con.State = 1 Then Con.Open()
            trans = Con.BeginTransaction
            Dim str As String = String.Empty
            '            ID	int	Unchecked
            'Definition	nvarchar(50)	Checked
            'Remarks	nvarchar(300)	Checked
            '            Unchecked()
            str = "Insert into tblTaskActivityType(Description, Remarks) Values( N'" & model.Description.Trim.Replace("'", "''") & "', N'" & model.Remarks.Trim.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
        Catch ex As Exception
            Throw ex
            trans.Rollback()
        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try

    End Sub
    'Public Function AllActivityTypes() As List(Of ActivityType)
    '    Dim dt As New DataTable
    '    Dim list As New List(Of ActivityType)
    '    Dim obj As ActivityType
    '    Try
    '        '            ID	int	Unchecked
    '        'Description	nvarchar(50)	Checked
    '        'Remarks	nvarchar(300)	Checked
    '        '            Unchecked()
    '        Dim sql = " Select ID, Description FROM tblTaskActivityType"
    '        dt = UtilityDAL.GetDataTable(sql)
    '        dt.AcceptChanges()
    '        For Each DR As DataRow In dt.Rows
    '            obj = New ActivityType
    '            obj.ID = Val(DR("ID").ToString)
    '            obj.Description = DR("Description").ToString
    '            'obj.Remarks = DR("Remarks").ToString
    '            list.Add(obj)
    '        Next
    '        Return list
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Public Function GetSingleActivityType(ByVal typeID As Integer) As List(Of ActivityType)
        Dim dt As New DataTable
        Dim list As New List(Of ActivityType)
        Dim obj As ActivityType
        Try
            '            ID	int	Unchecked
            'Description	nvarchar(50)	Checked
            'Remarks	nvarchar(300)	Checked
            '            Unchecked()
            Dim sql = " Select ID, Description FROM tblTaskActivityType Where ID =" & typeID & ""
            dt = UtilityDAL.GetDataTable(sql)
            dt.AcceptChanges()
            For Each DR As DataRow In dt.Rows
                obj = New ActivityType
                obj.ID = Val(DR("ID").ToString)
                obj.Description = DR("Description").ToString
                'obj.Remarks = DR("Remarks").ToString
                list.Add(obj)
            Next
            Return list
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '    ID	int	Unchecked
    'TypeID	int	Checked
    'Description	nvarchar(500)	Checked
    'Dated	datetime	Checked
    'Time	datetime	Checked
    'TaskId	int	Checked
    '		Unchecked
    Public Sub UpdateActivityDescription(ByVal ID As Integer, ByVal description As String)
        Dim Con As New SqlConnection(SQLHelper.CON_STR)

        Dim trans As SqlTransaction
        Try
            '           
            If Not Con.State = 1 Then Con.Open()
            trans = Con.BeginTransaction
            Dim str As String = String.Empty
            '            ID	int	Unchecked
            'Definition	nvarchar(50)	Checked
            'Remarks	nvarchar(300)	Checked
            '            Unchecked()
            str = "Update tblTaskActivityUpdate Set Description = N'" & description.Trim.Replace("'", "''") & "' Where ID = " & ID & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
        Catch ex As Exception
            Throw ex
            trans.Rollback()
        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try

    End Sub
    Public Sub UpdateActivityDated(ByVal ID As Integer, ByVal Dated As DateTime)
        Dim Con As New SqlConnection(SQLHelper.CON_STR)

        Dim trans As SqlTransaction
        Try
            '           
            If Not Con.State = 1 Then Con.Open()
            trans = Con.BeginTransaction
            Dim str As String = String.Empty
            '            ID	int	Unchecked
            'Definition	nvarchar(50)	Checked
            'Remarks	nvarchar(300)	Checked
            '            Unchecked()
            str = "Update tblTaskActivityUpdate Set Dated = '" & Dated.ToString("yyyy-M-d hh:mm:ss tt") & "' Where ID = " & ID & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
        Catch ex As Exception
            Throw ex
            trans.Rollback()
        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try

    End Sub
    Public Sub UpdateActivityTime(ByVal ID As Integer, ByVal Time As String)
        Dim Con As New SqlConnection(SQLHelper.CON_STR)

        Dim trans As SqlTransaction
        Try
            '           
            If Not Con.State = 1 Then Con.Open()
            trans = Con.BeginTransaction
            Dim str As String = String.Empty
            '            ID	int	Unchecked
            'Definition	nvarchar(50)	Checked
            'Remarks	nvarchar(300)	Checked
            '            Unchecked()
            str = "Update tblTaskActivityUpdate Set Time = N'" & Time.Trim.Replace("'", "''") & "' Where ID = " & ID & " "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
        Catch ex As Exception
            Throw ex
            trans.Rollback()
        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try

    End Sub
    Public Sub UpdateActivityType(ByVal TypeID As Integer, ByVal ID As Integer)
        Dim Con As New SqlConnection(SQLHelper.CON_STR)

        Dim trans As SqlTransaction
        Try
            '           
            If Not Con.State = 1 Then Con.Open()
            trans = Con.BeginTransaction
            Dim str As String = String.Empty
            '            ID	int	Unchecked
            'Definition	nvarchar(50)	Checked
            'Remarks	nvarchar(300)	Checked
            '            Unchecked()
            str = "Update tblTaskActivityUpdate Set TypeID = " & TypeID & " Where ID = " & ID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

            trans.Commit()
        Catch ex As Exception
            Throw ex
            trans.Rollback()
        Finally
            If Not Con Is Nothing AndAlso Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try

    End Sub
    Public Function AllActivityTypes() As List(Of ActivityType)
        Dim dt As New DataTable
        Dim list As New List(Of ActivityType)
        Dim obj As ActivityType
        Try
            '            ID	int	Unchecked
            'Description	nvarchar(50)	Checked
            'Remarks	nvarchar(300)	Checked
            '            Unchecked()
            Dim sql = " Select * FROM tblTaskActivityType"
            dt = UtilityDAL.GetDataTable(sql)
            dt.AcceptChanges()
            For Each DR As DataRow In dt.Rows
                obj = New ActivityType
                obj.ID = Val(DR("ID").ToString)
                obj.Description = DR("Description").ToString
                obj.Remarks = DR("Remarks").ToString
                list.Add(obj)
            Next
            Return list
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

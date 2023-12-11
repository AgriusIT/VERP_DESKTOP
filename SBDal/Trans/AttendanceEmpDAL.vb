'Mughees 26-4-2014 tASK nO 2593 Updating The Functions Aginst The Newly Adde Fields
'Ali Ansari 21-05-2015 Task#20150512 Attendance of all employees
Imports System
Imports System.Data
Imports SBDal
Imports SBModel
Imports SBUtility.Utility

Public Class AttendanceEmpDAL
    Public Function AddAttendance(ByVal EmpAtt As AttendanceEmp) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try

            Dim str As String = String.Empty
            'Task No 2593 uPDATE The Add Function Too Aginst The Task 
            'str = "Insert Into tblAttendanceDetail(EmpID, AttendanceDate, AttendanceType, AttendanceTime, AttendanceStatus, ShiftId) Values(" & EmpAtt.EmpID & ", N'" & EmpAtt.AttendanceDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(EmpAtt.AttendanceType = Nothing, "NULL", "N'" & EmpAtt.AttendanceType & "'") & ", " & IIf(EmpAtt.AttendanceTime = Nothing, "NULL", "N'" & EmpAtt.AttendanceTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & EmpAtt.AttendanceStatus & "'," & IIf(EmpAtt.ShiftId > 0, EmpAtt.ShiftId, "NULL")  & "' ) Select @@Identity"

            str = "Insert Into tblAttendanceDetail(EmpID, AttendanceDate, AttendanceType, AttendanceTime, AttendanceStatus, ShiftId,Flexibility_In_Time,Flexibility_Out_Time,Sch_In_Time,Sch_Out_Time,Auto) Values(" & EmpAtt.EmpID & ", N'" & EmpAtt.AttendanceDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(EmpAtt.AttendanceType = Nothing, "NULL", "N'" & EmpAtt.AttendanceType & "'") & ", " & IIf(EmpAtt.AttendanceTime = Nothing, "NULL", "N'" & EmpAtt.AttendanceTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & EmpAtt.AttendanceStatus & "'," & IIf(EmpAtt.ShiftId > 0, EmpAtt.ShiftId, "NULL") & ", N'" & EmpAtt.FlexiblityInTime.ToString & " ', N'" & EmpAtt.FlexiblityOutTime.ToString & " ', N'" & EmpAtt.SchInTime.ToString & " ', N'" & EmpAtt.SchOutTime.ToString & "' ," & IIf(EmpAtt.Auto = True, 1, 0) & " ) Select @@Identity"

            'Task End
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)


            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally

            Con.Close()
        End Try
    End Function
    'Task#20150512 Attendance of all employees
    Public Function AddAttendanceAll(ByVal EmpAtt As AttendanceEmp) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Insert Into tblAttendanceDetail(EmpID, AttendanceDate, AttendanceType, AttendanceTime, AttendanceStatus, ShiftId,Flexibility_In_Time,Flexibility_Out_Time,Sch_In_Time,Sch_Out_Time) " _
            & " select employee_id, N'" & EmpAtt.AttendanceDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(EmpAtt.AttendanceType = Nothing, "NULL", "N'" & EmpAtt.AttendanceType & "'") & ", " & IIf(EmpAtt.AttendanceTime = Nothing, "NULL", "N'" & EmpAtt.AttendanceTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & EmpAtt.AttendanceStatus & "',d.shiftid, d.FlexInTime, d.FlexoutTime, ShiftStartTime, ShiftEndTime from tbldefemployee a " _
            & " left join EmployeeDeptDefTable b on a.dept_id = b.employeedeptid " _
            & " left join EmployeeDesignationDefTable c on a.desig_id = c.employeedesignationid " _
            & " left join ShiftTable d on a.shiftgroupid = d.shiftid " _
            & " where a.active = 1  Select @@Identity "
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally

            Con.Close()
        End Try
    End Function
    'Task#20150512 Attendance of all employees
   



    Public Function UpdateAttendance(ByVal EmpAtt As AttendanceEmp) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'str = "Update tblAttendanceDetail Set EmpID=" & EmpAtt.EmpID & ", AttendanceDate=N'" & EmpAtt.AttendanceDate.ToString("yyyy-M-d h:mm:ss tt") & "', AttendanceType=" & IIf(EmpAtt.AttendanceType = Nothing, "NULL", "N'" & EmpAtt.AttendanceType & "'") & ", AttendanceTime=" & IIf(EmpAtt.AttendanceTime = Nothing, "NULL", "N'" & EmpAtt.AttendanceTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", AttendanceStatus=N'" & EmpAtt.AttendanceStatus & "', ShiftId=" & IIf(EmpAtt.ShiftId > 0, EmpAtt.ShiftId, "NULL & "' ) WHERE AttendanceId=" & EmpAtt.AttendanceId & "")
            'Task No 2593 Update The Update Query Aginst The Newly Added Fields
            str = "Update tblAttendanceDetail Set EmpID=" & EmpAtt.EmpID & ", AttendanceDate=N'" & EmpAtt.AttendanceDate.ToString("yyyy-M-d h:mm:ss tt") & "', AttendanceType=" & IIf(EmpAtt.AttendanceType = Nothing, "NULL", "N'" & EmpAtt.AttendanceType & "'") & ", AttendanceTime=" & IIf(EmpAtt.AttendanceTime = Nothing, "NULL", "N'" & EmpAtt.AttendanceTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", AttendanceStatus=N'" & EmpAtt.AttendanceStatus & "', ShiftId=" & IIf(EmpAtt.ShiftId > 0, EmpAtt.ShiftId, "NULL") & ",Flexibility_In_Time=N'" & EmpAtt.FlexiblityInTime.ToString & "' ,Flexibility_Out_Time=N'" & EmpAtt.FlexiblityOutTime.ToString & "' ,Auto=" & IIf(EmpAtt.Auto = True, 1, 0) & ",Sch_In_Time=N'" & EmpAtt.SchInTime.ToString & "',Sch_Out_Time=N'" & EmpAtt.SchOutTime.ToString & "' WHERE AttendanceId=" & EmpAtt.AttendanceId & ""

            'End Tsk
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function DeleteAttendance(ByVal EmpAtt As AttendanceEmp) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete From tblAttendanceDetail WHERE AttendanceId=" & EmpAtt.AttendanceId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            trans.Commit()
            Con.Close()
        End Try
    End Function
    Public Function GetAllRecords(Optional ByVal Condition As String = "", Optional ByVal HaveCostCenterRights As Boolean = False, Optional ByVal LoginUserId As Integer = 0) As DataTable
        Try
            Dim str As String = String.Empty
            'str = "    SELECT tblAttendanceDetail.AttendanceId, tblAttendanceDetail.EmpId, tblDefEmployee.Employee_Name, " _
            '    & "    EmployeeDesignationDefTable.EmployeeDesignationName AS Designation, EmployeeDeptDefTable.EmployeeDeptName AS Department,  " _
            '    & "    Convert(Datetime, LEFT(tblAttendanceDetail.AttendanceDate,11),102) AS AttendanceDate, tblAttendanceDetail.AttendanceType, tblAttendanceDetail.AttendanceTime, tblAttendanceDetail.ShiftId, ShiftTable.ShiftName, tblAttendanceDetail.AttendanceStatus " _
            '    & "    FROM tblAttendanceDetail LEFT OUTER JOIN " _
            '    & "    tblDefEmployee ON tblAttendanceDetail.EmpId = tblDefEmployee.Employee_ID LEFT OUTER JOIN " _
            '    & "    EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId LEFT OUTER JOIN " _
            '    & "    EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId LEFT OUTER JOIN ShiftTable ON ShiftTable.ShiftId = tblAttendanceDetail.ShiftId"
            'Task No 2593 Update The Update Query Aginst The Newly Added Fields
            str = "    SELECT tblAttendanceDetail.AttendanceId, tblAttendanceDetail.EmpId, tblDefEmployee.Employee_Name, " _
                   & "    EmployeeDesignationDefTable.EmployeeDesignationName AS Designation, EmployeeDeptDefTable.EmployeeDeptName AS Department,  " _
                   & "    Convert(Datetime, LEFT(tblAttendanceDetail.AttendanceDate,11),102) AS AttendanceDate, tblAttendanceDetail.Auto,tblAttendanceDetail.Sch_In_Time,tblAttendanceDetail.Sch_Out_Time,tblAttendanceDetail.Flexibility_In_Time,tblAttendanceDetail.Flexibility_Out_Time, tblAttendanceDetail.AttendanceType, tblAttendanceDetail.AttendanceTime, tblAttendanceDetail.ShiftId, ShiftTable.ShiftName, tblAttendanceDetail.AttendanceStatus " _
                   & "    FROM tblAttendanceDetail LEFT OUTER JOIN " _
                   & "    tblDefEmployee ON tblAttendanceDetail.EmpId = tblDefEmployee.Employee_ID LEFT OUTER JOIN " _
                   & "    EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId LEFT OUTER JOIN " _
                   & "    EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId LEFT OUTER JOIN ShiftTable ON ShiftTable.ShiftId = tblAttendanceDetail.ShiftId" _
                   & "    WHERE tblDefEmployee.Active = 1  "
            'End Task

            If HaveCostCenterRights = True Then
                str += " AND tblDefEmployee.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ") "
            End If
            If Not Condition = "All" Then
                str += " AND (Convert(varchar, tblAttendanceDetail.AttendanceDate,102) = convert(varchar, getDate(),102)) "
            End If

            ''TASK TFS3568
            'If HaveCostCenterRights = True Then
            '    str += " AND tblDefEmployee.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ") "
            'End If
            ''END TASK TFS6538
            str += " ORDER BY dbo.tblAttendanceDetail.AttendanceDate,dbo.tblAttendanceDetail.AttendanceTime ASC "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetOnCriteria(ByVal fromDate As DateTime, ByVal toDate As DateTime, ByVal HaveCostCenterRights As Boolean, ByVal LoginUserId As Integer, Optional ByVal designationID As Integer = 0, Optional ByVal employeeID As Integer = 0, Optional ByVal departmentID As Integer = 0, Optional ByVal CostCenterId As Integer = 0) As DataTable
        Try
            Dim str As String = String.Empty
            'str = "    SELECT tblAttendanceDetail.AttendanceId, tblAttendanceDetail.EmpId, tblDefEmployee.Employee_Name, " _
            '    & "    EmployeeDesignationDefTable.EmployeeDesignationName AS Designation, EmployeeDeptDefTable.EmployeeDeptName AS Department,  " _
            '    & "    Convert(Datetime, LEFT(tblAttendanceDetail.AttendanceDate,11),102) AS AttendanceDate, tblAttendanceDetail.AttendanceType, tblAttendanceDetail.AttendanceTime, tblAttendanceDetail.ShiftId, ShiftTable.ShiftName, tblAttendanceDetail.AttendanceStatus " _
            '    & "    FROM tblAttendanceDetail LEFT OUTER JOIN " _
            '    & "    tblDefEmployee ON tblAttendanceDetail.EmpId = tblDefEmployee.Employee_ID LEFT OUTER JOIN " _
            '    & "    EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId LEFT OUTER JOIN " _
            '    & "    EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId LEFT OUTER JOIN ShiftTable ON ShiftTable.ShiftId = tblAttendanceDetail.ShiftId"
            'Task No 2593 Update The Update Query Aginst The Newly Added Fields
            str = "    SELECT tblAttendanceDetail.AttendanceId, tblAttendanceDetail.EmpId, tblDefEmployee.Employee_Name, " _
                   & "    EmployeeDesignationDefTable.EmployeeDesignationName AS Designation, EmployeeDeptDefTable.EmployeeDeptName AS Department,  " _
                   & "    Convert(Datetime, LEFT(tblAttendanceDetail.AttendanceDate,11),102) AS AttendanceDate, tblAttendanceDetail.Auto,tblAttendanceDetail.Sch_In_Time,tblAttendanceDetail.Sch_Out_Time,tblAttendanceDetail.Flexibility_In_Time,tblAttendanceDetail.Flexibility_Out_Time, tblAttendanceDetail.AttendanceType, tblAttendanceDetail.AttendanceTime, tblAttendanceDetail.ShiftId, ShiftTable.ShiftName, tblAttendanceDetail.AttendanceStatus " _
                   & "    FROM tblAttendanceDetail LEFT OUTER JOIN " _
                   & "    tblDefEmployee ON tblAttendanceDetail.EmpId = tblDefEmployee.Employee_ID LEFT OUTER JOIN " _
                   & "    EmployeeDesignationDefTable ON tblDefEmployee.Desig_ID = EmployeeDesignationDefTable.EmployeeDesignationId LEFT OUTER JOIN " _
                   & "    EmployeeDeptDefTable ON tblDefEmployee.Dept_ID = EmployeeDeptDefTable.EmployeeDeptId LEFT OUTER JOIN ShiftTable ON ShiftTable.ShiftId = tblAttendanceDetail.ShiftId"
            'End Task

            str += " WHERE (Convert(varchar, tblAttendanceDetail.AttendanceDate,102) Between Convert(DateTime, '" & fromDate.ToString("yyyy-M-d 00:00:00") & "', 102) And Convert(DateTime, '" & toDate.ToString("yyyy-M-d 23:59:59") & "', 102)) "
            If designationID > 0 Then
                str += " And tblDefEmployee.Desig_ID = " & designationID & ""
            End If
            If employeeID > 0 Then
                str += " And tblAttendanceDetail.EmpId = " & employeeID & ""
            End If
            If departmentID > 0 Then
                str += " And tblDefEmployee.Dept_ID  = " & departmentID & ""
            End If

            If HaveCostCenterRights = True Then
                str += " AND tblDefEmployee.CostCentre IN (SELECT CostCentre_Id FROM tblUserCostCentreRights WHERE UserID = " & LoginUserId & ") "
            End If

            '' TASK TFS3568
            'If CostCenterId > 0 Then
            '    str += " And tblDefEmployee.CostCentre  = " & CostCenterId & ""
            'End If
            ''END TASK TFS3568
            str += " ORDER BY dbo.tblAttendanceDetail.AttendanceDate,dbo.tblAttendanceDetail.AttendanceTime ASC "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAttendanceStatusList() As List(Of AttendanceStatus)
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("Select * from tblDefAttendenceStatus Order By SortOrder ASC")
            dt.AcceptChanges()
            'Dim dr() As DataRow
            Dim objList As New List(Of AttendanceStatus)
            For Each r As DataRow In dt.Rows
                Dim obj As New AttendanceStatus
                obj.Att_Status_ID = Val(r.Item("Att_Status_ID").ToString)
                obj.Att_Status_Code = r.Item("Att_Status_Code").ToString
                obj.Att_Status_Name = r.Item("Att_Status_Name").ToString
                obj.SortORder = Val(r.Item("SortOrder").ToString)
                If IsDBNull(r.Item("Active")) Then
                    obj.Active = False
                Else
                    obj.Active = r.Item("Active")
                End If
                objList.Add(obj)
            Next
            Return objList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AddAttendanceRegister(ByVal EmpAtt As AttendanceEmp) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'str = " Update tblAttendanceDetail Set AttendanceStatus=N'" & EmpAtt.AttendanceStatus & "' WHERE EmpID =" & EmpAtt.EmpID & " And Convert(DateTime, AttendanceDate, 102) = Convert(DateTime, '" & EmpAtt.AttendanceDate.ToString("yyyy-M-d 00:00:00") & "', 102) "
            'str = " If Exists(Select AttendanceId From tblAttendanceDetail Where EmpID =" & EmpAtt.EmpID & " And Convert(DateTime, AttendanceDate, 102) = Convert(DateTime, '" & EmpAtt.AttendanceDate.ToString("yyyy-M-d 00:00:00") & "', 102)) Update tblAttendanceDetail Set AttendanceStatus=N'" & EmpAtt.AttendanceStatus & "' WHERE EmpID =" & EmpAtt.EmpID & " And Convert(DateTime, AttendanceDate, 102) = Convert(DateTime, '" & EmpAtt.AttendanceDate.ToString("yyyy-M-d 00:00:00") & "', 102) "
            str = " If Not Exists(Select AttendanceId From tblAttendanceDetail Where EmpID =" & EmpAtt.EmpID & " And Convert(DateTime, AttendanceDate, 102) = Convert(DateTime, '" & EmpAtt.AttendanceDate.ToString("yyyy-M-d 00:00:00") & "', 102)) Insert Into tblAttendanceDetail(EmpID, AttendanceDate, AttendanceType, AttendanceTime, AttendanceStatus, ShiftId,Flexibility_In_Time,Flexibility_Out_Time,Sch_In_Time,Sch_Out_Time,Auto) Values(" & EmpAtt.EmpID & ", N'" & EmpAtt.AttendanceDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(EmpAtt.AttendanceType = Nothing, "NULL", "N'" & EmpAtt.AttendanceType & "'") & ", " & IIf(EmpAtt.AttendanceTime = Nothing, "NULL", "N'" & EmpAtt.AttendanceTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & EmpAtt.AttendanceStatus & "'," & IIf(EmpAtt.ShiftId > 0, EmpAtt.ShiftId, "NULL") & ", N'" & EmpAtt.FlexiblityInTime.ToString & " ', N'" & EmpAtt.FlexiblityOutTime.ToString & " ', N'" & EmpAtt.SchInTime.ToString & " ', N'" & EmpAtt.SchOutTime.ToString & "' ," & IIf(EmpAtt.Auto = True, 1, 0) & " ) "
            '& " Else Update tblAttendanceDetail Set AttendanceStatus=N'" & EmpAtt.AttendanceStatus & "' WHERE EmpID =" & EmpAtt.EmpID & " And Convert(DateTime, AttendanceDate, 102) = Convert(DateTime, '" & EmpAtt.AttendanceDate.ToString("yyyy-M-d 00:00:00") & "', 102) Select @@Identity"
            '& " Else Update tblAttendanceDetail Set EmpID=" & EmpAtt.EmpID & ", AttendanceDate=N'" & EmpAtt.AttendanceDate.ToString("yyyy-M-d h:mm:ss tt") & "', AttendanceType=" & IIf(EmpAtt.AttendanceType = Nothing, "NULL", "N'" & EmpAtt.AttendanceType & "'") & ", AttendanceTime=" & IIf(EmpAtt.AttendanceTime = Nothing, "NULL", "N'" & EmpAtt.AttendanceTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", AttendanceStatus=N'" & EmpAtt.AttendanceStatus & "', ShiftId=" & IIf(EmpAtt.ShiftId > 0, EmpAtt.ShiftId, "NULL") & ",Flexibility_In_Time=N'" & EmpAtt.FlexiblityInTime.ToString & "' ,Flexibility_Out_Time=N'" & EmpAtt.FlexiblityOutTime.ToString & "' ,Auto=" & IIf(EmpAtt.Auto = True, 1, 0) & ",Sch_In_Time=N'" & EmpAtt.SchInTime.ToString & "',Sch_Out_Time=N'" & EmpAtt.SchOutTime.ToString & "' WHERE EmpID =" & EmpAtt.EmpID & " And Convert(DateTime, AttendanceDate, 102) = Convert(DateTime, '" & EmpAtt.AttendanceDate.ToString("yyyy-M-d 00:00:00") & "', 102) Select @@Identity"
            'Task End
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally

            Con.Close()
        End Try
    End Function
    Public Function AddSingle(ByVal EmpAtt As AttendanceEmp) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            'str = " If Exists(Select AttendanceId From tblAttendanceDetail Where EmpID =" & EmpAtt.EmpID & " And Convert(DateTime, AttendanceDate, 102) = Convert(DateTime, '" & EmpAtt.AttendanceDate.ToString("yyyy-M-d 00:00:00") & "', 102)) Update tblAttendanceDetail Set AttendanceStatus=N'" & EmpAtt.AttendanceStatus & "' WHERE EmpID =" & EmpAtt.EmpID & " And Convert(DateTime, AttendanceDate, 102) = Convert(DateTime, '" & EmpAtt.AttendanceDate.ToString("yyyy-M-d 00:00:00") & "', 102) "
            str = " If Not Exists(Select AttendanceStatus From tblAttendanceDetail Where EmpID =" & EmpAtt.EmpID & " And Convert(DateTime, AttendanceDate, 102) = Convert(DateTime, '" & EmpAtt.AttendanceDate.ToString("yyyy-M-d 00:00:00") & "', 102) And AttendanceStatus <> 'Present') Insert Into tblAttendanceDetail(EmpID, AttendanceDate, AttendanceType, AttendanceTime, AttendanceStatus, ShiftId,Flexibility_In_Time,Flexibility_Out_Time,Sch_In_Time,Sch_Out_Time,Auto) Values(" & EmpAtt.EmpID & ", N'" & EmpAtt.AttendanceDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(EmpAtt.AttendanceType = Nothing, "NULL", "N'" & EmpAtt.AttendanceType & "'") & ", " & IIf(EmpAtt.AttendanceTime = Nothing, "NULL", "N'" & EmpAtt.AttendanceTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", N'" & EmpAtt.AttendanceStatus & "'," & IIf(EmpAtt.ShiftId > 0, EmpAtt.ShiftId, "NULL") & ", N'" & EmpAtt.FlexiblityInTime.ToString & " ', N'" & EmpAtt.FlexiblityOutTime.ToString & " ', N'" & EmpAtt.SchInTime.ToString & " ', N'" & EmpAtt.SchOutTime.ToString & "' ," & IIf(EmpAtt.Auto = True, 1, 0) & " ) " _
                & " Else Update tblAttendanceDetail Set AttendanceStatus=N'" & EmpAtt.AttendanceStatus & "', AttendanceTime = " & IIf(EmpAtt.AttendanceTime = Nothing, "NULL", "N'" & EmpAtt.AttendanceTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & " WHERE EmpID =" & EmpAtt.EmpID & " And Convert(DateTime, AttendanceDate, 102) = Convert(DateTime, '" & EmpAtt.AttendanceDate.ToString("yyyy-M-d 00:00:00") & "', 102) And AttendanceStatus <> 'Present' "
            '& " Else Update tblAttendanceDetail Set EmpID=" & EmpAtt.EmpID & ", AttendanceDate=N'" & EmpAtt.AttendanceDate.ToString("yyyy-M-d h:mm:ss tt") & "', AttendanceType=" & IIf(EmpAtt.AttendanceType = Nothing, "NULL", "N'" & EmpAtt.AttendanceType & "'") & ", AttendanceTime=" & IIf(EmpAtt.AttendanceTime = Nothing, "NULL", "N'" & EmpAtt.AttendanceTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", AttendanceStatus=N'" & EmpAtt.AttendanceStatus & "', ShiftId=" & IIf(EmpAtt.ShiftId > 0, EmpAtt.ShiftId, "NULL") & ",Flexibility_In_Time=N'" & EmpAtt.FlexiblityInTime.ToString & "' ,Flexibility_Out_Time=N'" & EmpAtt.FlexiblityOutTime.ToString & "' ,Auto=" & IIf(EmpAtt.Auto = True, 1, 0) & ",Sch_In_Time=N'" & EmpAtt.SchInTime.ToString & "',Sch_Out_Time=N'" & EmpAtt.SchOutTime.ToString & "' WHERE EmpID =" & EmpAtt.EmpID & " And Convert(DateTime, AttendanceDate, 102) = Convert(DateTime, '" & EmpAtt.AttendanceDate.ToString("yyyy-M-d 00:00:00") & "', 102) Select @@Identity"
            'Task End
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
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

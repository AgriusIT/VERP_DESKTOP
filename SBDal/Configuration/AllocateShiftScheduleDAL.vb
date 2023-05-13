Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class AllocateShiftScheduleDAL
    Public Function Add(ByVal AllocateShift As List(Of AllocateShiftSchedule)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            For Each AllocationShiftDt As AllocateShiftSchedule In AllocateShift
                If ExistingRecord(AllocationShiftDt.ShiftGroupId, trans) = True Then
                    str = String.Empty
                    'Ali Faisal : Querry modified to Save ShiftId while Allocation the Shift with shift group on 25-Jan-2017.
                    'str = "UPDATE ShiftScheduleTable Set ShiftGroupId=" & AllocationShiftDt.ShiftGroupId & ", LoginUserId=" & AllocationShiftDt.LoginUserId & ", DateTimeLog=getDate() WHERE ShiftGroupId=" & AllocationShiftDt.ShiftGroupId
                    str = "UPDATE ShiftScheduleTable Set ShiftId=" & AllocationShiftDt.ShiftId & ", LoginUserId=" & AllocationShiftDt.LoginUserId & ", DateTimeLog=getDate() WHERE ShiftGroupId=" & AllocationShiftDt.ShiftGroupId
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Else
                    str = String.Empty
                    str = "INSERT INTO ShiftScheduleTable(ShiftId, ShiftGroupId, LoginUserId, DateTimeLog) " _
                    & " Values(" & Val(AllocationShiftDt.ShiftId) & ", " & Val(AllocationShiftDt.ShiftGroupId) & ", " & Val(AllocationShiftDt.LoginUserId) & ", getDate()) Select @@Identity"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
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
    Public Function GetAll() As DataTable
        Try
            Return UtilityDAL.GetDataTable("SELECT a.ShiftGroupId, a.ShiftGroupName, b.ShiftId FROM dbo.ShiftGroupTable a LEFT OUTER JOIN  dbo.ShiftScheduleTable b ON a.ShiftGroupId = b.ShiftGroupId")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Delete(ByVal ShiftGroupId As Integer, ByVal ShiftId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            Dim flg As Boolean = False
            str = "Select ShiftGroupId from tblDefEmployee WHERE ShiftGroupId=" & ShiftGroupId
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    flg = True
                End If
            End If
            If Not flg = True Then
                str = "Delete From ShiftScheduleTable WHERE ShiftGroupId=" & ShiftGroupId & " AND ShiftId=" & ShiftId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Else
                Return False
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
    Private Function ExistingRecord(ByVal ShiftGroupId As Integer, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim str As String = "Select * From ShiftScheduleTable WHERE ShiftGroupId=" & ShiftGroupId
            Dim dt As DataTable = UtilityDAL.GetDataTable(str, trans)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
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
End Class

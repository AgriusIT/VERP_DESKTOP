'25-Jun-2015 Task#125062015 Ahmad Sharif : add two columns in insert & update query ,Columns are (DateLock_Type and NoOfDays)

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBDal.UtilityDAL
Imports SBModel
Imports SBUtility.Utility
Public Class DateLockDAL
    Public Function Add(ByVal DateLock As DateLockBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strQuery As String = String.Empty


            If DateLock.Lock = True Then
                strQuery = "Select DateLockId From tblDateLock WHERE IsNull(Lock,0)=1 AND (Convert(varchar,DateLock,102) > Convert(Datetime,'" & DateLock.DateLock.ToString("yyyy-M-d 00:00:00") & "',102)) "
                Dim intId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strQuery)
                If intId > 0 Then
                    Throw New Exception("You can't go beyond the current date lock.")
                End If
            End If


            'Task#125062015 Add Two Columns in query Columns are (DateLock_Type and NoOfDays)
            strQuery = "INSERT INTO tblDateLock(DateLock, Lock, EntryDate,Username,DateLock_Type,NoOfDays) Values(N'" & DateLock.DateLock & "', " & IIf(DateLock.Lock = True, 1, 0) & ", N'" & DateLock.EntryDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & DateLock.Username & "', N'" & DateLock.DateLockType & "'," & DateLock.NoOfDays & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            strQuery = String.Empty
            strQuery = "Update ConfigValuesTable SET Config_Value=N'" & DateLock.DateLock & "', IsActive=" & IIf(DateLock.Lock = True, 1, 0) & " WHERE Config_Type='DateLock'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal DateLock As DateLockBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            'Task#125062015 add Two Columns in query Columns are (DateLock_Type and NoOfDays)
            Dim strQuery As String = String.Empty


            If DateLock.Lock = True Then
                strQuery = "Select DateLockId From tblDateLock WHERE DateLockId <> " & DateLock.DateLockId & " AND IsNull(Lock,0)=1 AND (Convert(varchar,DateLock,102) > Convert(Datetime,'" & DateLock.DateLock.ToString("yyyy-M-d 00:00:00") & "',102)) "
                Dim intId As Integer = SQLHelper.ExecuteScaler(trans, CommandType.Text, strQuery)
                If intId > 0 Then
                    Throw New Exception("You can't go beyond the current date lock.")
                End If
            End If

            strQuery = "UPDATE tblDateLock  SET DateLock=N'" & DateLock.DateLock & "', Lock=" & IIf(DateLock.Lock = True, 1, 0) & ", EntryDate=N'" & DateLock.EntryDate.ToString("yyyy-M-d h:mm:ss tt") & "',Username=N'" & DateLock.Username & "',DateLock_Type=N'" & DateLock.DateLockType & "',NoOfDays=" & DateLock.NoOfDays & " WHERE DateLockId=" & DateLock.DateLockId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            strQuery = String.Empty
            strQuery = "Update ConfigValuesTable SET Config_Value=N'" & DateLock.DateLock & "', IsActive=" & IIf(DateLock.Lock = True, 1, 0) & " WHERE Config_Type='DateLock'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Delete(ByVal DateLock As DateLockBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strQuery As String = "Delete From  tblDateLock   WHERE DateLockId=" & DateLock.DateLockId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)


            strQuery = String.Empty
            strQuery = "Update ConfigValuesTable SET Config_Value=IS NULL, IsActive=" & IIf(DateLock.Lock = True, 1, 0) & " WHERE Config_Type='DateLock'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strQuery)

            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Shared Function ExistDateLock(ByVal DateLock As DateTime) As Boolean
        Try

            Dim strQuery As String = "Select * From tblDateLock WHERE (Convert(Varchar, DateLock, 102) = Convert(DateTime, N'" & DateLock.ToString("yyyy-M-d 00:00:00") & "',102))"
            Dim dt As New DataTable
            dt = GetDataTable(strQuery)
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
    Public Shared Function getDateLockId(ByVal DateLock As DateTime) As Integer
        Try
            Dim strQuery As String = "Select DateLockId From tblDateLock WHERE (Convert(Varchar, DateLock, 102) = Convert(DateTime, N'" & DateLock.ToString("yyyy-M-d 00:00:00") & "',102))"
            Dim dt As New DataTable
            dt = GetDataTable(strQuery)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0)(0)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetAllDateLock() As List(Of DateLockBE)
        Try
            Dim DateLockList As New List(Of DateLockBE)
            Dim DateLock As DateLockBE
            Dim strQuery As String = String.Empty
            strQuery = "Select * From tblDateLock WHERE Lock=1"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strQuery, Nothing)
            If dr.HasRows Then
                Do While dr.Read
                    DateLock = New DateLockBE
                    DateLock.DateLockId = dr(0)
                    DateLock.DateLock = dr(1)
                    DateLock.Lock = dr(2)
                    DateLock.EntryDate = dr(3)
                    DateLock.Username = dr(4)
                    DateLockList.Add(DateLock)
                Loop
            End If
            Return DateLockList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

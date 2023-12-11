'Ali Faisal : TFS1529 : DAL for save, update and delete functions
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class EmployeeWarningDAL
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Save records of warning
    ''' </summary>
    ''' <param name="Warning"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal Warning As EmployeeWarningBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert records
            str = "INSERT INTO tblEmployeeWarning (DocNo, DocDate, EmployeeId, WarnById, WarningTypeId, Reason, Description) VALUES(N'" & Warning.DocNo & "','" & Warning.DocDate & "'," & Warning.EmployeeId & "," & Warning.WarnById & "," & Warning.WarningTypeId & ",N'" & Warning.Reason & "',N'" & Warning.Description & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Update records in warning
    ''' </summary>
    ''' <param name="Warning"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal Warning As EmployeeWarningBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update records
            str = "UPDATE tblEmployeeWarning SET DocNo = N'" & Warning.DocNo & "', DocDate = '" & Warning.DocDate & "', EmployeeId = " & Warning.EmployeeId & ", WarnById = " & Warning.WarnById & ", WarningTypeId = " & Warning.WarningTypeId & ", Reason = N'" & Warning.Reason & "', Description = N'" & Warning.Description & "' WHERE WarningId = " & Warning.WarningId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1529 : Delete records from warning
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(ByVal Id As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete records
            str = "DELETE FROM tblEmployeeWarning WHERE WarningId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
End Class

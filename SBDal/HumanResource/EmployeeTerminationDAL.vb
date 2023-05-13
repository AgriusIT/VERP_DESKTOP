'Ali Faisal : TFS1530 : Add DAL functions to save, update, delete and row delete for Employee termination
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class EmployeeTerminationDAL
    ''' <summary>
    ''' Ali Faisal : TFS1530 : Save records of Termination
    ''' </summary>
    ''' <param name="Termination"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal Termination As EmployeeTerminationBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert records
            str = "INSERT INTO tblEmployeeTermination (DocNo, DocDate, NoticeDate, EmployeeId, ApprovedById, TerminationTypeId, Reason, Details) VALUES(N'" & Termination.DocNo & "','" & Termination.DocDate & "','" & Termination.NoticeDate & "'," & Termination.EmployeeId & "," & Termination.ApprovedById & "," & Termination.TerminationTypeId & ",N'" & Termination.Reason & "',N'" & Termination.Details & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1530 : Update records in Termination
    ''' </summary>
    ''' <param name="Termination"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal Termination As EmployeeTerminationBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update records
            str = "UPDATE tblEmployeeTermination SET DocNo = N'" & Termination.DocNo & "', DocDate = '" & Termination.DocDate & "', NoticeDate = '" & Termination.NoticeDate & "', EmployeeId = " & Termination.EmployeeId & ", ApprovedById = " & Termination.ApprovedById & ", TerminationTypeId = " & Termination.TerminationTypeId & ", Reason = N'" & Termination.Reason & "', Details = N'" & Termination.Details & "' WHERE TerminationId = " & Termination.TerminationId & ""
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
    ''' Ali Faisal : TFS1530 : Delete records from termination
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
            str = "DELETE FROM tblEmployeeTermination WHERE TerminationId = " & Id & ""
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

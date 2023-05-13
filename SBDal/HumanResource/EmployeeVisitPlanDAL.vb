'Ali Faisal : TFS1533 : Add DAL functions to save, update, delete and row delete for Employee visit plan
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class EmployeeVisitPlanDAL
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Save records
    ''' </summary>
    ''' <param name="Plan"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal Plan As EmployeeVisitPlanBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert records
            str = "INSERT INTO tblEmployeeVisitPlan (DocNo, DocDate, EmployeeId, TimeFrom, TimeTo, Remarks) VALUES(N'" & Plan.DocNo & "','" & Plan.DocDate & "'," & Plan.EmployeeId & ",'" & Plan.TimeFrom & "','" & Plan.TimeTo & "',N'" & Plan.Remarks & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 'Ali Faisal : TFS1533 : Update records
    ''' </summary>
    ''' <param name="Plan"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal Plan As EmployeeVisitPlanBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update records
            str = "UPDATE tblEmployeeVisitPlan SET DocNo = N'" & Plan.DocNo & "', DocDate = '" & Plan.DocDate & "', EmployeeId = " & Plan.EmployeeId & ", TimeFrom = '" & Plan.TimeFrom & "', TimeTo = '" & Plan.TimeTo & "', Remarks = N'" & Plan.Remarks & "' WHERE PlanId = " & Plan.PlanId & ""
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
    ''' 'Ali Faisal : TFS1533 : Delete records
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
            str = "DELETE FROM tblEmployeeVisitPlan WHERE PlanId = " & Id & ""
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

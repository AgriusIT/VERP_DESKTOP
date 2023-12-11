'Ali Faisal : TFS1528 : Add DAL functions to save, update, delete and row delete for Employee fine deduction
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class EmployeeFineDeductionDAL
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Save Master and Detail records
    ''' </summary>
    ''' <param name="Fine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal Fine As EmployeeFineDeductionBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert master records
            str = "INSERT INTO tblEmployeeFineMaster (DocNo, DocDate) VALUES(N'" & Fine.DocNo & "' ,'" & Fine.DocDate & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert detail records
            For Each obj As EmployeeFineDeductionDetailBE In Fine.Detail
                str = "INSERT INTO tblEmployeeFineDetail (FineId, EmployeeId, DeductionId, Amount, Reason) VALUES(" & "ident_current('tblEmployeeFineMaster')" & "," & obj.EmployeeId & "," & obj.DeductionId & "," & obj.Amount & ",N'" & obj.Reason & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' 'Ali Faisal : TFS1528 : Update Master and Detail records and Save Detail records if not exists in database
    ''' </summary>
    ''' <param name="Fine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal Fine As EmployeeFineDeductionBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update master records
            str = "UPDATE tblEmployeeFineMaster SET DocNo = N'" & Fine.DocNo & "', DocDate = '" & Fine.DocDate & "' WHERE FineId = " & Fine.FineId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Detail records
            For Each obj As EmployeeFineDeductionDetailBE In Fine.Detail
                str = "IF EXISTS(SELECT FineDetailId FROM tblEmployeeFineDetail WHERE FineDetailId = " & obj.FineDetailId & ")  UPDATE tblEmployeeFineDetail SET FineId = " & Fine.FineId & ", EmployeeId = " & obj.EmployeeId & ", DeductionId = " & obj.DeductionId & ", Amount = " & obj.Amount & ", Reason = N'" & obj.Reason & "' WHERE FineDetailId = " & obj.FineDetailId & "" _
                   & " ELSE INSERT INTO tblEmployeeFineDetail (FineId, EmployeeId, DeductionId, Amount, Reason) VALUES(" & "ident_current('tblEmployeeFineMaster')" & "," & obj.EmployeeId & "," & obj.DeductionId & "," & obj.Amount & ",N'" & obj.Reason & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Next
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
    ''' 'Ali Faisal : TFS1528 : Delete Master and Detail records
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
            'Delete detail records
            str = "DELETE FROM tblEmployeeFineDetail WHERE FineId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete master records
            str = "DELETE FROM tblEmployeeFineMaster WHERE FineId = " & Id & ""
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
    ''' 'Ali Faisal : TFS1528 : Delete Detail record
    ''' </summary>
    ''' <param name="DetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteDetail(ByVal DetailId As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete detail records
            str = "DELETE FROM tblEmployeeFineDetail WHERE FineDetailId = " & DetailId & ""
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

'Ali Faisal : TFS1523 : Add DAL functions to save, update, delete and row delete for Employee Shift group change request
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class ShiftChangeRequestDAL
    ''' <summary>
    ''' 'Ali Faisal : TFS1523 : Save Master and Detail records
    ''' </summary>
    ''' <param name="Shift"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Save(ByVal Shift As ShiftChangeRequestBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert master records
            str = "INSERT INTO ShiftChangeRequestMaster (DocNo, DocDate, RequestTypeId, Remarks) VALUES(N'" & Shift.DocNo & "' ,'" & Shift.DocDate & "'," & Shift.RequestTypeId & ",N'" & Shift.Remarks & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert detail records
            For Each obj As ShiftChangeRequestDetailBE In Shift.Detail
                str = "INSERT INTO ShiftChangeRequestDetail (RequestId, EmployeeId, CurrentShifId, NewShiftId, CurrentCostCenterId, NewCostCenterId, Comments) VALUES(" & "ident_current('ShiftChangeRequestMaster')" & "," & obj.EmployeeId & "," & obj.CurrentShifId & "," & obj.NewShiftId & "," & obj.CurrentCostCenterId & "," & obj.NewCostCenterId & ",N'" & obj.Comments & "')"
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
    ''' 'Ali Faisal : TFS1523 : Update Master and Detail records and Save Detail records if not exists in database
    ''' </summary>
    ''' <param name="Shift"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal Shift As ShiftChangeRequestBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update master records
            str = "UPDATE ShiftChangeRequestMaster SET DocNo = N'" & Shift.DocNo & "', DocDate = '" & Shift.DocDate & "', RequestTypeId = " & Shift.RequestTypeId & ", Remarks = N'" & Shift.Remarks & "' WHERE RequestId = " & Shift.RequestId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Detail records
            For Each obj As ShiftChangeRequestDetailBE In Shift.Detail
                str = "IF EXISTS(SELECT RequestDetailId FROM ShiftChangeRequestDetail WHERE RequestDetailId = " & obj.RequestDetailId & ")  UPDATE ShiftChangeRequestDetail SET RequestId = " & Shift.RequestId & ", EmployeeId = " & obj.EmployeeId & ", CurrentShifId = " & obj.CurrentShifId & ", NewShiftId = " & obj.NewShiftId & ", CurrentCostCenterId = " & obj.CurrentCostCenterId & ", NewCostCenterId = " & obj.NewCostCenterId & ", Comments = N'" & obj.Comments & "' WHERE RequestDetailId = " & obj.RequestDetailId & "" _
                   & " ELSE INSERT INTO ShiftChangeRequestDetail (RequestId, EmployeeId, CurrentShifId, NewShiftId, CurrentCostCenterId, NewCostCenterId, Comments) VALUES(" & "ident_current('ShiftChangeRequestMaster')" & "," & obj.EmployeeId & "," & obj.CurrentShifId & "," & obj.NewShiftId & "," & obj.CurrentCostCenterId & "," & obj.NewCostCenterId & ",N'" & obj.Comments & "')"
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
    ''' 'Ali Faisal : TFS1523 : Delete Master and Detail records
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
            str = "DELETE FROM ShiftChangeRequestDetail WHERE RequestId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete master records
            str = "DELETE FROM ShiftChangeRequestMaster WHERE RequestId = " & Id & ""
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
    ''' 'Ali Faisal : TFS1523 : Delete Detail record
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
            str = "DELETE FROM ShiftChangeRequestDetail WHERE RequestDetailId = " & DetailId & ""
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

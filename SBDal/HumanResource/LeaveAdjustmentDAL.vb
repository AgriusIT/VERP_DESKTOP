'Ali Faisal : TFS1481 : DAL for save, update and delete functions
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class LeaveAdjustmentDAL
    ''' <summary>
    ''' Ali Faisal : TFS1481 : Insert records
    ''' </summary>
    ''' <param name="Adjust"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Function Save(ByVal Adjust As LeaveAdjustmentBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master records
            str = "INSERT INTO tblLeaveAdjustMaster (AdjustNo,AdjustDate,Remarks) VALUES(N'" & Adjust.AdjustNo & "','" & Adjust.AdjustDate & "',N'" & Adjust.Remarks & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Detail records
            For Each obj As LeaveAdjustmentDetailBE In Adjust.Detail
                str = "INSERT INTO tblLeaveAdjustDetail (AdjustId,EmployeeId,AdjustDays,LeaveTypeId,ReasonId,Comments) Values(" & "ident_current('tblLeaveAdjustMaster')" & "," & obj.EmployeeId & ",'" & obj.AdjustDays & "'," & obj.LeaveTypeId & "," & obj.ReasonId & ",N'" & obj.Comments & "')"
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
    ''' Ali Faisal : TFS1481 : Update records
    ''' </summary>
    ''' <param name="Adjust"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Function Update(ByVal Adjust As LeaveAdjustmentBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update Master records
            str = "UPDATE tblLeaveAdjustMaster SET AdjustNo = N'" & Adjust.AdjustNo & "',AdjustDate = '" & Adjust.AdjustDate & "',Remarks = N'" & Adjust.Remarks & "' WHERE AdjustId = " & Adjust.AdjustId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Update/Insert Detail records
            For Each obj As LeaveAdjustmentDetailBE In Adjust.Detail
                str = "If Exists(Select AdjustDetailId From tblLeaveAdjustDetail Where AdjustDetailId=" & obj.AdjustDetailId & ")  UPDATE tblLeaveAdjustDetail SET AdjustId = " & Adjust.AdjustId & ",EmployeeId = " & obj.EmployeeId & ",AdjustDays = '" & obj.AdjustDays & "',LeaveTypeId = " & obj.LeaveTypeId & ",ReasonId = " & obj.ReasonId & ",Comments = N'" & obj.Comments & "' Where AdjustDetailId = " & obj.AdjustDetailId & "" _
                   & " Else INSERT INTO tblLeaveAdjustDetail (AdjustId,EmployeeId,AdjustDays,LeaveTypeId,ReasonId,Comments) Values(" & Adjust.AdjustId & "," & obj.EmployeeId & ",'" & obj.AdjustDays & "'," & obj.LeaveTypeId & "," & obj.ReasonId & ",N'" & obj.Comments & "')"
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
    ''' Ali Faisal : TFS1481 : Delete records
    ''' </summary>
    ''' <param name="AdjustId"></param>
    ''' <returns></returns>
    ''' <remarks>Ali Faisal : TFS1481 : 21-Sep-2017</remarks>
    Public Function Delete(ByVal AdjustId As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Detail records
            str = "DELETE FROM tblLeaveAdjustDetail WHERE AdjustId = " & AdjustId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Master records
            str = "DELETE FROM tblLeaveAdjustMaster WHERE AdjustId = " & AdjustId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Delete the detail record.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks></remarks>
    Public Sub DeleteDetail(ByVal Id As Integer)
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Detail records
            str = "DELETE FROM tblLeaveAdjustDetail WHERE AdjustDetailId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub
End Class

Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class LeaveEncashmentDAL

    Public Function Save(ByVal ObjLeave As LeaveEncashmentBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = "INSERT INTO tblLeaveEncashment(Step1,Step2) VALUES(" & ObjLeave.TotalWorkingDays & "," & ObjLeave.LeaveEncashment & ") SELECT @@Identity"

            ObjLeave.LeaveEncashmentId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception

            trans.Rollback()

        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal ObjLeave As LeaveEncashmentBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = "UPDATE tblLeaveEncashment Set Step1='" & ObjLeave.TotalWorkingDays & "'," & " Step2='" & ObjLeave.LeaveEncashment & "' WHERE DetailId=" & ObjLeave.LeaveEncashmentId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()

        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal ObjLeave As LeaveEncashmentBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblLeaveEncashment WHERE DetailId=" & ObjLeave.LeaveEncashmentId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
            objTrans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAll() As DataTable
        Try
            Return UtilityDAL.GetDataTable("Select * From tblLeaveEncashment")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

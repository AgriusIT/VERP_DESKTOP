'Task No 2624 Adding New Code For Newly Added Form LateTimeSlot
Imports System.Data
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data.SqlClient
Imports System.Data.Sql
Public Class LateTimeSlotDAL


    Public Function Save(ByVal ObjLateTimeSlot As LateTimeSlotBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = "INSERT INTO tblLateTimeSlot(Slot_Start,Slot_End,Active) VALUES(" & ObjLateTimeSlot.SlotStart & "," & ObjLateTimeSlot.SlotEnd & "," & IIf(ObjLateTimeSlot.Active = True, 1, 0) & ") SELECT @@Identity"

            ObjLateTimeSlot.SlotId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception

            trans.Rollback()

        Finally
            Con.Close()
        End Try
    End Function
    Public Function Update(ByVal ObjLateTimeSlot As LateTimeSlotBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = "UPDATE tblLateTimeSlot Set Slot_Start='" & ObjLateTimeSlot.SlotStart & "'," & " Slot_End='" & ObjLateTimeSlot.SlotEnd & "'," & " Active='" & IIf(ObjLateTimeSlot.Active = True, 1, 0) & "' WHERE Id=" & ObjLateTimeSlot.SlotId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()

        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal ObjLateTimeSlot As LateTimeSlotBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim objTrans As SqlTransaction = Con.BeginTransaction
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblLateTimeSlot WHERE Id=" & ObjLateTimeSlot.SlotId & ""
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
            Return UtilityDAL.GetDataTable("Select * From tblLateTimeSlot")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task 2612

    Public Function CheckDuplicate(ByVal StartSlot As Integer, ByVal EndSlot As Integer) As Boolean
        Dim blnResult As Boolean
        Try
            Dim StrSql As String
            Dim dt As New DataTable

            StrSql = "select Slot_Start,Slot_End from tblLateTimeSlot where Slot_Start=" & StartSlot & "  and Slot_End=" & EndSlot & ""
            dt = UtilityDAL.GetDataTable(StrSql)

            If dt.Rows.Count > 0 Then
                StartSlot = dt.Rows(0).Item("Slot_Start")
                EndSlot = dt.Rows(0).Item("Slot_End")


                blnResult = True
            Else
                blnResult = False
            End If
        Catch ex As Exception
            Throw ex

        End Try
        Return blnResult

    End Function
End Class

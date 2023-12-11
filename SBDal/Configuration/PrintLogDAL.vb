Imports SBDal
Imports SBDal.SQLHelper
Imports SBUtility.Utility
Imports SBModel
Imports System.Data.SqlClient
Public Class PrintLogDAL
    Public Shared Function PrintLog(ByVal Print_Log As PrintLogBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = "INSERT INTO tblPrint_Log(DocumentNo, UserName, PrintDateTime) VALUES('" & Print_Log.DocumentNo & "', '" & Print_Log.UserName & "', '" & Date.Now.ToString("yyyy-M-d h:mm:ss tt") & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Shared Function GetPrintCont(ByVal DocumentNo As String) As Int32
        Try
            Dim Cont As Int32 = 0
            Dim dt As DataTable = UtilityDAL.GetDataTable("Select Count(Id) as Cont, DocumentNo From tblPrint_Log  WHERE DocumentNo In (Select DocumentNo From tblPrint_Log WHERE DocumentNo='" & DocumentNo & "')")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Cont = dt.Rows(0).Item(0)
                Else
                    Cont = 0
                End If
            End If
            If Cont > 1 Then
                Return Cont
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

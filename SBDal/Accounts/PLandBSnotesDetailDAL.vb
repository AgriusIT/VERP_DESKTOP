Imports System.Data.SqlClient
Imports SBModel
Public Class PLandBSnotesDetailDAL

    Function PlandBsdetail() As DataTable
        Dim _strquery As String
        _strquery = "select main_sub_sub_id , sub_sub_title ,DrBS_note_id, PL_note_id from tblCOAMainSubSub "

        Return UtilityDAL.GetDataTable(_strquery)
    End Function
    Function BSDetail() As DataTable
        Dim _strquery As String
        _strquery = "select note_no,note_title from tblDefGLNotes where note_type = 'BS'union select 0, '...Select any value...' "

        Return UtilityDAL.GetDataTable(_strquery)
    End Function
    Function PLDetail() As DataTable
        Dim _strquery As String
        _strquery = "select note_no,note_title from tblDefGLNotes where note_type = 'PL' union select 0, '...Select any value...'"

        Return UtilityDAL.GetDataTable(_strquery)
    End Function
    Public Function save(ByVal PLBS As List(Of PLandBSnotesDetail)) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then
            con.Open()
        End If
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim strquery As String
            For Each PLBSlist As PLandBSnotesDetail In PLBS
                strquery = "update tblCOAMainSubSub set DrBS_note_id = N'" & PLBSlist.DrBS_note_id & "',crBS_note_id = N'" & PLBSlist.CrBS_note_id & "', PL_note_id = N'" & PLBSlist.PL_note_id & "' where main_sub_sub_id = N'" & PLBSlist.main_sub_sub_id & "' "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strquery)
            Next
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
End Class

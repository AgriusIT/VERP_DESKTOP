Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal

Public Class DefCommissionDAL

    Public Function Save(ByVal comm As DefCommissionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Insert into tblDefCommissionDetail(SalemanId,Start_Value,End_Value,Percentage,Active,Sort_Order) values( " & comm.SalemanId & ", N'" & comm.Start_Value & "', N'" & comm.End_Value & "', N'" & comm.Percentage & "', " & IIf(comm.Active = True, 1, 0) & ", " & comm.Sort_Order & ")"
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            comm.SeqId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Return False
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal comm As DefCommissionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Update tblDefCommissionDetail SET SalemanId = " & comm.SalemanId & ",Start_Value = N'" & comm.Start_Value & "',End_Value = N'" & comm.End_Value & "',Percentage = N'" & comm.Percentage & "',Active = " & IIf(comm.Active = True, 1, 0) & ",Sort_Order = " & comm.Sort_Order & " Where SeqId = " & comm.SeqId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Return False
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Delete(ByVal comm As DefCommissionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete from tblDefCommissionDetail Where SeqId = " & comm.SeqId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Return False
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetAllREcords() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select SeqId, SaleManId,v.detail_title as [Sale Man], Start_Value, End_Value, Percentage, tblDefCommissionDetail.Active, Sort_Order from tblDefCommissionDetail INNER JOIN vwCOADetail v on v.coa_detail_id = tblDefCommissionDetail.SalemanId "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

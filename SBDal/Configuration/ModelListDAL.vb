Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class ModelListDAL
    Public Function Insert(ByVal ModelList As ModelList) As Boolean
        Dim strInsert As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim Trans As SqlTransaction = conn.BeginTransaction
        Try
            strInsert = " Insert Into tblDefModelList(Name, Description, Active) Values(N'" & ModelList.Name & "', N'" & ModelList.Description & "', " & IIf(ModelList.Active = True, 1, 0) & " )"
            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, strInsert)
            Trans.Commit()
            Return True
        Catch ex As Exception
            Trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function Edit(ByVal ModelList As ModelList) As Boolean
        Dim strEdit As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim Trans As SqlTransaction = conn.BeginTransaction
        Try
            strEdit = " Update tblDefModelList Set Name = N'" & ModelList.Name & "', Description = N'" & ModelList.Description & "' , Active = " & IIf(ModelList.Active = True, 1, 0) & " Where ModelId = " & ModelList.ModelId & ""
            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, strEdit)
            Trans.Commit()
            Return True
        Catch ex As Exception
            Trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function Remove(ByVal ModelId As Integer) As Boolean
        Dim strDelete As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim Trans As SqlTransaction = conn.BeginTransaction
        Try
            strDelete = "Delete From tblDefModelList Where ModelId = " & ModelId & ""
            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, strDelete)
            Trans.Commit()
            Return True
        Catch ex As Exception
            Trans.Rollback()
            Throw ex
        End Try
    End Function

    Public Function GetAll() As DataTable
        Dim strGetAll As String = ""
        Dim dt As DataTable
        Try
            strGetAll = "Select * FROM tblDefModelList Where IsNull(Active, 0) = 1"
            dt = UtilityDAL.GetDataTable(strGetAll)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SetCostCentreOnListAssociation(ByVal CostCentreId As Integer, ByVal JobCardId As Integer) As Boolean
        Dim strEdit As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim Trans As SqlTransaction = conn.BeginTransaction
        Try
            strEdit = " Update tblJobCard Set LiftID = " & CostCentreId & " Where JobCardID = " & JobCardId & ""
            SQLHelper.ExecuteNonQuery(Trans, CommandType.Text, strEdit)
            Trans.Commit()
            Return True
        Catch ex As Exception
            Trans.Rollback()
            Throw ex
        End Try
    End Function
End Class

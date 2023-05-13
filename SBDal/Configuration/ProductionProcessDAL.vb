Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDAL
Imports SBDAL.SqlHelper
Public Class ProductionProcessDAL

    Const _New As String = "New"
    Const _Update As String = "Update"
    Const _Delete As String = "Delete"

    Public Function Save(ByVal Obj As BEProductionProcess) As Boolean
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            Dim str As String = String.Empty
            str = "INSERT INTO ProductionProcess(ProcessName, Remarks, WIPAccountId) Values (N'" & Obj.ProcessName & "', N'" & Obj.Remarks & "', " & Obj.WIPAccountId & ") SELECT @@IDENTITY"
            Obj.ProductionProcessId = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            SaveDetail(Obj, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function

    Public Function SaveDetail(ByVal Obj As BEProductionProcess, ByVal trans As SqlTransaction) As Boolean
        'Dim Conn As New SqlConnection(CON_STR)
        'If Conn.State = ConnectionState.Closed Then Conn.Open()
        'Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            For Each detail As BEProductionProcessDetail In Obj.Detail
                Dim str As String = String.Empty
                If detail.State = _New Then
                    str = "INSERT INTO ProductionProcessDetail(ProductionProcessId, ProductionStepId, SortOrder) Values (" & Obj.ProductionProcessId & ", " & detail.ProductionStepId & ", " & detail.SortOrder & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Str)
                ElseIf detail.State = _Update Then
                    str = " Update ProductionProcessDetail SET ProductionProcessId = " & Obj.ProductionProcessId & ", ProductionStepId = " & detail.ProductionStepId & ", SortOrder = " & detail.SortOrder & " WHERE ProductionProcessDetailId = " & detail.ProductionProcessDetailId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                ElseIf detail.State = _Delete Then
                    str = "DELETE FROM ProductionProcessDetail WHERE ProductionProcessDetailId = " & detail.ProductionProcessDetailId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function


    Public Function Update(ByVal Obj As BEProductionProcess) As Boolean
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            Dim str As String = String.Empty
            'str = "INSERT INTO ProductionProcess(ProcessName, Remarks) Values (N'" & Obj.ProcessName & "', N'" & Obj.Remarks & "') SELECT @@IDENTITY"
            str = "UPDATE ProductionProcess SET ProcessName= N'" & Obj.ProcessName & "', Remarks=N'" & Obj.Remarks & "', WIPAccountId = " & Obj.WIPAccountId & "  WHERE ProductionProcessId = " & Obj.ProductionProcessId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            SaveDetail(Obj, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function

    Public Function Delete(ByVal ProductionProcessId As Integer) As Boolean
        Dim str As String = String.Empty
        Dim Conn As New SqlConnection(CON_STR)
        Dim trans As SqlTransaction
        Try
            If Conn.State = ConnectionState.Closed Then Conn.Open()
            trans = Conn.BeginTransaction
            Str = "DELETE FROM ProductionProcess WHERE ProductionProcessId = " & ProductionProcessId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Str)
            DeleteDetail(ProductionProcessId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function
    Public Function DeleteDetail(ByVal ProductionProcessId As Integer, trans As SqlTransaction) As Boolean
        'Dim Conn As New SqlConnection(CON_STR)
        'If Conn.State = ConnectionState.Closed Then Conn.Open()
        'Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "DELETE FROM ProductionProcessDetail WHERE ProductionProcessId = " & ProductionProcessId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAll() As DataTable
        Try
            Dim str As String = String.Empty
            str = "SELECT ProductionProcessId, ProcessName, Remarks, WIPAccountId, Account.detail_title AS WIPAccount FROM ProductionProcess LEFT OUTER JOIN vwCOADetail AS Account ON ProductionProcess.WIPAccountId = Account.coa_detail_id"
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAllDetail(ByVal ProductionProcessId As Integer) As DataTable
        Try
            Dim str As String = String.Empty
            str = "SELECT ProductionProcessDetailId, ProductionProcessId, ProductionStepId, tblproSteps.prod_step AS ProductionStep, Detail.SortOrder, 'Update' AS State FROM ProductionProcessDetail AS Detail LEFT OUTER JOIN tblproSteps ON Detail.ProductionStepId = tblproSteps.ProdStep_Id WHERE ProductionProcessId = " & ProductionProcessId & " Order BY Detail.SortOrder "
            Return UtilityDAL.GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function ValidateProcessName(ByVal ProcessName As String) As Boolean
        Try
            Dim str As String = String.Empty
            str = "SELECT Count(*) As Count1 FROM ProductionProcess WHERE ProcessName ='" & ProcessName & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class


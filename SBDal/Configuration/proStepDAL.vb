Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Imports SBDAL
Imports SBDAL.SqlHelper
Public Class proStepDAL

    Public Function Save(ByVal pro As proStepBE) As Boolean
        Dim Conn As New SqlConnection(CON_STR)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
        Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "INSERT INTO tblproSteps (prod_step, Prod_Less,sort_order, Active, QCVerificationRequired) Values (N'" & pro.Prod_Step & "'," & IIf(pro.Prod_Less = True, 1, 0) & ", N'" & pro.Sort_order & "'," & IIf(pro.Active = True, 1, 0) & ", " & IIf(pro.QCVerificationRequired = True, 1, 0) & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function


    Public Function Update(ByVal pro As proStepBE) As Boolean
        Dim Conn As New SqlConnection(CON_STR)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
        Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "UPDATE tblproSteps SET prod_step=N'" & pro.Prod_Step & "', Prod_Less=" & IIf(pro.Prod_Less = True, 1, 0) & ",  " _
                    & " sort_Order=" & pro.Sort_order & ", " _
                    & " Active = " & IIf(pro.Active = True, 1, 0) & ", QCVerificationRequired = " & IIf(pro.QCVerificationRequired = True, 1, 0) & "  WHERE ProdStep_Id = " & pro.ProdStep_Id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function

    Public Function Delete(ByVal pro As proStepBE) As Boolean
        Dim Conn As New SqlConnection(CON_STR)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
        Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "DELETE FROM tblproSteps WHERE ProdStep_Id = " & pro.ProdStep_Id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Conn.Close()
        End Try
    End Function
    Public Function getAll() As DataTable
        Try
            Dim str As String = String.Empty
            str = "SELECT ProdStep_Id, prod_step, prod_Less, sort_Order, Active, IsNull(QCVerificationRequired, 0) AS QCVerificationRequired FROM dbo.tblProSteps"
            Return UtilityDAL.GetDataTable(str)

        Catch ex As Exception
            Throw ex
        Finally
            ' Conn.Close()
        End Try
    End Function


    Public Shared Function IsStepConsumed(ByVal StepId As Integer) As Boolean
        Try
            Dim str As String = String.Empty
            Dim dt As DataTable
            str = "SELECT Count(ProductionStepId) AS Count1 FROM ProductionProcessDetail WHERE ProductionStepId = " & StepId & ""
            dt = UtilityDAL.GetDataTable(str)
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
        Finally
            ' Conn.Close()
        End Try
    End Function

End Class

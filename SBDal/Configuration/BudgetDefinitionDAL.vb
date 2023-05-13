'30-Jan-2018 TFS2055 : Ali Faisal : Add save,update and delete functions to save update and remove data of Budget
Imports System
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Public Class BudgetDefinitionDAL
    ''' <summary>
    ''' Ali Faisal : Save the Data of Master and Detail in Budget tables.
    ''' </summary>
    ''' <param name="Budget"></param>
    ''' <returns></returns>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Function Save(ByVal Budget As BudgetDefinitionBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Insert Master records
            str = "INSERT INTO AccountBudgetMaster (Title, CostCenterId, FromDate, ToDate, Amount, CurrencyId, Remarks) VALUES (N'" & Budget.Title & "'," & Budget.CostCenterId & ",'" & Budget.FromDate & "','" & Budget.ToDate & "','" & Budget.Amount & "'," & Budget.CurrencyId & ",N'" & Budget.Remarks & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Insert Detail records
            For Each obj As BudgetDefinitionDetailBE In Budget.Details
                str = "INSERT INTO AccountBudgetDetail (AccountBudgetMasterID, AccountId, AccountLevel, AmountRequiredAtAccountLevel, BudgetAmount, Comments, CategoryId) Values(" & "ident_current('AccountBudgetMaster')" & "," & obj.AccountId & ",'" & obj.AccountLevel & "','" & obj.AmountRequiredAtAccountLevel & "','" & obj.BudgetAmount & "',N'" & obj.Comments & "', " & obj.CategoryId & ")"
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
    ''' Ali Faisal : Update Master and Details records and also Check if detail records exists already then update else insert.
    ''' </summary>
    ''' <param name="Budget"></param>
    ''' <returns></returns>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Function Update(ByVal Budget As BudgetDefinitionBE) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Update Master records
            str = "UPDATE AccountBudgetMaster SET Title = N'" & Budget.Title & "', CostCenterId = " & Budget.CostCenterId & ", FromDate = '" & Budget.FromDate & "', ToDate = '" & Budget.ToDate & "', Amount = '" & Budget.Amount & "', CurrencyId = " & Budget.CurrencyId & ", Remarks = N'" & Budget.Remarks & "' WHERE  AccountBudgetMasterId = " & Budget.AccountBudgetMasterId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Update/Insert Detail records
            For Each obj As BudgetDefinitionDetailBE In Budget.Details
                str = "IF EXISTS (SELECT AccountBudgetDetailId FROM AccountBudgetDetail WHERE AccountBudgetDetailId = " & obj.AccountBudgetDetailId & ") UPDATE AccountBudgetDetail SET AccountBudgetMasterID = " & Budget.AccountBudgetMasterId & ", AccountId = " & obj.AccountId & ", AccountLevel = '" & obj.AccountLevel & "', AmountRequiredAtAccountLevel = '" & obj.AmountRequiredAtAccountLevel & "', BudgetAmount = '" & obj.BudgetAmount & "', Comments = N'" & obj.Comments & "', CategoryId = " & obj.CategoryId & " WHERE AccountBudgetDetailId = " & obj.AccountBudgetDetailId & "" _
                    & " ELSE INSERT INTO AccountBudgetDetail (AccountBudgetMasterID, AccountId, AccountLevel, AmountRequiredAtAccountLevel, BudgetAmount, Comments,CategoryId) Values(" & Budget.AccountBudgetMasterId & "," & obj.AccountId & ",'" & obj.AccountLevel & "','" & obj.AmountRequiredAtAccountLevel & "','" & obj.BudgetAmount & "',N'" & obj.Comments & "', " & obj.CategoryId & ")"
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
    ''' Ali Faisal : Delete the Detail and Master records.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
    Public Function Delete(ByVal Id As Integer) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            'Delete Detail records
            str = "DELETE FROM AccountBudgetDetail WHERE AccountBudgetMasterId = " & Id & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            'Delete Master records
            str = "DELETE FROM AccountBudgetMaster WHERE AccountBudgetMasterId = " & Id & ""
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
    ''' Ali Faisal : Delete the detail record.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>30-Jan-2018 TFS2055 : Ali Faisal</remarks>
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
            str = "DELETE FROM AccountBudgetDetail WHERE AccountBudgetDetailId = " & Id & ""
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
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal.UtilityDAL

Public Class AssetConditionDAL

    Public Function Save(ByVal Cond As AssetConditionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "insert into tblAssetCondition(Asset_Condition_Name,Asset_Condition_Description,Sort_Order,Active)values(N'" & Cond.Asset_Condition_Name & "',N'" & Cond.Asset_Condition_Description & "',N'" & Cond.Sort_Order & "'," & IIf(Cond.Active = True, 1, 0) & ")"
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

    Public Function Update(ByVal cond As AssetConditionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Update tblAssetCondition set Asset_Condition_Name = N'" & cond.Asset_Condition_Name & "', " _
            & " Asset_Condition_Description = N'" & cond.Asset_Condition_Description & "', " _
            & " Sort_Order = N'" & cond.Sort_Order & "', " _
            & " Active = " & IIf(cond.Active = True, 1, 0) & " where Asset_Condition_Id = " & cond.Asset_Condition_Id
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

    Public Function Delete(ByVal cond As AssetConditionBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete from tblAssetCondition where Asset_Condition_Id = " & cond.Asset_Condition_Id
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

    Public Function GetRecords() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select * from tblAssetCondition"
            Return GetDatatable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

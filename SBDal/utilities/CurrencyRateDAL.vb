Imports SBModel
Imports System.Data.SqlClient

Public Class CurrencyRateDAL
    Public Function Add(ByRef list As List(Of clsCurrencyRate)) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim sql As String = String.Empty
        Try
            For Each model As clsCurrencyRate In list
                If model.CurrencyRate > 0 Then
                    sql = "Insert into tblCurrencyRate(CurrencyRate, OldRate, UpdateDate, CurrencyId, BasicCurrencyId, UserId) Values(" & model.CurrencyRate & ", " & model.OldRate & ", Convert(datetime,  '" & model.UpdateDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', 102), " & model.CurrencyId & ", " & model.BasicCurrencyId & "," & LoginUser.LoginUserId & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sql)
                End If

            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        End Try
    End Function
    Public Function GetAll() As DataTable
        Dim list As New List(Of clsCurrencyRate)
        Dim sql As String = String.Empty
        Dim dt As New DataTable
        'Dim obj As clsCurrencyRate
        Try
            sql = " Select tblCurrency.currency_id, tblCurrency.currency_code, IsNull(CurrencyRate.CurrencyRate, 0) As CurrencyRate, IsNull(CurrencyRate.OldRate, 0) As OldRate, tblCurrency.Currency_API, IsNull(CurrencyRate.BasicCurrencyId, 0) As BasicCurrencyId" _
                & " From tblCurrency Left Outer Join (Select CurrencyId, CurrencyRate, OldRate, BasicCurrencyId From tblCurrencyRate Where CurrencyRateId in(Select Max(CurrencyRateId) From tblCurrencyRate group by CurrencyId)) As CurrencyRate On tblCurrency.currency_id = CurrencyRate.CurrencyId"
            dt = UtilityDAL.GetDataTable(sql)
            dt.AcceptChanges()
            'For Each row As DataRow In dt.Rows
            '    obj = New clsCurrencyRate
            '    obj.CurrencyId = Val(row.Item("currency_id").ToString)
            '    obj.CurrencyName = row.Item("currency_code").ToString
            '    obj.CurrencyRate = Val(row.Item("CurrencyRate").ToString)
            '    obj.OldRate = Val(row.Item("OldRate").ToString)
            '    If Not IsDBNull(row.Item("UpdateDate")) = True Then
            '        obj.UpdateDate = Convert.ToDateTime(row.Item("UpdateDate").ToString)
            '    Else
            '        obj.UpdateDate = Date.MinValue
            '    End If
            '    obj.CurrencyAPI = row.Item("Currency_API").ToString
            '    list.Add(obj)
            'Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Partial Class dsPostDatedCheques
    Partial Class dtPostDatedChequesDataTable

        Private Sub dtPostDatedChequesDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.debit_amountColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class

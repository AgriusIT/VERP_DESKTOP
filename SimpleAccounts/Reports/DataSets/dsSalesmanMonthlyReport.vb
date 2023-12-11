Partial Class CustomerMonthlyReport
    Partial Class DataTable1DataTable

        Private Sub DataTable1DataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.ReturnQtyColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class

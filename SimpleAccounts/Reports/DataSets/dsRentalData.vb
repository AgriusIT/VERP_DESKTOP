Partial Class dsRentalData
    Partial Class dtRentalDataDataTable

        Private Sub dtRentalDataDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.RateColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class

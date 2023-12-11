Partial Class dtRptLegendLedger
    Partial Class dtRptLegendLedgerDataTable

      
        Private Sub dtRptLegendLedgerDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.OpeningColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class

End Class

Partial Class dsEmployee_Card
    Partial Class EmployeeDataTable

        Private Sub EmployeeDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            'If (e.Column.ColumnName = Me.DataColumn1Column.ColumnName) Then
            '    'Add user code here
            'End If

        End Sub

        Private Sub EmployeeDataTable_EmployeeRowChanging(sender As Object, e As EmployeeRowChangeEvent) Handles Me.EmployeeRowChanging

        End Sub

    End Class

End Class

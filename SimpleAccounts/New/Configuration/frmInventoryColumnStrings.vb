Public Class frmInventoryColumnStrings

    ''By Mohsin Rasheed on Sep 20, 2017
    ''Fetching data from dbo.inventoryColumnStrings on form load

    Private Function GetData() As DataTable
        Dim str As String
        str = "SELECT Column1, Column2, Column3, Column1Visibility,Column2Visibility,Column3Visibility FROM inventoryColumnSettings"
        Dim dt As DataTable = GetDataTable(str)
        Return dt
    End Function

    Private Sub ResetData()

        Dim dt As DataTable = GetData()
        If dt.Rows.Count > 0 Then
            txtColumn1.Text = dt.Rows(0).Item("Column1").ToString
            txtColumn2.Text = dt.Rows(0).Item("Column2").ToString
            txtColumn3.Text = dt.Rows(0).Item("Column3").ToString
            cbColumn1.Checked = dt.Rows(0).Item("Column1Visibility").ToString
            cbColumn2.Checked = dt.Rows(0).Item("Column2Visibility").ToString
            cbColumn3.Checked = dt.Rows(0).Item("Column3Visibility").ToString
        End If

    End Sub
    ''Update the data in database in SAVE button click

    Private Sub SaveData()
        Dim str As String
        'str = "UPDATE inventoryColumnSettings SET Column1 = '" & txtColumn1.Text & "', Column2= '" & txtColumn2.Text & "', Column3= '" & txtColumn3.Text & "', Column1Visibility= " & IIf(cbColumn1.Checked = True, 1, 0) & ", Column2Visibility= " & IIf(cbColumn2.Checked = True, 1, 0) & ", Column3Visibility= " & IIf(cbColumn3.Checked = True, 1, 0) & ";"
        'Dim dt As DataTable = GetDataTable(str)

        'If dt.Rows.Count > 0 Then
        '    txtColumn1.Text = dt.Rows(0).Item("Column1").ToString
        '    txtColumn2.Text = dt.Rows(0).Item("Column2").ToString
        '    txtColumn3.Text = dt.Rows(0).Item("Column3").ToString
        '    cbColumn1.Checked = dt.Rows(0).Item("Column1Visibility").ToString
        '    cbColumn2.Checked = dt.Rows(0).Item("Column2Visibility").ToString
        '    cbColumn3.Checked = dt.Rows(0).Item("Column3Visibility").ToString
        'End If ??????????????????????????????????????? 

        'Dim str As String
        Dim dt As DataTable = GetData()

        If dt.Rows.Count > 0 Then

            str = "UPDATE inventoryColumnSettings SET Column1 = '" & txtColumn1.Text.Replace("'", "''") & "', Column2= '" & txtColumn2.Text.Replace("'", "''") & "', Column3= '" & txtColumn3.Text.Replace("'", "''") & "', Column1Visibility= " & IIf(cbColumn1.Checked = True, 1, 0) & ", Column2Visibility= " & IIf(cbColumn2.Checked = True, 1, 0) & ", Column3Visibility= " & IIf(cbColumn3.Checked = True, 1, 0) & " WHERE COLUMNID IN(SELECT TOP 1 COLUMNID FROM inventoryColumnSettings)"
        Else
            str = "INSERT INTO inventoryColumnSettings(ColumnID,[Column1],[Column2],[Column3],[Column1Visibility],[Column2Visibility],[Column3Visibility]) VALUES(1,'" & txtColumn1.Text.Replace("'", "''") & "', '" & txtColumn2.Text.Replace("'", "''") & "', '" & txtColumn3.Text.Replace("'", "''") & "', " & IIf(cbColumn1.Checked = True, 1, 0) & "," & IIf(cbColumn2.Checked = True, 1, 0) & ", " & IIf(cbColumn3.Checked = True, 1, 0) & ")"
        End If

        SBDal.UtilityDAL.ExecuteQuery(str)

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveData()
            msg_Information("Data Save/Updated Successfully")
            ResetData()
        Catch ex As Exception
            msg_Error("Cannot Save/Update Data")
        End Try

    End Sub

    Private Sub frmInventoryColumnStrings_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            ResetData()
        Catch ex As Exception
            msg_Error("Unable to fetch data")
        End Try

    End Sub
End Class
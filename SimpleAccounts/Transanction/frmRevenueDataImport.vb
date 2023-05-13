Imports System
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports SBDal

Public Class frmRevenueDataImport
    Public Shared Path As String
    Public Shared Files As String
    Private Sub btnImportdata_Click(sender As Object, e As EventArgs) Handles btnImportdata.Click
        Try
            Dim i As Integer
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "Microsoft Excel Files|*.xls;*.xlsx"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                'Check for duplication selection of file
                For i = 0 To grd.GetRows.Length - 1
                    If OpenFileDialog1.SafeFileName = grd.GetRows(i).Cells("FileName").Value Then
                        'User has selected a same file name again
                        ShowErrorMessage("This File Already Exists")
                        Exit Sub
                    End If
                Next
                Files = OpenFileDialog1.SafeFileName
                Path = OpenFileDialog1.FileName

                Dim row As Janus.Windows.GridEX.GridEXRow = grd.AddItem()

                row.BeginEdit()
                row.Cells("FileName").Value = Files
                row.Cells("PathName").Value = Path
                row.EndEdit()

                If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                    Me.grd.RootTable.Columns.Add("Delete")
                    Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grd.RootTable.Columns("Delete").Key = "Delete"
                    Me.grd.RootTable.Columns("Delete").Caption = "Action"
                End If
                Me.grd.RootTable.Columns("PathName").Visible = False
            End If
            If grd.RowCount > 0 Then
                lblNoOfFiles.Text = grd.RowCount
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

            If e.Column.Key = "Delete" Then
                Me.grd.GetRow.Delete()
                grd.UpdateData()
                lblNoOfFiles.Text = grd.RowCount
            End If

            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            If grd.RowCount > 0 Then
                frmMain.LoadControl("frmLoadExcelFile")
            Else
                ShowErrorMessage("Please select at least one file to go to next screen")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
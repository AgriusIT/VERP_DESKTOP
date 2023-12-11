Imports System
Imports System.Data
Imports System.IO
Public Class frmArticleImageDisplay
    Public Property ArticleDefId As Integer

    Private Sub DisplayImage()
        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select ArticlePicture From ArticleDefTableMaster WHERE ArticleId In(Select Distinct MasterID From ArticleDefTable WHERE ArticleId=" & ArticleDefId & ") AND ArticlePicture <> ''")
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0).ToString.Length > 0 Then
                    If IO.File.Exists(dt.Rows(0).Item(0).ToString) Then
                        Me.PictureBox1.Image = Image.FromFile(dt.Rows(0).Item(0).ToString)
                    End If
                End If
            Else
                Me.PictureBox1.Image = Me.PictureBox1.InitialImage
            End If
            dt.Clear()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmArticleImageDisplay_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If ArticleDefId = 0 Then Exit Sub
            DisplayImage()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmArticleImageDisplay_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            'Me.PictureBox1.Image = Nothing

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class

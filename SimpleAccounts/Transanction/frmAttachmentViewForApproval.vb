Imports System.Diagnostics
Imports System.ComponentModel
Imports System.Net
Imports System.IO
Imports SBModel
Imports System.Data.OleDb
Imports System.Security.Cryptography
Public Class frmAttachmentViewForApproval

    Public _Source As String = ""
    Public _VoucherId As Integer = 0

    Private Sub grdSaved_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.Click

    End Sub

    ''Private Sub grdSaved_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.ColumnButtonClick
    ''    Dim strSQL As String = String.Empty
    ''    Dim cmd As New OleDb.OleDbCommand
    ''    If Con.State = ConnectionState.Closed Then Con.Open()
    ''    Dim objTrans As OleDb.OleDbTransaction = Con.BeginTransaction
    ''    cmd.Connection = Con
    ''    cmd.Transaction = objTrans
    ''    Try
    ''        If Me.grdSaved.RowCount = 0 Then Exit Sub
    ''        If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
    ''        If e.Column.Key = "Column1" Then

    ''            strSQL = "Delete From DocumentAttachment WHERE Id=" & Me.grdSaved.GetRow.Cells("Id").Value.ToString & ""
    ''            cmd.CommandText = strSQL
    ''            cmd.CommandType = CommandType.Text
    ''            cmd.CommandTimeout = 300
    ''            cmd.ExecuteNonQuery()
    ''            objTrans.Commit()
    ''            GetAllRecords()
    ''            Dim dt As New DataTable
    ''            dt = GetAllRecords()
    ''            dt.AcceptChanges()

    ''            Me.grdSaved.DataSource = dt

    ''        End If
    ''    Catch ex As Exception
    ''        objTrans.Rollback()
    ''        ShowErrorMessage(ex.Message)
    ''    Finally
    ''        Con.Close()
    ''    End Try
    ''End Sub

    Public Function GetAllRecords(Optional ByVal Condition As String = "") As DataTable
        Try
            Dim dt As New DataTable
            'Marked Against Task#2015060005 to add components
            '            dt = GetDataTable("Select Id, DocId,FileName,Path, Convert(Image,'')  as Attachment_Image  From DocumentAttachment WHERE Source='" & _Source.Replace("'", "''") & "' AND DocId=" & _VoucherId & "")
            'Altered Against Task#2015060005 to add components

            'Altered Against Task#2015060005 Make Union Query to Add Image preview for image documents and blank image for other files
            Dim converter As New ImageConverter
            'Dim imgBytes() As Byte = converter.ConvertTo(My.Resources.Attachments, GetType(Byte()))

            dt = GetDataTable("Select Id, DocId,FileName,Path, Convert(Image,'')  as Attachment_Image,1 as Pic  From DocumentAttachment WHERE Source='" & _Source.Replace("'", "''") & "' AND DocId=" & _VoucherId & " and right(filename,3) in ('BMP','DIB','RLE','JPG','JPEG','JPE','JFIF','GIF','TIF','TIFF','PNG') union all Select Id, DocId,FileName,Path,  Convert(Image,'') ,0  From DocumentAttachment WHERE Source='" & _Source.Replace("'", "''") & "' AND DocId=" & _VoucherId & " and right(filename,3) not in ('BMP','DIB','RLE','JPG','JPEG','JPE','JFIF','GIF','TIF','TIFF','PNG')")
            ' dt = GetDataTable("Select Id, DocId,FileName,Path, Convert(Image,'')  as Attachment_Image,1 as Pic  From DocumentAttachment WHERE Source='" & _Source.Replace("'", "''") & "' AND DocId=" & _VoucherId & " and right(filename,3) in ('BMP','DIB','RLE','JPG','JPEG','JPE','JFIF','GIF','TIF','TIFF','PNG') union all Select Id, DocId,FileName,Path, ''  ,0  From DocumentAttachment WHERE Source='" & _Source.Replace("'", "''") & "' AND DocId=" & _VoucherId & " and right(filename,3) not in ('BMP','DIB','RLE','JPG','JPEG','JPE','JFIF','GIF','TIF','TIFF','PNG')")
            'Altered Against Task#2015060005 Make Union Query to Add Image preview for image documents and blank image for other files
            dt.AcceptChanges()
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        'Altered Against Task#2015060005 Make Union Query to Add Image preview for image documents and blank image for other files
                        If r.Item("PIC").ToString = "1" Then
                            'Altered Against Task#2015060005 Make Union Query to Add Image preview for image documents and blank image for other files


                            LoadPicture(r, "Attachment_Image", r.Item("Path").ToString & "\" & r.Item("FileName").ToString)
                        Else
                            '               r("Attachment_Image") = imgBytes
                        End If
                        r.EndEdit()
                    Next
                End If
            End If
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmAttachmentView_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Dim dt As New DataTable
            dt = GetAllRecords()
            dt.AcceptChanges()

            Me.grdSaved.DataSource = dt

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Altered Against Task#2015060005 File Open on Click
    Private Sub grdSaved_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.LinkClicked
        Dim myProcess As New Process()
        Dim curFile As String = String.Empty

        Try
            Dim FileToOpen As String = String.Empty
            If e.Column.Key = "Path" Then
                myProcess.StartInfo.UseShellExecute = False
                FileToOpen = grdSaved.CurrentRow.Cells("Path").Value.ToString & "/" & grdSaved.CurrentRow.Cells("FileName").Value.ToString
                ''''


                Dim SourcePath As String = FileToOpen  'This is just an example string and could be anything, it maps to fileToCopy in your code.
                'Dim SaveDirectory As String = "c:\DestinationFolder"

                Dim Filename As String = System.IO.Path.GetFileName(SourcePath) 'get the filename of the original file without the directory on it
                'Dim SavePath As String = System.IO.Path.Combine(SaveDirectory, Filename) 'combines the saveDirectory and the filename to get a fully qualified path.

                If System.IO.File.Exists(FileToOpen) Then
                    'The file exists
                    If Len(FileToOpen) > 0 Then

                        System.Diagnostics.Process.Start(FileToOpen)
                        myProcess.StartInfo.CreateNoWindow = True
                    End If
                Else
                    ShowErrorMessage("File does not exists")
                    Exit Sub
                End If

                Exit Sub

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
End Class
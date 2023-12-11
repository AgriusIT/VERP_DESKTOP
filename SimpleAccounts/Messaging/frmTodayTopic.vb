Imports System.Net
Imports System.Web
Imports System.IO
Public Class frmTodayTopic
    Dim dt As DataTable
    Private Sub frmTodayTopic_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            dt = New DataTable
            dt.TableName = "DailyTip"
            dt.Columns.Add("StartupTip", GetType(System.Boolean))

            Dim dr As DataRow
            If Not IO.File.Exists(str_ApplicationStartUpPath & "\Startuptip.Xml") = True Then
                dr = dt.NewRow
                dr(0) = True
                dt.Rows.InsertAt(dr, 0)
                dt.WriteXml(str_ApplicationStartUpPath & "\Startuptip.Xml")
            Else
                dt.ReadXml(str_ApplicationStartUpPath & "\Startuptip.Xml")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.CheckBox1.Checked = dt.Rows(0).Item(0)
                    End If
                End If
            End If

            Dim dtHtml As DataTable = GetDataTable("Select (ISNULL(Max(TipId),0)+1) as Cont from tblTips")
            If dtHtml Is Nothing Then Exit Sub


            Dim str As String = str_ApplicationStartUpPath & "\Tips\" & "" & dtHtml.Rows(0).Item(0) & ".html"
            str = str.Substring(str.LastIndexOf("\") + 1)


            If Val(str.Replace(".html", "")) = 1 Then
                Me.Button1.Enabled = False
            Else
                Me.Button1.Enabled = True
            End If

            If IO.File.Exists(str_ApplicationStartUpPath & "\Tips\" & "" & dtHtml.Rows(0).Item(0) & ".html") Then
                Me.WebBrowser1.Navigate(str_ApplicationStartUpPath & "\Tips\" & "" & dtHtml.Rows(0).Item(0) & ".html")
                AddTip(str)
            Else
                Me.WebBrowser1.Navigate(str_ApplicationStartUpPath & "\Tips\" & "" & Val(dtHtml.Rows(0).Item(0) - 1) & ".html")
            End If
            'Save html source if not exist in table 'Save html source if not exist in table
        Catch ex As Exception
            ShowErrorMessage("Error occurred while loading page: " & ex.Message)
        End Try
    End Sub
    Public Function GetURL() As String
        Try

            Dim strURL As String = String.Empty
            Dim Req As WebRequest = WebRequest.Create("https://dl.dropbox.com/u/68130832/Daily%20Tip%20Link.txt")
            Dim Resp As WebResponse = Req.GetResponse

            Dim fsReader As StreamReader
            fsReader = New StreamReader(Resp.GetResponseStream)
            strURL = fsReader.ReadToEnd

            fsReader.Close()
            fsReader.Dispose()

            Return strURL

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted

    End Sub
    Private Sub WebBrowser1_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles WebBrowser1.Navigated
        Try

            'Me.Text = WebBrowser1.Url.ToString

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub frmTodayTopic_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try

            dt = New DataTable
            dt.TableName = "DailyTip"
            dt.Columns.Add("StartupTip", GetType(System.Boolean))

            Dim dr As DataRow
            If Not IO.File.Exists(str_ApplicationStartUpPath & "\Startuptip.Xml") = True Then
                dr = dt.NewRow
                dr(0) = True
                dt.Rows.InsertAt(dr, 0)
                dt.WriteXml(str_ApplicationStartUpPath & "\Startuptip.Xml")
            Else
                dt.ReadXml(str_ApplicationStartUpPath & "\Startuptip.Xml")
                dt.Rows(0).Delete()
                dr = dt.NewRow
                dr(0) = Me.CheckBox1.Checked
                dt.Rows.InsertAt(dr, 0)
                dt.WriteXml(str_ApplicationStartUpPath & "\Startuptip.Xml")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            Dim str As String = Me.WebBrowser1.Url.ToString
            str = str.Substring(str.LastIndexOf("/") + 1)
            'AddTip(str) 'Save html source if not exist in table
            Dim nextUrl As Int32 = Val(str.Replace(".html", "")) + 1
            If IO.File.Exists(str_ApplicationStartUpPath & "\Tips\" & "" & nextUrl & ".html") Then
                Me.WebBrowser1.Navigate(str_ApplicationStartUpPath & "\Tips\" & "" & nextUrl & ".html")
            Else
                Me.WebBrowser1.Navigate(str_ApplicationStartUpPath & "\Tips\" & "1.html")
            End If
            Me.Button1.Enabled = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            Dim str As String = Me.WebBrowser1.Url.ToString
            str = str.Substring(str.LastIndexOf("/") + 1)
            'AddTip(str) 'Save html source if not exist in table
            Dim CurrentUrl As Int32 = Val(str.Replace(".html", ""))
            If CurrentUrl > 1 Then
                CurrentUrl = CurrentUrl - 1
            Else
                CurrentUrl = 1
                Me.Button1.Enabled = False
            End If
            If IO.File.Exists(str_ApplicationStartUpPath & "\Tips\" & "" & CurrentUrl & ".html") Then
                Me.WebBrowser1.Navigate(str_ApplicationStartUpPath & "\Tips\" & "" & CurrentUrl & ".html")
            Else
                Me.WebBrowser1.Navigate(str_ApplicationStartUpPath & "\Tips\" & "1.html")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Function AddTip(ByVal Tips As String) As Boolean
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Try
            Dim cmd As New OleDb.OleDbCommand
            cmd.Connection = trans.Connection
            cmd.Transaction = trans

            Dim str As String = String.Empty
            Dim dt As DataTable = GetDataTable("Select * From tblTips WHERE Source='" & Tips & "'")
            If Not dt.Rows.Count > 0 Then
                str = "INSERT INTO tblTips(Source) Values('" & Tips & "')"
                cmd.CommandText = str
                cmd.CommandType = CommandType.Text
                cmd.ExecuteNonQuery()
            Else
                Exit Function
            End If
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
End Class
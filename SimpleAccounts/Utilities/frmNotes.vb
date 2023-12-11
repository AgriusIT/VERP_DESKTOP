Public Class Notes

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.BackColor = Color.FromArgb(12, 120, 148)
        Button1.ForeColor = Color.FromArgb(255, 247, 255)
        Dim notes As New Pop_notes(Me)
        notes.Show()

        CType(Pop_notes, Windows.Forms.Form).Focus()
    End Sub
    Private Sub Notes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        grd.RootTable.RowHeight = 30
        DisplayRecord()
        GetTiles()
    End Sub
    Public Sub DisplayRecord()
        grd.DataSource = Nothing
        grd.DataSource = ReadTable("Select * from tblNotes where LoginUserID=" & LoginUserId & "order by ModifiedDated desc")
        grd.Refresh()

    End Sub
    Public Sub GetTiles()

        Dim dt As DataTable = ReadTable("select top 3  * from  tblNotes where LoginUserID='" & LoginUserId & "' order by ModifiedDated desc")

        If (dt.Rows.Count = 1) Then

            lblTitle1.Text = dt.Rows(0)("Title")
            lblDescription1.Text = dt.Rows(0)("Detail")
            lblTitle2.Text = "Not Found Yet"
            lblDescription2.Text = "Not Found"
            lblTitle3.Text = "Not Found Yet"
            lblDescription3.Text = "Not Found"

        ElseIf (dt.Rows.Count = 2) Then
            lblTitle1.Text = dt.Rows(0)("Title")
            lblDescription1.Text = dt.Rows(0)("Detail")
            lblTitle2.Text = dt.Rows(1)("Title")
            lblDescription2.Text = dt.Rows(1)("Detail")
            lblTitle3.Text = "Not Found Yet"
            lblDescription3.Text = "Not Found"

        ElseIf (dt.Rows.Count >= 3) Then
            lblTitle1.Text = dt.Rows(0)("Title")
            lblDescription1.Text = dt.Rows(0)("Detail")
            lblTitle2.Text = dt.Rows(1)("Title")
            lblDescription2.Text = dt.Rows(1)("Detail")
            lblTitle3.Text = dt.Rows(2)("Title")
            lblDescription3.Text = dt.Rows(2)("Detail")

        End If

    End Sub
    Private Sub Notes_Click(sender As Object, e As EventArgs) Handles MyBase.Click
        Button1.BackColor = Color.FromArgb(215, 215, 215)
        Button1.ForeColor = Color.FromArgb(153, 180, 189)
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        DisplayRecord()
        GetTiles()
    End Sub

    Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Me.grd.UpdateData()
        SQLquery("update tblNotes set Title= '" & grd.GetRow.Cells(1).Value & "',Detail='" & grd.GetRow.Cells(2).Value & "',ModifiedDated='" & System.DateTime.Now & "' where NotesID=" & grd.GetRow.Cells(0).Value)
        DisplayRecord()
        GetTiles()
    End Sub
End Class
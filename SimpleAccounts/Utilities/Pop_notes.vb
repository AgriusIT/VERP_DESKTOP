Public Class Pop_notes
    Dim notes As Notes
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByRef note As Notes)
        Me.notes = note
        InitializeComponent()
    End Sub
    Private Sub Pop_notes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CType(Me, Windows.Forms.Form).Focus()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim flag As Boolean = SQLquery("Insert into tblNotes (Title,Detail,SavedDate,ModifiedDated,LoginUserID) values ('" & TextBox1.Text & "','" & RichTextBox1.Text & "','" & System.DateTime.Now & "','" & System.DateTime.Now & "','" & LoginUserId & "')")
        If flag Then
            notes.DisplayRecord()
            notes.GetTiles()
            notes.Refresh()

            Me.Close()

            Application.DoEvents()
        End If
    End Sub
End Class
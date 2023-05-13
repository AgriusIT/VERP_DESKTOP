Public Class Form2


    Private _LastName As String
    Private _FirstName As String


    Public ReadOnly Property FullName() As String
        Get
            Return _FirstName & " " & _LastName
        End Get
    End Property

    Public WriteOnly Property LastName() As String
        Set(ByVal value As String)
            _LastName = value
        End Set
    End Property


    Public WriteOnly Property FirstName() As String
        Set(ByVal value As String)
            _FirstName = value
        End Set
    End Property


    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            FirstName = TextBox1.Text
            LastName = TextBox2.Text
            MsgBox(FullName)

        Catch ex As Exception

        End Try
    End Sub
End Class
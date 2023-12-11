Public Class LoginForm

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See http://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private UserVerified As Boolean = False

    Private Sub SaveLastSettings()
        Try

            Dim doc As New Xml.XmlDocument
            'create nodes 
            Dim root As Xml.XmlElement = doc.CreateElement("Settings")
            Dim child As Xml.XmlElement = doc.CreateElement("LastSettings")
            Dim element1 As Xml.XmlElement = doc.CreateElement("Rec")
            'create attribute 
            element1.SetAttribute("UserName", Me.UsernameTextBox.Text)

            'put them all together 
            child.AppendChild(element1)
            root.AppendChild(child)
            doc.AppendChild(root)

            'save it 
            doc.Save(Application.StartupPath & "\myLastSettings.xml")


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LoadLastSettings()

        Dim xmlr As System.Xml.XmlTextReader
        Dim strv As String = ""
        Try
            xmlr = New System.Xml.XmlTextReader(Application.StartupPath & "\myLastSettings.xml")
            xmlr.Read()
            While Not xmlr.EOF
                xmlr.Read()
                If Not xmlr.IsStartElement Then
                    Exit While
                ElseIf Not xmlr.HasAttributes Then

                ElseIf xmlr.HasAttributes Then

                    Dim SrNo = xmlr.GetAttribute("SrNo")
                    Dim dated = xmlr.GetAttribute("Date")
                    Dim Count = xmlr.GetAttribute("Count")
                    Dim Gap = xmlr.GetAttribute("Gap")
                    xmlr.Read()
                    'Response.Write(SkillName)
                    'strv = strv & "[" & SrNo & "]-[" & dated & "]-[" & Count & "]-[" & Gap & "]" & vbCrLf
                    Me.UsernameTextBox.Text = SrNo
                    
                End If


            End While


            xmlr.Close()
        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try



    End Sub
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click

        If Me.UsernameTextBox.Text.Length < 1 Then
            msg_Error("Please enter user name")
            Me.UsernameTextBox.Focus()
            Exit Sub
        End If

        If Me.PasswordTextBox.Text.Length < 1 Then
            msg_Error("Please enter password")
            Me.PasswordTextBox.Focus()
            Exit Sub
        End If

        Dim strUserValidity As String = CheckSecurityUser(Me.UsernameTextBox.Text, Me.PasswordTextBox.Text)

        If strUserValidity = "In-Valid" Then
            msg_Error(str_ErrorInvalidUser)
        ElseIf strUserValidity = "In-Active" Then
            msg_Error(str_ErrorInActiveUser)
        ElseIf strUserValidity = "Valid" Then
            Me.UserVerified = True
            Me.Close()
        Else
            End
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        End
    End Sub

    Private Sub LoginForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not Me.UserVerified = True Then
            End
        End If
        Me.SaveLastSettings()
    End Sub


    Private Sub LoginForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.LoadLastSettings()
    End Sub
End Class

Module ModAppSettings
    'Public LastGroup As Integer = 0
    Public LastItem As String = ""

    Sub SaveLastSettings()
        Try
            If LoginUserId = 0 Then
                Exit Sub
            End If
            Dim doc As New Xml.XmlDocument
            'create nodes 
            Dim root As Xml.XmlElement = doc.CreateElement("Settings")
            Dim child As Xml.XmlElement = doc.CreateElement("LastSettings")
            Dim element1 As Xml.XmlElement = doc.CreateElement("Rec")
            'create attribute 
            element1.SetAttribute("Company", ConCompany)
            element1.SetAttribute("UserName", LoginUserCode)
            If LoginUserRememberMe = True Then
                element1.SetAttribute("UserPassword", Encrypt(LoginUserPassword))
            Else
                element1.SetAttribute("UserPassword", String.Empty)
            End If
            element1.SetAttribute("RememberMe", LoginUserRememberMe)
            'element1.SetAttribute("GroupIndex", LastGroup)
            element1.SetAttribute("LastFormKey", LastItem)

            'put them all together 
            child.AppendChild(element1)
            root.AppendChild(child)
            doc.AppendChild(root)

            'save it 
            doc.Save(str_ApplicationStartUpPath & "\ApplicationSettings\myLastSettings.xml")


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Sub LoadLastSettings()

        Dim xmlr As System.Xml.XmlTextReader
        Dim strv As String = ""
        If Not System.IO.File.Exists(str_ApplicationStartUpPath & "\ApplicationSettings\myLastSettings.xml") Then Exit Sub
        Try
            xmlr = New System.Xml.XmlTextReader(str_ApplicationStartUpPath & "\ApplicationSettings\myLastSettings.xml")
            xmlr.Read()
            While Not xmlr.EOF
                xmlr.Read()
                If Not xmlr.IsStartElement Then
                    Exit While
                ElseIf Not xmlr.HasAttributes Then

                ElseIf xmlr.HasAttributes Then

                    Dim UserCode = xmlr.GetAttribute("UserName")
                    Dim GroupIndex = xmlr.GetAttribute("GroupIndex")
                    Dim ItemIndex = xmlr.GetAttribute("LastFormKey")
                    Dim Gap = xmlr.GetAttribute("Gap")
                    Dim comp = xmlr.GetAttribute("Company")
                    Dim UserPwd As String = String.Empty
                    If xmlr.GetAttribute("UserPassword").ToString.Length > 0 Then
                        UserPwd = Decrypt(xmlr.GetAttribute("UserPassword").ToString)
                    End If
                    Dim RememberMe = xmlr.GetAttribute("RememberMe")
                    xmlr.Read()
                    ConCompany = comp
                    LoginUserCode = UserCode
                    LoginUserPassword = UserPwd
                    LoginUserRememberMe = RememberMe
                    LastItem = ItemIndex
                    End If

            End While


            xmlr.Close()
        Catch ex As Exception
            'Response.Write(ex.Message)

        End Try



    End Sub

    Public Sub LoadUserImages()
        Try

            Dim dt As New DataTable
            Dim strSQL As String = String.Empty
            strSQL = "Select User_Id, User_Name, User_Picture, Convert(Image,NULL) as UserImage from tblUser WHERE User_Picture <> '' AND User_ID in (Select Distinct LogUserId from tblActivityLog WHERE LogActivityName='Login' AND LogFormCaption=N'" & System.Environment.MachineName.ToString & "')"
            dt = GetDataTable(strSQL)
            dt.TableName = "UserImages"
            dt.AcceptChanges()

            For Each r As DataRow In dt.Rows
                r.BeginEdit()
                r("User_Name") = Decrypt(r("User_Name").ToString)
                If IO.File.Exists(r.Item("User_Picture").ToString) Then
                    LoadPicture(r, "UserImage", r.Item("User_Picture").ToString)
                End If
                r.EndEdit()
            Next

            If IO.File.Exists(str_ApplicationStartUpPath & "\ApplicationSettings\UserImages.xml") Then
                IO.File.Delete(str_ApplicationStartUpPath & "\ApplicationSettings\UserImages.xml")
            End If
            dt.WriteXml(str_ApplicationStartUpPath & "\ApplicationSettings\UserImages.xml")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Module

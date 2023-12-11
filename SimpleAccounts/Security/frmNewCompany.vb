Imports System.Data.OleDb
Public Class frmNewCompany

    Private mstrRestorePath As String = "D:\SqlServer\DataBase"

#Region "Local Functions"

    Private Function IsValidate(Optional ByVal strMode As String = "") As Boolean
        Try
            'If Not System.IO.File.Exists(str_ApplicationStartUpPath & "\CompanyList.xml") Then
            '    System.IO.File.Copy(str_ApplicationStartUpPath & "\DB\CompanyList.xml", str_ApplicationStartUpPath & "\CompanyList.xml")
            'End If

            If Not System.IO.File.Exists(str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml") Then
                System.IO.File.Copy(str_ApplicationStartUpPath & "\DB\CompanyConnectionInfo.xml", str_ApplicationStartUpPath & "\CompanyConnectionInfo.xml")
            End If

            If Not System.IO.Directory.Exists(mstrRestorePath) Then
                System.IO.Directory.CreateDirectory(mstrRestorePath)
            End If

            If Me.txtCompanyName.Text.Trim.Length = 0 Then
                ShowErrorMessage("Please Enter Company Name.")
                Me.txtCompanyName.Focus()
                Return False

            ElseIf Me.txtServer.Text.Trim.Length = 0 Then
                ShowErrorMessage("Please Enter Server Name.")
                Me.txtServer.Focus()
                Return False

            ElseIf Me.txtUserID.Text.Trim.Length = 0 Then
                ShowErrorMessage("Please Enter SQL Server User ID.")
                Me.txtUserID.Focus()
                Return False

            ElseIf Me.txtPassword.Text.Trim.Length = 0 Then
                ShowErrorMessage("Please Enter SQL Server User Password.")
                Me.txtPassword.Focus()
                Return False
            End If

            If strMode = "New" Then
                If Me.txtBKPath.Text.Trim.Length = 0 Then
                    ShowErrorMessage("Please Select DB Backup.")
                    Me.btnLoadBKFile.Focus()
                    Return False
                End If

            End If


            If strMode = "New" Then
                Dim xmlDoc As New Xml.XmlDocument

                xmlDoc.Load(str_ApplicationStartUpPath & "\CompanyList.xml")
                Dim TagetNode As Xml.XmlNode = xmlDoc.SelectNodes("//AllCompanies/Company[@DBName='DB_" & Me.txtCompanyName.Text.Trim.Replace("'", "''").ToUpper & "']").Item(0)

                If Not IsNothing(TagetNode) Then
                    Throw New Exception("Company Already Exist.")
                    Return False
                End If
            End If


            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub RefreshScreen()

        Try
            Me.txtCompanyName.Tag = ""
            Me.txtCompanyName.Text = ""
            Me.txtServer.Tag = ""
            Me.txtServer.Text = ""
            Me.txtUserID.Text = ""
            Me.txtPassword.Text = ""
            Me.txtBKPath.Text = str_ApplicationStartUpPath & "\DB\SimplePos.bak"

            Me.btnSave.Text = "Save"
            Me.btnLoadBKFile.Enabled = True

            Me.txtCompanyName.Focus()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function RestoreDB() As Boolean




        Try

            ''Find Server Exist
            Dim MyPing As New Net.NetworkInformation.Ping
            Dim MyPingReply As Net.NetworkInformation.PingReply = MyPing.Send(Me.txtServer.Text, 10)

            If MyPingReply.Status.ToString = "Success" Then

                ''Restore
                Dim conn As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=sqloledb;Data Source=" & Me.txtServer.Text & ";Initial Catalog=Master;User Id=" & Me.txtUserID.Text & ";Password=" & Me.txtPassword.Text & ";")
                Try
                    conn.Open()

                    Try

                        Dim strSQL As String

                        Dim cmd As New OleDb.OleDbCommand
                        cmd.CommandType = CommandType.Text
                        cmd.Connection = conn


                        strSQL = " RESTORE FILELISTONLY FROM DISK = '" & Me.txtBKPath.Text & "'"
                        cmd.CommandText = strSQL


                        Dim mdfFileName As String = ""
                        Dim logFileName As String = ""

                        Dim DR As OleDb.OleDbDataReader
                        DR = cmd.ExecuteReader

                        If DR.HasRows Then
                            DR.Read()
                            mdfFileName = DR.Item(0)

                            DR.Read()
                            logFileName = DR.Item(0)

                        End If

                        DR.Close()
                        DR = Nothing

                        strSQL = "IF Not Exists (SELECT * FROM sysdatabases WHERE [name]='" & "DB_" & Me.txtCompanyName.Text.Trim & "')" & vbCrLf
                        strSQL = strSQL & " Begin " & vbCrLf

                        strSQL = strSQL & "         RESTORE DATABASE DB_" & Me.txtCompanyName.Text.Trim & vbCrLf
                        strSQL = strSQL & "         FROM DISK = '" & Me.txtBKPath.Text & "'" & vbCrLf
                        strSQL = strSQL & "         WITH MOVE '" & mdfFileName & "' TO '" & mstrRestorePath & "\DB_" & Me.txtCompanyName.Text.Trim & ".mdf'," & vbCrLf
                        strSQL = strSQL & "         MOVE '" & logFileName & "' TO '" & mstrRestorePath & "\DB_" & Me.txtCompanyName.Text.Trim & "_log.ldf'" & vbCrLf
                        strSQL = strSQL & " End " & vbCrLf


                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()

                        If conn.State = ConnectionState.Open Then conn.Close()
                        conn = Nothing

                    Catch ex As Exception

                        If conn.State = ConnectionState.Open Then conn.Close()
                        conn = Nothing
                        Throw New Exception("Fail to Restore DB Backup")
                        Return False

                    End Try

                Catch ex As Exception

                    If ex.Message = "Fail to Restore DB Backup" Then
                        Throw ex
                    Else

                        If conn.State = ConnectionState.Open Then conn.Close()
                        conn = Nothing
                        Throw New Exception("Connection to Master DB Failed.")
                        Return False

                    End If
                End Try

                Return True
            End If



        Catch ex As Exception

            If ex.Message = "Fail to Restore DB Backup" Then
                Throw ex
            ElseIf ex.Message = "Connection to Master DB Failed." Then
                Throw ex
            Else
                Throw New Exception("Server Not Found")
            End If


            Return False

        End Try
    End Function

    Private Sub LoadGrid()
        Try


            'Dim dt As New DataTable
            'dt = GetCompanyDataTable()
            'If Not dt.Rows.Count > 0 Then Exit Sub
            'dt.Columns.Add("ID", System.Type.GetType("System.String"))
            'dt.Columns.Add("Name", System.Type.GetType("System.String"))
            'dt.Columns.Add("Server", System.Type.GetType("System.String"))
            'dt.Columns.Add("User ID", System.Type.GetType("System.String"))
            'dt.Columns.Add("Password", System.Type.GetType("System.String"))
            'dt.Columns.Add("DBName", System.Type.GetType("System.String"))

            'Dim dr As DataRow

            'Dim myReader As Xml.XmlReader
            'myReader = Xml.XmlReader.Create(str_ApplicationStartUpPath & "\CompanyList.xml")

            'While myReader.Read

            '    If myReader.NodeType = Xml.XmlNodeType.Element Then

            '        If myReader.Name = "Company" Then
            '            dr = dt.NewRow

            '            dr("ID") = myReader.GetAttribute("ID").ToString
            '            dr("Name") = myReader.GetAttribute("Name").ToString
            '            dr("Server") = myReader.GetAttribute("Server").ToString
            '            dr("User ID") = myReader.GetAttribute("UserID").ToString
            '            dr("Password") = myReader.GetAttribute("Password").ToString
            '            dr("DBName") = myReader.GetAttribute("DBName").ToString

            '            dt.Rows.Add(dr)
            '        Else

            '        End If
            '    End If

            'End While


            'Me.grd.DataSource = dt
            'Me.grd.Refresh()

            'Me.grd.Columns(0).Visible = False
            'Me.grd.Columns("Password").Visible = False
            'Me.grd.Columns("DBName").Visible = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function AddNewCompany() As Boolean

        Dim xmlDoc As New Xml.XmlDocument
        Dim NewNode As Xml.XmlNode

        Try

            'Dim testCn As New OleDbConnection("Provider=SQLOLEDB.1;Password=" & Me.txtPassword.Text & ";Integrated Security Info=False;User ID=" & Me.txtUserID.Text.Trim & ";Initial Catalog=sp_dp;Data Source=" & Me.txtServer.Text)
            'Try
            '    testCn.Open()
            '    If testCn.State = ConnectionState.Open Then testCn.Close() Else MsgBox("Invalid connection infromation") : Exit Function

            'Catch ex As Exception
            '    MsgBox("Invalid connection infromation: " & ex.Message) : Exit Function
            'End Try

            xmlDoc.Load(str_ApplicationStartUpPath & "\CompanyList.xml")
            Dim rootNode As Xml.XmlNode = xmlDoc.SelectNodes("//AllCompanies").Item(0)



            NewNode = xmlDoc.CreateElement("Company")

            Dim NewAtt As Xml.XmlAttribute
            NewAtt = xmlDoc.CreateAttribute("ID")
            NewAtt.Value = Val(xmlDoc.GetElementsByTagName("Company").Count) + 1
            NewNode.Attributes.Append(NewAtt)

            NewAtt = xmlDoc.CreateAttribute("Name")
            NewAtt.Value = Me.txtCompanyName.Text.Trim.ToUpper
            NewNode.Attributes.Append(NewAtt)

            NewAtt = xmlDoc.CreateAttribute("Server")
            NewAtt.Value = Me.txtServer.Text.Trim.ToUpper
            NewNode.Attributes.Append(NewAtt)

            NewAtt = xmlDoc.CreateAttribute("UserID")
            NewAtt.Value = Me.txtUserID.Text
            NewNode.Attributes.Append(NewAtt)

            NewAtt = xmlDoc.CreateAttribute("Password")
            NewAtt.Value = Me.txtPassword.Text
            NewNode.Attributes.Append(NewAtt)

            NewAtt = xmlDoc.CreateAttribute("DBName")
            NewAtt.Value = "DB_" & Me.txtCompanyName.Text.Trim.ToUpper
            NewNode.Attributes.Append(NewAtt)


            ''Create New DAB

            If Me.RestoreDB() Then
                rootNode.AppendChild(NewNode)

            End If

            xmlDoc.Save(str_ApplicationStartUpPath & "\CompanyList.xml")

            Return True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        Finally

            NewNode = Nothing
            xmlDoc = Nothing

        End Try
    End Function

    Private Function UpdateExistingCompany() As Boolean
        Try


            Dim testCn As New OleDbConnection("Provider=SQLOLEDB.1;Password=" & Me.txtPassword.Text & ";Integrated Security Info=False;User ID=" & Me.txtUserID.Text.Trim & ";Initial Catalog=" & Me.txtServer.Tag.ToString & ";Data Source=" & Me.txtServer.Text)
            Try
                testCn.Open()
                If testCn.State = ConnectionState.Open Then testCn.Close() Else ShowErrorMessage("Invalid connection infromation") : Exit Function

            Catch ex As Exception
                ShowErrorMessage("Invalid connection infromation: " & ex.Message) : Exit Function
            End Try

            Dim xmlDoc As New Xml.XmlDocument

            xmlDoc.Load(str_ApplicationStartUpPath & "\CompanyList.xml")
            Dim TagetNode As Xml.XmlNode = xmlDoc.SelectNodes("//AllCompanies/Company[@ID=" & Me.txtCompanyName.Tag & "]").Item(0)

            If Not IsNothing(TagetNode) Then
                TagetNode.Attributes("Name").Value = Me.txtCompanyName.Text.Trim.ToUpper
                TagetNode.Attributes("Server").Value = Me.txtServer.Text.Trim.ToUpper
                TagetNode.Attributes("UserID").Value = Me.txtUserID.Text
                TagetNode.Attributes("Password").Value = Me.txtPassword.Text

                xmlDoc.Save(str_ApplicationStartUpPath & "\CompanyList.xml")

            End If

            Return True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Function


#End Region

    Private Sub frmNewCompany_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Me.LoadGrid()

            Me.btnNew_Click(btnNew, e)


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grd.SelectionChanged
        Try
            If Me.grd.RowCount = 0 Then
                Me.btnSave.Tag = 0
                Exit Sub
            End If

            Me.txtCompanyName.Tag = grd.Rows(grd.CurrentRow.Index).Cells(0).Value
            Me.txtCompanyName.Text = grd.Rows(grd.CurrentRow.Index).Cells(1).Value
            Me.txtServer.Text = grd.Rows(grd.CurrentRow.Index).Cells(2).Value
            Me.txtUserID.Text = grd.Rows(grd.CurrentRow.Index).Cells(3).Value
            Me.txtPassword.Text = grd.Rows(grd.CurrentRow.Index).Cells(4).Value
            Me.txtServer.Tag = grd.Rows(grd.CurrentRow.Index).Cells(5).Value
            Me.txtBKPath.Text = ""

            Me.btnLoadBKFile.Enabled = False

            Me.btnSave.Text = "Update"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click, btnSave.Click, btnLoadBKFile.Click
        Try
            Dim btn As Button = CType(sender, Button)


            If btn.Name = Me.btnLoadBKFile.Name Then
                BrowseBKFile.FileName = ""
                BrowseBKFile.ShowDialog()
                If BrowseBKFile.FileName.Length > 0 Then
                    Me.txtBKPath.Text = Me.BrowseBKFile.FileName
                Else
                    'Me.txtBKPath.Text = ""
                End If

            ElseIf btn.Name = Me.btnNew.Name Then


                Me.RefreshScreen()


            ElseIf btn.Name = Me.btnSave.Name Then

                If btn.Text = "Save" Then
                    If IsValidate("New") Then
                        If Me.AddNewCompany() Then Me.LoadGrid()

                    End If
                ElseIf btn.Text = "Update" Then
                    If IsValidate("Edit") Then
                        If Me.UpdateExistingCompany() Then Me.LoadGrid()
                    End If
                End If
            End If
        Catch ex As Exception

            ShowErrorMessage(ex.Message)
        Finally

        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub grd_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd.CellContentClick

    End Sub
End Class
Imports System
Imports System.Data.OleDb

Public Class frmTerminal

    Dim Id As Integer = 0

    Public FormMode As String = "adsf" ' String.Empty

    Enum enumUsers

        Id
        Code
        Name

    End Enum

    Function GetAllRecords(Optional ByVal Condition As String = "") As DataTable

        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable
        adp = New OleDbDataAdapter("SELECT * FROM tblSystemList", Con.ConnectionString)
        adp.Fill(dt)
    
        Return dt

    End Function

    Private Sub frmTerminal_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub frmDefUser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try

         Me.grdComputers.DataSource = GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub grdUsers_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdComputers.DoubleClick
        Try

            Me.Id = Me.grdComputers.GetRow.Cells(enumUsers.Id).Value.ToString
            Me.txtCode.Text = Me.grdComputers.GetRow.Cells(enumUsers.Code).Value.ToString
            Me.txtName.Text = Me.grdComputers.GetRow.Cells(enumUsers.Name).Value.ToString

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click

        Me.Id = 0
        Me.txtCode.Text = String.Empty
        Me.txtName.Text = String.Empty
        GetSecurityRights()

    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOpen.Click

        grdUsers_DoubleClick(Me, Nothing)

    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Me.ProcessRegisteration()
    End Sub
    Sub ProcessRegisteration()

        Dim cn As New OleDbConnection
        'If optSqlServer.Checked = True Then
        '    cn.ConnectionString = "Password=" & Me.txtPassword.Text.ToString & ";Integrated Security Info=False;User ID=" & Me.txtUserName.Text.ToString & ";Initial Catalog=" & Me.txtDBName.Text.ToString & ";Data Source=" & Me.txtServerName.Text.ToString
        'Else
        '    cn.ConnectionString = "Data Source=" & Me.txtServerName.Text.ToString & ";Initial Catalog=" & Me.txtDBName.Text.ToString & ";Integrated Security Info=True"
        'End If
        Dim strProgress As String = String.Empty

        If Not IsValidate() Then Exit Sub
        strProgress = "Starting registeration process"
        Me.lblProgress.Text = strProgress


        Me.ProgressBar1.Visible = True
        cn.ConnectionString = Con.ConnectionString

        Try
            Dim i As Integer = 0

            Do Until ProgressBar1.Value >= 15
                'i = i + 1
                'Me.lblProgress.Text = strProgress '& "".ToString.PadRight(i, ".").ToString.PadRight(3)
                ProgressBar1.Value = ProgressBar1.Value + 1
                Application.DoEvents()
                'System.Threading.Thread.Sleep(50)
                ' If i >= 3 Then i = 0
            Loop

            System.Threading.Thread.Sleep(500)

            If Not cn.State = ConnectionState.Open Then
                Me.lblProgress.Text = "Connecting database"
                cn.Open()
                Application.DoEvents()
                'System.Threading.Thread.Sleep(500)
                Me.lblProgress.Text = "Connected to the database"

            End If

            Do Until ProgressBar1.Value >= 30

                ProgressBar1.Value = ProgressBar1.Value + 1
                Application.DoEvents()
                'System.Threading.Thread.Sleep(50)

            Loop

            Me.lblProgress.Text = "Validating database"

            Do Until ProgressBar1.Value >= 40

                ProgressBar1.Value = ProgressBar1.Value + 1
                Application.DoEvents()
                'System.Threading.Thread.Sleep(50)

            Loop

            Dim cm As New OleDbCommand
            'Dim rd As oledbDataReader

            cm.Connection = cn
            'cm.CommandText = "select * from tblSystemList where SystemName=N'" & Environment.MachineName.ToString & "'"
            'rd = cm.ExecuteReader

            If Me.Id > 0 Then

                cm.CommandText = "Update tblSystemList set systemCode=N'" & Me.txtCode.Text & "', SystemName=N'" & Me.txtName.Text.ToString & "', SystemId=N'" & EncryptLicense(GetMACAddress) & "' where Id=" & Me.Id

            Else
                cm.CommandText = "INSERT INTO tblSystemList (SystemCode, SystemName, SystemId) VALUES   (N'" & Me.txtCode.Text & "',N'" & Me.txtName.Text.ToString & "', N'" & EncryptLicense(GetMACAddress) & "')"

            End If
            'rd.Close()

            Do Until ProgressBar1.Value >= 65

                ProgressBar1.Value = ProgressBar1.Value + 1
                Application.DoEvents()
                'System.Threading.Thread.Sleep(50)

            Loop

            Me.lblProgress.Text = "Updating terminal informations."
            cm.ExecuteNonQuery()

            Do Until ProgressBar1.Value >= 99

                ProgressBar1.Value = ProgressBar1.Value + 1
                Application.DoEvents()
                'System.Threading.Thread.Sleep(50)

            Loop

            'Me.lblProgress.Text = "Terminal registeration completed successfully."

            'msg_Information("Registeration completed successfully.")


            Me.ResetControls()

        Catch ex As OleDbException
            If ex.ErrorCode = 208 Then
                ShowErrorMessage("Database is not valid")
            Else
                ShowErrorMessage(ex.Message)

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            cn.Close()
        End Try

    End Sub

    Sub ResetControls()
        Me.txtCode.Text = String.Empty
        Me.txtName.Text = String.Empty
        Me.txtCode.Focus()
        Me.Id = 0
        Me.ProgressBar1.Value = 0
        Me.lblProgress.Text = String.Empty
        Me.ProgressBar1.Visible = False
        Me.grdComputers.DataSource = Me.GetAllRecords()
        GetSecurityRights()
    End Sub

    Function IsValidate() As Boolean
        Try
            Dim LID1 As String = getConfigValueByType("LID1").ToString
            If LID1 <> "Error" Then
                Dim LID2 As String = DecryptLicense(LID1).Replace("terminal", "")
                If LID2.Contains("Bad Data.") Then
                    LID2 = Decrypt(LID1).Replace("terminal", "")
                End If
                If Id = 0 Then
                    If grdComputers.RowCount >= Convert.ToInt32(LID2) Then
                        ShowErrorMessage("You are not allowed to add more terminals.")
                        txtCode.Focus()
                        Return False
                    End If
                End If
            End If
            If txtCode.Text = String.Empty Then
                ShowErrorMessage("Please enter code.")
                txtCode.Focus()
                Return False
            End If

            If txtName.Text = String.Empty Then
                ShowErrorMessage("Please enter name.")
                txtName.Focus()
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                'Me.btnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        'Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub


    Private Sub grdComputers_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdComputers.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

      
    End Sub
End Class

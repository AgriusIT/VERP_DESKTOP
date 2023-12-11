Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Security.AccessControl
Imports System.IO
Public Class frmTakingDatabaseBackup

    Public Function CreateBackup() As Boolean

        Dim strDate As String = String.Empty
        Dim strBackup As String = String.Empty
        Dim buLocation As String = String.Empty
        'Dim frmPB As frmTakingDatabaseBackup

        Try

            'frmPB.Width = 400
            'frmPB.Height = 75
            'frmPB.Text = "Taking backup of data please wait ..."
            'frmPB.StartPosition = FormStartPosition.CenterScreen

            'Dim pb As New ProgressBar
            'pb.Style = ProgressBarStyle.Marquee
            'pb.Enabled = True
            'pb.Dock = DockStyle.Fill
            'frmPB.Controls.Add(pb)

            'frmTakingDatabaseBackup.BringToFront()
            'frmTakingDatabaseBackup.TopMost = True
            Application.DoEvents()

            Dim ConStrBuilder As New OleDbConnectionStringBuilder(Con.ConnectionString)
            Dim dbName As String = ConStrBuilder.Item("Initial Catalog")
            ConStrBuilder.Item("Initial Catalog") = "master"
            Dim Con1 As New OleDbConnection(ConStrBuilder.ConnectionString.ToString)
            If Con1.State = ConnectionState.Closed Then Con1.Open()
            buLocation = getConfigValueByType("DatabaseBackup").ToString()
            Application.DoEvents()

            Dim ServerName As String = ConStrBuilder.DataSource
            If ServerName = "." Then
                ServerName = Environment.MachineName
            End If
            If ServerName.Contains("\") Then
                ServerName = ServerName.Substring(0, ServerName.LastIndexOf("\"))
            End If
            Application.DoEvents()
            'If Environment.MachineName = ServerName Then
            If Not buLocation.Trim.Length > 0 Then
                '// When configuration backup path is empty

                If Not IO.Directory.Exists(Application.StartupPath & "\Database\Backup") Then
                    IO.Directory.CreateDirectory(Application.StartupPath & "\Database\Backup")
                End If
                buLocation = Application.StartupPath & "\Database\Backup"
            Else
                '// When backup path is set
                '// Checking path exist and creating folder
                If Not IO.Directory.Exists(buLocation) Then
                    Try
                        IO.Directory.CreateDirectory(buLocation)
                    Catch ex As Exception

                        If Not IO.Directory.Exists(Application.StartupPath & "\Database\Backup") Then
                            IO.Directory.CreateDirectory(Application.StartupPath & "\Database\Backup")
                        End If

                        buLocation = Application.StartupPath & "\Database\Backup"

                    End Try
                End If

            End If

            Application.DoEvents()
            Try
                '//Changing Window's login user access to write on backup directory
                AddDirectorySecurity(buLocation, "Everyone", FileSystemRights.FullControl, AccessControlType.Allow)
            Catch ex As Exception

            End Try

            Application.DoEvents()
            Dim mPassword As String = Decrypt(getConfigValueByType("DatabaseBackupPassword").ToString())
            strDate = Date.Now.Date.ToString("dd/MM/yyyy")

            Dim BackupFileName As String = String.Empty
            For i As Integer = 0 To 10

                If Not File.Exists(buLocation + "\" + dbName + strDate.Replace("/", "") + IIf(i = 0, "", "_" & i.ToString) + ".Bak") Then
                    BackupFileName = buLocation + "\" + dbName + strDate.Replace("/", "") + IIf(i = 0, "", "_" & i.ToString) + ".Bak"
                    Exit For
                End If

            Next

            Application.DoEvents()
            If BackupFileName.Trim.Length = 0 Then
                Exit Function
            End If
            'strBackup = "Backup Database " & dbName & " To Disk='" & buLocation + "\" + dbName + strDate.Replace("/", "") + ".Bak" & "' WITH MEDIAPASSWORD = '" & mPassword & "'"
            strBackup = "Backup Database " & dbName & " To Disk='" & BackupFileName & "'"
            Dim SqlCommand As OleDbCommand = New OleDbCommand(strBackup, Con1)
            SqlCommand.CommandTimeout = 600
            SqlCommand.ExecuteNonQuery()
            Application.DoEvents()
            HasBackup = True
            Con1.Close()
            ConStrBuilder.Item("Initial Catalog") = dbName
            Dim time As DateTime = New DateTime
            ServerDate()
            time = GetServerDate
            Dim Con2 As New OleDbConnection(ConStrBuilder.ConnectionString.ToString)
            If Con2.State = ConnectionState.Closed Then Con2.Open()
            Application.DoEvents()

            Dim SqlCommand1 As OleDbCommand = New OleDbCommand()
            SqlCommand1.Connection = Con2
            SqlCommand1.CommandText = "Insert into DatabaseBackupProcess(BackupDate, Status, UserID, System) Values( Convert(datetime, '" & time.ToString("yyyy-M-d 00:00:00") & "', 102), 1, " & LoginUserId & ", N'" & Environment.MachineName.ToString() & "') "
            SqlCommand1.ExecuteNonQuery()
            Application.DoEvents()

            'SqlCommand1.CommandText = "Insert into DatabaseBackupLog(LogDate, Status, UserID, UserName, Terminal, BackupPath, BackupFile, DbName ) Values( Convert(datetime, '" & time.ToString("yyyy-M-d 00:00:00") & "', 102), 1, " & LoginUserId & ",  '" & LoginUserName & "', N'" & Environment.MachineName.ToString() & "', '" & BackupFileName & "', '" & dbName & "') "
            ''SqlCommand.CommandTimeout = 600
            'SqlCommand1.ExecuteNonQuery()
            'Compressed("'" & Me.txtlocation.Text + "\" + Me.cmbDatabases.Text + strDate.Replace("/", "") + ".Bak" & "'")

            Application.DoEvents()

            If buLocation.Length > 0 Then
                ' Dim strFile As String = "'" & buLocation + "\" + dbName + strDate.Replace("/", "") + ".Bak" & "'"
                If mPassword <> "Error" AndAlso mPassword.Trim.Length > 0 Then
                    Zip(BackupFileName.Replace("'", ""), mPassword)
                Else
                    Zip(BackupFileName.Replace("'", ""))
                End If
            End If

            'End If
            Application.DoEvents()

        Catch ex As Exception

            Throw ex

        Finally

            'frmPB.Close()

        End Try
    End Function

End Class
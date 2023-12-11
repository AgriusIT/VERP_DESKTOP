Imports System.IO
Imports System.Net
Imports System.ComponentModel
Imports System.IO.Compression
Public Class frmReleaseDownload

    Dim DeleteReleaseFileonError As Boolean = False
    
    Private Sub frmReleaseDownload_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            If IO.File.Exists(str_ApplicationStartUpPath & "\ReleaseUpdate\Temp\Updater.exe") Then

                If Directory.Exists(str_ApplicationStartUpPath & "\ReleaseUpdate\Temp\UpdateFiles") Then

                    Process.Start(str_ApplicationStartUpPath & "\ReleaseUpdate\Temp\Updater.exe")
                    End

                Else

                    Directory.Delete(str_ApplicationStartUpPath & "\ReleaseUpdate\Temp\", True)
                    Me.Close()
                End If
            Else

                GetRelease()

            End If

        Catch ex As Exception
            msg_Error("Error updating software: " & ex.Message)
        End Try
    End Sub
  
    Public Function GetRelease() As Boolean
    Try
            If Not File.Exists(str_ApplicationStartUpPath & "\ReleaseUpdate.zip") Then


                Dim filepath As String = String.Empty

                Dim req As WebRequest = WebRequest.Create("http://SIRIUS.net/Downloads/V5/LatestVersionLink.txt")
                Dim rep As WebResponse = req.GetResponse
                Dim fsReader As IO.StreamReader
                fsReader = New IO.StreamReader(rep.GetResponseStream)

                Dim webClient As New System.Net.WebClient()
                Dim LinkAddress As String = String.Empty

                Dim CurrentR As String = fsReader.ReadToEnd
                Dim CurrentRelease As String()
                CurrentRelease = CurrentR.Split(",")

                If CurrentRelease.Length > 0 Then
                    For Each r As String In CurrentRelease
                        Dim Str() As String = r.Split("=")
                        If Str.Length > 0 Then
                            Select Case Str(0).Trim

                                Case "Version"
                                    If Val(My.Application.Info.Version.ToString.Replace(".", "")) >= Val(Str(1)) Then
                                        Me.Close()
                                        Exit Function
                                    End If
                                Case "Path"
                                    LinkAddress = Str(1).ToString

                            End Select


                        End If

                    Next
                End If

                If LicenseExpiryType <> "Monthly" AndAlso LicenseStatus = "Expired" Then
                    msg_Error("Can't update your software version because services has expired.", 30)
                    Me.Close()
                    Exit Function
                End If

                Dim myUri As New Uri(LinkAddress)

                filepath = str_ApplicationStartUpPath & "\ReleaseUpdate.zip"
                req = WebRequest.Create(myUri.AbsoluteUri)
                rep = req.GetResponse

                If rep.ContentLength > 0 Then

                    frmModProperty.DownloadInProgress = True

                    AddHandler webClient.DownloadFileCompleted, AddressOf Completed
                    AddHandler webClient.DownloadProgressChanged, AddressOf ProgressChanged
                    webClient.DownloadFileAsync(myUri, filepath)

                Else
                    'MsgBox("File not found", MsgBoxStyle.Critical)
                End If
            Else

                DeleteReleaseFileonError = True
                bgw_Extract_Release.RunWorkerAsync()


            End If
        Catch ex As Exception
            Throw New Exception("Error while downloading software update:" & ex.Message, ex.InnerException)
        End Try
    End Function

    Private Sub ProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs)
        Try
            ProgressBar1.Value = e.ProgressPercentage
            Application.DoEvents()

        Catch ex As Exception
            msg_Error("Error while downloading software update: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Completed(sender As Object, e As AsyncCompletedEventArgs)
        Try
            DeleteReleaseFileonError = False
            bgw_Extract_Release.RunWorkerAsync()

            Me.lblHeader.Text = "Download completed"
            Application.DoEvents()

            Me.lblHeader.Text = "Configuring updates"

            While bgw_Extract_Release.IsBusy
                Application.DoEvents()
            End While
            frmModProperty.DownloadInProgress = False
            Me.Close()
        Catch ex As Exception
            msg_Error("Software update download completed but some error occured: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

  
    Private Sub bgw_Extract_Release_DoWork(sender As Object, e As DoWorkEventArgs) Handles bgw_Extract_Release.DoWork

        Try

            Dim extractPath As String = str_ApplicationStartUpPath & "\ReleaseUpdate\"

            If Directory.Exists(extractPath & "temp") Then

                Directory.Delete(extractPath & "temp", True)

            End If

            ZipFile.ExtractToDirectory(str_ApplicationStartUpPath & "\ReleaseUpdate.zip", extractPath & "temp")
            File.Delete(str_ApplicationStartUpPath & "\ReleaseUpdate.zip")

        Catch ex As Exception

            If DeleteReleaseFileonError = True AndAlso IO.File.Exists(str_ApplicationStartUpPath & "\ReleaseUpdate.zip") Then
                File.Delete(str_ApplicationStartUpPath & "\ReleaseUpdate.zip")
            End If
            msg_Error("Error uncompress async process of software update: " & ex.Message)

        Finally
            DeleteReleaseFileonError = False
            frmModProperty.DownloadInProgress = False

        End Try

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            frmModProperty.DownloadInProgress = False
            Me.Close()

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
End Class
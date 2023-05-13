Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq


Public Class frmConfigCompanyPath

    Public isFormOpen As Boolean = False
    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty

    Private Sub frmConfigCompanyPath_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try

            If getConfigValueByType("BackupDBPath").ToString <> "Error" Then
                Me.txtBackupDBPath.Text = getConfigValueByType("BackupDBPath").ToString
            Else
                Me.txtBackupDBPath.Text = String.Empty
            End If

            If getConfigValueByType("AssetPicturePath").ToString <> "Error" Then
                Me.txtAssetPicturePath.Text = getConfigValueByType("AssetPicturePath").ToString
            Else
                Me.txtAssetPicturePath.Text = String.Empty
            End If

            If getConfigValueByType("ArticlePicturePath").ToString <> "Error" Then
                Me.txtArticlePicturePath.Text = getConfigValueByType("ArticlePicturePath").ToString
            Else
                Me.txtArticlePicturePath.Text = String.Empty
            End If

            If getConfigValueByType("FileAttachmentPath").ToString <> "Error" Then
                Me.txtFilesAttachmentPath.Text = getConfigValueByType("FileAttachmentPath").ToString
            Else
                Me.txtFilesAttachmentPath.Text = String.Empty
            End If

            If getConfigValueByType("CMFADocumentAttachmentPath").ToString <> "Error" Then
                Me.txtCMFADocumentAttachmentPath.Text = getConfigValueByType("CMFADocumentAttachmentPath").ToString
            Else
                Me.txtCMFADocumentAttachmentPath.Text = String.Empty
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub linkSecurityRights_Click(sender As Object, e As EventArgs) Handles linkSecurityRights.Click
        Try
            If frmConfigSecurityRights.isFormOpen = True Then
                frmConfigSecurityRights.Dispose()
            End If

            frmConfigSecurityRights.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkSMS_Click(sender As Object, e As EventArgs) Handles linkSMS.Click
        Try
            If frmConfigSMS.isFormOpen = True Then
                frmConfigSMS.Dispose()
            End If

            frmConfigSMS.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkCompanyInfo_Click(sender As Object, e As EventArgs) Handles linkCompanyInfo.Click
        Try
            If frmConfigCompanyInfo.isFormOpen = True Then
                frmConfigCompanyInfo.Dispose()
            End If

            frmConfigCompanyInfo.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkDB_Click(sender As Object, e As EventArgs) Handles linkDB.Click
        Try
            If frmConfigDB.isFormOpen = True Then
                frmConfigDB.Dispose()
            End If

            frmConfigDB.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkGeneral_Click(sender As Object, e As EventArgs) Handles linkGeneral.Click
        Try
            If frmConfigCompany.isFormOpen = True Then
                frmConfigCompany.Dispose()
            End If

            frmConfigCompany.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBrowseBackupDB_Click(sender As Object, e As EventArgs) Handles btnBrowseBackupDB.Click
        Try
            If Me.FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtBackupDBPath.Text = FolderBrowserDialog1.SelectedPath
                frmConfigCompany.SaveConfiguration("BackupDBPath", Me.txtBackupDBPath.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBrowseArticlePicture_Click(sender As Object, e As EventArgs) Handles btnBrowseArticlePicture.Click
        Try
            If Me.FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtArticlePicturePath.Text = FolderBrowserDialog1.SelectedPath
                frmConfigCompany.SaveConfiguration("ArticlePicturePath", Me.txtArticlePicturePath.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBrowseAssetPicture_Click(sender As Object, e As EventArgs) Handles btnBrowseAssetPicture.Click
        Try
            If Me.FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtAssetPicturePath.Text = FolderBrowserDialog1.SelectedPath
                frmConfigCompany.SaveConfiguration("AssetPicturePath", Me.txtAssetPicturePath.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBrowseFilesAttachment_Click(sender As Object, e As EventArgs) Handles btnBrowseFilesAttachment.Click
        Try
            Dim OpenDirectory As New FolderBrowserDialog
            If OpenDirectory.ShowDialog = Windows.Forms.DialogResult.OK Then
                If IO.Directory.Exists(OpenDirectory.SelectedPath) Then
                    Me.txtFilesAttachmentPath.Text = OpenDirectory.SelectedPath
                    frmConfigCompany.SaveConfiguration("FileAttachmentPath", Me.txtFilesAttachmentPath.Text.ToString)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBrowseCMFADocument_Click(sender As Object, e As EventArgs) Handles btnBrowseCMFADocument.Click
        Try
            If Me.FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtCMFADocumentAttachmentPath.Text = FolderBrowserDialog1.SelectedPath
                frmConfigCompany.SaveConfiguration("CMFADocumentAttachmentPath", Me.txtCMFADocumentAttachmentPath.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class
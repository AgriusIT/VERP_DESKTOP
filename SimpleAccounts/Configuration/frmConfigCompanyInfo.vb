Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq


Public Class frmConfigCompanyInfo

    Public isFormOpen As Boolean = False
    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty

    Dim _strImagePath As String = String.Empty
    Dim IsChangedValue As Object
    Dim bytImage() As Byte

    Private Sub frmConfigCompanyInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try
            If getConfigValueByType("CompanyNameHeader").ToString <> "Error" Then
                Me.txtCompanyName.Text = getConfigValueByType("CompanyNameHeader").ToString
            Else
                Me.txtCompanyName.Text = String.Empty
            End If

            If Convert.ToString(getConfigValueByType("SIRIUSPartnerName").ToString) <> "Error" Then
                Me.txtPartnerName.Text = Convert.ToString(getConfigValueByType("SIRIUSPartnerName").ToString)
            Else
                Me.txtPartnerName.Text = String.Empty
            End If

            Dim dt As DataTable = frmConfigCompany.GetPicture("CompanyLogoConfiguration")

            If dt.Rows.Count > 0 Then
                txtCompanyLogo.Text = dt.Rows.Item(0).Item(0).ToString
            Else
                txtCompanyLogo.Text = String.Empty
            End If

            If getConfigValueByType("CompanyAddressHeader").ToString <> "Error" Then
                Me.txtCompanyAddress.Text = getConfigValueByType("CompanyAddressHeader").ToString
            Else
                Me.txtCompanyAddress.Text = String.Empty
            End If

            Me.chkCompanyWisePrefixOnVoucher.Checked = Convert.ToBoolean(getConfigValueByType("CompanyWisePrefix").ToString)

            If Me.chkCompanyWisePrefixOnVoucher.Checked = False Then
                Me.chkCompanyWisePrefixOnVoucherNot.Checked = True
            End If
            ''Start TFS4765 : Ayesha Rehman : 10-10-2018
            If Convert.ToString(getConfigValueByType("MainMenuNavigatorColor").ToString) <> "Error" Then
                Me.cmbMainMenuNavigatorColor.SelectedIndex = Convert.ToInt32(Val(getConfigValueByType("MainMenuNavigatorColor").ToString))
            Else
                Me.cmbMainMenuNavigatorColor.SelectedIndex = 0
            End If
            ''End TFS4765
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub txtCompanyName_Leave(sender As Object, e As EventArgs) Handles txtCompanyName.Leave
        Try
            Me.KeyType = "CompanyNameHeader"
            Me.KeyValue = Me.txtCompanyName.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPartnerName_Leave(sender As Object, e As EventArgs) Handles txtPartnerName.Leave
        Try
            Me.KeyType = "SIRIUSPartnerName"
            Me.KeyValue = Me.txtPartnerName.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub btnBrowseCompanyLogo_Click(sender As Object, e As EventArgs) Handles btnBrowseCompanyLogo.Click
        Try
            Me.OpenFileDialog1.Filter = "Image File |*.*png"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                If System.IO.File.Exists(Me.OpenFileDialog1.FileName) Then
                    _strImagePath = OpenFileDialog1.FileName.Replace(OpenFileDialog1.FileName, "CompanyLogo.png")

                    'Me.pbCompanyLogo.ImageLocation = OpenFileDialog1.FileName

                    pbCompanyLogo.Image = Image.FromFile(OpenFileDialog1.FileName)

                    Me.txtCompanyLogo.Text = _strImagePath

                    Dim ms As New System.IO.MemoryStream
                    Dim bmpImage As New Bitmap(pbCompanyLogo.Image)

                    bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                    bytImage = ms.ToArray()
                    ms.Close()

                    frmConfigCompany.SavePicture("CompanyLogoConfiguration", _strImagePath, bytImage)

                End If
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

    Private Sub linkCompany_Click(sender As Object, e As EventArgs) Handles linkCompany.Click
        Try
            If frmConfigCompany.isFormOpen = True Then
                frmConfigCompany.Dispose()
            End If

            frmConfigCompany.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCompanyAddress_Leave(sender As Object, e As EventArgs) Handles txtCompanyAddress.Leave
        Try
            KeyType = "CompanyAddressHeader"
            KeyValue = Me.txtCompanyAddress.Text

            frmConfigCompany.SaveConfiguration(Me.KeyType, Me.KeyValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkCompanyWisePrefixOnVoucher_CheckedChanged(sender As Object, e As EventArgs) Handles chkCompanyWisePrefixOnVoucher.CheckedChanged
        frmConfigCompany.saveRadioBtnConfig(sender)
    End Sub

    Private Sub linkPath_Click(sender As Object, e As EventArgs) Handles linkPath.Click
        Try
            If frmConfigCompanyPath.isFormOpen = True Then
                frmConfigCompanyPath.Dispose()
            End If

            frmConfigCompanyPath.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    
    Private Sub cmbMainMenuNavigatorColor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMainMenuNavigatorColor.SelectedIndexChanged
        Try
            Dim Color As Integer = 0
            Color = Me.cmbMainMenuNavigatorColor.SelectedIndex
            If cmbMainMenuNavigatorColor.Tag.ToString.Length > 0 Then frmConfigCompany.SaveConfiguration(cmbMainMenuNavigatorColor.Tag, Color)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try 
    End Sub
End Class
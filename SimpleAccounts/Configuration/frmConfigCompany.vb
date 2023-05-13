Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq

Public Class frmConfigCompany

    Public isFormOpen As Boolean = False
    Dim KeyType As String = String.Empty
    Dim KeyValue As String = String.Empty
    Dim key As String
    Dim _strImagePath As String = String.Empty
    Dim IsChangedValue As Object
    Dim bytImage() As Byte


    Public Function SaveConfiguration(ByVal KeyType As String, ByVal KeyValue As String, Optional ByVal CreateNewKey As Boolean = False) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim Cmd As New SqlCommand
        Cmd.Connection = Con
        Cmd.Transaction = trans
        Try
            Dim strSQL As String = String.Empty

            If CreateNewKey = True Then

                strSQL = "IF NOT EXISTS(SELECT * From ConfigValuesTable  WHERE Config_Type='" & KeyType & "' ) " _
                        & " Insert into ConfigValuesTable(config_id, config_Type, config_Value, Comments, IsActive) Select Max(config_id) + 1, '" & KeyType & "', '" & KeyValue & "', '', 1 from ConfigValuesTable " _
                        & "Else " _
                        & " UPDATE ConfigValuesTable SET Config_Value=N'" & KeyValue & "' WHERE Config_Type=N'" & KeyType & "'"

            Else

                strSQL = "UPDATE ConfigValuesTable SET Config_Value=N'" & KeyValue & "' WHERE Config_Type=N'" & KeyType & "'"

            End If

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strSQL
            Cmd.ExecuteNonQuery()
            trans.Commit()

            SaveActivityLog("Configuration", Me.Text.ToString, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, KeyType, , , , , Me.Name.ToString)
            key = KeyType
            Dim config As ConfigSystem = objConfigValueList.Find(AddressOf GetObj)

            objConfigValueList.Remove(config)
            Dim AddConfig As New ConfigSystem
            AddConfig.Config_Type = KeyType.ToString
            AddConfig.Config_Value = KeyValue.ToString
            If config IsNot Nothing Then
                If config.Comments IsNot Nothing Then
                    AddConfig.Comments = config.Comments
                Else
                    AddConfig.Comments = Nothing
                End If
                AddConfig.IsActive = config.IsActive
            End If

            objConfigValueList.Add(AddConfig)
            key = String.Empty
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetObj(ByVal Config As ConfigSystem) As Boolean
        Try
            If Config.Config_Type = key Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub saveRadioBtnConfig(sender As Object)

        Try
            Dim chk As Windows.Forms.RadioButton
            chk = CType(sender, RadioButton)
            If Not chk.Tag Is Nothing Then
                If chk.Tag.ToString.Length > 0 Then SaveConfiguration(chk.Tag, chk.Checked)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub saveComboBoxConfig(sender As Object)

        Try
            Dim cmb As Windows.Forms.ComboBox = CType(sender, Windows.Forms.ComboBox)

            If cmb.SelectedIndex = -1 Then Exit Sub

            If cmb.Text.Length > 0 Then SaveConfiguration(cmb.Tag, cmb.Text)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub saveComboBoxValueConfig(sender As Object)

        Try
            Dim cmb As Windows.Forms.ComboBox = CType(sender, Windows.Forms.ComboBox)

            If cmb.SelectedIndex = -1 Then Exit Sub

            If cmb.Text.Length > 0 Then SaveConfiguration(cmb.Tag, cmb.SelectedValue)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub saveComboBoxNumConfig(sender As Object)

        Try
            Dim nud As NumericUpDown = CType(sender, NumericUpDown) 'Set Cast Type
            SaveConfiguration(nud.Tag.ToString, nud.Value.ToString) 'Save Configuration
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Public Sub saveDTPValueConfig(sender As Object)

        Try
            Dim dtp As DateTimePicker = CType(sender, DateTimePicker) 'Set Cast Type
            SaveConfiguration(dtp.Tag.ToString, dtp.Value.Date.ToString("yyyy-M-d h:mm:ss tt")) 'Save Configuration
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Public Sub saveCheckConfig(sender As Object)

        Try
            Dim chk As Windows.Forms.CheckBox
            chk = CType(sender, CheckBox)
            If Not chk.Tag Is Nothing Then ''Added this LOC to check if Tag has something or not
                If chk.Tag.ToString.Length > 0 Then SaveConfiguration(chk.Tag, chk.Checked)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try

            Me.chkPreviewInvoice.Checked = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)

            If Me.chkPreviewInvoice.Checked = False Then
                Me.chkPreviewInvoiceNot.Checked = True
            End If

            Me.chkAutoUpdate.Checked = Convert.ToBoolean(getConfigValueByType("EnableAutoUpdate").ToString.Replace("Error", "True"))

            If Me.chkAutoUpdate.Checked = False Then
                Me.chkAutoUpdateNot.Checked = True
            End If

            Me.chkReminder.Checked = Convert.ToBoolean(getConfigValueByType("Reminder").ToString)

            If Me.chkReminder.Checked = False Then
                Me.chkReminderNot.Checked = True
            End If

            Me.chkBarcodeEnabled.Checked = Convert.ToBoolean(getConfigValueByType("BarcodeEnabled").ToString)

            If Me.chkBarcodeEnabled.Checked = False Then
                Me.chkBarcodeEnabledNot.Checked = True
            End If

            
            'Start TFS4884
            Me.chkItemConfigurationDependencies.Checked = Convert.ToBoolean(getConfigValueByType("ItemConfigurationDependencies").ToString)

            If Me.chkItemConfigurationDependencies.Checked = False Then
                Me.chkItemConfigurationDependenciesNot.Checked = True
            End If
            ''End TFS4884
            Me.chkSMSWithEngineNo.Checked = Convert.ToBoolean(getConfigValueByType("DeliveryChalanByEnigneNo").ToString)

            If Me.chkSMSWithEngineNo.Checked = False Then
                Me.chkSMSWithEngineNoNot.Checked = True
            End If

            Me.rbtListSearchStartWith.Checked = Convert.ToBoolean(getConfigValueByType("ListSearchStartWith"))
            Me.rbtListSearchContains.Checked = Convert.ToBoolean(getConfigValueByType("ListSearchContains"))

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmConfigCompany_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.isFormOpen = True
        getConfigValueList()
        GetAllRecords()

    End Sub

    Public Function SavePicture(Optional ByVal KeyType As String = "", Optional ByVal KeyPath As String = "", Optional ByVal bytImage() As Byte = Nothing) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim dt As New DataTable
        Dim strSQL As String = String.Empty
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim Cmd As New SqlCommand
        Cmd.Connection = Con
        Cmd.Transaction = trans
        Try
            Dim selectPic As String = "Select PictureId, PicturePath, PictureType FROM tblPictures Where PictureType='" & KeyType.Trim.Replace("'", "''") & "' "
            dt = GetDataTable(selectPic)
            If Not dt Is Nothing Then
                If dt.Rows.Count > 0 Then
                    strSQL = "Update tblPictures Set PicturePath =N'" & KeyPath.Trim.Replace("'", "''") & "', PictureType= N'" & KeyType.Trim.Replace("'", "''") & "' , Picture = @image" & " Where PictureType='" & KeyType.Trim.Replace("'", "''") & "' "
                Else
                    strSQL = " Insert into tblPictures(PicturePath, PictureType, Picture) Values(N'" & KeyPath & "', N'" & KeyType & "' , @image) "
                End If

            End If

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strSQL
            Cmd.Parameters.AddWithValue("@image", bytImage)
            Cmd.ExecuteNonQuery()
            trans.Commit()

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetPicture(Optional ByVal KeyType As String = "", Optional ByVal KeyPath As String = "") As DataTable
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim dt As New DataTable
        Dim strSQL As String = String.Empty

        Dim Cmd As New SqlCommand
        Cmd.Connection = Con

        Try
            Dim selectPicPath As String = "Select PicturePath FROM tblPictures Where PictureType='" & KeyType.Trim.Replace("'", "''") & "' "
            dt = GetDataTable(selectPicPath)
            Return dt
        Catch ex As Exception

            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

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

    Private Sub chkPreviewInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles chkPreviewInvoice.CheckedChanged
        saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkAutoUpdate_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoUpdate.CheckedChanged
        saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkBarcodeEnabled_CheckedChanged(sender As Object, e As EventArgs) Handles chkBarcodeEnabled.CheckedChanged
        saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkSMSWithEngineNo_CheckedChanged(sender As Object, e As EventArgs) Handles chkSMSWithEngineNo.CheckedChanged
        saveRadioBtnConfig(sender)
    End Sub

    Private Sub rbtListSearchStartWith_CheckedChanged(sender As Object, e As EventArgs) Handles rbtListSearchStartWith.CheckedChanged
        saveRadioBtnConfig(sender)
    End Sub

    Private Sub rbtListSearchContains_CheckedChanged(sender As Object, e As EventArgs) Handles rbtListSearchContains.CheckedChanged
        saveRadioBtnConfig(sender)
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

    Private Sub chkReminder_CheckedChanged(sender As Object, e As EventArgs) Handles chkReminder.CheckedChanged
        saveRadioBtnConfig(sender)
    End Sub

    Private Sub chkDisablePackQty_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisablePackQty.CheckedChanged, chkItemConfigurationDependencies.CheckedChanged
        Try
            saveRadioBtnConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class frmWizardConfig
    Implements IGeneral
    Private _ListConfigValue As New List(Of ConfigSystem)
    Private _CompanyName As New ConfigSystem
    Private _CompanyAddress As New ConfigSystem
    Private _ShowHeader As New ConfigSystem
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            If Condition = "CompanyInfo" Then
                _CompanyName.Config_Type = "CompanyNameHeader"
                _CompanyName.Config_Value = Me.txtCompanyName.Text
                _ListConfigValue.Add(_CompanyName)
                _CompanyAddress.Config_Type = "CompanyAddressHeader"
                _CompanyAddress.Config_Value = Me.txtAddress.Text
                _ListConfigValue.Add(_CompanyAddress)
                _ShowHeader.Config_Type = "ShowCompanyAddressOnPageHeader"
                _ShowHeader.Config_Value = "True"
                _ListConfigValue.Add(_ShowHeader)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Me.txtCompanyName.Text = getConfigValueByType("CompanyNameHeader").ToString
            Me.txtAddress.Text = getConfigValueByType("CompanyAddressHeader").ToString
            Me.cmbVoucherFormat.Text = getConfigValueByType("VoucherNo").ToString
            Me.chkAllowMinusStock.Checked = Convert.ToBoolean(getConfigValueByType("AllowMinusStock").ToString)
            Me.chkShowMasterGrid.Checked = Convert.ToBoolean(getConfigValueByType("ShowMasterGrid").ToString)
            Me.chkPreviewInvoice.Checked = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
            Me.chkAutoUpdate.Checked = Convert.ToBoolean(getConfigValueByType("EnableAutoUpdate").ToString)
            Me.txtTransitInssuranceTax.Text = Convert.ToDouble(getConfigValueByType("TransitInssuranceTax").ToString)
            Me.txtWHTax.Text = Convert.ToDouble(getConfigValueByType("WHTax_Percentage").ToString)
            Me.txtDefaultTax.Text = Convert.ToDouble(getConfigValueByType("Default_Tax_Percentage").ToString)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            GetAllRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If Condition = "CompanyInfo" Then
                If New SBDal.ConfigSystemDAL().SaveConfigSys(_ListConfigValue) Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try

            If Me.txtCompanyName.Text = String.Empty Then
                ShowErrorMessage("Please enter company name")
                Me.txtCompanyName.Focus()
                Exit Sub
            End If
            If Me.txtAddress.Text = String.Empty Then
                ShowErrorMessage("Please enter company address")
                Me.txtAddress.Focus()
                Exit Sub
            End If
            FillModel("CompanyInfo")
            If Save("CompanyInfo") Then
                msg_Information(str_informSave)
                SaveTabIndex(Me.UltraTabControl1.SelectedTab.Index)
            End If
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function SaveTabIndex(ByVal TabId As Integer) As Boolean
        Dim con As New SqlConnection(SQLHelper.CON_STR)
        If con.State = ConnectionState.Closed Then con.Open()
        Dim trans As SqlTransaction = con.BeginTransaction
        Try
            Dim flag As Boolean = False
            Dim strQuery As String = "Select * From tblWizardConfigTab WHERE Tab=" & TabId
            Dim dt As DataTable = UtilityDAL.GetDataTable(strQuery, trans)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    flag = True
                Else
                    flag = False
                End If
            End If
            Dim str As String = String.Empty
            If flag = False Then
                str = "INSERT INTO tblWizardConfigTab(Tab) Values(" & Me.UltraTabControl1.SelectedTab.Index & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            Else
            End If
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
        End Try
    End Function
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmWizardConfig_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ReSetControls()
            Dim dtTabData As DataTable = GetDataTable("Select ISNULL(Max(Tab),0) From tblWizardConfigTab")
            If dtTabData IsNot Nothing Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(dtTabData.Rows(0).Item(0))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(2).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(2).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
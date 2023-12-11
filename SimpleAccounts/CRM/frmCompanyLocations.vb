Imports SBDal
Imports System.Text.RegularExpressions
Public Class frmCompanyLocations


    'Implements IGeneral
    Dim operations As CRUD_db = New CRUD_db
    Dim ID As Integer = 0

    Private Sub frmCompanyLocations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FillCombos("LocationType")
            FillCombos("Country")
            FillCombos("Company")
            GetHistory()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Function IsValidEmailFormat(ByVal s As String) As Boolean
        If (s = "") Then
            Return True
        Else
            Return Regex.IsMatch(s, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")
        End If

    End Function
    Public Sub FillCombos(Optional Condition As String = "")
        Try

            Dim strSQL = String.Empty

            'If Condition = "" Or Condition = "ContactType" Then
            '    strSQL = String.Empty
            '    strSQL = "SELECT [ContactTypeId] ,[ContactType] ,[Active] FROM [tblDefContactType] where active=1"
            '    FillDropDown(Me.cmbContactType, strSQL, False)

            'End If

            If Condition = "LocationType" Then
                strSQL = String.Empty
                strSQL = "SELECT [LocationTypeId] ,[LocationType] ,[Active] FROM [tblDefCompanyLocationType] where active=1 "
                FillDropDown(Me.cmbType, strSQL)

            End If

            If Condition = "Country" Then
                strSQL = String.Empty
                strSQL = "SELECT [CountryId] ,[CountryName]  from tblListCountry where active = 1 order by SortOrder, CountryName desc"
                FillDropDown(Me.cmbCountry, strSQL)

                If cmbCountry.Items.Count > 1 Then
                    cmbCountry.SelectedIndex = 1
                End If


            End If

            If Condition = "Company" Then
                strSQL = String.Empty
                strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                        & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                        & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Vendor', 'Customer','Bank') and  vwCOADetail.coa_detail_id is not  null"
                FillUltraDropDown(Me.cmbReference, strSQL)
                Me.cmbReference.Rows(0).Activate()
                Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    'Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
    '    Try
    '        Dim strSQL As String = "Select [CompanyId], [LocationType], [LocationTitle], [AddressLine1], [AddressLine2], [Country], [State], [City], [Area], [EmailAddress] from tblDefCompanyLocations"
    '        '   Me.GrdStatus.DataSource = GetDataTable(strSQL)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "")
        Try

            If txtLocationTitle.Text.Length <= 0 Then

                msg_Error("Please enter company location")
                Return False
            Else
                Return True

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim flag As Boolean = True
        Dim emailarray As String = txtEmail.Text
        Dim result As String() = Regex.Split(emailarray, " ")
        For Each s As String In result
            flag = IsValidEmailFormat(s)
            If flag = False Then
                Exit For
            End If
        Next

        Try
            If IsValidate() AndAlso flag Then

                Dim strSQL As String = String.Empty
                If (btnSave.Text = "&Save") Then
                    strSQL = "INSERT INTO [dbo].[tblDefCompanyLocations] ([CompanyId], [LocationType], [LocationTitle], [AddressLine1], [AddressLine2], [Country], [State], [City], [Area], [EmailAddress], [PhoneNo], [FaxNo], [UANNo]) " & _
                        " values (" & Me.cmbReference.ActiveRow.Cells(0).Value & ", " & Me.cmbType.SelectedValue & ", '" & Me.txtLocationTitle.Text & "', '" & Me.txtAddressLine1.Text & "', '" & Me.txtAddressLine2.Text & "','" & Me.cmbCountry.SelectedValue & "',  '" & Me.cmbState.SelectedValue & "', '" & Me.cmbCity.SelectedValue & "', '" & Me.cmbArea.SelectedValue & "', '" & Me.txtEmail.Text & "', '" & Me.txtPhone.Text & "', '" & Me.txtFax.Text & "', '" & Me.txtUAN.Text & "') "
                    operations.Save(strSQL)
                    reset_txt()
                    GetHistory()
                Else
                    strSQL = "Update tblDefCompanyLocations set [CompanyId] = '" & cmbReference.Value & "' , [LocationType] = '" & cmbType.SelectedValue & "' , [LocationTitle]  = '" & txtLocationTitle.Text & "' , [AddressLine1] = '" & Me.txtAddressLine1.Text & "', [AddressLine2] = '" & Me.txtAddressLine2.Text & "', [Country] = '" & cmbCountry.SelectedValue & "', [State] = '" & cmbState.SelectedValue & "', [City] = '" & cmbCity.SelectedValue & "', [Area] = '" & cmbArea.SelectedValue & "', [EmailAddress] = '" & txtEmail.Text & "', [PhoneNo]  = '" & txtPhone.Text & "', [FaxNo]= '" & txtFax.Text & "', [UANNo]= '" & txtUAN.Text & "' where  LocationID= '" & ID & "' "
                    operations.Save(strSQL)
                    reset_txt()
                    GetHistory()
                End If
            Else
                msg_Error("Reason is not stated or invalid email.")
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try


    End Sub

    Private Sub cmbCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCountry.SelectedIndexChanged
        Try
            Dim strSQL As String
            strSQL = String.Empty
            strSQL = "SELECT [StateId] ,[StateName] from tblListState Where CountryId=" & Me.cmbCountry.SelectedValue & " and active = 1 order by SortOrder, StateName "
            FillDropDown(Me.cmbState, strSQL, True)
            If cmbState.Items.Count > 1 Then
                cmbState.SelectedIndex = 1
            End If


        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub cmbState_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbState.SelectedIndexChanged
        Try
            Dim strSQL As String
            strSQL = String.Empty
            strSQL = "SELECT [CityId] ,[CityName] from tblListCity Where StateId=" & Me.cmbState.SelectedValue & " and active = 1 order by SortOrder, CityName "
            FillDropDown(Me.cmbCity, strSQL, True)
            If cmbCity.Items.Count > 1 Then
                cmbCity.SelectedIndex = 1
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub cmbCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCity.SelectedIndexChanged
        Try
            Dim strSQL As String
            strSQL = String.Empty
            strSQL = "SELECT [TerritoryId] ,[TerritoryName] from tblListTerritory Where CityId=" & Me.cmbCity.SelectedValue & " and active = 1 order by SortOrder, TerritoryName "
            FillDropDown(Me.cmbArea, strSQL, True)
            If cmbArea.Items.Count > 1 Then
                cmbArea.SelectedIndex = 1
            End If


        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim ID As Integer = 0
            ID = Me.cmbType.SelectedValue
            Me.FillCombos("LocationType")
            Me.cmbType.SelectedValue = ID

            ID = Me.cmbCountry.SelectedValue
            Me.FillCombos("Country")
            Me.cmbCountry.SelectedValue = ID

            ID = Me.cmbReference.Value
            Me.FillCombos("Company")
            Me.cmbReference.Value = ID
            'reset()
            'GetHistory()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub GetHistory()
        Try
            Dim str As String = "SELECT        dbo.tblDefCompanyLocations.LocationId, dbo.tblDefCompanyLocationType.LocationType, " _
        & "  tblDefCompanyLocations.LocationTitle, dbo.tblListCountry.CountryName, dbo.tblListCity.CityName,  " _
        & "       dbo.tblListState.StateName, dbo.tblDefCompanyLocations.PhoneNo, dbo.tblDefCompanyLocations.FaxNo, dbo.tblDefCompanyLocations.UANNo, dbo.tblDefCompanyLocations.PhoneNo, dbo.tblDefCompanyLocations.EmailAddress,  " _
        & "  dbo.tblDefCompanyLocations.AddressLine1,  " _
        & "       dbo.tblDefCompanyLocations.AddressLine2, dbo.tblListTerritory.TerritoryName ,  " _
        & "  dbo.tblDefCompanyLocations.LocationType  as locationTypeID, dbo.tblDefCompanyLocations.Country,  " _
        & "  dbo.tblDefCompanyLocations.City, dbo.tblDefCompanyLocations.Area ,  " _
        & "  dbo.tblDefCompanyLocations.State  " _
        & "     FROM           dbo.tblDefCompanyLocations left outer JOIN dbo.tblDefCompanyLocationType " _
        & "  ON dbo.tblDefCompanyLocations.LocationType = dbo.tblDefCompanyLocationType.LocationTypeId " _
        & "  left outer JOIN dbo.tblListState on  dbo.tblDefCompanyLocations.State	= dbo.tblListState.StateId " _
        & "      left outer JOIN dbo.tblListCountry ON dbo.tblDefCompanyLocations.Country = dbo.tblListCountry.CountryId " _
        & "  left outer JOIN dbo.tblListCity ON dbo.tblDefCompanyLocations.City = dbo.tblListCity.CityId " _
        & "       left outer JOIN dbo.tblListTerritory ON dbo.tblDefCompanyLocations.Area = dbo.tblListTerritory.TerritoryId " _
        & " where dbo.tblDefCompanyLocations.CompanyId = '" & cmbReference.Value & "' "

            Dim dt As DataTable = operations.ReadTable(Str)
            grdLocations.DataSource = dt
        Catch ex As Exception
            grdLocations.DataSource = Nothing

        End Try

    End Sub

    Private Sub cmbReference_ValueChanged(sender As Object, e As EventArgs) Handles cmbReference.ValueChanged

    End Sub
    Sub reset()
        cmbReference.Value = 0
        grdLocations.DataSource = Nothing
        cmbType.SelectedValue = 0
        cmbCity.SelectedValue = 0
        cmbState.SelectedValue = 0
        cmbCountry.SelectedValue = 0
        cmbArea.SelectedValue = 0
        txtAddressLine1.ResetText()
        txtAddressLine2.ResetText()
        txtEmail.ResetText()
        txtLocationTitle.ResetText()
        txtPhone.ResetText()
        txtFax.ResetText()
        txtUAN.ResetText()
        btnSave.Text = "&Save"
    End Sub
    Sub reset_txt()
        cmbType.SelectedValue = 0
        cmbCountry.SelectedValue = 0
        cmbState.SelectedValue = 0
        cmbCity.SelectedValue = 0
        txtAddressLine1.ResetText()
        txtAddressLine2.ResetText()
        txtEmail.ResetText()
        txtLocationTitle.ResetText()
        txtPhone.ResetText()
        txtFax.ResetText()
        txtUAN.ResetText()
        btnSave.Text = "&Save"
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            reset()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdLocations_DoubleClick(sender As Object, e As EventArgs) Handles grdLocations.DoubleClick

        If (grdLocations.RowCount > 0) Then
            cmbType.SelectedValue = grdLocations.CurrentRow.Cells("locationTypeID").Value
            cmbCountry.SelectedValue = grdLocations.CurrentRow.Cells("Country").Value
            cmbState.SelectedValue = grdLocations.CurrentRow.Cells("State").Value
            cmbCity.SelectedValue = grdLocations.CurrentRow.Cells("City").Value
            cmbArea.SelectedValue = grdLocations.CurrentRow.Cells("Area").Value
            txtAddressLine1.Text = grdLocations.CurrentRow.Cells("AddressLine1").Value
            txtAddressLine2.Text = grdLocations.CurrentRow.Cells("AddressLine2").Value
            txtEmail.Text = grdLocations.CurrentRow.Cells("EmailAddress").Value
            txtLocationTitle.Text = grdLocations.CurrentRow.Cells("LocationTitle").Value
            txtPhone.Text = grdLocations.CurrentRow.Cells("PhoneNo").Value.ToString
            txtFax.Text = grdLocations.CurrentRow.Cells("FaxNo").Value.ToString
            txtUAN.Text = grdLocations.CurrentRow.Cells("UANNo").Value.ToString
            ID = grdLocations.CurrentRow.Cells("LocationId").Value
            btnSave.Text = "&Update"
        End If

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim str As String = "Delete from tblDefCompanyLocations where LocationID=" & ID
        operations.Save(str)
        reset_txt()
        GetHistory()
    End Sub

    Private Sub cmbReference_Leave(sender As Object, e As EventArgs) Handles cmbReference.Leave
        GetHistory()
    End Sub

    Private Sub txtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhone.KeyPress
        'If e.KeyChar <> ControlChars.Back Then
        '    e.Handled = Not (Char.IsDigit(e.KeyChar) Or e.KeyChar = " ")
        'End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub
End Class
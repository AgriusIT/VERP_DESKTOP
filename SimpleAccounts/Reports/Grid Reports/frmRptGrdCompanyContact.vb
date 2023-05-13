Imports System.Data.OleDb

Public Class frmRptGrdCompanyContact
    Private IsLoaded As Boolean = False

    Private Sub frmRptGrdCompanyContact_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                CloseDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub CloseDialog()
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            If rbtnSelective.Checked = True Then
                'If IsValidate() = True Then
                'AddRptParam("@Alphabet", Me.cmbAlphabets.SelectedItem)
                'AddRptParam("@IndexNo", Me.txtIndexNo.Text)
                'AddRptParam("@Type", Me.cmbTypes.SelectedItem)
                'AddRptParam("@CustomerID", Me.cmbSpecificCustomer.ActiveRow.Cells("Id").Value.ToString)

                Dim strFilter As String = String.Empty

                strFilter = "{SP_CompanyContacts;1.RefCompanyId} <> -1 "

                If Me.cmbAlphabets.SelectedIndex > 0 Then
                    strFilter += " AND Left({SP_CompanyContacts;1.Company},1) = '" & Me.cmbAlphabets.Text & "'"
                End If

                'If Not Me.txtIndexNo.Text = String.Empty Then
                '    strFilter += " AND {SP_CompanyContacts;1.IndexNo} = " & Me.txtIndexNo.Text
                'End If

                If Me.cmbTypes.SelectedIndex > 0 Then
                    strFilter += " AND {SP_CompanyContacts;1.Type}='" & Me.cmbTypes.Text & "'"
                End If

                If Me.cmbTypes.Text = "Friends & Family" Or Me.cmbTypes.Text = "Others" Then
                    If Me.cmbSpecificCustomer Is Nothing Then
                        Exit Sub
                    End If
                    'strFilter += " AND {SP_CompanyContacts;1.ContactName} = '" & Me.cmbSpecificCustomer.Text & "'"
                    'strFilter += " AND {SP_CompanyContacts;1.ContactName} = '" & Me.cmbSpecificCustomer.ActiveRow.Cells("Name").Value.ToString & "'"
                    If Me.cmbSpecificCustomer.ActiveRow IsNot Nothing Then
                        If Not Me.cmbSpecificCustomer.ActiveRow.Index = 0 Then
                            strFilter += " AND {SP_CompanyContacts;1.ContactName} = '" & Me.cmbSpecificCustomer.Text & "'"
                        End If
                    End If
                End If

                If Me.cmbSpecificCustomer.ActiveRow IsNot Nothing Then
                    If Me.cmbSpecificCustomer.ActiveRow.Cells(0).Value > 0 Then
                        strFilter += " AND {SP_CompanyContacts;1.RefCompanyId} = " & Me.cmbSpecificCustomer.Value
                    End If
                End If

                ShowReport("RptCompanyContacts", strFilter.ToString, "Nothing", "Nothing")
            Else
                ShowReport("RptCompanyContacts")
            End If
            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function IsValidate() As Boolean
        Try

            If Me.cmbAlphabets.SelectedIndex = 0 Then
                ShowErrorMessage("Please Select The Alphabet")
                Me.cmbAlphabets.Focus()
                Return False
            End If

            If Me.cmbTypes.SelectedIndex = 0 Then
                ShowErrorMessage("Please Select The Type")
                Me.cmbTypes.Focus()
                Return False
            End If

            If Me.cmbSpecificCustomer.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please Select The Specific Customer")
                Me.cmbTypes.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmRptGrdCompanyContact_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Me.cmbAlphabets.SelectedIndex = 0
            Me.cmbTypes.SelectedIndex = 0
            FillCombo("VendorCustomer")
            IsLoaded = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "VendorCustomer" Then
                Dim strSQL As String = String.Empty

                'If Me.cmbTypes.Text = "Friends & Family" Or Me.cmbTypes.Text = "Others" Then

                If Me.cmbTypes.Text = "Customer" Or Me.cmbTypes.Text = "Vendor" Or Me.cmbTypes.Text = "Bank" Then

                    strSQL = "select vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email" _
                            & " FROM  vwCOADetail WHERE (vwCOADetail.account_type in('" & Me.cmbTypes.Text & "')) and  vwCOADetail.coa_detail_id is not  null"

                Else

                    strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as Id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                            & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                            & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('" & Me.cmbTypes.Text & "') and  vwCOADetail.coa_detail_id is not  null" _
                            & " union all SELECT     tblCompanyContacts.RefCompanyId as Id, tblCompanyContacts.ContactName, tblCompanyContacts.Type, tblCompanyContacts.Address," _
                            & " tblCompanyContacts.Mobile, tblCompanyContacts.Mobile,tblCompanyContacts.Email " _
                            & " FROM  tblCompanyContacts where RefCompanyId=0 and type in ('" & Me.cmbTypes.Text & "')"
                End If

                FillUltraDropDown(Me.cmbSpecificCustomer, strSQL)
                Me.cmbSpecificCustomer.Rows(0).Activate()
                Me.cmbSpecificCustomer.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                Me.cmbSpecificCustomer.DisplayLayout.Bands(0).Columns("Type").Hidden = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Try
            Me.rbtnAll.Checked = True
            Me.cmbAlphabets.SelectedIndex = 0
            Me.txtIndexNo.Text = String.Empty
            Me.cmbTypes.SelectedIndex = 0
            Me.cmbSpecificCustomer.Rows(0).Activate()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbTypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTypes.SelectedIndexChanged
        Try
            If IsLoaded = False Then
                Exit Sub
            End If

            FillCombo("VendorCustomer")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtnAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnAll.CheckedChanged
        Try
            If rbtnAll.Checked = True Then
                GroupBox1.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtnSelective_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnSelective.CheckedChanged
        Try
            If rbtnSelective.Checked = True Then
                GroupBox1.Enabled = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
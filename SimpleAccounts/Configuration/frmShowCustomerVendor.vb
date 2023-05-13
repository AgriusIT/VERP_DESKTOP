Public Class frmShowCustomerVendor
    Public Shared dt As DataTable
    Public SelectedAccountId As Integer
    Public Sub GetAccountById()
        Try
            Dim str As String = "SELECT vwCOADetail.detail_title AS [Name], vwCOADetail.account_type AS Type, AccountBalance.Balance ,dbo.vwCOADetail.Contact_Phone as Phone, " & _
                                " dbo.vwCOADetail.Contact_Mobile as Mobile ,dbo.vwCOADetail.Contact_Address as Address , vwCOADetail.CityName AS City, tblCustomer.CridtLimt, tblCustomer.ContactName1, tblCustomer.ContactName2, tblCustomer.ContactName3, tblCustomer.ContactNo2, tblCustomer.ContactNo3, tblCustomer.Email, tblCustomer.Territory FROM vwCOADetail INNER JOIN tblCustomer ON vwCOADetail.coa_detail_id = tblCustomer.AccountId" & _
                                " LEFT OUTER JOIN (SELECT coa_detail_id, sum(tblVoucherDetail.debit_amount - tblVoucherDetail.credit_amount) as Balance " & _
                                " FROM  tblVoucherDetail WHERE  dbo.tblVoucherDetail.coa_detail_id = " & SelectedAccountId & " GROUP BY coa_detail_id ) AS AccountBalance ON vwCOADetail.coa_detail_id = AccountBalance.coa_detail_id " & _
                                " WHERE  dbo.vwCOADetail.coa_detail_id = " & SelectedAccountId & ""
            dt = GetDataTable(str)
            dt.TableName = "Account"
            If dt.Rows.Count > 0 Then
                Me.lblName.Text = dt.Rows(0).Item("Name").ToString
                Me.lblMobile.Text = dt.Rows(0).Item("Mobile").ToString
                Me.lblBalance.Text = dt.Rows(0).Item("Balance").ToString
                Me.lblPhone.Text = dt.Rows(0).Item("Phone").ToString
                Me.lblType.Text = dt.Rows(0).Item("Type").ToString
                Me.lblCity.Text = dt.Rows(0).Item("City").ToString
                Me.lblAddress.Text = dt.Rows(0).Item("Address").ToString

                Me.lblCreditLimit.Text = dt.Rows(0).Item("CridtLimt").ToString
                Me.lblCName1.Text = dt.Rows(0).Item("ContactName1").ToString
                Me.lblCName2.Text = dt.Rows(0).Item("ContactName2").ToString
                Me.lblCName3.Text = dt.Rows(0).Item("ContactName3").ToString
                Me.lblContact1.Text = dt.Rows(0).Item("Phone").ToString
                Me.lblContact2.Text = dt.Rows(0).Item("ContactNo2").ToString
                Me.lblContact3.Text = dt.Rows(0).Item("ContactNo3").ToString
                Me.lblEmail.Text = dt.Rows(0).Item("Email").ToString
            Else
                ShowErrorMessage("No data found against this Customer")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmAccountSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try

            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

   
    Private Sub frmAccountSearch_shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            GetAccountById()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub showInfo()
    '    Try
    '        Me.lblName.Text = dt.Rows(0).("Name")
    '        Me.lblMobile.Text = Me.grd.GetRow.Cells("Mobile").Value
    '        Me.lblBalance.Text = Me.grd.GetRow.Cells("Balance").Value
    '        Me.lblPhone.Text = Me.grd.GetRow.Cells("Phone").Value
    '        Me.lblType.Text = Me.grd.GetRow.Cells("Type").Value
    '        Me.lblCity.Text = Me.grd.GetRow.Cells("City").Value
    '        Me.lblAddress.Text = Me.grd.GetRow.Cells("Address").Value
    '        Me.lblName.Text = Me.grd.GetRow.Cells("Name").Value
    '        Me.lblMobile.Text = Me.grd.GetRow.Cells("Mobile").Value
    '        Me.lblBalance.Text = Me.grd.GetRow.Cells("Balance").Value
    '        Me.lblPhone.Text = Me.grd.GetRow.Cells("Phone").Value
    '        Me.lblType.Text = Me.grd.GetRow.Cells("Type").Value
    '        Me.lblCity.Text = Me.grd.GetRow.Cells("City").Value
    '        Me.lblAddress.Text = Me.grd.GetRow.Cells("Address").Value
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
   
    Private Sub btnClose_Click(sender As Object, e As EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmShowCustomerVendor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Button1.FlatAppearance.BorderSize = 0
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
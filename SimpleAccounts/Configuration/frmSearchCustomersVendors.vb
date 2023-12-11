''TASK TFS1781 New form to search customers and vendors. on 12-12-2017
''' <summary>
''' This Form Is designed to search Customer And Vender the basis of its Code and Name
''' </summary>
''' <remarks></remarks>
Public Class frmSearchCustomersVendors
    Public Shared dt As DataTable
    Public dv As New DataView
    Public SelectedAccountId As Integer = 0
    Public Shared Mobile As String = ""
    Public Shared CustomerCode As Integer = 0
    Public Shared CreditLimit As Double = 0
    Public Shared CustomerName As String = ""
    Public Shared CurrentBalance As Double = 0

    Private Sub GetAll()
        Dim Str As String = ""
        Try
            Str = " SELECT dbo.vwCOADetail.coa_detail_id AS Id,detail_title AS Name, detail_code AS Code, account_type AS Type, Contact_Mobile AS Mobile, Contact_Phone As Phone, tblCustomer.Phone As [Contact No 1] , tblCustomer.ContactNo2 As [Contact No 2] , tblCustomer.ContactNo3 As [Contact No 3] , Contact_Address AS Address, StateName As State , CityName AS City, TerritoryName As Territory , CustomerType ,vwCOADetail.SalesManName As SaleMan , vwCOADetail.ManagerName As Manager , vwCOADetail.DirectorName As Director , tblCustomer.CridtLimt as CreditLimit " _
                & " FROM vwCOADetail LEFT OUTER JOIN tblCustomer ON vwCOADetail.coa_detail_id = tblCustomer.AccountId"
            If rbtCustomers.Checked = True AndAlso rbtVendors.Checked = False Then
                Str += " where account_type In ('Customer')"
            End If
            If rbtVendors.Checked = True AndAlso rbtCustomers.Checked = False Then
                Str += " where  account_type In ('Vendor')"
            End If
            If (rbtVendors.Checked = True AndAlso rbtCustomers.Checked = True) Or (rbtVendors.Checked = False AndAlso rbtCustomers.Checked = False) Then
                Str += " where  account_type In ('Vendor', 'Customer')"
            End If
            dt = GetDataTable(Str)
            dt.TableName = "Account"
            dv.Table = dt
            Me.grd.DataSource = dv.ToTable
            'Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmSearchCustomersVendors_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                'frmSales.FormName = Me.Name
            ElseIf e.KeyCode = Keys.Enter Then
                Me.grd.Focus()
                If grd.RowCount > 0 Then
                    Me.SelectedAccountId = grd.GetRow.Cells(0).Value
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            If grd.RowCount > 0 Then
                Me.SelectedAccountId = grd.GetRow.Cells(0).Value
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Else
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_KeyDown(sender As Object, e As KeyEventArgs) Handles grd.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                'frmSales.FormName = Me.Name
            ElseIf e.KeyCode = Keys.Tab Then
                Me.txtSearch.Focus()
                Me.txtSearch.SelectAll()
            ElseIf e.KeyCode = Keys.Enter Then
                    CustomerName = grd.CurrentRow.Cells("Name").Value.ToString
                    CustomerCode = grd.CurrentRow.Cells("Id").Value.ToString
                    Mobile = grd.CurrentRow.Cells("Mobile").Value.ToString
                CreditLimit = Val(grd.CurrentRow.Cells("CreditLimit").Value.ToString)
                    CurrentBalance = GetCurrentBalance(Val(grd.CurrentRow.Cells("Id").Value.ToString))
                    frmPOSEntry.GetCustomerDetail(CustomerCode, CustomerName, Mobile, CreditLimit, CurrentBalance)
                    Me.Close()
                    'frmItemSearch_shown(Nothing, Nothing)
            End If
                'Me.grd.Focus()
                'If grd.RowCount > 0 Then
                '    Me.SelectedAccountId = grd.GetRow.Cells(0).Value
                '    Me.DialogResult = Windows.Forms.DialogResult.OK
                'Else
                '    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                'End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                CustomerName = grd.CurrentRow.Cells("Name").Value.ToString
                CustomerCode = grd.CurrentRow.Cells("Id").Value.ToString
                Mobile = grd.CurrentRow.Cells("Mobile").Value.ToString
                CreditLimit = grd.CurrentRow.Cells("CreditLimit").Value.ToString
                CurrentBalance = GetCurrentBalance(Val(grd.CurrentRow.Cells("Id").Value.ToString))
                frmPOSEntry.GetCustomerDetail(CustomerCode, CustomerName, Mobile, CreditLimit, CurrentBalance)
                Me.Close()
                'frmItemSearch_shown(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Down Then
                Me.grd.Focus()
            Else
                Me.txtSearch.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            If dv IsNot Nothing Then
                If Me.txtSearch.Text <> String.Empty Then

                    dv.RowFilter = " Name LIKE '%" & Me.txtSearch.Text & "%' OR Code LIKE '%" & Me.txtSearch.Text & "%' OR Mobile LIKE '%" & Me.txtSearch.Text & "%' OR Type LIKE '%" & Me.txtSearch.Text & "%' OR [Contact No 1] like '%" & Me.txtSearch.Text & "%' OR [Contact No 2] like '%" & Me.txtSearch.Text & "%' OR [Contact No 3] like '%" & Me.txtSearch.Text & "%' OR Address Like '%" & Me.txtSearch.Text & "%' Or State Like '%" & Me.txtSearch.Text & "%' Or  City Like '%" & Me.txtSearch.Text & "%' Or Territory Like '%" & Me.txtSearch.Text & "%' OR CustomerType Like '%" & Me.txtSearch.Text & "%'  OR SaleMan Like '%" & Me.txtSearch.Text & "%' OR  Manager Like '%" & Me.txtSearch.Text & "%' OR Director Like '%" & Me.txtSearch.Text & "%'  "
                    Me.grd.DataSource = dv
                Else
                    Me.grd.DataSource = dt
                End If
                ApplyGridSettings()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmSearchCustomersVendors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetAll()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cbCustomers_CheckedChanged(sender As Object, e As EventArgs) Handles rbtCustomers.CheckedChanged
        Try
            GetAll()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cbVendors_CheckedChanged(sender As Object, e As EventArgs) Handles rbtVendors.CheckedChanged
        Try
            GetAll()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings()
        Try
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns("Name").Width = 200
            Me.grd.RootTable.Columns("Address").Width = 200
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmSearchCustomersVendors_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            txtSearch.Text = String.Empty
            Me.txtSearch.Focus()
            GetAll()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs)
        Try
            If grd.RowCount > 0 Then
                Me.SelectedAccountId = grd.GetRow.Cells(0).Value
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Else
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grd.RowDoubleClick
        Try
            CustomerName = grd.CurrentRow.Cells("Name").Value.ToString
            CustomerCode = grd.CurrentRow.Cells("Id").Value.ToString
            Mobile = grd.CurrentRow.Cells("Mobile").Value.ToString
            CreditLimit = Val(grd.CurrentRow.Cells("CreditLimit").Value.ToString)
            CurrentBalance = GetCurrentBalance(Val(grd.CurrentRow.Cells("Id").Value.ToString))
            frmPOSEntry.GetCustomerDetail(CustomerCode, CustomerName, Mobile, CreditLimit, CurrentBalance)
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
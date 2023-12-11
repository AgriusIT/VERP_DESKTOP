''TASK TFS1781 New form to search customers and vendors. on 12-12-2017
''' <summary>
''' This Form Is designed to search Customer And Vender the basis of its Code and Name
''' </summary>
''' <remarks></remarks>
Public Class frmSearchWalkInCustomer
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
            'Ali Faisal : Top Search apllied to increase the speed of screen opening.
            Str = "SELECT DISTINCT TOP " & If(txtNo.Text = "", 100, txtNo.Text) & " CustomerName as Name, MobileNo as Mobile FROM SalesMasterTable WHERE isnull(CustomerName, '') not in (SELECT ISNULL(detail_title, 0) from vwcoadetail) and CustomerName is not NULL "
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
                'Ali Faisal : Top Search apllied to increase the speed of screen opening.
                Me.txtNo.Text = ""
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
                'Me.SelectedAccountId = grd.GetRow.Cells(0).Value
                Me.DialogResult = Windows.Forms.DialogResult.OK
                'Ali Faisal : Top Search apllied to increase the speed of screen opening.
                Me.txtNo.Text = ""
            Else
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                'Ali Faisal : Top Search apllied to increase the speed of screen opening.
                Me.txtNo.Text = ""
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_KeyDown(sender As Object, e As KeyEventArgs) Handles grd.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                'Ali Faisal : Top Search apllied to increase the speed of screen opening.
                Me.txtNo.Text = ""
                'frmSales.FormName = Me.Name
            ElseIf e.KeyCode = Keys.Tab Then
                Me.txtSearch.Focus()
                Me.txtSearch.SelectAll()
            ElseIf e.KeyCode = Keys.Enter Then
                CustomerName = grd.CurrentRow.Cells("Name").Value.ToString
                Mobile = grd.CurrentRow.Cells("Mobile").Value.ToString
                frmPOSEntry.GetCustomerDetail(0, CustomerName, Mobile, 0, 0)
                'Ali Faisal : Top Search apllied to increase the speed of screen opening.
                Me.txtNo.Text = ""
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
                Mobile = grd.CurrentRow.Cells("Mobile").Value.ToString
                frmPOSEntry.GetCustomerDetail(0, CustomerName, Mobile, 0, 0)
                'Ali Faisal : Top Search apllied to increase the speed of screen opening.
                Me.txtNo.Text = ""
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

                    dv.RowFilter = " Name LIKE '%" & Me.txtSearch.Text & "%' OR Mobile LIKE '%" & Me.txtSearch.Text & "%'"
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

    Private Sub cbCustomers_CheckedChanged(sender As Object, e As EventArgs)
        Try
            GetAll()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cbVendors_CheckedChanged(sender As Object, e As EventArgs)
        Try
            GetAll()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            'Ali Faisal : Top Search apllied to increase the speed of screen opening.
            Me.txtNo.Text = ""
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings()
        Try
            Me.grd.RootTable.Columns("Name").Width = 200
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
            Mobile = grd.CurrentRow.Cells("Mobile").Value.ToString
            frmPOSEntry.GetCustomerDetail(0, CustomerName, Mobile, 0, 0)
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : Top Search apllied to increase the speed of screen opening.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtNo_TextChanged(sender As Object, e As EventArgs) Handles txtNo.TextChanged
        Try
            GetAll()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
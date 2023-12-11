Imports System.Data.OleDb
Imports System.IO
Public Class frmAccountSearch
    Public Shared dt As DataTable
    Public dv As New DataView
    Public AccountType As String = String.Empty
    Public SelectedAccountId As Integer

    Public Sub GetAll()
        Try
            Dim str As String = "select coa_detail_id As id,detail_code As Code, detail_title  As Title,account_Type As Type ,Contact_Mobile AS Mobile, Contact_Phone As Phone, tblCustomer.Phone As [Contact No 1] , tblCustomer.ContactNo2 As [Contact No 2] , tblCustomer.ContactNo3 As [Contact No 3] , Contact_Address AS Address , StateName As State , CityName As City ,TerritoryName As Territory , CustomerType ,vwCOADetail.SalesManName As SaleMan , vwCOADetail.ManagerName As Manager , vwCOADetail.DirectorName AS Director  from vwCoaDetail LEFT OUTER JOIN tblCustomer ON vwCOADetail.coa_detail_id = tblCustomer.AccountId where 1=1 "
            If AccountType.Length > 0 Then

                str = str & "and account_type in ( " & AccountType & " )"
            End If
            dt = GetDataTable(str)
            dt.TableName = "Account"
            dv.Table = dt
            Me.grd.DataSource = dv.ToTable
            'Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmAccountSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
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
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmAccountSearch_shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtSearch.Text = String.Empty
            Me.txtSearch.Focus()
            GetAll()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings()
        Try
            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns(1).Width = 200
            Me.grd.RootTable.Columns(2).Width = 200
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        Try
            If e.KeyCode = Keys.Down Then
                Me.grd.Focus()
            Else
                Me.txtSearch.Focus()
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            If dv IsNot Nothing Then
                If Me.txtSearch.Text <> String.Empty Then

                    dv.RowFilter = "Code LIKE '%" & Me.txtSearch.Text & "%' or Title LIKE '%" & Me.txtSearch.Text & "%' or Type LIKE '%" & Me.txtSearch.Text & "%' or Mobile LIKE '%" & Me.txtSearch.Text & "%' or City LIKE '%" & Me.txtSearch.Text & "%' or Address LIKE '%" & Me.txtSearch.Text & "%' OR [Contact No 1] like '%" & Me.txtSearch.Text & "%' OR [Contact No 2] like '%" & Me.txtSearch.Text & "%' OR [Contact No 3] like '%" & Me.txtSearch.Text & "%' OR Address Like '%" & Me.txtSearch.Text & "%' Or State Like '%" & Me.txtSearch.Text & "%'  Or Territory Like '%" & Me.txtSearch.Text & "%' OR CustomerType Like '%" & Me.txtSearch.Text & "%'  OR SaleMan Like '%" & Me.txtSearch.Text & "%' OR  Manager Like '%" & Me.txtSearch.Text & "%' OR Director Like '%" & Me.txtSearch.Text & "%' "

                    Me.grd.DataSource = dv
                Else
                    Me.grd.DataSource = dt
                End If
                ApplyGridSettings()
            End If
            'Me.grd.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmAccountSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub grd_KeyDown(sender As Object, e As KeyEventArgs) Handles grd.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel

            ElseIf e.KeyCode = Keys.Enter Then
                Me.grd.Focus()
                If grd.RowCount > 0 Then
                    Me.SelectedAccountId = grd.GetRow.Cells(0).Value
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                Else
                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                End If
            ElseIf e.KeyCode = Keys.Up Then
                If grd.GetRow().RowIndex = 0 Then
                    txtSearch.Focus()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub grd_KeyUp(sender As Object, e As KeyEventArgs) Handles grd.KeyUp
    '    Try
    '        Me.txtSearch.Focus()
    '    Catch ex As Exception
    '        msg_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grd.RowDoubleClick
        Try
            If grd.RowCount > 0 Then
                Me.SelectedAccountId = grd.GetRow.Cells(0).Value
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Else
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
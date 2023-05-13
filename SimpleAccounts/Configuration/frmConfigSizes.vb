Imports SBDal

Public Class frmConfigSizes
    Public strSelectedSizeIDs As String = "0"
    Dim strNonSelectedIDs As String = "0"
    Dim _SearchNonSelectedIDs As New DataTable
    Dim _SearchSelectedIDs As New DataTable
    Dim strSQL As String
    Private Sub FillList()
        strSQL = "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 And ArticleSizeId not in (" & strSelectedSizeIDs & ")  order by ArticleSizeName, SortOrder "
        Me.UiListSizes.ListItem.DisplayMember = "Name"
        Me.UiListSizes.ListItem.ValueMember = "ID"
        Me.UiListSizes.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)
        Me.UiListSizes.DeSelect()
        _SearchNonSelectedIDs = CType(Me.UiListSizes.ListItem.DataSource, DataTable)
        _SearchNonSelectedIDs.AcceptChanges()
        strNonSelectedIDs = "0"
        For Each row As DataRow In _SearchNonSelectedIDs.Rows
            strNonSelectedIDs += "," & Val(row.Item("Id").ToString) & ""
        Next
        strSQL = "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 And ArticleSizeId not in (" & strNonSelectedIDs & ") order by ArticleSizeName, SortOrder "
        Me.lbUiSelectedSizes.ListItem.DisplayMember = "Name"
        Me.lbUiSelectedSizes.ListItem.ValueMember = "ID"
        Me.lbUiSelectedSizes.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)
        Me.lbUiSelectedSizes.DeSelect()
        'For Each Item As ListViewItem In lbUiSelectedSizes.ListItem.Items
        '    strSelectedSizeIDs += "" & Item.Text & ","
        'Next
        _SearchSelectedIDs = CType(Me.lbUiSelectedSizes.ListItem.DataSource, DataTable)
        _SearchSelectedIDs.AcceptChanges()
        strSelectedSizeIDs = "0"
        For Each row As DataRow In _SearchSelectedIDs.Rows
            strSelectedSizeIDs += "," & Val(row.Item("Id").ToString) & ""
        Next
    End Sub
    Sub New(ByVal IDs As String)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        strSelectedSizeIDs = IDs
        FillList()
        Reset()
    End Sub

    Private Sub btnMove_Click(sender As Object, e As EventArgs) Handles btnMove.Click
        Try
            If Me.UiListSizes.SelectedItems.Length <= 0 Then
                ShowErrorMessage("Please Select a Size")
            ElseIf Me.UiListSizes.SelectedItems.Length > 0 Then
                strSelectedSizeIDs += "," & Me.UiListSizes.SelectedIDs() & ""
                strNonSelectedIDs = strNonSelectedIDs.Replace(Me.UiListSizes.SelectedIDs(), 0)
                FillList()
                Reset()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If Me.lbUiSelectedSizes.SelectedItems.Length <= 0 Then
                ShowErrorMessage("Please Select a Size")
            ElseIf Me.lbUiSelectedSizes.SelectedItems.Length > 0 Then
                strNonSelectedIDs += "," & Me.lbUiSelectedSizes.SelectedIDs() & ""
                strSelectedSizeIDs = strSelectedSizeIDs.Replace(Me.lbUiSelectedSizes.SelectedIDs(), 0)
                'Me.lbUiSelectedSizes.ListItem.Items.Remove(Me.lbUiSelectedSizes.SelectedIDs())
                FillList()
                Reset()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSizes_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSizes.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                btnMove_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

   
    Private Sub txtSizes_KeyUp(sender As Object, e As EventArgs) Handles txtSizes.TextChanged
        Try
            Dim dv As New DataView
            _SearchNonSelectedIDs.TableName = "Default"
            _SearchNonSelectedIDs.CaseSensitive = False
            dv.Table = _SearchNonSelectedIDs
            dv.RowFilter = "Name Like '%" & Me.txtSizes.Text & "%'"
            Me.UiListSizes.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Reset()
        Me.txtSelectedSizes.Text = ""
        Me.txtSizes.Text = ""
        Me.UiListSizes.DeSelect()
        Me.lbUiSelectedSizes.DeSelect()
    End Sub

    Private Sub txtSelectedSizes_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSelectedSizes.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                btnBack_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtSelectedSizes_KeyUp(sender As Object, e As EventArgs) Handles txtSelectedSizes.TextChanged
        Try
            Dim dv As New DataView
            _SearchSelectedIDs.TableName = "Default"
            _SearchSelectedIDs.CaseSensitive = False
            dv.Table = _SearchSelectedIDs
            dv.RowFilter = "Name Like '%" & Me.txtSelectedSizes.Text & "%'"
            Me.lbUiSelectedSizes.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmConfigSizes_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub frmConfigSizes_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub frmConfigSizes_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.UiListSizes.DeSelect()
            Me.lbUiSelectedSizes.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
End Class
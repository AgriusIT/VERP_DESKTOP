Imports SBDal
Public Class frmConfigColor
    Public strSelectedColorIDs As String = "0"
    Dim strNonSelectedIDs As String = "0"
    Dim _SearchNonSelectedIDs As New DataTable
    Dim _SearchSelectedIDs As New DataTable
    Dim strSQL As String
    Sub New(ByVal IDs As String)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        strSelectedColorIDs = IDs
        FillList()
        Reset()
    End Sub
    Private Sub FillList()
        strSQL = "select ArticleColorId as Id,ArticleColorName as Name from ArticleColorDefTable where active=1 And ArticleColorId not in (" & strSelectedColorIDs & ") order by sortOrder"
        Me.UiListColor.ListItem.DisplayMember = "Name"
        Me.UiListColor.ListItem.ValueMember = "ID"
        Me.UiListColor.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)
        Me.UiListColor.DeSelect()
        _SearchNonSelectedIDs = CType(Me.UiListColor.ListItem.DataSource, DataTable)
        _SearchNonSelectedIDs.AcceptChanges()
        strNonSelectedIDs = "0"
        For Each row As DataRow In _SearchNonSelectedIDs.Rows
            strNonSelectedIDs += "," & Val(row.Item("Id").ToString) & ""
        Next
        strSQL = "select ArticleColorId as Id,ArticleColorName as Name from ArticleColorDefTable where active=1 And ArticleColorId not in (" & strNonSelectedIDs & ") order by sortOrder"
        Me.lbUiSelectedColors.ListItem.DisplayMember = "Name"
        Me.lbUiSelectedColors.ListItem.ValueMember = "ID"
        Me.lbUiSelectedColors.ListItem.DataSource = UtilityDAL.GetDataTable(strSQL)
        Me.lbUiSelectedColors.DeSelect()
        _SearchSelectedIDs = CType(Me.lbUiSelectedColors.ListItem.DataSource, DataTable)
        _SearchSelectedIDs.AcceptChanges()
        strSelectedColorIDs = "0"
        For Each row As DataRow In _SearchSelectedIDs.Rows
            strSelectedColorIDs += "," & Val(row.Item("Id").ToString) & ""
        Next
    End Sub
    Private Sub Reset()
        Me.txtSelectedColors.Text = ""
        Me.txtColors.Text = ""
        Me.UiListColor.DeSelect()
        Me.lbUiSelectedColors.DeSelect()
    End Sub

    Private Sub frmConfigColor_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub frmConfigColor_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Private Sub txtSelectedColors_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSelectedColors.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                btnBack_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSelectedColors_KeyUp(sender As Object, e As EventArgs) Handles txtSelectedColors.TextChanged
        Try
            Dim dv As New DataView
            _SearchSelectedIDs.TableName = "Default"
            _SearchSelectedIDs.CaseSensitive = False
            dv.Table = _SearchSelectedIDs
            dv.RowFilter = "Name Like '%" & Me.txtSelectedColors.Text & "%'"
            Me.lbUiSelectedColors.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtColors_KeyDown(sender As Object, e As KeyEventArgs) Handles txtColors.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                btnMove_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtColors_KeyPress(sender As Object, e As EventArgs) Handles txtColors.TextChanged
        Try
            Dim dv As New DataView
            _SearchNonSelectedIDs.TableName = "Default"
            _SearchNonSelectedIDs.CaseSensitive = False
            dv.Table = _SearchNonSelectedIDs
            dv.RowFilter = "Name Like '%" & Me.txtColors.Text & "%'"
            Me.UiListColor.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If Me.lbUiSelectedColors.SelectedItems.Length <= 0 Then
                ShowErrorMessage("Please Select a Size")
            ElseIf Me.lbUiSelectedColors.SelectedItems.Length > 0 Then
                strNonSelectedIDs += "," & Me.lbUiSelectedColors.SelectedIDs() & ""
                strSelectedColorIDs = strSelectedColorIDs.Replace(Me.lbUiSelectedColors.SelectedIDs(), 0)
                'Me.lbUiSelectedSizes.ListItem.Items.Remove(Me.lbUiSelectedSizes.SelectedIDs())
                FillList()
                Reset()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnMove_Click(sender As Object, e As EventArgs) Handles btnMove.Click
        Try
            If Me.UiListColor.SelectedItems.Length <= 0 Then
                ShowErrorMessage("Please Select a Size")
            ElseIf Me.UiListColor.SelectedItems.Length > 0 Then
                strSelectedColorIDs += "," & Me.UiListColor.SelectedIDs() & ""
                strNonSelectedIDs = strNonSelectedIDs.Replace(Me.UiListColor.SelectedIDs(), 0)
                FillList()
                Reset()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmConfigColor_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            Me.UiListColor.DeSelect()
            Me.lbUiSelectedColors.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
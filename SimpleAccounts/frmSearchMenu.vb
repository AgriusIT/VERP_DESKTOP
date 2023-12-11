Public Class frmSearchMenu
    Public _Menu As String = String.Empty
    Public Function Getforms(ByVal menu As String) As DataTable
        Dim dt As New DataTable
        Dim sql As String = String.Empty
        '        FormId	int	Unchecked
        'FormName	varchar(50)	Checked
        'FormCaption	varchar(50)	Checked
        'FormModule	varchar(50)	Checked
        'SortOrder	int	Checked
        'Active	bit	Checked
        'AccessKey	varchar(1000)	Checked
        '        Unchecked()
        Try
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Me.txtSearch.Text.Contains(">") Then

                Dim strSearch As String()
                strSearch = Me.txtSearch.Text.Split(">")

                sql = "Select FormId, FormName, FormCaption, FormModule , SortOrder, Active, AccessKey From tblForms Where (FormModule like '%" & strSearch(0).ToString.Replace(" ", "%") & "%')  and FormCaption like '%" & strSearch(1).ToString.Replace(" ", "%") & "%' order by  FormModule, FormCaption"

            Else
                sql = "Select FormId, FormName, FormCaption, FormModule , SortOrder, Active, AccessKey From tblForms Where (FormModule + ' ' + FormCaption)  like '%" & menu.ToString.Replace(" ", "%") & "%' order by  FormModule, FormCaption"

            End If

            dt = GetDataTable(sql)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            Application.DoEvents()
            Me.lblProgress.Visible = False
            Application.DoEvents()
        End Try
    End Function
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "")
        Try
            Me.grdMenuforms.AutomaticSort = False
            Dim gridgroupmodule As New Janus.Windows.GridEX.GridEXGroup(Me.grdMenuforms.RootTable.Columns("FormModule"))
            gridgroupmodule.GroupPrefix = String.Empty
            Me.grdMenuforms.RootTable.Groups.Add(gridgroupmodule)
            'Dim gridGroup As New Janus.Windows.GridEX.GridEXGroup(Me.grdMenuforms.RootTable.Columns("FormCaption"))
            'gridGroup.GroupPrefix = String.Empty
            'Me.grdMenuforms.RootTable.Groups.Add(gridGroup)


            Me.grdMenuforms.RootTable.Columns("FormName").Visible = False
            Me.grdMenuforms.RootTable.Columns("FormId").Visible = False
            Me.grdMenuforms.RootTable.Columns("FormCaption").Visible = True

            Me.grdMenuforms.RootTable.Columns("FormCaption").Caption = ""
            Me.grdMenuforms.RootTable.Columns("FormModule").Visible = False
            Me.grdMenuforms.RootTable.Columns("FormModule").Caption = "Module"
            Me.grdMenuforms.RootTable.Columns("FormCaption").Width = 350
            Me.grdMenuforms.RootTable.Columns("SortOrder").Visible = False
            Me.grdMenuforms.RootTable.Columns("Active").Visible = False
            Me.grdMenuforms.RootTable.Columns("AccessKey").Visible = False

            Me.grdMenuforms.ColumnAutoResize = True
            Me.txtSearch.Focus()
            Me.txtSearch.SelectAll()
            'Me.grdMenuforms.RootTable.Columns("FormModule").Width = 300


            'For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grdMenuforms.RootTable.Columns
            '    If col.Index <> Me.grdMenuforms.RootTable.Columns("FormCaption").Index Then
            '        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
            '    End If
            'col.FilterEditType = Janus.Windows.GridEX.FilterEditType.SameAsEditType
            'Next
            'gridgroupmodule.Collapse()
            
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmSearchMenu_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
            If e.KeyCode = Keys.Tab Then
                If Me.ActiveControl Is Me.txtSearch Then
                    Me.grdMenuforms.Focus()
                Else
                    Me.txtSearch.Focus()
                End If
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub



    Private Sub frmSearchMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = _Menu
            Me.txtSearch.Text = _Menu
            Me.grdMenuforms.DataSource = Getforms(_Menu)
            Me.grdMenuforms.RetrieveStructure()
            Me.ApplyGridSettings()
            Me.txtSearch.Focus()
            Me.txtSearch.SelectAll()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Openform(ByVal key As String)
        Try
            Me.lblProgress.Visible = True
            Application.DoEvents()

            If Not key = String.Empty Then
                frmMain.LoadControl(key)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Application.DoEvents()
            Me.lblProgress.Visible = False
        End Try
       
    End Sub

    Private Sub grdMenuforms_DoubleClick(sender As Object, e As EventArgs) Handles grdMenuforms.DoubleClick
        'Try

        '    Openform(Me.grdMenuforms.CurrentRow.Cells("AccessKey").Value.ToString)
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtSearch_Click(sender As Object, e As EventArgs) Handles txtSearch.Click
        'Try
        '    If e.KeyCode = Keys.Enter Then
        '        Dim searchManu As New frmSearchMenu
        '        searchManu._Menu = Me.txtSearch.Text
        '        searchManu.BringToFront()
        '        searchManu.ShowDialog()
        '    End If

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.Text = Me.txtSearch.Text
                Me.grdMenuforms.DataSource = Getforms(Me.txtSearch.Text)
                Me.grdMenuforms.RetrieveStructure()
                Me.ApplyGridSettings()
                Me.txtSearch.Focus()
                Me.txtSearch.SelectAll()
            End If

            If e.KeyCode = Keys.Down Then
                Me.grdMenuforms.Focus()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdMenuforms_KeyDown(sender As Object, e As KeyEventArgs) Handles grdMenuforms.KeyDown
        If e.KeyCode = Keys.Enter Then
            grdMenuforms_RowDoubleClick(Me, Nothing)
        End If

        If e.KeyCode = Keys.Tab Then
            If Me.ActiveControl Is Me.txtSearch Then
                Me.grdMenuforms.Focus()
            Else
                Me.txtSearch.Focus()
            End If
        End If

    End Sub

    Private Sub grdMenuforms_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdMenuforms.RowDoubleClick
        Try
            If grdMenuforms.CurrentRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Openform(Me.grdMenuforms.CurrentRow.Cells("FormName").Value.ToString)
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdMenuforms_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdMenuforms.FormattingRow

    End Sub
End Class
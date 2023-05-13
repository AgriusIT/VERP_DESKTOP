Imports System.Data.OleDb

Public Class frmImportDispatchStatus

    Private Sub RefreshControls()

        Try
            Me.txtStatus.Text = String.Empty
            Me.btnSave.Text = "&Save"

            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
        

    End Sub

    Private Sub GetSecurityRights()

        Try
            If (Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save") AndAlso Me.UltraTabControl1.SelectedTab.Index = 0 Then
                Me.btnDelete.Visible = False
                Me.btnSave.Visible = True
            Else
                Me.btnDelete.Visible = True
                If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                    Me.btnSave.Visible = False
                Else
                    Me.btnSave.Visible = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Filling the Grid with status
    Private Sub BindGrid()

        Try
            Dim dt As New DataTable
            dt = GetDataTable("Select StockDispatchStatusID,StockDispatchStatusName from tblStockDispatchStatus order by StockDispatchStatusID desc")
            dt.AcceptChanges()
            Me.grdHistory.DataSource = dt
            Me.grdHistory.RetrieveStructure()

            Me.grdHistory.RootTable.Columns("StockDispatchStatusID").Visible = False        'hide the "StockDispatchStatusID" column
            Me.grdHistory.RootTable.Columns("StockDispatchStatusName").Visible = True


            'Me.grdHistory.RootTable.Columns("StockDispatchStatusID").Caption = "ID"
            Me.grdHistory.RootTable.Columns("StockDispatchStatusName").Caption = "Status"   'Changing the caption of column "StockDispatchStatusName"

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function isValidate(Optional ByVal Condtion As String = "")

        Try
            If Me.txtStatus.Text = String.Empty Then
                ShowErrorMessage("Please enter the Status title")
                Me.txtStatus.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub SaveStatus(Optional ByVal Condition As String = "")
        Try

            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con        'passing the global connection object to our local connection object

            'checking if connection is already opened, then close the connection 
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If

            objCon.Open()           'open the connection
            objCommand.Connection = objCon

            Dim remarks As String = String.Empty
            Dim active As Boolean = True

            objCommand.CommandText = "Insert into tblStockDispatchStatus(StockDispatchStatusName) Values(N'" & Me.txtStatus.Text.Replace("'", "''").ToString & "')"
            objCommand.ExecuteNonQuery()

            objCon.Close()
            RefreshControls()
            BindGrid()

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Sub

    Private Sub UpdateStatus(Optional ByVal Condition As String = "")

        Try
            If msg_Confirm(str_ConfirmUpdate) = False Then
                Exit Try
            End If

            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con        'passing the global connection object to our local connection object

            'checking if connection is already opened, close the connection 
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If

            objCon.Open()           'open the connection
            objCommand.Connection = objCon

            objCommand.CommandText = "Update tblStockDispatchStatus set StockDispatchStatusName=N'" & Me.txtStatus.Text.Replace("'", "''").ToString & "' Where StockDispatchStatusID=" & CInt(Me.grdHistory.CurrentRow.Cells("StockDispatchStatusID").Value.ToString)
            objCommand.ExecuteScalar()

            objCon.Close()

            Me.btnSave.Text = "&Save"
            RefreshControls()
            BindGrid()


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub CloseDialogBox()
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmImportDispatchStatus_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try

            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If

            If e.KeyCode = Keys.Escape Then
                CloseDialogBox()
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub frmImportDispatchStatus_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            BindGrid()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub EditRecord()

        Try

            Me.txtStatus.Text = Me.grdHistory.CurrentRow.Cells("StockDispatchStatusName").Value.ToString
            Me.btnSave.Text = "&Update"

            GetSecurityRights()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub grdHistory_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdHistory.DoubleClick
        Try
            If Me.grdHistory.Row < 0 Then
                Exit Sub
            Else
                EditRecord()
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            If isValidate() = True Then
                If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                    SaveStatus()
                Else
                    UpdateStatus()
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If msg_Confirm(str_ConfirmDelete) = False Then
                Exit Try
            End If

            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con        'passing the global connection object to our local connection object

            'checking if connection is already opened, close the connection 
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If

            objCon.Open()           'open the connection
            objCommand.Connection = objCon

            objCommand.CommandText = "Delete from tblStockDispatchStatus Where StockDispatchStatusID=" & CInt(Me.grdHistory.CurrentRow.Cells("StockDispatchStatusID").Value.ToString)
            objCommand.ExecuteScalar()

            objCon.Close()

            RefreshControls()
            BindGrid()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            CloseDialogBox()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecord()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
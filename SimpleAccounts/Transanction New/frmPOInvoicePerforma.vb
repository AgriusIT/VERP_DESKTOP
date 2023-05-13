'18-Jun-2015 Task#118062015 Ahmad Sharif: Coding of full screen

Imports System.Data.OleDb

Public Class frmPOInvoicePerforma

    Private Sub RefreshControls()

        Try
            Me.txtIndent.Text = String.Empty
            Me.txtPerformaNo.Text = String.Empty
            Me.dtpIndentGenerate.Value = Date.Now.Date
            Me.cmbShippment.SelectedIndex = 0
            Me.cmbOrigin.Text = String.Empty
            Me.txtInstructions.Text = String.Empty

            FillCombo("Origin")

            GetSecurityRights()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub FillCombo(Optional ByVal Condition As String = "")

        Try
            'fill the cmbOrigin
            If Condition = "Origin" Then
                FillDropDown(Me.cmbOrigin, "Select Distinct ShippmentOrigin,ShippmentOrigin from tblPOPerformaInvoice ", False)
                Me.cmbOrigin.Text = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BindGrid()
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select PerformaInvoiceId,PerformaCode,PODocumentId,PODate,ShippmentVia,ShippmentInstructions,ShippmentOrigin from tblPOPerformaInvoice")
            dt.AcceptChanges()
            Me.grdSaved.DataSource = dt         'set the data to grdSaved
            Me.grdSaved.RetrieveStructure()

            'hiding and showing the columns of grdSaved
            Me.grdSaved.RootTable.Columns("PerformaInvoiceId").Visible = False
            Me.grdSaved.RootTable.Columns("PerformaCode").Visible = True
            Me.grdSaved.RootTable.Columns("PODocumentId").Visible = True
            Me.grdSaved.RootTable.Columns("PODate").FormatString = "dd/MMM/yyyy"
            Me.grdSaved.RootTable.Columns("ShippmentVia").Visible = True
            Me.grdSaved.RootTable.Columns("ShippmentInstructions").Visible = True
            Me.grdSaved.RootTable.Columns("ShippmentOrigin").Visible = True

            'changing the header captions text of grdSaved
            Me.grdSaved.RootTable.Columns("PerformaCode").Caption = "Code"
            Me.grdSaved.RootTable.Columns("PODocumentId").Caption = "Purchase Id"
            Me.grdSaved.RootTable.Columns("PODate").Caption = "Date"
            Me.grdSaved.RootTable.Columns("ShippmentVia").Caption = "Shippment Way"
            Me.grdSaved.RootTable.Columns("ShippmentInstructions").Caption = "Instructions"
            Me.grdSaved.RootTable.Columns("ShippmentOrigin").Caption = "Origin"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function GetDocumentNo() As String
        Try
            Return GetNextDocNo("PI", 6, "tblPOPerformaInvoice", "PerormaCode")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

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

                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    SaveRecord()
                    BindGrid()
                Else
                    UpdateRecord()
                End If

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GetSecurityRights()

        Try
            'if btnSave Text Is Save and first tab is selected then invisible some buttons on first tab
            If (Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save") AndAlso Me.UltraTabControl1.SelectedTab.Index = 0 Then
                Me.btnDelete.Visible = False    'invisible btnDelete on first tab
                Me.btnPrint.Visible = False     'invisible btnPrint on first tab
                Me.btnSave.Visible = True       'visible btnSave on first tab
            Else
                Me.btnDelete.Visible = True
                Me.btnPrint.Visible = True
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

    Private Sub SaveRecord()

        Try

            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con        'passing the global connection object to our local connection object

            'checking if connection is already opened, close the connection 
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If

            objCon.Open()           'open the connection
            objCommand.Connection = objCon

            'insert new record query
            objCommand.CommandText = "Insert into tblPOPerformaInvoice(PerformaCode,PODocumentId,PODate,ShippmentVia,ShippmentInstructions,ShippmentOrigin) Values('" & Me.txtPerformaNo.Text.Replace("'", "''").ToString & "',N'" & Me.txtIndent.Text.Replace("'", "''").ToString & "',N'" & Me.dtpIndentGenerate.Value & "',N'" & Me.cmbShippment.Text.Replace("'", "''").ToString & "',N'" & Me.txtInstructions.Text.Replace("'", "''").ToString & "',N'" & Me.cmbOrigin.Text.Replace("'", "''").ToString & "')"
            objCommand.ExecuteNonQuery()

            objCon.Close()      'closing the opened connection
            RefreshControls()   'calling the RefreshControls() Sub for resetting the form controls 

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdateRecord()

        Try
            'Confirm from user: do you want to update record or not?
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

            'update record query
            objCommand.CommandText = "Update tblPOPerformaInvoice SET " _
            & " PODate=N'" & Me.dtpIndentGenerate.Value & "'," _
            & " ShippmentVia=N'" & Me.cmbShippment.Text.Replace("'", "''").ToString & "'," _
            & " ShippmentInstructions=N'" & Me.txtInstructions.Text.Replace("'", "''").ToString & "'," _
            & " ShippmentOrigin=N'" & Me.cmbOrigin.Text.Replace("'", "''").ToString & "' WHERE PerformaCode='" & Me.grdSaved.CurrentRow.Cells("PerformaCode").Value.ToString & "'"

            objCommand.ExecuteScalar()

            objCon.Close()  'closing the opened connection

            Me.btnSave.Text = "&Save"   'settting the btnSave Text property to "Save"

            BindGrid()
            RefreshControls()

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function PONoExists() As Boolean

        Try
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            objCon = Con        'passing the global connection object to our local connection object

            'checking if connection is already opened, close the connection 
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If

            objCon.Open()           'open the connection
            objCommand.Connection = objCon
            objCommand.CommandText = "select * from PurchaseOrderMasterTable Where PurchaseOrderNo='" & Me.txtIndent.Text.ToString & "'"

            Dim result As Integer = objCommand.ExecuteNonQuery()

            If result = 0 Then
                Return False
            End If

            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function isValidate() As Boolean

        Try
            'If PONoExists() = False Then
            '    ShowErrorMessage("This PO is not exist")
            '    'BringToFront()
            '    Return False
            'End If

            '    If Me.txtIndent.Text = String.Empty Then
            '        ShowErrorMessage("Document Id should be available")
            '        Return False
            '    End If

            '    If Me.cmbShippment.Text = String.Empty Then
            '        ShowErrorMessage("Please select or enter the shippment")
            '        Me.cmbShippment.Focus()
            '        Return False
            '    End If

            If Me.cmbOrigin.Text = String.Empty Then
                ShowErrorMessage("Please select or enter the origin")
                Me.cmbOrigin.Focus()    'setting the focus of cursor on cmbOrigin combo box
                Return False
            End If

            Return True         'if all the form controls full the required fields fill, then return true

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'This event listen the keys on form, if F4 pressed then save button event raised, if Esc key pressed then close dialog box sub call
    Private Sub frmPOInvoicePerforma_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)     'calling btnSave event on F4 key pressed from keyboard
            End If
            If e.KeyCode = Keys.Escape Then
                CloseDialogBox()                    'calling CloseDialogBox() sub on Esc key pressed from keyboard
            End If
            
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPOInvoicePerforma_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmPOInvoicePerforma_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.cmbShippment.SelectedIndex = 0       'setting the 0 index of cmbShippment 
            BindGrid()                              'filling the grid
            FillCombo("Origin")                     'filling the cmbOrigin from database
            Me.txtIndent.Text = Me.Tag.ToString     'gettting PoNo from Purchase Order form
            Me.txtPerformaNo.Text = GetNextDocNo("PI", 6, "tblPOPerformaInvoice", "PerformaCode")       'Getting Perform Invoice number

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub EditRecord()
        Try
            'setting the values on form controls from grid columns for edit purpose
            Me.txtPerformaNo.Text = grdSaved.CurrentRow.Cells("PerformaCode").Value.ToString
            Me.txtIndent.Text = grdSaved.CurrentRow.Cells("PODocumentId").Value.ToString
            Me.dtpIndentGenerate.Value = grdSaved.CurrentRow.Cells("PODate").Value.ToString
            Me.cmbShippment.Text = grdSaved.CurrentRow.Cells("ShippmentVia").Value.ToString
            Me.txtInstructions.Text = grdSaved.CurrentRow.Cells("ShippmentInstructions").Value.ToString
            Me.cmbOrigin.Text = grdSaved.CurrentRow.Cells("ShippmentOrigin").Value.ToString

            Me.btnSave.Text = "&Update"     'changing the text of button save to update

            GetSecurityRights()     'applying settings on toolstrip buttons

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdPOInvoicePerforma_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            'checking if grid has no rows then exit this event immediately
            If Me.grdSaved.Row < 0 Then
                Exit Sub
            Else
                EditRecord()
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab       'set the first tab ,form tab
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Sub for close the dialog box
    Private Sub CloseDialogBox()
        Try
            Me.Close()          'closing dialog box
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Toolstrip button cancel event
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            CloseDialogBox()        'call Sub CloseDialogBox() for closing the dialog box
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            'confirm box for user : do you want to delete, if Yes then delete otherwise No 
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

            objCommand.CommandText = "Delete from tblPOPerformaInvoice Where PerformaCode='" & Me.grdSaved.CurrentRow.Cells("PerformaCode").Value.ToString & "'"
            objCommand.ExecuteScalar()

            objCon.Close()      'closing the open connection

            RefreshControls()   'calling the RefreshControls() Sub for resetting form
            BindGrid()          'After deletion calling the BindGrid() Sub for grid updated quickly

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecord()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab       'set the first tab, form tab for edit record
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            'If Me.MainTab.SelectedIndex = 0 Then
            '    Me.CtrlGrdBar2.Visible = False
            'Else
            '    Me.CtrlGrdBar2.Visible = True
            'End If
            GetSecurityRights()     'applying the toolstrip button settings
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
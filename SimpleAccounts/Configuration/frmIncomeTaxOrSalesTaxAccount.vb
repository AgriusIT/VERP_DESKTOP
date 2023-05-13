Imports System.Data.OleDb
Public Class frmIncomeTaxOrSalesTaxAccount
    ''' <summary>
    ''' this form is used to configure income tax and sales tax (Added by zainab #Task1608 )
    ''' </summary>
    ''' <remarks>User can easily set tax,sales tax and with holding tax</remarks>
    Dim CurrentId As Integer
    Dim IsLoadedForm As Boolean = False
    Dim AccountId As Integer = 0I
    Public formType As String
    ''' <summary>
    ''' to refresh a form 
    ''' </summary>
    ''' <remarks></remarks>
    Sub RefreshForm()
        Me.BtnSave.Text = "&Save"
        Me.uitxtName.Text = ""
        Me.txtTax.Text = ""
        Me.uitxtName.Focus()
        Me.uitxtComments.Text = ""
        Me.uitxtSortOrder.Text = 1
        Me.uichkActive.Checked = True
        Me.AccountId = 0I
        Me.txtIncomeDescription.Text = String.Empty
        Me.uitxtName.Focus()
        Me.IncomeBindGrid()
        GetSecurityRights()
    End Sub
    'Refresh a sales tax form
    Sub RefreshForm2()
        Me.btnSave2.Text = "&Save"
        Me.uitxtIncomeName.Text = ""
        Me.txtSalesTax.Text = ""
        Me.txtWHTax.Text = ""
        Me.uitxtIncomeName.Focus()
        Me.uitxtSalesComments.Text = ""
        Me.uitxtSalesSortOrder.Text = 1
        Me.uichkSalesActive.Checked = True
        Me.AccountId = 0I
        Me.txtSalesDescription.Text = String.Empty
        Me.uitxtIncomeName.Focus()
        Me.SalesBindGrid()
        GetSecurityRights2()
    End Sub

    Sub IncomeBindGrid()

        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable
        'Ali Faisal : TFS4712 : Accounts filter with "Cash, Bank, Customer, Vendor" and Comments error on Tax Configuration Opening required on Payment and Receipt.
        adp = New OleDbDataAdapter("SELECT IncomeTaxAccId, IncomeAccountName,IncomeConfigAccount,IncomeTaxPer , tblDefIncomeTaxAccount.Comments, tblDefIncomeTaxAccount.Active, SortOrder, Isnull(tblDefIncomeTaxAccount.AccountId,0) as AccountId, v.detail_title as Account," & IIf(formType <> "Payment", "'Receipt'", "'Payment'") & " AS FromName FROM tblDefIncomeTaxAccount LEFT OUTER JOIN vwCOADetail V on V.coa_detail_id = tblDefIncomeTaxAccount.AccountId where FormName=" & IIf(formType <> "Payment", "'Reciept'", "'Payment'") & " order by sortorder", Con)
        adp.Fill(dt)
        Me.DataGridView1.DataSource = dt
        Me.DataGridView1.RetrieveStructure()
        Me.DataGridView1.RootTable.Columns(0).Visible = False
        Me.DataGridView1.RootTable.Columns("AccountId").Visible = False 'hide column of account id 
        Me.DataGridView1.RootTable.Columns("Account").Visible = False
        '  Me.DataGridView1.RootTable.Columns("").Visible = False

    End Sub

    Sub SalesBindGrid()
        Dim adp1 As New OleDbDataAdapter
        Dim dt1 As New DataTable
        'Ali Faisal : TFS4712 : Accounts filter with "Cash, Bank, Customer, Vendor" and Comments error on Tax Configuration Opening required on Payment and Receipt.
        adp1 = New OleDbDataAdapter("SELECT SalesTaxAccId, SalesAccountName,SalesConfigAccount,SalesTaxPer , WHTaxPer,tblDefSalesTaxAccount.Comments, tblDefSalesTaxAccount.Active, SortOrder, Isnull(tblDefSalesTaxAccount.AccountId,0) as AccountId, v.detail_title as Account," & IIf(formType <> "Payment", "'Receipt'", "'Payment'") & " AS FromName FROM tblDefSalesTaxAccount LEFT OUTER JOIN vwCOADetail V on V.coa_detail_id = tblDefSalesTaxAccount.AccountId where  FormName=" & IIf(formType <> "Payment", "'Reciept'", "'Payment'") & "  order by sortorder", Con)
        adp1.Fill(dt1)
        Me.GridEX1.DataSource = dt1
        Me.GridEX1.RetrieveStructure()
        Me.GridEX1.RootTable.Columns(0).Visible = False
        Me.GridEX1.RootTable.Columns("AccountId").Visible = False 'hide column of account id 
        Me.GridEX1.RootTable.Columns("Account").Visible = False
        '  Me.DataGridView1.RootTable.Columns("").Visible = False
    End Sub
    Sub EditRecord()
        Me.uitxtName.Text = DataGridView1.CurrentRow.Cells("IncomeAccountName").Value
        Me.txtIncomeDescription.Text = DataGridView1.CurrentRow.Cells("IncomeConfigAccount").Value
        Me.txtTax.Text = DataGridView1.CurrentRow.Cells("IncomeTaxPer").Value
        Me.uitxtComments.Text = DataGridView1.CurrentRow.Cells("Comments").Value
        Me.uitxtSortOrder.Text = DataGridView1.CurrentRow.Cells("SortOrder").Value
        Me.uichkActive.Checked = IIf(DataGridView1.CurrentRow.Cells("Active").Value = 0, False, True)
        Me.CurrentId = Me.DataGridView1.CurrentRow.Cells(0).Value
        AccountId = Val(DataGridView1.CurrentRow.Cells("AccountId").Value.ToString) 'Set Value of AccountId
        If IsDBNull(Me.DataGridView1.GetRow.Cells("Account").Value) Then
            Me.txtIncomeDescription.Text = String.Empty
        Else
            Me.txtIncomeDescription.Text = Me.DataGridView1.GetRow.Cells("Account").Value.ToString
        End If

        Me.BtnSave.Text = "&Update"
        GetSecurityRights()
    End Sub
    Sub EditRecord2()
        Me.uitxtIncomeName.Text = GridEX1.CurrentRow.Cells("SalesAccountName").Value
        Me.txtSalesDescription.Text = GridEX1.CurrentRow.Cells("SalesConfigAccount").Value
        Me.txtSalesTax.Text = GridEX1.CurrentRow.Cells("SalesTaxPer").Value
        Me.txtWHTax.Text = GridEX1.CurrentRow.Cells("WHTaxPer").Value
        Me.uitxtSalesComments.Text = GridEX1.CurrentRow.Cells("Comments").Value
        Me.uitxtSalesSortOrder.Text = GridEX1.CurrentRow.Cells("SortOrder").Value
        Me.uichkSalesActive.Checked = IIf(GridEX1.CurrentRow.Cells("Active").Value = 0, False, True)
        'Me.formType = GridEX1.CurrentRow.Cells("FormName").Value
        Me.CurrentId = Me.GridEX1.CurrentRow.Cells(0).Value
        AccountId = Val(GridEX1.CurrentRow.Cells("AccountId").Value.ToString) 'Set Value of AccountId
        If IsDBNull(Me.GridEX1.GetRow.Cells("Account").Value) Then
            Me.txtSalesDescription.Text = String.Empty
        Else
            Me.txtSalesDescription.Text = Me.GridEX1.GetRow.Cells("Account").Value.ToString
        End If

        Me.btnSave2.Text = "&Update"
        GetSecurityRights2()
    End Sub


    Private Sub frmIncomeTaxOrSalesTaxAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            If formType = "Reciept" Then

                Me.lblHeader.Text = "Income Tax Account (Receipt)"
                Me.lbltitle2.Text = "Sales Tax Account (Receipt)"


            ElseIf formType = "Payment" Then
                Me.lblHeader.Text = "Income Tax Account (Payment)"
                Me.lbltitle2.Text = "Sales Tax Account (Payment)"
            End If

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.RefreshForm()
            Me.RefreshForm2()
            IsLoadedForm = True
            Get_All(frmModProperty.Tags)
            Get_All2(frmModProperty.Tags)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click

        Try
            Me.EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.RefreshForm()
    End Sub
   
  
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.txtIncomeDescription.Text = "" Then
            ShowErrorMessage("Please select an Account")
            Me.txtIncomeDescription.Focus()
            Exit Sub
        End If


        If Me.uitxtName.Text = "" Then
            ShowErrorMessage("Please enter valid Name")
            Me.uitxtName.Focus()
            Exit Sub
        End If

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        'If Not msg_Confirm(str_ConfirmSave) Then
        Me.uitxtName.Focus()
        'Exit Sub
        ' End If

        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con

        Try
            If Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "&Save" Then

                cm.CommandText = "insert into tblDefIncomeTaxAccount(IncomeAccountName, IncomeConfigAccount,IncomeTaxPer,Comments,sortorder, Active, AccountId,FormName) values(N'" & Me.uitxtName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtIncomeDescription.Text.ToString.Replace("'", "''") & "',N'" & Me.txtTax.Text.ToString.Replace("'", "''") & "',N'" & Me.uitxtComments.Text.ToString.Replace("'", "''") & "',N'" & Me.uitxtSortOrder.Text & "'," & IIf(Me.uichkActive.Checked = False, 0, 1) & ", " & AccountId & ",N'" & Me.formType.ToString.Replace("'", "''") & "') Select @@Identity"
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                cm.CommandText = "update tblDefIncomeTaxAccount set IncomeAccountName=N'" & Me.uitxtName.Text.ToString.Replace("'", "''") & "',IncomeConfigAccount=N'" & Me.txtIncomeDescription.Text.ToString.Replace("'", "''") & "',IncomeTaxPer=N'" & Me.txtTax.Text.ToString.Replace("'", "''") & "',Comments=N'" & Me.uitxtComments.Text.ToString.Replace("'", "''") & "', sortorder=N'" & Me.uitxtSortOrder.Text.ToString.Replace("'", "''") & "',Active=" & IIf(Me.uichkActive.Checked = False, 0, 1) & ", AccountId=" & AccountId & " ,FormName=N'" & Me.formType & "' where IncomeTaxAccId=" & Me.CurrentId
            End If
            Dim identity As Integer = Convert.ToInt32(cm.ExecuteScalar())

            Try
                SaveActivityLog("Config", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", identity, Me.CurrentId), True)
            Catch ex As Exception

            End Try

            Me.CurrentId = 0

        Catch ex As Exception
            ShowErrorMessage("Error occured while saving record: " & ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try


        Me.RefreshForm()
    End Sub
    Private Sub btnSave2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave2.Click
        If Me.uitxtIncomeName.Text = "" Then
            ShowErrorMessage("Please enter valid Name")
            Me.uitxtIncomeName.Focus()
            Exit Sub
        End If
        If Me.txtSalesDescription.Text = "" Then
            ShowErrorMessage("Please select an Account")
            Me.txtSalesDescription.Focus()
            Exit Sub
        End If

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        'If Not msg_Confirm(str_ConfirmSave) Then
        Me.uitxtIncomeName.Focus()
        'Exit Sub
        ' End If

        Dim cm As New OleDbCommand

        If Con.State = ConnectionState.Closed Then Con.Open()
        cm.Connection = Con

        Try
            If Me.btnSave2.Text = "&Save" Or Me.btnSave2.Text = "&Save" Then

                cm.CommandText =
                    "insert into tblDefSalesTaxAccount(SalesAccountName,SalesConfigAccount, SalesTaxPer,WHTaxPer,Comments,sortorder, Active, AccountId ,FormName) values(N'" & Me.uitxtIncomeName.Text.ToString.Replace("'", "''") & "',N'" & Me.txtSalesDescription.Text.ToString.Replace("'", "''") & "',N'" & Me.txtSalesTax.Text.ToString.Replace("'", "''") & "',N'" & Me.txtWHTax.Text.ToString.Replace("'", "''") & "',N'" & Me.uitxtSalesComments.Text.ToString.Replace("'", "''") & "',N'" & Me.uitxtSalesSortOrder.Text & "'," & IIf(Me.uichkSalesActive.Checked = False, 0, 1) & ", " & AccountId & ",N'" & Me.formType.ToString.Replace("'", "''") & "') Select @@Identity"
            Else
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                cm.CommandText = "update tblDefSalesTaxAccount set SalesAccountName=N'" & Me.uitxtIncomeName.Text.ToString.Replace("'", "''") & "',SalesConfigAccount=N'" & Me.txtSalesDescription.Text.ToString.Replace("'", "''") & "',SalesTaxPer=N'" & Me.txtSalesTax.Text.ToString.Replace("'", "''") & "',WHTaxPer=N'" & Me.txtWHTax.Text.ToString.Replace("'", "''") & "',Comments=N'" & Me.uitxtSalesComments.Text.ToString.Replace("'", "''") & "', sortorder=N'" & Me.uitxtSalesSortOrder.Text.ToString.Replace("'", "''") & "',Active=" & IIf(Me.uichkSalesActive.Checked = False, 0, 1) & ", AccountId=" & AccountId & ",FormName=N'" & Me.formType & "' where SalesTaxAccId=" & Me.CurrentId
            End If
            Dim identity As Integer = Convert.ToInt32(cm.ExecuteScalar())

            Try
                SaveActivityLog("Config", Me.Text, IIf(Me.btnSave2.Text = "Save" Or Me.btnSave2.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.btnSave2.Text = "Save" Or Me.btnSave2.Text = "&Save", identity, Me.CurrentId), True)
            Catch ex As Exception

            End Try

            Me.CurrentId = 0

        Catch ex As Exception
            ShowErrorMessage("Error occured while saving record: " & ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try
        Me.RefreshForm2()
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not DataGridView1.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If IsValidToDelete("SalesMasterTable", "IncomeTaxAccId", Me.DataGridView1.CurrentRow.Cells("IncomeTaxAccId").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                Try

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    Dim cm As New OleDbCommand

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from tblDefIncomeTaxAccount where IncomeTaxAccId=" & Me.DataGridView1.CurrentRow.Cells("IncomeTaxAccId").Value.ToString
                    cm.ExecuteNonQuery()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0
                Catch ex As Exception
                    msg_Error("Error occured while deleting record: " & ex.Message)
                Finally
                    Con.Close()
                    Me.lblProgress.Visible = False
                End Try

                Try
                    ''insert Activity Log
                    SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.DataGridView1.CurrentRow.Cells("IncomeTaxAccId").Value.ToString, True)
                Catch ex As Exception

                End Try


                Me.RefreshForm()


            End If
        Else
            msg_Error(str_ErrorDependentRecordFound)
        End If
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmIncomeTaxOrSalesTaxAccount)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.BtnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub GetSecurityRights2()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave2.Enabled = True
                Me.BtnDelete2.Enabled = True
                Me.btnPrint2.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmIncomeTaxOrSalesTaxAccount)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave2.Text = "Save" Or Me.btnSave2.Text = "&Save" Then
                            Me.btnSave2.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave2.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete2.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint2.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave2.Enabled = False
                Me.btnDelete2.Enabled = False
                Me.btnPrint2.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave2.Text = "&Save" Then btnSave2.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave2.Text = "&Update" Then btnSave2.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete2.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint2.Enabled = True
                      
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If Not Me.DataGridView1.GetRow Is Nothing Then
            Me.EditRecord()
        End If
    End Sub
  
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From tblDefIncomeTaxAccount WHERE IncomeTaxAccId=N'" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.uitxtName.Text = dt.Rows(0).Item("IncomeAccountName").ToString
                        Me.txtIncomeDescription.Text = dt.Rows(0).Item("IncomeConfigAccount").ToString
                        Me.txtTax.Text = dt.Rows(0).Item("IncomeTaxPer").ToString
                        Me.uitxtComments.Text = dt.Rows(0).Item("Comments").ToString
                        Me.uitxtSortOrder.Text = dt.Rows(0).Item("SortOrder").ToString
                        Me.uichkActive.Checked = dt.Rows(0).Item("Active")
                        Me.formType = dt.Rows(0).Item("FormName").ToString
                        Me.CurrentId = dt.Rows(0).Item("AccountId").ToString
                        AccountId = Val(dt.Rows(0).Item("AccountId").ToString)
                        Me.BtnSave.Text = "&Update"
                        Me.GetSecurityRights()
                        IsDrillDown = True
                    End If
                End If
                IsDrillDown = False
            End If
            Return Get_All
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Get_All2(ByVal Id As String)
        Try
            Get_All2 = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From tblDefSalesTaxAccount WHERE SalesTaxAccId=N'" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.uitxtIncomeName.Text = dt.Rows(0).Item("SalesAccountName").ToString
                        Me.txtSalesDescription.Text = dt.Rows(0).Item("SalesConfigAccount").ToString
                        Me.txtSalesTax.Text = dt.Rows(0).Item("SalesTaxPer").ToString
                        Me.txtWHTax.Text = dt.Rows(0).Item("WHTaxPer").ToString
                        Me.uitxtSalesComments.Text = dt.Rows(0).Item("Comments").ToString
                        Me.uitxtSalesSortOrder.Text = dt.Rows(0).Item("SortOrder").ToString
                        Me.uichkSalesActive.Checked = dt.Rows(0).Item("Active")
                        Me.CurrentId = dt.Rows(0).Item("AccountId").ToString
                        AccountId = Val(dt.Rows(0).Item("AccountId").ToString)
                        Me.btnSave2.Text = "&Update"
                        Me.GetSecurityRights2()
                        IsDrillDown = True
                    End If
                End If
                IsDrillDown = False
            End If
            Return Get_All2
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            ApplyStyleSheet(frmAddAccount)
            frmAddAccount.IsTaxForm = True
            If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If frmAddAccount.AccountId > 0 Then
                    AccountId = frmAddAccount.AccountId
                    Me.txtIncomeDescription.Text = frmAddAccount.AccountDesc
                    frmAddAccount.AccountId = 0
                End If
                frmAddAccount.IsTaxForm = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.F2 Then
            btnOpen_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub btnEdit2_Click(sender As Object, e As EventArgs) Handles btnEdit2.Click

        Try
            Me.EditRecord2()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew2_Click(sender As Object, e As EventArgs)
        Me.RefreshForm2()
    End Sub

    Private Sub btnAdd2_Click(sender As Object, e As EventArgs) Handles btnAdd2.Click
        Try
            ApplyStyleSheet(frmAddAccount)
            frmAddAccount.IsTaxForm = True
            If frmAddAccount.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If frmAddAccount.AccountId > 0 Then
                    AccountId = frmAddAccount.AccountId
                    Me.txtSalesDescription.Text = frmAddAccount.AccountDesc
                    frmAddAccount.AccountId = 0
                End If
                frmAddAccount.IsTaxForm = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete2_Click(sender As Object, e As EventArgs) Handles btnDelete2.Click
        If Not GridEX1.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If IsValidToDelete("SalesMasterTable", "SalesTaxAccId", Me.GridEX1.CurrentRow.Cells("SalesTaxAccId").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                Try

                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    Dim cm As New OleDbCommand

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from tblDefSalesTaxAccount where SalesTaxAccId=" & Me.GridEX1.CurrentRow.Cells("SalesTaxAccId").Value.ToString
                    cm.ExecuteNonQuery()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0
                Catch ex As Exception
                    msg_Error("Error occured while deleting record: " & ex.Message)
                Finally
                    Con.Close()
                    Me.lblProgress.Visible = False
                End Try

                Try
                    ''insert Activity Log
                    SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.GridEX1.CurrentRow.Cells("SalesTaxAccId").Value.ToString, True)
                Catch ex As Exception

                End Try


                Me.RefreshForm2()


            End If
        Else
            msg_Error(str_ErrorDependentRecordFound)
        End If
    End Sub

    Private Sub GridEX1_DoubleClick(sender As Object, e As EventArgs) Handles GridEX1.DoubleClick
        If Not Me.GridEX1.GetRow Is Nothing Then
            Me.EditRecord2()
        End If
    End Sub

    Private Sub GridEX1_KeyDown(sender As Object, e As KeyEventArgs) Handles GridEX1.KeyDown
        If e.KeyCode = Keys.F2 Then
            btnEdit2_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete2_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub btnNew2_Click_1(sender As Object, e As EventArgs) Handles btnNew2.Click
        Me.RefreshForm2()
    End Sub

End Class
'28-Feb-2018 TFS2236 : Ayesha Rehman: Add new form for Accounts of Employees, friends and family to Appear on Sales
Imports SBModel
Imports SBDal
Public Class frmMiscAccountsonSales
    Dim Id As Integer = 0I
    Dim MiscAccountsonSales As tblMiscAccountsonSalesBE

    Public objDAL As tblMiscAccountsDAL = New tblMiscAccountsDAL()
    Enum EnumGrd
        MiscAccountId
        AccountId
        Code
        Title
        Active
        SortOrder
    End Enum
    ''' <summary>
    ''' Ayesha Rehman : Apply grid setings to hide some columns and also apply filters on specific columns.
    ''' </summary>
    ''' <remarks>28-Feb-2018 TFS2236 : Ayesha Rehman</remarks>
    Public Sub ApplyGridSettings()
        Try
            Me.grd.RootTable.Columns(EnumGrd.MiscAccountId).Visible = False
            Me.grd.RootTable.Columns(EnumGrd.AccountId).Visible = False
            Me.grd.RootTable.Columns(EnumGrd.Active).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grd.RootTable.Columns(EnumGrd.Active).TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grd.RootTable.Columns(EnumGrd.SortOrder).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Delete").Width = 50
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehman: FillCombos for SubSubAccount
    ''' </summary>
    ''' <remarks>28-Feb-2018 TFS2236 : Ayesha Rehman</remarks>
    Public Sub FillCombos()
        Try
            Dim str As String = String.Empty
            str = "Select main_sub_sub_id , sub_sub_code As Code , sub_sub_title As Title from tblCOAMainSubSub "
      
            FillUltraDropDown(cmbSubLevelAccount, str)
            Me.cmbSubLevelAccount.Rows(0).Activate()
            Me.cmbSubLevelAccount.DisplayLayout.Bands(0).Columns("main_sub_sub_id").Hidden = True
            Me.cmbSubLevelAccount.DisplayLayout.Bands(0).Columns("Code").Width = 250
            Me.cmbSubLevelAccount.DisplayLayout.Bands(0).Columns("Title").Width = 250

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ayesha Rehma : To show all saved records in the grid 
    ''' </summary>
    ''' <remarks>28-Feb-2018 TFS2236 : Ayesha Rehman</remarks>
    Private Sub GetSubAccount()
        Try
            Dim strsql As String = "SELECT tblMiscAccountsonSales.MiscAccountId, tblCOAMainSubSub.main_sub_sub_id, tblCOAMainSubSub.sub_sub_code AS Code, tblCOAMainSubSub.sub_sub_title AS Title, " _
                                   & " tblMiscAccountsonSales.Active, tblMiscAccountsonSales.SortOrder " _
                                   & " FROM tblMiscAccountsonSales LEFT OUTER JOIN " _
                                   & " tblCOAMainSubSub ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSub.main_sub_sub_id where MiscAccountId > 0"
            Dim dt As DataTable = GetDataTable(strsql)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns(0).Visible = False
            ApplyGridSettings()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmMiscAccountsonSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillCombos()
        GetSubAccount()
    End Sub

    Private Sub btnAddDock_Click(sender As Object, e As EventArgs) Handles btnAddDock.Click
        Try

            If cmbSubLevelAccount.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please Select An Account")
                Me.cmbSubLevelAccount.Focus()
                Exit Sub
            End If
            Dim i As Integer = 0
            For i = 0 To grd.GetRows.Length - 1
                If cmbSubLevelAccount.ActiveRow.Cells(0).Value = grd.GetRows(i).Cells(1).Value Then
                    ShowErrorMessage("This Sub Account Already Exist")
                    Me.cmbSubLevelAccount.Focus()
                    Exit Sub
                End If
            Next
            Dim dtGrid As DataTable = CType(Me.grd.DataSource, DataTable)

            Dim dr As DataRow = dtGrid.NewRow
            dr(EnumGrd.MiscAccountId) = Id
            dr(EnumGrd.AccountId) = cmbSubLevelAccount.ActiveRow.Cells(0).Value
            dr(EnumGrd.Code) = cmbSubLevelAccount.ActiveRow.Cells(1).Value
            dr(EnumGrd.Title) = cmbSubLevelAccount.ActiveRow.Cells(2).Value.ToString
            dr(EnumGrd.Active) = IIf(Me.chkActive.Checked = True, 1, 0)
            dr(EnumGrd.SortOrder) = Me.txtSortOrder.Text

            dtGrid.Rows.Add(dr)
            dtGrid.AcceptChanges()
            FillModel()
            Save()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillModel()
        MiscAccountsonSales = New tblMiscAccountsonSalesBE
        MiscAccountsonSales.MiscAccountId = Id
        MiscAccountsonSales.AccountId = Me.cmbSubLevelAccount.ActiveRow.Cells(0).Value
        MiscAccountsonSales.Active = IIf(Me.chkActive.Checked = True, 1, 0)
        MiscAccountsonSales.SortOrder = Me.txtSortOrder.Text
        MiscAccountsonSales.ActivityLog = New ActivityLog
    End Sub
    Public Function Save(Optional Condition As String = "") As Boolean
        Try
            If New tblMiscAccountsDAL().Add(MiscAccountsonSales) Then

                SaveActivityLog("Accounts", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, Me.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw (ex)
        End Try
    End Function
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            MiscAccountsonSales = New tblMiscAccountsonSalesBE
            MiscAccountsonSales.MiscAccountId = Val(Me.grd.CurrentRow.Cells(EnumGrd.MiscAccountId).Value.ToString)
            MiscAccountsonSales.ActivityLog = New ActivityLog
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.Delete(MiscAccountsonSales)
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
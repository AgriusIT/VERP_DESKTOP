Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmQuoteRequestList
    Implements IGeneral
    Dim LeadProfile As LeadProfileBE
    Dim objDAL As LeadProfileDAL
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public DoHaveDeleteRights As Boolean = False
    Dim objCommand As New OleDbCommand
    Dim objCon As OleDbConnection
    Private Sub frmProDealerList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetAllRecords()
            Me.GetSecurityRights()
            'ApplySecurity(SBUtility.Utility.EnumDataMode.New)
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True

                'Me.BtnSave.Enabled = True
                'Me.BtnDelete.Enabled = True
                'Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefVendor)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        'If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                        '    Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        'Else
                        '    Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        'End If
                        'Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                'Me.BtnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
                'Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                        'ElseIf Rights.Item(i).FormControlName = "Save" Then
                        '    If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Update" Then
                        '    If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.BtnDelete.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Print" Then
                        '    Me.BtnPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            '// Adding Delete Button in the grid
            If Me.grd.RootTable.Columns.Contains("Done") = False Then
                Me.grd.RootTable.Columns.Add("Done")
                Me.grd.RootTable.Columns("Done").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Done").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Done").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Done").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
                Me.grd.RootTable.Columns("Done").Width = 70
                Me.grd.RootTable.Columns("Done").ButtonText = "Done"
                Me.grd.RootTable.Columns("Done").Key = "Done"
                Me.grd.RootTable.Columns("Done").Caption = "Action"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        '    Try
        '        If LoginGroup = "Administrator" Then
        '            Me.Visible = True
        '            DoHaveSaveRights = True
        '            DoHaveUpdateRights = True
        '            DoHaveDeleteRights = True
        '            Me.CtrlGrdBar1.mGridPrint.Enabled = True
        '            Me.CtrlGrdBar1.mGridExport.Enabled = True
        '            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
        '            Exit Sub
        '        End If
        '        If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
        '            If RegisterStatus = EnumRegisterStatus.Expired Then
        '                Me.Visible = False
        '                DoHaveSaveRights = False
        '                DoHaveUpdateRights = False
        '                DoHaveDeleteRights = False
        '                Me.CtrlGrdBar1.mGridPrint.Enabled = False
        '                Me.CtrlGrdBar1.mGridExport.Enabled = False
        '                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
        '                Exit Sub
        '            End If
        '        Else
        '            Me.Visible = False
        '            DoHaveSaveRights = False
        '            DoHaveUpdateRights = False
        '            DoHaveDeleteRights = False
        '            Me.CtrlGrdBar1.mGridPrint.Enabled = False
        '            Me.CtrlGrdBar1.mGridExport.Enabled = False
        '            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
        '            For Each RightsDt As GroupRights In Rights
        '                If RightsDt.FormControlName = "View" Then
        '                    Me.Visible = True
        '                ElseIf RightsDt.FormControlName = "Print" Then
        '                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
        '                ElseIf RightsDt.FormControlName = "Export" Then
        '                    Me.CtrlGrdBar1.mGridExport.Enabled = True
        '                ElseIf RightsDt.FormControlName = "Field Chooser" Then
        '                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
        '                ElseIf RightsDt.FormControlName = "Save" Then
        '                    DoHaveSaveRights = True
        '                ElseIf RightsDt.FormControlName = "Update" Then
        '                    DoHaveUpdateRights = True
        '                ElseIf RightsDt.FormControlName = "Done" Then
        '                    DoHaveDeleteRights = True
        '                End If
        '            Next
        '        End If
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        '    Try
        '        FillDropDown(Me.cmbLeadTitle, "Select LeadId, LeadTitle from LeadProfile where Active = 1", True)
        '        FillDropDown(Me.cmbResponsible, "Select Employee_ID, Employee_Name From tblDefEmployee", True)
        '        FillDropDown(Me.cmbInside, "Select Employee_ID, Employee_Name From tblDefEmployee", True)
        '        FillDropDown(Me.cmbManager, "Select Employee_ID, Employee_Name  From tblDefEmployee", True)
        '    Catch ex As Exception
        '        Throw ex
        '    End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim str As String
            str = "select * from tblDefOpportunity where StageId = 2 and OpportunityType = 'Hardware'"
            Dim dt As DataTable
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                ApplyGridSettings()
            Else
                ShowErrorMessage("There are no pending quotes")
                Me.grd.DataSource = Nothing
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    'Private Sub btnAddDock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        frmLeadProfile.LeadID = 0
    '        frmLeadProfile.DoHaveSaveRights = DoHaveSaveRights
    '        frmLeadProfile.ShowDialog()

    '        GetAllRecords()
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
    '    Try
    '        If Me.grd.RowCount > 0 Then
    '            frmLeadProfile.LeadID = Val(Me.grd.CurrentRow.Cells("LeadId").Value.ToString)
    '            frmLeadProfile.DoHaveUpdateRights = DoHaveUpdateRights
    '        Else
    '            frmLeadProfile.LeadID = 0
    '        End If
    '        frmLeadProfile.ShowDialog()
    '        GetAllRecords()

    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            LeadProfile = New LeadProfileBE
            objDAL = New LeadProfileDAL
            'LeadProfile.LeadId = Val(Me.grd.CurrentRow.Cells("OpportunityId").Value.ToString)
            If e.Column.Key = "Done" Then
                'If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                'If DoHaveDeleteRights = True Then
                '    objDAL.Delete(LeadProfile)
                '    Me.grd.GetRow.Delete()
                '    SaveActivityLog("Configuration", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.Text, True)
                'Else
                '    msg_Information("You do not have delete rights.")
                'End If
                objCon = Con
                If objCon.State = ConnectionState.Closed Then objCon.Open()
                objCommand.Connection = objCon

                Dim trans As OleDbTransaction = objCon.BeginTransaction
                objCommand.CommandType = CommandType.Text

                objCommand.Transaction = trans

                objCommand.CommandText = ""
                objCommand.CommandText = "Update tbldefOpportunity set StageId = 3, QuoteTime = '" & Date.Now.ToString("dd-MMM-yyy hh:mm:ss") & "' where OpportunityId = " & Val(Me.grd.CurrentRow.Cells("OpportunityId").Value.ToString) & ""
                objCommand.ExecuteNonQuery()
                trans.Commit()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Lead Profile"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs)
        Try

            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick
        Try
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub
End Class
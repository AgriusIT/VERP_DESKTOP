Imports System
Imports System.Data.SqlClient
Imports SBModel
Imports SBDal
Public Class frmBSandPLReports
    Implements IGeneral
    Dim ReportTemplateId As Integer
    Dim Template As BSandPLReportTemplateBE
    Public Shared TemplateId As Integer
    Public Shared Title As String
    Public Shared Type As String
        Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Me.grd.RootTable.Columns.Contains("Map") = False Then
                Me.grd.RootTable.Columns.Add("Map")
                Me.grd.RootTable.Columns("Map").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Map").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Map").ButtonText = "Map"
                Me.grd.RootTable.Columns("Map").Key = "Map"
                Me.grd.RootTable.Columns("Map").Caption = "Action"
            End If
            Me.grd.RootTable.Columns("ReportTemplateId").Visible = False
            Me.grd.RootTable.Columns("SortOrder").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
        End Sub

        Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
            Try
                If LoginGroup = "Administrator" Then
                    Me.Visible = True
                    Me.btnSave.Enabled = True
                    Me.btnDelete.Enabled = True
                    Exit Sub
                End If
                If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    For Each RightsDt As GroupRights In Rights
                        If RightsDt.FormControlName = "View" Then
                            Me.Visible = True
                        ElseIf RightsDt.FormControlName = "Save" Then
                            If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                        ElseIf RightsDt.FormControlName = "Update" Then
                            If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                        ElseIf RightsDt.FormControlName = "Delete" Then
                            Me.btnDelete.Enabled = True
                        End If
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
            Try
            If New BSandPLReportTemplateDAL().Delete_Record(Template) = True Then
                Return True
                SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.txtTitle.Text, True)
            Else
                Return False
            End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

        End Sub

        Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
            Try
            Template = New BSandPLReportTemplateBE
            Template.ReportTemplateId = ReportTemplateId
            Template.Title = Me.txtTitle.Text
            Template.Remarks = Me.txtRemarks.Text
            If rbtBalanceSheet.Checked = True Then
                Template.Type = "BS"
            ElseIf rbtProfitandLoss.Checked = True Then
                Template.Type = "PL"
            End If
            Template.SortOrder = Me.txtSortOrder.Text
            Template.Active = Me.chkActive.Checked
        Catch ex As Exception
            Throw ex
        End Try
        End Sub

        Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
            Try
            Me.grd.DataSource = New BSandPLReportTemplateDAL().GetAll()
                Me.grd.RetrieveStructure()
            ApplyGridSettings()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub EditRecords()
            Try
            Template = New BSandPLReportTemplateBE
            ReportTemplateId = Me.grd.CurrentRow.Cells("ReportTemplateId").Value
            Me.txtTitle.Text = Me.grd.CurrentRow.Cells("Title").Value
            Me.txtRemarks.Text = Me.grd.CurrentRow.Cells("Remarks").Value

            If Me.grd.CurrentRow.Cells("Type").Value.ToString = "BS" Then
                rbtBalanceSheet.Checked = True
            ElseIf Me.grd.CurrentRow.Cells("Type").Value.ToString = "PL" Then
                rbtProfitandLoss.Checked = True
            End If

            Me.txtSortOrder.Text = Me.grd.CurrentRow.Cells("SortOrder").Value

            If Me.grd.CurrentRow.Cells("Active").Value.ToString = "True" Then
                Me.chkActive.Checked = True
            Else
                Me.chkActive.Checked = False
            End If
            Me.btnSave.Text = "&Update"
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        End Sub

        Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
            Try
            If Me.txtTitle.Text = String.Empty Then
                ShowErrorMessage("Please enter Title name")
                Me.txtTitle.Focus()
                Return False
            End If
                FillModel()
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
            Try
            Me.txtTitle.Text = String.Empty
            Me.txtRemarks.Text = String.Empty
            Me.rbtBalanceSheet.Checked = True
            Me.txtSortOrder.Text = 1
            Me.chkActive.Checked = True
            GetAllRecords()
            Me.btnSave.Text = "&Save"
            Me.txtTitle.Focus()
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
            Try
            If New BSandPLReportTemplateDAL().Save(Template) = True Then
                Return True
                SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtTitle.Text, True)
            Else
                Return False
            End If

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

        End Sub

        Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

        End Sub

        Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            TemplateId = grd.CurrentRow.Cells("ReportTemplateId").Value
            Title = grd.CurrentRow.Cells("Title").Value.ToString
            Type = grd.CurrentRow.Cells("Type").Value.ToString
            frmMain.LoadControl("frmBSandPLTemplateMapping")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grd.RowDoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

        Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
            Try
            If New BSandPLReportTemplateDAL().Update(Template) = True Then
                Return True
                SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtTitle.Text, True)
            Else
                Return False
            End If
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            Try
                If IsValidate() = True Then
                    If Me.btnSave.Text = "&Save" Then
                        If Save() = True Then
                            DialogResult = Windows.Forms.DialogResult.Yes
                            ReSetControls()
                        End If
                    Else
                        If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                        If Update1() = True Then
                            DialogResult = Windows.Forms.DialogResult.Yes
                            msg_Information(str_informUpdate)
                            ReSetControls()
                        End If
                    End If
                End If
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
        End Sub

        Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
            Try
                If Me.grd.RowCount = 0 Then Exit Sub
            Template = New BSandPLReportTemplateBE
            Template.ReportTemplateId = Me.grd.CurrentRow.Cells("ReportTemplateId").Value
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                If Delete() = True Then
                    DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
        End Sub

        Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
            Try
                Me.Close()
            Catch ex As Exception
                ShowErrorMessage(ex.Message)
            End Try
        End Sub

    Private Sub frmBSandPLReports_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
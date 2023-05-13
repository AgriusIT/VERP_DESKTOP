Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.SqlClient
Public Class frmUpdateReturnableGatepassDetail
    Implements IGeneral
    Enum EnumGrd
        IssueDetailID
        Issue_ID
        Issue_No
        Issue_Date
        Issue_To
        Remarks
        IssueDetail
        Status
        ReceivedDate
        Comments
    End Enum
    Dim IssueDetailId As Integer
    Dim ReceivingDate As DateTime
    Dim DtRetunablData As New DataTable
    Dim IsFormLoaded As Boolean = True
    Dim ReturnableGatepass As ReturnAbleGatePassDetail
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                If col.Index <> EnumGrd.ReceivedDate AndAlso col.Index <> EnumGrd.Comments Then
                    col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub
    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            ReturnableGatepass = New ReturnAbleGatePassDetail
            ReturnableGatepass.IssueDetailID = IssueDetailId
            ReturnableGatepass.RecivingDate = Me.GridEX1.GetRow.Cells(EnumGrd.ReceivedDate).Value
            ReturnableGatepass.Comments = Me.GridEX1.GetRow.Cells(EnumGrd.Comments).Text.ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            If Condition = "Pending" Then
                DtRetunablData = New UpdateReturnableGatepassDAL().GetAllRecord("Pending")
            ElseIf Condition = "Received" Then
                DtRetunablData = New UpdateReturnableGatepassDAL().GetAllRecord("Received")
            ElseIf Condition = "All" Then
                DtRetunablData = New UpdateReturnableGatepassDAL().GetAllRecord("All")
            End If
            DtRetunablData.AcceptChanges()
            Me.GridEX1.DataSource = Nothing
            Me.GridEX1.DataSource = DtRetunablData
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            Return New UpdateReturnableGatepassDAL().Update(ReturnableGatepass)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmUpdateReturnableGatepassDetail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            GetAllRecords("Pending")
            IsFormLoaded = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub frmUpdateReturnableGatepassDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub
    Private Sub rbtPending_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtPending.CheckedChanged, rbtReceived.CheckedChanged, rbtAll.CheckedChanged
        Try
            If IsFormLoaded = True Then
                If Me.rbtPending.Checked = True Then
                    GetAllRecords("Pending")
                End If
            End If
            If Me.rbtReceived.Checked = True Then
                GetAllRecords("Received")
            End If
            If Me.rbtAll.Checked = True Then
                GetAllRecords("All")
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Me.Cursor = Cursors.WaitCursor
        Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub GridEX1_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.ColumnButtonClick
        Try
            If e.Column.Key = "Update" Then
                If IsDBNull(Me.GridEX1.GetRow.Cells("ReceivedDate").Value) Then
                    ShowErrorMessage("Please select received date.")
                    Exit Sub
                End If
                If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                FillModel()
                If Update1() Then DialogResult = Windows.Forms.DialogResult.Yes
                'msgbox("Record updated successfully", MsgBoxStyle.Information, str_MessageHeader)
                msg_Information(str_informUpdate)
                Me.rbtPending.Checked = True
                Me.GetAllRecords("Pending")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GridEX1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridEX1.SelectionChanged
        Try
            IssueDetailId = Me.GridEX1.GetRow.Cells(EnumGrd.IssueDetailID).Value
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GridEX1_CellValueChanged(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX1.CellValueChanged
        Try

            If e.Column.Key = "ReceivingDate" Then
                Me.GridEX1.UpdateData()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                'Me.BtnSave.Enabled = True
                'Me.BtnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If

            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                'Me.BtnNew.Enabled = False
                'Me.BtnEdit.Enabled = False
                'Me.BtnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                        'ElseIf Rights.Item(i).FormControlName = "Save" Then
                        '    If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Update" Then
                        '    If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.BtnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub


End Class
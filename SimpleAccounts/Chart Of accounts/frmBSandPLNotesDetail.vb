''10-Mar-2014 Task:2478   Imran Ali    Bug fix
Imports SBDal
Imports SBModel
Public Class frmBSandPLNotesDetail
    Implements IGeneral
    Dim PLBSdetail As List(Of PLandBSnotesDetail)

    Private Sub GridEX1_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow

    End Sub

    Private Sub frmBSandPLNotesDetail_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            SaveToolStripButton_Click(btnSave, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.Escape Then

            NewToolStripButton_Click(btnNew, Nothing)
            Exit Sub
        End If
    End Sub

    Private Sub frmBSandPLNotesDetail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.Visible = True
            Me.lblProgress.BackColor = Color.LightYellow
            'Me.Cursor = Cursors.WaitCursor Comment against task:2478
            Application.DoEvents()
            NewToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            Me.lblProgress.Visible = False

        End Try
    End Sub
    Private Sub frmBSandPLNotesDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        

    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each Col As Janus.Windows.GridEX.GridEXColumn In grd.RootTable.Columns
                If Col.Index <> 2 AndAlso Col.Index <> 3 Then
                    Col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
            Next
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Me.grd.RootTable.Columns("DrBS_note_id").ValueList.PopulateValueList(New PLandBSnotesDetailDAL().BSDetail.DefaultView, "note_no", "note_title")
            Me.grd.RootTable.Columns("PL_note_id").ValueList.PopulateValueList(New PLandBSnotesDetailDAL().PLDetail.DefaultView, "note_no", "note_title")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            PLBSdetail = New List(Of PLandBSnotesDetail)
            Dim PLBS As PLandBSnotesDetail
            Me.grd.UpdateData()
            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
            If Not dt Is Nothing Then
                For Each row As DataRow In dt.GetChanges.Rows
                    PLBS = New PLandBSnotesDetail
                    PLBS.main_sub_sub_id = row.Item("main_sub_sub_id")
                    PLBS.DrBS_note_id = row.Item("DrBS_note_id")
                    PLBS.CrBS_note_id = row.Item("DrBS_note_id")
                    PLBS.PL_note_id = row.Item("PL_note_id")
                    PLBSdetail.Add(PLBS)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grd.DataSource = Nothing
            Me.grd.DataSource = New PLandBSnotesDetailDAL().PlandBsdetail
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

    End Function

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        FillCombos()
        GetAllRecords()
        GetSecurityRights()
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            
            If Not btnSave.Text = "Save" And Not btnSave.Text = "&Save" Then
                If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
            End If
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Call FillModel()
            Call New PLandBSnotesDetailDAL().save(PLBSdetail)
            'msg_Information(str_informSave)
            Call NewToolStripButton_Click(Nothing, Nothing)
        Catch ex As Exception
            msg_Error(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try

    End Sub
    Private Sub GetSecurityRights()
        Try
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
                'Me.Visible = False
                'Me.btnEdit.Enabled = False
                'Me.btnSave.Enabled = False
                'Me.btnDelete.Enabled = False
                'Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                        'ElseIf Rights.Item(i).FormControlName = "Save" Then
                        '    If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Update" Then
                        '    If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        'Me.btnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        'me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "PL AND BS Note Setting"
        Catch ex As Exception

        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

   
    
End Class
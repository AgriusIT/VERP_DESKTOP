'Task #2015060021 Ali Ansari Rectifying Grid Save Error
'23-Jun-2015 Task#123062015 Ahmad Sharif
'24-Jun-2015 Task#124062015 Ahmad Sharif: Change SP on index 2
'24-Jun-2015 Task#124062015 Ahmad Sharif: Change SP on index 2
'14-Jul-2015 Task# 201507020 Ali Ansari Adding Contact Numbers of concerned persons,Customer Link and general lay out improvement
Imports SBDal
Imports SBModel
Imports SBUtility

Public Class frmGrdRptProjectVisitDetail

    Sub FillGrid()
        Try
            Dim strSQL As String = String.Empty
            If Me.cmbResult.SelectedIndex = 0 Then
                'strSQL = "SP_PROJECTVISITDETAIL1 '" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy") & "','" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy") & "'"

                strSQL = "SP_PROJECTVISITDETAIL1 '" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy") & "','" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy") & "','" & IIf(Me.cmbRepresentive.SelectedIndex = 0, "0", cmbRepresentive.Text) & "'"
            ElseIf Me.cmbResult.SelectedIndex = 1 Then
                'Task#124062015 Change SP 
                '                strSQL = "SP_PROJECTVISITDETAIL2 '" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy") & "','" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy") & "'"
                strSQL = "SP_PROJECTVISITDETAIL2 '" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy") & "','" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy") & "','" & IIf(Me.cmbRepresentive.SelectedIndex = 0, "0", cmbRepresentive.Text) & "'"
            ElseIf Me.cmbResult.SelectedIndex = 2 Then
                '                strSQL = "SP_PROJECTVISITDETAIL3 '" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy") & "','" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy") & "'"
                strSQL = "SP_PROJECTVISITDETAIL3 '" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy") & "','" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy") & "','" & IIf(Me.cmbRepresentive.SelectedIndex = 0, "0", cmbRepresentive.Text) & "'"
            ElseIf Me.cmbResult.SelectedIndex = 3 Then
                'strSQL = "SP_PROJECTVISITDETAIL4 '" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy") & "','" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy") & "'"
                'strSQL = "SP_PROJECTVISITDETAIL4 '" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy") & "','" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy") & "','" & Me.cmbRepresentive.Text & "'"
                strSQL = "SP_PROJECTVISITDETAIL4 '" & Me.dtpFromDate.Value.ToString("dd-MMM-yyyy") & "','" & Me.dtpToDate.Value.ToString("dd-MMM-yyyy") & "','" & IIf(Me.cmbRepresentive.SelectedIndex = 0, "0", cmbRepresentive.Text) & "'"
            End If

            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()

            If cmbResult.SelectedIndex = 0 Then
                Me.grd.Name = "grd_1"
            ElseIf cmbResult.SelectedIndex = 1 Then
                Me.grd.Name = "grd_2"
            ElseIf cmbResult.SelectedIndex = 2 Then
                Me.grd.Name = "grd_3"
            ElseIf cmbResult.SelectedIndex = 3 Then
                Me.grd.Name = "grd_4"
            End If

            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()

            'Me.grd.RootTable.Columns("DirecotrComments").Visible = False
            'Me.grd.RootTable.Columns("GMComments").Visible = False
            'Me.grd.RootTable.Columns("ASMComments").Visible = False
            'Me.grd.RootTable.Columns("ManagerComments").Visible = False
            'Me.grd.RootTable.Columns("RPComments").Visible = False
            'Me.grd.RootTable.Columns("OthersComments").Visible = False
            'Me.grd.RootTable.Columns("ProjectCode").Visible = False

            'Task#124062015 if grd has ProjectCode then hide the ProjectCode column
            If Me.grd.RootTable.Columns.Contains("ProjectCode") Then
                Me.grd.RootTable.Columns("ProjectCode").Visible = False
            End If

            If Me.grd.RootTable.Columns.Contains("projectvisitid") Then
                Me.grd.RootTable.Columns("projectvisitid").Visible = False
            End If

            'Task#123062015 if grd has Comments column then hide the column and set Column type of grd to link column type
            If Me.grd.RootTable.Columns.Contains("Comments") Then
                Me.grd.RootTable.Columns("Comments").Visible = True
                Me.grd.RootTable.Columns("Comments").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            End If

            'Task#123062015 if grd has BulName column then set Column type of BulName link column type 
            If Me.grd.RootTable.Columns.Contains("BulName") Then
                Me.grd.RootTable.Columns("BulName").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            End If

            'Task#123062015 if grd has ArcName column then set Column type of ArcName link column type 
            If Me.grd.RootTable.Columns.Contains("ArcName") Then
                Me.grd.RootTable.Columns("ArcName").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            End If

            'Task#123062015 if grd has ConName column then set Column type of ConName link column type 
            If Me.grd.RootTable.Columns.Contains("ConName") Then
                Me.grd.RootTable.Columns("ConName").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            End If
            'End Task#123062015


            'Altered Against Task# 2015070020 Making Customer Column to Link Type
            If Me.grd.RootTable.Columns.Contains("CustName") Then
                Me.grd.RootTable.Columns("CustName").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            End If
            'Altered Against Task# 2015070020 Making Customer Column to Link Type


            'Me.grd.RootTable.Columns(0).Visible = False
            'CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False

            If Me.grd.RootTable.Columns.Contains("VisitDate") Then Me.grd.RootTable.Columns("VisitDate").FormatString = "dd/MMM/yyyy"
            If Me.grd.RootTable.Columns.Contains("FollowUpDate") Then Me.grd.RootTable.Columns("FollowUpDate").FormatString = "dd/MMM/yyyy"

            If Me.cmbResult.SelectedIndex = 1 Then

                Dim grp As New Janus.Windows.GridEX.GridEXGroup
                grp.Column = Me.grd.RootTable.Columns("VisitDate")
                Me.grd.RootTable.Groups.Add(grp)
                grp.Expand()
                'Task#124062015 Hide Column ProjectVisitId and ProjectCode from grd
                'Me.grd.RootTable.Columns("ProjectVisitId").Visible = False
                'Me.grd.RootTable.Columns("ProjectCode").Visible = False
                'End Task#124062015

            ElseIf Me.cmbResult.SelectedIndex = 3 Then
                'Me.grd.RootTable.Columns("ConOwner").Caption = "Owner Name"
                Me.grd.RootTable.Columns("CustName").Caption = "Owner Name"
                Me.grd.RootTable.Columns("ComSE").Caption = "Representative Person"
            End If
            Me.grd.AutoSizeColumns()

            'Task#123062015 if grd has Comments column then setting the width of column Comments in grd
            If Me.grd.RootTable.Columns.Contains("Comments") Then
                Me.grd.RootTable.Columns("Comments").Width = 200
            End If
            'End Task#123062015

            'Altered Against Task #2015060021 Ali Ansari Rectifying Grid Save Error
            CtrlGrdBar1_Load(Nothing, Nothing)
            'Altered Against Task #2015060021 Ali Ansari Rectifying Grid Save Error
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                'Me.chkIssued.Checked = True 'TASK:M21 Set Issued Checked 
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True 'Task:2406 Added Field Chooser Rights
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    CtrlGrdBar1.mGridPrint.Enabled = False
                    CtrlGrdBar1.mGridExport.Enabled = False
                    CtrlGrdBar1.mGridChooseFielder.Enabled = False 'Task:2406 Added Field Chooser Rights
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmStoreIssuence)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                        CtrlGrdBar1.mGridExport.Enabled = True
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True  'Task:2406 Added Field Chooser Rights
                    End If
                End If
                'GetSecurityByPostingUser(UserPostingRights, Me.SaveToolStripButton, Me.DeleteToolStripButton)
            Else
                'Me.Visible = False
                'For i As Integer = 0 To Rights.Count - 1
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False 'Task:2406 Added Field Chooser Rights
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True  'Task:2406 Added Field Chooser Rights
                        'End Task:2716
                    End If
                Next
            End If

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowClick.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGrdRptProjectVisitDetail_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.cmbResult.Items.Add("Planned / Follow up date report")
            Me.cmbResult.Items.Add("Missed Visit Report")
            Me.cmbResult.Items.Add("Daily Activity Sheet")
            Me.cmbResult.Items.Add("R.P Visit Report")
            Me.cmbResult.SelectedIndex = 0
            ApplySecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Altered Against Task #2015060021 Ali Ansari Rectifying Grid Save Error
    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try

            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As IO.FileStream = Nothing
                fs = New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                If Not fs Is Nothing Then Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & vbCrLf & "Production Visit"
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Altered Against Task #2015060021 Ali Ansari Rectifying Grid Save Error

    'Task#123062015 Event for Click on item in grid
    Private Sub grd_LinkClicked(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.LinkClicked
        Try
            If e.Column.Key = "ConName" Then

                Dim frm As New frmProjectVisitInfo
                frm.ProjectCode = Me.grd.CurrentRow.Cells("ProjectCode").Value.ToString
                frm.Text = Me.grd.CurrentRow.Cells("ProjectName").Value.ToString

                frm.Tag = e.Column.Key.ToString
                'Marked Against Task# 201507020 making style sheet physible
                'frm.Size = New Size(528, 229)
                'frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
                frm.MaximizeBox = False
                frm.MinimizeBox = False
                'frm.WindowState = FormWindowState.Normal
                'frm.StartPosition = FormStartPosition.CenterParent
                'Marked Against Task# 201507020 making style sheet physible
                'Altered Against Task# 201507020 making style sheet physible

                ApplyStyleSheet(frm)
                frm.ShowDialog()
                'Altered Against Task# 201507020 making style sheet physible

            ElseIf e.Column.Key = "BulName" Then
                Dim frm As New frmProjectVisitInfo
                frm.ProjectCode = Me.grd.CurrentRow.Cells("ProjectCode").Value.ToString
                frm.Text = Me.grd.CurrentRow.Cells("ProjectName").Value.ToString
                frm.Tag = e.Column.Key.ToString
                'Marked Against Task# 201507020 making style sheet physible
                'frm.Size = New Size(528, 229)
                ' frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
                ' frm.MaximizeBox = False
                ' frm.MinimizeBox = False
                ' frm.WindowState = FormWindowState.Normal
                ' frm.StartPosition = FormStartPosition.CenterParent
                'Marked Against Task# 201507020 making style sheet physible
                'Altered Against Task# 201507020 making style sheet physible
                ApplyStyleSheet(frm)
                frm.ShowDialog()
                'Altered Against Task# 201507020 making style sheet physible
            ElseIf e.Column.Key = "ArcName" Then
                Dim frm As New frmProjectVisitInfo
                frm.ProjectCode = Me.grd.CurrentRow.Cells("ProjectCode").Value.ToString
                frm.Text = Me.grd.CurrentRow.Cells("ProjectName").Value.ToString
                frm.Tag = e.Column.Key.ToString
                'Marked Against Task# 201507020 making style sheet physible
                'frm.Size = New Size(528, 229)
                'frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
                'frm.MaximizeBox = False
                'frm.MinimizeBox = False
                'frm.WindowState = FormWindowState.Normal
                'frm.StartPosition = FormStartPosition.CenterParent
                'Marked Against Task# 201507020 making style sheet physible
                'Altered Against Task# 201507020 making style sheet physible
                ApplyStyleSheet(frm)
                frm.ShowDialog()
                'Altered Against Task# 201507020 making style sheet physible


            ElseIf e.Column.Key = "Comments" Then
                Dim frm As New frmProjectVisitComments
                'frm.ProjectCode = Me.grd.CurrentRow.Cells("ProjectCode").Value.ToString
                frm.ProjectCode = Me.grd.CurrentRow.Cells("projectvisitid").Value.ToString
                frm.Text = Me.grd.CurrentRow.Cells("ProjectName").Value.ToString
                'Marked Against Task# 201507020 making style sheet physible
                'frm.Size = New Size(668, 558)
                'frm.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog
                'frm.MaximizeBox = False
                'frm.MinimizeBox = False
                'frm.WindowState = FormWindowState.Normal
                'frm.StartPosition = FormStartPosition.CenterParent
                'Marked Against Task# 201507020 making style sheet physible
                'Altered Against Task# 201507020 making style sheet physible
                ApplyStyleSheet(frm)
                frm.ShowDialog()
                'Altered Against Task# 201507020 making style sheet physible

                'Altered Against Task# 201507020 add customer information link click 
            ElseIf e.Column.Key = "CustName" Then

                Dim frm As New frmProjectVisitInfo
                frm.ProjectCode = Me.grd.CurrentRow.Cells("ProjectCode").Value.ToString
                frm.Text = Me.grd.CurrentRow.Cells("CustName").Value.ToString
                frm.Tag = e.Column.Key.ToString
                ApplyStyleSheet(frm)
                frm.ShowDialog()
                'Altered Against Task# 201507020 add customer information link click 

            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub fillCombos()
        Try
            FillDropDown(Me.cmbRepresentive, "Select comse,comse from tblprojectportfolio group by comse")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    'End Task#123062015
    Private Sub frmGrdRptProjectVisitDetail_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            fillCombos()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.dtpFromDate.Value = Date.Today
            Me.dtpToDate.Value = Date.Today
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
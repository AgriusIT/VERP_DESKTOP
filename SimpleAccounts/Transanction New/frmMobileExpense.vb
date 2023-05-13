Imports SBModel
Imports SBDal
Public Class frmMobileExpense
    Dim MOB As List(Of mobileExpBE)
    Public Function Save() As Boolean
        Try
            If New mobileExpDAL().delete(Me.cmbMonth.SelectedValue, Me.cmbYear.SelectedValue) = True Then
                If New mobileExpDAL().save(MOB) = True Then
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Sub FillModel()
        Try
            Dim MOBex As mobileExpBE   ' created a variable hrere
            MOB = New List(Of mobileExpBE)
            For Each row As Janus.Windows.GridEX.GridEXRow In grdMobileExp.GetRows ' foreach loop is used
                MOBex = New mobileExpBE '' data will be filled step by step in it
                MOBex.employee_Id = row.Cells(0).Value
                MOBex.usedBill = row.Cells(4).Value
                MOBex.paidBill = row.Cells(5).Value
                MOBex.limit = row.Cells(6).Value
                MOBex.paidByEmp = row.Cells(7).Value
                MOBex.paidByCo = row.Cells(8).Value
                MOBex.month = Me.cmbMonth.SelectedValue
                MOBex.year = Me.cmbYear.SelectedValue
                MOB.Add(MOBex) '' after filling it will be added to this
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            If IsValidate() = True Then
                Me.grdMobileExp.UpdateData()
                'R-974 Ehtisham ul Haq user friendly system modification on 8-1-14 
                'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                If Save() = True Then

                    DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informSave)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Public Function IsValidate() As Boolean
        Try

            If Me.grdMobileExp.RowCount = 0 Then
                ShowErrorMessage("there is no record grid")
                Me.grdMobileExp.Focus()
                Return False
            End If
            FillModel()
            'If Me Then
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmMobileExpense_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13
            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                btnPrint_Click(Nothing, Nothing)
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmMobileExpense_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Me.cmbYear.ValueMember = "Year"
            Me.cmbYear.DisplayMember = "Year"
            Me.cmbYear.DataSource = GetYears()
            Me.cmbMonth.ValueMember = "Month"
            Me.cmbMonth.DisplayMember = "Month_Name"
            Me.cmbMonth.DataSource = GetMonths()
            Me.cmbMonth.SelectedValue = Date.Now.Month
            Me.cmbYear.SelectedValue = Date.Now.Year
            ApplySecurity()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
    End Sub
    Public Sub FillGrid()
        Try
            Dim dt As DataTable = New mobileExpDAL().GetEmpData
            Me.grdMobileExp.DataSource = dt
            Dim dtData As DataTable = New mobileExpDAL().GetAllRecords(Me.cmbMonth.SelectedValue, Me.cmbYear.SelectedValue)
            Dim dtGrid As DataTable = CType(Me.grdMobileExp.DataSource, DataTable)
            Dim dr() As DataRow
            For Each r As DataRow In dtGrid.Rows
                dr = dtData.Select("Employee_Id=" & r.Item("Emp_Id") & "")
                If dr IsNot Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            r.BeginEdit()
                            r(4) = Val(drFound(1))
                            r(5) = Val(drFound(2))
                            r(6) = Val(drFound(3))
                            r.EndEdit()
                        Next
                    End If
                End If
            Next
            dtGrid.AcceptChanges()
            dtGrid.Columns("PayableByEmp").Expression = "IIF(Total_Bill > 0, Total_Bill-Limit,0)"
            dtGrid.Columns("PayableByCompany").Expression = "IIF(PayableByEmp > 0, (Paid_Bill - PayableByEmp), Paid_Bill-0)"
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ApplyGridSettings()
        Try
            For i As Integer = 0 To 3
                Me.grdMobileExp.RootTable.Columns(i).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Next
            Me.grdMobileExp.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub ApplySecurity()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDetailAccount)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
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
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            ShowReport("rptMobileExpense", "{tblMobileExp.month} = " & Me.cmbMonth.SelectedValue & " AND {tblMobileExp.year} = " & Me.cmbYear.SelectedValue & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    'Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
    '    Try
    '        If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me..Value.ToString("yyyy-M-d 00:00:00") Then
    '            If DateLock.Lock = True Then
    '                Return True
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Function
    'Public Sub ValidateDateLock()
    '    Try
    '        Dim dateLock As New SBModel.DateLockBE
    '        dateLock = DateLockList.Find(AddressOf chkDateLock)
    '        If dateLock IsNot Nothing Then
    '            If dateLock.DateLock.ToString.Length > 0 Then
    '                flgDateLock = True
    '            Else
    '                flgDateLock = False
    '            End If
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    
    

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHeader.Click

    End Sub
End Class

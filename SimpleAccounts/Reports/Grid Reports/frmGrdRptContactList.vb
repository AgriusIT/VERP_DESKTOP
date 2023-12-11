''18-Apr-2014 TASK:2577 Imran Ali Send Branded SMS Functionlity
''
Imports SBModel
Imports SBDal
Imports SBUtility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class frmGrdRptContactList

    Private Sub frmGrdRptContactList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmGrdRptContactList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.txtOtherMobileNo.Visible = False
            Me.Label2.Visible = False
            Me.cmbLanguage.ValueMember = "Culture"
            Me.cmbLanguage.DisplayMember = "Country"
            Me.cmbLanguage.DataSource = GetMultiLanguageCodes()
            If Not Me.cmbLanguage.SelectedIndex = -1 Then Me.cmbLanguage.SelectedValue = "en-US"
            Me.cmbPeriod.Text = "Current Month"
            FillGrid()
            GetSecurityRights()
            Me.SplitContainer1.Panel2Collapsed = True 'Task:2577 Set Panel2 Hidden
            FillDropDown(Me.cmbSMSTemplates, "Select ID,TemplateKey From SMSTemplateTable Where TemplateKey like '%CustomTemplate-%'", True)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillGrid()
        Try
            Dim strSQL As String = "SP_ContactList"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns.Add("Column1", Janus.Windows.GridEX.ColumnType.CheckBox).EditType = Janus.Windows.GridEX.EditType.CheckBox
            Me.grd.RootTable.Columns("Column1").ActAsSelector = True
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                If Me.grd.RootTable.Columns(c).Index <> Me.grd.RootTable.Columns("Mobile").Index Then
                    Me.grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
                Me.grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.TextBox
            Next
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillServivcesGrid()
        Try
            Dim str As String = ""
            str = "SELECT     'Services Customer' AS ContactType, TblCompanyContacts.ContactName AS [Contact Name], TblCompanyContacts.Phone, TblCompanyContacts.Mobile, TblCompanyContacts.Email, TblCompanyContacts.Address " _
                & "FROM       tblJobCard INNER JOIN tblVehicleInfo ON tblJobCard.VehicleID = tblVehicleInfo.VahicleID INNER JOIN TblCompanyContacts ON tblVehicleInfo.CompanyContactID = TblCompanyContacts.PK_Id " _
                & "WHERE      (TblCompanyContacts.PK_Id IN(SELECT CompanyContactID FROM tblVehicleInfo AS tblVehicleInfo_1)) And tblJobCard.JobCardDate between '" & Me.dtpFrom.Value.ToString("yyyy-MM-dd") & "' And '" & Me.dtpTo.Value.ToString("yyyy-MM-dd") & "'"
            Dim dt As DataTable
            dt = GetDataTable(str)
            Me.grdServices.DataSource = dt
            Me.grdServices.RetrieveStructure()
            Me.grdServices.RootTable.Columns.Add("Column1", Janus.Windows.GridEX.ColumnType.CheckBox).EditType = Janus.Windows.GridEX.EditType.CheckBox
            Me.grdServices.RootTable.Columns("Column1").ActAsSelector = True
            For c As Integer = 0 To Me.grdServices.RootTable.Columns.Count - 1
                If Me.grdServices.RootTable.Columns(c).Index <> Me.grdServices.RootTable.Columns("Mobile").Index Then
                    Me.grdServices.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If
                Me.grdServices.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.TextBox
            Next
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grdServices.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFrom.Value = Date.Today
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-1)
                Me.dtpTo.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpTo.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbLanguage.SelectedIndex
            Me.cmbLanguage.ValueMember = "Culture"
            Me.cmbLanguage.DisplayMember = "Country"
            Me.cmbLanguage.DataSource = GetMultiLanguageCodes()
            Me.cmbLanguage.SelectedIndex = id
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If Me.TabControl1.SelectedTab Is Me.tabContactList Then
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    'Me.grd.SaveLayoutFile(fs)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Contact List"
            Else
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdServices.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdServices.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                    Me.grdServices.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
                Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Services Customer List"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSend.Enabled = True
                Exit Sub
            End If
            'Me.Visible = False
            Me.btnSend.Enabled = False

            For Each Rightsdt As GroupRights In Rights
                If Rightsdt.FormControlName = "View" Then
                    'Me.Visible = True
                ElseIf Rightsdt.FormControlName = "Send Message" Then
                    btnSend.Enabled = True
                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Task:2577 Added Event For Message Write
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            If Me.SplitContainer1.Panel2Collapsed = True Then
                Me.SplitContainer1.Panel2Collapsed = False
                Me.txtOtherMobileNo.Visible = True
                Me.Label2.Visible = True
            Else
                Me.SplitContainer1.Panel2Collapsed = True
                Me.txtOtherMobileNo.Visible = False
                Me.Label2.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2577
    'Task:2577 Added Event For SEND SMS
    Private Sub btnSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSend.Click
        If Me.grd.RowCount = 0 Then Exit Sub
        Me.lblProcess.Text = "Saving..."
        Me.lblProcess.Visible = True
        'Application.DoEvents()
        
        Try
            Dim strPhoneNo() As String = {}

            If Me.grd.GetCheckedRows.Length > 0 Then
                For Each objRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetCheckedRows
                    'Call SendBrandedSMS(objRow.Cells("Phone").Value.ToString, Me.txtMsgBody.Text.ToString)

                    'strPhoneNo = CStr(Me.txtOtherMobileNo.Text.ToString.Replace("-", "").Replace("+", "").Replace(" ", "").Replace("P", "").Replace("#", "").Replace(".", "").Replace(",", ";")).Split(";")
                    strPhoneNo = CStr(objRow.Cells("Mobile").Value.ToString.Replace("-", "").Replace("+", "").Replace(" ", "").Replace("P", "").Replace("#", "").Replace(".", "").Replace(",", ";")).Split(";")

                    'strPhoneNo = CStr(Me.txtOtherMobileNo.Text.ToString.Replace("-", "").Replace("+", "").Replace(" ", "").Replace("P", "").Replace("#", "").Replace(".", "").Replace(",", ";")).Split(";")

                    If strPhoneNo.Length > 0 Then
                        For Each strPhone As String In strPhoneNo
                            If strPhone.Length <= 11 Then
                                strPhone = "92" & Microsoft.VisualBasic.Right(strPhone, 10)
                            Else
                                strPhone = strPhone
                            End If
                            If strPhone.Length > 10 Then
                                SaveSMSLog(Me.txtMsgBody.Text.Replace("'", "''"), strPhone, "Informative Message")
                            End If
                        Next
                    End If
                Next


                'If Me.txtOtherMobileNo.Text.Length > 0 Then
                '    Me.txtOtherMobileNo.Text = Me.txtOtherMobileNo.Text.Replace(",", ";")
                'End If

                'Ahmad Sharif split the numbers in other Mobile No textbox

                strPhoneNo = CStr(Me.txtOtherMobileNo.Text.ToString.Replace("-", "").Replace("+", "").Replace(" ", "").Replace("P", "").Replace("#", "").Replace(".", "").Replace(",", ";")).Split(";")

                If strPhoneNo.Length > 0 Then
                    For Each strPhone As String In strPhoneNo
                        If strPhone.Length <= 11 Then
                            strPhone = "92" & Microsoft.VisualBasic.Right(strPhone, 10)
                        Else
                            strPhone = strPhone
                        End If
                        If strPhone.Length > 10 Then
                            SaveSMSLog(Me.txtMsgBody.Text.Replace("'", "''"), strPhone, "Informative Message")
                        End If
                    Next
                End If
            ElseIf Me.grdServices.GetCheckedRows.Length > 0 Then
                For Each objRow As Janus.Windows.GridEX.GridEXRow In Me.grdServices.GetCheckedRows
                    'Call SendBrandedSMS(objRow.Cells("Phone").Value.ToString, Me.txtMsgBody.Text.ToString)

                    'strPhoneNo = CStr(Me.txtOtherMobileNo.Text.ToString.Replace("-", "").Replace("+", "").Replace(" ", "").Replace("P", "").Replace("#", "").Replace(".", "").Replace(",", ";")).Split(";")
                    strPhoneNo = CStr(objRow.Cells("Mobile").Value.ToString.Replace("-", "").Replace("+", "").Replace(" ", "").Replace("P", "").Replace("#", "").Replace(".", "").Replace(",", ";")).Split(";")

                    'strPhoneNo = CStr(Me.txtOtherMobileNo.Text.ToString.Replace("-", "").Replace("+", "").Replace(" ", "").Replace("P", "").Replace("#", "").Replace(".", "").Replace(",", ";")).Split(";")

                    If strPhoneNo.Length > 0 Then
                        For Each strPhone As String In strPhoneNo
                            If strPhone.Length <= 11 Then
                                strPhone = "92" & Microsoft.VisualBasic.Right(strPhone, 10)
                            Else
                                strPhone = strPhone
                            End If
                            If strPhone.Length > 10 Then
                                SaveSMSLog(Me.txtMsgBody.Text.Replace("'", "''"), strPhone, "Services Message")
                            End If
                        Next
                    End If
                Next


                'If Me.txtOtherMobileNo.Text.Length > 0 Then
                '    Me.txtOtherMobileNo.Text = Me.txtOtherMobileNo.Text.Replace(",", ";")
                'End If

                'Ahmad Sharif split the numbers in other Mobile No textbox

                strPhoneNo = CStr(Me.txtOtherMobileNo.Text.ToString.Replace("-", "").Replace("+", "").Replace(" ", "").Replace("P", "").Replace("#", "").Replace(".", "").Replace(",", ";")).Split(";")

                If strPhoneNo.Length > 0 Then
                    For Each strPhone As String In strPhoneNo
                        If strPhone.Length <= 11 Then
                            strPhone = "92" & Microsoft.VisualBasic.Right(strPhone, 10)
                        Else
                            strPhone = strPhone
                        End If
                        If strPhone.Length > 10 Then
                            SaveSMSLog(Me.txtMsgBody.Text.Replace("'", "''"), strPhone, "Informative Message")
                        End If
                    Next
                End If
            Else
                If Me.txtOtherMobileNo.Text.Length > 0 Then
                    strPhoneNo = CStr(Me.txtOtherMobileNo.Text.Replace(",", ";")).Split(";")
                    If strPhoneNo.Length > 0 Then
                        For Each strPhone As String In strPhoneNo
                            If strPhone.Length <= 11 Then
                                strPhone = "92" & Microsoft.VisualBasic.Right(strPhone, 10)
                            Else
                                strPhone = strPhone
                            End If
                            If strPhone.Length > 10 Then
                                SaveSMSLog(Me.txtMsgBody.Text.Replace("'", "''"), strPhone, "Informative Message")
                            End If
                        Next
                    End If
                End If
            End If
            Me.lblProcess.Text = "Messages have been preserved, for sending messages"
            System.Threading.Thread.Sleep(5000)
            Me.lblProcess.Text = String.Empty
            Me.txtMsgBody.Text = String.Empty
            Me.txtOtherMobileNo.Text = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2577
    'Task:2577 Added Event for clear msgbody
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Me.txtMsgBody.Text = String.Empty
            Me.txtOtherMobileNo.Text = String.Empty
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'End Task:2577
    Private Sub cmbLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLanguage.SelectedIndexChanged
        Try
            Dim objCulture As System.Globalization.CultureInfo
            If Me.cmbLanguage.SelectedIndex = -1 Then Exit Sub
            objCulture = New System.Globalization.CultureInfo(Me.cmbLanguage.SelectedValue.ToString)
            Me.txtMsgBody.RightToLeft = IIf(objCulture.TextInfo.IsRightToLeft = True, System.Windows.Forms.RightToLeft.Yes, System.Windows.Forms.RightToLeft.No)
            ChangeInputLanguage(objCulture)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ': Ahmad Sharif : Added KeyPress event for digit only
    Private Sub txtOtherMobileNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOtherMobileNo.KeyPress
        Dim allowedChars As String = "0123456789,"
        If e.KeyChar <> ControlChars.Back Then
            If allowedChars.IndexOf(e.KeyChar) = -1 Then
                ' Invalid Character
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            FillServivcesGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub SMSTemplate()
        Try
            Dim Con As New SqlConnection(SQLHelper.CON_STR)
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim trans As SqlTransaction = Con.BeginTransaction
            Try
                Dim str As String = ""
                Dim str1 As String = ""
                str = "Select ID From SMSTemplateTable Where TemplateKey = N'CustomTemplate-" & Me.txtSMSTemplateName.Text & "'"
                str1 = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, str))
                If str1.Length > 1 Then
                    ShowInformationMessage("Template exists already, please choose an other name")
                    Exit Sub
                End If
                Dim strSQL As String = String.Empty
                strSQL = String.Empty
                strSQL = "INSERT INTO SMSTemplateTable(TemplateKey,TemplateDescription) VALUES(N'CustomTemplate-" & Me.txtSMSTemplateName.Text & "',N'" & Me.txtMsgBody.Text & "' ) "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                Throw ex
            Finally
                Con.Close()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSaveTemplate_Click(sender As Object, e As EventArgs) Handles btnSaveTemplate.Click
        Try
            If Me.txtMsgBody.Text = "" Then
                ShowInformationMessage("Please enter the Template/Msg body")
                Me.txtMsgBody.Focus()
                Exit Sub
            End If
            If Me.txtSMSTemplateName.Text = "" Then
                ShowInformationMessage("Please enter the Template Name")
                Me.txtSMSTemplateName.Focus()
                Exit Sub
            End If
            SMSTemplate()
            Me.txtSMSTemplateName.Text = ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSMSTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSMSTemplates.SelectedIndexChanged
        Try
            Dim Con As New SqlConnection(SQLHelper.CON_STR)
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim trans As SqlTransaction = Con.BeginTransaction
            Dim str As String = ""
            Dim str1 As String = ""
            str = "Select TemplateDescription From SMSTemplateTable Where ID = " & Me.cmbSMSTemplates.SelectedValue & ""
            str1 = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            Me.txtMsgBody.Text = str1
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
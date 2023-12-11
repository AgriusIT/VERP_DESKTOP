'21-May-2014 TSK 2640 JUNAID Monthly Balances
Imports System.Data
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports SBDal.PrintLogDAL

Public Class frmScheduleSMS
    Dim EnabledBrandedSMS As Boolean = False
    Public Function IsValidate() As Boolean
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            SavedData()
            SavedToGrid()
            'me.BtnSave.Text="&Update"
            'Reset()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub SavedData()
        Try
            'If IsValidate() = True Then
            Me.BtnSave.Name = "&Update"
            'If msg_Confirm(str_ConfirmSave) = True Then
            Dim cm As New OleDb.OleDbCommand
            Dim cn As New OleDb.OleDbConnection(Con.ConnectionString)
            cm.CommandText = "Insert into tblSMSSchedule (ScheduleDate, IsCustomer, IsVendor, Status) values( Convert(DateTime,'" & dtpSchDate.Value & "',102), '" & IIf(chkCustomerType.Checked = True, 1, 0) & "','" & IIf(chkVendorType.Checked = True, 1, 0) & "','Pending')"
            'cm.CommandText = "UPDATE tblSMSSchedule SET ScheduleDate='" & dtpSchDate.Value & "', IsCustomer='" & IIf(chkCustomerType.Checked = True, 1, 0) & "', IsVendor='" & IIf(chkVendorType.Checked = True, 1, 0) & "', Status='" & TextBox2.Text.ToString & "'"

            cm.Connection = cn
            cn.Open()
            cm.ExecuteNonQuery()
            cn.Close()
            'msg_Information(str_informSave)
            'VedndorTypeGetRecored()
            'ResetControl()
            'End If
            'End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Save records in Grid in History Tab
    Private Sub SavedToGrid()
        Try
            Dim cm As New OleDb.OleDbCommand
            Dim cn As New OleDb.OleDbConnection(Con.ConnectionString)
            Dim str As String
            ''cm.CommandText = "Insert into tblSMSSchedule (ScheduleDate, ScheduleTime, IsCustomer, IsVendor, Status) values( '" & dtpSchDate.Value & "' , '" & dtpSchTime.Value & "' , '" & IIf(chkCustomerType.Checked = True, 1, 0) & "','" & IIf(chkVendorType.Checked = True, 1, 0) & "', 'Pending')"
            'cm.CommandText = str
            'cm.Connection = cn
            'cn.Open()
            'cm.ExecuteNonQuery()
            'cn.Close()

            Me.grdSaved.BoundMode = Janus.Windows.GridEX.BoundMode.Unbound
            'If IsValidate() = True Then
            For Each dr As Janus.Windows.GridEX.GridEXRow In Me.grdData.GetRows
                If (Convert.ToBoolean(dr.Cells("StatusKey").Value)) = True Then
                    Dim datarow As Janus.Windows.GridEX.GridEXRow = grdSaved.AddItem()
                    datarow.BeginEdit()
                    ' Check Duplcate Records in Main Grid
                    'If datarow.Cells("Account Code").Value.ToString <> dr.Cells("Account Code").Value.ToString Then
                    datarow.Cells("Account Code").Value = dr.Cells("Account Code").Value.ToString
                    datarow.Cells("Account Title").Value = dr.Cells("Account Title").Value.ToString
                    datarow.Cells("Account Type").Value = dr.Cells("Account Type").Value.ToString
                    datarow.Cells("Mobile No").Value = dr.Cells("Mobile No").Value.ToString
                    datarow.Cells("Status").Value = "Pending"
                    datarow.Cells("Balance").Value = dr.Cells("Balance").Value.ToString

                    str = String.Empty
                    str = "INSERT INTO tblSMSConfigData (AccountCode, AccountTitle, AccountType, MobileNo, State, Balance) VALUES ('" & dr.Cells("Account Code").Value.ToString.Replace("'", "''") & "','" & dr.Cells("Account Title").Value.ToString.Replace("'", "''") & "','" & dr.Cells("Account Type").Value.ToString.Replace("'", "''") & "','" & dr.Cells("Mobile No").Value.ToString & "','" & datarow.Cells("Status").Value & "', '" & datarow.Cells("Balance").Value & "')"
                    cm.CommandText = str
                    cm.Connection = cn
                    cn.Open()
                    cm.ExecuteNonQuery()
                    cn.Close()
                    'End If
                    'str = String.Empty
                    'str = "Insert into tblSMSSchedule (ScheduleDate, IsCustomer, IsVendor, Customer_Id ,Vendor_Id, Status) values( '" & dtpSchDate.Value & "', '" & IIf(chkCustomerType.Checked = True, 1, 0) & "','" & IIf(chkVendorType.Checked = True, 1, 0) & "','" & dr.Cells("CustomerID").Value & "','" & dr.Cells("VendorId").Value & "' ,'Pending')"
                    'cm.CommandText = str
                    'cm.Connection = cn
                    'cn.Open()
                    'cm.ExecuteNonQuery()
                    'cn.Close()


                End If
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub chkCustomerType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCustomerType.CheckedChanged
        Try
            Me.grdData.BoundMode = Janus.Windows.GridEX.BoundMode.Bound
            Dim dt As DataTable = Nothing
            Dim str As String = String.Empty
            If (chkCustomerType.CheckState = CheckState.Checked) Then
                If (chkVendorType.CheckState = CheckState.Checked) Then
                    str = "Select vnd.VendorID, coaMain.detail_code as [Account Code],coaMain.detail_title as [Account Title],coaDetail.account_type as [Account Type],vnd.Mobile as [Mobile No], vch.Debit_Amount-vch.Credit_Amount as [Balance] FROM tblVendor vnd Left Outer JOIN " & _
                                                           "tblCOAMainSubSubDetail coaMain ON vnd.AccountId=coaMain.coa_detail_id Left Outer Join " & _
                                                           "tblVoucherDetail vch ON vch.coa_detail_id=coaMain.coa_detail_id INNER JOIN " & _
                                                           "tblCOAMainSubSub coaDetail ON coaDetail.main_sub_sub_id=coaMain.main_sub_sub_id " & _
                                                           "WHERE coaDetail.Account_Type='Vendor' UNION ALL "
                End If
                str += "Select cust.CustomerID, coaMain.detail_code as [Account Code],coaMain.detail_title as [Account Title],coaDetail.account_type as [Account Type],cust.Phone as [Mobile No], vch.Debit_Amount-vch.Credit_Amount as Balance FROM tblCustomer cust Left Outer JOIN " & _
                        "tblCOAMainSubSubDetail as coaMain ON cust.AccountId=coaMain.coa_detail_id Left Outer Join " & _
                        "tblVoucherDetail vch ON vch.coa_detail_id=coaMain.coa_detail_id INNER JOIN " & _
                        "tblCOAMainSubSub coaDetail ON coaDetail.main_sub_sub_id=coaMain.main_sub_sub_id " & _
                        "WHERE coaDetail.Account_Type='Customer'"
                dt = GetDataTable(str)
                Me.grdData.DataSource = dt
            Else
                dt = Nothing
                Me.grdData.DataSource = dt

            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkVendorType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVendorType.CheckedChanged
        Try
            Me.grdData.BoundMode = Janus.Windows.GridEX.BoundMode.Bound
            Dim dt As DataTable = Nothing
            Dim str As String = String.Empty
            If (chkVendorType.CheckState = CheckState.Checked) Then
                If (chkCustomerType.CheckState = CheckState.Checked) Then
                    str = "Select cust.CustomerID, coaMain.detail_code as [Account Code],coaMain.detail_title as [Account Title],coaDetail.account_type as [Account Type],cust.Phone as [Mobile No],vch.Debit_Amount-vch.Credit_Amount as [Balance] FROM tblCustomer cust Left Outer JOIN " & _
                                           "tblCOAMainSubSubDetail as coaMain ON cust.AccountId=coaMain.coa_detail_id Left Outer Join " & _
                                           "tblVoucherDetail vch ON vch.coa_detail_id=coaMain.coa_detail_id INNER JOIN " & _
                                           "tblCOAMainSubSub coaDetail ON coaDetail.main_sub_sub_id=coaMain.main_sub_sub_id " & _
                                           "WHERE coaDetail.Account_Type='Customer' UNION ALL "
                End If

                str += "Select vnd.VendorID, coaMain.detail_code as [Account Code],coaMain.detail_title as [Account Title],coaDetail.account_type as [Account Type],vnd.Mobile as [Mobile No], vch.Debit_Amount-vch.Credit_Amount as Balance FROM tblVendor vnd Left Outer JOIN " & _
                                        "tblCOAMainSubSubDetail coaMain ON vnd.AccountId=coaMain.coa_detail_id Left Outer Join " & _
                                        "tblVoucherDetail vch ON vch.coa_detail_id=coaMain.coa_detail_id INNER JOIN " & _
                                        "tblCOAMainSubSub coaDetail ON coaDetail.main_sub_sub_id=coaMain.main_sub_sub_id " & _
                                        "WHERE coaDetail.Account_Type='Vendor'"
                dt = GetDataTable(str)
                Me.grdData.DataSource = dt
            Else
                dt = Nothing
                Me.grdData.DataSource = dt

            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            Reset()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Reset()
        Try
            Me.chkCustomerType.CheckState = CheckState.Unchecked
            Me.chkVendorType.CheckState = CheckState.Unchecked
            'Dim dt As datatabe
            'dt = Nothing
            'Me.grdData.DataSource = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Load checked row/records in Grid
    'Private Sub ShowCheckedRecords()
    '    Try
    '        Me.grdData.BoundMode = Janus.Windows.GridEX.BoundMode.Unbound
    '        'Dim dt As DataTable = GetDataTable("SELECT * FROM tblSMSConfigData WHERE AccoundCode='"&me.grdSaved.GetRow.Cells("Account Code").Value&"'")
    '        Dim dt As DataTable = GetDataTable("SELECT * FROM tblSMSConfigData")

    '        If dt IsNot Nothing Then
    '            For Each dr As DataRow In dt.Rows
    '                Dim datarow As Janus.Windows.GridEX.GridEXRow = Me.grdData.AddItem()
    '                datarow.BeginEdit()
    '                datarow.Cells("Account Code").Value = dr.Item("AccountCode").ToString
    '                datarow.Cells("Account Title").Value = dr.Item("AccountTitle").ToString
    '                datarow.Cells("Account Type").Value = dr.Item("AccountType").ToString
    '                datarow.Cells("Mobile No").Value = dr.Item("MobileNo").ToString
    '                'datarow.Cells("Status").Value = IIf(dr.Item("State").ToString = "Activated", True, False)
    '            Next
    '        End If
    '        'Me.grdData.RootTable.Columns("Account Code")

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If UltraTabPageControl2.Tab.Active Then
                Me.BtnSave.Visible = False
                Me.BtnNew.Visible = False
            Else
                Me.BtnSave.Visible = True
                Me.BtnNew.Visible = True
            End If
            'Me.BtnSave.Visible = False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmScheduleSMS_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        Try
            If Not getConfigValueByType("EnabledBrandedSMS").ToString = "Error" Then
                EnabledBrandedSMS = getConfigValueByType("EnabledBrandedSMS")
            End If

            Timer1.Enabled = True
            'ShowCheckedRecords()
            Reset()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSMSSchedule()
        Try
            Dim dt As DataTable = GetDataTable("SELECT * FROM tblSMSSchedule")

            If EnabledBrandedSMS = True Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("ScheduleDate").ToString = Date.Now Then

                        For Each dr As Janus.Windows.GridEX.GridEXRow In Me.grdSaved.GetRows
                            If dr.Cells("Account Type").Value = "Customer" Then
                                If dr.Cells("Status").Value = "Pending" Then

                                    Dim strMsgBody As String = "Dear Customer Your Balance is:" & dr.Cells("Balance").Value.ToString & ""
                                    If Not IsDBNull(dr.Cells("Mobile No").Value) Then
                                        'Task:2631 Set By Ref Value 
                                        Call SendBrandedSMS("92" & Microsoft.VisualBasic.Right(dr.Cells("Mobile No").Value.ToString.Replace("-", "").Replace(".", "").Replace("+", "").Replace("/", "").Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", ""), 10), strMsgBody)
                                    End If
                                End If

                            ElseIf (dr.Cells("Account Type").Value = "Vendor") Then
                                If dr.Cells("Status").Value = "Pending" Then
                                    Dim strMsgBody2 As String = "Dear Vendor Your Balance is:" & dr.Cells("Balance").Value.ToString & ""
                                    If Not IsDBNull(dr.Cells("Mobile No").Value) Then
                                        'Task:2631 Set By Ref Value 
                                        Call SendBrandedSMS("92" & Microsoft.VisualBasic.Right(dr.Cells("Mobile No").Value.ToString.Replace("-", "").Replace(".", "").Replace("+", "").Replace("/", "").Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", ""), 10), strMsgBody2)
                                    End If
                                End If
                            End If
                        Next

                    End If
                Next
                'end of Schedule Date loop
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            '5 Minute interval
            Timer1.Interval = 300000
            If BackgroundWorker1.IsBusy Then Exit Sub
            BackgroundWorker1.RunWorkerAsync()
            Do While BackgroundWorker1.IsBusy
                Application.DoEvents()
            Loop
        Catch ex As Exception
        Finally
            Me.Timer1.Enabled = True
        End Try
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Me.Timer1.Enabled = False
            GetSMSSchedule()
        Catch ex As Exception
        Finally
            Me.Timer1.Enabled = True
        End Try
    End Sub

    'Load records in Hisotry Tab-Grid 
    Private Sub ShowHistoryGridRecord()
        Try
            Dim dt As DataTable = Nothing
            For Each dr As Janus.Windows.GridEX.GridEXRow In Me.grdData.GetRows
                If (Convert.ToBoolean(dr.Cells("StatusKey").Value)) = True Then
                    Dim drr As DataRow
                    drr = dt.NewRow
                    drr.Item("Account Code").Value = dr.Cells("Account Code").Value.ToString
                    drr.Item("Account Title").Value = dr.Cells("Account Title").Value.ToString
                    drr.Item("Account Type").Value = dr.Cells("Account Type").Value.ToString
                    drr.Item("Mobile No").Value = dr.Cells("Mobile No").Value.ToString
                    drr.Item("Status").Value = "Pending"
                    drr.Item("Balance").Value = dr.Cells("Balance").Value.ToString
                    dt.Rows.Add(drr)
                    'Dim datarow As Janus.Windows.GridEX.GridEXRow = grdSaved.AddItem()
                    'datarow.BeginEdit()
                    'datarow.Cells("Account Code").Value = dr.Cells("Account Code").Value.ToString
                    'datarow.Cells("Account Title").Value = dr.Cells("Account Title").Value.ToString
                    'datarow.Cells("Account Type").Value = dr.Cells("Account Type").Value.ToString
                    'datarow.Cells("Mobile No").Value = dr.Cells("Mobile No").Value.ToString
                    'datarow.Cells("Status").Value = "Pending"
                    'datarow.Cells("Balance").Value = dr.Cells("Balance").Value.ToString

                End If
            Next
            Me.grdSaved.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            ShowHistoryGridRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
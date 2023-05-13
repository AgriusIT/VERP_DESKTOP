'20-Aug-2015 Task#20082015 Ahmad Sharif: Verfiy , if any record exist against the location then stop to delete the location
''TASK TFS4691 Filter should be applied to Restricted Grid in both cases whether Restricted Item Check is checked or not. Done by Muhammad Amin on 11-10-2018

Imports System.Data.OleDb
Public Class FrmLocation
    Enum great
        location_id = 0
        location_code = 1
        location_name = 2
        location_address = 3
        location_phone = 4
        location_fax = 5
        location_url = 6
        Type = 7
        comments = 8
        Restricted = 9
        sort_order = 10
        Mobile_No = 11
        AllowMinusStock
        Active
    End Enum
    Dim RestrictedItemByLocation As SBModel.RestrictedItemsByLocation
    Dim IsLoadedForm As Boolean = False
    Private Sub GridEX1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridEX1.DoubleClick
        'Try
        '    Me.txtlocationid.Text = GridEX1.GetRow.Cells(great.location_id).Text
        '    Me.txtlocationcode.Text = GridEX1.GetRow.Cells(great.location_code).Text
        '    Me.txtlocationName.Text = GridEX1.GetRow.Cells(great.location_name).Text
        '    Me.txtComments.Text = GridEX1.GetRow.Cells(great.comments).Text
        '    Me.txtsortOrder.Text = GridEX1.GetRow.Cells(great.sort_order).Text
        '    Me.txtlocationAddress.Text = GridEX1.GetRow.Cells(great.location_address).Text
        '    Me.txtlocationPhone.Text = GridEX1.GetRow.Cells(great.location_phone).Text
        '    Me.txtlocationFax.Text = GridEX1.GetRow.Cells(great.location_fax).Text
        '    Me.cmbType.Text = GridEX1.GetRow.Cells(great.Type).Text
        '    Me.txtlocationUrl.Text = GridEX1.GetRow.Cells(great.location_url).Text
        '    Me.chkRestrictedItems.Checked = GridEX1.GetRow.Cells(great.Restricted).Value
        '    Me.txtMobileNo.Text = GridEX1.GetRow.Cells(great.Mobile_No).Value.ToString
        '    Me.chkAllowMinusStock.Checked = Me.GridEX1.GetRow.Cells(great.AllowMinusStock).Value
        '    Me.cbActive.Checked = Me.GridEX1.GetRow.Cells(great.Active).Value
        '    BtnSave.Text = "&Update"
        '    GetSecurityRights()
        '    UltraTabControl1_SelectedTabChanging(Nothing, Nothing)
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)

        'End Try
    End Sub
    Sub resetcontrol()
        txtlocationid.Text = ""
        txtlocationcode.Text = ""
        txtlocationName.Text = ""
        txtlocationAddress.Text = ""
        txtlocationPhone.Text = ""
        txtlocationFax.Text = ""
        txtComments.Text = ""
        txtsortOrder.Text = 1
        txtlocationUrl.Text = ""
        Me.cmbType.Text = String.Empty
        Me.txtMobileNo.Text = String.Empty
        Me.chkAllowMinusStock.Checked = False
        Me.cbActive.Checked = False
        BtnSave.Text = "&Save"
        GetSecurityRights()
    End Sub
    Sub GetAllRecorced()
        Try

      
            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            Dim strSQL As String = "SELECT location_id [Location Id], location_code[Location Code], location_name [Location Name], location_address [Address], location_phone [Phone], location_fax[FAX], location_url [URL], location_type [Type], comments [Comments], isnull(RestrictedItems,0) as Restricted, Sort_Order, Mobile_No, IsNull(AllowMinusStock,0) as AllowMinusStock, ISNULL(Active, 0) As Active " _
                                              & " FROM tblDefLocation "
            adp = New OleDbDataAdapter(strSQL, Con)
            adp.Fill(dt)

            GridEX1.DataSource = dt
            GridEX1.RetrieveStructure()
            ApplyGridSettings()


           
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub ApplyGridSettings()
        Me.GridEX1.RootTable.Columns(great.location_id).Visible = False
        Me.GridEX1.RootTable.Columns(great.Mobile_No).Visible = False
        Me.GridEX1.AutoSizeColumns()
    End Sub

    Private Sub FrmLocation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.F4 Then
                If Me.BtnSave.Enabled = True Then
                    btnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If
            If e.KeyCode = Keys.D And e.Alt Then
                If Me.BtnDelete.Enabled = False Then
                    RemoveHandler Me.BtnDelete.Click, AddressOf btnDelete_Click
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub
    Private Sub frmLocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            '----- Loading Locations ----------
            GetAllRecorced()
            IsLoadedForm = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        resetcontrol()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Sub UpdateRecord()
        Try
            If khyber() = True Then
                'If MsgBox("Are you sure you want to Update?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                If msg_Confirm(str_ConfirmUpdate) = True Then
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Dim cm As New OleDb.OleDbCommand
                    Dim cn As New OleDb.OleDbCommand(Con.ConnectionString)
                    cm.CommandText = ""
                    'cm.CommandText = " update tblDefLocation set location_code= N'" & txtlocationcode.Text & "',location_Name= N'" & txtlocationName.Text & "',location_address=N'" & txtlocationAddress.Text & "',location_phone= N'" & txtlocationPhone.Text & "',location_fax= N'" & txtlocationFax.Text & "',comments=N'" & txtComments.Text & "',sort_order= N'" & txtsortOrder.Text & "',location_Url= N'" & txtlocationUrl.Text & "',location_type= N'" & cmbType.Text & "', RestrictedItems=" & IIf(Me.chkRestrictedItems.Checked = True, 1, 0) & " where location_id = " & txtlocationid.Text
                    'cm.CommandText = " update tblDefLocation set location_code= N'" & txtlocationcode.Text & "',location_Name= N'" & txtlocationName.Text & "',location_address=N'" & txtlocationAddress.Text & "',location_phone= N'" & txtlocationPhone.Text & "',location_fax= N'" & txtlocationFax.Text & "',comments=N'" & txtComments.Text & "',sort_order= N'" & txtsortOrder.Text & "',location_Url= N'" & txtlocationUrl.Text & "',location_type= N'" & cmbType.Text & "', RestrictedItems=" & IIf(Me.chkRestrictedItems.Checked = True, 1, 0) & ", Mobile_No=N'" & Me.txtMobileNo.Text.Replace("'", "''") & "' where location_id = " & txtlocationid.Text
                    cm.CommandText = " update tblDefLocation set location_code= N'" & txtlocationcode.Text & "',location_Name= N'" & txtlocationName.Text & "',location_address=N'" & txtlocationAddress.Text & "',location_phone= N'" & txtlocationPhone.Text & "',location_fax= N'" & txtlocationFax.Text & "',comments=N'" & txtComments.Text & "',sort_order= N'" & txtsortOrder.Text & "',location_Url= N'" & txtlocationUrl.Text & "',location_type= N'" & cmbType.Text & "', RestrictedItems=" & IIf(Me.chkRestrictedItems.Checked = True, 1, 0) & ", Mobile_No=N'" & Me.txtMobileNo.Text.Replace("'", "''") & "', AllowMinusStock=" & IIf(Me.chkAllowMinusStock.Checked = True, 1, 0) & ", Active=" & IIf(Me.cbActive.Checked = True, 1, 0) & " where location_id = " & Val(txtlocationid.Text)
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    'Fill Model
                    cm.ExecuteNonQuery()
                    Dim RestrictedItemByLocation_List As List(Of SBModel.RestrictedItemsByLocation)
                    RestrictedItemByLocation_List = New List(Of SBModel.RestrictedItemsByLocation)
                    Me.GridEX2.UpdateData()
                    Dim dt1 As DataTable = Me.GridEX2.DataSource
                    For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.GridEX2.GetRows
                        'For Each grdRow As Janus.Windows.GridEX.GridEXRow In Me.GridEX2.GetCheckedRows

                        RestrictedItemByLocation = New SBModel.RestrictedItemsByLocation
                        RestrictedItemByLocation.LocationId = Me.txtlocationid.Text
                        RestrictedItemByLocation.ArticleDefId = grdRow.Cells("ArticleId").Value
                        RestrictedItemByLocation.Restricted = grdRow.Cells("Restricted").Value
                        RestrictedItemByLocation.UserName = LoginUserName
                        RestrictedItemByLocation.EntryDate = Date.Now
                        RestrictedItemByLocation_List.Add(RestrictedItemByLocation)
                    Next
                    Call New SBDal.RestrictedItemByLocationDAL().Add(RestrictedItemByLocation_List, Convert.ToInt32(Me.txtlocationid.Text))
                    'If Con.State = ConnectionState.Open Then Con.Close()
                    'Con.Open()
                    'cm.ExecuteNonQuery()
                    'Con.Close()
                    'msg_Information(str_informUpdate)
                    resetcontrol()
                    GetAllRecorced()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Sub SaveRecord()
        Try
            If khyber() = True Then
                'If MsgBox("Are you sure you want to Save?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                ' If msg_Confirm(str_ConfirmSave) = True Then
                'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()

                Dim cm As New OleDbCommand
                'cm.CommandText = "insert into tblDefLocation(location_code,location_name,location_address,location_phone,location_fax,comments,sort_order,location_url,location_type, RestrictedItems) values(N'" & txtlocationcode.Text & "',N'" & txtlocationName.Text & "',N'" & txtlocationAddress.Text & "',N'" & txtlocationPhone.Text & "',N'" & txtlocationFax.Text & "',N'" & txtComments.Text & "',N'" & txtsortOrder.Text & "',N'" & txtlocationUrl.Text & "',N'" & cmbType.Text & "', " & IIf(Me.chkRestrictedItems.Checked = True, 1, 0) & ")"
                'cm.CommandText = "insert into tblDefLocation(location_code,location_name,location_address,location_phone,location_fax,comments,sort_order,location_url,location_type, RestrictedItems, Mobile_No) values(N'" & txtlocationcode.Text & "',N'" & txtlocationName.Text & "',N'" & txtlocationAddress.Text & "',N'" & txtlocationPhone.Text & "',N'" & txtlocationFax.Text & "',N'" & txtComments.Text & "',N'" & txtsortOrder.Text & "',N'" & txtlocationUrl.Text & "',N'" & cmbType.Text & "', " & IIf(Me.chkRestrictedItems.Checked = True, 1, 0) & ",N'" & Me.txtMobileNo.Text.Replace("'", "''") & "')"
                cm.CommandText = "insert into tblDefLocation(location_code,location_name,location_address,location_phone,location_fax,comments,sort_order,location_url,location_type, RestrictedItems, Mobile_No,AllowMinusStock, Active) values(N'" & txtlocationcode.Text & "',N'" & txtlocationName.Text & "',N'" & txtlocationAddress.Text & "',N'" & txtlocationPhone.Text & "',N'" & txtlocationFax.Text & "',N'" & txtComments.Text & "',N'" & txtsortOrder.Text & "',N'" & txtlocationUrl.Text & "',N'" & cmbType.Text & "', " & IIf(Me.chkRestrictedItems.Checked = True, 1, 0) & ",N'" & Me.txtMobileNo.Text.Replace("'", "''") & "'," & IIf(Me.chkAllowMinusStock.Checked = True, 1, 0) & " ," & IIf(Me.cbActive.Checked = True, 1, 0) & ")"
                If Con.State = ConnectionState.Closed Then Con.Open()
                cm.Connection = Con
                'Con.Open()
                cm.ExecuteNonQuery()
                Con.Close()
                'msg_Information(str_informSave)
                resetcontrol()
                GetAllRecorced()
            End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Function khyber() As Boolean
        If txtlocationName.Text.Length = 0 Then
            ShowErrorMessage("please enter the location")
            txtlocationName.Focus()
            Return False
        End If
        Return True
    End Function


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        resetcontrol()
        txtlocationcode.Focus()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            'Task#20082015 Verfiy , if any record exist against the location then stop to delete the location by Ahmad Sharif
            If VerfiyLocationExistInRecords(Me.GridEX1.GetRow.Cells("Location Id").Value) = False Then
                ShowErrorMessage("You can't delete this location, Dependent Record exists against this location")
                Exit Sub
            End If
            'End Task#20082015

            'If MsgBox("Are you sure you want to Delete?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                txtlocationid.Text = Me.GridEX1.GetRow.Cells("Location Id").Value
                'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()

                Dim cm As New OleDb.OleDbCommand
                Dim cn As New OleDb.OleDbCommand(Con.ConnectionString)

                cm.CommandText = ""
                cm.CommandText = "delete from tblDefLocation where location_id = " & txtlocationid.Text
                cm.Connection = Con
                If Con.State = ConnectionState.Open Then Con.Close()
                Con.Open()
                cm.ExecuteNonQuery()

                cm.CommandText = ""
                cm.CommandText = "delete from RestrictedItemByLocationTable where locationid = " & txtlocationid.Text
                cm.Connection = Con
                If Con.State = ConnectionState.Open Then Con.Close()
                Con.Open()
                cm.ExecuteNonQuery()

                Con.Close()
                '  msg_Information(str_informDelete)
                resetcontrol()
                GetAllRecorced()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
            Me.lblProgress.Visible = False
        End Try
    End Sub

    'Task#20082015 Verfiy , if any record exist against the location then stop to delete the location by Ahmad Sharif
    Private Function VerfiyLocationExistInRecords(ByVal locId As Integer) As Boolean
        Dim objCon As New OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDbTransaction = objCon.BeginTransaction()

        Try
            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            Dim strSQL As String = String.Empty
            strSQL = "select LocationId from stockdetailtable where LocationId=" & locId
            cmd.CommandText = strSQL

            Dim loc As Integer = Convert.ToInt32(cmd.ExecuteScalar())

            trans.Commit()

            If loc > 0 Then
                Return False
            End If

            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function
    'End Task#20082015

    'Ali Faisal : Changes made for Save and update location problem on 23-08-2016
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.BtnSave.Enabled = False Then Exit Sub
        Try
            Dim LID2 As String = DecryptLicense(getConfigValueByType("LID2").ToString.Replace("Error", "location3").Replace("''", "location3"))
            If LID2.Contains("Bad Data.") Then
                LID2 = Decrypt(getConfigValueByType("LID2").ToString.Replace("Error", "location3").Replace("''", "location3"))
            End If
            If txtlocationid.Text = String.Empty Then
                If Not LID2 Is Nothing Then
                    If Me.GridEX1.RowCount >= Val(LID2.ToLower.Replace("location", "")) Then
                        ShowErrorMessage("You are not allowd to enter more location")
                        Me.txtlocationcode.Focus()
                        Exit Sub
                    Else
                        SaveRecord()
                    End If
                Else
                    Throw New Exception("some of data is not provided.")
                End If
            Else
                'UpdateRecord()
                'If Me.GridEX1.RowCount > Val(LID2.ToLower.Replace("location", "")) Then
                'ShowErrorMessage("You are not allowd to enter more location")
                'Exit Sub
                'Else
                UpdateRecord()
                Me.txtlocationcode.Focus()
            End If
            'End If
            resetcontrol()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        resetcontrol()
        txtlocationcode.Focus()
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged

    End Sub
    Private Sub UltraTabControl1_SelectedTabChanging(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs) Handles UltraTabControl1.SelectedTabChanging
        Try
            If Me.BtnSave.Text = "&Save" AndAlso e.Tab.Index = 1 Then
                e.Cancel = True
            ElseIf Me.BtnSave.Text <> "&Save" AndAlso Me.chkRestrictedItems.Checked = True Then
                Me.GridEX2.DataSource = Nothing
                Me.GridEX2.DataSource = New SBDal.RestrictedItemByLocationDAL().GetRestrictedItemsByLocationId(Val(Me.txtlocationid.Text))
                Me.GridEX2.RemoveFilters()
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX2.RootTable.Columns
                    If Not col.Key = "Restricted" Then
                        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                        col.FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
                        'col.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.BeginsWith
                        'col.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
                        'col.FilterRowComparison = Janus.Windows.GridEX.ConditionOperator.BeginsWith
                    Else
                        col.EditType = Janus.Windows.GridEX.EditType.CheckBox
                    End If
                Next
            Else
                Me.GridEX2.DataSource = New SBDal.RestrictedItemByLocationDAL().GetRestrictedItemsByLocationId(Val(Me.txtlocationid.Text))
                Me.GridEX2.RemoveFilters()
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.GridEX2.RootTable.Columns
                    If Not col.Key = "Restricted" Then
                        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                        col.FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
                        'col.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.BeginsWith
                        'col.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
                        'col.FilterRowComparison = Janus.Windows.GridEX.ConditionOperator.BeginsWith
                    End If
                Next
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub chkRestrictedItems_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRestrictedItems.CheckedChanged
    '    Try
    '        UltraTabControl1_SelectedTabChanging(UltraTabControl1, Nothing)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        Try
            For Each r As Janus.Windows.GridEX.GridEXRow In Me.GridEX2.GetRows
                r.BeginEdit()
                If Me.chkAll.Checked = True Then
                    r.Cells("Restricted").Value = 1
                Else
                    r.Cells("Restricted").Value = 0
                End If
                r.EndEdit()
            Next
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnNew.Enabled = True
                Me.BtnCancel.Enabled = True
                'Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                Me.BtnDelete.Enabled = False
                Me.BtnNew.Enabled = True
                Me.BtnCancel.Enabled = True
                'Me.BtnPrint.Enabled = False
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
                        'Me.BtnPrint.Enabled = True
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
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From tblDefLocation WHERE Location_Id=N'" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.txtlocationid.Text = dt.Rows(0).Item("Location_Id").ToString
                        Me.txtlocationcode.Text = dt.Rows(0).Item("Location_Code").ToString
                        Me.txtlocationName.Text = dt.Rows(0).Item("Location_Name").ToString
                        Me.txtComments.Text = dt.Rows(0).Item("Comments").ToString
                        Me.txtsortOrder.Text = dt.Rows(0).Item("Sort_Order").ToString
                        Me.txtlocationAddress.Text = dt.Rows(0).Item("Location_Address").ToString
                        Me.txtlocationPhone.Text = dt.Rows(0).Item("location_phone").ToString
                        Me.txtlocationFax.Text = dt.Rows(0).Item("location_fax").ToString
                        Me.cmbType.Text = dt.Rows(0).Item("location_type").ToString
                        Me.txtlocationUrl.Text = dt.Rows(0).Item("location_url").ToString
                        Me.chkRestrictedItems.Checked = dt.Rows(0).Item("RestrictedItems").ToString
                        If Not IsDBNull(dt.Rows(0).Item("AllowMinusStock")) Then
                            Me.chkRestrictedItems.Checked = dt.Rows(0).Item("RestrictedItems").ToString
                        Else
                            Me.chkRestrictedItems.Checked = False
                        End If
                        If Not IsDBNull(dt.Rows(0).Item("Active")) Then
                            Me.cbActive.Checked = dt.Rows(0).Item("Active").ToString
                        Else
                            Me.cbActive.Checked = False
                        End If
                        Me.BtnSave.Text = "&Update"
                        Me.GetSecurityRights()
                        UltraTabControl1_SelectedTabChanging(Nothing, Nothing)
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

    Private Sub GridEX1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14
  

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub


    'added by zainab task1590
    Private Sub GridEX1_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles GridEX1.RowDoubleClick
        Try
            Me.txtlocationid.Text = GridEX1.GetRow.Cells(great.location_id).Text
            Me.txtlocationcode.Text = GridEX1.GetRow.Cells(great.location_code).Text
            Me.txtlocationName.Text = GridEX1.GetRow.Cells(great.location_name).Text
            Me.txtComments.Text = GridEX1.GetRow.Cells(great.comments).Text
            Me.txtsortOrder.Text = GridEX1.GetRow.Cells(great.sort_order).Text
            Me.txtlocationAddress.Text = GridEX1.GetRow.Cells(great.location_address).Text
            Me.txtlocationPhone.Text = GridEX1.GetRow.Cells(great.location_phone).Text
            Me.txtlocationFax.Text = GridEX1.GetRow.Cells(great.location_fax).Text
            Me.cmbType.Text = GridEX1.GetRow.Cells(great.Type).Text
            Me.txtlocationUrl.Text = GridEX1.GetRow.Cells(great.location_url).Text
            Me.chkRestrictedItems.Checked = GridEX1.GetRow.Cells(great.Restricted).Value
            Me.txtMobileNo.Text = GridEX1.GetRow.Cells(great.Mobile_No).Value.ToString
            Me.chkAllowMinusStock.Checked = Me.GridEX1.GetRow.Cells(great.AllowMinusStock).Value
            Me.cbActive.Checked = Me.GridEX1.GetRow.Cells(great.Active).Value
            BtnSave.Text = "&Update"
            GetSecurityRights()
            UltraTabControl1_SelectedTabChanging(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try
    End Sub

    Private Sub GridEX2_ApplyingFilter(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles GridEX2.ApplyingFilter
        Try

        Catch ex As Exception

        End Try
        'Me.GridEX2.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
    End Sub

    Private Sub GridEX2_FilterApplied(sender As Object, e As EventArgs) Handles GridEX2.FilterApplied
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
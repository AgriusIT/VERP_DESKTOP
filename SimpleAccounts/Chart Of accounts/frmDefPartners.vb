'2015-06-19 Ali Ansari Task#201506019 Add New Form Of Partner

Imports System.Drawing.Image
Imports System.Data.oledb
Imports System.Math
Imports System.IO

Public Class frmDefPartners
    Implements IGeneral
    Dim IsLoadedForm As Boolean = False
    
    Enum enmPartner
        PartnerID
        Prefix
        PartnerName
        FatherName
        DOB
        CNIC
        Gender
        MartialStatus
        PhoneNo
        NTN
        Mobile
        Address
        CityID
        CityName
        StateID
        StateName
        StartingDate
        EndingDate
        PartnershipRatio
        Active
        AccountNo
        BankName
    End Enum

    Private Sub frmDefPartners_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                BtnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                btnCancel_Click(Nothing, Nothing)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmDefPartners_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()

            FillCombo()
            FillCombobox("State")
            FillCombobox("Region")
            RefreshControls()
            DisplayRecord()
            IsLoadedForm = True
            ShowHeaderCompany()
            Get_All(frmModProperty.Tags)
            BtnPrint.Visible = False

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False

        End Try
    End Sub

    Private Sub frmDefPartners_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.chkActive.Checked = True
            Me.dtpEndingDate.Checked = False

            FillCombobox("State")
            FillCombobox("City")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Function for filling combo boxes
    Private Sub FillCombobox(Optional ByVal Condition As String = "")
        Try
            If Condition = "State" Then
                FillDropDown(Me.cmbState, "select * from tblListState")
            ElseIf Condition = "City" Then
                FillDropDown(Me.ddlCity, "select * from tblListCity")
            End If
        Catch ex As Exception
            Throw ex
        End Try


    End Sub
    ' Tasks# 201506019 Displaying Saved Record Ali Ansari
    Private Sub DisplayRecord()
        Dim str As String = String.Empty
        Try
            str = "SELECT a.PartnerID,a.prefix, a.PartnerName, a.FatherName,a.DOB,a.CNIC,a.Gender,a.MartialStatus, " _
                         & "  a.PhoneNo, a.MobileNo,a.NTN,a.Address,a.CityID, b.CityName,isnull(a.stateid,0) as StateId,c.statename, " _
                         & " a.CommencingDate,a.EndingDate,a.PartnershipRatio,a.AccountNo,a.BankName,a.accountcode,a.Active " _
                         & " FROM dbo.tblDefPartner a LEFT  JOIN " _
                         & " dbo.tblListCity B ON a.CityID = b.CityId " _
                         & " left join dbo.tblListState c on A.stateid = C.stateid "

            FillGridEx(grdSaved, str)
            Me.grdSaved.RetrieveStructure()
            If Not grdSaved.RowCount > 0 Then Exit Sub
            grdSaved.RootTable.Columns("PartnerID").Visible = False
            grdSaved.RootTable.Columns("CityID").Visible = False
            grdSaved.RootTable.Columns("StateID").Visible = False
            grdSaved.RootTable.Columns("AccountCode").Visible = False
            grdSaved.RootTable.Columns("PartnerName").Caption = "Name"
            grdSaved.RootTable.Columns("FatherName").Caption = "Father Name"
            grdSaved.RootTable.Columns("CityName").Caption = "City"
            grdSaved.RootTable.Columns("StateName").Caption = "State"
            grdSaved.RootTable.Columns("PartnershipRatio").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            grdSaved.RootTable.Columns("PartnershipRatio").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grdSaved.RootTable.Columns("PartnershipRatio").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            grdSaved.RootTable.Columns("PartnershipRatio").FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns("CommencingDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("EndingDate").FormatString = str_DisplayDateFormat
            Me.grdSaved.RootTable.Columns("DOB").FormatString = str_DisplayDateFormat

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ' Tasks# 201506019 Displaying Saved Record Ali Ansari
    '' Tasks# 201506019 Refresh Controls Ali Ansari
    Private Sub RefreshControls()
        Try
            Me.BtnSave.Text = "&Save"
            txtPartnerId.Text = String.Empty
            txtName.Text = ""
            txtFather.Text = ""
            txtNIC.Text = ""
            txtNTN.Text = ""
            txtAddress.Text = ""
            txtPhone.Text = ""
            txtMobile.Text = ""
            ddlGender.SelectedIndex = 0
            ddlCity.SelectedIndex = 0
            ddlMaritalStatus.SelectedIndex = 0
            chkActive.Checked = True
            dtpDOB.Value = Date.Today
            DtpStartingDate.Value = Date.Today 'Format(Now, "dd/MM/yyyy")
            dtpEndingDate.Value = Date.Today 'Format(Now, "dd/MM/yyyy")
            dtpEndingDate.Checked = False
            TxtBankAccountNo.Text = String.Empty
            TxtBankName.Text = String.Empty
            TxtPartnerPercentage.Text = String.Empty
            Me.txtName.Focus()
            Me.TABHISTORY.SelectedTab = Me.TABHISTORY.Tabs(0).TabPage.Tab
            If Not Me.CmbPrefix.SelectedIndex = -1 Then Me.CmbPrefix.SelectedIndex = 0 ' Reseting Prefix
            If Not Me.cmbState.SelectedIndex = -1 Then Me.CmbPrefix.SelectedIndex = 0 ' Reseting State
            Me.cmbState.SelectedIndex = 0           'Task#1 13-Jun-2015 resetting State
            GetSecurityRights()
            DisplayRecord()
            BtnPrint.Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '' Tasks# 201506019 Refresh Controls Ali Ansari
    'Save & Update Records in Chart of Account And TblDefPartner Table
    Private Function Save() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        objCon = Con
        If objCon.State = ConnectionState.Open Then objCon.Close()


        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Dim identity As Integer
        Try

            objCommand.CommandType = CommandType.Text

            objCommand.Transaction = trans

            Dim AccountCode As String = String.Empty

            Dim dt As New DataTable
            Dim da As New OleDbDataAdapter
            objCommand.CommandText = "Select main_code,sub_code,sub_sub_code,detail_code,sub_sub_code + '-' + RIGHT('00000' + CAST(max(RIGHT(detail_code, 5))  + 1 as varchar), 5) as DetailCodeMax from vwCOADetail WHERE coa_detail_id in(Select Max(coa_detail_id) as coa_detail_id From tblCOAMainSubSubDetail WHERE main_sub_sub_id in(Select main_sub_sub_Id From tblCOAMainSubSub WHERE Main_Sub_Id in(Select main_sub_id from tblCOAMainSub WHERE coa_main_id in(Select coa_main_id From tblCOAMain where main_type='Capital') ))) group by main_code,sub_code,sub_sub_code,detail_code "
            da.SelectCommand = objCommand
            da.Fill(dt)
            dt.AcceptChanges()
            If dt IsNot Nothing Then
                If dt.Rows.Count = 1 Then
                    AccountCode = Trim(dt.Rows(0).Item(4).ToString)
                    objCommand.CommandText = "INSERT INTO tblCOAMainSubSubDetail(main_sub_sub_id, detail_code, detail_title,active) Values ('" & Val(dt.Rows(0).Item(0).ToString) & "', '" & Trim(dt.Rows(0).Item(4).ToString) & "', '" & Me.txtName.Text.Replace("'", "''") & "',1)"

                    objCommand.ExecuteNonQuery()
                Else
                    ShowErrorMessage("Kindly add atleast one(1) account in capital")
                    Save = False
                    Exit Function
                End If
            End If


            objCommand.CommandText = "Insert into tblDefPartner (Prefix,PartnerName,FatherName, DOB," _
                                            & " CNIC, Gender,MartialStatus, NTN,PhoneNo, MobileNo,Address,CityID,   " _
                                            & "Stateid,CommencingDate,EndingDate,PartnershipRatio,Active,AccountNo,BankName,accountcode ) values( " _
                                            & " '" & CmbPrefix.Text & "',N'" & txtName.Text.Replace("'", "''") & "',N'" & txtFather.Text.Replace("'", "''") & "'," _
                                            & " N'" & dtpDOB.Value.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & txtNIC.Text & "',N'" & ddlGender.SelectedItem.ToString & "'," _
                                            & " N'" & ddlMaritalStatus.SelectedItem.ToString & "',N'" & txtNTN.Text & "', N'" & txtPhone.Text.Replace("'", "''") & "'," _
                                            & " N'" & txtMobile.Text.Replace("'", "''") & "',N'" & txtAddress.Text.Replace("'", "''") & "'," & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ", " _
                                            & " " & IIf(cmbState.SelectedIndex = 0, "Null", cmbState.SelectedValue) & ", N'" & DtpStartingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," _
                                            & "  N'" & dtpEndingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & Val(TxtPartnerPercentage.Text) & " ," & Abs(Val(chkActive.Checked)) & ",N'" & TxtBankAccountNo.Text.Replace("'", "''") & "',N'" & TxtBankName.Text.Replace("'", "''") & "','" & AccountCode & "') Select @@Identity"



            identity = Convert.ToInt32(objCommand.ExecuteScalar())
            trans.Commit()

        Catch ex As Exception
            trans.Rollback()
            Save = False
        End Try

        Save = True
        Try

            SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, identity, True)
        Catch ex As Exception

        End Try

    End Function
    'Save & Update Records in Chart of Account And TblDefPartner Table
    'Task# 201506019 Fill Combo Boxes of City & State
    Private Sub FillCombo(Optional ByVal strCondition As String = "")

        Dim str As String = String.Empty
        Try
            If strCondition = String.Empty Then

                str = String.Empty
                str = "Select CityID, CityName from tblListCity"
                FillDropDown(ddlCity, str)


                'State
                str = String.Empty
                str = "select stateid,statename from tblliststate where active = 1 order by sortorder"
                FillDropDown(cmbState, str, True)



            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Task# 201506019 Fill Combo Boxes of City & State

    'Task# 201506019 Validate Name field and Total Ratio Ali Ansari
    Private Function FormValidate() As Boolean
        Try
            Dim Dt As DataTable
            Dim ParnerRatio As Integer = 0I
            Dim id As Integer = 0I
            If txtName.Text = "" Then
                msg_Error("Please Enter Partner Name")
                txtName.Focus() : FormValidate = False : Exit Function
            End If


            If Val(TxtPartnerPercentage.Text) = 0 Then
                msg_Error("Please select partner percentage")
                TxtPartnerPercentage.Focus() : FormValidate = False : Exit Function
            End If


            If Len(txtNIC.Text) = 0 Then
                msg_Error("Please enter NIC number")
                txtNIC.Focus() : FormValidate = False : Exit Function
            End If

            Dt = GetDataTable("select isnull(sum(PartnershipRatio),0)  as PartnerRatio from  tbldefpartner where partnerid <> " & Val(txtPartnerId.Text) & " ")
            Dt.AcceptChanges()
            If Dt.Rows.Count > 0 Then
                ParnerRatio = Dt.Rows(0).Item(0).ToString
                ParnerRatio = ParnerRatio + Val(TxtPartnerPercentage.Text)
                If ParnerRatio > 100 Then
                    ShowErrorMessage("Partners ratio exceeds 100%")
                    TxtPartnerPercentage.Focus() : FormValidate = False : Exit Function
                    Exit Function
                End If
            End If


            Dt = Nothing
            Dt = GetDataTable("Select Count(*) From tbldefpartner WHERE CNIC = " & Val(txtNIC.Text) & "")
            Dt.AcceptChanges()
            If dt IsNot Nothing Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    ShowErrorMessage("Partner with same NIC no already exists. ")
                    Exit Function
                End If
            End If
            Return True



        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Task# 201506019 Validate Name field and Total Ratio Ali Ansari

    'Task# 201506019 Update Records Ali Ansari
    Private Function Update_Record() As Boolean

        Dim objCommand As New OleDbCommand
        Dim objCon As OleDbConnection
        objCon = Con 'New oledbConnection("Password=sa;Integrated Security Info=False;User ID=sa;Password=lumensoft2003; Initial Catalog=SimplePOS;Data Source=MKhalid")
        If objCon.State = ConnectionState.Open Then objCon.Close()

        objCon.Open()
        objCommand.Connection = objCon

        Dim trans As OleDbTransaction = objCon.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text

            objCommand.Transaction = trans


            objCommand.CommandText = "Update tblDefPartner set PartnerName=N'" & txtName.Text.Replace("'", "''") & "', FatherName=N'" & txtFather.Text.Replace("'", "''") & "', " _
                              & " CNIC=N'" & txtNIC.Text & "', NTN=N'" & txtNTN.Text & "', Gender=N'" & ddlGender.SelectedItem.ToString & "', " _
                              & " MartialStatus=N'" & ddlMaritalStatus.SelectedItem.ToString & "',  DOB=N'" & dtpDOB.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                              & " CityID=" & IIf(ddlCity.SelectedIndex = 0, "Null", ddlCity.SelectedValue) & ", Address=N'" & txtAddress.Text.Replace("'", "''") & "', " _
                              & "PhoneNo=N'" & txtPhone.Text.Replace("'", "''") & "', MobileNo=N'" & txtMobile.Text & "',  " _
                              & " CommencingDate=N'" & DtpStartingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', EndingDate = " & IIf(Me.dtpEndingDate.Checked = False, "NULL", "N'" & dtpEndingDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'") & "," _
                              & " Active=" & Abs(Val(chkActive.Checked)) & ", " _
                              & " Prefix = '" & CmbPrefix.Text & "',stateid = " & IIf(cmbState.SelectedIndex = 0, "Null", cmbState.SelectedValue) & "," _
                              & " PartnershipRatio = " & Val(TxtPartnerPercentage.Text) & ",AccountNo = N'" & TxtBankAccountNo.Text.Replace("'", "''") & "',BankName = N'" & TxtBankName.Text.Replace("'", "''") & "'  Where PartnerID= " & txtPartnerId.Text & ""

            objCommand.ExecuteNonQuery()
            objCommand.CommandText = ""
            objCommand.CommandText = "Update tblCOAMainSubSubDetail set detail_title = N'" & txtName.Text.Replace("'", "''") & "' where detail_code = '" & grdSaved.CurrentRow.Cells("AccountCode").Value.ToString & "' "
            objCommand.ExecuteNonQuery()
            trans.Commit()
            Update_Record = True
            Try
                'SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, txtPartnerId.Text, True)
            Catch ex As Exception

            End Try

        Catch ex As Exception
            trans.Rollback()
            Update_Record = False
        End Try


    End Function
    'Task# 201506019 Update Records Ali Ansari
    'Task# 201506019 Transfering Record From Grid  Ali Ansari
    Sub EditRecord()
        Try

            txtPartnerId.Text = grdSaved.CurrentRow.Cells("PartnerID").Value.ToString
            txtName.Text = grdSaved.CurrentRow.Cells("PartnerName").Value.ToString
            txtFather.Text = grdSaved.CurrentRow.Cells("FatherName").Value.ToString
            txtNIC.Text = grdSaved.CurrentRow.Cells("CNIC").Value.ToString
            txtNTN.Text = grdSaved.CurrentRow.Cells("NTN").Value.ToString
            ddlGender.SelectedIndex = ddlGender.FindStringExact((grdSaved.CurrentRow.Cells("Gender").Value.ToString)) 'grdSaved.currentrow.Cells(8).Value & ""
            ddlMaritalStatus.SelectedIndex = ddlMaritalStatus.FindStringExact((grdSaved.CurrentRow.Cells("MartialStatus").Value.ToString))
            dtpDOB.Value = grdSaved.CurrentRow.Cells("DOB").Value & ""
            If grdSaved.CurrentRow.Cells("CityID").Value & "" <> "" Then
                ddlCity.SelectedValue = Val(grdSaved.CurrentRow.Cells("CityID").Value)
            Else
                ddlCity.SelectedIndex = 0
            End If

            txtAddress.Text = grdSaved.CurrentRow.Cells("Address").Value.ToString
            txtPhone.Text = grdSaved.CurrentRow.Cells("PhoneNo").Value.ToString
            txtMobile.Text = grdSaved.CurrentRow.Cells("MobileNo").Value.ToString
            TxtBankAccountNo.Text = grdSaved.CurrentRow.Cells("AccountNo").Value.ToString
            TxtBankName.Text = grdSaved.CurrentRow.Cells("BankName").Value.ToString
            TxtPartnerPercentage.Text = grdSaved.CurrentRow.Cells("PartnershipRatio").Value.ToString

            If grdSaved.CurrentRow.Cells("CommencingDate").Value & "" <> "" Then
                DtpStartingDate.Value = grdSaved.CurrentRow.Cells("CommencingDate").Value & ""
            End If


            If IsDBNull(Me.grdSaved.GetRow.Cells("EndingDate").Value) Then
                Me.dtpEndingDate.Value = Now
                Me.dtpEndingDate.Checked = False
            Else

                Me.dtpEndingDate.Value = Me.grdSaved.GetRow.Cells("EndingDate").Value
                Me.dtpEndingDate.Checked = True
            End If

            Me.CmbPrefix.Text = Me.grdSaved.GetRow.Cells("Prefix").Value.ToString
            If grdSaved.CurrentRow.Cells("StateID").Value & "" <> "" Then
                cmbState.SelectedValue = Val(grdSaved.CurrentRow.Cells("StateID").Value)
            Else
                cmbState.SelectedIndex = 0
            End If

            If grdSaved.CurrentRow.Cells("CityID").Value & "" <> "" Then
                ddlCity.SelectedValue = Val(grdSaved.CurrentRow.Cells("CityID").Value)
            Else
                cmbState.SelectedIndex = 0
            End If

            If Not IsDBNull(grdSaved.CurrentRow.Cells("Active").Value) Then
                chkActive.Checked = grdSaved.CurrentRow.Cells("Active").Value
            Else
                chkActive.Checked = False
            End If
            BtnSave.Text = "&Update"


            GetSecurityRights()
            Me.TABHISTORY.SelectedTab = Me.TABHISTORY.Tabs(0).TabPage.Tab

        Catch ex As Exception
            msg_Error("An error occured while opening record: " & ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            Me.RefreshControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            If Not Me.grdSaved.GetRow Is Nothing Then
                Me.EditRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click

        If Not grdSaved.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If

        If msg_Confirm(str_ConfirmDelete) = True Then
            Try


                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()

                Dim cm As New OleDbCommand

                If Con.State = ConnectionState.Closed Then Con.Open()
                cm.Connection = Con
                cm.CommandText = "delete from tbldefpartner where partnerid=" & Me.grdSaved.CurrentRow.Cells("PartnerId").Value.ToString
                cm.ExecuteNonQuery()


                cm.CommandText = ""
                cm.CommandText = "delete from tblCOAMainSubSubDetail  where detail_code = '" & grdSaved.CurrentRow.Cells("AccountCode").Value.ToString & "' "
                cm.ExecuteNonQuery()

            Catch ex As Exception
                msg_Error("Error occured while deleting record: " & ex.Message)
            Finally
                Con.Close()
                Me.lblProgress.Visible = False
            End Try
            Me.RefreshControls()


        End If
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                'Exit Sub
                'End If
            Else
                If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                    Dim dt As DataTable = GetFormRights(EnumForms.frmDefEmployee)
                    If Not dt Is Nothing Then
                        If Not dt.Rows.Count = 0 Then
                            If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                                Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                            Else
                                Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                            End If
                            Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                            Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                            Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                        End If
                    End If
                Else
                    'Me.Visible = False
                    Me.BtnSave.Enabled = False
                    Me.BtnDelete.Enabled = False
                    Me.BtnPrint.Enabled = False
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
                            Me.BtnPrint.Enabled = True
                            'CtrlGrdBar1.mGridPrint.Enabled = True
                            'ElseIf Rights.Item(i).FormControlName = "Export" Then
                            'CtrlGrdBar1.mGridExport.Enabled = True
                            'ElseIf Rights.Item(i).FormControlName = "Post" Then
                            'me.chkPost.Visible = True
                        End If
                    Next
                End If
            End If

            'Task#1 13-Jun-2015 apply security right on tab
            If (Me.BtnSave.Text = "&Save" Or Me.BtnSave.Text = "Save") AndAlso Me.TABHISTORY.SelectedTab.Index = 0 Then
                Me.BtnDelete.Visible = False
                Me.BtnPrint.Visible = False
                Me.BtnSave.Visible = True
            Else
                Me.BtnDelete.Visible = True
                Me.BtnPrint.Visible = True
                If Me.TABHISTORY.SelectedTab.Index = 1 Then
                    Me.BtnSave.Visible = False
                Else
                    Me.BtnSave.Visible = True
                End If
            End If
            'End Task#1 13-Jun-2015

        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            If Me.grdSaved.RowCount <= 0 Then
                Exit Sub
            Else
                Me.EditRecord()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Function Get_All(ByVal Id As String)
        'Try
        '    Get_All = Nothing
        '    If IsLoadedForm = True Then
        '        Dim dt As DataTable = GetDataTable("Select * From tblDefEmployee WHERE Employee_ID=N'" & Id & "'")
        '        If dt IsNot Nothing Then
        '            If dt.Rows.Count > 0 Then
        '                txtEmpID.Text = dt.Rows(0).Item("Employee_Id").ToString
        '                txtName.Text = dt.Rows(0).Item("Employee_Name").ToString
        '                txtCode.Text = dt.Rows(0).Item("Employee_Code").ToString
        '                txtFather.Text = dt.Rows(0).Item("Father_Name").ToString
        '                txtNIC.Text = dt.Rows(0).Item("NIC").ToString
        '                txtNTN.Text = dt.Rows(0).Item("NTN").ToString
        '                ddlGender.SelectedIndex = ddlGender.FindStringExact((dt.Rows(0).Item("Gender").ToString)) 'dt.Rows(0).Item(8).Value & ""
        '                ddlMaritalStatus.SelectedIndex = ddlMaritalStatus.FindStringExact((dt.Rows(0).Item("Martial_Status").ToString))
        '                txtReligion.Text = dt.Rows(0).Item("Religion").ToString
        '                dtpDOB.Value = dt.Rows(0).Item("DOB").ToString
        '                If dt.Rows(0).Item("City_ID") <> "" Then
        '                    ddlCity.SelectedValue = Val(dt.Rows(0).Item("City_ID"))
        '                Else
        '                    ddlCity.SelectedIndex = 0
        '                End If

        '                txtAddress.Text = dt.Rows(0).Item("Address").ToString
        '                txtPhone.Text = dt.Rows(0).Item("Phone").ToString

        '                txtMobile.Text = dt.Rows(0).Item("Mobile").ToString
        '                txtEmail.Text = dt.Rows(0).Item("Email").ToString

        '                dtpJoining.Value = dt.Rows(0).Item("Joining_Date").ToString
        '                If dt.Rows(0).Item("Dept_ID").ToString <> "" Then
        '                    ddlDept.SelectedValue = Val(dt.Rows(0).Item("Dept_ID").ToString)
        '                Else
        '                    ddlDept.SelectedIndex = 0
        '                End If
        '                If dt.Rows(0).Item("Desig_ID").ToString <> "" Then
        '                    ddlDesignation.SelectedValue = Val(dt.Rows(0).Item("Desig_ID").ToString)
        '                Else
        '                    ddlDesignation.SelectedIndex = 0
        '                End If
        '                txtSalary.Text = dt.Rows(0).Item("Salary").ToString
        '                If Not IsDBNull(dt.Rows(0).Item("Active")) Then
        '                    chkActive.Checked = dt.Rows(0).Item("Active").ToString
        '                Else
        '                    chkActive.Checked = False
        '                End If
        '                If dt.Rows(0).Item("Leaving_Date").ToString <> "" Then
        '                    dtpLeaving.Value = dt.Rows(0).Item("Leaving_Date").ToString
        '                End If
        '                txtComments.Text = dt.Rows(0).Item("Comments").ToString
        '                If Not IsDBNull(dt.Rows(0).Item("SalePerson").ToString) Then
        '                    chkSalePerson.Checked = dt.Rows(0).Item("SalePerson").ToString
        '                Else
        '                    chkSalePerson.Checked = False
        '                End If
        '                If Not IsDBNull(dt.Rows(0).Item("Sale_Order_Person").ToString) Then
        '                    Me.chkSaleOrderPerson.Checked = dt.Rows(0).Item("Sale_Order_Person").ToString
        '                Else
        '                    chkSaleOrderPerson.Checked = False
        '                End If
        '                If Not IsDBNull(dt.Rows(0).Item("AlternateEmpNo").ToString) Then
        '                    Me.txtAlternateEmpNo.Text = dt.Rows(0).Item("AlternateEmpNo")
        '                Else
        '                    Me.txtAlternateEmpNo.Text = String.Empty
        '                End If
        '                EmpSalaryAccountId = dt.Rows(0).Item("EmpSalaryAccountId").Value
        '                'Me.txtAccountDescription.Text = dt.Rows(0).Item(enmEmployee.EmpAccountDesc).Text
        '                Me.txtrefrance.Text = dt.Rows(0).Item("Reference").ToString
        '                Me.txtpessiNo.Text = dt.Rows(0).Item("PessiNo").ToString
        '                Me.txtEobiNo.Text = dt.Rows(0).Item("EobiNo").ToString
        '                Me.cmbShiftGroup.SelectedValue = dt.Rows(0).Item("ShiftGroupId").ToString
        '                EmpReceiveableAccountId = dt.Rows(0).Item("ReceiveableAccountId").ToString
        '                'Me.txtReceiveableAccount.Text = dt.Rows(0).Item("ReceiveableAccountDesc").ToString
        '                If Not IsDBNull(dt.Rows(0).Item("EmpPicture").ToString) Then
        '                    If IO.File.Exists(dt.Rows(0).Item("EmpPicture").ToString) Then
        '                        Try
        '                            Dim fs As FileStream = File.OpenRead(dt.Rows(0).Item("EmpPicture").ToString)
        '                            Me.pbemployee.Image = Image.FromStream(fs, False, False)
        '                        Catch ex As Exception

        '                        End Try
        '                    Else
        '                        Me.pbemployee.Image = Nothing
        '                    End If
        '                Else
        '                    Me.pbemployee.Image = Nothing
        '                End If

        '                txtFamilyCode.Text = dt.Rows(0).Item("Family_Code").ToString
        '                Me.txtIdRemark.Text = dt.Rows(0).Item("ID_Remark").ToString
        '                Me.cmbQualification.Text = dt.Rows(0).Item("Qualification").ToString
        '                Me.cmbBloodGroup.Text = dt.Rows(0).Item("Blood_Group").ToString
        '                Me.cmbLanguage.Text = dt.Rows(0).Item("Language").ToString
        '                Me.txtSocialSecurityNo.Text = dt.Rows(0).Item("Social_Security_No").ToString
        '                Me.txtInsuranceNo.Text = dt.Rows(0).Item("Insurance_No").ToString
        '                Me.txtEmergencyNo.Text = dt.Rows(0).Item("Emergency_No").ToString
        '                Me.txtPassportNo.Text = dt.Rows(0).Item("Passport_No").ToString
        '                Me.txtBankAccountNo.Text = dt.Rows(0).Item("BankAccount_No").ToString
        '                Me.txtNicPlace.Text = dt.Rows(0).Item("NIC_Place").ToString
        '                Me.cmbDomicile.Text = dt.Rows(0).Item("Domicile").ToString
        '                Me.cmbRelation.Text = dt.Rows(0).Item("Relation").ToString
        '                Me.txtReplacementNewCode.Text = dt.Rows(0).Item("InReplacementNewCode").ToString
        '                Me.txtPreviousCode.Text = dt.Rows(0).Item("Previous_Code").ToString
        '                If IsDBNull(dt.Rows(0).Item("Last_Update").ToString) Then
        '                    Me.dtpLastUpdate.Value = Date.Now
        '                Else
        '                    Me.dtpLastUpdate.Value = dt.Rows(0).Item("Last_Update").ToString
        '                End If
        '                Me.cmbJobType.Text = dt.Rows(0).Item("JobType").ToString
        '                Me.cmbDivision.SelectedValue = dt.Rows(0).Item("Dept_Division")
        '                Me.cmbPayrollDivision.SelectedValue = dt.Rows(0).Item("PayRoll_Division")
        '                'Altered Against Task# 20150505 Ali Ansari
        '                Me.CmbPrefix.Text = dt.Rows(0).Item("Prefix").ToString
        '                'Altered Against Task# 20150505 Ali Ansari

        '                'Altered Against Task# 20150511 Ali Ansari adding state,region,zone,belt
        '                If dt.Rows(0).Item("Stateid") <> "" Then
        '                    cmbState.SelectedValue = Val(dt.Rows(0).Item("StateID"))
        '                Else
        '                    cmbState.SelectedIndex = 0
        '                End If
        '                If dt.Rows(0).Item("RegionId") <> "" Then
        '                    cmbRegion.SelectedValue = Val(dt.Rows(0).Item("RegionId"))
        '                Else
        '                    cmbRegion.SelectedIndex = 0
        '                End If

        '                If dt.Rows(0).Item("ZoneId") <> "" Then
        '                    cmbZone.SelectedValue = Val(dt.Rows(0).Item("ZoneId"))
        '                Else
        '                    cmbZone.SelectedIndex = 0
        '                End If

        '                If dt.Rows(0).Item("BeltId") <> "" Then
        '                    cmbBelt.SelectedValue = Val(dt.Rows(0).Item("BeltId"))
        '                Else
        '                    cmbBelt.SelectedIndex = 0
        '                End If
        '                'Altered Against Task# 20150511 Ali Ansari adding state,region,zone,belt
        '                Me.BtnSave.Text = "&Update"
        '                Me.GetSecurityRights()
        '                IsDrillDown = True
        '            End If
        '        End If
        '        IsDrillDown = False
        '    End If
        '    Return Get_All
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Function

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        Try
            Dim str As String
            Dim Id As Integer = 0I

            Id = Me.ddlCity.SelectedValue
            str = "Select CityID, CityName from tblListCity"
            FillDropDown(ddlCity, str)
            Me.ddlCity.SelectedValue = Id

            Id = 0

            Id = Me.cmbState.SelectedValue
            str = "Select StateID, StateName from tblListState"
            FillDropDown(cmbState, str)
            Me.ddlCity.SelectedValue = Id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub




    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function


    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function


    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles TABHISTORY.SelectedTabChanged
        Try
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub




    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click

    End Sub


    Private Sub GridEX1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(Nothing, Nothing)
        End If
    End Sub



    Private Sub BtnPrint_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.ButtonClick

    End Sub


    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbState_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbState.SelectedIndexChanged
        Try
            FillDropDown(Me.ddlCity, "Select * from tblListCity where STateId=" & Me.cmbState.SelectedValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            If FormValidate() Then

                If BtnSave.Text = "Save" Or BtnSave.Text = "&Save" Then
                    If Save() = True Then
                        Me.lblProgress.Visible = False 'Task:2593 Set Status
                        RefreshControls()
                    Else
                        ShowErrorMessage("Error while saving record")
                    End If
                Else



                    If Update_Record() = True Then
                        Me.lblProgress.Visible = False 'Task:2593 Set Status
                        RefreshControls()
                    Else
                        ShowErrorMessage("Error while saving record")
                    End If

                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub TxtBankAccountNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtBankAccountNo.KeyPress
        Try
            NumValidation(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtMobile_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMobile.KeyPress
        Try
            NumValidation(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtNIC_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNIC.KeyPress
        Try
            NumValidation(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtNTN_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNTN.KeyPress
        Try
            NumValidation(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub TxtPartnerPercentage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtPartnerPercentage.KeyPress
        Try
            NumValidation(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPhone_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPhone.KeyPress
        Try
            NumValidation(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & " Partner"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class

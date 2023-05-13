Imports System.Data.OleDb
''//To apply stikeout on format condition in janus//'' TASK # 943
Imports Janus.Windows.GridEX

Public Class frmDefJobCard
    Implements IGeneral
    Dim IsFormOpend As Boolean = False
    Dim InvId As Integer = 0
    Dim ContactId As Integer = 0
    Dim VehicleId As Integer = 0
    Dim JobCardId As Integer = 0
    Dim Number As Boolean = False
    'Waqar: TFS1606: Added this variable to show the company based on rights.
    Dim flgCompanyRights As Boolean = False
    ''' <summary>
    ''' TO GET TOP 50 AND ALL BY USING CONDITION
    ''' </summary>
    ''' <param name="StrCondition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAll(Optional ByVal StrCondition As String = "")
        Try
            Dim str As String = String.Empty
            Dim strFilter As String = String.Empty
            ''//Get all and Get top 50//'' TASK # 975
            'Ali Faisal : TFS1606 : Added CompanyId column in history
            str = "SELECT  " & IIf(StrCondition.ToString = "All", "", "Top 50") & "     tblJobCard.JobCardID, tblJobCard.JobCardNo, tblJobCard.JobCardDate, tblJobCard.VehicleID, tblVehicleInfo.RegistrationNo, tblVehicleInfo.ModelID, tblVehicleInfo.ColorID, tblVehicleInfo.ChessisNo, " _
              & " tblVehicleInfo.EngineNo, tblVehicleInfo.DOP, tblVehicleInfo.PreReading, tblVehicleInfo.CurrentReading, tblVehicleInfo.CompanyContactID, TblCompanyContacts.ContactName, TblCompanyContacts.Mobile, TblCompanyContacts.CNIC, TblCompanyContacts.DOB,  tblJobCard.Remarks, TblCompanyContacts.Address, ISNULL(tblJobCard.PaymentStatus, 0) As PaymentStatus, ISNULL(tblJobCard.DeliverStatus, 0) As DeliverStatus, IsNull(tblJobCard.CompanyId,0) CompanyId " _
              & " FROM            tblJobCard INNER JOIN" _
              & "           tblVehicleInfo ON tblJobCard.VehicleID = tblVehicleInfo.VahicleID INNER JOIN" _
              & "           TblCompanyContacts ON tblVehicleInfo.CompanyContactID = TblCompanyContacts.PK_Id order by tblJobCard.JobCardNo Desc"
            Dim dt As DataTable = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.ApplyGridSettings()
            grdSaved.RootTable.Columns("JobCardId").Visible = False
            grdSaved.RootTable.Columns("CompanyContactID").Visible = False
            grdSaved.RootTable.Columns("VehicleId").Visible = False
            grdSaved.RootTable.Columns("ModelID").Visible = False
            grdSaved.RootTable.Columns("ColorID").Visible = False
            'Ali Faisal : TFS1606 : Hide ComapnyId from History
            grdSaved.RootTable.Columns("CompanyId").Visible = False
            'Ali Faisal : TFS1606 : End
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmDefJobCard_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                If btnSave.Enabled = True Then
                    btnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                btnNew_Click(Nothing, Nothing)
                Exit Sub
            End If
            'Waqar: TFS1606: Added this code to get the value from configuration and apply right on company dropdown
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            'End Task TFS1606
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    

    ''''///REMOVE BY WAQAR RAZA BECAUSE IT CAN BE HANDLED WITH A CONDITION IN GETALL// TASK # 975
    ''//

    'Public Function GetTop() As DataView
    '    Try
    '        Dim str As String = String.Empty
    '        Dim strFilter As String = String.Empty
    '        str = "SELECT     Top 50   tblJobCard.JobCardID, tblJobCard.JobCardNo, tblJobCard.JobCardDate, tblJobCard.VehicleID, tblVehicleInfo.RegistrationNo, tblVehicleInfo.ModelID, tblVehicleInfo.ColorID, tblVehicleInfo.ChessisNo, " _
    '          & " tblVehicleInfo.EngineNo, tblVehicleInfo.DOP, tblVehicleInfo.PreReading, tblVehicleInfo.CurrentReading, tblVehicleInfo.CompanyContactID, TblCompanyContacts.ContactName, TblCompanyContacts.Mobile, TblCompanyContacts.CNIC, TblCompanyContacts.DOB, tblJobCard.Remarks, TblCompanyContacts.Address, ISNULL(tblJobCard.PaymentStatus, 0) As PaymentStatus, ISNULL(tblJobCard.DeliverStatus, 0) As DeliverStatus" _
    '          & " FROM            tblJobCard INNER JOIN" _
    '          & "           tblVehicleInfo ON tblJobCard.VehicleID = tblVehicleInfo.VahicleID INNER JOIN" _
    '          & "           TblCompanyContacts ON tblVehicleInfo.CompanyContactID = TblCompanyContacts.PK_Id order by tblJobCard.JobCardNo Desc"
    '        Dim dt As DataTable = GetDataTable(str)
    '        Me.grdSaved.DataSource = dt
    '        Me.grdSaved.RetrieveStructure()
    '        Me.ApplyGridSettings()
    '        grdSaved.RootTable.Columns("JobCardId").Visible = False
    '        grdSaved.RootTable.Columns("CompanyContactID").Visible = False
    '        grdSaved.RootTable.Columns("VehicleId").Visible = False
    '        grdSaved.RootTable.Columns("ModelID").Visible = False
    '        grdSaved.RootTable.Columns("ColorID").Visible = False
    '        Dim fc As GridEXFormatCondition
    '        fc = New GridEXFormatCondition(grdSaved.RootTable.Columns("DeliverStatus"), ConditionOperator.Equal, True)
    '        fc.FormatStyle.FontStrikeout = TriState.True
    '        fc.FormatStyle.ForeColor = Color.Red
    '        grdSaved.RootTable.FormatConditions.Add(fc)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    Private Sub frmDefJobCard_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ReSetControls()
            FillCombos()
            ''//MODIFIED BY WAQAR TO CALL TOP50 USINF SAME FUNCTION//'' TASK # 975
            GetAll("Top 50")
            Me.cmbSearch.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefCustomer)
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
                Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                        Me.btnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.CtrlGrdBar1.Visible = True
                'Added by Waqar to Remove the Bug reported by QA
                'Start Task:
                Me.btnNew.Visible = False
                Me.btnSave.Visible = False
                Me.btnRefresh.Visible = False
                Me.btnLoadAll.Visible = True
                Me.btnExit.Visible = True
                Me.btnDelivered.Visible = True
            Else
                Me.btnLoadAll.Visible = False
                Me.CtrlGrdBar1.Visible = False
                Me.btnExit.Visible = False
                Me.btnNew.Visible = True
                Me.btnSave.Visible = True
                Me.btnRefresh.Visible = True
                Me.btnDelivered.Visible = False
                'End Task:
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim objCommand As New OleDbCommand
            Dim objTrans As OleDbTransaction

            If Me.txtRegNo.Text = "" Then
                msg_Error("Please Enter Registration No")
                Me.txtRegNo.Focus()
                Exit Sub
            End If

            If Me.txtCustomerName.Text = "" Then
                msg_Error("Please Enter Customer Name")
                Me.txtCustomerName.Focus()
                Exit Sub
            End If

            If Me.txtPhoneNo.Text = "" Then
                msg_Error("Please Enter Phone No")
                Me.txtPhoneNo.Focus()

            End If
            ''//ADDED BY WAQAR TO RESTIRCT THE SYSTEM TO GET MODEL//'' TASK # 975
            If Not Me.cmbModel.SelectedValue > 0 Then
                ShowErrorMessage("Please Enter Model")
                Me.cmbModel.Focus()
                Exit Sub
            End If
            ''''///Ristrict the user to enter exactly11 disgits///'''' TASK # 975
            If Me.txtPhoneNo.Text.Length <> 11 Then
                msg_Error("Please Enter 11 digits of Mobile")
                Me.txtPhoneNo.Focus()
                Exit Sub
            End If
            ''''///User Can leave this textbox///'''' TASK # 975
            If Me.txtCNIC.Text.Length <> 13 AndAlso Me.txtCNIC.Text.Length > 0 Then
                msg_Error("Please Enter 13 Digits of CNIC")
                Me.txtCNIC.Focus()
                Exit Sub
            End If
            If Con.State = ConnectionState.Closed Then Con.Open()
            objTrans = Con.BeginTransaction
            Try
                objCommand = New OleDbCommand
                objCommand.Connection = Con
                objCommand.Transaction = objTrans
                Dim str As String = "SELECT     tblVehicleInfo.RegistrationNo, isnull(DeliverStatus, 0) as DeliverStatus FROM tblJobCard RIGHT OUTER JOIN tblVehicleInfo ON tblJobCard.VehicleID = tblVehicleInfo.VahicleID where tblVehicleInfo.RegistrationNo = '" & Me.txtRegNo.Text & "' and DeliverStatus = 0  "
                Dim dt As DataTable = GetDataTable(str)
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If dt.Rows.Count = 0 Then
                        objCommand.CommandText = "IF EXISTS (Select Mobile from tblCompanyContacts where Mobile = '" & Me.txtPhoneNo.Text & "' or RegistrationNo= '" & Me.txtRegNo.Text.Trim.Replace("'", "''") & "') Update tblCompanyContacts Set ContactName = '" & Me.txtCustomerName.Text & "',Mobile= N'" & Me.txtPhoneNo.Text & "',CNIC= N'" & Me.txtCNIC.Text & "',DOB = N'" & dtpDOB.Value.ToString("dd-MMM-yyyy h:mm:ss tt") & "',Address = N'" & Me.txtAddress.Text & "', RefCompanyId= 0, RegistrationNo=N'" & Me.txtRegNo.Text & "', CurrentReading = N'" & Me.txtCurrentReading.Text & "' where Mobile = '" & Me.txtPhoneNo.Text & "' or RegistrationNo= '" & Me.txtRegNo.Text.Trim.Replace("'", "''") & "' " _
                        & "ELSE Insert into tblCompanyContacts (ContactName,Mobile,CNIC,DOB,Address, RefCompanyId, RegistrationNo, CurrentReading) " _
                        & " values ('" & Me.txtCustomerName.Text & "',N'" & Me.txtPhoneNo.Text & "', N'" & Me.txtCNIC.Text & "',N'" & dtpDOB.Value.ToString("dd-MMM-yyyy h:mm:ss") & "',N'" & Me.txtAddress.Text & "', 0, '" & Me.txtRegNo.Text & "', N'" & Me.txtCurrentReading.Text & "') SELECT PK_ID from tblCompanyContacts where Mobile = '" & Me.txtPhoneNo.Text & "' " 'TFS1280 Start :08-Aug-17:Rai Haider on select previous customer customerid from tblCompanyContacts for which will be send zero on select identity End TFS1280
                        Dim value As Object = objCommand.ExecuteScalar()
                        If value Is DBNull.Value Or value Is Nothing Then
                            InvId = 0
                        Else
                            InvId = Convert.ToInt32(value)
                        End If
                        'InvId = objCommand.ExecuteScalar()
                        If ContactId > 0 AndAlso InvId = 0 Then
                            InvId = ContactId
                        End If
                        objCommand.CommandText = "Insert into tblVehicleInfo (RegistrationNo,ChessisNo,EngineNo,ColorID,ModelID,DOP,PreReading,CurrentReading,CompanyContactID) " _
                        & " values ('" & Me.txtRegNo.Text & "',N'" & Me.txtChessisNo.Text & "', N'" & Me.txtEngineNo.Text & "',N'" & Me.cmbColor.SelectedValue & "',N'" & Me.cmbModel.SelectedValue & "',N'" & dtpDOP.Value.ToString("dd-MMM-yyyy h:mm:ss") & "',N'" & Me.txtPreReading.Text & "',N'" & Me.txtCurrentReading.Text & "'," & InvId & ") Select @@Identity"
                        InvId = Convert.ToInt32(objCommand.ExecuteScalar())
                        'Ali Faisal : TFS1606 : Add CompanyId Column
                        objCommand.CommandText = "Insert into tblJobCard (JobCardNo,JobCardDate,Remarks,VehicleId,PaymentStatus,DeliverStatus,CompanyId) " _
                        & " values ('" & Me.txtJobCardNo.Text & "',N'" & dtpJobCardDate.Value.ToString("dd-MMM-yyyy h:mm:ss") & "',N'" & Me.txtRemarks.Text & "'," & InvId & ", 0 ,0, " & Me.cmbCompany.SelectedValue & " ) Select @@Identity"
                        'Ali Faisal : TFS1606 : End
                        objCommand.ExecuteNonQuery()
                        ''msg_Information(str_informSave)
                    Else
                        msg_Error("This JobCard is Already Exists and Not Delivered Yet")
                    End If
                Else
                    'Added by Waqar to remove bug reported by QA
                    If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                    objCommand.CommandText = "Update tblCompanyContacts Set ContactName = '" & Me.txtCustomerName.Text & "',Mobile= N'" & Me.txtPhoneNo.Text & "',CNIC= N'" & Me.txtCNIC.Text & "',DOB = N'" & dtpDOB.Value.ToString("dd-MMM-yyyy h:mm:ss") & "',Address = N'" & Me.txtAddress.Text & "', RegistrationNo=N'" & Me.txtRegNo.Text & "', CurrentReading = N'" & Me.txtCurrentReading.Text & "' Where PK_Id =" & ContactId & ""
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = "Update tblVehicleInfo Set RegistrationNo = '" & Me.txtRegNo.Text & "',ChessisNo = N'" & Me.txtChessisNo.Text & "',EngineNo = N'" & Me.txtEngineNo.Text & "',ColorID = N'" & Me.cmbColor.SelectedValue & "',ModelID = N'" & Me.cmbModel.SelectedValue & "',DOP = N'" & dtpDOP.Value.ToString("dd-MMM-yyyy h:mm:ss") & "',PreReading = N'" & Me.txtPreReading.Text & "',CurrentReading = N'" & Me.txtCurrentReading.Text & "' Where VahicleID = " & VehicleId & ""
                    objCommand.ExecuteNonQuery()
                    'Ali Faisal : TFS1606 : Add column of CompanyId
                    objCommand.CommandText = "Update tblJobCard Set JobCardNo = '" & Me.txtJobCardNo.Text & "',JobCardDate = N'" & dtpJobCardDate.Value.ToString("dd-MMM-yyyy h:mm:ss tt") & "',Remarks = N'" & Me.txtRemarks.Text & "', CompanyId = " & Me.cmbCompany.SelectedValue & " Where JobCardId = " & JobCardId & ""
                    'Ali Faisal : TFS1606 : End
                    objCommand.ExecuteNonQuery()

                End If
                Me.cmbSearch.Focus()
                '    objCommand.CommandText = "insert into tblJobCard(JobCardNo, JobCardDate, Customer_Name, RegistrationNo, Phone_No, DOB, CNIC, Model, Color, Chessis_No, Engine_No, DOP, Remarks, Address) values('" & Me.txtJobCardNo.Text & "','" & Me.dtpJobCardDate.Value.ToString("dd-MMM-yyyy") & "','" & Me.txtCustomerName.Text & "',N'" & Me.txtRegNo.Text & "', '" & Me.txtPhoneNo.Text & "','" & Me.dtpDOB.Value.ToString("dd-MMM-yyyy") & "',N'" & Me.txtCNIC.Text & "',N'" & Me.txtModel.Text & "',N'" & Me.cmbColor.Text & "',N'" & Me.txtChessisNo.Text & "',N'" & Me.txtEngineNo.Text & "',N'" & Me.dtpDOP.Value.ToString("dd-MMM-yyyy") & "','" & Me.txtRemarks.Text & "',N'" & Me.txtAddress.Text & "')"
                'Else
                '    objCommand.CommandText = "Update tblJobCard set JobCardNo=N'" & txtJobCardNo.Text.Replace("'", "''") & "', JobCardDate=N'" & dtpJobCardDate.Value.ToString("dd-MMM-yyyy h:mm:ss tt") & "',RegistrationNo=N'" & txtRegNo.Text & "', Model=N'" & txtModel.Text.Replace("'", "''") & "', " _
                '              & " Color=N'" & cmbColor.SelectedValue & "', Chessis_No=N'" & txtChessisNo.Text.Replace("'", "''") & "', Engine_No=N'" & txtEngineNo.Text.Replace("'", "''") & "', " _
                '              & " Customer_Name=N'" & txtCustomerName.Text.Replace("'", "''") & "', Phone_No=N'" & txtPhoneNo.Text.Replace("'", "''") & "',  CNIC=N'" & txtCNIC.Text.Replace("'", "''") & "', DOB=N'" & dtpDOB.Value.ToString("dd-MMM-yyyy h:mm:ss tt") & "', " _
                '              & " DOP=N'" & dtpDOP.Value.ToString("dd-MMM-yyyy h:mm:ss tt") & "'Where JobCardNo =" & txtJobCardNo.Text & ""
                'End If
                'objCommand.Transaction = objTrans
                '' Dim identity As Integer = Convert.ToInt32(objCommand.ExecuteScalar())
                objTrans.Commit()

                Try
                    SaveActivityLog("Config", Me.Text, IIf(Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save", InvId, Me.grdSaved.CurrentRow.Cells("JobCardID").Value.ToString), True)
                Catch ex As Exception
                End Try
                GetAll("Top 50")
                'Start Task : TFS1266: 07-Aug-2017:Rai Haider
                'Add Checkbox for Auto Print and Move to sales Screen After save
                'If chkprintonsave.Checked is True/On Auto print send to printer else A popup Shown for Print option 
                If Me.chkprintonsave.Checked = True Then
                    CallShowReport(False)
                Else

                    If msg_Confirm("Do you want to print") = True Then
                        CallShowReport(True)
                    End If

                End If

                'If chkmovesalesonsave.Checked is True/On A popup Shown option for Move to Sales Screen
                If Me.chkmovesalesonsave.Checked = True Then

                    If msg_Confirm("Do you want to move to Sales") = True Then
                        frmMain.LoadControl("RecordSales")
                    End If
                End If
                'End Tsak :TFS1266
                Me.ReSetControls()
                ''//MODIFIED BY WAQAR TO CALL TOP50 USING SAME FUNCTION//''TASK # 975
            Catch ex As Exception
                objTrans.Rollback()
                ShowErrorMessage("Error occured while saving record: " & ex.Message)
            Finally
                If Con.State = ConnectionState.Open Then Con.Close()
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub EditRecord()
        Try
            If Not Val(grdSaved.CurrentRow.Cells("JobCardID").Value.ToString) > 0 Then Exit Sub
            Me.txtJobCardNo.Text = grdSaved.CurrentRow.Cells("JobCardNo").Value
            Me.dtpJobCardDate.Value = IIf(IsDBNull(grdSaved.CurrentRow.Cells("JobCardDate").Value), DateTime.Today, grdSaved.CurrentRow.Cells("JobCardDate").Value)
            Me.txtCustomerName.Text = grdSaved.CurrentRow.Cells("ContactName").Value
            Me.txtRegNo.Text = grdSaved.CurrentRow.Cells("RegistrationNo").Value.ToString
            ContactId = Val(Me.grdSaved.CurrentRow.Cells("CompanyContactID").Value)
            VehicleId = Val(Me.grdSaved.CurrentRow.Cells("VehicleId").Value)
            JobCardId = Val(Me.grdSaved.CurrentRow.Cells("JobCardID").Value)
            Me.txtPhoneNo.Text = grdSaved.CurrentRow.Cells("Mobile").Value
            Me.txtCNIC.Text = grdSaved.CurrentRow.Cells("CNIC").Value.ToString
            Me.txtRemarks.Text = grdSaved.CurrentRow.Cells("Remarks").Value.ToString
            Me.txtAddress.Text = grdSaved.CurrentRow.Cells("Address").Value.ToString
            Me.cmbModel.SelectedValue = Val(grdSaved.CurrentRow.Cells("ModelID").Value.ToString)
            Me.cmbColor.SelectedValue = Val(grdSaved.CurrentRow.Cells("ColorID").Value.ToString)
            Me.txtChessisNo.Text = grdSaved.CurrentRow.Cells("ChessisNo").Value.ToString
            Me.txtEngineNo.Text = grdSaved.CurrentRow.Cells("EngineNo").Value.ToString
            Me.dtpDOB.Value = IIf(IsDBNull(grdSaved.CurrentRow.Cells("DOB").Value), DateTime.Today, grdSaved.CurrentRow.Cells("DOB").Value)
            Me.dtpDOB.Checked = IIf(IsDBNull(grdSaved.CurrentRow.Cells("DOB").Value), False, True)
            Me.dtpDOP.Value = IIf(IsDBNull(grdSaved.CurrentRow.Cells("DOP").Value), DateTime.Today, grdSaved.CurrentRow.Cells("DOP").Value)
            Me.txtPreReading.Text = grdSaved.CurrentRow.Cells("PreReading").Value.ToString
            Me.txtCurrentReading.Text = grdSaved.CurrentRow.Cells("CurrentReading").Value.ToString
            'Ali Faisal : TFS1606 : Get Company dropdown value from history
            Me.cmbCompany.SelectedValue = Val(grdSaved.CurrentRow.Cells("CompanyId").Value)
            'Ali Faisal : TFS1606 : End
            Me.btnSave.Text = "&Update"
            Me.UltraTabControl1.SelectedTab = UltraTabPageControl1.Tab
            Me.GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            'Changed the query on HFR demand
            'Start Task
            Dim str As String = "SELECT        distinct  tblDefModelList.Name as Model, TblCompanyContacts.RegistrationNo, TblCompanyContacts.PK_Id, TblCompanyContacts.ContactName, TblCompanyContacts.Mobile, TblCompanyContacts.CurrentReading, TblCompanyContacts.CNIC, " _
            & "TblCompanyContacts.DOB, TblCompanyContacts.Address, ArticleColorDefTable.ArticleColorName as Color, tblJobCard.PaymentStatus " _
            & "FROM            tblDefModelList INNER JOIN " _
                   & "      tblVehicleInfo ON tblDefModelList.ModelId = tblVehicleInfo.ModelID INNER JOIN " _
                   & "      ArticleColorDefTable ON tblVehicleInfo.ColorID = ArticleColorDefTable.ArticleColorId INNER JOIN " _
                   & "      TblCompanyContacts ON tblVehicleInfo.CompanyContactID = TblCompanyContacts.PK_Id Left Outer Join" _
                   & "      tblJobCard ON tblVehicleInfo.VahicleID = tblJobCard.VehicleID" _
            & " WHERE        (TblCompanyContacts.RegistrationNo <> '') and tbljobcard.paymentstatus is NULL OR tbljobcard.paymentstatus <> 'False'"
            FillUltraDropDown(Me.cmbSearch, str, False)
            'End Task


            '// Filling Model list
            FillDropDown(Me.cmbModel, "select * from tblDefModelList where active= 1 order by Name", True)

            '// Filling colours in combo
            FillDropDown(Me.cmbColor, "select * from ArticleColorDefTable where active= 1 order by SortOrder, ArticleColorName", True)

            ''//ADDED BY WAQAR TO FILL COMBOBOX FOR EXIT FOR TRYTASK # 943
            FillDropDown(Me.cmbEmployee, "Select Employee_ID, Employee_Name from tblDefEmployee WHERE Active = 1", True)
            Me.cmbSearch.DisplayLayout.Bands(0).Columns("DOB").Hidden = True
            ''//HIDDEN BY WAQAR FOR 
            ''START TASK # 943
            Me.cmbSearch.DisplayLayout.Bands(0).Columns("PK_Id").Hidden = True
            Me.cmbSearch.DisplayLayout.Bands(0).Columns("CNIC").Hidden = True
            Me.cmbSearch.DisplayLayout.Bands(0).Columns("Address").Hidden = True
            Me.cmbSearch.DisplayLayout.Bands(0).Columns("PaymentStatus").Hidden = True
            ''END TASK # 943
            Me.cmbSearch.DisplayLayout.Bands(0).Columns("RegistrationNo").Header.Caption = "Registration No"
            Me.cmbSearch.DisplayLayout.Bands(0).Columns("ContactName").Header.Caption = "Customer Name"
            Me.cmbSearch.DisplayLayout.Bands(0).Columns("Mobile").Header.Caption = "Phone No"
            'Ali Faisal : TFS1606 : Company dropdown filling
            'Waqar Raza: TFS1606: Modified the query to show company bsed on rights using variable and configuration which we discuss earlier against this task
            FillDropDown(Me.cmbCompany, "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & " And CompanyId Is Not Null) Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable WHERE CompanyName <> '' " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ") Else Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & "", False)
            'Waqar Raza: TFS1606: End Task
            'Ali Faisal : TFS1606 : End
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    Private Sub ClearFields()
        Try
            Me.btnSave.Text = "&Save"
            Me.txtJobCardNo.Text = ""
            Me.txtCustomerName.Text = ""
            Me.cmbSearch.Text = ""
            If Me.cmbColor.SelectedIndex > 0 Then Me.cmbColor.SelectedIndex = 0
            If Me.cmbModel.SelectedIndex > 0 Then Me.cmbModel.SelectedIndex = 0
            Me.txtRegNo.Text = ""
            Me.txtChessisNo.Text = ""
            Me.txtEngineNo.Text = ""
            Me.txtCustomerName.Text = ""
            Me.txtPhoneNo.Text = ""
            Me.txtCNIC.Text = ""
            Me.txtRemarks.Text = ""
            Me.txtAddress.Text = ""
            Me.dtpDOP.Value = Today
            Me.dtpDOB.Checked = False
            Me.dtpDOB.Value = Today
            Me.txtPreReading.Text = ""
            Me.txtCurrentReading.Text = ""
            Me.dtpJobCardDate.Value = Today
            ContactId = 0
            Me.txtRegNo.Enabled = False

            ''//ADDED BY WAQAR TO SHO HIDDEN PANEL WHEN YOUR PRESS EXIT FOR TRY//
            ''Start TASK # 943
            SplitContainer1.Panel1Collapsed = True
            If Me.cmbEmployee.SelectedIndex > 0 Then Me.cmbEmployee.SelectedIndex = 0
            ''End TASK # 943
            Me.txtRemakrsTry.Text = ""
            'Ali Faisal : TFS1606 : Add dropdown of company
            Me.cmbCompany.SelectedValue = 1
            'Ali Faisal : TFS1606 : End
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.FillCombos()
            Me.GetSecurityRights()
            ClearFields()
            Me.txtJobCardNo.Text = GetNextDocNo("JC", 5, "tblJobCard", "JobCardNo")

        Catch ex As Exception
            Throw ex
        End Try
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

    Private Sub grdSaved_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grdSaved.RowDoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Not grdSaved.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                Exit Sub
            End If
            If msg_Confirm(str_ConfirmDelete) = True Then
                Try
                    Dim cm As New OleDbCommand

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                   cm.CommandText = "delete from tblJobCard where JobCardID=" & Me.grdSaved.CurrentRow.Cells("JobCardID").Value.ToString
                    cm.ExecuteNonQuery()
                    msg_Information(str_informDelete)
                    ''//MODIFIED BY WAQAR TO CALL TOP50 USING SAME FUNCTION//''TASK # 975
                    GetAll("Top 50")
                    Try
                        SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.grdSaved.CurrentRow.Cells("JobCardID").Value.ToString, True)
                    Catch ex As Exception
                    End Try
                Catch ex As Exception
                    msg_Error("Error occured while deleting record: " & ex.Message)
                Finally
                    If Con.State = ConnectionState.Open Then Con.Close()
                End Try
                Me.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            Me.cmbSearch.Focus()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoadAll_Click(sender As Object, e As EventArgs) Handles btnLoadAll.Click
        Try
            ''//MODIFIED BY WAQAR TO GET ALL USING SAME FUNCTION//''TASK # 975
            GetAll("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rboRegNo_CheckedChanged(sender As Object, e As EventArgs) Handles rboRegNo.CheckedChanged, rboName.CheckedChanged, rboPhoneNo.CheckedChanged
        Try


            Dim RadioB As RadioButton = sender

            '// Exiting fuction when this function is called due to unchecking the previous radio button
            If RadioB.Checked = False Then
                Exit Sub
            End If

            If RadioB.Name = rboName.Name Then
                Me.cmbSearch.DisplayMember = "ContactName"

            ElseIf RadioB.Name = rboPhoneNo.Name Then
                Me.cmbSearch.DisplayMember = "Mobile"

            ElseIf RadioB.Name = rboRegNo.Name Then
                Me.cmbSearch.DisplayMember = "RegistrationNo"

            End If
            cmbSearch.Focus()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    '12-July-2017: Task TFS1048: Waqar Raza: Changed from row selection to leave to allow the user to select any value from drop down.
    'TFS1279 Start:09-Aug-17:Rai Haider : On Radio Button Selection On Job Card  load wrong data 
    Private Sub cmbSearch_Leave(sender As Object, e As EventArgs) Handles cmbSearch.Leave
        Try
            If cmbSearch.ActiveRow Is Nothing Then
                If Me.rboName.Checked = True Then
                    Me.txtCustomerName.Text = cmbSearch.Text
                    Me.txtRegNo.Text = ""
                    Me.txtPhoneNo.Text = ""
                End If
                If Me.rboRegNo.Checked = True Then
                    Me.txtRegNo.Text = cmbSearch.Text
                    Me.txtCustomerName.Text = ""
                    Me.txtPhoneNo.Text = ""
                End If
                If Me.rboPhoneNo.Checked = True Then
                    Me.txtPhoneNo.Text = cmbSearch.Text
                    Me.txtCustomerName.Text = ""
                    Me.txtRegNo.Text = ""
                End If
                'End TFS1279:Rai Haider
                '' ReSetControls()
            Else
                ''//ADDED BY WAQAR TO LOAD THE MODEL ON ROW SELECTION//'' TASK # 975
                Me.cmbModel.Text = cmbSearch.ActiveRow.Cells("Model").Value.ToString

                Me.txtCustomerName.Text = cmbSearch.ActiveRow.Cells("ContactName").Value.ToString
                Me.txtPhoneNo.Text = cmbSearch.ActiveRow.Cells("Mobile").Value.ToString
                Me.txtRegNo.Text = cmbSearch.ActiveRow.Cells("RegistrationNo").Value.ToString
                Me.dtpDOB.Value = cmbSearch.ActiveRow.Cells("DOB").Value.ToString
                ''//ADDED BY WAQAR TO LOAD THE COLOR ON ROW SELECTION//'' TASK # 975
                Me.cmbColor.Text = cmbSearch.ActiveRow.Cells("Color").Value.ToString

                Me.txtCNIC.Text = cmbSearch.ActiveRow.Cells("CNIC").Value.ToString
                'Ali Faisal : Commented text box to not select the Remarks on selection
                'Me.txtRemarks.Text = cmbSearch.ActiveRow.Cells("Remarks").Value.ToString
                Me.txtAddress.Text = cmbSearch.ActiveRow.Cells("Address").Value.ToString
                Me.txtPreReading.Text = cmbSearch.ActiveRow.Cells("CurrentReading").Value.ToString
                Me.txtCurrentReading.Text = ""
                ContactId = Val(cmbSearch.ActiveRow.Cells("PK_Id").Value.ToString)
                Me.txtCurrentReading.Focus()
                Me.txtRegNo.Enabled = False

            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            ''//MODIFIED BY WAQAR TO CALL TOP50 USING SAME FUNCTION//''TASK # 975
            GetAll("Top 50")
            'Added by Waqar to remove bug reported by QA
            'Start Task:
            Dim id As Integer
            Dim id1 As Integer
            id = Me.cmbModel.SelectedValue
            id1 = Me.cmbColor.SelectedValue
            Me.cmbModel.SelectedValue = id
            Me.cmbColor.SelectedValue = id1
            'End Task:
            FillCombos()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSearch_Enter(sender As Object, e As EventArgs) Handles cmbSearch.Enter
        Try
            Me.cmbSearch.PerformAction(Infragistics.Win.UltraWinGrid.UltraComboAction.ToggleDropdown)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            CallShowReport(True)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub CallShowReport(Optional ByVal Print As Boolean = False)
        Try
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@ID", Val(Me.grdSaved.CurrentRow.Cells("JobCardID").Value))
            ShowReport("rptJobCard", , , , Print)
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtPhoneNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPhoneNo.KeyPress
        txtPhoneNo.MaxLength = 11
        If Char.IsNumber(e.KeyChar) = False AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtCNIC_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCNIC.KeyPress
        txtCNIC.MaxLength = 13
        If Char.IsNumber(e.KeyChar) = False AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub
    '''///TO SHOW THE HIDDEN PANEL FOR VAHICLE EXIT FOR TRY///'''TASK # 943
    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If SplitContainer1.Panel1Collapsed = True Then
            SplitContainer1.Panel1Collapsed = False
        Else
            SplitContainer1.Panel1Collapsed = True
        End If
    End Sub
    '''///TO SAVE THE RECORD WHEN VEHICLE WILL BR OUT FOR TRY///'''TASK # 943
    Private Sub btnSaveTry_Click(sender As Object, e As EventArgs) Handles btnSaveTry.Click
        Try
            Dim jid As String = String.Empty
            jid = Val(Me.grdSaved.GetRow.Cells("JobCardID").Value.ToString)
            Dim str As New OleDbCommand
            If Con.State = ConnectionState.Closed Then Con.Open()
            str.Connection = Con
            str.CommandText = "Insert into tblJobCardExitTry (JobCardID, EmployeeID, TryRemarks) values(" & jid & "," & cmbEmployee.SelectedValue & ", N'" & txtRemakrsTry.Text & "')"
            str.ExecuteNonQuery()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            If Con.State = ConnectionState.Open Then Con.Close()
        End Try
    End Sub

    Private Sub grdSaved_Click(sender As Object, e As EventArgs) Handles grdSaved.Click

    End Sub
    ''//TASK # 943
    ''' <summary>
    ''' TO DELIVER THE JOBCARD FROM HISTORY
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelivered_Click(sender As Object, e As EventArgs) Handles btnDelivered.Click
        Try
            If msg_Confirm(str_ConfirmDeliver) = True Then
                Dim job As Integer = Me.grdSaved.GetRow.Cells("JobCardId").Value.ToString
                Dim pay As String = Me.grdSaved.GetRow.Cells("PaymentStatus").Value
                Dim str As New OleDbCommand
                If Con.State = ConnectionState.Closed Then Con.Open()
                str.Connection = Con
                If pay = "True" Then
                    str.CommandText = "Update tblJobCard set DeliverStatus ='True' where JobCardId=" & job & ""
                    str.ExecuteNonQuery()
                End If
            End If
            GetAll("Top 50")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            If Con.State = ConnectionState.Open Then Con.Close()
        End Try
    End Sub
    '''///TO ENABLE DELIVERED BUTTON ON BASIS OF SALES WITH PAID///''''TASK # 943
    Private Sub grdSaved_SelectionChanged(sender As Object, e As EventArgs) Handles grdSaved.SelectionChanged
        Try
            If Not Me.grdSaved.GetRow Is Nothing Then
                If Me.grdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    Dim paymentstatus As Boolean
                    Dim deliver As Boolean
                    paymentstatus = Me.grdSaved.GetRow.Cells("PaymentStatus").Value
                    deliver = Me.grdSaved.GetRow.Cells("DeliverStatus").Value
                    If paymentstatus = True AndAlso deliver = False Then
                        btnDelivered.Enabled = True
                    Else
                        btnDelivered.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''29-Jun-2017: Task # 975: Waqar Raza: To apply some action if paymentstatus is 1 or deliverstatus is 1.
    Private Sub grdSaved_FormattingRow(sender As Object, e As RowLoadEventArgs) Handles grdSaved.FormattingRow
        Try
            If e.Row.Cells("PaymentStatus").Value = True Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.BackColor = Color.LightGray
                e.Row.RowStyle = rowstyle
            End If
            If e.Row.Cells("DeliverStatus").Value = True Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.FontStrikeout = TriState.True
                e.Row.RowStyle = rowstyle
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles chkmovesalesonsave.CheckedChanged

    End Sub
End Class
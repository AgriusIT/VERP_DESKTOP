Imports SBModel
Imports SBDal
Imports System.Text.RegularExpressions

Public Class frmSkippingSalesInvoicesNumbers
    Dim SalesSkip As SalesInvoiceSkip
    Dim SalesSkipDAL As New SalesInvoiceSkipDAL
    Dim SalesInvoiceSkipId As Integer = 0
    Dim CompanyBasePrefix As Boolean = False
    Dim flgCompanyRights As Boolean = False
    Dim DocumentNo1 As String = String.Empty
    Dim DocumentNo As String = String.Empty
    Dim DocNo As String = String.Empty
    Dim DocPrefix As String = String.Empty
    Dim VoucherType As String = String.Empty
    Private Sub frmSkippingSalesInvoicesNumbers_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            'GetDocumentNo()
            GetSecurityRights()
            FillCombo()
            If Not getConfigValueByType("Company-Based-Prefix").ToString = "Error" Then
                CompanyBasePrefix = Convert.ToBoolean(getConfigValueByType("Company-Based-Prefix").ToString)
            End If
            If Not getConfigValueByType("CompanyRights").ToString = "Error" Then
                flgCompanyRights = getConfigValueByType("CompanyRights")
            End If
            'Me.txtInvoiceNoToSkip.Text = GetDocumentNo()
            VoucherType = getConfigValueByType("VoucherNo").ToString
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillCombo()
        Dim Str As String = String.Empty
        Try
            Str = "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & " And CompanyId Is Not Null) " _
               & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable WHERE CompanyName <> '' " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")" _
               & "Else " _
               & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
            'End Task:2677
            FillDropDown(Me.cmbCompany, Str, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub FillModel()
        Try
            'Dim DocNo As String = GetDocumentNo()
            ' '''
            'Dim LastIndexOfDash As Integer = DocNo.LastIndexOf("-")
            'DocumentNo = DocNo.Substring(LastIndexOfDash + 1)
            'DocumentNo1 = DocNo.Substring(LastIndexOfDash + 1)
            'Dim DocumentNoPrefix As String = DocNo.Substring(0, LastIndexOfDash + 1)
            ''Dim LeadingZeros As String = GetLeadingZerosLength(DocumentNo)
            'Dim LeadingZeros As String = ""
            'Dim IncrementDocumentNo As String = DocumentNo + 1
            'If Me.txtInvoiceNoToSkip.Text.Length < DocumentNo.Length Then
            '    Dim shortLength As Integer = DocumentNo.Length - Me.txtInvoiceNoToSkip.Text.Length
            '    For i As Integer = 1 To shortLength
            '        LeadingZeros += "0"
            '    Next
            'End If
            'If LeadingZeros.Length > 0 Then
            '    DocumentNo = LeadingZeros & Me.txtInvoiceNoToSkip.Text
            'Else
            '    DocumentNo = Me.txtInvoiceNoToSkip.Text
            'End If
            'DocNo = DocumentNoPrefix & DocumentNo
            '''
            SalesSkip = New SalesInvoiceSkip()
            SalesSkip.SalesInvoiceSkipId = SalesInvoiceSkipId
            SalesSkip.SalesInvoiceSkipNo = Me.txtInvoiceNo.Text
            SalesSkip.Reason = Me.txtReason.Text
            SalesSkip.CompanyId = Me.cmbCompany.SelectedValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function Save() As Boolean
        Try
            If SalesSkipDAL.Save(SalesSkip) Then
                SaveActivityLog("Sales", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.AccountTransaction, SalesSkip.SalesInvoiceSkipNo, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function Edit() As Boolean
        Try
            If SalesSkipDAL.Update(SalesSkip) Then
                SaveActivityLog("Sales", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.AccountTransaction, SalesSkip.SalesInvoiceSkipNo, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function Delete() As Boolean
        Try
            If SalesSkipDAL.Delete(Val(Me.GridEX1.GetRow.Cells("SalesInvoiceSkipId").Value.ToString)) Then
                SaveActivityLog("Sales", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.AccountTransaction, Me.GridEX1.GetRow.Cells("SalesInvoiceSkipNo").Value.ToString, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function GetDocumentNo() As String
        Try
            Dim NOW As String = DateTime.Now.ToString
            If Me.txtInvoiceNoToSkip.Text.Length > 0 Then
                If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                    ' Return GetSerialNo(IIf(CompanyBasePrefix = False & IIf(Me.GetPrefix(IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue)).Length = 0, "SI" & Me.cmbCompany.SelectedValue & "-" & Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year & "-", "SalesMasterTable", "SalesNo")
                    If CompanyBasePrefix = True AndAlso GetPrefix(IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue)).Length > 0 Then
                        Return GetSerialNo("" & GetPrefix(Me.cmbCompany.SelectedValue) & "" & "-", "SalesMasterTable", "SalesNo")
                    Else
                        Return GetSerialNo("SI" & Me.cmbCompany.SelectedValue & "-" + Microsoft.VisualBasic.Right(CInt(Me.txtYear.Text), 2) + "-", "SalesMasterTable", "SalesNo")
                    End If
                ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                    Return GetNextDocNo("SI" & Me.cmbCompany.SelectedValue & "-" & Format(DateTime.Now, "yy") & GetMonth(Me.cmbMonth.Text).ToString("00"), 4, "SalesMasterTable", "SalesNo")
                Else
                    Return GetNextDocNo("SI" & Me.cmbCompany.SelectedValue, 6, "SalesMasterTable", "SalesNo")
                End If
                '' "^\\d{3}-\\d{3}-\\d{4}$"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsValidated() As Boolean
        Try
            If Me.txtInvoiceNo.Text.Length = 0 Then
                ShowErrorMessage("Please enter invoice no.")
                txtInvoiceNoToSkip.Focus()
                Exit Function
                Return False
            End If


            'If DocumentNo.Length > DocumentNo1.Length Then
            '    ShowErrorMessage("Please enter less than or equivalent to " & DocumentNo1.Length & " digits.")
            '    txtInvoiceNoToSkip.Focus()
            '    Exit Function
            '    Return False
            'End If
            If SalesInvoiceSkipDAL.IsIssued(Me.txtInvoiceNo.Text) Then
                ShowErrorMessage("Entered invoice number has already been ISSUED.")
                txtInvoiceNoToSkip.Focus()
                Exit Function
                Return False
            End If

            If SalesInvoiceSkipDAL.IsSkipped(Me.txtInvoiceNo.Text) Then
                ShowErrorMessage("Entered invoice number has already been SKIPPED.")
                txtInvoiceNoToSkip.Focus()
                Exit Function
                Return False
            End If
            FillModel()
            'Dim reg As New Regex
            'Regex rgx = new Regex(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");

            '     1298-673-4192 is a valid part number.
            '//  A08Z-931-468A is a valid part number.
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidated() Then
                If btnSave.Text = "&Save" Or btnSave.Text = "Save" Then
                    If Save() Then
                        msg_Information("Data has been saved successfully.")
                        ResetControls()
                    Else
                        msg_Information("Data could not save")
                    End If
                Else
                    If SalesInvoiceSkipId = 0 Then
                        ShowErrorMessage("Please select a record to edit.")
                        Exit Sub
                    End If
                    If msg_Confirm("Do you want to edit the record?") = False Then Exit Sub
                    If Edit() Then
                        msg_Information("Data has been updated successfully.")
                        ResetControls()
                    Else
                        msg_Information("Data could not edit")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs)
        'Try
        '    If SalesInvoiceSkipId = 0 Then
        '        ShowErrorMessage("Please select a record to edit.")
        '        Exit Sub
        '    End If
        '    If msg_Confirm("Do you want to edit the record?") = False Then Exit Sub
        '    If IsValidated() Then
        '        If Edit() Then
        '            msg_Information("Data has been updated successfully.")
        '            ResetControls()
        '        Else
        '            msg_Information("Data could not edit")
        '        End If
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            'If IsValidated() Then
            '    FillModel()
            'If SalesInvoiceSkipId = 0 Then
            '    ShowErrorMessage("Please select a record to delete.")
            '    Exit Sub
            'End If
            If msg_Confirm("Do you want to delete the record?") = False Then Exit Sub
            If Delete() Then
                msg_Information("Data has been deleted successfully.")
                ResetControls()
            Else
                msg_Information("Data could not delete")
            End If
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ResetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim Id As Integer = 0
        Try
            Id = Me.cmbCompany.SelectedValue
            FillCombo()
            Me.cmbCompany.SelectedValue = Id
            'Me.txtInvoiceNoToSkip.Text = GetDocumentNo()
            'FillModel()
            'Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub ResetControls()
        Try
            Me.txtInvoiceNoToSkip.Text = ""
            Me.txtInvoiceNo.Text = ""
            Me.txtReason.Text = ""
            SalesInvoiceSkipId = 0
            Me.btnSave.Text = "&Save"
            InitializeYearMonth()
            If VoucherType = "Yearly" Then
                Me.cmbMonth.Enabled = False
                Me.txtYear.Enabled = True
            ElseIf VoucherType = "Monthly" Then
                Me.cmbMonth.Enabled = True
                Me.txtYear.Enabled = True
            Else
                Me.cmbMonth.Enabled = False
                Me.txtYear.Enabled = False
            End If
            FillCombo()
            GetAll()
            'SalesSkipDAL.GetAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetAll()
        Try
            Me.GridEX1.DataSource = SalesSkipDAL.GetAll()
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns("SalesInvoiceSkipId").Visible = False  ''SalesInvoiceSkipNo
            Me.GridEX1.RootTable.Columns("SalesInvoiceSkipNo").Caption = "Skipped Invoice Number"
            Me.GridEX1.RootTable.Columns("SalesInvoiceSkipNo").Width = 300
            Me.GridEX1.RootTable.Columns("CompanyId").Visible = False
            Me.GridEX1.RootTable.Columns("Reason").Width = 300
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GridEX1_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles GridEX1.RowDoubleClick
        Try
            SalesInvoiceSkipId = Val(Me.GridEX1.GetRow.Cells("SalesInvoiceSkipId").Value.ToString)
            Me.txtInvoiceNo.Text = Me.GridEX1.GetRow.Cells("SalesInvoiceSkipNo").Value.ToString
            Me.txtReason.Text = Me.GridEX1.GetRow.Cells("Reason").Value.ToString
            Me.cmbCompany.SelectedValue = Val(Me.GridEX1.GetRow.Cells("CompanyId").Value.ToString)
            Dim LastIndexOfDash As Integer = txtInvoiceNo.Text.LastIndexOf("-")
            Me.txtInvoiceNoToSkip.Text = txtInvoiceNo.Text.Substring(LastIndexOfDash + 1)
            Me.btnSave.Text = "&Update"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True

                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Me.btnSave.Enabled = False
                    Exit Sub
                End If
            Else
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                        ''TASK TFS1384 replaced Report Print and Report Export with Report Print and Report Export on 07-09-2017
                    ElseIf RightsDt.FormControlName = "Report Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Report Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        Me.btnSave.Enabled = True
                        'ElseIf RightsDt.FormControlName = "Update" Then
                        '    Me.btnUpdate.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                        ''End TASK TFS1384
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function GetLeadingZerosLength(ByVal str As String) As String
        Try
            Dim Zeros As String = String.Empty
            For Each c As Char In str
                If c = "0"c Then
                    Zeros += "0"
                    'Else
                    '    Exit For
                End If
            Next
            Return Zeros
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtInvoiceNoToSkip_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInvoiceNoToSkip.KeyPress
        Try
            e.Handled = e.KeyChar <> ChrW(Keys.Back) And Not Char.IsSeparator(e.KeyChar) And Not Char.IsLetter(e.KeyChar) And Not Char.IsDigit(e.KeyChar)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub InitializeYearMonth()
        Try
            ''Initializing year
            Me.txtYear.Text = Now.Year.ToString
            ''
            ''Initializing month
            Select Case Now.Month
                Case 1
                    Me.cmbMonth.Text = "Jan"
                Case 2
                    Me.cmbMonth.Text = "Feb"
                Case 3
                    Me.cmbMonth.Text = "Mar"
                Case 4
                    Me.cmbMonth.Text = "Apr"
                Case 5
                    Me.cmbMonth.Text = "May"
                Case 6
                    Me.cmbMonth.Text = "Jun"
                Case 7
                    Me.cmbMonth.Text = "Jul"
                Case 8
                    Me.cmbMonth.Text = "Aug"
                Case 9
                    Me.cmbMonth.Text = "Sep"
                Case 10
                    Me.cmbMonth.Text = "Oct"
                Case 11
                    Me.cmbMonth.Text = "Nov"
                Case 12
                    Me.cmbMonth.Text = "Dec"
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function GetMonth(ByVal month As String) As Integer
        Try
            Select Case month
                Case "Jan"
                    Return 1
                Case "Feb"
                    Return 2
                Case "Mar"
                    Return 3
                Case "Apr"
                    Return 4
                Case "May"
                    Return 5
                Case "Jun"
                    Return 6
                Case "Jul"
                    Return 7
                Case "Aug"
                    Return 8
                Case "Sep"
                    Return 9
                Case "Oct"
                    Return 10
                Case "Nov"
                    Return 11
                Case "Dec"
                    Return 12
                Case Else
                    Return 0
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Private Sub txtInvoiceNoToSkip_Leave(sender As Object, e As EventArgs) Handles txtInvoiceNoToSkip.Leave
    '    Try
    '        'If DocumentNo.Length > DocumentNo1.Length Then
    '        '    ShowErrorMessage("Please enter less than or equivalent to " & DocumentNo1.Length & " digits.")
    '        '    txtInvoiceNoToSkip.Focus()
    '        '    Exit Sub
    '        'End If



    '         = GetDocumentNo()
    '        '''
    '        Dim LastIndexOfDash As Integer = DocNo.LastIndexOf("-")
    '        DocumentNo = DocNo.Substring(LastIndexOfDash + 1)
    '        DocumentNo1 = DocNo.Substring(LastIndexOfDash + 1)
    '        Dim DocumentNoPrefix As String = DocNo.Substring(0, LastIndexOfDash + 1)
    '        'Dim LeadingZeros As String = GetLeadingZerosLength(DocumentNo)
    '        Dim LeadingZeros As String = ""
    '        Dim IncrementDocumentNo As String = DocumentNo + 1
    '        If Me.txtInvoiceNoToSkip.Text.Length < DocumentNo.Length Then
    '            Dim shortLength As Integer = DocumentNo.Length - Me.txtInvoiceNoToSkip.Text.Length
    '            For i As Integer = 1 To shortLength
    '                LeadingZeros += "0"
    '            Next
    '        End If
    '        If LeadingZeros.Length > 0 Then
    '            DocumentNo = LeadingZeros & Me.txtInvoiceNoToSkip.Text
    '        Else
    '            DocumentNo = Me.txtInvoiceNoToSkip.Text
    '        End If
    '        'Me.txtInvoiceNo.Text = DocumentNoPrefix & DocumentNo
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Function GetNewDocument() As String
        Try
            Dim NOW As String = DateTime.Now.ToString
            DocPrefix = String.Empty
            If Me.txtInvoiceNoToSkip.Text.Length > 0 Then
                If VoucherType = "Yearly" Then
                    ' Return GetSerialNo(IIf(CompanyBasePrefix = False & IIf(Me.GetPrefix(IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue)).Length = 0, "SI" & Me.cmbCompany.SelectedValue & "-" & Microsoft.VisualBasic.Right(Me.dtpPODate.Value.Year & "-", "SalesMasterTable", "SalesNo")
                    If CompanyBasePrefix = True AndAlso GetPrefix(IIf(Me.cmbCompany.SelectedIndex = -1, 0, Me.cmbCompany.SelectedValue)).Length > 0 Then
                        Return GetSerialNo("" & GetPrefix(Me.cmbCompany.SelectedValue) & "" & "-", "SalesMasterTable", "SalesNo")
                        'Docp = CStr(FirstChar + Microsoft.VisualBasic.Right("00000" + CStr(Serial), 5))
                        'DocPrefix = GetPrefix(Me.cmbCompany.SelectedValue) & "-"
                        DocPrefix = CStr(GetPrefix(Me.cmbCompany.SelectedValue) & "-" + Microsoft.VisualBasic.Right("00000" + Me.txtInvoiceNoToSkip.Text, 5))
                    Else
                        'Return GetSerialNo("SI" & Me.cmbCompany.SelectedValue & "-" + Microsoft.VisualBasic.Right(CInt(Me.txtYear.Text), 2) + "-", "SalesMasterTable", "SalesNo")
                        DocPrefix = "SI" & Me.cmbCompany.SelectedValue & "-" + Microsoft.VisualBasic.Right(CInt(Me.txtYear.Text), 2) + "-" + Microsoft.VisualBasic.Right("00000" + Me.txtInvoiceNoToSkip.Text, 5)
                    End If
                ElseIf VoucherType = "Monthly" Then
                    'Return GetNextDocNo("SI" & Me.cmbCompany.SelectedValue & "-" & Format(DateTime.Now, "yy") & GetMonth(Me.cmbMonth.Text).ToString("00"), 4, "SalesMasterTable", "SalesNo")
                    DocPrefix = "SI" & Me.cmbCompany.SelectedValue & "-" & Microsoft.VisualBasic.Right(CInt(Me.txtYear.Text), 2) & GetMonth(Me.cmbMonth.Text).ToString("00") + "-" + Microsoft.VisualBasic.Right("0000" + Me.txtInvoiceNoToSkip.Text, 4)
                Else
                    'Return GetNextDocNo("SI" & Me.cmbCompany.SelectedValue, 6, "SalesMasterTable", "SalesNo")
                    DocPrefix = "SI" & Me.cmbCompany.SelectedValue & "-" & Microsoft.VisualBasic.Right("000000" + Me.txtInvoiceNoToSkip.Text, 6)
                End If
                Return DocPrefix
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtInvoiceNoToSkip_TextChanged(sender As Object, e As EventArgs) Handles txtInvoiceNoToSkip.TextChanged
        Try
            Me.txtInvoiceNo.Text = GetNewDocument()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtYear_TextChanged(sender As Object, e As EventArgs) Handles txtYear.TextChanged
        Try
            Me.txtInvoiceNo.Text = GetNewDocument()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonth.SelectedIndexChanged
        Try
            Me.txtInvoiceNo.Text = GetNewDocument()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
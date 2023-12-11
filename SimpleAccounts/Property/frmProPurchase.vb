Imports SBDal
Imports SBModel
Imports System.Data
Imports System.Data.SqlClient
Public Class frmProPurchase

    Implements IGeneral
    Dim Id As Integer = 0I
    Dim DetailAcccountId As Integer = 0
    Public Shared PPID As Integer
    Dim Purchase As PropertyPurchaseBE
    Public PurchaseDAL As PropertyPurchaseDAL = New PropertyPurchaseDAL()
    Dim DetailAcccount As COADeail
    Public DetailAcccountDAL As COADetailDAL = New COADetailDAL()
    Dim PropertyTypeId As Integer
    Dim list As List(Of PurchaseTypeBE)
    Dim Tasklist As List(Of TaskBE)
    Dim VoucherNo As String = ""
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Dim date1 As String = ""
    Public EditAble As Integer = 0
    Public DocNo As String = ""
    Public Cost_CenterId As Integer = 0
    Dim Account As Integer = 0 'Val(getConfigValueByType("SellerSubSub").ToString)
    Public PropertyDate As DateTime
    Private Sub frmProPurchase_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                frmPropertySearch.ShowDialog()
            End If
            If (e.KeyCode = Keys.Escape) Then
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim AccountSubSubId As Integer = Val(getConfigValueByType("SellerSubSub").ToString)

            If IsValidate() = True Then
                If PPID = 0 Then

                    If COADetailDAL.GetAccountName(DetailAcccount.DetailTitle, AccountSubSubId) = True Then

                        If msg_Confirm("Account name already exist do you want to Continue with same name") = True Then

                            If DetailAcccountId > 0 Then
                                Purchase.SellerAccountId = DetailAcccountId
                            Else
                                DetailAcccountId = New COADetailDAL().Add(DetailAcccount, AccountSubSubId)
                                Purchase.SellerAccountId = DetailAcccountId
                            End If
                            If PurchaseDAL.Add(Purchase) Then
                                If PurchaseDAL.AddPurchaseType(list) = True Then
                                    If PurchaseDAL.AddTasks(Tasklist) = True Then
                                        msg_Information("Record has been saved successfully.")
                                        ReSetControls()
                                        Me.Close()
                                    End If
                                    'SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtPOSTitle.Text, True)
                                End If
                            End If

                        End If

                    Else

                        If DetailAcccountId > 0 Then
                            Purchase.SellerAccountId = DetailAcccountId
                        Else
                            DetailAcccountId = New COADetailDAL().Add(DetailAcccount, AccountSubSubId)
                            Purchase.SellerAccountId = DetailAcccountId
                        End If
                        If PurchaseDAL.Add(Purchase) Then
                            If PurchaseDAL.AddPurchaseType(list) = True Then
                                If PurchaseDAL.AddTasks(Tasklist) = True Then
                                    msg_Information("Record has been saved successfully.")
                                    ReSetControls()
                                    Me.Close()
                                End If
                                'SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtPOSTitle.Text, True)
                            End If
                        End If

                    End If

                Else

                    If COADetailDAL.GetAccountName(DetailAcccount.DetailTitle, AccountSubSubId) = True Then

                        If msg_Confirm("Account name already exist do you want to Continue with same name") = True Then

                            Dim str As String = ""
                            Dim conn As New SqlConnection(SQLHelper.CON_STR)
                            Dim trans As SqlTransaction
                            If conn.State = ConnectionState.Closed Then
                                conn.Open()
                            End If
                            trans = conn.BeginTransaction

                            If New COADetailDAL().Update(DetailAcccount, trans) Then
                                ShowInformationMessage("Account Title has Updated Successfully")
                            End If
                            trans.Commit()

                            If DetailAcccountId > 0 Then
                                Purchase.SellerAccountId = DetailAcccountId
                            Else
                                DetailAcccountId = New COADetailDAL().Add(DetailAcccount, AccountSubSubId)
                                Purchase.SellerAccountId = DetailAcccountId
                            End If

                            'Purchase.SellerAccountId = DetailAcccountId
                            If PurchaseDAL.Update(Purchase) Then
                                If PurchaseDAL.UpdatePurchaseType(list) = True Then
                                    If PurchaseDAL.UpdateTasks(Tasklist) = True Then
                                        msg_Information("Record has been updated successfully.")
                                        ReSetControls()
                                        Me.Close()
                                    End If
                                End If
                            End If
                        End If

                    Else

                        Dim str As String = ""
                        Dim conn As New SqlConnection(SQLHelper.CON_STR)
                        Dim trans As SqlTransaction
                        If conn.State = ConnectionState.Closed Then
                            conn.Open()
                        End If
                        trans = conn.BeginTransaction

                        If New COADetailDAL().Update(DetailAcccount, trans) Then
                            ShowInformationMessage("Account Title has Updated Successfully")
                        End If
                        trans.Commit()

                        If DetailAcccountId > 0 Then
                            Purchase.SellerAccountId = DetailAcccountId
                        Else
                            DetailAcccountId = New COADetailDAL().Add(DetailAcccount, AccountSubSubId)
                            Purchase.SellerAccountId = DetailAcccountId
                        End If

                        'Purchase.SellerAccountId = DetailAcccountId
                        If PurchaseDAL.Update(Purchase) Then
                            If PurchaseDAL.UpdatePurchaseType(list) = True Then
                                If PurchaseDAL.UpdateTasks(Tasklist) = True Then
                                    msg_Information("Record has been updated successfully.")
                                    ReSetControls()
                                    Me.Close()
                                End If
                            End If
                        End If
                    End If

                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmProPurchase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Account = Val(getConfigValueByType("SellerSubSub").ToString)

            If Account <= 0 Then

                ShowErrorMessage("Please Select a sub sub Account to map Against the Seller")

            End If

            FillCombos()
            Me.txtSerialNo.Select()
            btnSave.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor

            btnCancel.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor

            Dim dt As DataTable
            If (EditAble = -1) Then
                dt = New PropertyPurchaseDAL().UpdateRecord(DocNo)
                If dt.Rows.Count > 0 Then
                    PPID = dt.Rows(0).Item("PropertyPurchaseId")
                Else
                    PPID = 0
                End If
            End If
            dt = New PropertyPurchaseDAL().GetById(PPID)
            'ToolTip
            'Dim dt As DataTable = New PropertyPurchaseDAL().GetById(PPID)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Me.txtTitle.Text = dt.Rows(0).Item("Title")
                    Me.txtPlotNo.Text = dt.Rows(0).Item("PlotNo")
                    Me.txtBlockNo.Text = dt.Rows(0).Item("Block")
                    txtSector.Text = dt.Rows(0).Item("Sector")
                    txtRemarks.Text = dt.Rows(0).Item("Remarks")
                    txtSerialNo.Text = dt.Rows(0).Item("SerialNo")
                    txtLocation.Text = dt.Rows(0).Item("Location")
                    txtCity.Text = dt.Rows(0).Item("City")
                    'Purchase.SellerAccountId = txtCNIC.Text
                    txtPropertyType.Text = dt.Rows(0).Item("PropertyType")
                    txtCellNo.Text = dt.Rows(0).Item("CellNo")
                    cmbName.Text = GetAccountById(Val(dt.Rows(0).Item("SellerAccountId").ToString))
                    txtPrice.Text = dt.Rows(0).Item("Price")
                    VoucherNo = dt.Rows(0).Item("voucher_no").ToString
                    btnSave.Enabled = DoHaveUpdateRights
                    'txtNetPayables.Text = dt.Rows(0).Item("Title")
                    'txtAmountPaid.Text = dt.Rows(0).Item("Title")
                    txtLandArea.Text = dt.Rows(0).Item("PlotSize").ToString
                    If IsDBNull(dt.Rows(0).Item("PurchaseDate")) Then
                        Me.dtpPurchaseDate.Value = Now
                    Else
                        Me.dtpPurchaseDate.Value = dt.Rows(0).Item("PurchaseDate")
                    End If
                Next
            Else
                ReSetControls()
                If EditAble = -1 Then
                    txtSerialNo.Text = DocNo
                    Me.loadRecord()
                End If
            End If
            Ttip.SetToolTip(Me.txtSerialNo, "Enter document serial number")
            Ttip.SetToolTip(Me.txtTitle, "Enter property title")
            Ttip.SetToolTip(Me.txtPlotNo, "Enter property plot number")
            Ttip.SetToolTip(Me.txtBlockNo, "Enter property block number")
            Ttip.SetToolTip(Me.txtSector, "Enter property sector")
            Ttip.SetToolTip(Me.txtPropertyType, "Select property type")
            Ttip.SetToolTip(Me.cmbName, "Select seller account")
            Ttip.SetToolTip(Me.txtCellNo, "Enter property price")
            Ttip.SetToolTip(Me.txtRemarks, "Enter remarks")
            Ttip.SetToolTip(Me.btnCancel, "Click to close the window")
            Ttip.SetToolTip(Me.btnSave, "Click to save the data")
            GetPaymentType(PPID)
            GetTaskTemplate()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Function GetPaymentType(ByVal PPID As Integer) As Boolean
        Try
            Dim str As String
            Dim dt As DataTable
            str = "Select PaymentTypeId, PropertyPurchaseId, PaymentTypeDate, PaymentTypeName, Isnull(Amount, 0) as Amount, Isnull(AmountPaid, 0) as AmountPaid from PaymentType where PropertyPurchaseId = " & PPID & ""
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                grdPayment.DataSource = dt
                grdPayment.RetrieveStructure()
                Me.grdPayment.RootTable.Columns("PaymentTypeId").Visible = False
                Me.grdPayment.RootTable.Columns("PropertyPurchaseId").Visible = False
                Me.grdPayment.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdPayment.RootTable.Columns("AmountPaid").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function GetTaskTemplate() As Boolean
        Try
            Dim str As String
            Dim dt As DataTable
            If PPID > 0 Then
                str = "select TaskId, Name, DueDate, Completed as Status  from TblDefTasks where FormName = 'frmProPurchase' AND Ref_No = N'" & txtSerialNo.Text & "'"
                dt = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    grdTask.DataSource = dt
                    grdTask.RetrieveStructure()
                    Me.grdTask.RootTable.Columns("TaskId").Visible = False
                End If
            Else
                str = "select TaskTemplateDetailId as TaskId, TaskTemplateId, Title as Name,  DueDate, ISNULL(Status, 0) as Status from TaskTemplateDetail where TaskTemplateId = 1"
                dt = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    grdTask.DataSource = dt
                    grdTask.RetrieveStructure()
                    Me.grdTask.RootTable.Columns("TaskTemplateId").Visible = False
                    Me.grdTask.RootTable.Columns("TaskId").Visible = False
                    'If Val(grdTask.RootTable.Columns("Status").ToString) = 0 Then
                    '    'grdTask.RootTable.Columns("Status").edi()
                    'End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtCellNo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCellNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim dt As DataTable

                Dim str As String
                str = "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title AS Name, vwCOADetail.detail_code AS Code, vwCOADetail.account_type AS Type, vwCOADetail.Contact_Mobile AS Mobile, vwCOADetail.CityName AS City, vwCOADetail.Contact_Address AS Address, ISNULL(SUM(tblVoucherDetail.debit_amount - tblVoucherDetail.credit_amount ),0) AS Amount " & _
                       "FROM vwCOADetail LEFT OUTER JOIN tblVoucherDetail ON vwCOADetail.coa_detail_id = tblVoucherDetail.coa_detail_id  " & _
                       "WHERE vwCOADetail.coa_detail_id > 0 AND Contact_Mobile = '" & txtCellNo.Text & "' " & _
                       "GROUP BY vwCOADetail.coa_detail_id, vwCOADetail.detail_title , vwCOADetail.detail_code , vwCOADetail.account_type , vwCOADetail.Contact_Mobile , vwCOADetail.CityName , vwCOADetail.Contact_Address "
                dt = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    cmbName.Text = dt.Rows(0).Item("Name").ToString
                    txtAmountPaid.Text = dt.Rows(0).Item("Amount")
                    DetailAcccountId = dt.Rows(0).Item("Id")
                Else
                    'ShowErrorMessage("Data not found against this Cell No.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub txtSerialNo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSerialNo.KeyDown
    '    Try
    '        Dim str As String
    '        Dim dt As DataTable
    '        If e.KeyCode = Keys.Enter Then
    '            str = "SELECT        PropertyItem.Title, PropertyItem.EntryDate, PropertyItem.PlotNo, PropertyItem.Sector, PropertyItem.Block, PropertyItem.PlotSize,tblListTerritory.TerritoryName, PropertyItem.Ownership, PropertyType.PropertyType, " & _
    '            "PropertyItem.DemandAmount, PropertyItem.Remarks, PropertyItem.Active, PropertyItem.Features, tblListCity.CityName, PropertyItem.PropertyTypeId,PropertyProfile.CostCenterId " & _
    '                "FROM            tblListCity RIGHT OUTER JOIN " & _
    '                     "tblListTerritory ON tblListCity.CityId = tblListTerritory.CityId RIGHT OUTER JOIN " & _
    '                     "PropertyItem ON tblListTerritory.TerritoryId = PropertyItem.TerritoryId LEFT OUTER JOIN " & _
    '                     "PropertyType ON PropertyItem.PropertyTypeId = PropertyType.PropertyTypeId RIGHT OUTER JOIN " & _
    '                     "PropertyProfile ON PropertyItem.PropertyItemId = PropertyProfile.InvId " & _
    '                        "WHERE        (PropertyProfile.DocNo = '" & txtSerialNo.Text & "')"
    '            dt = GetDataTable(str)
    '            If dt.Rows.Count > 0 Then
    '                txtTitle.Text = dt.Rows(0).Item("Title").ToString
    '                txtPlotNo.Text = dt.Rows(0).Item("PlotNo").ToString
    '                txtBlockNo.Text = dt.Rows(0).Item("Block").ToString
    '                txtSector.Text = dt.Rows(0).Item("Sector").ToString
    '                txtLocation.Text = dt.Rows(0).Item("TerritoryName").ToString
    '                txtCity.Text = dt.Rows(0).Item("CityName").ToString
    '                txtPropertyType.Text = dt.Rows(0).Item("PropertyType").ToString
    '                Cost_CenterId = dt.Rows(0).Item("CostCenterId").ToString
    '                txtLandArea.Text = dt.Rows(0).Item("PlotSize").ToString
    '            Else
    '                ShowErrorMessage("Data not found against this Serial No. " & vbCrLf & "Please Enter a Serial No which exists.")
    '                Exit Sub
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub loadRecord()
        Try
            Dim str As String
            Dim dt As DataTable

            str = "SELECT        PropertyItem.Title, PropertyItem.EntryDate, PropertyItem.PlotNo, PropertyItem.Sector, PropertyItem.Block, PropertyItem.PlotSize,tblListTerritory.TerritoryName, PropertyItem.Ownership, PropertyType.PropertyType, " & _
            "PropertyItem.DemandAmount, PropertyItem.Remarks, PropertyItem.Active, PropertyItem.Features, tblListCity.CityName, PropertyItem.PropertyTypeId,PropertyProfile.CostCenterId " & _
                "FROM            tblListCity RIGHT OUTER JOIN " & _
                     "tblListTerritory ON tblListCity.CityId = tblListTerritory.CityId RIGHT OUTER JOIN " & _
                     "PropertyItem ON tblListTerritory.TerritoryId = PropertyItem.TerritoryId LEFT OUTER JOIN " & _
                     "PropertyType ON PropertyItem.PropertyTypeId = PropertyType.PropertyTypeId RIGHT OUTER JOIN " & _
                     "PropertyProfile ON PropertyItem.PropertyItemId = PropertyProfile.InvId " & _
                        "WHERE        (PropertyProfile.DocNo = '" & txtSerialNo.Text & "')"
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                txtTitle.Text = dt.Rows(0).Item("Title").ToString
                txtPlotNo.Text = dt.Rows(0).Item("PlotNo").ToString
                txtBlockNo.Text = dt.Rows(0).Item("Block").ToString
                txtSector.Text = dt.Rows(0).Item("Sector").ToString
                txtLocation.Text = dt.Rows(0).Item("TerritoryName").ToString
                txtCity.Text = dt.Rows(0).Item("CityName").ToString
                txtPropertyType.Text = dt.Rows(0).Item("PropertyType").ToString
                Cost_CenterId = dt.Rows(0).Item("CostCenterId").ToString
                txtLandArea.Text = dt.Rows(0).Item("PlotSize").ToString
            Else
                ShowErrorMessage("Data not found against this Serial No. " & vbCrLf & "Please Enter a Serial No which exists.")
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            str = "select distinct detail_title, detail_title As Name from vwCOADetail where main_sub_sub_id = " & Account & " And detail_title <> ''"
            'FillDropDown(txtName, str, True)
            FillUltraDropDown(cmbName, str, True)
            Me.cmbName.Rows(0).Activate()
            If Me.cmbName.DisplayLayout.Bands.Count > 0 Then
                Me.cmbName.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbName.DisplayLayout.Bands(0).Columns(1).Width = 160
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModelForAccount(Optional Condition As String = "")
        Try
            Dim str As String
            Dim dt As DataTable
            str = "SELECT coa_detail_id FROM vwCOADetail where main_sub_sub_id = " & Account & " and detail_title = '" & cmbName.Text & "'"
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                DetailAcccountId = dt.Rows(0).Item("coa_detail_id")
            Else
                DetailAcccountId = 0
            End If
            DetailAcccount = New COADeail
            DetailAcccount.COADetailID = DetailAcccountId
            DetailAcccount.DetailTitle = Me.cmbName.Text
            'DetailAcccount.Contact_Mobile = Me.txtCellNo.Text
            DetailAcccount.Active = 1

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function GetAccountById(ByVal ID As Integer) As String
        Dim strSQL As String = String.Empty
        Dim DetailAccountTitle As String = String.Empty
        Dim DetailContactMobile As String = String.Empty
        Dim AmountPaid As String = String.Empty
        Try

            strSQL = " SELECT        tblCOAMainSubSubDetail.coa_detail_id, tblCOAMainSubSubDetail.detail_title,  IsNull(SUM(tblVoucherDetail.debit_amount), 0) as Amount ,abs(IsNull(SUM(tblVoucherDetail.debit_amount) - SUM(tblVoucherDetail.credit_amount), 0)) as NetAmount " & _
                        "FROM            tblCOAMainSubSubDetail Left Outer JOIN " & _
                         "tblVoucherDetail ON tblCOAMainSubSubDetail.coa_detail_id = tblVoucherDetail.coa_detail_id where tblCOAMainSubSubDetail.coa_detail_id = " & ID & "" & _
                         "Group by tblCOAMainSubSubDetail.coa_detail_id, tblCOAMainSubSubDetail.detail_title"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)

            If dt.Rows.Count > 0 Then
                DetailAccountTitle = dt.Rows(0).Item("detail_title").ToString()
                AmountPaid = dt.Rows(0).Item("Amount").ToString()
                txtAmountPaid.Text = dt.Rows(0).Item("Amount").ToString()
                txtNetPayables.Text = dt.Rows(0).Item("NetAmount").ToString()
                DetailAcccountId = Val(dt.Rows(0).Item("coa_detail_id").ToString())
            End If

            Return DetailAccountTitle
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Purchase = New PropertyPurchaseBE
            Purchase.PropertyPurchaseId = PPID
            Purchase.PropertyDate = Me.dtpPurchaseDate.Value
            Purchase.Title = Me.txtTitle.Text
            Purchase.PlotNo = Me.txtPlotNo.Text
            Purchase.Block = Me.txtBlockNo.Text
            Purchase.Sector = txtSector.Text
            Purchase.Remarks = txtRemarks.Text
            Purchase.SerialNo = txtSerialNo.Text
            Purchase.Location = txtLocation.Text
            Purchase.City = txtCity.Text
            Purchase.CellNo = txtCellNo.Text
            Purchase.PurchaseId = getConfigValueByType("PropertyPurchaseAccount").ToString
            'Purchase.SellerAccountId = txtCNIC.Text
            Purchase.Price = txtPrice.Text
            Purchase.PropertyType = txtPropertyType.Text
            Purchase.Cost_CenterId = Cost_CenterId
            Purchase.PlotSize = txtLandArea.Text
            If PPID = 0 Then
                Purchase.VoucherNo = GetDocumentNo()
            Else
                If VoucherNo.Length > 0 Then
                    Purchase.VoucherNo = VoucherNo
                Else
                    Purchase.VoucherNo = GetDocumentNo()
                End If
            End If
            Purchase.ActivityLog = New ActivityLog
            list = New List(Of PurchaseTypeBE)
            For i As Integer = 0 To grdPayment.RowCount - 1
                Dim PDetail As New PurchaseTypeBE
                PDetail.PaymentTypeId = grdPayment.GetRows(i).Cells("PaymentTypeId").Value
                date1 = grdPayment.GetRows(i).Cells("PaymentTypeDate").Value.ToString
                If (date1 = "") Then
                    PDetail.PaymentTypeDate = Date.Now.ToString("yyyy-MM-dd hh:mm:ss")
                Else
                    PDetail.PaymentTypeDate = grdPayment.GetRows(i).Cells("PaymentTypeDate").Value.ToString()
                End If
                PDetail.PaymentTypeName = grdPayment.GetRows(i).Cells("PaymentTypeName").Value.ToString
                PDetail.Amount = grdPayment.GetRows(i).Cells("Amount").Value.ToString
                PDetail.AmountPaid = grdPayment.GetRows(i).Cells("AmountPaid").Value.ToString
                'PDetail.PropertyPurchaseId = PPID
                list.Add(PDetail)
            Next
            Tasklist = New List(Of TaskBE)
            For i As Integer = 0 To grdTask.RowCount - 1
                Dim TDetail As New TaskBE
                TDetail.TaskId = grdTask.GetRows(i).Cells("TaskId").Value
                TDetail.Name = grdTask.GetRows(i).Cells("Name").Value.ToString
                'TDetail.PropertyPurchaseId = PPID
                date1 = grdTask.GetRows(i).Cells("DueDate").Value.ToString
                If (date1 = "") Then
                    TDetail.DueDate = Date.Now.ToString("yyyy-MM-dd hh:mm:ss")
                Else
                    TDetail.DueDate = grdTask.GetRows(i).Cells("DueDate").Value.ToString()
                End If
                TDetail.Status = grdTask.GetRows(i).Cells("Status").Value.ToString
                Tasklist.Add(TDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            ''If Me.txtSerialNo.Text = String.Empty Then
            ''    ShowErrorMessage("Please enter Serial No")
            ''    Me.txtname.Focus()
            ''    Return False
            ''End If
            If Me.cmbName.Text = ".... Select Any Value ...." Or Me.cmbName.Text = "" Then
                ShowErrorMessage("Please enter Name")
                Me.cmbName.Focus()
                Return False
            End If
            If Me.txtSerialNo.Text = String.Empty Then
                ShowErrorMessage("Please enter Serial No to Search")
                Me.cmbName.Focus()
                Return False
            End If

            If Me.txtTitle.Text.Length < 1 Then
                ShowErrorMessage("Title is required")
                Me.txtTitle.Focus()
                Return False
            End If
            FillModel()
            If CalculateAmmount() = False Then
                Return False
            End If
            FillModelForAccount()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("PP-" + Microsoft.VisualBasic.Right(dtpPurchaseDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("PP-" & Format(dtpPurchaseDate.Value, "yy") & dtpPurchaseDate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
            Else
                Return GetNextDocNo("PP-", 6, "tblVoucher", "Voucher_No")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateRecord(ByVal docNo As String)

    End Function
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            txtSerialNo.Focus()
            PPID = 0
            Me.txtTitle.Text = ""
            Me.txtPlotNo.Text = ""
            Me.txtBlockNo.Text = ""
            'VoucherNo = GetDocumentNo()
            txtSector.Text = ""
            txtRemarks.Text = ""
            txtSerialNo.Text = ""
            txtLocation.Text = ""
            txtCity.Text = ""
            'Purchase.SellerAccountId = txtCNIC.Text
            txtPrice.Text = ""
            txtPropertyType.Text = ""
            txtCellNo.Text = ""
            cmbName.Rows(0).Activate()
            txtNetPayables.Text = ""
            txtAmountPaid.Text = ""
            txtLandArea.Text = ""
            DetailAcccountId = 0
            btnSave.Enabled = DoHaveSaveRights
            Cost_CenterId = 0
            dtpPurchaseDate.Value = Now
        Catch ex As Exception
            msg_Error(ex.Message)
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

    Private Sub txtPrice_TextChanged(sender As Object, e As EventArgs) Handles txtPrice.TextChanged
        Try
            'txtNetPayables.Text = Val(Val(txtPrice.Text) - Val(txtAmountPaid.Text))
            If txtPrice.Text = "" Then
                lblNumberConverter.Text = ""
            Else
                lblNumberConverter.Text = ModGlobel.NumberToWords(Me.txtPrice.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function CalculateAmmount() As Boolean
        Try
            Dim Sum As Double = 0
            Sum = grdPayment.GetRows(0).Cells("Amount").Value + grdPayment.GetRows(1).Cells("Amount").Value + grdPayment.GetRows(2).Cells("Amount").Value + grdPayment.GetRows(3).Cells("Amount").Value

            If Sum > Me.txtPrice.Text Then
                ShowErrorMessage("Amount should not greater than above enterd amount")
                Return False
            Else

                Return True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    'Private Sub cmbName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbName.SelectedIndexChanged
    '    Try
    '        Dim str As String
    '        Dim dt As DataTable
    '        str = "SELECT PropertyPurchase.CellNo FROM PropertyPurchase INNER JOIN vwCOADetail ON PropertyPurchase.SellerAccountId = vwCOADetail.coa_detail_id where vwCOADetail.main_sub_sub_id = " & Account & " and detail_title = '" & cmbName.Text & "'"
    '        dt = GetDataTable(str)
    '        If dt.Rows.Count > 0 Then
    '            txtCellNo.Text = dt.Rows(0).Item("CellNo")
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub cmbName_ValueChanged(sender As Object, e As EventArgs) Handles cmbName.ValueChanged

        Try
            Dim str As String
            Dim dt As DataTable
            str = "SELECT PropertyPurchase.CellNo FROM PropertyPurchase INNER JOIN vwCOADetail ON PropertyPurchase.SellerAccountId = vwCOADetail.coa_detail_id where vwCOADetail.main_sub_sub_id = " & Account & " and detail_title = '" & cmbName.Text & "'"
            dt = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                txtCellNo.Text = dt.Rows(0).Item("CellNo")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
End Class
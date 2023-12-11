''23-Aug-2014 Task:2799 Imran Ali Add new fields Salesman, Manager, Director In Customer Information (Cotton Craft)
'2015-05-20 Task#20150511 Adding Region,Zone and Belt in Customer Form
Imports System.Data.OleDb

Public Class frmDefCustomer

    Dim CurrentId As Integer
    Dim IsLoadedForm As Boolean = False
    Dim arrFile As List(Of String) ''TFS1854
    ' Task No 2543  Ading New Item And Event To ADD NEW Customer and Apply NumValidation Function For Text Boxes To Validate The Numeric Value

    Sub RefreshForm()
        Try
            Me.BtnSave.Text = "&Save"
            Me.uitxtName.Text = ""
            Me.uitxtName.Focus()
            Me.txtCustomerCode.Text = String.Empty
            If Me.cmbState.SelectedIndex > 0 Then Me.cmbState.SelectedIndex = 0
            If Me.CmbCustomerTypes.SelectedIndex > 0 Then Me.CmbCustomerTypes.SelectedIndex = 0
            Me.uitxtAddress.Text = ""
            Me.uitxtPhone.Text = ""
            Me.uitxtMobile.Text = ""
            Me.uitxtEmail.Text = ""
            Me.uitxtCrditLimit.Text = 0
            Me.txtNTNNo.Text = String.Empty
            Me.txtSalesTaxNo.Text = String.Empty
            Me.uitxtcomments.Text = ""
            Me.txtDiscPer.Text = 0
            Me.TxtFuel.Text = 0
            Me.txtCNG.Text = 0
            Me.TxtOtherExpn.Text = 0
            Me.txtCreditDays.Text = String.Empty
            If Not Me.cmbRootPlan.SelectedIndex = -1 Then Me.cmbRootPlan.SelectedIndex = 0
            Me.uitxtSortOrder.Text = 1
            Me.uichkActive.Checked = True
            Me.chkHold.Checked = False
            Me.dtpExpiryDate.Checked = False
            Me.dtpExpiryDate.Value = Today
            Me.dtpStart.Checked = False
            Me.dtpStart.Value = Today
            'Me.dtpEnd.Checked = False
            'Me.dtpEnd.Value = Today
            Me.fillCombos()

            'Task:2799 Reseting Controls
            If Not Me.cmbSaleman.SelectedIndex = -1 Then Me.cmbSaleman.SelectedIndex = 0
            If Not Me.cmbManager.SelectedIndex = -1 Then Me.cmbManager.SelectedIndex = 0
            If Not Me.cmbDirector.SelectedIndex = -1 Then Me.cmbDirector.SelectedIndex = 0
            'End Task:2799
            'Task#2015051011 Reseting Region,Zone and Belt Combos
            If Not Me.cmbRegion.SelectedIndex = -1 Then Me.cmbRegion.SelectedIndex = 0
            If Not Me.cmbZone.SelectedIndex = -1 Then Me.cmbZone.SelectedIndex = 0
            If Not Me.cmbBelt.SelectedIndex = -1 Then Me.cmbBelt.SelectedIndex = 0
            'Not included in task although Reset Combo box of Province and Report to manager'
            If Not Me.cmbState.SelectedIndex = -1 Then Me.cmbState.SelectedIndex = 0
            If Not Me.cmbManager.SelectedIndex = -1 Then Me.cmbManager.SelectedIndex = 0
            'Task#2015051011 Reseting Region,Zone and Belt Combos
            'AliFaisal : Reset Contact names and numbers on 25-Nov-2016
            Me.uitxtContactNo1Name.Text = String.Empty
            Me.uitxtContactNo2Name.Text = String.Empty
            Me.uitxtContactNo3Name.Text = String.Empty
            Me.uitxtMobile2.Text = String.Empty
            Me.uitxtMobile3.Text = String.Empty
            If Not Me.cmbUnBuildAccount.SelectedIndex = -1 Then Me.cmbUnBuildAccount.SelectedIndex = 0
            ''Start TFSAyesha
            arrFile = New List(Of String) ''TFS1854
            Me.btnAttachments.Text = "Attachment (" & arrFile.Count & ")" ''TFS1854
            ''End TFSAyesha
            Me.BindGrid()
            Me.GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub fillCombos()
        Try
            FillDropDown(Me.cmbState, "Select * from tblListstate")
            'Rafay:Task Start:fill combo 
            FillDropDown(Me.cmbRegion, "Select * from tblListCountry")
            'Rafay:Task End
            FillDropDown(Me.CmbCustomerTypes, "SELECT Typeid, Name FROM dbo.TblDefCustomerType ")
            FillDropDown(Me.cmbRootPlan, "Select RootPlanId, RootPlanName From tblDefRootPlan")
            'Task:2799 Fill Combos
            FillDropDown(Me.cmbSaleman, "Select Employee_Id, Employee_Name, Employee_Code From tblDefEmployee WHERE SalePerson=1 And Active = 1 ORDER BY 2 ASC")
            'FillDropDown(Me.cmbManager, "Select Employee_Id, Employee_Name, Employee_Code From tblDefEmployee WHERE SalePerson <> 1 And Active = 1 ORDER BY 2 ASC")
            'FillDropDown(Me.cmbDirector, "Select Employee_Id, Employee_Name, Employee_Code From tblDefEmployee WHERE SalePerson <> 1 And Active = 1 ORDER BY 2 ASC")
            FillDropDown(Me.cmbManager, "Select Employee_Id, Employee_Name, Employee_Code From tblDefEmployee WHERE  Active = 1 ORDER BY 2 ASC")
            FillDropDown(Me.cmbDirector, "Select Employee_Id, Employee_Name, Employee_Code From tblDefEmployee WHERE  Active = 1 ORDER BY 2 ASC")
            'End Task:2799
            If Me.DataGridView1.RowCount > 0 AndAlso uitxtName.Text <> "" Then
                FillDropDown(Me.cmbUnBuildAccount, "SELECT coa_detail_id, detail_title FROM vwCOADetail WHERE Active = 1 AND account_type = 'Customer' AND coa_detail_id <> " & Val(Me.DataGridView1.GetRow.Cells("Customer").Value) & "")
            Else
                FillDropDown(Me.cmbUnBuildAccount, "SELECT coa_detail_id, detail_title FROM vwCOADetail WHERE Active = 1 AND account_type = 'Customer'")
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub BindGrid(Optional ByVal Condition As String = "")

        Dim adp As New OleDbDataAdapter
        Dim dt As New DataTable
        'Dim Sql As String = "SELECT     TOP 100 PERCENT dbo.tblListTerritory.TerritoryName, dbo.tblListCity.CityId, dbo.tblListCity.CityName, dbo.tblListState.StateId, " & _
        '                    "dbo.tblListState.StateName, dbo.tblCustomer.CustomerID, dbo.tblCustomer.CustomerName, dbo.tblCustomer.Territory, dbo.tblCustomer.Address, dbo.tblCustomer.Phone,  " & _
        '                    "dbo.tblCustomer.Mobile, dbo.tblCustomer.Email, dbo.tblCustomer.Comments, dbo.tblCustomer.Active, dbo.tblCustomer.SortOrder " & _
        '                    "FROM         dbo.tblListState INNER JOIN " & _
        '                    "dbo.tblListCity ON dbo.tblListState.StateId = dbo.tblListCity.StateId INNER JOIN " & _
        '                    "dbo.tblListTerritory ON dbo.tblListCity.CityId = dbo.tblListTerritory.CityId INNER JOIN " & _
        '                    "dbo.tblCustomer ON dbo.tblListTerritory.TerritoryId = dbo.tblCustomer.Territory " & _
        '                    "ORDER BY dbo.tblCustomer.SortOrder"
        Try
            'Added Fields SaleMan, Manager, Director
            'Dim Sql As String = "SELECT dbo.vwCOADetail.coa_detail_id AS CustomerId, dbo.vwCOADetail.detail_title as CustomerName, tblcustomer.CustomerCode, tblcustomer.CustomerTypes, dbo.tblListState.StateName, dbo.tblListCity.CityName,  " & _
            '                "dbo.tblListTerritory.TerritoryName, dbo.tblCustomer.Address, dbo.tblCustomer.Phone, dbo.tblCustomer.Mobile, dbo.tblCustomer.Email, dbo.tblCustomer.cridtlimt,Fuel,CNG,DiscPer,otherExpanses, " & _
            '                   "dbo.tblCustomer.Comments, tblCustomer.CustomerNTNNo as [NTN No], tblCustomer.CustomerSalesTaxNo as [SalesTax No], isnull(dbo.tblListCity.CityId,0) as CityId, isnull(dbo.tblListState.StateId,0) as StateId, isnull(dbo.tblListTerritory.TerritoryId,0) as Territory , tblCustomer.ExpiryDate, isnull(dbo.tblCustomer.Active,1) as Active, isnull(dbo.tblCustomer.SortOrder,1) as sortOrder, ISNULL(RootPlanId,0) as RootPlanId, ISNULL(tblCustomer.SaleMan,0) as SaleMan, ISNULL(tblCustomer.Manager,0) as Manager, ISNULL(tblCustomer.Director,0) as Director " & _
            '                "FROM         dbo.tblCustomer Left Outer JOIN " & _
            '                "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId Left Outer JOIN " & _
            '                "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId Left Outer JOIN " & _
            '                "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id "
            'End Task:2799
            'Marked Against Task#20150511 Ali Ansari adding zone,region,belt
            'Dim Sql As String = "SELECT dbo.vwCOADetail.coa_detail_id AS CustomerId, dbo.vwCOADetail.detail_title as CustomerName, tblcustomer.CustomerCode, tblcustomer.CustomerTypes, tblDefCustomerType.Name as [Customer Type], dbo.tblListState.StateName, dbo.tblListCity.CityName,  " & _
            '               "dbo.tblListTerritory.TerritoryName, dbo.tblCustomer.Address, dbo.tblCustomer.Phone, dbo.tblCustomer.Mobile, dbo.tblCustomer.Email, dbo.tblCustomer.cridtlimt,Fuel,CNG,DiscPer,otherExpanses, " & _
            '                  "dbo.tblCustomer.Comments, tblCustomer.CustomerNTNNo as [NTN No], tblCustomer.CustomerSalesTaxNo as [SalesTax No], isnull(dbo.tblListCity.CityId,0) as CityId, isnull(dbo.tblListState.StateId,0) as StateId, isnull(dbo.tblListTerritory.TerritoryId,0) as Territory , tblCustomer.ExpiryDate, isnull(dbo.tblCustomer.Active,1) as Active, isnull(dbo.tblCustomer.SortOrder,1) as sortOrder, ISNULL(RootPlanId,0) as RootPlanId, ISNULL(tblCustomer.SaleMan,0) as SaleMan, ISNULL(tblCustomer.Manager,0) as Manager, ISNULL(tblCustomer.Director,0) as Director " & _
            '               "FROM         dbo.tblCustomer LEFT OUTER JOIN tblDefCustomerType On tblDefCustomerType.TypeId = tblCustomer.CustomerTypes Left Outer JOIN " & _
            '               "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId Left Outer JOIN " & _
            '               "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId Left Outer JOIN " & _
            '               "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '               "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id "
            'Marked Against Task#20150511 Ali Ansari adding zone,region,belt
            'Altered Against Task#20150511 Ali Ansari adding zone,region,belt
            'AliFaisal : Added Contact Names and Numbers to history grid on 25-Nov-2016
            'Rafay:Modified query to add country (Change name region to Country)
            Dim Sql As String = "SELECT dbo.vwCOADetail.coa_detail_id AS CustomerId,dbo.tblCustomer.CustomerID As [Identity], dbo.vwCOADetail.detail_title as CustomerName, tblcustomer.CustomerCode, tblcustomer.CustomerTypes, tblDefCustomerType.Name as [Customer Type], IsNull([No_of_Attachment],0) as [No_of_Attachment], dbo.tblListState.StateName, dbo.tblListCity.CityName,  " & _
                                     "dbo.tblListTerritory.TerritoryName,dbo.tblListCountry.CountryName,  dbo.tblListzone.ZoneName, dbo.tblListbelt.BeltName, dbo.tblCustomer.Address, dbo.tblCustomer.Phone, dbo.tblCustomer.ContactName1 AS PersonName, dbo.tblCustomer.Mobile, dbo.tblCustomer.Email, dbo.tblCustomer.cridtlimt,Fuel,CNG,DiscPer,otherExpanses, " & _
                                        "dbo.tblCustomer.Comments, tblCustomer.CustomerNTNNo as [NTN No], tblCustomer.CustomerSalesTaxNo as [SalesTax No], isnull(dbo.tblListCity.CityId,0) as CityId, isnull(dbo.tblListState.StateId,0) as StateId, isnull(dbo.tblListTerritory.TerritoryId,0) as Territory , isnull(dbo.tblListCountry.CountryId,0) as CountryId , isnull(dbo.tblListZone.ZoneId,0) as ZoneId ,isnull(dbo.tblListbelt.beltId,0) as BeltId ,tblCustomer.ExpiryDate, isnull(dbo.tblCustomer.Active,1) as Active, isnull(dbo.tblCustomer.Hold,0) as Hold, isnull(dbo.tblCustomer.SortOrder,1) as sortOrder, ISNULL(RootPlanId,0) as RootPlanId, ISNULL(tblCustomer.SaleMan,0) as SaleMan, ISNULL(tblCustomer.Manager,0) as Manager, ISNULL(tblCustomer.Director,0) as Director, tblCustomer.StartDate, tblCustomer.EndDate, IsNull(tblCustomer.CreditDays,0) as CreditDays, tblCustomer.ContactName2 AS PersonName1, tblCustomer.ContactName3 AS PersonName2, tblCustomer.ContactNo2 AS ContactNo1, tblCustomer.ContactNo3 AS ContactNo2 , tblCustomer.UnBuiltAccountId " & _
                                     "FROM  dbo.tblCustomer LEFT OUTER JOIN tblDefCustomerType On tblDefCustomerType.TypeId = tblCustomer.CustomerTypes Left Outer JOIN " & _
                                     "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId Left Outer JOIN " & _
                                     "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId Left Outer JOIN " & _
                                     "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                     "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                     "left join dbo.tblListCountry on dbo.tblcustomer.CountryId = tblListCountry.CountryId " & _
                                     "left join dbo.tbllistzone on dbo.tblcustomer.Zoneid = tbllistzone.Zoneid " & _
                                     "left join dbo.tbllistBelt on dbo.tblcustomer.beltid = tbllistbelt.beltid " & _
                                     "LEFT OUTER JOIN(Select Count(*) as [No_of_Attachment], DocId From DocumentAttachment WHERE Source='" & Me.Name & "' Group By DocId) Att On Att.DocId =  dbo.tblCustomer.AccountId"

            'Altered Against Task#20150511 Ali Ansari adding zone,region,belt

            If Me.ToolStripComboBox1.ComboBox.SelectedIndex > 0 Then
                'Task No 2543  Update Query To Geting Data In Ordered Sorted Form
                Sql = Sql + " WHERE     (dbo.vwCOADetail.account_type = 'Vendor') and dbo.vwCOADetail.coa_detail_id is not null   order by tblCustomer.CustomerId DESC "

            Else
                'Task No 2543  Update Query To Geting Data In Ordered Sorted Form

                Sql = Sql + " WHERE     (dbo.vwCOADetail.account_type = 'Customer') and dbo.vwCOADetail.coa_detail_id is not null order by tblCustomer.CustomerId DESC --tblCustomer.Sortorder, vwCOADetail.detail_title "

            End If
            adp = New OleDbDataAdapter(Sql, Con)
            adp.Fill(dt)
            Me.DataGridView1.DataSource = dt
            Me.DataGridView1.RetrieveStructure()

            Me.DataGridView1.RootTable.Columns("CustomerId").Visible = False
            Me.DataGridView1.RootTable.Columns("Identity").Visible = False
            Me.DataGridView1.RootTable.Columns("CityId").Visible = False
            Me.DataGridView1.RootTable.Columns("StateId").Visible = False
            Me.DataGridView1.RootTable.Columns("Territory").Visible = False
            Me.DataGridView1.RootTable.Columns("RootPlanId").Visible = False
            Me.DataGridView1.RootTable.Columns("CustomerTypes").Visible = False
            'Task:2799 Set Hidden Columns
            Me.DataGridView1.RootTable.Columns("Saleman").Visible = False
            Me.DataGridView1.RootTable.Columns("Manager").Visible = False
            Me.DataGridView1.RootTable.Columns("Director").Visible = False
            'End Task:2799
            'Altered Against Task#20150511 Ali Ansari adding zone,region,belt
            'Rafay :task STart :Replace regionid into countryid
            Me.DataGridView1.RootTable.Columns("CountryId").Visible = False
            'Rafay:Task End
            Me.DataGridView1.RootTable.Columns("ZoneId").Visible = False
            Me.DataGridView1.RootTable.Columns("BeltId").Visible = False
            'Altered Against Task#20150511 Ali Ansari adding zone,region,belt
            Me.DataGridView1.RootTable.Columns("UnBuiltAccountId").Visible = False
            Me.DataGridView1.RootTable.Columns("No_of_Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.DataGridView1.RootTable.Columns("No_of_Attachment").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.DataGridView1.RootTable.Columns("No_of_Attachment").Caption = "No Of Attachments"
            CtrlGrdBar1_Load(Nothing, Nothing)

            Me.DataGridView1.AutoSizeColumns()

        Catch ex As Exception
            ShowErrorMessage("Error occured while loading customers: " & ex.Message)
        End Try

    End Sub
    Sub EditRecord()
        Try
            If Not Val(DataGridView1.CurrentRow.Cells("CustomerId").Value.ToString) > 0 Then Exit Sub
        Me.uitxtName.Text = DataGridView1.CurrentRow.Cells("CustomerName").Value
        Me.txtCustomerCode.Text = DataGridView1.CurrentRow.Cells("CustomerCode").Value.ToString
        Me.CmbCustomerTypes.SelectedValue = Val(DataGridView1.CurrentRow.Cells("CustomerTypes").Value.ToString)
            Me.cmbState.SelectedValue = DataGridView1.CurrentRow.Cells("StateId").Value
            'Rafay:Task Start:give Countryid to cmbregion because  we replace region to country 
            Me.cmbRegion.SelectedValue = DataGridView1.CurrentRow.Cells("CountryId").Value
            'Rafay:Task End
        Me.cmbCity.SelectedValue = DataGridView1.CurrentRow.Cells("CityId").Value.ToString
        Me.cmbTerritory.SelectedValue = DataGridView1.CurrentRow.Cells("Territory").Value.ToString
        Me.uitxtAddress.Text = DataGridView1.CurrentRow.Cells("Address").Value.ToString
        Me.uitxtPhone.Text = DataGridView1.CurrentRow.Cells("Phone").Value.ToString
        Me.uitxtMobile.Text = DataGridView1.CurrentRow.Cells("Mobile").Value.ToString
        Me.uitxtCrditLimit.Text = DataGridView1.CurrentRow.Cells("CridtLimt").Value.ToString
        Me.uitxtEmail.Text = DataGridView1.CurrentRow.Cells("EMail").Value.ToString
        Me.uitxtcomments.Text = DataGridView1.CurrentRow.Cells("Comments").Value.ToString
        Me.uitxtSortOrder.Text = DataGridView1.CurrentRow.Cells("SortOrder").Value.ToString
            Me.uichkActive.Checked = IIf(DataGridView1.CurrentRow.Cells("Active").Value = 0, False, True)
            Me.chkHold.Checked = IIf(DataGridView1.CurrentRow.Cells("Hold").Value = 0, False, True)
        Me.CurrentId = DataGridView1.CurrentRow.Cells("CustomerId").Value

        Me.dtpExpiryDate.Value = IIf(IsDBNull(DataGridView1.CurrentRow.Cells("ExpiryDate").Value), DateTime.Today, DataGridView1.CurrentRow.Cells("ExpiryDate").Value)
        Me.dtpExpiryDate.Checked = IIf(IsDBNull(DataGridView1.CurrentRow.Cells("ExpiryDate").Value), False, True)
        Me.dtpStart.Value = IIf(IsDBNull(DataGridView1.CurrentRow.Cells("StartDate").Value), DateTime.Today, DataGridView1.CurrentRow.Cells("StartDate").Value)
        Me.dtpStart.Checked = IIf(IsDBNull(DataGridView1.CurrentRow.Cells("StartDate").Value), False, True)
        'Me.dtpExpiryDate.Value = IIf(IsDBNull(DataGridView1.CurrentRow.Cells("EndDate").Value), DateTime.Today, DataGridView1.CurrentRow.Cells("EndDate").Value)
        'Me.dtpExpiryDate.Checked = IIf(IsDBNull(DataGridView1.CurrentRow.Cells("EndDate").Value), False, True)

        Me.txtDiscPer.Text = DataGridView1.CurrentRow.Cells("Discper").Value.ToString
        Me.TxtFuel.Text = DataGridView1.CurrentRow.Cells("Fuel").Value.ToString
        Me.TxtOtherExpn.Text = DataGridView1.CurrentRow.Cells("otherExpanses").Value.ToString
        Me.txtNTNNo.Text = DataGridView1.GetRow.Cells("NTN No").Text.ToString
        Me.txtSalesTaxNo.Text = DataGridView1.GetRow.Cells("SalesTax No").Text.ToString
        Me.cmbRootPlan.SelectedValue = Val(DataGridView1.GetRow.Cells("RootPlanId").Text.ToString)
        Me.txtCNG.Text = Val(DataGridView1.GetRow.Cells("CNG").Text.ToString)

        'Task:2799 Retrieve Record 
        Me.cmbSaleman.SelectedValue = DataGridView1.GetRow.Cells("SaleMan").Text.ToString
        Me.cmbManager.SelectedValue = DataGridView1.GetRow.Cells("Manager").Text.ToString
        Me.cmbDirector.SelectedValue = Val(DataGridView1.GetRow.Cells("Director").Text.ToString)
        'End Task:2799
            'Task#20150511 Edit Ids Columns of Region, Zone, Belt''''
            'rafay :'Rafay:Task Start:give Countryid to cmbregion because  we replace region to country 
            Me.cmbRegion.SelectedValue = DataGridView1.CurrentRow.Cells("CountryId").Value.ToString
            'rafay: Task End
        Me.cmbZone.SelectedValue = DataGridView1.CurrentRow.Cells("ZoneId").Value.ToString
        Me.cmbBelt.SelectedValue = DataGridView1.CurrentRow.Cells("BeltId").Value.ToString
        'Task#20150511 Edit Ids Columns of Region, Zone, Belt''''
        Me.txtCreditDays.Text = Val(Me.DataGridView1.GetRow.Cells("CreditDays").Value.ToString)
        'AliFaisal : Retrieve record from history of all contact names and numbers on 25-Nov-2016
        Me.uitxtContactNo1Name.Text = Me.DataGridView1.GetRow.Cells("PersonName").Text.ToString
        Me.uitxtContactNo2Name.Text = Me.DataGridView1.GetRow.Cells("PersonName1").Text.ToString
        Me.uitxtContactNo3Name.Text = Me.DataGridView1.GetRow.Cells("PersonName2").Text.ToString
        Me.uitxtMobile2.Text = Me.DataGridView1.GetRow.Cells("ContactNo1").Text.ToString
        Me.uitxtMobile3.Text = Me.DataGridView1.GetRow.Cells("ContactNo2").Text.ToString
        Me.cmbUnBuildAccount.SelectedValue = Val(Me.DataGridView1.GetRow.Cells("UnBuiltAccountId").Value.ToString)
        Dim intCountAttachedFiles As Integer = 0I
        ''Start TFS2864
        If Me.BtnSave.Text <> "&Save" Then
            If Me.DataGridView1.RowCount > 0 Then
                intCountAttachedFiles = Val(DataGridView1.CurrentRow.Cells("No_of_Attachment").Value)
                Me.btnAttachments.Text = "Attachment (" & intCountAttachedFiles & ")"
            End If
        End If
        ''End TFS2864
            Me.BtnSave.Text = "&Update"
            Me.GetSecurityRights()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmDefCustomer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmDefType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ToolStripComboBox1.ComboBox.SelectedIndex = 0
        Me.RefreshForm()
        IsLoadedForm = True
        Get_All(frmModProperty.Tags)
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        If Not Me.DataGridView1.GetRow Is Nothing Then
            Me.BtnSave.Text = "&Update"
            Me.EditRecord()
        End If
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Me.RefreshForm()
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim objCommand As New OleDbCommand
        Dim objTrans As OleDbTransaction

        ''Aashir: Validate if teritory have selected before save or Update
        'start
        'rafay comment

        'If (Me.cmbState.SelectedIndex > 1 Or Me.cmbCity.SelectedIndex > 1) And Me.cmbTerritory.SelectedIndex < 1 Then
        '    msg_Error("Please select Territory")
        '    Exit Sub
        'End If


        'end

        If Me.uitxtName.Text = "" Then
            msg_Error("Please enter valid name")
            Me.uitxtName.Focus()
            Exit Sub
        End If
        'Ali Faisal: Code commented to remove restrictions on info save 
        'If Me.cmbState.SelectedIndex = 0 Then
        '    msg_Error("Please select a state")
        '    Me.cmbState.Focus()
        '    Exit Sub
        'End If

        'If Me.cmbCity.SelectedIndex = 0 Then
        '    msg_Error("Please select a city")
        '    Me.cmbCity.Focus()
        '    Exit Sub
        'End If

        'If Me.cmbTerritory.SelectedIndex = 0 Then
        '    msg_Error("Please select a territory")
        '    Me.cmbTerritory.Focus()
        '    Exit Sub
        'End If
        If Me.BtnSave.Text = "&Save" Then
            If Not msg_Confirm(str_ConfirmSave) = True Then
                Me.uitxtName.Focus()
                Exit Sub
            End If
        Else
            If Not msg_Confirm(str_ConfirmUpdate) = True Then
                Me.uitxtName.Focus()
                Exit Sub
            End If
        End If
        If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
        objTrans = Con.BeginTransaction
        Try

            objCommand.Connection = Con
            'If Me.SaveToolStripButton.Text = "&Save" Or Me.SaveToolStripButton.Text = "&Save" Then
            objCommand.CommandText = "delete from tblCustomer where AccountId=" & Me.CurrentId
            objCommand.Transaction = objTrans
            objCommand.ExecuteNonQuery()
            objCommand = New OleDbCommand
            objCommand.Connection = Con
            'objTrans = Con.BeginTransaction
            'Before against task:2799 
            'objCommand.CommandText = "insert into tblCustomer(accountId,CustomerName,CustomerTypes, Territory,Address,Phone,Mobile,Fuel,Discper,OtherExpanses,cridtlimt,Email,Comments,sortorder, Active, ExpiryDate, CustomerNTNNo, CustomerSalesTaxNo, CustomerCode,RootPlanId,CNG) values(" & Me.CurrentId & ",N'" & Me.uitxtName.Text & "', " & Me.CmbCustomerTypes.SelectedValue & "," & Me.cmbTerritory.SelectedValue & ",N'" & Me.uitxtAddress.Text & "',N'" & Me.uitxtPhone.Text & "',N'" & Me.uitxtMobile.Text & "',N'" & Me.TxtFuel.Text & "',N'" & Me.txtDiscPer.Text & "',N'" & Me.TxtOtherExpn.Text & "'," & Val(Me.uitxtCrditLimit.Text) & ",N'" & Me.uitxtEmail.Text & "',N'" & Me.uitxtcomments.Text & "',N'" & Me.uitxtSortOrder.Text & "'," & IIf(Me.uichkActive.Checked = False, 0, 1) & ", " & IIf(Me.dtpExpiryDate.Checked, "N'" & Me.dtpExpiryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & Me.txtNTNNo.Text & "', N'" & Me.txtSalesTaxNo.Text & "',N'" & Me.txtCustomerCode.Text & "'," & Me.cmbRootPlan.SelectedValue & ",N'" & Me.txtCNG.Text & "') Select @@Identity"
            'ask:2799 Added Fields, SaleMan,MAnager,Director
            'Marked against task#20150511 adding region,zone and belt by Ali Ansari
            '            objCommand.CommandText = "insert into tblCustomer(accountId,CustomerName,CustomerTypes, Territory,Address,Phone,Mobile,Fuel,Discper,OtherExpanses,cridtlimt,Email,Comments,sortorder, Active, ExpiryDate, CustomerNTNNo, CustomerSalesTaxNo, CustomerCode,RootPlanId,CNG,SaleMan,Manager,Director) values(" & Me.CurrentId & ",N'" & Me.uitxtName.Text & "', " & Me.CmbCustomerTypes.SelectedValue & "," & Me.cmbTerritory.SelectedValue & ",N'" & Me.uitxtAddress.Text & "',N'" & Me.uitxtPhone.Text & "',N'" & Me.uitxtMobile.Text & "',N'" & Me.TxtFuel.Text & "',N'" & Me.txtDiscPer.Text & "',N'" & Me.TxtOtherExpn.Text & "'," & Val(Me.uitxtCrditLimit.Text) & ",N'" & Me.uitxtEmail.Text & "',N'" & Me.uitxtcomments.Text & "',N'" & Me.uitxtSortOrder.Text & "'," & IIf(Me.uichkActive.Checked = False, 0, 1) & ", " & IIf(Me.dtpExpiryDate.Checked, "N'" & Me.dtpExpiryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & Me.txtNTNNo.Text & "', N'" & Me.txtSalesTaxNo.Text & "',N'" & Me.txtCustomerCode.Text & "'," & Me.cmbRootPlan.SelectedValue & ",N'" & Me.txtCNG.Text & "', " & Me.cmbSaleman.SelectedValue & ", " & Me.cmbManager.SelectedValue & "," & Me.cmbDirector.SelectedValue & ") Select @@Identity"
            'Marked against task#20150511 adding region,zone and belt by Ali Ansari
            'End Task:2799

            'Altered against task#20150511 adding region,zone and belt by Ali Ansari
            '' objCommand.CommandText = "insert into tblCustomer(accountId,CustomerName,CustomerTypes, Territory,Address,Phone,Mobile,Fuel,Discper,OtherExpanses,cridtlimt,Email,Comments,sortorder, Active, ExpiryDate, CustomerNTNNo, CustomerSalesTaxNo, CustomerCode,RootPlanId,CNG,SaleMan,Manager,Director,RegionId,ZoneId,BeltId, StartDate, EndDate) values(" & Me.CurrentId & ",N'" & Me.uitxtName.Text & "', " & Me.CmbCustomerTypes.SelectedValue & "," & Me.cmbTerritory.SelectedValue & ",N'" & Me.uitxtAddress.Text & "',N'" & Me.uitxtPhone.Text & "',N'" & Me.uitxtMobile.Text & "',N'" & Me.TxtFuel.Text & "',N'" & Me.txtDiscPer.Text & "',N'" & Me.TxtOtherExpn.Text & "'," & Val(Me.uitxtCrditLimit.Text) & ",N'" & Me.uitxtEmail.Text & "',N'" & Me.uitxtcomments.Text & "',N'" & Me.uitxtSortOrder.Text & "'," & IIf(Me.uichkActive.Checked = False, 0, 1) & ", " & IIf(Me.dtpExpiryDate.Checked, "N'" & Me.dtpExpiryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & Me.txtNTNNo.Text & "', N'" & Me.txtSalesTaxNo.Text & "',N'" & Me.txtCustomerCode.Text & "'," & Me.cmbRootPlan.SelectedValue & ",N'" & Me.txtCNG.Text & "', " & Me.cmbSaleman.SelectedValue & ", " & Me.cmbManager.SelectedValue & "," & Me.cmbDirector.SelectedValue & "," & Me.cmbRegion.SelectedValue & ", " & Me.cmbZone.SelectedValue & ", " & Me.cmbBelt.SelectedValue & " , " & IIf(Me.dtpStart.Checked, "N'" & Me.dtpStart.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & " , " & IIf(Me.dtpExpiryDate.Checked, "N'" & Me.dtpExpiryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ") Select @@Identity"
            'AliFaisal : Alter to save contact names and numbers on 25-Nov-2016
            objCommand.CommandText = "insert into tblCustomer(accountId, CustomerName, CustomerTypes, Territory, Address, Phone, Mobile, Fuel, Discper, OtherExpanses, cridtlimt, Email, Comments, sortorder, Active, ExpiryDate, CustomerNTNNo, CustomerSalesTaxNo, CustomerCode, RootPlanId, CNG, SaleMan, Manager, Director, CountryId, ZoneId, BeltId, StartDate, EndDate, ContactName1, ContactName2, ContactName3, ContactNo2, ContactNo3, CreditDays, UnBuiltAccountId, Hold) values(" & Me.CurrentId & ",N'" & Me.uitxtName.Text & "', " & Me.CmbCustomerTypes.SelectedValue & "," & Me.cmbTerritory.SelectedValue & ",N'" & Me.uitxtAddress.Text & "',N'" & Me.uitxtPhone.Text & "',N'" & Me.uitxtMobile.Text & "',N'" & Me.TxtFuel.Text & "',N'" & Me.txtDiscPer.Text & "',N'" & Me.TxtOtherExpn.Text & "'," & Val(Me.uitxtCrditLimit.Text) & ",N'" & Me.uitxtEmail.Text & "',N'" & Me.uitxtcomments.Text & "',N'" & Me.uitxtSortOrder.Text & "'," & IIf(Me.uichkActive.Checked = False, 0, 1) & ", " & IIf(Me.dtpExpiryDate.Checked, "N'" & Me.dtpExpiryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ", N'" & Me.txtNTNNo.Text & "', N'" & Me.txtSalesTaxNo.Text & "',N'" & Me.txtCustomerCode.Text & "'," & Me.cmbRootPlan.SelectedValue & ",N'" & Me.txtCNG.Text & "', " & Me.cmbSaleman.SelectedValue & ", " & Me.cmbManager.SelectedValue & "," & Me.cmbDirector.SelectedValue & ", N'" & Me.cmbRegion.SelectedValue & "', N'" & Me.cmbZone.SelectedValue & "', N'" & Me.cmbBelt.SelectedValue & "', " & IIf(Me.dtpStart.Checked, "N'" & Me.dtpStart.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & " , " & IIf(Me.dtpExpiryDate.Checked, "N'" & Me.dtpExpiryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ",N'" & Me.uitxtContactNo1Name.Text & "', N'" & Me.uitxtContactNo2Name.Text & "', N'" & Me.uitxtContactNo3Name.Text & "', N'" & Me.uitxtMobile2.Text & "', N'" & Me.uitxtMobile3.Text & "',N'" & Me.txtCreditDays.Text & "', " & Me.cmbUnBuildAccount.SelectedValue & "," & IIf(Me.chkHold.Checked = False, 0, 1) & ") Select @@Identity"
            'Altered against task#20150511 adding region,zone and belt by Ali Ansari
            ', " & IIf(Me.dtpExpiryDate.Checked, "N'" & Me.dtpExpiryDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'", "NULL") & ",

            'Else
            'cm.CommandText = "update tblCustomer set CustomerName=N'" & Me.uitxtName.Text & "',Territory =" & Me.cmbTerritory.SelectedValue & ",Address =N'" & Me.uitxtAddress.Text & "',Phone=N'" & Me.uitxtPhone.Text & "',Mobile=N'" & Me.uitxtMobile.Text & "',Email=N'" & Me.uitxtEmail.Text & "',Comments=N'" & Me.uitxtcomments.Text & "', sortorder=N'" & Me.uitxtSortOrder.Text & "',Active=" & IIf(Me.uichkActive.Checked = False, 0, 1) & " where CustomerId=" & Me.CurrentId
            'End If
            objCommand.Transaction = objTrans
            Dim identity As Integer = Convert.ToInt32(objCommand.ExecuteScalar())
            ''TFSAyesha 
            If arrFile IsNot Nothing Then

                If arrFile.Count > 0 Then
                    SaveDocument(CurrentId, Me.Name, objTrans)
                End If

            End If
            ''End TFSAyesha
            objTrans.Commit()
            'MsgBox("Record Saved Successfully", MsgBoxStyle.Information, str_MessageHeader)
            msg_Information(str_informSave)
            Me.CurrentId = 0


            Try
                ''insert Activity Log
                SaveActivityLog("Config", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", identity, Me.DataGridView1.CurrentRow.Cells("CustomerId").Value.ToString), True)
            Catch ex As Exception

            End Try

            Me.RefreshForm()
        Catch ex As Exception
            objTrans.Rollback()
            ShowErrorMessage("Error occured while saving record: " & ex.Message)
        Finally
            Con.Close()
        End Try

    End Sub
    ''' <summary>
    ''' This Function Is Added to save Attachments with documents
    ''' </summary>
    ''' <param name="DocId"></param>
    ''' <param name="Source"></param>
    ''' <param name="objTrans"></param>
    ''' <returns>Ayesha Rehman : 27-03-2018</returns>
    ''' <remarks></remarks>
    Public Function SaveDocument(ByVal DocId As Integer, ByVal Source As String, ByVal objTrans As OleDb.OleDbTransaction) As Boolean
        Dim cmd As New OleDbCommand
        cmd.Connection = objTrans.Connection
        cmd.Transaction = objTrans
        Try

            Dim dt As New DataTable
            dt = GetDataTable("Select DocId, Source, Path + '\' + FileName  as [FileNames]  From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            dt.AcceptChanges()


            Dim objdt As New DataTable
            objdt = GetDataTable("Select IsNull(Count(*),0)+1 as Cont From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'", objTrans)
            Dim intId As Integer = objdt.Rows(0)(0)

            Dim strSQL As String = String.Empty
            cmd.CommandText = String.Empty
            strSQL = "Delete From DocumentAttachment WHERE DocId=" & DocId & " AND Source=N'" & Source & "'"
            cmd.CommandText = strSQL
            cmd.ExecuteNonQuery()

            Dim objPath As String = getConfigValueByType("FileAttachmentPath").ToString
            'Altered Against Task#2015060001 Ali Ansari
            If arrFile.Count > 0 Then
                'Altered Against Task#2015060001 Ali Ansari
                'Marked Against Task#2015060001 Ali Ansari
                '            If arrFile.Length > 0 Then
                'Marked Against Task#2015060001 Ali Ansari
                For Each objFile As String In arrFile
                    If IO.File.Exists(objFile) Then
                        If IO.Directory.Exists(objPath) = False Then
                            IO.Directory.CreateDirectory(objPath)
                        End If
                        Dim New_Files As String = intId & "_" & DocId & "_" & Date.Now.ToString("yyyyMMdd") & "." & objFile.Substring(objFile.LastIndexOf(".") + 1)
                        Dim dr As DataRow
                        dr = dt.NewRow
                        dr(0) = DocId
                        dr(1) = Source
                        dr(2) = objPath & "\" & New_Files
                        dt.Rows.Add(dr)
                        dt.AcceptChanges()
                        If IO.File.Exists(objPath & "\" & New_Files) Then
                            IO.File.Delete(objPath & "\" & New_Files)
                        End If
                        IO.File.Copy(objFile, objPath & "\" & New_Files)
                        intId += 1
                    End If
                Next
            End If


            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    For Each r As DataRow In dt.Rows
                        Dim strPath As String = objPath
                        Dim strFileName As String = r.Item("FileNames").ToString.Substring(r.Item("FileNames").ToString.LastIndexOf("\") + 1)
                        cmd.CommandText = String.Empty
                        strSQL = "INSERT INTO DocumentAttachment(DocId, Source, FileName, Path) Values(" & Val(r("DocId").ToString) & ",N'" & r.Item("Source").ToString.Replace("'", "''") & "', N'" & strFileName.Replace("'", "''") & "', N'" & strPath.Replace("'", "''") & "')"
                        cmd.CommandText = strSQL
                        cmd.ExecuteNonQuery()
                    Next
                End If
            End If


        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function

    Private Sub cmbState_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbState.SelectedIndexChanged
        Try
            FillDropDown(Me.cmbCity, "Select * from tblListCity where StateId=" & Me.cmbState.SelectedValue)
            'Task#20150511 Ali Ansari Fill Combos of Region, Zone and Belt
            'FillDropDown(Me.cmbRegion, "Select * from tblListregion where stateId=" & Me.cmbState.SelectedValue)
            'Task#20150511 Ali Ansari Fill Combos of Region, Zone and Belt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbCity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCity.SelectedIndexChanged
        Try
            FillDropDown(Me.cmbTerritory, "Select * from tblListTerritory where CityId=" & Me.cmbCity.SelectedValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DeleteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Not DataGridView1.RowCount > 0 Then
            msg_Error(str_ErrorNoRecordFound)
            Exit Sub
        End If
        If IsValidToDelete("SalesMasterTable", "CustomerCode", Me.DataGridView1.CurrentRow.Cells("CustomerId").Value.ToString) = True And IsValidToDelete("SalesOrderMasterTable", "VendorId", Me.DataGridView1.CurrentRow.Cells("CustomerId").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                Try
                    Dim cm As New OleDbCommand

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from tblCustomer where CustomerId=" & Me.DataGridView1.CurrentRow.Cells("CustomerId").Value.ToString
                    cm.ExecuteNonQuery()
                    msg_Information(str_informDelete)

                    Try
                        ''insert Activity Log
                        SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.DataGridView1.CurrentRow.Cells("CustomerId").Value.ToString, True)
                    Catch ex As Exception

                    End Try

                    Me.CurrentId = 0
                Catch ex As Exception
                    msg_Error("Error occured while deleting record: " & ex.Message)
                Finally
                    Con.Close()
                End Try
                Me.RefreshForm()
            End If
        Else
            msg_Error(str_ErrorDependentRecordFound)
        End If
    End Sub
    'Private Sub DataGridView1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
    '    'Me.DataGridView1.
    '    'Me.EditRecord()
    '    'Me.UltraTabControl1.SelectedTab = UltraTabPageControl1.Tab
    'End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefCustomer)
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
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub


    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        Me.BindGrid()
    End Sub

    Private Sub CmbCustomerTypes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTerritory.SelectedIndexChanged
        'FillDropDown(Me.CmbCustomerTypes, "Select * from tblDefcustomerTypes where TypeId=" & Me.CmbCustomerTypes.SelectedIndex)

    End Sub
    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If Not Me.DataGridView1.GetRow Is Nothing Then
            Me.BtnSave.Text = "&Update"
            Me.EditRecord()
            Me.UltraTabControl1.SelectedTab = UltraTabPageControl1.Tab
        End If
    End Sub
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From tblCustomer WHERE CustomerId=N'" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        If Not Val(dt.Rows(0).Item("CustomerId").ToString) > 0 Then Exit Function
                        Me.uitxtName.Text = dt.Rows(0).Item("CustomerName").ToString
                        Me.CmbCustomerTypes.SelectedValue = Val(dt.Rows(0).Item("CustomerTypes").ToString)
                        Me.cmbState.SelectedValue = dt.Rows(0).Item("StateId").ToString
                        Me.cmbCity.SelectedValue = dt.Rows(0).Item("CityId").ToString
                        Me.cmbTerritory.SelectedValue = dt.Rows(0).Item("Territory").ToString
                        Me.uitxtAddress.Text = dt.Rows(0).Item("Address").ToString
                        Me.uitxtPhone.Text = dt.Rows(0).Item("Phone").ToString
                        Me.uitxtMobile.Text = dt.Rows(0).Item("Mobile").ToString
                        Me.uitxtCrditLimit.Text = dt.Rows(0).Item("CridtLimt").ToString
                        Me.uitxtEmail.Text = dt.Rows(0).Item("EMail").ToString
                        Me.uitxtcomments.Text = dt.Rows(0).Item("Comments").ToString
                        Me.uitxtSortOrder.Text = dt.Rows(0).Item("SortOrder").ToString
                        Me.uichkActive.Checked = IIf(dt.Rows(0).Item("Active") = 0, False, True)
                        Me.chkHold.Checked = IIf(dt.Rows(0).Item("Hold") = 0, False, True)
                        Me.CurrentId = dt.Rows(0).Item("CustomerId").Value

                        Me.dtpExpiryDate.Value = IIf(IsDBNull(dt.Rows(0).Item("ExpiryDate")), DateTime.Today, dt.Rows(0).Item("ExpiryDate"))
                        Me.dtpExpiryDate.Checked = IIf(IsDBNull(dt.Rows(0).Item("ExpiryDate")), False, True)
                        Me.dtpStart.Value = IIf(IsDBNull(dt.Rows(0).Item("StartDate")), DateTime.Today, dt.Rows(0).Item("StartDate"))
                        Me.dtpStart.Checked = IIf(IsDBNull(dt.Rows(0).Item("StartDate")), False, True)
                        'Me.dtpEnd.Value = IIf(IsDBNull(dt.Rows(0).Item("EndDate")), DateTime.Today, dt.Rows(0).Item("EndDate"))
                        'Me.dtpEnd.Checked = IIf(IsDBNull(dt.Rows(0).Item("EndDate")), False, True)

                        Me.txtDiscPer.Text = dt.Rows(0).Item("Discper").Value.ToString
                        Me.TxtFuel.Text = dt.Rows(0).Item("Fuel").ToString
                        Me.txtCNG.Text = dt.Rows(0).Item("CNG").ToString
                        Me.TxtOtherExpn.Text = dt.Rows(0).Item("otherExpanses").ToString
                        Me.txtNTNNo.Text = DataGridView1.GetRow.Cells("NTN No").ToString
                        Me.txtSalesTaxNo.Text = DataGridView1.GetRow.Cells("SalesTax No").ToString
                        Me.cmbRootPlan.SelectedValue = Val(DataGridView1.GetRow.Cells("RootPlanId").ToString)
                        'Altered By Ali Ansari against Task#20150511 displaying Zone,Region and Belt
                        'Rafay:Task Start:give Countryid to cmbregion because  we replace region to country (Task:waqar raza)
                        Me.cmbRegion.SelectedValue = Val(dt.Rows(0).Item("CountryId").ToString)
                        'Rafay:Task ENd
                        Me.cmbZone.SelectedValue = Val(dt.Rows(0).Item("ZoneID").ToString)
                        Me.cmbBelt.SelectedValue = Val(dt.Rows(0).Item("BeltID").ToString)
                        'Altered By Ali Ansari against Task#20150511 displaying Zone,Region and Belt
                        Me.cmbUnBuildAccount.SelectedValue = Val(dt.Rows(0).Item("UnBuiltAccountId").ToString)
                        Me.BtnSave.Text = "&Update"
                        Me.GetSecurityRights()
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
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbState.SelectedValue
            FillDropDown(Me.cmbState, "Select * from tblListstate")
            Me.cmbState.SelectedValue = id
            id = Me.CmbCustomerTypes.SelectedValue
            FillDropDown(Me.CmbCustomerTypes, "SELECT Typeid, Name " _
     & "FROM dbo.TblDefCustomerType ")
            Me.CmbCustomerTypes.SelectedValue = id
            id = Me.cmbCity.SelectedValue
            FillDropDown(Me.cmbCity, "Select * from tblListCity where STateId=" & Me.cmbState.SelectedValue)
            Me.cmbCity.SelectedValue = id
            id = Me.cmbTerritory.SelectedValue
            FillDropDown(Me.cmbTerritory, "Select * from tblListTerritory where CityId=" & Me.cmbCity.SelectedValue)
            Me.cmbTerritory.SelectedValue = id
            id = Me.cmbRootPlan.SelectedValue
            FillDropDown(Me.cmbRootPlan, "Select RootPlanId, RootPlanName From tblDefRootPlan")
            Me.cmbRootPlan.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHeader.Click

    End Sub
    ' Task No 2543  Ading New Item And Event To ADD NEW Customer
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewCustomr.Click

        'frmMain.LoadControl("AddCustomer")
        FrmAddCustomers.FormType = "Customer"
        FrmAddCustomers.ShowDialog()
        BindGrid()
        DataGridView1_DoubleClick(Nothing, Nothing)



    End Sub

    'Task No 2543  Apply NumValidation Function For Text Box To Validate The Numeric Value
    Private Sub txtDiscPer_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscPer.KeyPress
        Try
            NumValidation(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Task No 2543  Apply NumValidation Function For Text Box To Validate The Numeric Value
    Private Sub TxtOtherExpn_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtOtherExpn.KeyPress
        Try
            NumValidation(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Task No 2543  Apply NumValidation Function For Text Box To Validate The Numeric Value

    Private Sub TxtFuel_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFuel.KeyPress
        Try
            NumValidation(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Task No 2543  Apply NumValidation Function For Text Box To Validate The Numeric Value

    Private Sub txtCNG_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCNG.KeyPress
        Try
            NumValidation(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Task No 2543  Apply NumValidation Function For Text Box To Validate The Numeric Value

    Private Sub uitxtSortOrder_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles uitxtSortOrder.KeyPress
        Try
            NumValidation(sender, e)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load

        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.DataGridView1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.DataGridView1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.DataGridView1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.CtrlGrdBar1.Visible = True
            Else
                Me.CtrlGrdBar1.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        Try
            'Task#20150511 Ali Ansari Fill Combos of Region, Zone and Belt
            'Rafay:Task Start :Fill Combo of 
            FillDropDown(Me.cmbState, "Select * from tblListState where CountryId=" & Me.cmbRegion.SelectedValue)
            ' Rafay:Tasks End
            'Task#20150511 Ali Ansari Fill Combos of Region, Zone and Belt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbZone_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbZone.SelectedIndexChanged
        Try
            'Task#20150511 Ali Ansari Fill Combos of Region, Zone and Belt
            FillDropDown(Me.cmbBelt, "Select * from tblListbelt where ZoneId=" & Me.cmbZone.SelectedValue)
            'Task#20150511 Ali Ansari Fill Combos of Region, Zone and Belt
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click

    End Sub

    Private Sub btnPrintCustomerWelcome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintCustomerWelcome.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.DataGridView1.RowCount = 0 Then Exit Sub
            'ShowReport("rptwelcomeletter", , , , , DataGridView1.GetRow.Cells("customerid").Value.ToString)
            AddRptParam("@CustID", DataGridView1.GetRow.Cells("customerid").Value.ToString)
            ShowReport("rptwelcomeletter")
            'ShowReport("rptwelcomeletter", , DataGridView1.GetRow.Cells("customerid").Value.ToString, , False, , , , False, , , )
            'ShowReport("rptwelcomeletter", DataGridView1.GetRow.Cells("customerid").Value.ToString, , False, , , , , , , , )
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtCreditDays_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCreditDays.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub uitxtAddress_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub uichkActive_CheckedChanged(sender As Object, e As EventArgs)

    End Sub


    'WAQAR RAZA: I have redesigned this form and i added 5 more txtboxes named as: 
    '(uitxtContactNo1Name)
    '(uitxtContactNo2Name) 
    '(uitxtContactNo3Name)
    '(uitxtMobile2) 
    '(uitxtMobile3)

    Private Sub cmbSaleman_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSaleman.SelectedIndexChanged

    End Sub
    Private Sub cmbManager_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbManager.SelectedIndexChanged

    End Sub
    Private Sub cmbDirector_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDirector.SelectedIndexChanged

    End Sub
    Private Sub cmbBelt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBelt.SelectedIndexChanged

    End Sub
    ''' <summary>
    ''' This Event is Added for Attachments to the Customers
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAttachments_Click(sender As Object, e As EventArgs) Handles btnAttachments.Click
        Try
            SetAttachments()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TASK TFS1854
    ''' </summary>
    ''' <remarks> This function handle Attachment on Agreemnet Screen </remarks>


    Private Sub SetAttachments()
        Try
            Dim intCountAttachedFiles As Integer = 0I
            OpenFileDialog1.FileName = String.Empty
            OpenFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|Portable Document Format|*.pdf|Corel Draw Files|*.cdr|All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG|" + _
            "All files (.)|*.*"
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim a As Integer = 0I
                For a = 0 To OpenFileDialog1.FileNames.Length - 1
                    arrFile.Add(OpenFileDialog1.FileNames(a))
                Next a
                If Me.BtnSave.Text <> "&Save" Then
                    If Me.DataGridView1.RowCount > 0 Then
                        intCountAttachedFiles = Val(DataGridView1.CurrentRow.Cells("No_of_Attachment").Value)
                    End If
                End If
                Me.btnAttachments.Text = "Attachment (" & arrFile.Count + intCountAttachedFiles & ")"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles DataGridView1.LinkClicked
        Try
            If e.Column.Key = "No_of_Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                frm._VoucherId = Val(Me.DataGridView1.GetRow.Cells("CustomerId").Value.ToString)
                frm.ShowDialog()
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub uitxtAddress_Enter(sender As Object, e As EventArgs) Handles uitxtAddress.Enter

    End Sub

    Private Sub uitxtAddress_KeyDown(sender As Object, e As KeyEventArgs) Handles uitxtAddress.KeyDown

    End Sub

    Private Sub uitxtAddress_TextChanged_1(sender As Object, e As EventArgs) Handles uitxtAddress.TextChanged

    End Sub
End Class
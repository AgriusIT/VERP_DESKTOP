''19-Dec-2013  ReqId-883 Imran Ali            limit on advance Vendor payments
''28-Dec-2013 RM6 Imran Ali        Release 2.1.0.0 Bug
' Task No 2543  Ading New Item And Event To ADD NEW Vendor  and  Apply NumValidation Function For Text Boxes To Validate The Numeric Value
'20-May-2014 Task:2639 JUNAID vendor type in vendor information.
'21-May-2015 Task#20150511 Ali Ansari adding Region,Zone and Belt
Imports System.Data.OleDb
Public Class frmDefVendor
    Dim CurrentId As Integer
    'Rafay:Task start
    Dim arrFile As List(Of String)
    'Rafay: Task End
    Public Shared IsLoadedForm As Boolean = False
    Sub RefreshForm()
        Try
            Me.BtnSave.Text = "&Save"
            Me.uitxtName.Text = ""
            Me.uitxtName.Focus()
            If Me.cmbState.SelectedIndex > 0 Then Me.cmbState.SelectedIndex = 0
            'task 2639
            If Me.cmbVendorType.SelectedIndex > 0 Then Me.cmbVendorType.SelectedIndex = 0
            'end task 2639
            Me.uitxtAddress.Text = ""
            ''Aashir: 4448 :Added Brand Text Feild
            'start
            Me.txtBrand.Text = ""
            'end
            Me.uitxtPhone.Text = ""
            Me.uitxtMobile.Text = ""
            Me.uitxtEmail.Text = ""
            Me.txtNTNNo.Text = String.Empty
            Me.txtSalesTaxNo.Text = String.Empty
            Me.uitxtcomments.Text = ""
            Me.uitxtSortOrder.Text = 1
            Me.uichkActive.Checked = True
            Me.txtCreditLimit.Text = String.Empty 'ReqId-883 Reset Credit Limit
            Me.txtPayeeTitle.Text = String.Empty
            'Reseting Region,Zone and Belt Combos
            If Me.cmbRegion.SelectedIndex > 0 Then Me.cmbZone.SelectedIndex = 0
            If Me.cmbZone.SelectedIndex > 0 Then Me.cmbRegion.SelectedIndex = 0
            If Me.cmbBelt.SelectedIndex > 0 Then Me.cmbBelt.SelectedIndex = 0
            'Reseting Region,Zone and Belt Combos
            'Ali Faisal : TFS2213 : Add new field Credit Days
            Me.txtCreditDays.Text = 0
            'Ali Faisal : TFS2213 : End
            'Rafay ":Task Start"
            arrFile = New List(Of String) ''TFS1854
            Me.btnAttachments.Text = "Attachment (" & arrFile.Count & ")"
            'Rafay":Task End

            Me.fillCombos()
            Me.BindGrid()
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub fillCombos()
        Try
            FillDropDown(Me.cmbState, "Select * from tblListstate")
            FillDropDown(Me.cmbVendorType, "Select * from tblVendorType")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''TFS3684 : Ayesha Rehman : 27-06-2018 : Added Public to use this function from another form
    Public Sub BindGrid()
        Try
            Dim adp As New OleDbDataAdapter
            Dim dt As New DataTable
            'Dim Sql As String = "SELECT     TOP 100 PERCENT dbo.tblListTerritory.TerritoryName, dbo.tblListCity.CityId, dbo.tblListCity.CityName, dbo.tblListState.StateId, " & _
            '                    "dbo.tblListState.StateName, dbo.tblVendor.VendorID, dbo.tblVendor.VendorName, dbo.tblVendor.Territory, dbo.tblVendor.Address, dbo.tblVendor.Phone,  " & _
            '                    "dbo.tblVendor.Mobile, dbo.tblVendor.Email, dbo.tblVendor.Comments, dbo.tblVendor.Active, dbo.tblVendor.SortOrder " & _
            '                    "FROM         dbo.tblListState INNER JOIN " & _
            '                    "dbo.tblListCity ON dbo.tblListState.StateId = dbo.tblListCity.StateId INNER JOIN " & _
            '                    "dbo.tblListTerritory ON dbo.tblListCity.CityId = dbo.tblListTerritory.CityId INNER JOIN " & _
            '                    "dbo.tblVendor ON dbo.tblListTerritory.TerritoryId = dbo.tblVendor.Territory " & _
            '                    "ORDER BY dbo.tblVendor.SortOrder"
            ''Before against Request no 883
            'Dim Sql As String = "SELECT     dbo.vwCOADetail.coa_detail_id AS VendorId, dbo.vwCOADetail.detail_title as VendorName, dbo.tblListState.StateName, dbo.tblListCity.CityName,  " & _
            '                    "dbo.tblListTerritory.TerritoryName, dbo.tblVendor.Address, dbo.tblVendor.Phone, dbo.tblVendor.Mobile, dbo.tblVendor.Email, " & _
            '                    "dbo.tblVendor.Comments, tblVendor.VENDORNTNNO as [NTN No], tblVendor.VENDORSALESTAXNO as [SalesTax No], isnull(dbo.tblListCity.CityId,0) as CityId, isnull(dbo.tblListState.StateId,0) as StateId, isnull(dbo.tblListTerritory.TerritoryId,0) as Territory , isnull(dbo.tblVendor.Active,1) as Active, isnull(dbo.tblVendor.SortOrder,1) as sortOrder " & _
            '                    "FROM         dbo.tblVendor INNER JOIN " & _
            '                    "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '                    "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '                    "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                    "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                    "WHERE     (dbo.vwCOADetail.account_type = 'Vendor') and vwCOADetail.coa_detail_id is not Null order by tblVendor.Sortorder, vwCOADetail.detail_title "

            ''ReqId-883 Added Column Credit_Limit
            ''Before against request no.M6
            'Dim Sql As String = "SELECT     dbo.vwCOADetail.coa_detail_id AS VendorId, dbo.vwCOADetail.detail_title as VendorName, dbo.tblListState.StateName, dbo.tblListCity.CityName,  " & _
            '                   "dbo.tblListTerritory.TerritoryName, dbo.tblVendor.Address, dbo.tblVendor.Phone, dbo.tblVendor.Mobile, dbo.tblVendor.Email, " & _
            '                   "dbo.tblVendor.Comments, tblVendor.VENDORNTNNO as [NTN No], tblVendor.VENDORSALESTAXNO as [SalesTax No], isnull(dbo.tblListCity.CityId,0) as CityId, isnull(dbo.tblListState.StateId,0) as StateId, isnull(dbo.tblListTerritory.TerritoryId,0) as Territory , isnull(dbo.tblVendor.Active,1) as Active, isnull(dbo.tblVendor.SortOrder,1) as sortOrder, Isnull(dbo.tblVendor.Credit_Limit,0) as Credit_Limit " & _
            '                   "FROM         dbo.tblVendor INNER JOIN " & _
            '                   "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
            '                   "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
            '                   "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                   "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                   "WHERE     (dbo.vwCOADetail.account_type = 'Vendor') and vwCOADetail.coa_detail_id is not Null order by tblVendor.Sortorder, vwCOADetail.detail_title "
            'RM6: Change Joins
            'Dim Sql As String = "SELECT     dbo.vwCOADetail.coa_detail_id AS VendorId, dbo.vwCOADetail.detail_title as VendorName, dbo.tblListState.StateName, dbo.tblListCity.CityName,  " & _
            '                  "dbo.tblListTerritory.TerritoryName, dbo.tblVendor.Address, dbo.tblVendor.Phone, dbo.tblVendor.Mobile, dbo.tblVendor.Email, " & _
            '                  "dbo.tblVendor.Comments, tblVendor.VENDORNTNNO as [NTN No], tblVendor.VENDORSALESTAXNO as [SalesTax No], isnull(dbo.tblListCity.CityId,0) as CityId, isnull(dbo.tblListState.StateId,0) as StateId, isnull(dbo.tblListTerritory.TerritoryId,0) as Territory , isnull(dbo.tblVendor.Active,1) as Active, isnull(dbo.tblVendor.SortOrder,1) as sortOrder, Isnull(dbo.tblVendor.Credit_Limit,0) as Advance_Limit " & _
            '                  "FROM         dbo.tblVendor LEFT OUTER JOIN " & _
            '                  "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
            '                  "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId LEFT OUTER JOIN " & _
            '                  "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                  "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                  "WHERE     (dbo.vwCOADetail.account_type = 'Vendor') and vwCOADetail.coa_detail_id is not Null order by tblVendor.Sortorder, vwCOADetail.detail_title"
            ''End R:M6
            'Task No 2543 Update The Query For Getting Data In Ordered Form and Delete the Orderd By Old Parameters By new One Parameter tblVendor.VendorId DESC "
            'Dim Sql As String = "SELECT     dbo.vwCOADetail.coa_detail_id AS VendorId, dbo.vwCOADetail.detail_title as VendorName, dbo.tblListState.StateName, dbo.tblListCity.CityName,  " & _
            '                 "dbo.tblListTerritory.TerritoryName, dbo.tblVendor.Address, dbo.tblVendor.Phone, dbo.tblVendor.Mobile, dbo.tblVendor.Email, " & _
            '                 "dbo.tblVendor.Comments, tblVendor.VENDORNTNNO as [NTN No], tblVendor.VENDORSALESTAXNO as [SalesTax No], isnull(dbo.tblListCity.CityId,0) as CityId, isnull(dbo.tblListState.StateId,0) as StateId, isnull(dbo.tblListTerritory.TerritoryId,0) as Territory , isnull(dbo.tblVendor.Active,1) as Active, isnull(dbo.tblVendor.SortOrder,1) as sortOrder, Isnull(dbo.tblVendor.Credit_Limit,0) as Advance_Limit " & _
            '                 "FROM         dbo.tblVendor LEFT OUTER JOIN " & _
            '                 "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
            '                 "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId LEFT OUTER JOIN " & _
            '                 "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '                 "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '                 "WHERE     (dbo.vwCOADetail.account_type = 'Vendor') and vwCOADetail.coa_detail_id is not Null order by tblVendor.VendorId DESC "
            ' ''End R:M6
            'Dim Sql As String = "SELECT dbo.vwCOADetail.coa_detail_id AS VendorId, dbo.vwCOADetail.detail_title as VendorName, dbo.tblListState.StateName, dbo.tblListCity.CityName, " & _
            '     "dbo.tblListTerritory.TerritoryName, dbo.tblVendor.Address, dbo.tblVendor.Phone, dbo.tblVendor.Mobile, dbo.tblVendor.Email, " & _
            '     "dbo.tblVendor.Comments, tblVendor.VENDORNTNNO as [NTN No], tblVendor.VENDORSALESTAXNO as [SalesTax No], isnull(dbo.tblListCity.CityId,0) as CityId, isnull(dbo.tblListState.StateId,0) as StateId, isnull(dbo.tblListTerritory.TerritoryId,0) as Territory , isnull(dbo.tblVendor.Active,1) as Active, isnull(dbo.tblVendor.SortOrder,1) as sortOrder, Isnull(dbo.tblVendor.Credit_Limit,0) as Advance_Limit, Isnull(dbo.tblVendor.VendorsTypeId,0) as VendorsTypeId " & _
            '     "FROM         dbo.tblVendor LEFT OUTER JOIN " & _
            '     "dbo.tblVendorType ON tblVendor.VendorsTypeId=tblVendorType.VendorTypeId LEFT OUTER JOIN " & _
            '     "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
            '     "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId LEFT OUTER JOIN " & _
            '     "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '     "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '     "WHERE     (dbo.vwCOADetail.account_type = 'Vendor') and vwCOADetail.coa_detail_id is not Null order by tblVendor.VendorId DESC "
            'End R:M6
            'Marked against task# 20150511 Ali Ansari
            'Dim Sql As String = "SELECT dbo.vwCOADetail.coa_detail_id AS VendorId, dbo.vwCOADetail.detail_title as VendorName, tblVendorType.VendorType as [Vendor Type], dbo.tblListState.StateName, dbo.tblListCity.CityName, " & _
            '     "dbo.tblListTerritory.TerritoryName, dbo.tblVendor.Address, dbo.tblVendor.Phone, dbo.tblVendor.Mobile, dbo.tblVendor.Email, " & _
            '     "dbo.tblVendor.Comments, tblVendor.VENDORNTNNO as [NTN No], tblVendor.VENDORSALESTAXNO as [SalesTax No], isnull(dbo.tblListCity.CityId,0) as CityId, isnull(dbo.tblListState.StateId,0) as StateId, isnull(dbo.tblListTerritory.TerritoryId,0) as Territory , isnull(dbo.tblVendor.Active,1) as Active, isnull(dbo.tblVendor.SortOrder,1) as sortOrder, Isnull(dbo.tblVendor.Credit_Limit,0) as Advance_Limit, Isnull(dbo.tblVendor.VendorsTypeId,0) as VendorsTypeId, tblVendor.PayeeTitle " & _
            '     "FROM         dbo.tblVendor LEFT OUTER JOIN " & _
            '     "dbo.tblVendorType ON tblVendor.VendorsTypeId=tblVendorType.VendorTypeId LEFT OUTER JOIN " & _
            '     "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
            '     "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId LEFT OUTER JOIN " & _
            '     "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
            '     "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
            '     "WHERE     (dbo.vwCOADetail.account_type = 'Vendor') and vwCOADetail.coa_detail_id is not Null order by tblVendor.VendorId DESC "
            'Marked against task# 20150511 Ali Ansari
            'Altered against task# 20150511 get region,zone and belt Ali Ansari
            'Ali Faisal : TFS2213 : Add new field Credit Days
            'Ayesha Rehman : TFS3684 : Change Query order by coa_detail_id to get the newly added vendor at top of the history grid
            ''Aashir: 4448 :Added Brand Text Feild
            Dim Sql As String = "SELECT dbo.vwCOADetail.coa_detail_id AS VendorId, dbo.vwCOADetail.detail_title as VendorName, tblVendorType.VendorType ,IsNull([No_of_Attachment],0) as [No_of_Attachment], dbo.tblListState.StateName, dbo.tblListCity.CityName, " & _
                " isnull(dbo.tblListRegion.RegionId,0) as RegionID , isnull(dbo.tblListZone.ZoneId,0) as ZoneId ,isnull(dbo.tblListbelt.beltId,0) as BeltId ,dbo.tblListregion.RegionName,  dbo.tblListzone.ZoneName, dbo.tblListbelt.BeltName, " & _
                "dbo.tblListTerritory.TerritoryName, dbo.tblVendor.Address, dbo.tblVendor.Phone, dbo.tblVendor.Mobile, dbo.tblVendor.Email, " & _
                "dbo.tblVendor.Comments, tblVendor.VENDORNTNNO as [NTN No], tblVendor.VENDORSALESTAXNO as [SalesTax No], isnull(dbo.tblListCity.CityId,0) as CityId, isnull(dbo.tblListState.StateId,0) as StateId, isnull(dbo.tblListTerritory.TerritoryId,0) as Territory , isnull(dbo.tblVendor.Active,1) as Active, isnull(dbo.tblVendor.SortOrder,1) as sortOrder, Isnull(dbo.tblVendor.Credit_Limit,0) as Advance_Limit, Isnull(dbo.tblVendor.VendorsTypeId,0) as VendorsTypeId, tblVendor.PayeeTitle, ISNULL(tblVendor.CreditDays,0) AS CreditDays, tblVendor.Brand " & _
                "FROM         dbo.tblVendor LEFT OUTER JOIN " & _
                "dbo.tblVendorType ON tblVendor.VendorsTypeId=tblVendorType.VendorTypeId LEFT OUTER JOIN " & _
                "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId LEFT OUTER JOIN " & _
                "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                "left join tbllistregion on dbo.tblVendor.regionid = tbllistregion.regionid " & _
                "left join tbllistzone on dbo.tblVendor.zoneid = tbllistzone.zoneid " & _
                "left join tbllistbelt on dbo.tblvendor.beltid = tbllistbelt.beltid " & _
             "LEFT JOIN(Select Count(*) as [No_of_Attachment], DocId From DocumentAttachment WHERE Source='" & Me.Name & "' Group By DocId) Att On Att.DocId = dbo.tblVendor.VendorId " & _
                "WHERE     (dbo.vwCOADetail.account_type = 'Vendor') and vwCOADetail.coa_detail_id is not Null order by vwCOADetail.coa_detail_id DESC "
            '"LEFT JOIN(Select Count(*) as [No_of_Attachment], DocId From DocumentAttachment WHERE Source='" & Me.Name & "' Group By DocId) Att On Att.DocId = dbo.tblVendor.VendorId" & _
            ''IsNull([No_of_Attachment],0) as [No_of_Attachment]
            Me.DataGridView1.RootTable.Columns("RegionID").Visible = False
            Me.DataGridView1.RootTable.Columns("ZoneId").Visible = False
            Me.DataGridView1.RootTable.Columns("BeltId").Visible = False
            'rafay:To Add column no of attachment in data grid 
            'Me.DataGridView1.RootTable.Columns("No_of_Attachment").Visible = True
            Me.DataGridView1.RootTable.Columns("No_of_Attachment").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.DataGridView1.RootTable.Columns("No_of_Attachment").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.DataGridView1.RootTable.Columns("No_of_Attachment").Caption = "No Of Attachments"
            'rafay
            'Altered against task# 20150511 get region,zone and belt Ali Ansari
            'Ali Faisal : TFS2213 : End
            adp = New OleDbDataAdapter(Sql, Con)
            adp.Fill(dt)
            Me.DataGridView1.DataSource = dt
            'Me.DataGridView1.RetrieveStructure()
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.DataGridView1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub EditRecord()
        Try

            Me.uitxtName.Text = DataGridView1.CurrentRow.Cells("Name").Value.ToString
            Me.cmbState.SelectedValue = DataGridView1.CurrentRow.Cells("StateId").Value.ToString
            'task 2639
            Me.cmbVendorType.SelectedValue = DataGridView1.CurrentRow.Cells("VendorsTypeId").Value.ToString
            'Me.cmbVendorType.ValueMember = "VendorTypeId"
            'Me.cmbVendorType.DisplayMember = "VendorType"
            'end task 2639
            Me.cmbCity.SelectedValue = DataGridView1.CurrentRow.Cells("CityId").Value.ToString
            Me.cmbTerritory.SelectedValue = DataGridView1.CurrentRow.Cells("Territoryid").Value
            Me.uitxtAddress.Text = DataGridView1.CurrentRow.Cells("Address").Value.ToString
            Me.uitxtPhone.Text = DataGridView1.CurrentRow.Cells("Phone").Value.ToString
            Me.uitxtMobile.Text = DataGridView1.CurrentRow.Cells("Mobile").Value.ToString
            Me.uitxtEmail.Text = DataGridView1.CurrentRow.Cells("E Mail").Value.ToString
            Me.uitxtcomments.Text = DataGridView1.CurrentRow.Cells("Comments").Value.ToString
            Me.uitxtSortOrder.Text = DataGridView1.CurrentRow.Cells("SortOrder").Value.ToString
            Me.uichkActive.Checked = IIf(DataGridView1.CurrentRow.Cells("Active").Value = 0, False, True)
            Me.CurrentId = Me.DataGridView1.CurrentRow.Cells("VendorId").Value.ToString
            Me.txtNTNNo.Text = Me.DataGridView1.GetRow.Cells("NTN No").Text.ToString
            Me.txtSalesTaxNo.Text = Me.DataGridView1.GetRow.Cells("SalesTax No").Text.ToString
            Me.txtCreditLimit.Text = Val(Me.DataGridView1.GetRow.Cells("Advance_Limit").Text.ToString)
            Me.txtPayeeTitle.Text = Me.DataGridView1.GetRow.Cells("PayeeTitle").Value.ToString
            'Altered against Task#20150511 edit region,zone and belt
            Me.cmbRegion.SelectedValue = DataGridView1.GetRow.Cells("RegionId").Value.ToString
            Me.cmbZone.SelectedValue = DataGridView1.GetRow.Cells("ZoneId").Value.ToString
            Me.cmbBelt.SelectedValue = DataGridView1.GetRow.Cells("BeltId").Value.ToString
            'Altered against Task#20150511 edit region,zone and belt
            'Ali Faisal : TFS2213 : Add new field Credit Days
            Me.txtCreditDays.Text = DataGridView1.GetRow.Cells("CreditDays").Value.ToString
            'Ali Faisal : TFS2213 : End
            ''Aashir: 4448 :Added Brand Text Feild
            'start
            Me.txtBrand.Text = DataGridView1.GetRow.Cells("Brand").Value.ToString
            'end\
            Dim intCountAttachedFiles As Integer = 0I
            'Rafay:Task Start
            If Me.BtnSave.Text <> "&Save" Then
                If Me.DataGridView1.RowCount > 0 Then
                    intCountAttachedFiles = Val(DataGridView1.CurrentRow.Cells("No_of_Attachment").Value)
                    Me.btnAttachments.Text = "Attachment (" & intCountAttachedFiles & ")"
                End If
                'Rafay:Task end
            End If
            Me.BtnSave.Text = "&Update"
            GetSecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmDefVendor_BindingContextChanged(sender As Object, e As EventArgs) Handles Me.BindingContextChanged

    End Sub

    Private Sub frmDefVendor_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 15 -1-14
            If e.KeyCode = Keys.F4 Then
                SaveToolStripButton_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then

                NewToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        End Try

    End Sub

    Private Sub frmDefType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Try
            Me.RefreshForm()
            IsLoadedForm = True
            Get_All(frmModProperty.Tags)
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub OpenToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            Me.EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            Me.RefreshForm()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
    '    Me.EditRecord()
    'End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click

        Dim objCommand As New OleDbCommand
        Dim objTrans As OleDbTransaction

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

        'If MsgBox("Do you want to save ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, str_MessageHeader) = MsgBoxResult.No Then
        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

        Me.lblProgress.Text = "Processing Please Wait ..."
        Me.lblProgress.Visible = True
        Application.DoEvents()
        'If Not msg_Confirm(str_ConfirmSave) = True Then
        Me.uitxtName.Focus()
        'Exit Sub
        'End If
        If Con.State = ConnectionState.Closed Or Con.State = ConnectionState.Broken Then Con.Open()
        objTrans = Con.BeginTransaction
        Try

            objCommand.Connection = Con

            'If Me.SaveToolStripButton.Text = "&Save" Or Me.SaveToolStripButton.Text = "&Save" Then
            objCommand.CommandText = "delete from tblVendor where AccountId=" & Me.CurrentId
            objCommand.Transaction = objTrans
            objCommand.ExecuteNonQuery()

            objCommand = New OleDbCommand
            objCommand.Connection = Con
            'objTrans = Con.BeginTransaction
            ''before this against request no 883
            'objCommand.CommandText = "insert into tblVendor(accountId,VendorName, Territory,Address,Phone,Mobile,Email,Comments,sortorder, Active, VendorNTNNo, VendorSalesTaxNo) values(" & Me.CurrentId & ",N'" & Me.uitxtName.Text & "'," & Me.cmbTerritory.SelectedValue & ",N'" & Me.uitxtAddress.Text & "',N'" & Me.uitxtPhone.Text & "',N'" & Me.uitxtMobile.Text & "',N'" & Me.uitxtEmail.Text & "',N'" & Me.uitxtcomments.Text & "',N'" & Me.uitxtSortOrder.Text & "'," & IIf(Me.uichkActive.Checked = False, 0, 1) & ", N'" & Me.txtNTNNo.Text & "', N'" & Me.txtSalesTaxNo.Text & "') Select @@Identity"
            ''ReqId-883 Added Column Credit_Limitikjkjok
            'objCommand.CommandText = "insert into tblVendor(accountId,VendorName, Territory,Address,Phone,Mobile,Email,Comments,sortorder, Active, VendorNTNNo, VendorSalesTaxNo,Credit_Limit) values(" & Me.CurrentId & ",N'" & Me.uitxtName.Text.Replace("'", "''") & "'," & Me.cmbTerritory.SelectedValue & ",N'" & Me.uitxtAddress.Text.Replace("'", "''") & "',N'" & Me.uitxtPhone.Text.Replace("'", "''") & "',N'" & Me.uitxtMobile.Text.Replace("'", "''") & "',N'" & Me.uitxtEmail.Text.Replace("'", "''") & "',N'" & Me.uitxtcomments.Text.Replace("'", "''") & "'," & Val(Me.uitxtSortOrder.Text) & "," & IIf(Me.uichkActive.Checked = False, 0, 1) & ", N'" & Me.txtNTNNo.Text.Replace("'", "''") & "', N'" & Me.txtSalesTaxNo.Text.Replace("'", "''") & "', " & Val(Me.txtCreditLimit.Text) & ") Select @@Identity"
            'task 2639
            'Marked against task#20150511  Ali Ansari
            'objCommand.CommandText = "insert into tblVendor(accountId,VendorName, Territory,Address,Phone,Mobile,Email,Comments,sortorder, Active, VendorNTNNo, VendorSalesTaxNo,Credit_Limit,VendorsTypeId,PayeeTitle) values(" & Me.CurrentId & ",N'" & Me.uitxtName.Text.Replace("'", "''") & "'," & Me.cmbTerritory.SelectedValue & ",N'" & Me.uitxtAddress.Text.Replace("'", "''") & "',N'" & Me.uitxtPhone.Text.Replace("'", "''") & "',N'" & Me.uitxtMobile.Text.Replace("'", "''") & "',N'" & Me.uitxtEmail.Text.Replace("'", "''") & "',N'" & Me.uitxtcomments.Text.Replace("'", "''") & "'," & Val(Me.uitxtSortOrder.Text) & "," & IIf(Me.uichkActive.Checked = False, 0, 1) & ", N'" & Me.txtNTNNo.Text.Replace("'", "''") & "', N'" & Me.txtSalesTaxNo.Text.Replace("'", "''") & "', " & Val(Me.txtCreditLimit.Text) & ", N'" & Me.cmbVendorType.SelectedValue & "',N'" & Me.txtPayeeTitle.Text.Replace("'", "''") & "') Select @@Identity"
            'Marked against task#20150511  Ali Ansari
            'end task 2639

            'Altered against task#20150511 insert region,zone and belt in tblvendor Ali Ansari
            'Ali Faisal : TFS2213 : Add new field Credit Days
            ''Aashir: 4448 :Added Brand Text Feild
            objCommand.CommandText = "insert into tblVendor(accountId,VendorName, Territory,Address,Phone,Mobile,Email,Comments,sortorder, Active, VendorNTNNo, VendorSalesTaxNo,Credit_Limit,VendorsTypeId,PayeeTitle,regionid,zoneid,beltid,CreditDays,Brand) values(" & Me.CurrentId & ",N'" & Me.uitxtName.Text.Replace("'", "''") & "'," & Me.cmbTerritory.SelectedValue & ",N'" & Me.uitxtAddress.Text.Replace("'", "''") & "',N'" & Me.uitxtPhone.Text.Replace("'", "''") & "',N'" & Me.uitxtMobile.Text.Replace("'", "''") & "',N'" & Me.uitxtEmail.Text.Replace("'", "''") & "',N'" & Me.uitxtcomments.Text.Replace("'", "''") & "'," & Val(Me.uitxtSortOrder.Text) & "," & IIf(Me.uichkActive.Checked = False, 0, 1) & ", N'" & Me.txtNTNNo.Text.Replace("'", "''") & "', N'" & Me.txtSalesTaxNo.Text.Replace("'", "''") & "', " & Val(Me.txtCreditLimit.Text) & ", N'" & Me.cmbVendorType.SelectedValue & "',N'" & Me.txtPayeeTitle.Text.Replace("'", "''") & "', N'" & Me.cmbRegion.SelectedValue & "', N'" & Me.cmbZone.SelectedValue & "', N'" & Me.cmbBelt.SelectedValue & "','" & Me.txtCreditDays.Text & "','" & Me.txtBrand.Text & "') Select @@Identity"
            'Ali Faisal : TFS2213 : End
            'Altered against task#20150511 insert region,zone and belt in tblvendor Ali Ansari

            ''End ReqId - 833
            'Else
            'cm.CommandText = "update tblVendor set VendorName=N'" & Me.uitxtName.Text & "',Territory =" & Me.cmbTerritory.SelectedValue & ",Address =N'" & Me.uitxtAddress.Text & "',Phone=N'" & Me.uitxtPhone.Text & "',Mobile=N'" & Me.uitxtMobile.Text & "',Email=N'" & Me.uitxtEmail.Text & "',Comments=N'" & Me.uitxtcomments.Text & "', sortorder=N'" & Me.uitxtSortOrder.Text & "',Active=" & IIf(Me.uichkActive.Checked = False, 0, 1) & " where VendorId=" & Me.CurrentId
            'End If
            objCommand.Transaction = objTrans
            Dim identity As Integer = Convert.ToInt32(objCommand.ExecuteScalar())
            'rafay: Task Start :
            If arrFile IsNot Nothing Then

                If arrFile.Count > 0 Then
                    SaveDocument(CurrentId, Me.Name, objTrans)
                End If

            End If
            'rafay :Task End
            objTrans.Commit()
            ' MsgBox("Record Saved Successfully", MsgBoxStyle.Information, str_MessageHeader)
            ' msg_Information(str_informSave)

            Try
                ''insert Activity Log
                'SaveActivityLog("Config", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", identity, Me.CurrentId), True)
                SaveActivityLog("Config", Me.Text, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", EnumActions.Save, EnumActions.Update), LoginUserId, EnumRecordType.Configuration, IIf(Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save", identity, Me.CurrentId), True)
            Catch ex As Exception

            End Try

            Me.CurrentId = 0
            Me.RefreshForm()
        Catch ex As Exception
            objTrans.Rollback()
            ShowErrorMessage("Error occured while saving record: " & ex.Message)
        Finally
            Con.Close()
            '  Me.lblProgress.Visible = False
        End Try

    End Sub
    '' Rafay: Task Start:This Function Is Added to save Attachments with documents
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
    End Function 'Rafay:task end
    Private Sub cmbState_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbState.SelectedIndexChanged
        Try
            FillDropDown(Me.cmbCity, "Select * from tblListCity where STateId=" & Me.cmbState.SelectedValue)
            'Task# 20150511 filling combo of regiion
            FillDropDown(Me.cmbRegion, "Select * from tblListregion where stateId=" & Me.cmbState.SelectedValue)
            'Task# 20150511 filling combo of regiion
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
        If IsValidToDelete("ReceivingMasterTable", "Vendorid", Me.DataGridView1.CurrentRow.Cells("VendorId").Value.ToString) = True And IsValidToDelete("SalesOrderMasterTable", "VendorId", Me.DataGridView1.CurrentRow.Cells("VendorId").Value.ToString) = True Then
            If msg_Confirm(str_ConfirmDelete) = True Then
                'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Try
                    Dim cm As New OleDbCommand
                    If Con.State = ConnectionState.Closed Then Con.Open()
                    cm.Connection = Con
                    cm.CommandText = "delete from tblVendor where VendorId=" & Me.DataGridView1.CurrentRow.Cells("VendorId").Value.ToString
                    cm.ExecuteNonQuery()
                    'msg_Information(str_informDelete)
                    Me.CurrentId = 0

                    Try
                        ''insert Activity Log
                        SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.DataGridView1.CurrentRow.Cells("VendorId").Value.ToString, True)
                    Catch ex As Exception

                    End Try

                Catch ex As Exception
                    msg_Error("Error occured while deleting record: " & ex.Message)
                Finally
                    Con.Close()
                    Me.lblProgress.Visible = False
                End Try


                Me.RefreshForm()


            End If
        Else
            msg_Error(str_ErrorDependentRecordFound)
        End If
    End Sub

    'Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick

    'End Sub
    '
    ' Private Sub DataGridView1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
    '  Me.DataGridView1.CurrentRow.Selected = True
    ' Me.EditRecord()
    ' End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefVendor)
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
            Throw ex
        End Try
    End Sub
    ''TFS3684 : Ayesha Rehman : 27-06-2018 : Added Public Property  to use this function from another form
    Public Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Try
            Me.EditRecord()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From tblVendor WHERE VendorID=N'" & Id & "'")
                'Task 2639
                'Dim dtVendorType As DataTable = GetDataTable("SELECT VendorTypeId, VendorType FROM tblVendorType ")
                'Me.cmbVendorType.DataSource = dtVendorType
                ''Me.cmbVendorType.DisplayMember = dt2.Columns(1).ColumnName.ToString
                ''Me.cmbVendorType.ValueMember = dt2.Columns(0).ColumnName.ToString
                'Me.cmbVendorType.DisplayMember = "VendorType"
                'Me.cmbVendorType.ValueMember = "VendorTypeId"


                'End task 2639
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        'task 2639
                        Me.cmbVendorType.SelectedValue = dt.Rows(0).Item("VendorsTypeId").ToString
                        'end task 2639
                        Me.uitxtName.Text = dt.Rows(0).Item("Name").ToString
                        Me.cmbState.SelectedValue = dt.Rows(0).Item("StateId").ToString
                        Me.cmbCity.SelectedValue = dt.Rows(0).Item("CityId").ToString
                        Me.cmbTerritory.SelectedValue = dt.Rows(0).Item("Territoryid").ToString
                        Me.uitxtAddress.Text = dt.Rows(0).Item("Address").ToString
                        Me.uitxtPhone.Text = dt.Rows(0).Item("Phone").ToString
                        Me.uitxtMobile.Text = dt.Rows(0).Item("Mobile").ToString
                        Me.uitxtEmail.Text = dt.Rows(0).Item("Email").ToString
                        Me.uitxtcomments.Text = dt.Rows(0).Item("Comments").ToString
                        Me.uitxtSortOrder.Text = dt.Rows(0).Item("SortOrder").ToString
                        Me.uichkActive.Checked = IIf(dt.Rows(0).Item("Active") = 0, False, True)
                        Me.CurrentId = dt.Rows(0).Item("VendorID").ToString
                        Me.txtNTNNo.Text = dt.Rows(0).Item("NTN No").ToString
                        Me.txtSalesTaxNo.Text = dt.Rows(0).Item("SalesTax No").ToString
                        Me.txtCreditLimit.Text = dt.Rows(0).Item("Advance_Limit").ToString 'ReqId-883 Retrieve Credit Limit Value
                        'Task#20150511 Edit Ids Columns of Region, Zone, Belt''''
                        Me.cmbRegion.SelectedValue = dt.Rows(0).Item("RegionId").ToString
                        Me.cmbZone.SelectedValue = dt.Rows(0).Item("ZoneId").ToString
                        Me.cmbBelt.SelectedValue = dt.Rows(0).Item("BeltId").ToString
                        'Task#20150511 Edit Ids Columns of Region, Zone, Belt''''
                        'Ali Faisal : TFS2213 : Add new field Credit Days
                        Me.txtCreditDays.Text = dt.Rows(0).Item("CreditDays").ToString
                        'Ali Faisal : TFS2213 : End
                        ''Aashir: 4448 :Added Brand Text Feild
                        Me.txtBrand.Text = dt.Rows(0).Item("Brand").ToString
                        'end
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
            'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14 

            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim id As Integer = 0I
            id = Me.cmbState.SelectedValue
            fillCombos()
            Me.cmbState.SelectedValue = id
            id = Me.cmbCity.SelectedValue
            FillDropDown(Me.cmbCity, "Select * from tblListCity where STateId=" & Me.cmbState.SelectedValue)
            Me.cmbCity.SelectedValue = id
            id = Me.cmbTerritory.SelectedValue
            FillDropDown(Me.cmbTerritory, "Select * from tblListTerritory where CityId=" & Me.cmbCity.SelectedValue)
            Me.cmbTerritory.SelectedValue = id
            id = Me.cmbVendorType.SelectedIndex
            FillDropDown(Me.cmbVendorType, "Select * from tblVendorType")
            Me.cmbVendorType.SelectedIndex = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown

        'R-974 Ehtisham ul Haq user friendly system modification on 15-1-14
        If e.KeyCode = Keys.F2 Then
            OpenToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            DeleteToolStripButton_Click(Nothing, Nothing)
            Exit Sub
        End If

    End Sub
    ' Task No 2543  Ading New Item And Event To ADD NEW Vendor
    Private Sub BtnNewVendor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewVendor.Click

        'frmMain.LoadControl("AddVendor")
        FrmAddCustomers.FormType = "Vendor"
        FrmAddCustomers.ShowDialog()
        BindGrid()
        DataGridView1_DoubleClick(Nothing, Nothing)


    End Sub
    'Task No 2543  Apply NumValidation Function For Text Box To Validate The Numeric Value
    Private Sub txtCreditLimit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCreditLimit.KeyPress
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

    'Private Sub cmbVendorType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVendorType.SelectedIndexChanged
    '    Try
    '        FillDropDown(Me.cmbVendorType, "Select * from tblVendor where VendorsTypeId=" & Me.cmbVendorType.SelectedValue)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try

    'End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.DataGridView1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.DataGridView1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.DataGridView1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.General
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Vendors Information"
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

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        Try
            FillDropDown(Me.cmbZone, "Select * from tblListzone where regionId=" & Me.cmbRegion.SelectedValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbZone_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbZone.SelectedIndexChanged
        Try
            FillDropDown(Me.cmbBelt, "Select * from tblListbelt where ZoneId=" & Me.cmbZone.SelectedValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub btnAttachments_Click(sender As Object, e As EventArgs) Handles btnAttachments.Click
        Try
            SetAttachments()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
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
    'rafay :
    Private Sub DataGridView1_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles DataGridView1.LinkClicked
        Try
            If e.Column.Key = "No_of_Attachment" Then
                Dim frm As New frmAttachmentView
                frm._Source = Me.Name
                ''frm._VoucherId =
                frm._VoucherId = Val(Me.DataGridView1.GetRow.Cells("VendorId").Value.ToString)
                frm.ShowDialog()

                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub DataGridView1_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles DataGridView1.FormattingRow

    End Sub
End Class
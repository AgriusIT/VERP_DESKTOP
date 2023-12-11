'TFS3764 :  Ayesha Rehman : Add Item for Purchase and GRN and also Print BARCode for Customers (MAll)
'TFS4552 :  Ayesha Rehman : 04-10-2018 : Item searched from F1 popup should be bold when selected from list of Items.
Imports System.Data.OleDb
Imports System.IO
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data ''TFS3764
Imports System.Data.SqlClient ''TFS3764
Imports Neodynamic.SDK.Barcode ''TFS3764
Public Class frmItemSearch
    Public Shared ArticleId As Integer
    Public Shared Qty As Double ''TFS4732
    Public Shared dt As DataTable
    Public Shared PackName As String
    Public Shared PackQty As Double ''TFS4732
    Public Shared TotalQty As Double ''TFS4732
    Public Shared Rate As Double ''TFS4732
    Public Shared VendorId As Integer = 0
    Public Shared CompanyId As Integer = 0
    Public Shared ModelId As Integer = 0
    Public Shared LocationId As Integer = 0
    Public Shared formName As String
    Public Shared ArticleBarCode As String = ""
    Private mobjModel As Article ''TFS3762
    Private CurrentId As Integer = 0I ''TFS3762
    Dim strSQL As String = "" ''TFS3762
    Dim flgAddItemOnPurchaseAndGRN As Boolean = False
    Public dv As New DataView
    Dim SearchByBarcode As Boolean = False
    Dim NewArticle As Boolean = False
    Dim IsItemWiseDiscount As Boolean = False
    Dim DefaultBarCodeSourceValue As String = String.Empty
    Public Shared DiscountTypeId As Integer = 0
    Public Shared DiscountFactor As Double = 0
    Dim flgFastBarcodePrinting As Boolean = False
    Public Shared LocationComments As String = ""
    Public Sub GetAll()
        Dim Query As String = String.Empty
        Try
            ''Following lines of code are commented against TASK TFS4070 ON 03-08-2018
            'Dim str As String = "select ArticleId, ArticleCode as [Item Code], ArticleDescription AS Item_Name , ArticleTypeName as Type , ArticleGroupName AS Department, PurchasePrice, SalePrice, ArticleColorName As Color ,ArticleSizeName As Size, PackQty as [Pack Qty], 'Open' As PackInfo , StockLevel, IsNull([No Of Attachment],0) as Attachments , " & _
            '                    "StockLevelOpt, StockLevelMax , Active, SortOrder , ArticleBrandName as Brand, ArticleGenderName as Origin, ArticleUnitName as Unit, ArticleBARCode," & _
            '                    "ArticleCompanyName AS Category ,ArticleLpoName AS SubCategory, Remarks, ServiceItem as ServiceItem, ArticlePicture, TradePrice, Freight, ISNULL(MarketReturns, 0) as MarketReturns, ISNULL(GST_Applicable, 0) AS GST_Applicable, ISNULL(FlatRate_Applicable,0) as FlatRate_Applicable," & _
            '                    "ISNULL(FlatRate, 0) as FlatRate, ISNULL(ItemWeight, 0) AS ItemWeight, HS_Code, ISNULL(LargestPackQty, 0) AS LargestPackQty, IsNull(ArticleDefView.MasterId, 0) AS MasterId, " & _
            '                    "ISNULL(Cost_Price, 0) AS Cost_Price, ISNULL(ArticleBrandId, 0) AS ArticleBrandId, ISNULL(ApplyAdjustmentFuelExp, 0) AS ApplyAdjustmentFuelExp  from ArticleDefView LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = 'frmDefArticle') Group By DocId, Source) Doc_Att on Doc_Att.DocId = ArticleDefView.MasterID where Active=1"

            ''TFS4395 : Ayesha Rehman : 19-09-2018 : Edit Column MultiBarcode , [dbo].[MultiBarCode] (ArticleDefView.ArticleId) As MultiBarcode
            ''MultiBarCode column removed bcz query taking longer time to execute function of multibarcode
            If IsItemWiseDiscount = False Then
                Query = "SELECT ArticleDefView.ArticleId, ArticleCode as [Item Code], ArticleDescription AS Item_Name , ArticleTypeName as Type , ArticleGroupName AS Department, PurchasePrice, SalePrice, ArticleColorName As Color ,ArticleSizeName As Size, PackQty as [Pack Qty], 'Open' As PackInfo , StockLevel, IsNull([No Of Attachment],0) as Attachments , " & _
                                    "StockLevelOpt, StockLevelMax , Active, SortOrder , ArticleBrandName as Brand, ArticleGenderName as Origin, ArticleUnitName as Unit, ArticleBARCode," & _
                                    "ArticleCompanyName AS Category ,ArticleLpoName AS SubCategory, Remarks, ServiceItem as ServiceItem, ArticlePicture, TradePrice, Freight, ISNULL(MarketReturns, 0) as MarketReturns, ISNULL(GST_Applicable, 0) AS GST_Applicable, ISNULL(FlatRate_Applicable,0) as FlatRate_Applicable," & _
                                    "ISNULL(FlatRate, 0) as FlatRate, ISNULL(ItemWeight, 0) AS ItemWeight, HS_Code, ISNULL(LargestPackQty, 0) AS LargestPackQty, IsNull(ArticleDefView.MasterId, 0) AS MasterId, " & _
                                    "ISNULL(Cost_Price, 0) AS Cost_Price, ISNULL(ArticleBrandId, 0) AS ArticleBrandId, ISNULL(ApplyAdjustmentFuelExp, 0) AS ApplyAdjustmentFuelExp, 1 as DiscountId, 0 AS DiscFactor ,ArticleDefView.ArticleBARCodeDisable  from ArticleDefView LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = 'frmDefArticle') Group By DocId, Source) Doc_Att on Doc_Att.DocId = ArticleDefView.MasterID where Active=1"
            Else
                Query = "SELECT ArticleDefView.ArticleId, ArticleCode as [Item Code], ArticleDescription AS Item_Name , ArticleTypeName as Type , ArticleGroupName AS Department, PurchasePrice, SalePrice, ArticleColorName As Color ,ArticleSizeName As Size, PackQty as [Pack Qty], 'Open' As PackInfo , StockLevel, IsNull([No Of Attachment],0) as Attachments , " & _
                                    "StockLevelOpt, StockLevelMax , Active, SortOrder, ArticleBrandName as Brand, ArticleGenderName as Origin, ArticleUnitName as Unit, ArticleBARCode," & _
                                    "ArticleCompanyName AS Category ,ArticleLpoName AS SubCategory, Remarks, ServiceItem as ServiceItem, ArticlePicture, TradePrice, Freight, ISNULL(MarketReturns, 0) as MarketReturns, ISNULL(GST_Applicable, 0) AS GST_Applicable, ISNULL(FlatRate_Applicable,0) as FlatRate_Applicable," & _
                                    "ISNULL(FlatRate, 0) as FlatRate, ISNULL(ItemWeight, 0) AS ItemWeight, HS_Code, ISNULL(LargestPackQty, 0) AS LargestPackQty, IsNull(ArticleDefView.MasterId, 0) AS MasterId, " & _
                                    "ISNULL(Cost_Price, 0) AS Cost_Price, ISNULL(ArticleBrandId, 0) AS ArticleBrandId, ISNULL(ApplyAdjustmentFuelExp, 0) AS ApplyAdjustmentFuelExp, IsNull(Discount.DiscountId, 0) AS DiscountId, IsNull(Discount.Discount, 0) AS DiscFactor ,ArticleDefView.ArticleBARCodeDisable  from ArticleDefView LEFT OUTER JOIN (SELECT IsNull(ArticleId, 0) AS ArticleId, IsNull(DiscountMaster.DiscountType, 0) AS DiscountId, IsNull(DiscountMaster.Discount, 0) AS Discount FROM ItemWiseDiscountDetail AS Detail INNER JOIN ItemWiseDiscountMaster AS DiscountMaster ON Detail.ItemWiseDiscountId = DiscountMaster.ID WHERE Convert(varchar, GETDATE(), 102) BETWEEN Convert(varchar, DiscountMaster.FromDate, 102) AND Convert(varchar, DiscountMaster.ToDate, 102)) AS Discount ON ArticleDefView.ArticleId = Discount.ArticleId LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = 'frmDefArticle') Group By DocId, Source) Doc_Att on Doc_Att.DocId = ArticleDefView.MasterID WHERE Active=1"
            End If
            If CompanyId > 0 Then
                Query = Query & " AND ArticleDefView.CompanyId=" & CompanyId & ""
            End If
            If LocationId > 0 Then
                Query = Query & " AND ArticleDefView.ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & LocationId & " AND Restricted=1)"
            End If
            If ModelId > 0 Then
                Query = Query & " AND ArticleDefView.ArticleId In (Select ArticleId From ArticleModelList WHERE ModelId=" & ModelId & ")"
            End If
            If VendorId > 0 Then
                Query = Query & " AND MasterId in(Select ArticleDefId From ArticleDefCustomers WHERE CustomerId=N'" & VendorId & "')"
            End If
            Query += " order by ArticleId Desc" ''TFS3762
            dt = GetDataTable(Query)
            dt.TableName = "Item"
            dv.Table = dt
            Me.grd.DataSource = dv.ToTable
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' A new function is made to show items in grids without filters For Customers (MALL)
    ''' </summary>
    ''' <remarks>TFS3762 : Ayesha Rehman : 12-07-2018</remarks>
    Public Sub GetAllForPurchaseNGRN()
        Try
            ''TFS4395 : Ayesha Rehman : 19-09-2018 : Edit Column MultiBarcode 
            Dim str As String = "select ArticleId, ArticleCode as [Item Code], ArticleDescription AS Item_Name , ArticleTypeName as Type , ArticleGroupName AS Department, PurchasePrice, SalePrice, ArticleColorName As Color ,ArticleSizeName As Size, PackQty as [Pack Qty], 'Open' As PackInfo , ArticleCompanyName AS Category , ArticleLpoName AS SubCategory , StockLevel, IsNull([No Of Attachment],0) as Attachments , " & _
                                "StockLevelOpt, StockLevelMax , Active, SortOrder , ArticleBrandName as Brand, ArticleGenderName as Origin, ArticleUnitName as Unit, ArticleBARCode, " & _
                                " Remarks, ServiceItem as ServiceItem, ArticlePicture, TradePrice, Freight, ISNULL(MarketReturns, 0) as MarketReturns, ISNULL(GST_Applicable, 0) AS GST_Applicable, ISNULL(FlatRate_Applicable,0) as FlatRate_Applicable," & _
                                "ISNULL(FlatRate, 0) as FlatRate, ISNULL(ItemWeight, 0) AS ItemWeight, HS_Code, ISNULL(LargestPackQty, 0) AS LargestPackQty, IsNull(ArticleDefView.MasterId, 0) AS MasterId, " & _
                                "ISNULL(Cost_Price, 0) AS Cost_Price, ISNULL(ArticleBrandId, 0) AS ArticleBrandId, ISNULL(ApplyAdjustmentFuelExp, 0) AS ApplyAdjustmentFuelExp, 1 as DiscountId, 0 AS DiscFactor ,ArticleDefView.ArticleBARCodeDisable from ArticleDefView LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = 'frmDefArticle') Group By DocId, Source) Doc_Att on Doc_Att.DocId = ArticleDefView.MasterID where Active = 1 order by ArticleId desc"
            dt = GetDataTable(str)
            dt.TableName = "Item"
            dv.Table = dt
            Me.grd.DataSource = dv.ToTable
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns("SubCategory").Caption = "Vendor"  ''TFS3762
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ApplyGridSettings()
        Try
            '  Me.grd.RootTable.Columns("Category").Caption = "Category > Sub Category"  ''TFS3762
            Me.grd.RootTable.Columns("ArticleId").Visible = False
            Me.grd.RootTable.Columns("ArticleBrandId").Visible = False
            Me.grd.RootTable.Columns("MasterId").Visible = False
            Me.grd.RootTable.Columns("PurchasePrice").Visible = False
            Me.grd.RootTable.Columns("Cost_Price").Visible = False
            Me.grd.RootTable.Columns("Color").Visible = True
            Me.grd.RootTable.Columns("Size").Visible = True
            Me.grd.RootTable.Columns("StockLevel").Visible = False
            Me.grd.RootTable.Columns("StockLevelOpt").Visible = False
            Me.grd.RootTable.Columns("StockLevelMax").Visible = False
            Me.grd.RootTable.Columns("SortOrder").Visible = False
            Me.grd.RootTable.Columns("Active").Visible = False
            Me.grd.RootTable.Columns("Brand").Visible = False
            Me.grd.RootTable.Columns("Origin").Visible = False
            Me.grd.RootTable.Columns("FlatRate").Visible = False
            Me.grd.RootTable.Columns("TradePrice").Visible = False
            Me.grd.RootTable.Columns("Freight").Visible = False
            Me.grd.RootTable.Columns("HS_Code").Visible = False
            Me.grd.RootTable.Columns("ServiceItem").Visible = False
            Me.grd.RootTable.Columns("ArticlePicture").Visible = False
            Me.grd.RootTable.Columns("MarketReturns").Visible = False
            Me.grd.RootTable.Columns("GST_Applicable").Visible = False
            Me.grd.RootTable.Columns("FlatRate_Applicable").Visible = False
            Me.grd.RootTable.Columns("LargestPackQty").Visible = False
            Me.grd.RootTable.Columns("DiscountId").Visible = False
            Me.grd.RootTable.Columns("DiscFactor").Visible = False
            Me.grd.RootTable.Columns("Item Code").Width = 103
            Me.grd.RootTable.Columns("Item_Name").Width = 385
            Me.grd.RootTable.Columns("Size").Width = 200
            Me.grd.RootTable.Columns("Color").Width = 103
            ' Me.grd.RootTable.Columns("MultiBarcode").Visible = False ''TFS4395 ''Commented bcz we are not getting MultiBarcode Column Now
            'Me.grd.RootTable.Columns("Remarks").Width = 500
            Me.grd.RootTable.Columns("PackInfo").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grd.RootTable.Columns("PackInfo").ColumnType = Janus.Windows.GridEX.ColumnType.Link
            Me.grd.RootTable.Columns("Attachments").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grd.RootTable.Columns("Attachments").ColumnType = Janus.Windows.GridEX.ColumnType.Link
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmItemSearch_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            NewArticle = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmItemSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                'ElseIf e.KeyCode = Keys.Enter Then
                '    'ArticleId = grd.CurrentRow.Cells("ArticleId").Value.ToString
                '    'Qty = Val(txtQty.Text)
                '    'frmPOSEntry.AddItemInGrid(ArticleId, Qty)
                '    frmItemSearch_shown(Nothing, Nothing)
                '    Me.grd.Focus()
                '    If grd.RowCount > 0 Then
                '        frmItemSearch.ArticleId = grd.GetRow.Cells(0).Value
                '        Me.DialogResult = Windows.Forms.DialogResult.OK
                '    Else
                '        Me.DialogResult = Windows.Forms.DialogResult.Cancel
                '    End If
            End If
            If e.KeyCode = Keys.F3 Then
                frmStockLocationWise.ShowDialog()
            End If
            'If e.KeyCode = Keys.F6 Then
            '    If grd.RowCount > 0 Then
            '        If Val(grd.CurrentRow.Cells("MasterId").Value.ToString) > 0 Then
            '            Dim frmRelItems As New frmRelatedItems(Val(grd.CurrentRow.Cells("MasterId").Value.ToString), formName)
            '            frmRelItems.ShowDialog()
            '        End If
            '    End If
            'End If
            ''Start :  This key is added to search Item by Barcode 
            If e.KeyCode = Keys.F1 And SearchByBarcode = False Then
                SearchByBarcode = True
                pnlCategory.Visible = False
                pnlArticle.Visible = False
                Me.grd.Location = New Point(0, 97)
                Me.grd.Size = New Point(1017, 352)
                lblItem.Text = "Barcode Search"
            ElseIf e.KeyCode = Keys.F1 And SearchByBarcode = True Then
                SearchByBarcode = False
                pnlCategory.Visible = False
                pnlArticle.Visible = False
                Me.grd.Location = New Point(0, 97)
                Me.grd.Size = New Point(1017, 352)
                lblItem.Text = "Full Search"
            End If
            ''End
            If e.KeyCode = Keys.F10 And NewArticle = False Then
                Try
                    Me.lblProgress.Text = "Loading Please Wait ..."
                    Me.lblProgress.BackColor = Color.LightYellow
                    Me.lblProgress.Visible = True
                    Me.Cursor = Cursors.WaitCursor
                    NewArticle = True
                    lblRate.Visible = False
                    txtRate1.Visible = False
                    FillComboForCategory()
                    Me.pnlCategory.Visible = True
                    Me.btnAddArticle.Visible = False
                    Me.grd.Location = New Point(0, 134)
                    Me.grd.Size = New Point(1017, 310)
                    ResetArticleDetail()
                    btnAddArticle_Click(Nothing, Nothing)
                    Me.lblProgress.Visible = False
                    Me.Cursor = Cursors.Default
                Catch ex As Exception
                    ShowErrorMessage(ex.Message)
                End Try
            ElseIf e.KeyCode = Keys.F10 And NewArticle = True Then

                NewArticle = False
                If Me.pnlArticle.Visible = True Then
                    Me.pnlArticle.Visible = False
                    Me.btnAddArticle.Visible = False
                    Me.chkDisableBarCode.Checked = True
                    Me.grd.Location = New Point(0, 138)
                    Me.grd.Size = New Point(1017, 310)
                End If

            End If

            'If e.KeyCode = Keys.F11 Then
            '    If flgAddItemOnPurchaseAndGRN = True Then
            '        If Me.pnlArticle.Visible = True Then
            '            Me.pnlArticle.Visible = False
            '            Me.btnAddArticle.Visible = False
            '            Me.chkDisableBarCode.Checked = True
            '            Me.grd.Location = New Point(0, 138)
            '            Me.grd.Size = New Point(1017, 310)
            '        End If
            '    End If
            'End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Public Sub frmItemSearch_shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            txtSearch.Text = ArticleBarCode
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            ''start TFS3764 : Bar Code Professional Library is used to Print Bar code
            Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseKey = "KD83KNYYMA6XA6E7HNEL7QEUSGXGLKPQVCH7YVMEPXD7BG5FUCPA"
            Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseOwner = "LumenSoft Technologies-Standard Edition-OEM Developer License"
            ''End TFS3764

            Me.txtQty.Text = "1"
            Me.cmbUnit.SelectedIndex = 0
            Me.txtSearch.Focus()
            pnlCategory.Visible = False
            pnlArticle.Visible = False

            Me.chkDisableBarCode.Checked = False
            lblBarCode.Visible = False
            Me.txtBarCode.Visible = False
            Me.btnSaveArticle.Location = New Point(638, 130)

            grd.Location = New Point(0, 97)
            Me.grd.Size = New Point(1017, 352)
            If Not getConfigValueByType("AddItemForMall").ToString = "Error" Then
                flgAddItemOnPurchaseAndGRN = Convert.ToBoolean(getConfigValueByType("AddItemForMall").ToString)
            End If
            If Not getConfigValueByType("ItemWiseDiscount").ToString = "Error" Then
                IsItemWiseDiscount = Convert.ToBoolean(getConfigValueByType("ItemWiseDiscount").ToString)
            End If
            ''DefaultBarCodeSource
            If Not getConfigValueByType("DefaultBarCodeSource").ToString = "Error" Then
                DefaultBarCodeSourceValue = getConfigValueByType("DefaultBarCodeSource").ToString
            End If
            If flgAddItemOnPurchaseAndGRN = True Then
                If formName = "frmPurchaseNew" Or formName = "frmReceivingNote" Then
                    GetAllForPurchaseNGRN()
                    Me.cmbUnit.SelectedIndex = 1
                Else
                    GetAll()
                    lblRate.Visible = True
                    txtRate1.Visible = True
                End If
            Else
                GetAll()
                lblRate.Visible = True
                txtRate1.Visible = True
            End If
            If ArticleBarCode <> "" Then
                TextBox1_TextChanged(Nothing, Nothing)
                ArticleBarCode = ""
            End If
            flgFastBarcodePrinting = Convert.ToBoolean(getConfigValueByType("FastBarcodePrinting").ToString)
            cmbSize.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        Try

            If e.KeyCode = Keys.Enter Then

                ArticleId = grd.CurrentRow.Cells("ArticleId").Value.ToString
                Dim DiscountId As Integer = Val(grd.CurrentRow.Cells("DiscountId").Value.ToString)
                Dim DiscFactor As Double = grd.CurrentRow.Cells("DiscFactor").Value
                Qty = Val(txtQty.Text)
                PackName = cmbUnit.Text
                PackQty = txtPackQty.Text
                Rate = txtRate1.Text
                TotalQty = txtTotalQty.Text

                Dim PackQtyGrid As Double = Val(grd.CurrentRow.Cells("Pack Qty").Value.ToString)
                If SearchByBarcode = True Then
                    PackQty = PackQtyGrid
                End If
                DiscountTypeId = DiscountId
                DiscountFactor = DiscFactor
                'If formName <> "" Then
                '    frmPOSEntry.AddItemInGrid(ArticleId, Qty, PackName, PackQty, Rate)
                '    'frmItemSearch_shown(Nothing, Nothing)
                'End If
                If flgAddItemOnPurchaseAndGRN = True Then
                    If formName <> "" And formName <> "frmPurchaseNew" And formName <> "frmReceivingNote" Then
                        frmPOSEntry.AddItemInGrid(ArticleId, Qty, PackName, PackQty, Rate, DiscountId, DiscFactor)
                    ElseIf formName = "frmPurchaseNew" Then
                        frmPurchaseNew.AddItemToGridFromItemSearch()
                        If CBool(grd.CurrentRow.Cells("ArticleBARCodeDisable").Value) = False Then
                            If flgFastBarcodePrinting = True Then
                                If Not msg_Confirm("Do you want to Print BarCode?") = True Then Exit Sub
                                PrintBarCode(ArticleId, Qty)
                            End If
                        End If
                    ElseIf formName = "frmReceivingNote" Then
                        frmReceivingNote.AddItemToGridFromItemSearch()
                        If CBool(grd.CurrentRow.Cells("ArticleBARCodeDisable").Value) = False Then
                            If flgFastBarcodePrinting = True Then
                                If Not msg_Confirm("Do you want to Print BarCode?") = True Then Exit Sub
                                PrintBarCode(ArticleId, Qty)
                            End If
                        End If
                    Else
                        ''Start TFS3332
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        formName = ""
                        ''End TFS3332
                    End If
                Else
                    If formName <> "" Then
                        frmPOSEntry.AddItemInGrid(ArticleId, Qty, PackName, PackQty, Rate, DiscountId, DiscFactor)
                    Else
                        ''Start TFS3332
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        formName = ""
                        ''End TFS3332
                    End If
                End If
                txtSearch.Text = ""
                txtSearch.Focus()
            End If
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
            If e.KeyCode = Keys.Down Then
                Me.grd.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try

            If dv IsNot Nothing Then
                If pnlCategory.Visible = False Then
                    If Me.txtSearch.Text <> String.Empty Then
                        If SearchByBarcode = True Then
                            dv.RowFilter = " ArticleBARCode = '" & Me.txtSearch.Text & "' or ArticleId = " & GetArticleId() & ""
                        Else
                            dv.RowFilter = "Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or  ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%' "
                        End If
                        Me.grd.DataSource = dv
                    Else
                        Me.grd.DataSource = dt
                        lblItemName.Text = ""
                        txtRate1.Text = "0"
                    End If
                Else

                    If Me.cmbLPO.Value <> 0 And Me.cmbCompany.Value <> 0 Then
                        If txtSearch.Text <> "" Then
                            '' Or MultiBarcode Like '%" & txtSearch.Text & "%' 
                            dv.RowFilter = "[Category]  LIKE '%" & Me.cmbCompany.Text.ToString.Replace("'", "''") & "%' And [SubCategory] LIKE '%" & Me.cmbLPO.Text.ToString.Replace("'", "''") & "%' And (Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or  ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%'  ) "
                        Else
                            dv.RowFilter = "[Category]  LIKE '%" & Me.cmbCompany.Text.ToString.Replace("'", "''") & "%' And [SubCategory] LIKE '%" & Me.cmbLPO.Text.ToString.Replace("'", "''") & "%' "
                        End If
                        Me.grd.DataSource = dv
                    ElseIf Me.cmbCompany.Value > 0 And Me.cmbLPO.Value <= 0 Then
                        If txtSearch.Text <> "" Then
                            dv.RowFilter = "[Category]  LIKE '%" & Me.cmbCompany.Text.ToString.Replace("'", "''") & "%' And (Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%') "
                        Else
                            dv.RowFilter = "[Category]  LIKE '%" & Me.cmbCompany.Text.ToString.Replace("'", "''") & "%'  "
                        End If
                        Me.grd.DataSource = dv
                    ElseIf Me.cmbCompany.Value <= 0 And Me.cmbLPO.Value > 0 Then
                        If txtSearch.Text <> "" Then
                            dv.RowFilter = "[SubCategory] LIKE '%" & Me.cmbLPO.Text.ToString.Replace("'", "''") & "%' And (Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or  ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%' ) "
                        Else
                            dv.RowFilter = "[SubCategory] LIKE '%" & Me.cmbLPO.Text.ToString.Replace("'", "''") & "%' "
                        End If
                        Me.grd.DataSource = dv
                    ElseIf Me.txtSearch.Text <> String.Empty Then
                        dv.RowFilter = "Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or  ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%'  "
                        Me.grd.DataSource = dv
                    Else
                        Me.grd.DataSource = dt
                        lblItemName.Text = ""
                        txtRate1.Text = "0"
                    End If
                End If
            End If
            FillCombo()
            If grd.RowCount > 0 Then
                lblItemName.Text = grd.CurrentRow.Cells("Item_Name").Value.ToString
                Me.txtRate1.Text = grd.CurrentRow.Cells("SalePrice").Value.ToString
            End If
            If txtSearch.Text = "" Then
                lblItemName.Text = ""
                txtRate1.Text = "0"
            End If
            ' Me.uitxtItemName.Text = txtSearch.Text.ToString  ''TFS3764
            'Me.grd.AutoSizeColumns()
            'ArticleId = grd.CurrentRow.Cells("ArticleId").Value.ToString
            'Qty = Val(txtQty.Text)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetArticleId() As Integer
        Try
            Dim dtbarcode As DataTable = GetDataTable("Select ArticleId from ArticleBarcodeDefTable where ArticleBarCode = '" & txtSearch.Text & "'")
            If dtbarcode.Rows.Count > 0 Then
                Return Val(dtbarcode.Rows(0).Item("ArticleId").ToString)
            End If
            Return 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ItemAdd()
        Try
            dt = CType(Me.grd.DataSource, DataTable)
            Dim dr As DataRow
            dr = dt.NewRow
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            formName = ""
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_Click(sender As Object, e As EventArgs) Handles grd.Click
        Try
            If NewArticle = False Then
                If grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    txtRate1.Text = Val(grd.GetRow.Cells("SalePrice").Value.ToString)
                    txtPackQty.Text = Val(grd.GetRow.Cells("Pack Qty").Value.ToString)
                    lblItemName.Text = grd.GetRow.Cells("Item_Name").Value.ToString ''TFS4552
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(sender As Object, e As EventArgs) Handles grd.DoubleClick

    End Sub
    Private Sub grd_KeyDown(sender As Object, e As KeyEventArgs) Handles grd.KeyDown
        Try

            If e.KeyCode = Keys.Enter Then
                If grd.RowCount > 0 Then
                    ArticleId = grd.GetRow.Cells("ArticleId").Value.ToString
                    Dim DiscountId As Integer = Val(grd.CurrentRow.Cells("DiscountId").Value.ToString)
                    Dim DiscFactor As Double = grd.CurrentRow.Cells("DiscFactor").Value
                    Qty = Val(txtQty.Text)
                    PackName = cmbUnit.Text
                    PackQty = txtPackQty.Text
                    Rate = txtRate1.Text
                    TotalQty = txtTotalQty.Text
                    Dim PackQtyGrid As Double = Val(grd.CurrentRow.Cells("Pack Qty").Value.ToString)
                    If SearchByBarcode = True Then
                        PackQty = PackQtyGrid
                    End If
                    DiscountTypeId = DiscountId
                    DiscountFactor = DiscFactor
                    ''New Configuration Added to add items in grd in Case forms are Purchase or GRN
                    If flgAddItemOnPurchaseAndGRN = True Then
                        If formName <> "" And formName <> "frmPurchaseNew" And formName <> "frmReceivingNote" Then
                            frmPOSEntry.AddItemInGrid(ArticleId, Qty, PackName, PackQty, Rate, DiscountId, DiscFactor)
                        ElseIf formName = "frmPurchaseNew" Then
                            frmPurchaseNew.AddItemToGridFromItemSearch()
                            If CBool(grd.CurrentRow.Cells("ArticleBARCodeDisable").Value) = False Then
                                If flgFastBarcodePrinting = True Then
                                    If Not msg_Confirm("Do you want to Print BarCode?") = True Then Exit Sub
                                    PrintBarCode(ArticleId, Qty)
                                End If
                            End If
                        ElseIf formName = "frmReceivingNote" Then
                            frmReceivingNote.AddItemToGridFromItemSearch()
                            If CBool(grd.CurrentRow.Cells("ArticleBARCodeDisable").Value) = False Then
                                If flgFastBarcodePrinting = True Then
                                    If Not msg_Confirm("Do you want to Print BarCode?") = True Then Exit Sub
                                    PrintBarCode(ArticleId, Qty)
                                End If
                            End If
                        Else
                            ''Start TFS3332
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                            formName = ""
                            ''End TFS3332
                        End If
                    Else
                        If formName <> "" Then
                            frmPOSEntry.AddItemInGrid(ArticleId, Qty, PackName, PackQty, Rate, DiscountId, DiscFactor)


                        Else
                            ''Start TFS3332
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                            formName = ""
                            ''End TFS3332
                        End If
                    End If

                    'If formName <> "" Then
                    '    Me.DialogResult = Windows.Forms.DialogResult.OK
                    '    formName = ""
                    'Else
                    '    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                    '    formName = ""
                    'End If
                End If
            End If
            FillCombo()
            'If cmbUnit.SelectedText = "Pack" Then
            '    txtPackQty.Text = grd.CurrentRow.Cells("PackQty").Value.ToString
            'Else
            '    txtPackQty.Text = 1
            'End If
            If e.KeyCode = Keys.Up Then
                If grd.GetRow().RowIndex = 0 Then
                    txtSearch.Focus()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombo()
        Me.cmbUnit.ValueMember = "ArticlePackId"
        Me.cmbUnit.DisplayMember = "PackName"
        Me.cmbUnit.DataSource = GetPackData(ArticleId)
        If flgAddItemOnPurchaseAndGRN = True Then
            Me.cmbUnit.SelectedIndex = 1
        End If
    End Sub
    Private Sub cmbUnit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try
            If cmbUnit.Text = "Loose" Then
                ' lblPackQty.visble = False
                txtPackQty.Enabled = False
                txtPackQty.Text = 1
            Else
                If grd.RecordCount > 0 Then
                    txtPackQty.Text = grd.CurrentRow.Cells("Pack Qty").Value.ToString
                    'lblPackQty.Visible = True
                    txtPackQty.Enabled = True
                Else
                    txtPackQty.Text = 1
                    ' lblPackQty.Visible = True
                    txtPackQty.Enabled = True
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_KeyUp(sender As Object, e As KeyEventArgs) Handles grd.KeyUp
        Try
            If NewArticle = False Then
                If grd.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                    txtRate1.Text = Val(grd.GetRow.Cells("SalePrice").Value.ToString)
                    txtPackQty.Text = Val(grd.GetRow.Cells("Pack Qty").Value.ToString)
                    lblItemName.Text = grd.GetRow.Cells("Item_Name").Value.ToString ''TFS4552
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_LinkClicked(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.LinkClicked
        Try


            If e.Column.Key = "Attachments" Then
                Dim frm As New frmAttachmentView
                frm._Source = "frmDefArticle"
                frm._VoucherId = Val(Me.grd.GetRow.Cells("MasterId").Value.ToString)
                frm.ShowDialog()
                Exit Sub
            ElseIf e.Column.Key = "PackInfo" Then
                If grd.RowCount > 0 Then
                    If Val(grd.CurrentRow.Cells("MasterId").Value.ToString) > 0 Then
                        Dim frmItemPackInfo As New frmItemPackInfo(Val(grd.CurrentRow.Cells("MasterId").Value.ToString))
                        frmItemPackInfo.ShowDialog()
                    End If
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grd_MarginChanged(sender As Object, e As EventArgs) Handles grd.MarginChanged

    End Sub

    Private Sub grd_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles grd.RowDoubleClick
        Try
            If grd.RowCount > 0 Then
                ArticleId = grd.GetRow.Cells("ArticleId").Value.ToString
                Dim DiscountId As Integer = Val(grd.CurrentRow.Cells("DiscountId").Value.ToString)
                Dim DiscFactor As Double = grd.CurrentRow.Cells("DiscFactor").Value
                Qty = Val(txtQty.Text)
                PackName = cmbUnit.Text
                PackQty = txtPackQty.Text
                Rate = txtRate1.Text
                TotalQty = txtTotalQty.Text
                Dim PackQtyGrid As Double = Val(grd.CurrentRow.Cells("Pack Qty").Value.ToString)
                If SearchByBarcode = True Then
                    PackQty = PackQtyGrid
                End If
                DiscountTypeId = DiscountId
                DiscountFactor = DiscFactor
                ''New Configuration Added to add items in grd in Case forms are Purchase or GRN
                If flgAddItemOnPurchaseAndGRN = True Then
                    If formName <> "" And formName <> "frmPurchaseNew" And formName <> "frmReceivingNote" Then
                        frmPOSEntry.AddItemInGrid(ArticleId, Qty, PackName, PackQty, Rate, DiscountId, DiscFactor)
                    ElseIf formName = "frmPurchaseNew" Then
                        frmPurchaseNew.AddItemToGridFromItemSearch()
                        If CBool(grd.CurrentRow.Cells("ArticleBARCodeDisable").Value) = False Then
                            If flgFastBarcodePrinting = True Then
                                If Not msg_Confirm("Do you want to Print BarCode?") = True Then Exit Sub
                                PrintBarCode(ArticleId, Qty)
                            End If
                        End If
                    ElseIf formName = "frmReceivingNote" Then
                        frmReceivingNote.AddItemToGridFromItemSearch()
                        If CBool(grd.CurrentRow.Cells("ArticleBARCodeDisable").Value) = False Then
                            If flgFastBarcodePrinting = True Then
                                If Not msg_Confirm("Do you want to Print BarCode?") = True Then Exit Sub
                                PrintBarCode(ArticleId, Qty)
                            End If
                        End If
                    Else
                        ''Start TFS3332
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        formName = ""
                        ''End TFS3332
                    End If
                Else
                    If formName <> "" Then
                        frmPOSEntry.AddItemInGrid(ArticleId, Qty, PackName, PackQty, Rate, DiscountId, DiscFactor)
                    Else
                        ''Start TFS3332
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        formName = ""
                        ''End TFS3332
                    End If
                End If
                'If formName <> "" Then
                '    frmPOSEntry.AddItemInGrid(ArticleId, Qty, PackName, PackQty, Rate)
                'Else
                '    Me.DialogResult = Windows.Forms.DialogResult.OK
                '    formName = ""
                'End If
                'If formName = "" Then
                '    Me.DialogResult = Windows.Forms.DialogResult.OK
                '    formName = ""
                'Else
                '    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                '    formName = ""
                'End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_KeyDown(sender As Object, e As KeyEventArgs) Handles txtQty.KeyDown, txtRate1.KeyDown, cmbUnit.KeyDown, txtPackQty.KeyDown, txtTotalQty.KeyDown
        Try
            Dim qty As Boolean = False
            Dim Rate As Boolean = False
            Dim PackQty As Boolean = False
            If txtQty.Focused = True Then
                qty = True
                Rate = False
                PackQty = False
            ElseIf txtPackQty.Focused = True Then
                qty = False
                Rate = False
                PackQty = True
            ElseIf txtRate1.Focused = True Then
                qty = False
                Rate = True
                PackQty = False
            End If
            If e.KeyCode = Keys.Enter Then
                ArticleId = grd.CurrentRow.Cells("ArticleId").Value.ToString
                Dim DiscountId As Integer = Val(grd.CurrentRow.Cells("DiscountId").Value.ToString)
                Dim DiscFactor As Double = grd.CurrentRow.Cells("DiscFactor").Value
                qty = Val(txtQty.Text)
                PackName = cmbUnit.Text
                PackQty = txtPackQty.Text
                Rate = txtRate1.Text
                txtTotalQty.Text = qty * PackQty
                TotalQty = txtTotalQty.Text
                Dim PackQtyGrid As Double = Val(grd.CurrentRow.Cells("Pack Qty").Value.ToString)
                If SearchByBarcode = True Then
                    PackQty = PackQtyGrid
                End If
                If formName <> "" Then
                    frmPOSEntry.AddItemInGrid(ArticleId, qty, PackName, PackQty, Rate, DiscountId, DiscFactor)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
            If Not cmbUnit.Focus = True Then
                If e.KeyCode = Keys.Down Then
                    Me.grd.Focus()
                End If
            End If
            If qty = True Then
                txtQty.Focus()
            ElseIf Rate = True Then
                txtRate1.Focus()
            ElseIf PackQty = True Then
                txtPackQty.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillCombosForArticle()
        Try
            strSQL = "select ArticleColorId as Id,ArticleColorName as Name from ArticleColorDefTable where active=1 order by sortOrder"
            'Me.cmbColor.DisplayMember = "Name"
            'Me.cmbColor.ValueMember = "Id"
            'Me.cmbColor.DataSource = UtilityDAL.GetDataTable(strSQL)
            FillUltraDropDown(Me.cmbColor, strSQL)
            Me.cmbColor.Rows(1).Activate()
            If Me.cmbColor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbColor.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                Me.cmbColor.DisplayLayout.Bands(0).Columns("Name").Width = 300
            End If
            strSQL = "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 order by ArticleSizeName, SortOrder "
            'Me.cmbSize.DisplayMember = "Name"
            'Me.cmbSize.ValueMember = "Id"
            'Me.cmbSize.DataSource = UtilityDAL.GetDataTable(strSQL)
            FillDropDown(Me.cmbSize, strSQL)

            strSQL = "select ArticleTypeId as Id, ArticleTypeName as Name, TypeCode from ArticleTypeDefTable where active=1 order by sortOrder"
            'Me.cmbType.DisplayMember = "Name"
            'Me.cmbType.ValueMember = "ID"
            'Me.cmbType.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
            FillUltraDropDown(Me.cmbType, strSQL)
            Me.cmbType.Rows(0).Activate()
            If Me.cmbType.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbType.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                Me.cmbType.DisplayLayout.Bands(0).Columns("Name").Width = 300
            End If
            strSQL = "SELECT  ArticleGroupDefTable.ArticleGroupId AS Id, ArticleGroupDefTable.ArticleGroupName AS Name, ArticleGroupDefTable.SubSubID, ArticleGroupDefTable.GroupCode " & _
                   " FROM ArticleGroupDefTable LEFT OUTER JOIN " & _
                     "    tblCOAMainSubSubDetail ON ArticleGroupDefTable.SubSubID = tblCOAMainSubSubDetail.coa_detail_id" & _
                   " WHERE     (ArticleGroupDefTable.Active = 1) AND (tblCOAMainSubSubDetail.main_sub_sub_id In(Select main_sub_sub_id from tblCOAMainSubSub WHERE Account_Type='Inventory'))" & _
                   " ORDER BY ArticleGroupDefTable.SortOrder"
            'Me.cmbCategory.DisplayMember = "Name"
            'Me.cmbCategory.ValueMember = "Id"
            'Me.cmbCategory.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
            FillUltraDropDown(Me.cmbCategory, strSQL)
            Me.cmbCategory.Rows(0).Activate()
            If Me.cmbCategory.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbCategory.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                Me.cmbCategory.DisplayLayout.Bands(0).Columns("Name").Width = 300
            End If
            ''filling Unit combo
            strSQL = "select ArticleUnitId as Id, ArticleUnitName as Name from ArticleUnitDefTable where active=1 order by sortOrder"
            'Me.cmbArticleUnit.DisplayMember = "Name"
            'Me.cmbArticleUnit.ValueMember = "ID"
            'Me.cmbArticleUnit.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
            FillUltraDropDown(Me.cmbArticleUnit, strSQL)
            Me.cmbArticleUnit.Rows(1).Activate()
            If Me.cmbArticleUnit.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbArticleUnit.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                Me.cmbArticleUnit.DisplayLayout.Bands(0).Columns("Name").Width = 300
            End If
            strSQL = "select ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable where active=1 order by sortOrder"
            FillUltraDropDown(Me.cmbGender, strSQL)
            Me.cmbGender.Rows(0).Activate()
            If Me.cmbGender.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbGender.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                Me.cmbGender.DisplayLayout.Bands(0).Columns("Name").Width = 300
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' This Function is Added to Filter more items on the basis of Comapny and Category
    ''' </summary>
    ''' <remarks>TFS3764 : Ayesha Rehman : 19-07-2018</remarks>
    Private Sub FillComboForCategory()
        Try

            strSQL = "SELECT  ArticleCompanyId AS ID, ArticleCompanyName AS Name, CategoryCode" & _
                " FROM ArticleCompanyDefTable" & _
                " WHERE Active = 1"
            'Me.cmbCompany.DisplayMember = "Name"
            'Me.cmbCompany.ValueMember = "ID"
            'Me.cmbCompany.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
            FillUltraDropDown(Me.cmbCompany, strSQL)
            Me.cmbCompany.Rows(0).Activate()
            If Me.cmbCompany.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbCompany.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                Me.cmbCompany.DisplayLayout.Bands(0).Columns("Name").Width = 150
                Me.cmbCompany.DisplayLayout.Bands(0).Columns("CategoryCode").Width = 150
            End If
            ' strSQL = "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID,dbo.ArticleCompanyDefTable.ArticleCompanyName + ' > ' + dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId , dbo.ArticleLpoDefTable.SubCategoryCode  FROM         dbo.ArticleLpoDefTable INNER JOIN               dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId"
            strSQL = "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID, dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId , dbo.ArticleLpoDefTable.SubCategoryCode  FROM         dbo.ArticleLpoDefTable INNER JOIN               dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId"
            'Me.cmbLPO.DisplayMember = "Name"
            'Me.cmbLPO.ValueMember = "ID"
            'Me.cmbLPO.DataSource = AddZeroIndexString(UtilityDAL.GetDataTable(strSQL))
            FillUltraDropDown(Me.cmbLPO, strSQL)
            Me.cmbLPO.Rows(0).Activate()
            If Me.cmbLPO.DisplayLayout.Bands(0).Columns.Count > 0 Then
                Me.cmbLPO.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                Me.cmbLPO.DisplayLayout.Bands(0).Columns("ArticleCompanyId").Hidden = True
                Me.cmbLPO.DisplayLayout.Bands(0).Columns("SubCategoryCode").Hidden = True
                Me.cmbLPO.DisplayLayout.Bands(0).Columns("Name").Width = 500
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillModelForArticle()
        Try
            Dim str As String = String.Empty
            Me.mobjModel = New Article
            With mobjModel
                .ArticleID = Me.CurrentId
                .ArticleCode = Me.uitxtItemCode.Text
                If Me.chkDisableBarCode.Checked = True Then
                    .ArticleBARCode = Me.txtBarCode.Text
                Else
                    .ArticleBARCode = Me.uitxtItemCode.Text
                End If
                .ArticleDescription = Me.uitxtItemName.Text
                .ArticleGroupID = Me.cmbCategory.Value
                .ArticleTypeID = Me.cmbType.Value
                .ArticleUnitID = Me.cmbArticleUnit.Value
                .ArticleLPOID = Me.cmbLPO.Value
                .ArticleGenderID = Me.cmbGender.Value
                .PurchasePrice = Val(Me.uitxtPrice.Text)
                .SalePrice = Val(Me.uitxtSalePrice.Text)
                .PackQty = Val(Me.txtPackQty.Text)
                .ArticleBARCodeDisable = Me.chkDisableBarCode.Checked ''TFS3763
                .ArticleCategoryId = IIf(Me.cmbCompany.Value > 0, Me.cmbCompany.Value, 0)
                .DefaultBarCodeSourceValue = DefaultBarCodeSourceValue
                .DiscountFactor = Val(txtDiscount.Text)
                'fill Detail
                .ArticleDetails = New List(Of ArticleDetail)
                Dim dtl As ArticleDetail

                dtl = New ArticleDetail
                With dtl

                    .ArticleCode = Me.uitxtItemCode.Text
                    If Me.chkDisableBarCode.Checked = True Then
                        .ArticleBARCode = Me.txtBarCode.Text
                    Else
                        .ArticleBARCode = Me.uitxtItemCode.Text
                    End If
                    '.ArticleBARCode = Me.txtBarCode.Text
                    .ArticleDescription = Me.uitxtItemName.Text
                    .ArticleGroupID = Me.cmbCategory.Value
                    .ArticleTypeID = Me.cmbType.Value
                    .ArticleUnitID = Me.cmbArticleUnit.Value
                    .ArticleLPOID = Me.cmbLPO.Value
                    .ArticleGenderID = Me.cmbGender.Value
                    .PurchasePrice = Val(Me.uitxtPrice.Text)
                    .SalePrice = Val(Me.uitxtSalePrice.Text)
                    .PackQty = Val(Me.txtPackQty.Text)
                    Dim strSize As String = "Select COUNT(*) As SizeCount  from ArticleSizeDefTable where ArticleSizeName = '" & Me.cmbSize.Text & "'"
                    Dim dtItemSize As DataTable = GetDataTable(strSize)
                    If dtItemSize.Rows.Count > 0 Then
                        If Val(dtItemSize.Rows(0).Item(0).ToString) = 0 Then
                            Dim Con As New SqlConnection(SQLHelper.CON_STR)
                            Con.Open()
                            Dim trans As SqlTransaction = Con.BeginTransaction()
                            Try
                                Dim strSQL As String = "Insert into ArticleSizeDefTable (ArticleSizeName , Active ,SortOrder ,IsDate) values('" & Me.cmbSize.Text & "'," & 1 & "," & 1 & ",'" & Date.Now.ToString("yyyy-M-d h:mm:ss tt") & "') Select @@Identity"
                                .SizeRangeID = Convert.ToInt32(SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL))
                                trans.Commit()
                            Catch ex As Exception
                                trans.Rollback()
                                Throw ex
                            Finally
                                Con.Close()
                            End Try
                        Else
                            .SizeRangeID = Me.cmbSize.SelectedValue
                        End If
                    Else
                        .SizeRangeID = Me.cmbSize.SelectedValue
                    End If
                    .ArticleColorID = Me.cmbColor.Value
                    .ArticleCategoryId = IIf(Me.cmbCompany.Value > 0, Me.cmbCompany.Value, 0)
                    .ArticleBARCodeDisable = Me.chkDisableBarCode.Checked ''TFS3763

                End With
                .ArticleDetails.Add(dtl)
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbCompany_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbCompany.RowSelected, cmbCategory.RowSelected, cmbType.RowSelected
        Try
            Dim cmb As Infragistics.Win.UltraWinGrid.UltraCombo = CType(sender, Infragistics.Win.UltraWinGrid.UltraCombo)
            Dim strPrefix As String = ""
            If cmb.Text <> strZeroIndexItem Then
                If pnlArticle.Visible = True Then
                    If cmbCompany.Value IsNot Nothing Then
                        strPrefix = IIf(cmbCategory.Value > 0, Me.cmbCategory.ActiveRow.Cells("GroupCode").Value.ToString, "") & "-" & IIf(cmbType.Value > 0, Me.cmbType.ActiveRow.Cells("TypeCode").Value.ToString, "") & "-" & IIf(cmbCompany.Text <> strZeroIndexItem, Me.cmbCompany.ActiveRow.Cells("CategoryCode").Value.ToString, "")
                    End If
                    Me.uitxtItemCode.Text = ArticleDAL.GetArticleCode(CStr(strPrefix) & "-")
                    Me.txtBarCode.Text = Me.uitxtItemCode.Text
                Else
                    If cmbCompany.Value IsNot Nothing Then
                        strPrefix = IIf(cmbCompany.Text <> strZeroIndexItem, Me.cmbCompany.ActiveRow.Cells("CategoryCode").Value.ToString, "")
                    End If
                    Me.uitxtItemCode.Text = ArticleDAL.GetArticleCode(CStr(strPrefix) & "-")
                    Me.txtBarCode.Text = Me.uitxtItemCode.Text
                End If
            Else
                Me.uitxtItemCode.Text = String.Empty
                Me.txtBarCode.Text = String.Empty
            End If
            If cmb.Text <> strZeroIndexItem Then
                If cmbCompany.Value IsNot Nothing Then
                    Dim strItemName As String = IIf(cmbCompany.Text <> strZeroIndexItem, Me.cmbCompany.ActiveRow.Cells("Name").Value.ToString, "") & "~" & IIf(cmbLPO.Text <> strZeroIndexItem, Me.cmbLPO.ActiveRow.Cells("Name").Value.ToString, "")
                    Me.uitxtItemName.Text = strItemName
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbLPO_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbLPO.RowSelected
        Try
            Dim cmb As Infragistics.Win.UltraWinGrid.UltraCombo = CType(sender, Infragistics.Win.UltraWinGrid.UltraCombo)
            If cmb.Text <> strZeroIndexItem Then
                If cmbLPO.Value IsNot Nothing Then
                    Dim strItemName As String = IIf(cmbCompany.Text <> strZeroIndexItem, Me.cmbCompany.ActiveRow.Cells("Name").Value.ToString, "") & "~" & IIf(cmbLPO.Text <> strZeroIndexItem, Me.cmbLPO.ActiveRow.Cells("Name").Value.ToString, "")
                    Me.uitxtItemName.Text = strItemName
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub cmbLPO_ValueChanged(sender As Object, e As EventArgs) Handles cmbLPO.ValueChanged, cmbCompany.ValueChanged
        Try


            'If dv IsNot Nothing Then
            '    If Me.cmbLPO.Value <> 0 And Me.cmbCompany.Value <> 0 Then
            '        If txtSearch.Text <> "" Then
            '            dv.RowFilter = "[Category]  LIKE '%" & Me.cmbCompany.Text.ToString.Replace("'", "''") & "%' And [SubCategory] LIKE '%" & Me.cmbLPO.Text.ToString.Replace("'", "''") & "%' And (Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or  ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%' ) "
            '        Else
            '            dv.RowFilter = "[Category]  LIKE '%" & Me.cmbCompany.Text.ToString.Replace("'", "''") & "%' And [SubCategory] LIKE '%" & Me.cmbLPO.Text.ToString.Replace("'", "''") & "%' "
            '        End If
            '        Me.grd.DataSource = dv
            '    ElseIf Me.cmbCompany.Value > 0 And Me.cmbLPO.Value <= 0 Then
            '        If txtSearch.Text <> "" Then
            '            dv.RowFilter = "[Category]  LIKE '%" & Me.cmbCompany.Text.ToString.Replace("'", "''") & "%' And (Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or  ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%' ) "
            '        Else
            '            dv.RowFilter = "[Category]  LIKE '%" & Me.cmbCompany.Text.ToString.Replace("'", "''") & "%'  "
            '        End If
            '        Me.grd.DataSource = dv
            '    ElseIf Me.cmbCompany.Value <= 0 And Me.cmbLPO.Value > 0 Then
            '        If txtSearch.Text <> "" Then
            '            dv.RowFilter = "[SubCategory] LIKE '%" & Me.cmbLPO.Text.ToString.Replace("'", "''") & "%' And (Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or  ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%' ) "
            '        Else
            '            dv.RowFilter = "[SubCategory] LIKE '%" & Me.cmbLPO.Text.ToString.Replace("'", "''") & "%' "
            '        End If
            '        Me.grd.DataSource = dv
            '    ElseIf Me.txtSearch.Text <> String.Empty Then
            '        dv.RowFilter = "Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or  ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%' "
            '        Me.grd.DataSource = dv
            '    Else
            '        Me.grd.DataSource = dt
            '        lblItemName.Text = ""
            '        txtRate1.Text = "0"
            '    End If
            'End If
            If dv IsNot Nothing Then
                If Me.cmbLPO.Text <> strZeroIndexItem And Me.cmbCompany.Text <> strZeroIndexItem Then
                    If txtSearch.Text <> "" Then
                        dv.RowFilter = "[Category]  LIKE '%" & Me.cmbCompany.Text.ToString.Replace("'", "''") & "%' And [SubCategory] LIKE '%" & Me.cmbLPO.Text.ToString.Replace("'", "''") & "%' And (Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or  ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%' ) "
                    Else
                        dv.RowFilter = "[Category]  LIKE '%" & Me.cmbCompany.Text.ToString.Replace("'", "''") & "%' And [SubCategory] LIKE '%" & Me.cmbLPO.Text.ToString.Replace("'", "''") & "%' "
                    End If
                    Me.grd.DataSource = dv
                ElseIf Me.cmbCompany.Text <> strZeroIndexItem And Me.cmbLPO.Text = strZeroIndexItem Then
                    If txtSearch.Text <> "" Then
                        dv.RowFilter = "[Category]  LIKE '%" & Me.cmbCompany.Text.ToString.Replace("'", "''") & "%' And (Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or  ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%' ) "
                    Else
                        dv.RowFilter = "[Category]  LIKE '%" & Me.cmbCompany.Text.ToString.Replace("'", "''") & "%'  "
                    End If
                    Me.grd.DataSource = dv
                ElseIf Me.cmbCompany.Text = strZeroIndexItem And Me.cmbLPO.Text <> strZeroIndexItem Then
                    If txtSearch.Text <> "" Then
                        dv.RowFilter = "[SubCategory] LIKE '%" & Me.cmbLPO.Text.ToString.Replace("'", "''") & "%' And (Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or  ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%' ) "
                    Else
                        dv.RowFilter = "[SubCategory] LIKE '%" & Me.cmbLPO.Text.ToString.Replace("'", "''") & "%' "
                    End If
                    Me.grd.DataSource = dv
                ElseIf Me.txtSearch.Text <> String.Empty Then
                    dv.RowFilter = "Item_Name LIKE '%" & Me.txtSearch.Text & "%' or  [Item Code] LIKE '%" & Me.txtSearch.Text & "%' or  Color LIKE '%" & Me.txtSearch.Text & "%' or  Size LIKE '%" & Me.txtSearch.Text & "%' or  ArticleBARCode LIKE '%" & Me.txtSearch.Text & "%' "
                    Me.grd.DataSource = dv
                Else
                    Me.grd.DataSource = dt
                    lblItemName.Text = ""
                    txtRate1.Text = "0"
                End If
            End If
            FillCombo()
            If grd.RowCount > 0 Then
                lblItemName.Text = grd.CurrentRow.Cells("Item_Name").Value.ToString
                Me.txtRate1.Text = grd.CurrentRow.Cells("SalePrice").Value.ToString
            End If
            If txtSearch.Text = "" Then
                lblItemName.Text = ""
                txtRate1.Text = "0"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub ResetArticleDetail()
        Try
            FillCombosForArticle()
            cmbArticleUnit.Rows(1).Activate()
            cmbCategory.Rows(0).Activate()
            cmbColor.Rows(1).Activate()
            strSQL = "select ArticleSizeId as Id, ArticleSizeName as Name from ArticleSizeDefTable where active=1 order by ArticleSizeName, SortOrder "
            FillDropDown(Me.cmbSize, strSQL)
            cmbSize.SelectedIndex = 0
            cmbGender.Rows(0).Activate()
            cmbLPO.Rows(0).Activate()
            cmbCompany.Rows(0).Activate()
            cmbType.Rows(0).Activate()
            uitxtItemCode.Text = ""
            uitxtItemName.Text = ""
            uitxtPackQty.Text = ""
            uitxtPrice.Text = ""
            uitxtSalePrice.Text = ""
            txtMargin.Text = ""
            txtDiscount.Text = ""
            txtBarCode.Text = ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnAddArticle_Click(sender As Object, e As EventArgs) Handles btnAddArticle.Click
        Try
            FillCombosForArticle()
            Me.uitxtPackQty.Text = 1
            Me.uitxtSalePrice.Text = 0
            Me.uitxtPrice.Text = 0
            Me.txtDiscount.Text = 0
            Me.txtMargin.Text = 0
            pnlArticle.Visible = True
            btnAddArticle.Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSaveArticle_Click(sender As Object, e As EventArgs) Handles btnSaveArticle.Click
        Try
            If Not Me.IsValidate Then Exit Sub
            If New CustomArticleDAL().Add(Me.mobjModel) Then
                msg_Information(str_informSave)
            End If
            'PrintBarCode()
            ResetArticleDetail()
            GetAllForPurchaseNGRN()
            If grd.RowCount > 0 Then
                ArticleId = grd.GetRow.Cells("ArticleId").Value.ToString
                Qty = Val(txtQty.Text)
                PackName = cmbUnit.Text
                PackQty = txtPackQty.Text
                Rate = txtRate1.Text
                TotalQty = txtTotalQty.Text
                If formName = "frmPurchaseNew" Then
                    frmPurchaseNew.AddItemToGridFromItemSearch()
                    If CBool(grd.CurrentRow.Cells("ArticleBARCodeDisable").Value) = False Then
                        If flgFastBarcodePrinting = True Then
                            If Not msg_Confirm("Do you want to Print BarCode?") = True Then Exit Sub
                            PrintBarCode(ArticleId, Qty)
                        End If
                    End If
                ElseIf formName = "frmReceivingNote" Then
                    frmReceivingNote.AddItemToGridFromItemSearch()
                    If CBool(grd.CurrentRow.Cells("ArticleBARCodeDisable").Value) = False Then
                        If flgFastBarcodePrinting = True Then
                            If Not msg_Confirm("Do you want to Print BarCode?") = True Then Exit Sub
                            PrintBarCode(ArticleId, Qty)
                        End If
                    End If
                End If
            End If
            '    Me.pnlArticle.Visible = False
            '    Me.btnAddArticle.Visible = True
            '    Me.chkDisableBarCode.Checked = True
            '    Me.grd.Location = New Point(0, 138)
            'Me.grd.Size = New Point(1017, 310)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Validating before saving the Item
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>TFS3764 : Ayesha Rehamn : 17-07-2018</remarks>
    Public Function IsValidate()
        Try
            If Not Me.cmbCategory.Value > 0 Then
                ShowErrorMessage("Please select category")
                Me.cmbCategory.Focus()
                Return False
            ElseIf Not Me.cmbType.Value > 0 Then
                ShowErrorMessage("Please select type")
                Me.cmbType.Focus()
                Return False
            ElseIf Not Me.cmbArticleUnit.Value > 0 Then
                ShowErrorMessage("Please select unit")
                Me.cmbUnit.Focus()
                Return False
            ElseIf Me.uitxtItemCode.Text = String.Empty Then
                ShowErrorMessage("Please enter valid item code")
                Me.uitxtItemCode.Focus()
                Return False

            ElseIf Me.uitxtItemName.Text = String.Empty Then
                ShowErrorMessage("Please enter valid item name")
                Me.uitxtItemName.Focus()
                Return False


            ElseIf Not (Me.cmbSize.SelectedValue > 0 Or Me.cmbSize.Text <> strZeroIndexItem) Then
                ShowErrorMessage("Please select size")
                Me.cmbSize.Focus()
                Return False


            ElseIf Not Me.cmbColor.Value > 0 Then
                ShowErrorMessage("Please select color")
                Me.cmbColor.Focus()
                Return False

                If Not Me.uitxtPrice.Text > 0 Then
                    msg_Error("Please enter valid purchase price")
                    Me.uitxtPrice.Focus()
                    Exit Function
                End If

                If Not Me.uitxtSalePrice.Text > 0 Then
                    msg_Error("Please enter valid sale price")
                    Me.uitxtSalePrice.Focus()
                    Exit Function
                End If

            ElseIf Not Me.txtPackQty.Text > 0 Then
                ShowErrorMessage("Please enter minimum pack quantity of 1")
                Me.uitxtPackQty.Focus()
                Return False
            ElseIf Me.chkDisableBarCode.Checked = True AndAlso Me.txtBarCode.Text.Length = 0 Then
                ShowErrorMessage("Pre Printed Barcode is required.")
                Me.txtBarCode.Focus()
                Return False
            End If

            'Article Code Validate On Aritlce Master Table .... 
            Dim str As String = "Select * From ArticleDefTableMaster WHERE ArticleCode='" & Me.uitxtItemCode.Text.Replace("'", "''") & "'"
            Dim dtItemCode As DataTable = GetDataTable(str)
            If dtItemCode.Rows.Count > 0 Then
                ShowErrorMessage("Item Code Already Exist " & vbCrLf & dtItemCode.Rows(0).Item(1).ToString + "-" + dtItemCode.Rows(0).Item(2).ToString)
                Me.uitxtItemCode.Focus()
                Return False
            End If

            ''Start TFS4395
            If Me.chkDisableBarCode.Checked = True Then
                ''Start TFS4395
                Dim strItemCode As String = "Select * From ArticleDefTable WHERE ArticleBARCode='" & Me.txtBarCode.Text.Replace("'", "''") & "'"
                Dim dtItemBarCode As DataTable = GetDataTable(strItemCode)
                If dtItemBarCode.Rows.Count > 0 Then
                    ShowErrorMessage("Item ArticleBARCode Already Exist " & vbCrLf & dtItemBarCode.Rows(0).Item(1).ToString + "-" + dtItemBarCode.Rows(0).Item(2).ToString)
                    Me.txtBarCode.Focus()
                    Return False
                End If

                Dim strMultiCode As String = "Select ArticleCode,ArticleName from ArticleBarcodeDefTable WHERE ArticleBARCode='" & Me.txtBarCode.Text.Replace("'", "''") & "'"
                Dim dtItemMultiCode As DataTable = GetDataTable(strMultiCode)
                If dtItemMultiCode.Rows.Count > 0 Then
                    ShowErrorMessage("Item ArticleBARCode Already Exist " & vbCrLf & dtItemMultiCode.Rows(0).Item(0).ToString + "-" + dtItemMultiCode.Rows(0).Item(1).ToString)
                    Me.txtBarCode.Focus()
                    Return False
                End If
        ''End TFS4395

            End If
        ''End TFS4395

        Me.FillModelForArticle()

        Return True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Private Sub uitxtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles uitxtPrice.KeyPress, uitxtPackQty.KeyPress, uitxtSalePrice.KeyPress, txtRate1.KeyPress, txtPackQty.KeyPress, txtQty.KeyPress, txtTotalQty.KeyPress, txtMargin.KeyPress, txtDiscount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub uitxtItemCode_TextChanged(sender As Object, e As EventArgs) Handles uitxtItemCode.TextChanged
    '    Try
    '        ''Enternig the same ItemCode in the bar Code
    '        txtBarCode.Text = uitxtItemCode.Text
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    ''' <summary>
    ''' A new function is made to Print bar Code while item is saved  For Customers (MALL)
    ''' </summary>
    ''' <remarks>TFS3762 : Ayesha Rehman : 12-07-2018</remarks>
    Private Sub PrintBarCode(ByVal ArticleId As Integer, ByVal NoOfPacks As Integer)
        Try

            Dim PrintBarcode As PrintBarCode
            PrintBarcode = New PrintBarCode()
            PrintBarcode.Articleid = ArticleId
            PrintBarcode.NoOfCopies = NoOfPacks
            PrintBarcode.PrintBarCode()
        Catch ex As Exception
            Throw ex
        End Try
        'Dim str As String = ""
        'Dim conn As New SqlConnection(SQLHelper.CON_STR)
        'Dim trans As SqlTransaction
        'Dim PrinterName As String = ""
        'Dim PrintCount As Integer = 1
        'Dim PrintFont As String = "Verdana"
        'Dim PrintFontSize As Integer = 8
        'Try
        '    If conn.State = ConnectionState.Closed Then
        '        conn.Open()
        '    End If
        '    trans = conn.BeginTransaction

        '    str = "Select Top(1) ArticleBARCodeDisable from ArticleDefView order by ArticleId desc "
        '    Dim flgDisabled As Boolean = CType(SQLHelper.ExecuteScaler(trans, CommandType.Text, str), Boolean)
        '    If flgDisabled = True Then Exit Sub
        '    str = "Select Top(1) ArticleId from ArticleDefView order by ArticleId desc "
        '    Dim ArticleId As Integer = CType(SQLHelper.ExecuteScaler(trans, CommandType.Text, str), Integer)
        '    Dim dt As DataTable = DTBarCode(ArticleId)
        '    If flgDisabled = False Then
        '        PrinterName = getConfigValueByType("PrinterNameForBarCode").ToString()
        '        PrintFont = getConfigValueByType("BarCodeFont").ToString()
        '        PrintFontSize = Val(getConfigValueByType("BarCodeFontSize").ToString())
        '        PrintCount = Val(getConfigValueByType("PrintCountForBarCode").ToString())
        '        ShowReport("rptArticleBarCode", , , , True, , , dt, , , , , , , , PrinterName, PrintCount, PrintFont, PrintFontSize)
        '    End If

        '    trans.Commit()

        'Catch ex As Exception
        '    trans.Rollback()
        '    Throw ex
        'Finally
        '    conn.Close()
        'End Try


    End Sub
    Private Function DTBarCode(ByVal ArticleID As Int32) As DataTable 'TASK42
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim cbProname As Boolean = False
            Dim cbProPrice As Boolean = False
            Dim cbProductCode As Boolean = False
            Dim cbVendorCode As Boolean = False
            Dim cbPackQuantity As Boolean = False
            Dim cbValues As String = String.Empty
            cbValues = getConfigValueByType("BARCodeDisplayInformation").ToString()
            If cbValues.Length > 0 Then
                Dim arday() As String = cbValues.Split("|")
                If arday.Length > 4 Then
                    'PN&False|PP&True|PC&True|VC&True|PQ&False
                    cbProname = Convert.ToBoolean(arday(0).Trim.Substring(3))
                    cbProPrice = Convert.ToBoolean(arday(1).Trim.Substring(3))
                    cbProductCode = Convert.ToBoolean(arday(2).Trim.Substring(3))
                    cbVendorCode = Convert.ToBoolean(arday(3).Trim.Substring(3))
                    cbPackQuantity = Convert.ToBoolean(arday(4).Trim.Substring(3))
                End If
            End If
            Dim DT As New DataTable

            DT = GetDataTable("ArticleBarCode " & ArticleID & "," & cbProname & "," & cbProductCode & "," & cbProPrice & "," & cbVendorCode & "," & cbPackQuantity & "")
            DT.AcceptChanges()

            For Each DR As DataRow In DT.Rows
                DR.BeginEdit()
                Dim bcp As New BarcodeProfessional()

                bcp.Symbology = Symbology.Code128

                bcp.BarcodeUnit = BarcodeUnit.Inch
                bcp.BarWidth = 0.01
                bcp.BarHeight = 0.15
                bcp.Extended = True
                bcp.DisplayCode = False

                ' bcp.Text=Symbology.
                'bcp.Text = String.Empty
                'bcp.Symbology = Symbology.Code128

                bcp.AddChecksum = False

                bcp.Code = DR.Item("ArticleBARCode").ToString
                DR.Item("BarCode") = bcp.GetBarcodeImage(System.Drawing.Imaging.ImageFormat.Png)
                DR.EndEdit()
            Next
            Return DT
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Function

    Private Sub chkDisableBarCode_CheckedChanged(sender As Object, e As EventArgs) Handles chkDisableBarCode.CheckedChanged
        If Me.chkDisableBarCode.Checked Then
            lblBarCode.Visible = True
            Me.txtBarCode.Visible = True
            Me.btnSaveArticle.Location = New Point(638, 210)
            ''638, 160
        Else
            lblBarCode.Visible = False
            Me.txtBarCode.Visible = False
            Me.btnSaveArticle.Location = New Point(638, 130)

            ''638, 108
        End If
    End Sub

    Private Sub txtMargin_TextChanged(sender As Object, e As EventArgs) Handles txtMargin.TextChanged
        Try
            uitxtPrice.Text = Val(uitxtSalePrice.Text) - (Val(uitxtSalePrice.Text) * Val(txtMargin.Text) / 100)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged, txtPackQty.TextChanged
        Try
            txtTotalQty.Text = Val(txtQty.Text) * Val(txtPackQty.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
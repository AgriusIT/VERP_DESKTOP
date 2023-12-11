Imports SBModel

Public Class frmRptPurchaseGRNRejectedQty
    Private Sub FillCombo(Optional ByVal Condition As String = "")
        Dim Str As String = ""
        Try
            If Condition = "Product" Then
                Str = " SELECT DISTINCT ArticleDefView.ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination, ArticleUnitName as Unit, ISNULL(PurchasePrice,0) as Price, Isnull(StockDetail.Stock,0) as Stock, ArticleDefView.ArticleCompanyName as Category,ArticleDefView.ArticleLpoName as Model , ArticleDefView.SizeRangeID as [Size ID], Location.Ranks as Rake, Isnull(ArticleDefView.SubSubId,0) as AccountId, ArticleDefView.SortOrder, ArticleDefView.MasterId " & _
                      " FROM ArticleDefView LEFT OUTER JOIN (Select ArticalID, Ranks From ArticalDefLocation WHERE Ranks <> '' AND Ranks IS NOT NULL) Location  ON Location.ArticalID = ArticleDefView.MasterId " & _
                      " LEFT JOIN (Select ArticleDefId, Sum(IsNull(InQty, 0)-IsNull(OutQty, 0)) As Stock From StockDetailTable Group By ArticleDefId) As StockDetail ON ArticleDefView.ArticleId = StockDetail.ArticleDefId " & _
                      " WHERE ArticleDefView.Active=1 "
                FillUltraDropDown(Me.cmbProduct, Str)
                Me.cmbProduct.Rows(0).Activate()
                If Me.cmbProduct.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbProduct.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                    Me.cmbProduct.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                    Me.cmbProduct.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True
                    Me.cmbProduct.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                    Me.cmbProduct.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                    If Me.rbCode.Checked = True Then
                        Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(1).Column.Key.ToString
                    Else
                        Me.cmbProduct.DisplayMember = Me.cmbProduct.Rows(0).Cells(2).Column.Key.ToString
                    End If
                End If
            ElseIf Condition = "Project" Then
                FillDropDown(Me.cmbProject, "Select CostCenterId, Name From tblDefCostCenter WHERE Active=1")
                'ElseIf Condition = "Employee" Then
                '    FillDropDown(Me.cmbVendor, "Select Employee_ID, Employee_Name From tblDefEmployee Where Active = 1 " & IIf(Me.cmbDepartment.SelectedValue > 0, " AND Dept_ID = " & Me.cmbDepartment.SelectedValue & "", "") & " " & IIf(Me.cmbProject.SelectedValue > 0, " AND CostCentre = " & Me.cmbProject.SelectedValue & "", "") & " ORDER BY 2 ASC")
            ElseIf Condition = "Department" Then
                'FillDropDown(Me.cmbDepartment, "Select ArticleGroupId, ArticleGroupName From ArticleGroupDefTable WHERE Active=1 ORDER BY 2 ASC")
                FillListBox(Me.UiListControl1.ListItem, "Select ArticleGroupId, ArticleGroupName From ArticleGroupDefTable WHERE Active=1 ORDER BY 2 ASC")

            ElseIf Condition = "Vendor" Then
                Str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name, dbo.vwCOADetail.detail_code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                                                "dbo.tblListTerritory.TerritoryName as Territory,dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                                                "FROM dbo.tblVendor INNER JOIN " & _
                                                "dbo.tblListTerritory ON dbo.tblVendor.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                                                "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                                                "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                                                "dbo.vwCOADetail ON dbo.tblVendor.AccountId = dbo.vwCOADetail.coa_detail_id " & _
                                                "WHERE  1 = 1 "
                ''Start TFS2124
                If getConfigValueByType("Show Customer On Purchase") = "True" Then
                    Str += " AND dbo.vwCOADetail.Account_Type in ('Vendor','Customer') "
                Else
                    Str += " AND  dbo.vwCOADetail.Account_Type='Vendor' "
                End If
                ''End TFS2124
                Str += " order by tblVendor.Sortorder, vwCOADetail.detail_title "
                FillUltraDropDown(cmbVendor, Str)
                If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbVendor.Rows(0).Activate()
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head" 'Task:2373 Change Caption
                End If
            ElseIf Condition = "Location" Then
                FillDropDown(Me.cmbLocation, "Select Location_Id, Location_Name From tblDefLocation")
            ElseIf Condition = "Type" Then
                'FillDropDown(Me.cmbType, "Select ArticleTypeId, ArticleTypeName From ArticleTypeDefTable WHERE Active=1 ORDER BY 2 ASC")
                FillListBox(Me.lstType.ListItem, "Select ArticleTypeId, ArticleTypeName From ArticleTypeDefTable WHERE Active=1 ORDER BY 2 ASC")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub frmRptProductionBasedSalary_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            FillCombo("Vendor")
            FillCombo("Project")
            FillCombo("Location")
            FillCombo("Product")
            FillCombo("Location")
            FillCombo("Department")
            FillCombo("Type")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbProject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProject.SelectedIndexChanged
        Try
            If Not cmbProject.SelectedIndex = -1 Then
                FillCombo("Employee")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            GetData()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Purchase GRN rejected quantity " & Chr(10) & "From Date: " & DateTimePicker1.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & DateTimePicker1.Value.ToString("dd-MM-yyyy").ToString & ""
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GridEX1_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles GridEX1.FormattingRow

    End Sub

    Private Sub GetData()
        Dim dt As DataTable
        Dim Str As String = ""
        Try
            If rbPurchase.Checked = True Then
                'For purchase section
                Str = " SELECT Article.ArticleId, Article.ArticleGroupName, Article.ArticleTypeName, Article.ArticleCompanyName, Article.ArticleCode, Article.ArticleDescription, Article.ArticleUnitName, Article.ArticleColorName , Article.ArticleSizeName , " _
                & " IsNull(Detail.ReceivedQty, 0) AS ReceivedQty, IsNull(Detail.RejectedQty, 0) AS RejectedQty, IsNull(Detail.Qty, 0) AS TotalQty " _
                & " FROM ArticleDefView AS Article INNER JOIN ReceivingDetailTable AS Detail ON Article.ArticleId = Detail.ArticleDefId " _
                & " INNER JOIN ReceivingMasterTable AS Receiving ON Detail.ReceivingId = Receiving.ReceivingId " _
                & " LEFT OUTER JOIN tblDefLocation AS Location ON Detail.LocationId = Location.location_id " _
                & " LEFT OUTER JOIN tblDefCostCenter AS Project ON Receiving.CostCenterId= Project.CostCenterID " _
                & " WHERE IsNull(Detail.RejectedQty, 0) > 0 And LEFT(Receiving.ReceivingNo, 3) = 'Pur' AND Convert(varchar, Receiving.ReceivingDate, 102) Between Convert(DateTime, '" & DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "', 102) And Convert(DateTime, '" & DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "', 102) " _
                & " " & IIf(Me.UiListControl1.SelectedIDs.Length > 0, " And Article.ArticleGroupId In (" & Me.UiListControl1.SelectedIDs & ")", "") & " " & IIf(Me.lstType.SelectedIDs.Length > 0, " And Article.ArticleTypeId In (" & Me.lstType.SelectedIDs & ")", "") & " "

                If Me.cmbVendor.Value > 0 Then
                    Str += " And Receiving.VendorId = " & Me.cmbVendor.Value & ""
                End If
                If Me.cmbProject.SelectedIndex > 0 Then
                    Str += " And Receiving.CostCenterId = " & Me.cmbProject.SelectedValue & ""
                End If
                If Me.cmbLocation.SelectedIndex > 0 Then
                    Str += " And Detail.LocationId = " & Me.cmbLocation.SelectedValue & ""
                End If
                If Me.cmbProduct.Value > 0 Then
                    Str += " And Article.ArticleId = " & Me.cmbProduct.Value & ""
                End If
            Else
                'For GRN Section
                Str = " SELECT Article.ArticleId, Article.ArticleGroupName, Article.ArticleTypeName, Article.ArticleCompanyName, Article.ArticleCode, Article.ArticleDescription, Article.ArticleUnitName, Article.ArticleColorName , Article.ArticleSizeName , " _
                  & " IsNull(Detail.ReceivedQty, 0) AS ReceivedQty, IsNull(Detail.RejectedQty, 0) AS RejectedQty, IsNull(Detail.Qty, 0) AS TotalQty " _
                  & " FROM ArticleDefView AS Article INNER JOIN ReceivingNoteDetailTable AS Detail ON Article.ArticleId = Detail.ArticleDefId " _
                  & "  INNER JOIN ReceivingNoteMasterTable AS Receiving ON Detail.ReceivingNoteId = Receiving.ReceivingNoteId " _
                  & " LEFT OUTER JOIN tblDefLocation AS Location ON Detail.LocationId = Location.location_id " _
                  & "  LEFT OUTER JOIN tblDefCostCenter AS Project ON Receiving.CostCenterId= Project.CostCenterID " _
                  & " WHERE IsNull(Detail.RejectedQty, 0) > 0 And Convert(varchar, Receiving.ReceivingDate, 102) Between Convert(DateTime, '" & DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") & "', 102) And Convert(DateTime, '" & DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "', 102)" _
                   & " " & IIf(Me.UiListControl1.SelectedIDs.Length > 0, " And Article.ArticleGroupId In (" & Me.UiListControl1.SelectedIDs & ")", "") & " " & IIf(Me.lstType.SelectedIDs.Length > 0, " And Article.ArticleTypeId In (" & Me.lstType.SelectedIDs & ")", "") & " "
                If Me.cmbVendor.Value > 0 Then
                    Str += " And Receiving.VendorId = " & Me.cmbVendor.Value & ""
                End If
                If Me.cmbProject.SelectedIndex > 0 Then
                    Str += " And Receiving.CostCenterId = " & Me.cmbProject.SelectedValue & ""
                End If
                If Me.cmbLocation.SelectedIndex > 0 Then
                    Str += " And Detail.LocationId = " & Me.cmbLocation.SelectedValue & ""
                End If
                If Me.cmbProduct.Value > 0 Then
                    Str += " And Article.ArticleId = " & Me.cmbProduct.Value & ""
                End If
            End If
            dt = GetDataTable(Str)
            dt.AcceptChanges()
            GridEX1.DataSource = dt
            GridEX1.RetrieveStructure()
            ''Caption section
            GridEX1.RootTable.Columns("ArticleGroupName").Caption = "Department"
            GridEX1.RootTable.Columns("ArticleTypeName").Caption = "Type"
            GridEX1.RootTable.Columns("ArticleCompanyName").Caption = "Category"
            GridEX1.RootTable.Columns("ArticleCode").Caption = "Article Code"
            GridEX1.RootTable.Columns("ArticleDescription").Caption = "Category"
            GridEX1.RootTable.Columns("ArticleUnitName").Caption = "Unit"
            GridEX1.RootTable.Columns("ArticleColorName").Caption = "Color"
            GridEX1.RootTable.Columns("ArticleSizeName").Caption = "Size"

            ''End caption section


            ''Visible hide section
            GridEX1.RootTable.Columns("ArticleId").Visible = False

            ''End visible hide section

            GridEX1.RootTable.Columns("ReceivedQty").FormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("RejectedQty").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("TotalQty").FormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("ReceivedQty").TotalFormatString = "N" & DecimalPointInQty
            GridEX1.RootTable.Columns("RejectedQty").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("TotalQty").TotalFormatString = "N" & DecimalPointInValue
            GridEX1.RootTable.Columns("ReceivedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("RejectedQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("TotalQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("ReceivedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("RejectedQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            GridEX1.RootTable.Columns("TotalQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far

            GridEX1.RootTable.Columns("ReceivedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("RejectedQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            GridEX1.RootTable.Columns("TotalQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Exit Sub
                End If
            Else
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                        ''TASK TFS1384 replaced Report Print and Report Export with Report Print and Report Export on 07-09-2017
                    ElseIf RightsDt.FormControlName = "Report Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Report Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True
                        ''End TASK TFS1384
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Dim ID As Integer = 0
        Try
            ID = Me.cmbVendor.Value
            FillCombo("Vendor")
            Me.cmbVendor.Value = ID

            ID = Me.cmbProject.SelectedValue
            FillCombo("Project")
            Me.cmbProject.SelectedValue = ID

            ID = Me.cmbLocation.SelectedValue
            FillCombo("Location")
            Me.cmbLocation.SelectedValue = ID

            ID = Me.cmbProduct.Value
            FillCombo("Product")
            Me.cmbProduct.Value = ID

            FillCombo("Department")
            FillCombo("Type")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
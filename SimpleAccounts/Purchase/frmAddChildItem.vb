Imports System.Windows.Forms
Imports System.Data.OleDb

Public Class frmAddChildItem
    Public UnitId As Integer = 0
    Public TypeId As Integer = 0
    Public CategoryId As Integer = 0
    Public SubCategoryId As Integer = 0
    Public OriginId As Integer = 0
    'Public Sub New()

    '    ' This call is required by the designer.
    '    InitializeComponent()

    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub

    'Public Sub New(ByVal UnitId As Integer, ByVal Unit As String, ByVal TypeId As Integer, ByVal Type As String,
    '               ByVal CategoryId As Integer, ByVal Category As String, ByVal SubCategoryId As Integer, ByVal SubCategory As String,
    '               ByVal OriginId As Integer, ByVal Origin As String)
    'Public Sub New(ByVal UnitId As Integer, ByVal TypeId As Integer,
    '             ByVal CategoryId As Integer, ByVal SubCategoryId As Integer,
    '             ByVal OriginId As Integer)
    '    ' This call is required by the designer.
    '    InitializeComponent()
    '    ' Add any initialization after the InitializeComponent() call.
    '    FillAllCombos()
    '    ''
    '    Me.cmbUnit.Value = UnitId
    '    Me.cmbType.Value = TypeId
    '    Me.cmbCategory.Value = CategoryId
    '    Me.cmbSubCategory.Value = SubCategoryId
    '    Me.cmbOrigin.Value = OriginId
    '    ''
    '    Me.cmbUnit.Enabled = True
    '    Me.cmbType.Enabled = True
    '    Me.cmbCategory.Enabled = True
    '    Me.cmbSubCategory.Enabled = True
    '    Me.cmbOrigin.Enabled = True
    '    Me.txtReferenceNumber.Text = "Reference Number"
    '    Me.txtQty.Text = "Qty"
    '    Me.txtComments.Text = "Comments"
    '    Me.txtRequirementDescription.Text = ""
    'End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            If frmVendorQuotation.grdItems.DataSource Is Nothing Then
                frmVendorQuotation.DisplayDetailForHistory(-1)
            End If
            frmVendorQuotation.AddChildToGrid(Me.txtRequirementDescription.Text, Me.txtQty.Text, Me.txtReferenceNumber.Text, IIf(Me.cmbItem.Value > 0, Me.cmbItem.Value, 0), IIf(Me.cmbItem.Value > 0, Me.cmbItem.ActiveRow.Cells("Code").Value.ToString, ""), IIf(Me.cmbItem.Value > 0, Me.cmbItem.ActiveRow.Cells("Item").Value.ToString, ""), IIf(Me.cmbUnit.Value > 0, Me.cmbUnit.Value, 0), IIf(Me.cmbUnit.Value > 0, Me.cmbUnit.Text, ""), IIf(Me.cmbType.Value > 0, Me.cmbType.Value, 0), IIf(Me.cmbType.Value > 0, Me.cmbType.Text, ""), IIf(Me.cmbCategory.Value > 0, Me.cmbCategory.Value, 0), IIf(Me.cmbCategory.Value > 0, Me.cmbCategory.Text, ""), IIf(Me.cmbSubCategory.Value > 0, Me.cmbSubCategory.Value, 0), IIf(Me.cmbSubCategory.Value > 0, Me.cmbSubCategory.Text, ""), IIf(Me.cmbOrigin.Value > 0, Me.cmbOrigin.Value, 0), IIf(Me.cmbOrigin.Value > 0, Me.cmbOrigin.Text, ""), Me.txtComments.Text)
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Public Sub FillCombos(Optional Condition As String = "")
        Try

            Dim strSQL As String

            If Condition = "" Or Condition = "Customer" Then
                'strSQL = String.Empty
                'strSQL = "SELECT    IsNull(vwCOADetail.coa_detail_id,0) as coa_detail_id, vwCOADetail.detail_title as Name, dbo.vwCOADetail.account_type as Type,dbo.vwCOADetail.Contact_address as Address," _
                '        & " dbo.vwCOADetail.Contact_Mobile as MobileNo, dbo.vwCOADetail.Contact_Phone as Phone,dbo.vwCOADetail.Contact_Email as Email " _
                '        & " FROM  vwCOADetail WHERE vwCOADetail.account_type in('Vendor', 'Customer') and  vwCOADetail.coa_detail_id is not  null"
                'FillUltraDropDown(Me.cmbReference, strSQL)
                'Me.cmbReference.Rows(0).Activate()
                'Me.cmbReference.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
            ElseIf Condition = "Type" Then
                strSQL = "select ArticleTypeId as Id, ArticleTypeName as Name, TypeCode from ArticleTypeDefTable where active=1 order by sortOrder"
                FillUltraDropDownType(Me.cmbType, strSQL)
                If Me.cmbType.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbType.Rows(0).Activate()
                    Me.cmbType.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If

            ElseIf Condition = "Origin" Then
                strSQL = "select ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable where active=1 order by sortOrder"
                FillUltraDropDownOrigin(Me.cmbOrigin, strSQL)
                If Me.cmbOrigin.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbOrigin.Rows(0).Activate()
                    Me.cmbOrigin.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
            ElseIf Condition = "Unit" Then
                ''filling Unit combo
                strSQL = "select ArticleUnitId as Id, ArticleUnitName as Name from ArticleUnitDefTable where active=1 order by sortOrder"
                'strSQL = "Select Distinct ArticlePackId, PackName As Name, PackQty, ArticleMasterId From ArticleDefPackTable order by ArticlePackId"
                FillUltraDropDownUnit(Me.cmbUnit, strSQL)
                If Me.cmbUnit.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbUnit.Rows(0).Activate()
                    Me.cmbUnit.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
            ElseIf Condition = "SubCategory" Then
                strSQL = "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID, dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId , dbo.ArticleLpoDefTable.SubCategoryCode  FROM   dbo.ArticleLpoDefTable INNER JOIN dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId"
                FillUltraDropDownSubCategory(Me.cmbSubCategory, strSQL)
                If Me.cmbSubCategory.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbSubCategory.Rows(0).Activate()
                    Me.cmbSubCategory.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                End If
            ElseIf Condition = "Category" Then
                strSQL = "SELECT  ArticleCompanyId AS ID, ArticleCompanyName AS Name, CategoryCode" & _
                    " FROM ArticleCompanyDefTable"
                FillUltraDropDownCategory(Me.cmbCategory, strSQL)
                If Me.cmbCategory.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbCategory.Rows(0).Activate()
                    Me.cmbCategory.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                End If

            ElseIf Condition = "TypeAgainstItem" Then
                strSQL = "select ArticleTypeId as Id, ArticleTypeName as Name, TypeCode from ArticleTypeDefTable where ArticleTypeId=" & Val(Me.cmbItem.ActiveRow.Cells("ArticleTypeId").Value.ToString) & " And active=1 order by sortOrder"
                FillUltraDropDownType(Me.cmbType, strSQL)
                If Me.cmbType.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbType.Rows(0).Activate()
                    Me.cmbType.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If

            ElseIf Condition = "OriginAgainstItem" Then
                strSQL = "select ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable where  ArticleGenderId=" & Val(Me.cmbItem.ActiveRow.Cells("ArticleGenderId").Value.ToString) & " And active=1 order by sortOrder"
                FillUltraDropDownOrigin(Me.cmbOrigin, strSQL)
                If Me.cmbOrigin.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbOrigin.Rows(0).Activate()
                    Me.cmbOrigin.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
            ElseIf Condition = "UnitAgainstItem" Then
                ''filling Unit combo
                strSQL = "select ArticleUnitId as Id, ArticleUnitName as Name from ArticleUnitDefTable where ArticleUnitId=" & Val(Me.cmbItem.ActiveRow.Cells("ArticleUnitId").Value.ToString) & " And active=1 order by sortOrder"
                'strSQL = "Select Distinct ArticlePackId, PackName As Name, PackQty, ArticleMasterId From ArticleDefPackTable order by ArticlePackId"
                FillUltraDropDownUnit(Me.cmbUnit, strSQL)
                If Me.cmbUnit.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbUnit.Rows(0).Activate()
                    Me.cmbUnit.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                End If
            ElseIf Condition = "SubCategoryAgainstItem" Then
                strSQL = "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID, dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId , dbo.ArticleLpoDefTable.SubCategoryCode  FROM   dbo.ArticleLpoDefTable INNER JOIN dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId Where dbo.ArticleLpoDefTable.ArticleLpoId =" & Val(Me.cmbItem.ActiveRow.Cells("ArticleLPOId").Value.ToString) & ""
                FillUltraDropDownSubCategory(Me.cmbSubCategory, strSQL)
                If Me.cmbSubCategory.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbSubCategory.Rows(0).Activate()
                    Me.cmbSubCategory.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                End If
            ElseIf Condition = "CategoryAgainstItem" Then
                strSQL = "SELECT  ArticleCompanyId AS ID, ArticleCompanyName AS Name Where ArticleCompanyId =" & Val(Me.cmbItem.ActiveRow.Cells("ArticleCategoryId").Value.ToString) & "" & _
                    " FROM ArticleCompanyDefTable"
                FillUltraDropDownCategory(Me.cmbCategory, strSQL)
                If Me.cmbCategory.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbCategory.Rows(0).Activate()
                    Me.cmbCategory.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                End If
            ElseIf Condition = "Item" Then
                'FillUltraDropDown(Me.cmbItem, "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item,  ArticleSizeName as Size, ArticleColorName as Combination,Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SalePrice,0) as Price, ArticleDefView.SizeRangeID as [Size ID], ArticleDefView.MasterId FROM ArticleDefView where Active=1 " & IIf(flgCompanyRights = True, " AND ArticleDefView.CompanyId=" & MyCompanyId & "", "") & "")
                FillUltraDropDownItem(Me.cmbItem, "SELECT ArticleDefTable.ArticleId as Id, ArticleDefTable.ArticleDescription as Item, ArticleDefTable.ArticleCode as Code, dbo.ArticleSizeDefTable.ArticleSizeName as Size, dbo.ArticleColorDefTable.ArticleColorName as Combination, Isnull(ArticleDefTable.PurchasePrice,0) as PurchasePrice, Isnull(ArticleDefTable.SalePrice,0) as Price, ArticleDefTable.SizeRangeID as [Size ID], ArticleDefTable.MasterId, ArticleDefTable.ArticleGenderId, ArticleDefTable.ArticleCategoryId, ArticleDefTable.ArticleLPOId, ArticleDefTable.ArticleUnitId, ArticleDefTable.ArticleTypeId FROM ArticleDefTable Left Join dbo.ArticleColorDefTable ON ArticleDefTable.ArticleColorId = dbo.ArticleColorDefTable.ArticleColorId Left Join dbo.ArticleSizeDefTable On ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId Where ArticleDefTable.Active=1 ")
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleGenderId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleCategoryId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleLPOId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleUnitId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ArticleTypeId").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Header.Caption = "Price"
                'ElseIf Condition = "Location" Then
                '    strSQL = "SELECT  LocationId AS ID, LocationTitle AS Name " & _
                '        " FROM tblDefCompanyLocations"
                '    FillUltraDropDown(Me.cmbCompanyLocation, strSQL)
                '    If Me.cmbCompanyLocation.DisplayLayout.Bands.Count > 0 Then
                '        Me.cmbCompanyLocation.Rows(0).Activate()
                '        Me.cmbCompanyLocation.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                '    End If
                'ElseIf Condition = "ContactPerson" Then
                '    strSQL = "SELECT  PK_Id AS ID, ContactName AS Name, RefCompanyId" & _
                '        " FROM TblCompanyContacts"
                '    FillUltraDropDown(Me.cmbContactPerson, strSQL)
                '    If Me.cmbContactPerson.DisplayLayout.Bands.Count > 0 Then
                '        Me.cmbContactPerson.Rows(0).Activate()
                '        Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("ID").Hidden = True
                '        Me.cmbContactPerson.DisplayLayout.Bands(0).Columns("Name").Hidden = True
                '    End If

                'ElseIf Condition = "IndentDepartment" Then
                '    strSQL = "SELECT Distinct IndentingDepartment, IndentingDepartment " & _
                '        " FROM PurchaseInquiryMaster"
                '    FillDropDown(Me.cmbIndentDept, strSQL)
            End If
            'ElseIf Condition = "Customer" Then
            '    strSQL = "Select coa_detail_id, detail_title From vwCOADetail WHERE account_type='Vendor'"
            '    FillUltraDropDown(Me.cmbReference, strSQL)
            '    If Me.cmbReference.DisplayLayout.Bands.Count > 0 Then
            '        Me.cmbReference.Rows(0).Activate()
            '    End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Public Sub FillUltraDropDownType(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql


            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)

            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)

                dr(1) = "Type of item" 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If

            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt

            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub
    Public Sub FillUltraDropDownOrigin(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql


            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)

            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)

                dr(1) = "Origin" 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If

            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt

            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub
    Public Sub FillUltraDropDownUnit(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql


            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)

            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)

                dr(1) = "Unit" 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If

            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt

            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub
    Public Sub FillUltraDropDownCategory(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text


            objCommand.CommandText = strSql


            Dim dt As New DataTable
            Dim dr As DataRow

            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)

            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)

                dr(1) = "Category" 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If

            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt

            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing

        End Try
    End Sub
    Public Sub FillUltraDropDownSubCategory(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)

        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()

            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = strSql
            Dim dt As New DataTable
            Dim dr As DataRow
            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)
            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)

                dr(1) = "Sub Category" 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If
            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        End Try
    End Sub
    Public Sub FillUltraDropDownItem(ByVal objDropDwon As Infragistics.Win.UltraWinGrid.UltraCombo, ByVal strSql As String, Optional ByVal ZeroIndex As Boolean = True)
        Dim objCommand As New OleDbCommand
        Dim objDataAdapter As New OleDbDataAdapter
        Dim objDataSet As New DataSet
        Try
            If Con.State = ConnectionState.Open Then Con.Close()
            Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = strSql
            Dim dt As New DataTable
            Dim dr As DataRow
            objDataAdapter.SelectCommand = objCommand
            objDataAdapter.Fill(dt)
            If ZeroIndex Then
                dr = dt.NewRow
                dr(0) = Convert.ToInt32(0) '(objDataSet.Tables(0).Columns(0).ColumnName)
                dr(1) = "Select an item" 'Convert.ToString("Select any Value") 'objDataSet.Tables(0).Columns(1).ColumnName)
                dt.Rows.InsertAt(dr, 0)
            End If
            objDropDwon.DisplayMember = dt.Columns(1).ColumnName.ToString() 'objDataSet.Tables(0).Columns(1).ColumnName
            objDropDwon.ValueMember = dt.Columns(0).ColumnName.ToString() 'objDataSet.Tables(0).Columns(0).ColumnName)
            objDropDwon.DataSource = dt
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Catch ex As Exception
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        Finally
            'Con = Nothing
            objCommand = Nothing
            objDataAdapter = Nothing
            objDataSet = Nothing
        End Try
    End Sub


    Private Sub txtQty_MouseHover(sender As Object, e As EventArgs) Handles txtQty.MouseHover
        Try
            If Me.txtQty.Text = "Qty" Then
                Me.txtQty.Text = ""
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillAllCombos()
        Try
            FillCombos("Item")
            FillCombos("Type")
            FillCombos("Origin")
            FillCombos("Unit")
            FillCombos("Category")
            FillCombos("SubCategory")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_MouseLeave(sender As Object, e As EventArgs) Handles txtQty.MouseLeave
        Try
            If Me.txtQty.Text = "" Then
                Me.txtQty.Text = "Qty"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtReferenceNumber_MouseHover(sender As Object, e As EventArgs) Handles txtReferenceNumber.MouseHover
        Try
            If Me.txtReferenceNumber.Text = "Reference Number" Then
                Me.txtReferenceNumber.Text = ""
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtReferenceNumber_MouseLeave(sender As Object, e As EventArgs) Handles txtReferenceNumber.MouseLeave
        Try
            If Me.txtReferenceNumber.Text = "" Then
                Me.txtReferenceNumber.Text = "Reference Number"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtComments_MouseHover(sender As Object, e As EventArgs) Handles txtComments.MouseHover
        Try
            If Me.txtComments.Text = "Comments" Then
                Me.txtComments.Text = ""
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtComments_MouseLeave(sender As Object, e As EventArgs) Handles txtComments.MouseLeave
        Try
            If Me.txtComments.Text = "" Then
                Me.txtComments.Text = "Comments"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmAddChildItem_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            FillAllCombos()
            Me.cmbUnit.Enabled = True
            Me.cmbType.Enabled = True
            Me.cmbCategory.Enabled = True
            Me.cmbSubCategory.Enabled = True
            Me.cmbOrigin.Enabled = True
            Me.txtReferenceNumber.Text = "Reference Number"
            Me.txtQty.Text = "Qty"
            Me.txtComments.Text = "Comments"
            Me.txtRequirementDescription.Text = ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmAddChildItem_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillAllCombos()
            Me.cmbUnit.Enabled = True
            Me.cmbType.Enabled = True
            Me.cmbCategory.Enabled = True
            Me.cmbSubCategory.Enabled = True
            Me.cmbOrigin.Enabled = True
            ''
            Me.cmbUnit.Value = UnitId
            Me.cmbType.Value = TypeId
            Me.cmbCategory.Value = CategoryId
            Me.cmbSubCategory.Value = SubCategoryId
            Me.cmbOrigin.Value = OriginId
            ''
            Me.txtReferenceNumber.Text = "Reference Number"
            Me.txtQty.Text = "Qty"
            Me.txtComments.Text = "Comments"
            Me.txtRequirementDescription.Text = ""
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_ValueChanged(sender As Object, e As EventArgs) Handles cmbItem.ValueChanged
        Try
            If Not Me.cmbItem.Text = "Select an item" Then
                Me.cmbUnit.Value = Val(Me.cmbItem.ActiveRow.Cells("ArticleUnitId").Value.ToString)
                Me.cmbType.Value = Val(Me.cmbItem.ActiveRow.Cells("ArticleTypeId").Value.ToString)
                Me.cmbCategory.Value = Val(Me.cmbItem.ActiveRow.Cells("ArticleCategoryId").Value.ToString)
                Me.cmbSubCategory.Value = Val(Me.cmbItem.ActiveRow.Cells("ArticleLPOId").Value.ToString)
                Me.cmbOrigin.Value = Val(Me.cmbItem.ActiveRow.Cells("ArticleGenderId").Value.ToString)
                Me.cmbUnit.Enabled = False
                Me.cmbType.Enabled = False
                Me.cmbCategory.Enabled = False
                Me.cmbSubCategory.Enabled = False
                'Me.cmbOrigin.Enabled = False
            Else
                'Me.cmbUnit.Rows(0).Activate()
                'Me.cmbType.Rows(0).Activate()
                'Me.cmbCategory.Rows(0).Activate()
                'Me.cmbSubCategory.Rows(0).Activate()
                'Me.cmbOrigin.Rows(0).Activate()
                Me.cmbUnit.Enabled = True
                Me.cmbType.Enabled = True
                Me.cmbCategory.Enabled = True
                Me.cmbSubCategory.Enabled = True
                Me.cmbOrigin.Enabled = True

                'FillCombos("Item")
                'FillCombos("TypeAgainstItem")
                'FillCombos("OriginAgainstItem")
                'FillCombos("UnitAgainstItem")
                'FillCombos("CategoryAgainstItem")
                'FillCombos("SubCategoryAgainstItem")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddAlternate_Click(sender As Object, e As EventArgs) Handles btnAddAlternate.Click
        Try
            If frmVendorQuotation.grdItems.DataSource Is Nothing Then
                frmVendorQuotation.DisplayDetailForHistory(-1)
            End If
            frmVendorQuotation.AddAlternateToGrid(Me.txtRequirementDescription.Text, Me.txtQty.Text, Me.txtReferenceNumber.Text, IIf(Me.cmbItem.Value > 0, Me.cmbItem.Value, 0), IIf(Me.cmbItem.Value > 0, Me.cmbItem.ActiveRow.Cells("Code").Value.ToString, ""), IIf(Me.cmbItem.Value > 0, Me.cmbItem.ActiveRow.Cells("Item").Value.ToString, ""), IIf(Me.cmbUnit.Value > 0, Me.cmbUnit.Value, 0), IIf(Me.cmbUnit.Value > 0, Me.cmbUnit.Text, ""), IIf(Me.cmbType.Value > 0, Me.cmbType.Value, 0), IIf(Me.cmbType.Value > 0, Me.cmbType.Text, ""), IIf(Me.cmbCategory.Value > 0, Me.cmbCategory.Value, 0), IIf(Me.cmbCategory.Value > 0, Me.cmbCategory.Text, ""), IIf(Me.cmbSubCategory.Value > 0, Me.cmbSubCategory.Value, 0), IIf(Me.cmbSubCategory.Value > 0, Me.cmbSubCategory.Text, ""), IIf(Me.cmbOrigin.Value > 0, Me.cmbOrigin.Value, 0), IIf(Me.cmbOrigin.Value > 0, Me.cmbOrigin.Text, ""), Me.txtComments.Text)
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class

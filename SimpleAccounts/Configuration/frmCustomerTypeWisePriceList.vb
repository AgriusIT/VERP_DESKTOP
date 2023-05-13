''TASK TFS1868 Muhammad Ameen done on 14-12-2017. Customer Type Wise Price List
Imports SBDal
Imports SBModel

Public Class frmCustomerTypeWisePriceList

    Private Sub FillCombo(ByVal Condition As String)
        Dim Str As String = ""
        Try
            If Condition = "CustomerType" Then
                FillDropDown(Me.cmbCustomerType, "SELECT Typeid, Name FROM dbo.TblDefCustomerType ")
            ElseIf Condition = "Items" Then
                Str = " SELECT TOP 100 PERCENT dbo.ArticleDefTable.ArticleId, " & _
                    " dbo.ArticleDefTable.ArticleDescription + ' ~ ' + dbo.ArticleDefTable.ArticleCode + '------' + + y.ArticleColorName + '-' + b.ArticleSizeName + '-' + a.ArticleUnitName AS ArticleDescription, " & _
                       " y.ArticleColorName, b.ArticleSizeName, a.ArticleUnitName " & _
                         " FROM dbo.ArticleDefTable INNER JOIN " & _
                          "   dbo.ArticleUnitDefTable a ON dbo.ArticleDefTable.ArticleUnitId = a.ArticleUnitId INNER JOIN " & _
                            " dbo.ArticleSizeDefTable b ON dbo.ArticleDefTable.SizeRangeId = b.ArticleSizeId INNER JOIN " & _
                              " dbo.ArticleColorDefTable y ON y.ArticleColorId = dbo.ArticleDefTable.ArticleColorId WHERE dbo.ArticleDefTable.ArticleDescription LIKE '%" & Me.txtsearch.Text & "%' Or dbo.ArticleDefTable.ArticleCode LIKE '%" & Me.txtsearch.Text & "%' " & _
                                " ORDER BY dbo.ArticleDefTable.ArticleDescription"
                'End Task:2797
                Me.lstItems.ListItem.ValueMember = "ArticleID"
                Me.lstItems.ListItem.DisplayMember = "ArticleDescription"
                Me.lstItems.ListItem.DataSource = UtilityDAL.GetDataTable(Str)
            ElseIf Condition = "Customers" Then
                Str = "SELECT CustomerId, CustomerName FROM tblCustomer Where CustomerTypes = " & Me.cmbCustomerType.SelectedValue & " AND CustomerTypes > 0 "
                Me.UiCustomers.ListItem.ValueMember = "CustomerId"
                Me.UiCustomers.ListItem.DisplayMember = "CustomerName"
                Me.UiCustomers.ListItem.DataSource = UtilityDAL.GetDataTable(Str)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbCustomerType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCustomerType.SelectedIndexChanged
        Try
            If Not cmbCustomerType.SelectedIndex = -1 Then
                FillCombo("Customers")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtsearch_TextChanged(sender As Object, e As EventArgs) Handles txtsearch.TextChanged
        Try
            If Me.txtsearch.Text.Length > 0 Then
                FillCombo("Items")
            Else
                lstItems.ListItem.DataSource = Nothing
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetData()
        Dim Str As String = ""
        Try
            Str = " SELECT ArticleId, ArticleCode, ArticleDescription, ArticleGenderName AS Gender, ArticleTypeName As [Type], Article.ArticleCompanyName AS Category, Article.ArticleColorName AS Color, Article.ArticleSizeName AS Size, Case When (Article.SalePrice-Discount.Discount) > 0 Then (Article.SalePrice-Discount.Discount) Else 0 END AS SalePrice, Case When (Article.Cost_Price-Discount.Discount) > 0 Then (Article.Cost_Price-Discount.Discount) Else 0  END AS CostPrice , Case When (Article.PurchasePrice-Discount.Discount) > 0 Then (Article.PurchasePrice-Discount.Discount) Else 0 END AS PurchasePrice  FROM ArticleDefView AS Article " _
                & " LEFT OUTER JOIN (SELECT ArticleDefID, ISNULL(Discount, 0) AS Discount FROM tbldefcustomerbasediscounts WHERE TypeID = " & cmbCustomerType.SelectedValue & ") AS Discount ON Article.ArticleId = Discount.ArticleDefID " & IIf(Me.lstItems.SelectedIDs.Length > 0, "Where Article.ArticleId IN(" & Me.lstItems.SelectedIDs & ")", "") & " "
            Dim dt As DataTable = GetDataTable(Str)
            GridEX1.DataSource = dt
            GridEX1.RetrieveStructure()
            GridEX1.RootTable.Columns("ArticleId").Visible = False
            GridEX1.RootTable.Columns("PurchasePrice").Visible = False
            GridEX1.RootTable.Columns("CostPrice").Visible = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmCustomerTypeWisePriceList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            GetSecurityRights()
            FillCombo("CustomerType")
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

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Customer Type Wise Price List " & Chr(10) & ""
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

End Class
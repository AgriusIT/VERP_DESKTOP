'05-April-2018 TFS2940 : Ayesha Rehman: Add new to Add ArticleAlias Agianst Different Combinations
'30-Oct-2018 TFS4810 : Ayesha Rehman: Color and Size combinition wise Vendor's Article Alias name.
Imports SBModel
Imports SBDal
Public Class frmArticleAliasSizeAndColorWise
    Implements IGeneral
    Dim Id As Integer = 0I
    Public Shared MasterId As Integer = 0I
    Dim ArticleAlias As ArticleAliasTableBE
    Dim ArticleAliasList As List(Of ArticleAliasTableBE)
    Public objDAL As ArticleAliasTableDAL = New ArticleAliasTableDAL()

   
    Private Sub frmArticleAliasSizeAndColorWise_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            FillCombos()
            ''Start TFS4810
            If chkAdvanceSearch.Checked = True Then
                Me.grd.Visible = True
                Me.pnlgrd.Visible = True
                Me.grdOld.Visible = False
                Me.pnlOldgrd.Visible = False
                GetArticleAliasTable()
            Else
                Me.grd.Visible = False
                Me.pnlgrd.Visible = False
                Me.grdOld.Visible = True
                Me.pnlOldgrd.Visible = True
                GetArticleAliasTableOld(cmbVendor.ActiveRow.Cells(0).Value)
            End If
            ''End TFS4810
            btnSave.FlatAppearance.BorderSize = 0
            btnSave.FlatAppearance.MouseOverBackColor = btnSave.BackColor
            btnCancel.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.MouseOverBackColor = btnCancel.BackColor
            If GetExistingInfoAboutVendor(Me.cmbVendor.Value) = False Then
                Me.grd.DataSource = Nothing
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    ''' <summary>
    ''' To Delete Data Which Already Exists
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New ArticleAliasTableDAL().Delete(ArticleAlias) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim strSQL = String.Empty
            strSQL = "SELECT dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_code as [Code]  " & _
                     "FROM dbo.vwCOADetail WHERE dbo.vwCOADetail.detail_title <> '' AND (dbo.vwCOADetail.account_type in('Customer','Vendor'))   "

            FillUltraDropDown(cmbVendor, strSQL)
            Me.cmbVendor.Rows(0).Activate()
            Me.cmbVendor.DisplayLayout.Bands(0).Columns("Id").Hidden = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try

            Dim dtArticleSize As New DataTable
            Dim ArticleSizeName As String = ""

            dtArticleSize = GetDataTable("Select Distinct ArticleSizeName from ArticleDefView where MasterId=" & MasterId & "")
            If dtArticleSize.Rows.Count > 0 Then
                For Each row As DataRow In dtArticleSize.Rows

                    If Not row.Item("ArticleSizeName").ToString = String.Empty Then

                        ArticleSizeName += "" & row.Item("ArticleSizeName").ToString & ","

                    End If

                Next
            End If
            If Not ArticleSizeName = String.Empty Then

                ArticleSizeName = ArticleSizeName.Trim.Substring(0, ArticleSizeName.Length - 1)
            End If

            ArticleAliasList = New List(Of ArticleAliasTableBE)
            Dim parts As String() = ArticleSizeName.Split(New Char() {","c})
            'Dim part As String
            'For Each part In parts
            '    MsgBox(part)
            'Next

            Dim i As Integer = 0
            Dim ArticleAlias As ArticleAliasTableBE
            If Me.grd.RowCount > 0 Then
                For Each r As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                    For i = 0 To parts.Length - 1
                        ArticleAlias = New ArticleAliasTableBE
                        ArticleAlias.SizeId = GetSizeId(parts(i))
                        ArticleAlias.ColorId = GetColorId(ArticleAlias.SizeId, r.Cells(1).Value)
                        ArticleAlias.ArticleId = GetArticleId(ArticleAlias.SizeId, ArticleAlias.ColorId)
                        ArticleAlias.VendorID = Me.cmbVendor.ActiveRow.Cells(0).Value
                        ArticleAlias.MasterId = MasterId
                        ArticleAlias.ArticleAliasName = r.Cells(parts(i)).Value.ToString
                        ArticleAliasList.Add(ArticleAlias)
                    Next
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''Start TFS4810
    Private Sub FillModelOld()
        Try
            ArticleAliasList = New List(Of ArticleAliasTableBE)

            If Me.grdOld.RowCount > 0 Then
                For Each r As Janus.Windows.GridEX.GridEXRow In grdOld.GetRows
                    ArticleAlias = New ArticleAliasTableBE
                    ArticleAlias.ArticleAliasID = Val(r.Cells("ArticleAliasID").Value)
                    ArticleAlias.ArticleId = Val(r.Cells("ArticleId").Value)
                    ArticleAlias.SizeId = Val(r.Cells("SizeID").Value)
                    ArticleAlias.ColorId = Val(r.Cells("ColorId").Value)
                    ArticleAlias.VendorID = Me.cmbVendor.ActiveRow.Cells(0).Value
                    ArticleAlias.MasterId = MasterId
                    ArticleAlias.ArticleAliasName = r.Cells("ArticleAlias").Value.ToString
                    ArticleAliasList.Add(ArticleAlias)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''End TFS4810
    Private Function GetSizeId(ByVal SizeName As String) As Integer
        Try
            Dim dt As DataTable
            dt = GetDataTable("Select Distinct SizeRangeId from ArticleDefView where MasterID = " & MasterId & " and ArticleSizeName ='" & SizeName & "'")
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows(0).Item("SizeRangeId").ToString)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function
    Private Function GetColorId(ByVal SizeId As Integer, ByVal ColorName As String) As Integer
        Try
            Dim dt As DataTable
            dt = GetDataTable("Select Distinct ArticleColorId from ArticleDefView where MasterID = " & MasterId & " and SizeRangeId = " & SizeId & " and ArticleColorName ='" & ColorName & "'")
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows(0).Item("ArticleColorId").ToString)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function
    Private Function GetArticleId(ByVal SizeId As Integer, ByVal ColorId As Integer) As Integer
        Try
            Dim dt As DataTable
            dt = GetDataTable("Select  ArticleId from ArticleDefView where MasterID = " & MasterId & " and SizeRangeId = " & SizeId & " and ArticleColorId = " & ColorId & "")
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows(0).Item("ArticleId").ToString)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' This Sub is made to get Record According to Vendor Id
    ''' </summary>
    ''' <param name="VendorId"></param>
    ''' <remarks>Ayesha Rehman : TFS2940 : 05-04-2018</remarks>
    Private Sub GetById(ByVal VendorId As Integer)
        Try
            Dim dtArticleSize As New DataTable
            Dim ArticleSizeName As String = ""
            Dim ArticleSizeNameU As String = ""
            Dim ArticleSizeNameD As String = ""
            Me.grd.DataSource = Nothing
            dtArticleSize = GetDataTable("Select Distinct ArticleSizeName from ArticleDefView where MasterId=" & MasterId & "")


            If dtArticleSize.Rows.Count > 0 Then

                For Each row As DataRow In dtArticleSize.Rows

                    If Not row.Item("ArticleSizeName").ToString = String.Empty Then
                        ArticleSizeName += "" & row.Item("ArticleSizeName").ToString & ","
                        ArticleSizeNameD += "[" & row.Item("ArticleSizeName").ToString & "],"

                        ArticleSizeNameU += "ISNULL([" & row.Item("ArticleSizeName").ToString & "],0) As [" & row.Item("ArticleSizeName").ToString & "],"

                    End If

                Next

            End If
            If Not ArticleSizeNameD = String.Empty And Not ArticleSizeNameU = String.Empty Then

                ArticleSizeNameU = ArticleSizeNameU.Trim.Substring(0, ArticleSizeNameU.Length - 1)
                ArticleSizeNameD = ArticleSizeNameD.Trim.Substring(0, ArticleSizeNameD.Length - 1)
                ArticleSizeName = ArticleSizeName.Trim.Substring(0, ArticleSizeName.Length - 1)
                'Dim strsql As String = "SELECT MasterID, Color , " & ArticleSizeNameU & " FROM " _
                '                       & "(SELECT     ArticleDefView.MasterID, ISNULL(ArticleAliasTable.ArticleAliasName, '') AS ArticleAlias, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Color " _
                '                       & " FROM ArticleDefView LEFT OUTER JOIN ArticleAliasTable ON ArticleDefView.ArticleId = ArticleAliasTable.ArticleId " _
                '                       & " WHERE(ArticleDefView.MasterID = " & MasterId & " " _
                '                       & " " & IIf(GetExistingInfoAboutVendor(VendorId) = True, " And ArticleAliasTable.VendorID = " & VendorId & "", "") & " )) AS Pro " _
                '                       & "PIVOT (max(ArticleAlias) for Size IN(" & ArticleSizeNameD & ")) As PVT "
                ''Query Edit Agianst TFS4810
                Dim strsql As String = "SELECT MasterID, Color , " & ArticleSizeNameU & " FROM " _
                                       & "(SELECT     ArticleDefView.MasterID, ISNULL(a.ArticleAlias, '') AS ArticleAlias, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Color " _
                                       & " FROM ArticleDefView LEFT OUTER JOIN (Select ArticleAliasTable.ArticleId ,ArticleAliasTable.ArticleAliasID ,IsNull(ArticleAliasTable.ArticleAliasName, '') AS ArticleAlias " _
                                       & " from ArticleAliasTable Inner join ArticleDefView On ArticleDefView.ArticleId = ArticleAliasTable.ArticleId Where ArticleDefView.MasterID= " & MasterId & " And  ArticleAliasTable.VendorID = " & VendorId & " ) a on a.ArticleId = ArticleDefView.ArticleId  " _
                                       & " WHERE(ArticleDefView.MasterID = " & MasterId & ")) AS Pro " _
                                       & " PIVOT (max(ArticleAlias) for Size IN(" & ArticleSizeNameD & ")) As PVT "
                Dim dt As DataTable = GetDataTable(strsql)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()

            Else
                Me.grd.DataSource = Nothing
            End If

            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns(1).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Dim parts As String() = ArticleSizeName.Split(New Char() {","c})
            Dim part As String
            For Each part In parts
                Me.grd.RootTable.Columns(part).EditType = Janus.Windows.GridEX.EditType.TextBox

            Next

                If GetExistingInfoAboutVendor(VendorId) = False Then

                If Me.grd.RowCount > 0 Then
                    For Each r As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                        For i As Integer = 0 To parts.Length - 1
                            r.BeginEdit()
                            r.Cells(parts(i)).Value = String.Empty
                            r.EndEdit()
                        Next
                    Next
                End If
                End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' This Sub is Made to validate if there any data available Against this Master Id
    ''' </summary>
    ''' <param name="VendorId"></param>
    ''' <remarks>Ayesha Rehman : TFS2940 : 05-04-2018</remarks>
    Private Function GetExistingInfoAboutVendor(ByVal VendorId As Integer) As Boolean
        Try
        Dim dt As DataTable
        dt = GetDataTable("Select * from ArticleAliasTable where VendorID = " & VendorId & " And MasterId = " & MasterId & " ")
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub
    ''' <summary>
    ''' This Function is Made To Validate If A vendor is selected or not
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>Ayesha Rehman : TFS2940 : 06-04-2018</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If cmbVendor.ActiveRow.Cells(0).Value = 0 Then
                ShowValidationMessage("Please select Vendor")
                Me.cmbVendor.Focus()
                Return False
            End If
            ''Start TFS4810
            If chkAdvanceSearch.Checked = True Then
                FillModel()
            Else
                FillModelOld()
            End If
            ''End TFS4810
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' This Sub is Made To Reset Controls
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>Ayesha Rehman : TFS2940 : 06-04-2018</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            cmbVendor.Rows(0).Activate()
            Me.chkAdvanceSearch.Checked = False ''TFS4810
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Save Data
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>Ayesha Rehman : TFS2940 : 06-04-2018</remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New ArticleAliasTableDAL().Add(ArticleAliasList) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function
    ''' <summary>
    ''' This Fuction is Made to check If Data Already Exist Against This Master And Vendor id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Ayesha Rehman : TFS2940 : 06-04-2018</remarks>
    Public Function GetPerviousInfo() As Boolean
        Try
            Dim dt As DataTable
            dt = GetDataTable("Select * from ArticleAliasTable Where MasterId = " & MasterId & " And VendorID = " & cmbVendor.ActiveRow.Cells(0).Value & " ")
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    ''' <summary>
    ''' To Update Existing Data
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>Ayesha Rehman : TFS2940 : 06-04-2018</remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New ArticleAliasTableDAL().Update(ArticleAliasList) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' This Sub is made to get the Article Alias Against Master Id of the Article
    ''' </summary>
    ''' <remarks>Ayesha Rehman : TFS2940 : 04-04-2018</remarks>
    Private Sub GetArticleAliasTable()
        Try
            Dim dtArticleSize As New DataTable
            Dim ArticleSizeName As String = ""
            Dim ArticleSizeNameU As String = ""
            Dim ArticleSizeNameD As String = ""
            dtArticleSize = GetDataTable("Select Distinct ArticleSizeName from ArticleDefView where MasterId=" & MasterId & "")


            If dtArticleSize.Rows.Count > 0 Then

                For Each row As DataRow In dtArticleSize.Rows

                    If Not row.Item("ArticleSizeName").ToString = String.Empty Then

                        ArticleSizeName += "" & row.Item("ArticleSizeName").ToString & ","

                        ArticleSizeNameD += "[" & row.Item("ArticleSizeName").ToString & "],"

                        ArticleSizeNameU += "ISNULL([" & row.Item("ArticleSizeName").ToString & "],0) As [" & row.Item("ArticleSizeName").ToString & "],"

                    End If

                Next

            End If
            If Not ArticleSizeNameD = String.Empty And Not ArticleSizeNameU = String.Empty Then

                ArticleSizeNameU = ArticleSizeNameU.Trim.Substring(0, ArticleSizeNameU.Length - 1)
                ArticleSizeNameD = ArticleSizeNameD.Trim.Substring(0, ArticleSizeNameD.Length - 1)
                ArticleSizeName = ArticleSizeName.Trim.Substring(0, ArticleSizeName.Length - 1)
                Dim strsql As String = "SELECT MasterID, Color , " & ArticleSizeNameU & " FROM " _
                                       & "(SELECT     ArticleDefView.MasterID, ISNULL(ArticleAliasTable.ArticleAliasName, '') AS ArticleAlias, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Color " _
                                       & "FROM ArticleDefView LEFT OUTER JOIN ArticleAliasTable ON ArticleDefView.ArticleId = ArticleAliasTable.ArticleId " _
                                       & " WHERE(ArticleDefView.MasterID = " & MasterId & ") ) AS Pro " _
                                       & "PIVOT (max(ArticleAlias) for Size IN(" & ArticleSizeNameD & ")) As PVT"


                Dim dt As DataTable = GetDataTable(strsql)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()

            Else
                Me.grd.DataSource = Nothing
            End If


            Me.grd.RootTable.Columns(0).Visible = False
            Me.grd.RootTable.Columns(1).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Dim parts As String() = ArticleSizeName.Split(New Char() {","c})
            Dim part As String

            For Each part In parts
                Me.grd.RootTable.Columns(part).EditType = Janus.Windows.GridEX.EditType.TextBox
                'MsgBox(part)
            Next
            Me.grd.DataSource = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' THis Sub Is Made to Get ArticleAlias in Row Wise
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TFS4810 : Ayesah Rehman : 30-10-2018</remarks>
    Private Sub GetArticleAliasTableOld(ByVal VendorId As Integer)
        Try
            Dim strSQL As String

            'strSQL = " SELECT     ArticleDefView.MasterID,ArticleDefView.ArticleId,ArticleAliasTable.ArticleAliasID,ArticleDefView.SizeRangeId  AS SizeID,ArticleDefView.ArticleColorId  AS ColorId ,ArticleDefView.ArticleDescription,ArticleDefView.ArticleCode,ArticleDefView.ArticleSizeName AS Size,ArticleDefView.ArticleColorName AS Color ,ISNULL(ArticleAliasTable.ArticleAliasName, '') AS ArticleAlias " _
            '         & " FROM ArticleDefView LEFT OUTER JOIN ArticleAliasTable ON ArticleDefView.ArticleId = ArticleAliasTable.ArticleId  " _
            '         & " Where ArticleDefView.MasterId = " & MasterId & " " & IIf(VendorId > 0, " And ArticleAliasTable.VendorID = " & VendorId & "", "")
            strSQL = " SELECT ArticleDefTable.ArticleId,ArticleDefTable.MasterId,IsNull(a.ArticleAliasID,0) As ArticleAliasID,ArticleDefTable.ArticleCode,ArticleSizeDefTable.ArticleSizeId AS SizeID," & _
                     " ArticleColorDefTable.ArticleColorId AS ColorId,ArticleColorDefTable.ArticleColorName As Color,ArticleSizeDefTable.ArticleSizeName As Size ,IsNull(a.ArticleAlias,'') As ArticleAlias " & _
                     " FROM ArticleDefTable INNER JOIN ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId INNER JOIN " & _
                     " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId Left Outer JOIN  (Select ArticleAliasTable.ArticleId ,ArticleAliasTable.ArticleAliasID ,IsNull(ArticleAliasTable.ArticleAliasName, '') AS ArticleAlias " & _
                     " from ArticleAliasTable Inner join ArticleDefTable On ArticleDefTable.ArticleId = ArticleAliasTable.ArticleId Where ArticleDefTable.MasterID= " & MasterId & " And  ArticleAliasTable.VendorID = " & VendorId & " ) a on a.ArticleId = ArticleDefTable.ArticleId  Where ArticleDefTable.MasterID = " & MasterId & " "
            Dim dt As DataTable = GetDataTable(strSQL)
            Me.grdOld.DataSource = Nothing
            Me.grdOld.DataSource = dt
            Me.grdOld.RetrieveStructure()
            For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grdOld.RootTable.Columns
                c.EditType = Janus.Windows.GridEX.EditType.NoEdit
                c.FilterEditType = Janus.Windows.GridEX.EditType.TextBox
            Next

            Me.grdOld.RootTable.Columns(0).Visible = False
            Me.grdOld.RootTable.Columns(1).Visible = False
            Me.grdOld.RootTable.Columns("SizeID").Visible = False
            Me.grdOld.RootTable.Columns("ColorId").Visible = False
            Me.grdOld.RootTable.Columns("ArticleAliasID").Visible = False
            Me.grdOld.RootTable.Columns("ArticleAlias").EditType = Janus.Windows.GridEX.EditType.TextBox


        Catch ex As Exception

        End Try
    End Sub
    Private Sub cmbVendor_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbVendor.KeyDown
        '' Ayesha Rehman :Added for Selection of Customer or Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.AccountType = "'Customer','Vendor' "
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbVendor.Value = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVendor_ValueChanged(sender As Object, e As EventArgs) Handles cmbVendor.ValueChanged
        Try
            ''Start TFS4810
            If chkAdvanceSearch.Checked = True Then
                Me.grd.Visible = True
                Me.grdOld.Visible = False
                Me.pnlgrd.Visible = True
                Me.pnlOldgrd.Visible = False
                If cmbVendor.ActiveRow.Cells(0).Value > 0 Then
                    GetById(cmbVendor.ActiveRow.Cells(0).Value)
                Else
                    GetArticleAliasTable()
                End If
            Else
                Me.grd.Visible = False
                Me.grdOld.Visible = True
                Me.pnlgrd.Visible = False
                Me.pnlOldgrd.Visible = True
                If cmbVendor.ActiveRow.Cells(0).Value > 0 Then
                    GetArticleAliasTableOld(cmbVendor.ActiveRow.Cells(0).Value)
                Else
                    GetArticleAliasTableOld(cmbVendor.ActiveRow.Cells(0).Value)
                End If
            End If
            ''End TFS4810
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If chkAdvanceSearch.Checked = True Then ''TFS4810
                    If GetPerviousInfo() = False Then
                        If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                        If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                        msg_Information(str_informSave)
                        ReSetControls()
                        GetArticleAliasTable()
                    Else
                        If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                        If Delete() = True Then
                            If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                        End If
                        msg_Information(str_informUpdate)
                        ReSetControls()
                        GetArticleAliasTable()

                    End If
                Else
                    If GetPerviousInfo() = False Then ''TFS4810
                        If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                        If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                        msg_Information(str_informSave)
                        ReSetControls()
                        GetArticleAliasTableOld(cmbVendor.ActiveRow.Cells(0).Value)
                    Else
                        If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                        If Delete() = True Then
                            If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                        End If
                        msg_Information(str_informUpdate)
                        ReSetControls()
                        GetArticleAliasTableOld(cmbVendor.ActiveRow.Cells(0).Value)
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
  
    ''Close the form on Cancel Click
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkAdvanceSearch_CheckedChanged(sender As Object, e As EventArgs) Handles chkAdvanceSearch.CheckedChanged
        Try
            If chkAdvanceSearch.Checked = True Then ''TFS4810
                Me.grd.Visible = True
                Me.pnlgrd.Visible = True
                Me.grdOld.Visible = False
                Me.pnlOldgrd.Visible = False
                If cmbVendor.ActiveRow.Cells(0).Value > 0 Then
                    GetById(cmbVendor.ActiveRow.Cells(0).Value)
                Else
                    GetArticleAliasTable()
                End If
            Else
                Me.grd.Visible = False
                Me.pnlgrd.Visible = False
                Me.grdOld.Visible = True
                Me.pnlOldgrd.Visible = True
                GetArticleAliasTableOld(cmbVendor.ActiveRow.Cells(0).Value)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
'Imports DAL
Public Class frmCustomerDiscounts
    Implements IGeneral

    Enum EnumGrid
        ID
        ArticleId
        Category
        Code
        Description
        Size
        Color
        Count
    End Enum
    Enum EnmGrid
        ArticleId
        Category
        Code
        Description
        Size
        Color
        Count
    End Enum
    Private Sub frmCustomerDiscounts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.FillGrid()
            Me.GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillGrid()
        Try
            Dim strsql As String = String.Empty

            strsql = " SELECT ArticleDefTable.ArticleId As Id, ArticleDefTable.ArticleId, ArticleGenderDefTable.ArticleGenderName as Category, ArticleDefTable.ArticleCode, ArticleDefTable.ArticleDescription AS Item, ArticleSizeDefTable.ArticleSizeName AS Size, " _
                    & " ArticleColorDefTable.ArticleColorName AS Color " _
                    & " FROM ArticleDefTable INNER JOIN " _
                    & " ArticleSizeDefTable ON ArticleDefTable.SizeRangeId = ArticleSizeDefTable.ArticleSizeId INNER JOIN " _
                    & " ArticleColorDefTable ON ArticleDefTable.ArticleColorId = ArticleColorDefTable.ArticleColorId INNER JOIN " _
                    & " ArticleGroupDefTable ON ArticleDefTable.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId LEFT OUTER JOIN  " _
                    & " ArticleGenderDefTable ON ArticleGenderDefTable.ArticleGenderId = ArticleDefTable.ArticleGenderId " _
                    & " Where ArticleGroupDefTable.SalesItem=1 And ArticleDefTable.Active=1 order by  ArticleDefTable.SortOrder Asc "

            Dim dtMain As DataTable = GetDataTable(strsql)

            strsql = "select typeid, name from tbldefcustomertype order by sorting , name"
            Dim dt As DataTable = GetDataTable(strsql)

            For Each r As DataRow In dt.Rows
                If Not dt.Columns.Contains(r.Item(1)) Then
                    dtMain.Columns.Add(r(0), GetType(System.Int32), r(0))
                    dtMain.Columns.Add(r(1), GetType(System.String))
                End If
            Next
            Me.grdDiscounts.DataSource = dtMain
            Me.grdDiscounts.RetrieveStructure()
            Me.grdDiscounts.RootTable.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True
            Dim grpCategory As New Janus.Windows.GridEX.GridEXGroup(Me.grdDiscounts.RootTable.Columns("Category"))
            Me.grdDiscounts.RootTable.Groups.Add(grpCategory)
            Me.ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdDiscounts.AutomaticSort = False
            Me.grdDiscounts.RootTable.Columns(EnumGrid.ID).Visible = False
            Me.grdDiscounts.RootTable.Columns(EnumGrid.ArticleId).Visible = False
            For c As Integer = EnumGrid.Count To Me.grdDiscounts.RootTable.Columns.Count - 2 Step 2
                Me.grdDiscounts.RootTable.Columns(c).Visible = False
                Me.grdDiscounts.RootTable.Columns(c + 1).AllowSort = False
            Next
            Me.grdDiscounts.RootTable.Columns(EnumGrid.Code).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdDiscounts.RootTable.Columns(EnumGrid.Description).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdDiscounts.RootTable.Columns(EnumGrid.Size).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdDiscounts.RootTable.Columns(EnumGrid.Color).EditType = Janus.Windows.GridEX.EditType.NoEdit

            Me.grdDiscounts.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
            Me.grdDiscounts.RecordNavigator = True

            Me.grdDiscounts.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
            Me.grdDiscounts.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
            Me.grdDiscounts.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
            Me.grdDiscounts.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains
            Me.grdDiscounts.FrozenColumns = EnmGrid.Count - 1
            Me.grdDiscounts.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim strsql As String = "Select * from tbldefcustomerbasediscounts"
            Dim dt As DataTable = GetDataTable(strsql)
            dt.Columns.RemoveAt(0)
            dt.AcceptChanges()
            Dim dtGrid As DataTable = CType(Me.grdDiscounts.DataSource, DataTable)
            Dim dr() As DataRow
            For Each r As DataRow In dtGrid.Rows
                dr = dt.Select("articledefid = " & r(EnumGrid.ID))
                If Not dr Is Nothing Then
                    If dr.Length > 0 Then
                        For Each drFound As DataRow In dr
                            r.BeginEdit()
                            r(dtGrid.Columns.IndexOf(drFound(1)) + 1) = drFound(2)
                            r.EndEdit()
                        Next
                    End If
                End If
            Next
            dtGrid.Columns.RemoveAt(0)
            dtGrid.AcceptChanges()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Return True
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save


        Dim dtGrid As DataTable = CType(Me.grdDiscounts.DataSource, DataTable).GetChanges(DataRowState.Modified)
        If dtGrid Is Nothing Then
            ShowErrorMessage("No changes made")
            Exit Function
        End If


        Dim con As New OleDb.OleDbConnection(ModConnection.Con.ConnectionString)
        If Not con.State = 1 Then con.Open()
        Dim trans As OleDb.OleDbTransaction = con.BeginTransaction

        Try

            If Not Me.IsValidate Then Exit Function

            'If MsgBox("Do you want to save ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, str_MessageHeader) = MsgBoxResult.No Then
            If Not msg_Confirm(str_ConfirmSave) = True Then
                Exit Function
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim strSQL As String = String.Empty
            'strSQL = "delete from tbldefcustomerbasediscounts"
            Dim cmd As New OleDb.OleDbCommand(strSQL, con, trans)
            'cmd.ExecuteNonQuery()

            Dim dblDiscount As Double = 0D

            'Me.grdDiscounts.UpdateData()

            'For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdDiscounts.GetRows
            '    For c As Int32 = EnumGrid.Count To Me.grdDiscounts.RootTable.Columns.Count - 2 Step 2
            '        Double.TryParse(r.Cells(c + 1).Value.ToString(), dblDiscount)

            '        strSQL = "delete from tbldefcustomerbasediscounts where articledefid = " & r.Cells(EnumGrid.ID).Value _
            '        & " and typeid = " & r.Cells(c).Value
            '        cmd.CommandText = strSQL
            '        cmd.ExecuteNonQuery()

            '        strSQL = "insert into tbldefcustomerbasediscounts (articledefid, typeid , discount) " _
            '        & " values(" & r.Cells(EnumGrid.ID).Value & "," & r.Cells(c).Value & "," & dblDiscount & " )"
            '        cmd.CommandText = strSQL
            '        cmd.ExecuteNonQuery()
            '    Next
            'Next

            For Each r As DataRow In dtGrid.Rows
                For c As Int32 = EnmGrid.Count To Me.grdDiscounts.RootTable.Columns.Count - 2 Step 2
                    'cmd.CommandText = ""
                    'strSQL = "delete from tbldefcustomerbasediscounts where articledefid = " & Val(r(EnumGrid.ID)) _
                    '& " and typeid = " & r(c)
                    'cmd.CommandText = strSQL
                    'cmd.ExecuteNonQuery()

                    'Double.TryParse(r(c + 1).ToString(), dblDiscount)
                    'cmd.CommandText = ""
                    'strSQL = "insert into tbldefcustomerbasediscounts (articledefid, typeid , discount) " _
                    '& " values(" & r(EnumGrid.ID) & "," & r(c) & "," & dblDiscount & " )"
                    'cmd.CommandText = strSQL
                    'cmd.ExecuteNonQuery()

                    cmd.CommandText = ""
                    strSQL = "delete from tbldefcustomerbasediscounts where articledefid = " & Val(r(EnmGrid.ArticleId)) _
                    & " and typeid = " & r(c)
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()

                    Double.TryParse(r(c + 1).ToString(), dblDiscount)
                    cmd.CommandText = ""
                    strSQL = "insert into tbldefcustomerbasediscounts (articledefid, typeid , discount) " _
                    & " values(" & r(EnmGrid.ArticleId) & "," & r(c) & "," & dblDiscount & " )"
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()

                Next
            Next
            trans.Commit()
            Me.GetAllRecords()
            ShowInformationMessage("Discounts have been saved successfully.")
        Catch ex As SqlClient.SqlException
            trans.Rollback()
            Throw ex
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            con.Close()
            Me.Cursor = Cursors.Default
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub SaveToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            Me.Save()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdDiscounts_ColumnHeaderClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdDiscounts.ColumnHeaderClick
        'Try
        '    Dim discount As Double = Me.grdDiscounts.GetValue(Me.grdDiscounts.CurrentColumn)
        '    For iCounter As Int32 = Me.grdDiscounts.Row + 1 To Me.grdDiscounts.RecordCount - 1
        '        Me.grdDiscounts.Row = iCounter
        '        If Me.grdDiscounts.GetValue(Me.grdDiscounts.CurrentColumn) = 0 Then
        '            Me.grdDiscounts.SetValue(Me.grdDiscounts.CurrentColumn, discount)
        '        Else
        '            Exit For
        '        End If
        '    Next

        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub grdDiscounts_FormattingRow(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdDiscounts.FormattingRow

    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click

        Try

            Me.FillGrid()
            Me.GetAllRecords()
            GetSecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                'Me.btnDelete.Enabled = True
                'Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDefColor)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.BtnSave.Text = "Save" Or Me.BtnSave.Text = "&Save" Then
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.BtnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        'Me.BtnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.BtnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.BtnSave.Enabled = False
                'Me.BtnDelete.Enabled = False
                'Me.BtnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        '    Me.BtnDelete.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Print" Then
                        '    Me.BtnPrint.Enabled = True
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
End Class
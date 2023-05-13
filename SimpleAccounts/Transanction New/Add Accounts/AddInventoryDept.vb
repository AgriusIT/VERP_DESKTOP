Public Class AddInventoryDept

    Private Sub AddInventoryDept_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            btnAdd_Click(Nothing, Nothing)
        End If
       
    End Sub
    Private Sub AddInventoryDept_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillDropDown(Me.cmbDept, "Select main_sub_sub_id as main_sub_id, sub_sub_title as sub_title From tblCOAMainSubSub WHERE Account_Type='Inventory'")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Me.cmbDept.SelectedIndex = 0 Then Exit Sub
            If Me.txtInventoryDept.Text = String.Empty Then Exit Sub

            Dim str As String = String.Empty
            Dim ObjComm As New OleDb.OleDbCommand
            ObjComm.Connection = Con
            ObjComm.Transaction = trans
            ObjComm.CommandType = CommandType.Text



            ObjComm.CommandText = ""
            'ObjComm.CommandText = "INSERT INTO tblCOAMainSubSub(main_sub_id, sub_sub_code, sub_sub_title, account_type, DrBS_note_id, CrBS_note_id, PL_note_id) Values " _
            '& "  (" & Me.cmbDept.SelectedValue & ", '" & Me.txtMainCode.Text + "-" + Me.txtCode.Text & "', '" & Me.txtInventoryDept.Text & "', 'Inventory',0,0,0)Select @@Identity"
            ObjComm.CommandText = "INSERT INTO tblCOAMainSubSubDetail(main_sub_sub_id, detail_code, detail_title) Values " _
           & "  (" & Me.cmbDept.SelectedValue & ", '" & Me.txtMainCode.Text + "-" + Me.txtCode.Text & "', '" & Me.txtInventoryDept.Text.Replace("'", "''") & "')Select @@Identity"
            ObjComm.ExecuteNonQuery()



            Dim da As New OleDb.OleDbDataAdapter
            ObjComm.CommandText = ""
            ObjComm.CommandText = "Select Count(*),ArticleGroupName,GroupCode From ArticleGroupDefTable WHERE (GroupCode='" & Me.txtDeptCode.Text.Replace("'", "''") & "' Or ArticleGroupName='" & Me.txtInventoryDept.Text.Replace("'", "''") & "') GROUP BY ArticleGroupName,GroupCode "
            da.SelectCommand = ObjComm
            Dim dt As New DataTable
            da.Fill(dt)
            dt.AcceptChanges()
            If dt.HasErrors = False Then
                If dt.Rows.Count > 0 Then
                    If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                        If Me.txtInventoryDept.Text.ToUpper = dt.Rows(0).Item(1).ToString.ToUpper Then
                            Throw New Exception("[Department: " & dt.Rows(0).Item(1).ToString.Replace("'", "''") & "] is already exist")
                        ElseIf Me.txtDeptCode.Text.ToUpper = dt.Rows(0).Item(2).ToString.ToUpper Then
                            Throw New Exception("[Department Code: " & dt.Rows(0).Item(2).ToString.Replace("'", "''") & "] is already exist")
                        End If
                    End If
                End If
            End If



            ObjComm.CommandText = ""
            'ObjComm.CommandText = "INSERT INTO ArticleGroupDefTable(ArticleGroupName, Active, SubSubID, SalesItem, ServiceItem) Values('" & Me.txtInventoryDept.Text.Replace("'", "''") & "', 1,  ident_current('tblCOAMainSubSubDetail'), " & IIf(Me.chkSalesItem.Checked = True, 1, 0) & ", " & IIf(Me.chkServiceItem.Checked = True, 1, 0) & ")"
            ObjComm.CommandText = "INSERT INTO ArticleGroupDefTable(ArticleGroupName,GroupCode, Active, SubSubID, SalesItem, ServiceItem) Values('" & Me.txtInventoryDept.Text.Replace("'", "''") & "','" & Me.txtDeptCode.Text.Replace("'", "''") & "', 1,  ident_current('tblCOAMainSubSubDetail'), " & IIf(Me.chkSalesItem.Checked = True, 1, 0) & ", " & IIf(Me.chkServiceItem.Checked = True, 1, 0) & ")"
            ObjComm.ExecuteNonQuery()

            trans.Commit()
            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Me.Close()
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Function GetSubCode(ByVal Sub_Id As Integer) As String
        Try
            Dim str As String = String.Empty
            Dim Sub_Code As String = String.Empty
            'str = "Select sub_code From tblCOAMainSub WHERE main_sub_id='" & Sub_Id & "'"
            str = "Select sub_Sub_code From tblCOAMainSubSub WHERE main_sub_sub_id='" & Sub_Id & "'"

            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Sub_Code = dt.Rows(0).Item(0).ToString
                Return Sub_Code
            Else
                Return Sub_Code = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub cmbDept_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDept.SelectedIndexChanged
        Try
            Me.txtMainCode.Text = String.Empty
            Me.txtCode.Text = String.Empty
            Me.txtMainCode.Text = GetSubCode(Me.cmbDept.SelectedValue)
            If Me.cmbDept.SelectedIndex > 0 Then
                'Me.txtCode.Text = Microsoft.VisualBasic.Right(GetNextDocNo(Me.txtMainCode.Text, 7, "tblCOAMainSubSub", "sub_sub_code"), 3)
                Me.txtCode.Text = Microsoft.VisualBasic.Right(GetNextDocNo(Me.txtMainCode.Text, 5, "tblCOAMainSubSubDetail", "detail_code"), 5)
            Else
                Me.txtCode.Text = String.Empty
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkSalesItem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSalesItem.CheckedChanged
        Try
            'If Me.chkSalesItem.Checked = True Then
            '    Me.chkServiceItem.Checked = False
            'Else
            '    Me.chkServiceItem.Checked = True
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub chkServiceItem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkServiceItem.CheckedChanged
        Try
            'If Me.chkServiceItem.Checked = True Then
            '    Me.chkSalesItem.Checked = False
            'Else
            '    Me.chkSalesItem.Checked = True
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
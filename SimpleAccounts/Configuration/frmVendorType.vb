'17-May-2014 Task:2639 JUNAID vendor type in vendor information.

Public Class frmVendorType
    Dim IsLoadedForm As Boolean = False
    Enum EnmVendorTypes
        TypeId
        Name
        Description
        Sorting
        Active
    End Enum
    Sub VendorTypeEditRecord()
        Try
            If Not GrdVendorTypes.RowCount <> 0 Then Exit Sub
            Me.TxtId.Text = Me.GrdVendorTypes.GetRow.Cells(EnmVendorTypes.TypeId).Text
            Me.TxtName.Text = Me.GrdVendorTypes.GetRow.Cells(EnmVendorTypes.Name).Text
            Me.TxtDescription.Text = Me.GrdVendorTypes.GetRow.Cells(EnmVendorTypes.Description).Text
            Me.TxtSort.Text = Me.GrdVendorTypes.GetRow.Cells(EnmVendorTypes.Sorting).Text
            Me.UichkActive.Checked = Me.GrdVendorTypes.GetRow.Cells(EnmVendorTypes.Active).Text
            Me.UltraTabControl1.SelectedTab = UltraTabPageControl1.Tab
            Me.BtnDelete.Enabled = True
            BtnSave.Text = "&Update"
            BtnDelete.Visible = True
            GetSecurityRights()
            TxtName.Focus()

        Catch ex As Exception
            Throw ex
        End Try
       
    End Sub

    Sub VendorTypeRestControl()
        Try
            TxtName.Text = ""
            TxtId.Text = ""
            TxtDescription.Text = ""
            TxtSort.Text = "1"
            UichkActive.Checked = True
            BtnSave.Text = "&Save"
            'BtnDelete.Visible = False
            TxtName.Focus()
            GetSecurityRights()
            Me.BtnDelete.Enabled = False
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
        
    End Sub

    Sub VedndorTypeGetRecored()
        Try
            ' Me.GrdCustomerTypes.DataSource = cls.GetAll
            'CustomerTypeRestControl()
            ' Me.GrdCustomerTypes.RetrieveStructure()
            Me.GrdVendorTypes.DataSource = SBDal.UtilityDAL.GetDataTable("SELECT dbo.TblVendorType.*" _
            & "FROM   dbo.TblVendorType")
            VendorTypeRestControl()
            Me.GrdVendorTypes.RetrieveStructure()
            GrdVendorTypes.RootTable.Columns(EnmVendorTypes.TypeId).Visible = False
            ' GrdCustomerTypes.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
        Catch ex As Exception
            Throw ex
        End Try
       
    End Sub

    Sub VendorTypeDelete()
        Try
            'Me.GrdVendorTypes.DataSource = SBDal.UtilityDAL.GetDataTable("SELECT dbo.TblVendorType.*" _
            ' & "FROM   dbo.TblVendorType")

            If msg_Confirm(str_ConfirmDelete) = True Then
                Dim cm As New OleDb.OleDbCommand
                Dim cn As New OleDb.OleDbConnection(Con.ConnectionString)
                'If UltraTabControl1.SelectedTab=UltraTabPageControl1.
                cm.CommandText = "delete from TblVendorType where VendorTypeId=" & Me.GrdVendorTypes.CurrentRow.Cells(0).Value.ToString
                cm.Connection = cn
                cn.Open()
                cm.ExecuteNonQuery()
                cn.Close()
                msg_Information(str_informDelete)
                VendorTypeRestControl()
                VedndorTypeGetRecored()
            End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub VendorTypeSave()
        Try
            'If msg_Confirm(str_ConfirmSave) = True Then
            Dim cm As New OleDb.OleDbCommand
            Dim cn As New OleDb.OleDbConnection(Con.ConnectionString)
            cm.CommandText = "insert into tblVendorType (VendorType,Description,SortOrder,Active) values(N'" & TxtName.Text.Replace("'", "''") & "',N'" & TxtDescription.Text.Replace("'", "''") & "',N'" & TxtSort.Text.Replace("'", "''") & "',N'" & IIf(UichkActive.Checked = True, 1, 0) & "')"
            cm.Connection = cn
            cn.Open()
            cm.ExecuteNonQuery()
            cn.Close()
            'msg_Information(str_informSave)
            VedndorTypeGetRecored()
            VendorTypeRestControl()
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub VendorTypeUpdate()
        Try
            If msg_Confirm(str_ConfirmUpdate) = True Then
                Dim cm As New OleDb.OleDbCommand
                Dim cn As New OleDb.OleDbConnection(Con.ConnectionString)
                cm.CommandText = " update tblVendorType set [VendorType]= N'" & TxtName.Text.Replace("'", "''") & "', Description= N'" & TxtDescription.Text.Replace("'", "''") & "', SortOrder= N'" & TxtSort.Text.Replace("'", "''") & "', Active= N'" & IIf(UichkActive.Checked = True, 1, 0) & "' where VendorTypeId = " & TxtId.Text.Replace("'", "''")
                cm.Connection = cn
                cn.Open()
                cm.ExecuteNonQuery()
                cn.Close()
                'msg_Information(str_informUpdate)
                VendorTypeRestControl()
                VedndorTypeGetRecored()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Try
            If Me.TxtName.Text = String.Empty Then
                ShowErrorMessage("Please enter vendor type.")
                Me.TxtName.Focus()
                Exit Sub
            End If
            If BtnSave.Text = "Save" Or BtnSave.Text = "&Save" Then
                VendorTypeSave()
            Else
                VendorTypeUpdate()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            VendorTypeRestControl()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            VendorTypeEditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            VendorTypeDelete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmVendorType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                VendorTypeRestControl()
            End If
            If e.KeyCode = Keys.F4 Then
                VendorTypeSave()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmVendorType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            VedndorTypeGetRecored()
            VendorTypeRestControl()
            IsLoadedForm = True
            Get_All(frmModProperty.Tags)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub GrdVendorTypes_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            VendorTypeEditRecord()
            'Me.UltraTabControl1.SelectedTab = UltraTabPageControl1.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.BtnSave.Enabled = True
                Me.BtnDelete.Enabled = True
                Me.BtnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "True" Then
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
    Public Function Get_All(ByVal Id As String)
        Try
            Get_All = Nothing
            If IsLoadedForm = True Then
                Dim dt As DataTable = GetDataTable("Select * From tblVendorType WHERE VendorTypeId=N'" & Id & "'")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.TxtId.Text = dt.Rows(0).Item("VendorTypeId")
                        Me.TxtName.Text = dt.Rows(0).Item("VendorType").ToString
                        Me.TxtDescription.Text = dt.Rows(0).Item("Description").ToString
                        Me.TxtSort.Text = dt.Rows(0).Item("SortOrder").ToString
                        Me.UichkActive.Checked = dt.Rows(0).Item("Active")
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

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.BtnDelete.Visible = True
                BtnDelete.Enabled = True
            Else
                Me.BtnDelete.Visible = True
                If Not Me.BtnSave.Text = "&Update" Then BtnDelete.Enabled = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnHeader.Click

    End Sub
End Class
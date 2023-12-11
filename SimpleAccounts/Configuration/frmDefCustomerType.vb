''TASK TFS1778 Muhammad Ameen done on 27-12-2017. Addition of new field(SubCustomerType).
''TASK TFS2994 Ayesha Rehman done on 10-04-2018. Addition of new field(SubCustomerTypeId).Not working Properly on Update
Public Class frmDefCustomerType
    Dim IsLoadedForm As Boolean = False
    Dim TypeId As Integer = 0I
    Enum EnmCustomerTypes
        TypeId
        SubCustomerTypeId
        SubCustomerType
        Name
        Remarks
        Sorting
        Active
    End Enum
    Sub CustomerTypeEditRecord()
        Try
            If Not GrdCustomerTypes.RowCount <> 0 Then Exit Sub
            TypeId = Val(GrdCustomerTypes.GetRow.Cells(EnmCustomerTypes.TypeId).Value.ToString)
            Me.TxtId.Text = Val(TypeId)
            Me.TxtName.Text = GrdCustomerTypes.GetRow.Cells(EnmCustomerTypes.Name).Value.ToString
            Me.TxtRemarks.Text = GrdCustomerTypes.GetRow.Cells(EnmCustomerTypes.Remarks).Value.ToString
            Me.TxtSort.Text = GrdCustomerTypes.GetRow.Cells(EnmCustomerTypes.Sorting).Value
            Me.cmbSubCustomerType.SelectedValue = GrdCustomerTypes.GetRow.Cells(EnmCustomerTypes.SubCustomerTypeId).Value
            Me.UichkActive.Text = GrdCustomerTypes.GetRow.Cells(EnmCustomerTypes.Active).Value
            GetSecurityRights()
            BtnSave.Text = "&Update"
            TxtName.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub CustomerTypeRestControl()
        TxtName.Text = ""
        TxtId.Text = ""
        TxtRemarks.Text = ""
        TxtSort.Text = "1"
        UichkActive.Checked = True
        BtnSave.Text = "&Save"
        TxtName.Focus()
        'Below line is commented due to changing dropdown list database table.
        'FillDropDown(Me.cmbSubCustomerType, "Select Distinct SubCustomerType, SubCustomerType From TblDefCustomerType WHERE SubCustomerType <> '' Order By SubCustomerType ", False)
        FillDropDown(cmbSubCustomerType, "Select CategoryID, CategoryName From tblCustomerCategory where Active = 1")
        If Not Me.cmbSubCustomerType.SelectedIndex = -1 Then
            Me.cmbSubCustomerType.SelectedIndex = 0
        End If
        GetSecurityRights()
        TypeId = 0
    End Sub
    Sub CustomertypeGetRecored()
        ' Me.GrdCustomerTypes.DataSource = cls.GetAll
        'CustomerTypeRestControl()
        ' Me.GrdCustomerTypes.RetrieveStructure()
        'Me.GrdCustomerTypes.DataSource = SBDal.UtilityDAL.GetDataTable("SELECT     dbo.TblDefCustomerType.*" _
        Me.GrdCustomerTypes.DataSource = SBDal.UtilityDAL.GetDataTable("SELECT TypeId, TblDefCustomerType.SubCustomerTypeId , tblCustomerCategory.CategoryName AS Category, Name as Type, TblDefCustomerType.Remarks, sorting as SortOrder, TblDefCustomerType.Active " _
               & "FROM   dbo.TblDefCustomerType Left Outer Join tblCustomerCategory on TblDefCustomerType.SubCustomerTypeId = tblCustomerCategory.CategoryID order by sorting ")
        CustomerTypeRestControl()
        Me.GrdCustomerTypes.RetrieveStructure()
        GrdCustomerTypes.RootTable.Columns(EnmCustomerTypes.TypeId).Visible = False
        GrdCustomerTypes.RootTable.Columns(EnmCustomerTypes.SubCustomerTypeId).Visible = False
        ' GrdCustomerTypes.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
    End Sub
    Sub CustomerTypeDelete()
        Try
            If msg_Confirm(str_ConfirmDelete) = True Then
                Dim cm As New OleDb.OleDbCommand
                Dim cn As New OleDb.OleDbConnection(Con.ConnectionString)
                If TypeId > 0 Then
                    cm.CommandText = "delete from TblDefCustomerType where typeid=" & TypeId
                Else
                    cm.CommandText = "delete from TblDefCustomerType where typeid=" & Val(Me.GrdCustomerTypes.GetRow.Cells(EnmCustomerTypes.TypeId).Value.ToString)
                End If
                cm.Connection = cn
                cn.Open()
                cm.ExecuteNonQuery()
                cn.Close()
                msg_Information(str_informDelete)
                CustomerTypeRestControl()
                CustomertypeGetRecored()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub CustomerTypeSave()
        Try
            If msg_Confirm(str_ConfirmSave) = True Then
                Dim cm As New OleDb.OleDbCommand
                Dim cn As New OleDb.OleDbConnection(Con.ConnectionString)
                cm.CommandText = "insert into TblDefCustomerType (Name,Remarks,sorting,Active, SubCustomerType ,SubCustomerTypeId) values(N'" & TxtName.Text & "',N'" & TxtRemarks.Text & "',N'" & TxtSort.Text & "',N'" & IIf(UichkActive.Checked = True, 1, 0) & "', N'" & Me.cmbSubCustomerType.Text & "'," & Me.cmbSubCustomerType.SelectedValue & ")"
                cm.Connection = cn
                cn.Open()
                cm.ExecuteNonQuery()
                cn.Close()
                msg_Information(str_informSave)
                CustomertypeGetRecored()
                CustomerTypeRestControl()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CustomertypeUpdate()
        Try
            If msg_Confirm(str_ConfirmUpdate) = True Then
                Dim cm As New OleDb.OleDbCommand
                Dim cn As New OleDb.OleDbConnection(Con.ConnectionString)
                cm.CommandText = " update TblDefCustomerType set [name]= N'" & TxtName.Text & "', remarks= N'" & TxtRemarks.Text & "', sorting= N'" & TxtSort.Text & "', active= N'" & IIf(UichkActive.Checked = True, 1, 0) & "', SubCustomerType = N'" & Me.cmbSubCustomerType.Text & "', SubCustomerTypeId = " & Me.cmbSubCustomerType.SelectedValue & " where typeid = " & TxtId.Text
                cm.Connection = cn
                cn.Open()
                cm.ExecuteNonQuery()
                cn.Close()
                msg_Information(str_informUpdate)
                CustomerTypeRestControl()
                CustomertypeGetRecored()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If BtnSave.Text = "Save" Or BtnSave.Text = "&Save" Then
            CustomerTypeSave()
        Else
            CustomertypeUpdate()
        End If
    End Sub

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        Try
            CustomerTypeRestControl()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            CustomerTypeEditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            CustomerTypeDelete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmDefCustomerType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CustomertypeGetRecored()
        CustomerTypeRestControl()
        'FillDropDown(Me.cmbSubCustomerType, "Select Distinct SubCustomerType, SubCustomerType From TblDefCustomerType WHERE SubCustomerType <> ''", False)
        IsLoadedForm = True
        Get_All(frmModProperty.Tags)
    End Sub

    Private Sub GrdCustomerTypes_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        CustomerTypeEditRecord()
        Me.UltraTabControl1.SelectedTab = UltraTabPageControl1.Tab
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
                Dim dt As DataTable = GetDataTable("Select * From TblDefCustomerType WHERE Typeid=" & Val(Id) & "")
                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        Me.TxtId.Text = dt.Rows(0).Item("TypeId")
                        Me.TxtName.Text = dt.Rows(0).Item("Name").ToString
                        Me.TxtRemarks.Text = dt.Rows(0).Item("Remarks").ToString
                        Me.TxtSort.Text = dt.Rows(0).Item("Sorting").ToString
                        Me.UichkActive.Text = dt.Rows(0).Item("Active")
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

    Private Sub GrdCustomerTypes_DoubleClick_1(sender As Object, e As EventArgs) Handles GrdCustomerTypes.DoubleClick
        Try
            CustomerTypeEditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCustomerCategory_Click(sender As Object, e As EventArgs) Handles btnCustomerCategory.Click
        'This button will show the frmCustomerCategory Form
        Try
            frmCustomerCategory.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
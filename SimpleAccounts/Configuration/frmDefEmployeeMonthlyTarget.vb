Public Class frmDefEmployeeMonthlyTarget
    Implements IGeneral
    Dim intEmpTargetID As Integer = 0I
    Enum enmDetail
        CityID
        TerritoryID
        City
        Area
        Count
    End Enum
    Enum enmArticleType
        ArticleTypeID
        ArticleTypeName
    End Enum
    Enum enmTargetDetail
        EmpTargetDetailID
        EmpTargetID
        Target_YEAR
        Target_Month
        StartDate
        EndDate
        EmployeeID
        CityID
        TerritoryID
        ArticleTypeID
        Target_Weight
        Target_Value
    End Enum
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmDetailAccount)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False

                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand

        Try
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 300


            cmd.CommandText = ""
            cmd.CommandText = "Delete From SalesManTargetDetailTable WHERE EmpTargetID=" & Val(Me.grdSaved.GetRow.Cells("EmpTargetID").Value.ToString) & ""
            cmd.ExecuteNonQuery()


            cmd.CommandText = ""
            cmd.CommandText = "Delete From SalesManTargetMasterTable WHERE EmpTargetID=" & Val(Me.grdSaved.GetRow.Cells("EmpTargetID").Value.ToString) & ""
            cmd.ExecuteNonQuery()

            trans.Commit()

            Return True

        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            objCon.Close()
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try

            Dim str As String = String.Empty
            If Condition = String.Empty Then
                str = "Select Employee_ID,  Employee_Name,Father_Name,Employee_Code,Dept.EmployeeDeptName as Department, Desig.EmployeeDesignationName as Designation, tblDefEmployee.ShiftGroupId, EmpPicture, tblDefEmployee.Mobile From tblDefEmployee INNER JOIN EmployeeDeptDefTable Dept on Dept.EmployeeDeptId = tblDefEmployee.Dept_Id INNER JOIN EmployeeDesignationDefTable Desig on Desig.EmployeeDesignationId = tblDefEmployee.Desig_Id where tblDefEmployee.active = 1 ORDER BY Employee_Name Asc"
                FillUltraDropDown(Me.cmbEmployees, str)
                Me.cmbEmployees.Rows(0).Activate()
                If Me.cmbEmployees.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbEmployees.DisplayLayout.Bands(0).Columns("Employee_ID").Hidden = True
                    Me.cmbEmployees.DisplayLayout.Bands(0).Columns("ShiftGroupId").Hidden = True
                    Me.cmbEmployees.DisplayLayout.Bands(0).Columns("EmpPicture").Hidden = True
                End If
            ElseIf Condition = "Month" Then
                Me.cmbMonth.ValueMember = "Month"
                Me.cmbMonth.DisplayMember = "Month_Name"
                Me.cmbMonth.DataSource = GetMonths()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub EditRecord()
        Try
            If grdSaved.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            intEmpTargetID = Val(Me.grdSaved.GetRow.Cells("EmpTargetID").Value.ToString)
            Me.cmbEmployees.Value = Val(Me.grdSaved.GetRow.Cells("EmployeeID").Value.ToString)
            Me.txtYear.Text = Val(Me.grdSaved.GetRow.Cells("Target YEAR").Value.ToString)
            Me.cmbMonth.SelectedValue = Val(Me.grdSaved.GetRow.Cells("Target Month").Value.ToString)
            GetAllRecords("Detail")
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.cmbEmployees.Focus()
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Condition = "Master" Then
                Dim dt As New DataTable
                dt = GetDataTable("SELECT TOP 100 PERCENT Target.EmpTargetID, Target.EmployeeID, Emp.Employee_Code as [Emp Code], Emp.Employee_Name as [Employee], Target.Target_YEAR as [Target Year], Target.Target_Month as [Target Month], " _
               & "  Target.StartDate, Target.EndDate, Target.Total_Target_Weight as [Target Weight], Target.Total_Target_Value as [Target Value], Target.UserID, Target.UserName " _
               & " FROM dbo.SalesManTargetMasterTable AS Target INNER JOIN dbo.EmployeesView AS Emp ON Target.EmployeeID = Emp.Employee_ID ORDER BY Target.Target_YEAR, Target.Target_Month DESC")
                dt.AcceptChanges()
                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
                Me.grdSaved.RootTable.Columns("EmpTargetID").Visible = False
                Me.grdSaved.RootTable.Columns("EmployeeID").Visible = False
                Me.grdSaved.RootTable.Columns("UserID").Visible = False
                Me.grdSaved.RootTable.Columns("Target Weight").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns("Target Value").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdSaved.RootTable.Columns("Target Weight").FormatString = "N" & DecimalPointInQty
                Me.grdSaved.RootTable.Columns("Target Value").FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("Target Weight").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("Target Value").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("Target Weight").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("Target Value").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdSaved.AutoSizeColumns()
            ElseIf Condition = "Detail" Then
                Dim dtDetail As New DataTable
                dtDetail = GetDataTable("SELECT dbo.tblListCity.CityId, dbo.tblListTerritory.TerritoryId, dbo.tblListCity.CityName AS City, dbo.tblListTerritory.TerritoryName AS Area FROM   dbo.tblListCity  INNER JOIN dbo.tblListTerritory ON dbo.tblListCity.CityId = dbo.tblListTerritory.CityId Order BY CityName ASC")
                dtDetail.AcceptChanges()
                Dim dtArticleType As DataTable
                dtArticleType = GetDataTable("Select ArticleTypeID, ArticleTypeName From ArticleTypeDefTable Order BY ArticleTypeName ASC")
                dtArticleType.AcceptChanges()
                For Each r As DataRow In dtArticleType.Rows
                    If Not dtDetail.Columns.Contains(r.Item("ArticleTypeName").ToString()) Then
                        dtDetail.Columns.Add(r("ArticleTypeID"), GetType(System.Int32), r("ArticleTypeID"))
                        dtDetail.Columns.Add(r("ArticleTypeName"), GetType(System.String))
                        dtDetail.Columns.Add(r("ArticleTypeID") & "^" & "Weight", GetType(System.Double))
                        dtDetail.Columns.Add(r("ArticleTypeID") & "^" & "Value", GetType(System.Double))
                    End If
                Next
                dtDetail.AcceptChanges()

                For Each r As DataRow In dtDetail.Rows
                    For c As Integer = enmDetail.Count To dtDetail.Columns.Count - 4 Step 4
                        r.BeginEdit()
                        r(c + 2) = 0
                        r(c + 3) = 0
                        r.EndEdit()
                    Next
                Next

                Dim dtData As New DataTable
                dtData = GetDataTable("Select * from SalesManTargetDetailTable WHERE EmpTargetID=" & intEmpTargetID & "")
                dtData.AcceptChanges()


                For Each r As DataRow In dtDetail.Rows
                    Dim dr() As DataRow = dtData.Select("CityID=" & Val(r.Item("CityId").ToString) & " AND TerritoryID=" & Val(r.Item("TerritoryId").ToString) & "")
                    If dr IsNot Nothing Then
                        If dr.Length > 0 Then
                            For Each drFound As DataRow In dr
                                r.BeginEdit()
                                r(dtDetail.Columns.IndexOf(drFound(enmTargetDetail.ArticleTypeID)) + 2) = Val(drFound(enmTargetDetail.Target_Weight).ToString)
                                r(dtDetail.Columns.IndexOf(drFound(enmTargetDetail.ArticleTypeID)) + 3) = Val(drFound(enmTargetDetail.Target_Value).ToString)
                                r.EndEdit()
                            Next
                        End If
                    End If
                Next



                Dim strTotalWeight As String = String.Empty
                Dim strTotalValue As String = String.Empty

                For c As Integer = enmDetail.Count To dtDetail.Columns.Count - 4 Step 4

                    If strTotalWeight.Length > 0 Then
                        strTotalWeight += "+" & "[" & dtDetail.Columns(c + 2).ColumnName.ToString & "]"
                    Else
                        strTotalWeight = "[" & dtDetail.Columns(c + 2).ColumnName.ToString & "]"
                    End If

                    If strTotalValue.Length > 0 Then
                        strTotalValue += "+" & "[" & dtDetail.Columns(c + 3).ColumnName.ToString & "]"
                    Else
                        strTotalValue = "[" & dtDetail.Columns(c + 3).ColumnName.ToString & "]"
                    End If

                Next

                dtDetail.Columns.Add("Total_Weight", GetType(System.Double))
                dtDetail.Columns.Add("Total_Value", GetType(System.Double))



                dtDetail.Columns("Total_Weight").Expression = strTotalWeight.ToString
                dtDetail.Columns("Total_Value").Expression = strTotalValue.ToString

                dtDetail.AcceptChanges()



                Me.grdTargetSetup.DataSource = dtDetail
                Me.grdTargetSetup.RetrieveStructure()


                Me.grdTargetSetup.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
                Me.grdTargetSetup.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

                Me.grdTargetSetup.RootTable.CellLayoutMode = Janus.Windows.GridEX.CellLayoutMode.UseColumnSets
                Dim ColumnSet As New Janus.Windows.GridEX.GridEXColumnSet
                Dim ColumnSet1 As New Janus.Windows.GridEX.GridEXColumnSet
                Dim ColumnSet2 As New Janus.Windows.GridEX.GridEXColumnSet

                Me.grdTargetSetup.RootTable.ColumnSetRowCount = 1
                ColumnSet = Me.grdTargetSetup.RootTable.ColumnSets.Add
                ColumnSet.ColumnCount = 2
                ColumnSet.Caption = "Area Detail"
                ColumnSet.Add(Me.grdTargetSetup.RootTable.Columns("City"), 0, 0)
                ColumnSet.Add(Me.grdTargetSetup.RootTable.Columns("Area"), 0, 1)

                Me.grdTargetSetup.RootTable.Columns(enmDetail.CityID).Visible = False
                Me.grdTargetSetup.RootTable.Columns(enmDetail.TerritoryID).Visible = False

                For c As Integer = enmDetail.Count To grdTargetSetup.RootTable.Columns.Count - 4 Step 4

                    If Me.grdTargetSetup.RootTable.Columns(c).DataMember <> "Total_Weight" AndAlso Me.grdTargetSetup.RootTable.Columns(c).DataMember <> "Total_Value" Then

                        Me.grdTargetSetup.RootTable.Columns(c).Visible = False

                        Me.grdTargetSetup.RootTable.Columns(c + 2).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                        Me.grdTargetSetup.RootTable.Columns(c + 3).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                        Me.grdTargetSetup.RootTable.Columns(c + 2).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.grdTargetSetup.RootTable.Columns(c + 2).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                        Me.grdTargetSetup.RootTable.Columns(c + 3).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                        Me.grdTargetSetup.RootTable.Columns(c + 3).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

                        Me.grdTargetSetup.RootTable.Columns(c + 2).Caption = "Weight In Kg"
                        Me.grdTargetSetup.RootTable.Columns(c + 3).Caption = "Value"

                        ColumnSet1 = Me.grdTargetSetup.RootTable.ColumnSets.Add
                        ColumnSet1.ColumnCount = 2
                        ColumnSet1.HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
                        ColumnSet1.Caption = Me.grdTargetSetup.RootTable.Columns(c + 1).Caption
                        ColumnSet1.Add(Me.grdTargetSetup.RootTable.Columns(c + 2), 0, 0)
                        ColumnSet1.Add(Me.grdTargetSetup.RootTable.Columns(c + 3), 0, 1)

                        Me.grdTargetSetup.RootTable.Columns(c + 2).AllowSort = False
                        Me.grdTargetSetup.RootTable.Columns(c + 3).AllowSort = False
                    End If
                Next
                ColumnSet2 = Me.grdTargetSetup.RootTable.ColumnSets.Add
                ColumnSet2.ColumnCount = 2
                ColumnSet2.Caption = ""
                ColumnSet2.Add(Me.grdTargetSetup.RootTable.Columns("Total_Weight"), 0, 0)
                ColumnSet2.Add(Me.grdTargetSetup.RootTable.Columns("Total_Value"), 0, 1)

                Me.grdTargetSetup.RootTable.Columns("Total_Value").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
                Me.grdTargetSetup.RootTable.Columns("Total_Weight").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

                Me.grdTargetSetup.RootTable.Columns("Total_Weight").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdTargetSetup.RootTable.Columns("Total_Weight").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdTargetSetup.RootTable.Columns("Total_Value").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
                Me.grdTargetSetup.RootTable.Columns("Total_Value").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


                Me.grdTargetSetup.AutoSizeColumns()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbEmployees Is Nothing Then
                ShowErrorMessage("Invalid Sale Person.")
                Me.cmbEmployees.Focus()
                Return False
            End If
            If Me.cmbEmployees.ActiveRow.Cells(0).Value = 0 Then
                ShowErrorMessage("Please Select Sale Person")
                Me.cmbEmployees.Focus()
                Return False
            End If

            If Me.txtYear.Text.Length < 4 Then
                ShowErrorMessage("Invalid Year.")
                Me.txtYear.Focus()
                Return False
            End If
            If Me.cmbMonth.SelectedIndex = -1 Then
                ShowErrorMessage("Invalid Month")
                Me.cmbMonth.Focus()
                Return False
            End If
            If Me.grdTargetSetup.RowCount = 0 Then
                ShowErrorMessage("Record not in grid.")
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            intEmpTargetID = 0I
            Me.cmbEmployees.Rows(0).Activate()
            Me.txtYear.Text = DateTime.Now.Year
            Me.cmbMonth.SelectedValue = Date.Now.Month
            GetAllRecords("Master")
            GetAllRecords("Detail")
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
            Me.cmbEmployees.Focus()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Dim objCon As New OleDb.OleDbConnection(Con.ConnectionString)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim trans As OleDb.OleDbTransaction = objCon.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand

        Try
            cmd.Connection = objCon
            cmd.Transaction = trans
            cmd.CommandType = CommandType.Text
            cmd.CommandTimeout = 300


            Dim dStartDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Val(Me.cmbMonth.SelectedValue) & "-1")
            Dim dEndDate As DateTime = CDate(Val(Me.txtYear.Text) & "-" & Val(Me.cmbMonth.SelectedValue) & "-" & Date.DaysInMonth(dStartDate.Year, dStartDate.Month) & "")



            Dim CurrentID As Integer = 0I

            If intEmpTargetID <= 0 Then

                cmd.CommandText = ""
                cmd.CommandText = "INSERT INTO SalesManTargetMasterTable(EmployeeID,Target_YEAR,Target_Month,StartDate,EndDate,UserID,UserName) " _
                    & " VALUES(" & Me.cmbEmployees.Value & "," & Val(Me.txtYear.Text) & "," & Val(Me.cmbMonth.SelectedValue) & ",Convert(DateTime,'" & dStartDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(DateTime,'" & dEndDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & LoginUserId & ",N'" & LoginUserName.Replace("'", "''") & "' ) Select @@Identity"
                CurrentID = cmd.ExecuteScalar()

            Else

                CurrentID = intEmpTargetID

                cmd.CommandText = ""
                cmd.CommandText = "UPDATE SalesManTargetMasterTable SET EmployeeID=" & Me.cmbEmployees.Value & ",Target_YEAR=" & Val(Me.txtYear.Text) & ",Target_Month=" & Val(Me.cmbMonth.SelectedValue) & ",StartDate=Convert(DateTime,'" & dStartDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),EndDate=Convert(DateTime,'" & dEndDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102) WHERE EmpTargetID=" & intEmpTargetID & ""
                cmd.ExecuteNonQuery()

                cmd.CommandText = ""
                cmd.CommandText = "Delete From SalesManTargetDetailTable WHERE EmpTargetID=" & intEmpTargetID & ""
                cmd.ExecuteNonQuery()


            End If

            For Each jRow As Janus.Windows.GridEX.GridEXRow In Me.grdTargetSetup.GetRows
                For c As Integer = enmDetail.Count To Me.grdTargetSetup.RootTable.Columns.Count - 4 Step 4
                    If Me.grdTargetSetup.RootTable.Columns(c).DataMember <> "Total_Weight" AndAlso Me.grdTargetSetup.RootTable.Columns(c).DataMember <> "Total_Value" Then
                        If Val(jRow.Cells(c + 2).Value.ToString) > 0 Then
                            cmd.CommandText = ""
                            cmd.CommandText = "INSERT INTO SalesManTargetDetailTable(EmpTargetID, EmployeeID,Target_YEAR,Target_Month,StartDate,EndDate,Target_Weight, Target_Value,CityID,TerritoryID, ArticleTypeID) " _
                                & " VALUES(" & CurrentID & ", " & Me.cmbEmployees.Value & "," & Val(Me.txtYear.Text) & "," & Val(Me.cmbMonth.SelectedValue) & ",Convert(DateTime,'" & dStartDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Convert(DateTime,'" & dEndDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & Val(jRow.Cells(c + 2).Value.ToString) & " ," & Val(jRow.Cells(c + 3).Value.ToString) & "," & Val(jRow.Cells("CityID").Value.ToString) & "," & Val(jRow.Cells("TerritoryID").Value.ToString) & "," & Val(jRow.Cells(c).Value.ToString) & ") Select @@Identity"
                            cmd.ExecuteNonQuery()
                        End If
                    End If
                Next
            Next


            cmd.CommandText = ""
            cmd.CommandText = "Update SalesManTargetMasterTable Set Total_Target_Weight=IsNull(a.TargetWeight,0),Total_Target_Value=IsNull(a.TargetValue,0) From SalesManTargetMasterTable, (Select EmpTargetID, SUM(IsNull(Target_Weight,0)) as TargetWeight, SUM(IsNull(Target_Value,0)) as TargetValue From  SalesManTargetDetailTable Group By EmpTargetID) a WHERE a.EmpTargetID = SalesManTargetMasterTable.EmpTargetID AND SalesManTargetMasterTable.EmpTargetID=" & CurrentID & ""
            cmd.ExecuteNonQuery()

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    Private Sub frmDefEmployeeMonthlyTarget_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.Cursor = Cursors.WaitCursor
        Try
            FillCombos()
            FillCombos("Month")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            Dim id As Integer = 0I
            If Me.cmbEmployees.ActiveRow Is Nothing Then
                Me.cmbEmployees.Rows(0).Activate()
            End If
            id = Me.cmbEmployees.ActiveRow.Cells(0).Value
            FillCombos()
            Me.cmbEmployees.Value = id
            Me.txtYear.Text = DateTime.Now.Year
            id = Me.cmbMonth.SelectedIndex
            FillCombos("Month")
            Me.cmbMonth.SelectedIndex = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try

            If msg_Confirm(str_ConfirmDelete) = False Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                Else
                    If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdTargetSetup_ColumnHeaderClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdTargetSetup.ColumnHeaderClick
        Try

            If e.Column.DataMember <> "City" AndAlso e.Column.DataMember <> "Area" AndAlso e.Column.DataMember <> "Total_Weight" AndAlso e.Column.DataMember <> "Total_Value" Then
                For Each r As Janus.Windows.GridEX.GridEXRow In Me.grdTargetSetup.GetRows
                    r.BeginEdit()
                    r.Cells(e.Column.Index).Value = Val(Me.grdTargetSetup.GetRows(0).Cells(e.Column.Index).Value.ToString)
                    r.EndEdit()
                Next
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdSaved.GetRow Is Nothing AndAlso grdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmDefEmployeeMonthlyTarget"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdSaved.CurrentRow.Cells(2).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Employee Monthly Target (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Accounts
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub
End Class
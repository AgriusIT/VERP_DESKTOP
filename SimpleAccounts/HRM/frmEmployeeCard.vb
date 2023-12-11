Imports System.Data.SqlClient
Imports Neodynamic.SDK.Barcode

Public Class frmEmployeeCard
    'Dim cn As New SqlConnection("Data Source=.\SQL2K12;Initial Catalog=bilal_erp_db;Persist Security Info=True;User ID=sa;Password=sa")

    Private Sub frmEmployeeCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseKey = "KD83KNYYMA6XA6E7HNEL7QEUSGXGLKPQVCH7YVMEPXD7BG5FUCPA"
        'Neodynamic.SDK.Barcode.BarcodeProfessional.LicenseOwner = "LumenSoft Technologies-Standard Edition-OEM Developer License"



    End Sub

    Private Sub N1_Click(sender As Object, e As EventArgs) Handles N1.Click
        Try
            'Dim adp As New SqlDataAdapter("SELECT  [EmployeeDeptId] ,[EmployeeDeptName] FROM [EmployeeDeptDefTable] where Active=1", cn)
            Dim query As String = "SELECT  [EmployeeDeptId] ,[EmployeeDeptName] FROM [EmployeeDeptDefTable] where Active=1"
            Dim dt As New DataTable
            'adp.Fill(dt)
            dt = GetDataTable(query)
            Me.lstViewDepartments.Items.Clear()
            For Each row As DataRow In dt.Rows
                Dim listItem As New ListViewItem
                listItem.Name = row.Item(0).ToString
                listItem.Text = row.Item(1).ToString
                listItem.ImageKey = 1
                Me.lstViewDepartments.Items.Add(listItem)
            Next
            If Not Me.TabControl1.SelectedIndex = Me.TabControl1.TabCount - 1 Then Me.TabControl1.SelectedIndex = Me.TabControl1.SelectedIndex + 1
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub N2_Click(sender As Object, e As EventArgs) Handles N2.Click
        Try
            Dim strSQL As String = "SELECT tblDefEmployee.Employee_ID, EmployeeDeptDefTable.EmployeeDeptName, tblDefEmployee.Employee_Code, tblDefEmployee.Employee_Name " _
                                    & " FROM EmployeeDeptDefTable LEFT OUTER JOIN " _
                                    & " tblDefEmployee ON EmployeeDeptDefTable.EmployeeDeptId = tblDefEmployee.Dept_ID " _
                                    & " WHERE (EmployeeDeptDefTable.Active = 1) AND (isnull(tblDefEmployee.Active,1) = 1 )"

            If Me.lstViewDepartments.SelectedItems.Count > 0 Then

                Dim strDeptIds As String = String.Empty
                For Each item As ListViewItem In Me.lstViewDepartments.SelectedItems
                    If strDeptIds.Trim.Length > 0 Then
                        strDeptIds = strDeptIds & ","
                    End If
                    strDeptIds = strDeptIds & item.Name
                Next

                If strDeptIds.Length > 0 Then strSQL = strSQL & " and EmployeeDeptDefTable.EmployeeDeptId in (" & strDeptIds & ")"

            End If

            'Dim adp As New SqlDataAdapter(strSQL, cn)
            Dim dt As New DataTable
            'adp.Fill(dt)
            dt = GetDataTable(strSQL)
            Me.GridEX1.DataSource = dt
            Me.GridEX1.RetrieveStructure()
            Me.GridEX1.AutoSizeColumns()
            Me.GridEX1.RootTable.Columns(0).Visible = False
            Dim grp As New Janus.Windows.GridEX.GridEXGroup
            grp.Column = Me.GridEX1.RootTable.Columns(1)
            Me.GridEX1.RootTable.Groups.Add(grp)

            Dim col As New Janus.Windows.GridEX.GridEXColumn
            col.ActAsSelector = True
            Me.GridEX1.RootTable.Columns.Add(col)
            Me.GridEX1.CheckAllRecords()
            If Not Me.TabControl1.SelectedIndex = Me.TabControl1.TabCount - 1 Then Me.TabControl1.SelectedIndex = Me.TabControl1.SelectedIndex + 1

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub N3_Click(sender As Object, e As EventArgs) Handles N3.Click
        'Try
        '    Dim strSQL As String = "SELECT EmployeesView.Employee_ID, EmployeesView.NIC, EmployeesView.Gender, EmployeesView.EmployeeDesignationName, EmployeesView.Blood_Group, EmployeesView.EmpPicture, EmployeesView.DOB, EmployeesView.Employee_ID, EmployeesView.EmployeeDeptName, EmployeesView.Employee_Code, EmployeesView.Employee_Name, Convert(image, null) as Picture " _
        '                               & " FROM            EmployeesView " _
        '                               & " WHERE (isnull(EmployeesView.Active,1) = 1 )"
        '    If GridEX1.RootTable.Columns(4).ActAsSelector = True Then
        '        Dim empID As String = String.Empty
        '        For Each ROW As Janus.Windows.GridEX.GridEXRow In GridEX1.GetCheckedRows
        '            If empID.Length > 0 Then
        '                empID += ", " & ROW.Cells(0).Value
        '            Else
        '                empID = empID & ROW.Cells(0).Value
        '            End If
        '        Next

        '        strSQL = strSQL & "AND EmployeesView.Employee_ID IN (" & empID & ")"
        '    End If
        '    Dim DT As New DataTable
        '    DT = GetDataTable(strSQL)
        '    DT.AcceptChanges()
        '    For Each DR As DataRow In DT.Rows
        '        DR.BeginEdit()
        '        LoadPicture(DR, "Picture", DR.Item("EmpPicture"))
        '        DR.EndEdit()
        '    Next
        '    ShowReport("Employee_Card", , , , , , , DT)

        If Not Me.TabControl1.SelectedIndex = Me.TabControl1.TabCount - 1 Then Me.TabControl1.SelectedIndex = Me.TabControl1.SelectedIndex + 1
        'Catch ex As Exception
        '    MsgBox(ex.Message, MsgBoxStyle.Critical)
        'End Try
    End Sub

    Private Sub N4_Click(sender As Object, e As EventArgs) Handles N4.Click
        Try

            Dim strSQL As String = "SELECT EmployeesView.Employee_ID, EmployeesView.NIC, EmployeesView.Gender, EmployeesView.EmployeeDesignationName, EmployeesView.Blood_Group, EmployeesView.EmpPicture, EmployeesView.DOB, EmployeesView.Employee_ID, EmployeesView.EmployeeDeptName, EmployeesView.Employee_Code, EmployeesView.Employee_Name, Convert(image, null) as Picture, " _
                                       & " EmployeesView.Joining_Date, Convert(image, null) as BarCode " _
                                       & " FROM EmployeesView " _
                                       & " WHERE (isnull(EmployeesView.Active,1) = 1 )"
            If GridEX1.RootTable.Columns(4).ActAsSelector = True Then


                Dim empID As String = String.Empty
                For Each ROW As Janus.Windows.GridEX.GridEXRow In GridEX1.GetCheckedRows

                    If Not IsDBNull(ROW.Cells(0).Value) Then
                        If empID.Length > 0 Then

                            empID += "," & ROW.Cells(0).Value


                        Else
                            empID = empID & ROW.Cells(0).Value
                        End If
                    End If

                Next

                If empID.Length > 0 Then strSQL = strSQL & "AND EmployeesView.Employee_ID IN (" & empID & ")"

            End If
            Dim DT As New DataTable
            DT = GetDataTable(strSQL)
            DT.AcceptChanges()
            For Each DR As DataRow In DT.Rows
                DR.BeginEdit()
                Dim bcp As New BarcodeProfessional()
                'bcp.Symbology = Symbology.Code39
                'bcp.Symbology = Symbology.Code93



                bcp.Extended = True
                bcp.DisplayCode = False

                ' bcp.Text=Symbology.
                'bcp.Text = String.Empty
                bcp.Symbology = Symbology.Code128

                bcp.AddChecksum = False

                'bcp.BarWidth = 3
                'bcp.BarHeight = 0.04F
                bcp.Code = "?" & DR.Item("Employee_Code").ToString
                'bcp.Code = DR.Item("Employee_Code").ToString
                DR.Item("BarCode") = bcp.GetBarcodeImage(System.Drawing.Imaging.ImageFormat.Png)
                LoadPicture(DR, "Picture", DR.Item("EmpPicture"))
                DR.EndEdit()
            Next
            ShowReport("Employee_Card", , , , , , , DT)
            If Not Me.TabControl1.SelectedIndex = Me.TabControl1.TabCount - 1 Then Me.TabControl1.SelectedIndex = Me.TabControl1.SelectedIndex + 1
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        Me.Close()
    End Sub
    Private Sub P3_Click(sender As Object, e As EventArgs) Handles P3.Click
        If Not Me.TabControl1.SelectedIndex = Me.TabControl1.TabCount + 1 Then Me.TabControl1.SelectedIndex = Me.TabControl1.SelectedIndex - 1
    End Sub

    Private Sub P4_Click(sender As Object, e As EventArgs) Handles P4.Click
        If Not Me.TabControl1.SelectedIndex = Me.TabControl1.TabCount + 1 Then Me.TabControl1.SelectedIndex = Me.TabControl1.SelectedIndex - 1
    End Sub

    Private Sub P2_Click(sender As Object, e As EventArgs) Handles P2.Click
        If Not Me.TabControl1.SelectedIndex = Me.TabControl1.TabCount + 1 Then Me.TabControl1.SelectedIndex = Me.TabControl1.SelectedIndex - 1
    End Sub

    Private Sub frmEmployeeCard_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

    End Sub

    Private Function Neodynamic() As Object
        Throw New NotImplementedException
    End Function

End Class
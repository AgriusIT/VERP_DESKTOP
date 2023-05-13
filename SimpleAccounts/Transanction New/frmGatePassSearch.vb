''12-jan-2018 task# 2087   Muhammad Abdullah new form created
Imports SBDal
Imports SBModel
Public Class frmGatePassSearch
    Public ResultGateList As List(Of GatePassM)
    Private Sub grdItems_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdItems.FormattingRow

    End Sub
    Private Sub frmGatePassSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToolTip1.SetToolTip(Me.dtFrom, "Select Initial Date ")
        ToolTip1.SetToolTip(Me.dtTo, "Select End Date ")
        ToolTip1.SetToolTip(Me.txtDetail, "Enter Detail For Search ")
        ToolTip1.SetToolTip(Me.Button1, "Click For Search ")
        ToolTip1.SetToolTip(Me.OK_Button, "Click To Load Selected Data ")
        ToolTip1.SetToolTip(Me.Cancel_Button, "Click To Cancel and Close ")
        ToolTip1.SetToolTip(Me.cmbDocNo, "Select Doc No For Search ")

        Try
            dtFrom.Value = DateTime.Now.AddMonths(-1)
            dtFrom.Checked = False
            FillCombos()
            cmbDocNo.SelectedIndex = -1
            Me.grdItems.DataSource = Nothing
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            Me.cmbDocNo.DisplayMember = "Issue_No"
            Me.cmbDocNo.ValueMember = "Issue_id"
            Dim strQuery As String = String.Empty
            'strQuery = " SELECT Issue_id, Issue_No from GatepassMasterTable order by Issue_id DESC"

            ''Aashir: 4269: Received Gatepass Should not show.
            strQuery = " SELECT OutTable.Issue_id, OutTable.Issue_No  " _
                            & "  FROM (SELECT     GatepassMasterTable.Issue_id, GatepassMasterTable.Issue_No,  " _
                            & "  GatepassDetailTable.SrNo, GatepassDetailTable.IssueQty  " _
                            & "  FROM GatepassMasterTable INNER JOIN  " _
                            & "  GatepassDetailTable ON GatepassMasterTable.Issue_id = GatepassDetailTable.Issue_Id) AS OutTable LEFT OUTER JOIN  " _
                            & "  (SELECT outDetailSrNo, outMasterId, SUM(InQuantity) AS InQty  " _
                            & "  FROM PartialInDetailTable  " _
                            & "  GROUP BY outDetailSrNo, outMasterId) AS InTable ON OutTable.Issue_id = InTable.outMasterId AND OutTable.SrNo = InTable.outDetailSrNo   " _
                            & "  where ISNULL(OutTable.IssueQty, 0) - ISNULL(InTable.InQty, 0) > 0 order by Issue_id DESC"
            ''End 4269
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strQuery)
            Me.cmbDocNo.DataSource = dt
        Catch ex As Exception
            Throw ex
        End Try
        '' New CustomerDAL().getBusinessType
    End Sub
    Public Sub FillGrid()
        Try
            Dim strQuery As String = String.Empty

            'strQuery += " SELECT       GatepassMasterTable.Issue_id, GatepassMasterTable.Issue_No, GatepassMasterTable.Issue_date,  GatepassMasterTable.Issue_to,   "
            'strQuery += "   GatepassDetailTable.SrNo, GatepassDetailTable.IssueDetail, GatepassDetailTable.Reference, GatepassDetailTable.Comments,"
            'strQuery += "  GatepassDetailTable.IssueQty, ISNULL"
            'strQuery += "          ((SELECT        SUM(InQuantity) AS Expr1 "
            'strQuery += "    FROM            PartialInDetailTable AS inDetail"
            'strQuery += "   WHERE        (outDetailSrNo = GatepassDetailTable.SrNo)), 0) AS InQty, "
            'strQuery += "	 ISNULL(GatepassDetailTable.IssueQty - ISNULL((SELECT        SUM(InQuantity) AS Expr1"
            'strQuery += "    FROM            PartialInDetailTable AS inDetail"
            'strQuery += "     WHERE        (outDetailSrNo = dbo.GatepassDetailTable.SrNo)), 0), 0) AS Balance"
            'strQuery += "   FROM      GatepassMasterTable LEFT OUTER JOIN"
            'strQuery += "         GatepassDetailTable ON GatepassDetailTable.Issue_Id = GatepassMasterTable.Issue_id where 1 = 1 "

            strQuery = "SELECT        OutTable.Issue_id, OutTable.Issue_No, OutTable.Issue_date, OutTable.Issue_to, OutTable.SrNo, OutTable.IssueDetail, OutTable.Reference, " _
                        & " OutTable.Comments, OutTable.IssueQty, IsNull(InTable.InQty,0) as InQty, IsNull(OutTable.IssueQty,0) - IsNull(InTable.InQty,0) AS Balance " _
                        & " FROM            (SELECT        GatepassMasterTable.Issue_id, GatepassMasterTable.Issue_No, GatepassMasterTable.Issue_date, GatepassMasterTable.Issue_to,  " _
                        & " GatepassDetailTable.SrNo, GatepassDetailTable.IssueDetail, " _
                        & " GatepassDetailTable.IssueQty, GatepassDetailTable.Reference, GatepassDetailTable.Comments " _
                        & " FROM            GatepassMasterTable INNER JOIN " _
                        & " GatepassDetailTable ON GatepassMasterTable.Issue_id = GatepassDetailTable.Issue_Id) AS OutTable LEFT OUTER JOIN " _
                        & " (SELECT        outDetailSrNo, outMasterId, SUM(InQuantity) AS InQty " _
                        & " FROM PartialInDetailTable " _
                        & " GROUP BY outDetailSrNo, outMasterId) AS InTable ON OutTable.Issue_id = InTable.outMasterId AND OutTable.SrNo = InTable.outDetailSrNo    where (IsNull(OutTable.IssueQty,0)   - IsNull(InTable.InQty,0)   ) > 0 "


            If dtFrom.Checked = True Then
                strQuery += " and Issue_date >= '" & dtFrom.Value.ToString() & "'"
            End If
            If dtTo.Checked = True Then
                strQuery += " and Issue_date <= '" & dtTo.Value.ToString() & "'"
            End If
            If cmbDocNo.SelectedIndex > -1 Then
                strQuery += " and OutTable.Issue_id = '" & cmbDocNo.SelectedValue.ToString() & "'"
            End If
            If txtDetail.Text.Trim() <> "" Then
                strQuery += " and IssueDetail like '%" & txtDetail.Text.Replace(" ", "%").ToString() & "%'"
            End If
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strQuery)
            grdItems.DataSource = dt
            grdItems.Refetch()

        Catch ex As Exception
            Throw ex
        End Try
        ' New CustomerDAL().getBusinessType
    End Sub
    'Public Function GetColumnList(TableName As String, Optional IncludePrimayKey As Boolean = True, Optional IncludeBracketsArroundColumnNames As Boolean = False) As String
    '    Dim resultString As String = ""
    '    Dim strQuery As String = " SELECT Column_name FROM   INFORMATION_SCHEMA.COLUMNS  WHERE TABLE_NAME = N'" & TableName & "'"
    '    Dim dt As New DataTable
    '    dt = UtilityDAL.GetDataTable(strQuery)
    '    If IncludePrimayKey = False Then
    '        strQuery = "SELECT column_name FROM INFORMATION_SCHEMA.COLUMNS  where table_name = N 'ArticleStatus' " &
    '            "and column_name not in (select column_Name from information_schema.key_column_usage where table_name = N'ArticleStatus') order by column_name "
    '        dt = UtilityDAL.GetDataTable(strQuery)
    '    End If
    '    If (IncludeBracketsArroundColumnNames = False) Then
    '        For i As Integer = 0 To dt.Rows.Count() - 1
    '            resultString += dt.Rows(i)("Column_name").ToString()
    '            If i < dt.Rows.Count() - 1 Then
    '                resultString += ", "
    '            End If
    '        Next
    '        Return resultString
    '    Else
    '        For i As Integer = 0 To dt.Rows.Count() - 1
    '            resultString += "[" & dt.Rows(i)("Column_name").ToString() & "]"
    '            If i < dt.Rows.Count() - 1 Then
    '                resultString += ", "
    '            End If
    '        Next
    '        Return resultString
    '    End If


    '    Return strQuery
    'End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            FillGrid()
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

        'MessageBox.Show(GetColumnList("GatepassMasterTable", True, True))
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Try

        Catch ex As Exception

        End Try

        Dim gateList As New List(Of GatePassM)
        Dim str As String = ""
        For i As Integer = 0 To grdItems.RowCount - 1
            If Convert.ToBoolean(grdItems.GetRows(i).Cells("CheckBox").Value.ToString()) = True Then

                Dim gatePass As GatePassM = New GatePassM()
                gatePass.SrNo = grdItems.GetRows(i).Cells("SrNo").Value.ToString()
                gatePass.Issue_date = grdItems.GetRows(i).Cells("Issue_date").Value.ToString()
                gatePass.Issue_id = grdItems.GetRows(i).Cells("Issue_id").Value.ToString()
                gatePass.Issue_No = grdItems.GetRows(i).Cells("Issue_No").Value.ToString()
                gatePass.IssueDetail = grdItems.GetRows(i).Cells("IssueDetail").Value.ToString()
                gatePass.Issue_to = grdItems.GetRows(i).Cells("Issue_to").Value.ToString()
                gatePass.Reference = grdItems.GetRows(i).Cells("Reference").Value.ToString()
                gatePass.Comments = grdItems.GetRows(i).Cells("Comments").Value.ToString()
                gatePass.IssueQty = grdItems.GetRows(i).Cells("IssueQty").Value.ToString()
                gatePass.InQty = grdItems.GetRows(i).Cells("InQty").Value.ToString()
                gatePass.Balance = grdItems.GetRows(i).Cells("Balance").Value.ToString()
                gateList.Add(gatePass)
            End If

        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.ResultGateList = gateList
        'MessageBox.Show(str)
    End Sub
   

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

   

    Private Sub frmGatePassSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()
            End If
        Catch ex As Exception

        End Try
       
    End Sub
End Class

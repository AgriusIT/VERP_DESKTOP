Imports System.Windows.Forms.DataVisualization.Charting
Imports SBDal
Imports SBModel
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data.OleDb
Public Class frmOpportunityChart
    Dim UserId As Integer = 0
    Private Sub frmOpportunityChart_Load(sender As Object, e As EventArgs) Handles Me.Load

        LoadData()
        GetSecurityRights()
    End Sub

    Private Sub LoadData()
        Chart1.DataSource = GetData()
        Chart1.Series("Stages").XValueMember = "TotalAmount1"
        Chart1.Series("Stages").YValueMembers = "Total"
        '  Chart1.Series("Stages").LabelFormat = "{#VALX,##0}"
        ' Chart1.Series("Stages").LegendText = "#VALX  #TotalAmount"
        'Chart1.Series("Stages").Label = "#VALY  #AXISLABEL"
        'Chart1.Series("Stages")("PieLabelStyle") = "outside"
    End Sub
    Private Function GetData() As DataTable
        Dim strSQL As String = String.Empty
        Try
            'Rafay:Modified query to add country name
            If LoginUser.LoginUserGroup = "Administrator" Then
                strSQL = "SELECT COUNT( tblDefOpportunity.OpportunityId) as total, " _
                & " concat (tblDefOpportunityStage.Title, ' = ' , cast(sum(tblDefOpportunity.TotalAmount) As bigint)) as TotalAmount1 " _
                & "  FROM tblDefOpportunity INNER JOIN " _
                & " tblDefOpportunityStage ON tblDefOpportunity.StageId = tblDefOpportunityStage.Id " _
                & " group by tblDefOpportunityStage.Title "
            Else
                strSQL = "SELECT COUNT( tblDefOpportunity.OpportunityId) as total, " _
                & "concat (tblDefOpportunityStage.Title, ' = ' , cast(sum(tblDefOpportunity.TotalAmount) As bigint) ) as TotalAmount1 " _
                & "  FROM tblDefOpportunity INNER JOIN " _
                & " tblDefOpportunityStage ON tblDefOpportunity.StageId = tblDefOpportunityStage.Id " _
                & "	where tblDefOpportunity.UserId = " & LoginUser.LoginUserId & " group by tblDefOpportunityStage.Title "
            End If
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Dim objCon As New OleDbConnection(Con.ConnectionString)
    'If objCon.State = ConnectionState.Open Then objCon.Close()
    'objCon.Open()
    'Try
    '    Dim dtchart As New DataTable()            
    '    GetUser(LoginUserId)
    '    'Dim cmd As New OleDbCommand("OpportunityChart" & UserId & "", objCon)
    '    Dim cmd As New OleDbCommand("OpportunityChart ", objCon)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@UserId", OleDbType.Integer).Value = LoginUserId
    '    Dim dr As OleDbDataReader = cmd.ExecuteReader
    '    dtchart.Load(dr)
    '    Return dtchart
    'Catch ex As Exception
    '    Throw ex

    'Finally
    '    objCon.Close()
    'End Try
    Private Sub Chart1_Click(sender As Object, e As EventArgs) Handles Chart1.Click
        'Try
        '    Dim pointindex As Integer
        '    Dim result As HitTestResult
        '    result = Chart1.HitTest(Cursor.Position.X, Cursor.Position.Y)
        '    If result.ChartElementType = ChartElementType.DataPoint Then
        '        pointindex = result.PointIndex
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub


    Public Function GetUser(ByVal UserId As Integer) As DataTable
        Dim Str As String = String.Empty
        Try
            Str = "SELECT User_ID FROM tblUser WHERE tblUser.User_ID =" & UserId & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(Str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Exit Sub
            End If
            Me.Visible = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Chart1_MouseUp(sender As Object, e As MouseEventArgs) Handles Chart1.MouseUp
        'Try
        '    Dim pointindex As Integer
        '    Dim result As HitTestResult
        '    result = Chart1.HitTest(Cursor.Position.X, Cursor.Position.Y)
        '    If result.ChartElementType = ChartElementType.DataPoint Then
        '        pointindex = result.PointIndex
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try
    End Sub
    Private Function pieHitPointIndex(ByVal pie As Chart, ByVal e As MouseEventArgs) As Integer
        'Dim hitPiece As HitTestResult = pie.HitTest(e.X, e.Y, ChartElementType.DataPoint)
        'Dim hitLegend As HitTestResult = pie.HitTest(e.X, e.Y, ChartElementType.LegendItem)
        'Dim pointIndex As Integer = -1
        'If hitPiece.Series IsNot Nothing Then pointIndex = hitPiece.PointIndex
        'If hitLegend.Series IsNot Nothing Then pointIndex = hitLegend.PointIndex
        'Return pointIndex
    End Function

    Private Sub Chart1_MouseClick(sender As Object, e As MouseEventArgs) Handles Chart1.MouseClick
        'Try
        '    Dim hit As HitTestResult = Chart1.HitTest(e.X, e.Y, ChartElementType.DataPoint)

        '    If hit.PointIndex >= 0 AndAlso hit.Series IsNot Nothing Then
        '        Dim dp As DataPoint = Chart1.Series(0).Points(hit.PointIndex)
        '        Label2.Text = "Value #" & hit.PointIndex & " = " + dp.XValue
        '    Else
        '        Label2.Text = ""
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub Chart1_MouseMove(sender As Object, e As MouseEventArgs) Handles Chart1.MouseMove
        '    Dim pie As Chart = CType(sender, Chart)
        '    Dim pointIndex As Integer = pieHitPointIndex(pie, e)

        '    If pointIndex >= 0 Then
        '        Cursor = Cursors.Hand
        '    Else
        '        Cursor = Cursors.[Default]
        '    End If
    End Sub

End Class